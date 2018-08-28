using GGL.Graphic;
using System;
using System.Diagnostics;
//using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace CityGame
{
    public partial class Game
    {
        DrawData[] groundGraphics = new DrawData[262144];
        DrawData[] objectGraphics = new DrawData[262144];

        private void updateAnimator()
        {
            if (animatorTimer.ElapsedMilliseconds > 250)
            {
                animatorTimer.Restart();
                for (int i = 0; i < 20; i++)
                {
                    if (animator[i] < i) animator[i]++;
                    else animator[i] = 0;
                }
            }
        }
        private void render(object sender, EventArgs e)
        {
            
            updateAnimator();

            GL2D.ClearBuffer(Color.Blue);

            int groundGraphicsIndex = 0, objectGraphicsIndex = 0;

            Stopwatch drawtime = new Stopwatch();
            drawtime.Start();

            float posX = Cam.PosX, posY = Cam.PosY, scale = Cam.Scale;

            float size = Cam.Size;
            float scSize = (size * scale);
            int width = World.Width;
            int height = World.Height;

            int startX = width, endX = 0, startY = 0, endY = height;

            for (int ix = startX; ix >= endX; ix--)
            {
                for (int iy = startY; iy <= endY; iy++)
                {
                    int worldPosX = ix;
                    int worldPosY = iy;
                    int worldPos = worldPosX + worldPosY * width;

                    if (!(worldPosX >= 0 && worldPosY >= 0 && worldPosX < width && worldPosY < height)) continue;

                    float drawPosX = ((-posX + ix * size) * scale) + window.Width / 2f;
                    float drawPosY = ((-posY + iy * size) * scale) + window.Height / 2f;

                    if (!(drawPosX > -64 && drawPosY > -64 && drawPosX < window.Width && drawPosY < window.Height)) continue;

                    int ground = World.Ground[worldPos];// +World.GroundTile[pos];
                    float groundX = ground % 16;
                    float groundY = (ground - groundX) / 16;

                    int refX = World.ReferenceX[worldPos];
                    int refY = World.ReferenceY[worldPos];
                    int refPos = worldPos - refX - refY * World.Width;
                    int typ = World.Typ[refPos];

                    /*
                    GL2D.DrawSquare(groundTexture,
                        new PointF[] { new PointF(groundX * size, groundY * size), new PointF(groundX * size+size, groundY* size), new PointF(groundX * size + size, groundY * size + size), new PointF(groundX * size, groundY * size + size) },
                        new PointF[] {
                            new PointF(drawPosX+ World.VertexHeight[worldPos], drawPosY-World.VertexHeight[worldPos]),
                            new PointF(drawPosX+ scSize + World.VertexHeight[worldPos+1], drawPosY - World.VertexHeight[worldPos+1]),
                            new PointF(drawPosX+ scSize + World.VertexHeight[worldPos+World.Width+1], drawPosY+ scSize - World.VertexHeight[worldPos+World.Width+1]),
                            new PointF(drawPosX + World.VertexHeight[worldPos+World.Width], drawPosY+ scSize - World.VertexHeight[worldPos+World.Width]) },
                        Color.White
                        );
                        */
                    GL2D.DrawImage(groundTexture, groundX * 64, groundY * 64, 64, 64, drawPosX, drawPosY, scSize, scSize, Color.White);


                    if (World.Zone[worldPos] != 0)
                    {
                        groundGraphics[groundGraphicsIndex++].Update(zoneTexture, 0,0, 64, 64, drawPosX, drawPosY, scSize, scSize, zones[World.Zone[worldPos]].Color);
                    }
                    if (objects[typ].Ground != null)
                    {
                        Texture texture = objects[typ].Ground[0];
                        int anim = animator[texture.Height / 64 - 1];
                        groundGraphics[groundGraphicsIndex++].Update(texture, 64 * World.TileGround[worldPos], 64 * anim, 64, 64, drawPosX, drawPosY, scSize, scSize, Color.White);
                    }
                    if (objects[typ].Texture != null)
                    {
                        int tile = World.TileStruct[refPos];
                        int version = World.Version[refPos];
                        Texture texture = objects[typ].Texture[tile][version];
                        int objectSize = objects[typ].Size;
                        int anim = animator[texture.Height / texture.Width - 1];
                        int overdrawSrs = texture.Width - 64 * objectSize;
                        if (refX == objectSize - 1 || refY == 0)
                        {
                            float overdrawDst = (overdrawSrs * (scSize / 64f));

                            objectGraphics[objectGraphicsIndex++].Update(texture,
                                64 * refX, 64 * refY + (texture.Width * anim), 64 + overdrawSrs, 64 + overdrawSrs,
                                drawPosX, drawPosY - overdrawDst, scSize + overdrawDst, scSize + overdrawDst,
                                Color.White);
                        }
                        else //if (refX == 0 && refY)
                        {
                            objectGraphics[objectGraphicsIndex++].Update(texture,
                                64 * refX, 64 * refY + (texture.Width * anim) + overdrawSrs, 64, 64,
                                drawPosX, drawPosY, scSize, scSize,
                                Color.White);
                        }
                    }

                }
            }

            GL2D.DrawImage(groundGraphics, groundGraphicsIndex);
            GL2D.DrawImage(objectGraphics, objectGraphicsIndex);

            drawtime.Stop();

            Stopwatch rendertime = new Stopwatch();
            rendertime.Start();

            GL2D.UpdateBuffer();
            GL2D.UseShader(basicShader);
            GL2D.Render();


            renderBuildPreview();

            rendertime.Stop();

            Graphics g = GL2D.GetGDIContext();
            g.DrawRectangle(new Pen(Color.Red, 10), new Rectangle(100, 100, 300, 300));

            GL2D.SwapBuffers();

            Program.MenuOverlay.debugLabel.Text = "fulltime in ms: " + "-" + "\ndrawtime in ms: " + drawtime.ElapsedMilliseconds + "\nrendertime in ms: " + rendertime.ElapsedMilliseconds + "\nFPS: " + "-" + "\ndrawedTiles: " + "-" + "\nTime: " + date;

        }
        //private void RenderGround();
        unsafe private void renderBuildPreview()
        {
            if (SelectetBuildIndex == 0) return;
            if (!buildPreviewEnabled) return;

            int size = objects[SelectetBuildIndex].Size;
            int builMode = objects[SelectetBuildIndex].BuildMode;
            int width = World.Width;
            int height = World.Height;
            
            if (mouse.Button != MouseButtons.Left || builMode == 0 || builMode == 1)//single,rain
            {
                int pos = HoveredWorldPos;
                int x = pos % width;
                int y = (pos - x) / width;


                if (x >= 0 && y >= 0 && x < width && y < height)
                {
                    byte buildTyp = replaceBuildTyp((byte)SelectetBuildIndex, World.Typ[pos]);

                    Color color;
                    if (World.CanBuild((byte)buildTyp, pos) && World.TestAreaDependet((byte)buildTyp, pos) == 0) color = Color.FromArgb(150, 0, 255, 0);
                    else color = Color.FromArgb(150, 255, 0, 0);

                    if (objects[buildTyp].Ground != null) drawGroundOnPos(objects[buildTyp].Ground[0], pos, World.AutoTileGround((byte)buildTyp, pos), color);
                    if (objects[buildTyp].Texture != null) drawObjectOnPos(buildTyp,0, World.AutoTileStruct(buildTyp, pos), pos, color);
                }
            }
            else
            {
                int pos = HoveredWorldPos;
                int x = pos % width;
                int y = (pos - x) / width;

                int pos2 = DownFieldPos;
                int x2 = pos2 % width;
                int y2 = (pos2 - x2) / width;

                if (builMode == 2|| builMode == 3 || builMode == 4)//line
                {
                    byte l = 0, u = 0, r = 0, o = 0;
                    int direction = (Math.Abs(x - x2) > Math.Abs(y-y2))?0:1;
                    int dist = (direction == 0)? x - x2: y - y2;
                    bool invert = false;
                    if (dist < 0)
                    {
                        dist = -dist;
                        invert = true;
                    }
                    if (direction == 0)
                    {
                        l = 1;r = 1;
                    }
                    else
                    {
                        u = 1;o = 1;
                    }
                    bool live = true;
                    for (int i = 0; i <= dist; i++)
                    {
                        byte buildTyp = replaceBuildTyp((byte)SelectetBuildIndex, World.Typ[pos2]);

                        Color color = Color.FromArgb(150, 0, 255, 0);
                        if (World.CanBuild((byte)buildTyp, pos2) && World.TestAreaDependet((byte)buildTyp, pos2) == 0)
                        {
                            if (objects[buildTyp].Ground != null)
                                drawGroundOnPos(objects[buildTyp].Ground[0], pos2, World.applyAutoTile(buildTyp, pos2, objects[buildTyp].GroundMode, objects[buildTyp].GroundNeighbors, l, u, r, o), color);
                            if (objects[buildTyp].Texture != null)
                                drawObjectOnPos(buildTyp, 0, World.applyAutoTile(buildTyp, pos2, objects[buildTyp].StructMode, objects[buildTyp].StructNeighbors, l, u, r, o), pos2, color);
                        }
                        if (!invert)
                            pos2 += (direction == 0) ? 1 : width;
                        else pos2 -= (direction == 0) ? 1 : width;
                        //if (builMode == 4 && !live) break;
                    }
                }
                else if (builMode == 5||builMode == 6)//area
                {
                    int startX = Math.Min(x, x2);
                    int startY = Math.Min(y, y2);
                    int endX = Math.Max(x, x2);
                    int endY = Math.Max(y, y2);

                    int resetX = endX - startX;

                    pos = startX + startY * World.Width;

                    byte l = 0, u = 0, r = 0, o = 0;
                    for (int iy = startY; iy <= endY; iy++)
                    {
                        for (int ix = startX; ix <= endX; ix++)
                        {
                            l = (byte)((ix == startX) ? 0 : 1);
                            r = (byte)((ix == endX) ? 0 : 1);
                            o = (byte)((iy == startY) ? 0 : 1);
                            u = (byte)((iy == endY) ? 0 : 1);
                            if (ix >= 0 && iy >= 0 && ix < width && iy < height)
                            {
                                byte buildTyp = replaceBuildTyp((byte)SelectetBuildIndex, World.Typ[pos]);

                                pos = ix + iy * width;
                                Color color = Color.FromArgb(150, 0, 255, 0);
                                if (World.CanBuild((byte)buildTyp, pos) && World.TestAreaDependet((byte)buildTyp, pos) == 0)
                                {
                                    if (objects[buildTyp].Ground != null)
                                        drawGroundOnPos(objects[buildTyp].Ground[0], pos, World.applyAutoTile(buildTyp, pos, objects[buildTyp].GroundMode, objects[buildTyp].GroundNeighbors, l, u, r, o), color);
                                    if (objects[buildTyp].Texture != null)
                                        drawObjectOnPos(buildTyp, 0, World.applyAutoTile(buildTyp, pos, objects[buildTyp].StructMode, objects[buildTyp].StructNeighbors, l, u, r, o), pos, color);
                                }
                            }
                            pos += 1;
                        }
                        pos += width - resetX;

                    }
                }
            }

            GL2D.UpdateBuffer();
            GL2D.UseShader(glowShader);
            GL2D.Render();
            
        }

        private void drawPos(Texture texture, int pos, out float drawPosX, out float drawPosY)
        {
            float size = Cam.Size, scale = Cam.Scale;
            int width = World.Width, height = World.Height;

            float posX = pos % width;
            float posY = (pos - posX) / width;

            drawPosX = (((-Cam.PosX + posX * size) * scale) + window.Width / 2);
            drawPosY = (((-Cam.PosY + posY * size) * scale) + window.Height / 2);
        }

        private void drawGroundOnPos(Texture texture, int pos, int tile, Color color)
        {
            float scSize = Cam.Size * Cam.Scale;
            float drawPosX, drawPosY;
            drawPos(texture, pos, out drawPosX, out drawPosY);
            GL2D.DrawImage(texture, new RectangleF(64 * tile, 0, 64, 64), new RectangleF(drawPosX, drawPosY, scSize, scSize), color);
        }
        private void drawGroundOnPos(Texture texture, int pos, Color color)
        {
            drawGroundOnPos(texture, pos, 0, color);
        }

        private void drawObjectOnPos(int typ, int pos, Color color)
        {
            drawObjectOnPos(typ, 0, 0, pos, color);
        }
        private void drawObjectOnPos(int typ, int version, int tile, int pos, Color color)
        {
            int offsetY = objects[typ].Texture[tile][0].Width - objects[typ].Size * Cam.Size;
            drawObjectOnPos(objects[typ].Texture[tile][0], pos, offsetY, color);
        }
        private void drawObjectOnPos(Texture texture, int pos, Color color)
        {
            drawObjectOnPos(texture, pos, 0, color);
        }
        private void drawObjectOnPos(Texture texture, int pos, int offsetY, Color color)
        {
            float scale = Cam.Scale;
            float drawPosX, drawPosY;
            drawPos(texture, pos, out drawPosX, out drawPosY);
            GL2D.DrawImage(texture, new RectangleF(0, 0, texture.Width, texture.Width), new RectangleF(drawPosX, drawPosY - offsetY * scale, texture.Width * scale, texture.Width * scale), color);
        }
    }
}