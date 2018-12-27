
using System;
using System.Diagnostics;
//using System.Linq;
using System.Drawing;
using System.Windows.Forms;

using CsGL2D;
namespace CityGame
{
    public partial class Game
    {

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
            /*
            Console.WriteLine();
            GL2D.ClearBuffer(Color.Gray);
            GL2D.DrawImage(groundTexture, new Rectangle(64, 64, 64, 64), Color.Lime);
            GL2D.DrawImage(groundTexture, new Rectangle(512, 512, 64, 64), Color.White);
            GL2D.UpdateBuffer();
            GL2D.Render();
            Console.WriteLine(GL2D.GetError());
            GL2D.SwapBuffers();

            return;
            */
            updateAnimator();

            buffer.Reset();
            Program.MainWindow.Ctx.Clear(Color.Navy);
            //GL2D.ClearBuffer(Color.Navy);
            int groundGraphicsIndex = 0, objectGraphicsIndex = 0;

            Stopwatch drawtime = new Stopwatch();
            drawtime.Start();

            float posX = Cam.PosX, posY = Cam.PosY, scale = Cam.Scale;
            float tileSize = Cam.Size;
            float scSize = (tileSize * scale);
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

                    float drawPosX = ((-posX + ix * tileSize) * scale) + window.Width / 2f;
                    float drawPosY = ((-posY + iy * tileSize) * scale) + window.Height / 2f;

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
                    buffer.DrawImage(groundTexture, new RectangleF((int)(groundX * 64), (int)(groundY * 64), 64, 64), new RectangleF(drawPosX, drawPosY, scSize, scSize), Color.White);

                    if (World.Zone[worldPos] != 0)
                    {
                        buffer.DrawImage(zoneTexture, new RectangleF(0,0, 64, 64), new RectangleF(drawPosX, drawPosY, scSize, scSize), Zones[World.Zone[worldPos]].Color);
                    }
                    if (Objects[typ].Ground != null)
                    {
                        Texture texture = Objects[typ].Ground[0];
                        int anim = animator[texture.Height / 64 - 1];
                        buffer.DrawImage(texture, new RectangleF(64 * World.TileGround[worldPos], 64 * anim, 64, 64), new RectangleF(drawPosX, drawPosY, scSize, scSize), Color.White);
                    }
                    if (Objects[typ].Texture != null)
                    {
                        int tile = World.TileStruct[refPos];
                        if (Objects[typ].Texture[tile].Length != 0)
                        {
                            int version = World.Version[refPos];
                            Texture texture = Objects[typ].Texture[tile][version];
                            int objectSize = Objects[typ].Size;
                            int anim = animator[texture.Height / texture.Width - 1];
                            int overdrawSrs = texture.Width - 64 * objectSize;
                            if (refX == objectSize - 1 || refY == 0)
                            {
                                float overdrawDst = (overdrawSrs * (scSize / 64f));

                                buffer.DrawImage(texture,
                                    new RectangleF(64 * refX, 64 * refY + (texture.Width * anim), 64 + overdrawSrs, 64 + overdrawSrs),
                                    new RectangleF(drawPosX, drawPosY - overdrawDst, scSize + overdrawDst, scSize + overdrawDst),
                                    Color.White);
                            }
                            else //if (refX == 0 && refY)
                            {

                                buffer.DrawImage(texture,
                                    new RectangleF(64 * refX, 64 * refY + (texture.Width * anim) + overdrawSrs, 64, 64),
                                    new RectangleF(drawPosX, drawPosY, scSize, scSize),
                                    Color.White);
                            }
                        }
                    }

                }
            }

            drawtime.Stop();

            Stopwatch rendertime = new Stopwatch();
            rendertime.Start();

            //GL2D.UpdateBuffer();
            //GL2D.UseShader(basicShader);
            //GL2D.Render();

            Program.MainWindow.Ctx.Render(buffer, basicShader) ;

            renderPreview();

            rendertime.Stop();

            /*
            Graphics g = GL2D.GetGDIContext();
            g.DrawRectangle(new Pen(Color.Red, 10), new Rectangle(100, 100, 300, 300));
            */
            Program.MainWindow.Ctx.Refresh();

            Program.MenuOverlay.debugLabel.Text = "fulltime in ms: " + "-" + "\ndrawtime in ms: " + drawtime.ElapsedMilliseconds + "\nrendertime in ms: " + rendertime.ElapsedMilliseconds + "\nFPS: " + "-" + "\ndrawedTiles: " + "-" + "\nTime: " + date;

        }
        //private void RenderGround();
        private void renderPreview()
        {
            buffer.Reset();
            if (SelectetBuildIndex.Value == 0) return;
            if (!buildPreviewEnabled) return;

            int size = Objects[SelectetBuildIndex.Value].Size;
            int builMode = SelectetBuildIndex.BuildMode;
            int width = World.Width;
            int height = World.Height;
            
            if (mouse.Button != MouseButtons.Left || builMode == 0 || builMode == 1)//single,rain
            {
                int pos = HoveredWorldPos;
                int x = pos % width;
                int y = (pos - x) / width;


                if (x >= 0 && y >= 0 && x < width && y < height)
                {
                    if (SelectetBuildIndex.Typ == 1)
                        renderBuildPreview(pos, 0, 0, 0, 0);
                    else if (SelectetBuildIndex.Typ == 2)
                        renderZonePreview(pos);
                }
            }
            else
            {
                int pos = HoveredWorldPos;
                int x = pos % width;
                int y = (pos - x) / width;

                int pos2 = MouseDownWorldPos;
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
                    if (direction == 0){l = 1;r = 1;}
                    else { u = 1;o = 1; }
                    for (int i = 0; i <= dist; i++)
                    {
                        if (SelectetBuildIndex.Typ == 1)
                            renderBuildPreview(pos2, l, u, r, o);
                        else if (SelectetBuildIndex.Typ == 2)
                            renderZonePreview(pos2);
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
                                pos = ix + iy * width;
                                if (SelectetBuildIndex.Typ == 1)
                                    renderBuildPreview(pos, l, u, r, o);
                                else if (SelectetBuildIndex.Typ == 2)
                                    renderZonePreview(pos);
                            }
                            pos += 1;
                        }
                        pos += width - resetX;

                    }
                }
            }


            //GL2D.UpdateBuffer();

            Shader usedShader = null;
            if (SelectetBuildIndex.Typ == 1)
                usedShader = glowShader;
            else if (SelectetBuildIndex.Typ == 2)
                usedShader = basicShader;

            Program.MainWindow.Ctx.Render(buffer, usedShader);

            Console.WriteLine(buffer.Index);
        }
        private void renderZonePreview(int pos)
        {
            Zone zone = Zones[SelectetBuildIndex.Value];
            Color color = zone.Color;
            drawGroundOnPos(zoneTexture, pos, color);
        }
        private void renderBuildPreview(int pos,byte l,byte u,byte r,byte o)
        {
            byte buildTyp = replaceBuildTyp(World.Typ[pos]);

            Color color;
            if (World.CanBuild((byte)buildTyp, pos) && World.TestAreaDependet((byte)buildTyp, pos) == 0)color = Color.FromArgb(150, 0, 255, 0);
            else color = Color.FromArgb(150, 255, 0, 0);

            if (Objects[buildTyp].Ground != null)
                drawGroundOnPos(Objects[buildTyp].Ground[0], pos, World.applyAutoTile(buildTyp, pos, Objects[buildTyp].GroundMode, Objects[buildTyp].GroundNeighbors, l, u, r, o), color);
            if (Objects[buildTyp].Texture != null)
                drawObjectOnPos(buildTyp, 0, World.applyAutoTile(buildTyp, pos, Objects[buildTyp].StructMode, Objects[buildTyp].StructNeighbors, l, u, r, o), pos, color);
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
            buffer.DrawImage(texture, new RectangleF(64 * tile, 0, 64, 64), new RectangleF(drawPosX, drawPosY, scSize, scSize), color);
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
            if (Objects[typ].Texture[tile].Length == 0) return;
            int offsetY = Objects[typ].Texture[tile][0].Width - Objects[typ].Size * Cam.Size;
            drawObjectOnPos(Objects[typ].Texture[tile][0], pos, offsetY, color);
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
            buffer.DrawImage(texture, new RectangleF(0, 0, texture.Width, texture.Width), new RectangleF(drawPosX, drawPosY - offsetY * scale, texture.Width * scale, texture.Width * scale), color);
        }
    }
}