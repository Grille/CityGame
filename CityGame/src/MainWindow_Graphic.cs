using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
//using System.Threading.Tasks;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using GGL;
using GGL.Graphic;

namespace CityGame
{
    public partial class MainWindow : Form // Graphic
    {
        Texture MiniMapTex;

        private void renderMinimap(byte[] data)
        {
            int size = World.Width * World.Height;
            int iDst = 0;
            for (int iSrc = 0; iSrc < size; iSrc++)
            {
                int pos = iSrc - World.ReferenceX[iSrc] - World.ReferenceY[iSrc] * World.Width;
                if (gameObject[World.Typ[pos]].Texture == null)
                {
                    if (gameObject[World.Typ[pos]].Ground == null)
                    {
                        if (World.Ground[pos] == 52)
                        {
                            data[iDst++] = 151;
                            data[iDst++] = 183;
                            data[iDst++] = 199;
                        }
                        else
                        {
                            data[iDst++] = 17;
                            data[iDst++] = 100;
                            data[iDst++] = 38;
                        }
                    }
                    else
                    {
                        Color color = gameObject[World.Typ[pos]].Ground[0].BaseColor;
                        data[iDst++] = color.B;
                        data[iDst++] = color.G;
                        data[iDst++] = color.R;
                    }

                }
                else
                {
                    Color color = gameObject[World.Typ[pos]].Texture[World.Version[pos], World.Tile[pos]].BaseColor;
                    float pz = (float)color.A / 255f;
                    data[iDst++] = (byte)(color.B * pz + 17 * (1 - pz));
                    data[iDst++] = (byte)(color.G * pz + 100 * (1 - pz));
                    data[iDst++] = (byte)(color.R * pz + 38 * (1 - pz));

                }
                data[iDst++] = 255;
            }
        }
        public Bitmap UpdateGDIMiniMap()
        {
            LockBitmap lockBitmap = new LockBitmap(World.Width, World.Height);
            byte[] data = lockBitmap.getData();
            renderMinimap(lockBitmap.getData());
            return lockBitmap.returnBitmap();
        }
        /*
        public void UpdateGLMiniMap()
        {
            byte[] data = new byte[World.Width * World.Height * 3];
            renderMinimap(data);
            highViewMap.Update(World.Width, World.Height, data);
        }
        */

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
        int arraySize;
        DrawData[] groundGraphics = new DrawData[262144];
        DrawData[] objectGraphics = new DrawData[262144];

        float consist = 0;
        private void Render()
        {
            updateAnimator();

            consist += 2;
            GL2D.ClearBuffer(Color.Blue);

            int groundGraphicsIndex = 0, objectGraphicsIndex = 0;

            Stopwatch drawtime = new Stopwatch();
            drawtime.Start();

            float posX = Cam.PosX, posY = Cam.PosY, scale = Cam.Scale;

            float size = Cam.Size;
            float scSize = (size * scale);
            int width = World.Width;
            int height = World.Height;

            int startX = width, endX = 0,startY = 0, endY = height;

            for (int ix = startX; ix >= endX; ix--)
            {
                for (int iy = startY; iy <= endY; iy++)
                {
                    int worldPosX = ix;
                    int worldPosY = iy;
                    int worldPos = worldPosX + worldPosY * width;

                    if (!(worldPosX >= 0 && worldPosY >= 0 && worldPosX < width && worldPosY < height)) continue;

                    float drawPosX = ((-posX+ix*size)*scale) + Width / 2;
                    float drawPosY = ((-posY+iy*size)*scale) + Height / 2;

                    if (!(drawPosX > -64 && drawPosY > -64 && drawPosX < Width && drawPosY < Height)) continue;

                    int ground = World.Ground[worldPos];// +World.GroundTile[pos];
                    int groundX = ground % 16;
                    int groundY = (ground - groundX) / 16;

                    int refX = World.ReferenceX[worldPos];
                    int refY = World.ReferenceY[worldPos];
                    int refPos = worldPos - refX - refY * World.Width;
                    int typ = World.Typ[refPos];

                    GL2D.DrawImage(groundTexture, groundX * 64, groundY * 64, 64, 64, drawPosX, drawPosY, scSize, scSize, Color.White);

                    if (gameObject[typ].Ground != null)
                    {
                        int tile = 0;
                        Texture texture = gameObject[typ].Ground[0];
                        int anim = animator[texture.Height / 64 - 1];
                        if (gameObject[typ].GroundMode == 1) tile = World.Tile[worldPos];
                        groundGraphics[groundGraphicsIndex++].Update(texture, 64 * tile, 64 * anim, 64, 64, drawPosX, drawPosY, scSize, scSize, Color.White);
                    }
                    if (gameObject[typ].Texture != null)
                    {
                        int tile = World.Tile[refPos];
                        int version = World.Version[refPos];
                        Texture texture = gameObject[typ].Texture[version, tile];
                        int objectSize = gameObject[typ].Size;
                        int anim = animator[texture.Height / texture.Width - 1];
                        int overdrawSrs = texture.Width - 64 * objectSize;
                        if (refX == objectSize - 1 || refY == 0)
                        {
                            int overdrawDst = (int)(overdrawSrs * (scSize / 64f));

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
            

            RenderPreview();
            
            rendertime.Stop();
            GL2D.SwapBuffers();

            Program.MenuOverlay.debugLabel.Text = "fulltime in ms: " + "-" + "\ndrawtime in ms: " + drawtime.ElapsedMilliseconds + "\nrendertime in ms: " + rendertime.ElapsedMilliseconds + "\nFPS: " + "-" + "\ndrawedTiles: " + "-" + "\nTime: " + date;

        }
        //private void RenderGround();
        unsafe private void RenderPreview()
        {
            if (!showCurBuild) return;

            int size = gameObject[CurBuildIndex].Size;
            int builMode = gameObject[CurBuildIndex].BuildMode;
            int width = World.Width;
            int height = World.Height;

            if (mouse.Button != MouseButtons.Left || builMode == 0 || builMode == 1)//single,rain
            {
                int pos = CurFieldPos;
                int x = pos % width;
                int y = (pos - x) / width;

                if (x >= 0 && y >= 0 && x < width && y < height)
                {
                    Color color;
                    if (World.CanBuild((byte)CurBuildIndex, pos) && World.TestAreaDependet((byte)CurBuildIndex, pos) == 0) color = Color.FromArgb(150, 0, 255, 0);
                    else color = Color.FromArgb(150, 255, 0, 0);

                    int tile = 0;
                    if (gameObject[CurBuildIndex].GraphicMode != 0) tile = World.AutoTile((byte)CurBuildIndex, pos);
                    if (gameObject[CurBuildIndex].Ground != null) drawGroundOnPos(gameObject[CurBuildIndex].Ground[0], pos, gameObject[CurBuildIndex].GroundMode == 0 ? 0 : tile, color);
                    if (gameObject[CurBuildIndex].Texture != null) drawObjectOnPos(CurBuildIndex, pos, color);
                }
            }
            else
            {
                int pos = CurFieldPos;
                int x = pos % width;
                int y = (pos - x) / width;

                int pos2 = DownFieldPos;
                int x2 = pos2 % width;
                int y2 = (pos2 - x) / width;

                int startX = Math.Min(x, x2);
                int startY = Math.Min(y, y2);
                int endX = Math.Max(x, x2);
                int endY = Math.Max(y, y2);

                int resetX = endX - startX;

                pos = startX + startY * World.Width;
                if (builMode == 2)//line
                {

                }
                else if (builMode == 3)//area
                {
                    for (int iy = startY; iy <= endY; iy++)
                    {
                        for (int ix = startX; ix <= endX; ix++)
                        {
                            if (ix >= 0 && iy >= 0 && ix < width && iy < height)
                            {
                                pos = ix + iy * width;
                                Color color;
                                if (World.CanBuild((byte)CurBuildIndex, pos) && World.TestAreaDependet((byte)CurBuildIndex, pos) == 0) color = Color.FromArgb(150, 0, 255, 0);
                                else color = Color.FromArgb(150, 255, 0, 0);
                                int tile = 0;
                                if (gameObject[CurBuildIndex].GraphicMode != 0) tile = World.AutoTile((byte)CurBuildIndex, pos);
                                if (gameObject[CurBuildIndex].Ground != null) drawGroundOnPos(gameObject[CurBuildIndex].Ground[0], pos, gameObject[CurBuildIndex].GroundMode == 0 ? 0 : tile, color);
                                if (gameObject[CurBuildIndex].Texture != null) drawObjectOnPos(CurBuildIndex, pos, color);
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

        private void drawPos(Texture texture,int pos, out float drawPosX, out float drawPosY)
        {
            float size = Cam.Size,scale = Cam.Scale;
            int width = World.Width,height = World.Height;

            float posX = pos % width;
            float posY = (pos - posX) / width;

            drawPosX = (((-Cam.PosX + posX * size) * scale) + Width / 2);
            drawPosY = (((-Cam.PosY + posY * size) * scale) + Height / 2);
        }

        private void drawGroundOnPos(Texture texture, int pos,int tile, Color color)
        {
            float scSize = Cam.Size*Cam.Scale;
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
            drawObjectOnPos(typ,0,0, pos, color);
        }
        private void drawObjectOnPos(int typ,int version,int tile, int pos, Color color)
        {
            int offsetY = gameObject[typ].Texture[version, tile].Width - gameObject[typ].Size * Cam.Size;
            drawObjectOnPos(gameObject[typ].Texture[version, tile], pos, offsetY, color);
        }
        private void drawObjectOnPos(Texture texture,int pos,Color color)
        {
            drawObjectOnPos(texture, pos, 0, color);
        }
        private void drawObjectOnPos(Texture texture, int pos,int offsetY, Color color)
        {
            float scale = Cam.Scale;
            float drawPosX, drawPosY;
            drawPos(texture, pos, out drawPosX, out drawPosY);
            GL2D.DrawImage(texture, new RectangleF(0, 0, texture.Width, texture.Width), new RectangleF(drawPosX, drawPosY- offsetY*scale, texture.Width * scale, texture.Width * scale), color);
        }
    }
}
