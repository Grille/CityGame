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

        public void UpdateMiniMap()
        {
            int size = World.Width * World.Height;
            byte[] data = new byte[size * 3];
            int iDst = 0;
            for (int iSrc = 0; iSrc < size; iSrc++)
            {
                int pos = iSrc - World.ReferenceX[iSrc] - World.ReferenceY[iSrc]*World.Width;
                if (gameObject[World.Typ[pos]].Texture == null)
                {
                    if (gameObject[World.Typ[pos]].Ground == null)
                    {
                        if (World.Ground[pos] == 52)
                        {
                            data[iDst++] = 199;
                            data[iDst++] = 183;
                            data[iDst++] = 151;
                        }
                        else
                        {
                            data[iDst++] = 38;
                            data[iDst++] = 100;
                            data[iDst++] = 17;
                        }
                    }
                    else
                    {
                        Color color = gameObject[World.Typ[pos]].Ground[0].BaseColor;
                        data[iDst++] = color.R;
                        data[iDst++] = color.G;
                        data[iDst++] = color.B;
                    }
                }
                else
                {
                    Color color = gameObject[World.Typ[pos]].Texture[World.Version[pos], World.Tile[pos]].BaseColor;
                    float pz = (float)color.A / 255f;
                    data[iDst++] = (byte)(color.R * pz + 38 * (1-pz));
                    data[iDst++] = (byte)(color.G * pz + 100 * (1 - pz));
                    data[iDst++] = (byte)(color.B * pz + 17 * (1 - pz));
                    
                }
                /*
                switch (World.Typ[iSrc])
                {
                    case 0:
                        data[iDst++] = 38;
                        data[iDst++] = 100;
                        data[iDst++] = 17;
                        break;
                    case 1:
                    case 2:
                        data[iDst++] = 76;
                        data[iDst++] = 127;
                        data[iDst++] = 255;
                        break;
                    case 3:
                    case 4:
                    case 5:
                        data[iDst++] = 20;
                        data[iDst++] = 75;
                        data[iDst++] = 5;
                        break;
                    default:
                        data[iDst++] = 128;
                        data[iDst++] = 128;
                        data[iDst++] = 128;
                        break;
                }
                */
            }
            highViewMap.Update(World.Width, World.Height, data);
        }

        private Bitmap RenderMinimap()
        {
            return new Bitmap(1, 1);
            //LockBitmap lockBitmap = new LockBitmap(World.Width, World.Height);
            //byte[] rgbData = lockBitmap.getData();
            //int iDst = 0;
            //for (int iSrc = 0; iSrc < World.Width * World.Height; iSrc++)
            //{
            //    rgbData[iDst++] = 0;
            //    rgbData[iDst++] = 255;
            //    rgbData[iDst++] = 0;
            //    rgbData[iDst++] = 255;
            //}
            //return lockBitmap.returnBitmap();
        }
        private void updateAnimator()
        {
            for (int i = 0; i < 20; i++)
            {
                if (animator[i] < i) animator[i]++;
                else animator[i] = 0;
            }
        }
        int arraySize;
        ImageDrawData[] groundDetailData;
        ImageDrawData[] objectData;
        private void Render()
        {
            isRendering = true;
            Stopwatch time = new Stopwatch();
            int drawedTiles = 0;
            time.Start();
            int mapScale = Cam.Size;
            //int textureScale = 64;
            if (animatorTimer.ElapsedMilliseconds > 250)
            {
                animatorTimer.Restart();
                updateAnimator();
            }

            Stopwatch drawtime = new Stopwatch();
            drawtime.Start();
            int newArraySize = (Width / mapScale + 8) * (Height / mapScale + 8);
            if (arraySize != newArraySize)
            {
                arraySize = newArraySize;
                //int[] groundEqualTest = new int[arraySize];
                groundDetailData = new ImageDrawData[arraySize];
                //int[] SquareEqualTest = new int[arraySize];
                objectData = new ImageDrawData[arraySize];
                //int[] objectEqualTest = new int[arraySize];
            }

            int groundDetailDataIndex = 0;
            int objectDataIndex = 0;
            //GGL.SquareDrawData[] groundData = new SquareDrawData[10000];

            GL2D.UniformInt(0, date.Hour);
            GL2D.UniformInt(1, date.Minute);


            GL2D.ClearBuffer(Color.Blue);

            BufferOffset bo1 = GL2D.GetOffset(), bo2 = GL2D.GetOffset();

            int camPosX = Cam.PosX;
            int camPosY = Cam.PosY;
            int width = World.Width;
            int height = World.Height;

            if (Cam.Size >= 8)
            {

                for (int ix = Width / mapScale + 2; ix >= -2; ix--)
            {
                for (int iy = 0; iy <= Height / mapScale + 1 + 2; iy++)
                {
                    //// find map position
                    int posX = ix + camPosX;
                    int posY = iy + camPosY;
                    int pos = posX + posY * width;
                    //// position exists?
                    if (posX >= 0 && posY >= 0 && posX < width && posY < height)
                    {
                        // draw positions
                        int drawPosX = ix * mapScale - Cam.DetailX;
                        int drawPosY = iy * mapScale - Cam.DetailY;
                        // select ground texture
                        int ground = World.Ground[pos];// +World.GroundTile[pos];
                        int groundX = ground % 16;
                        int groundY = (ground - groundX) / 16;
                        // select ground
                        drawedTiles++;
                        GL2D.DrawImage(groundTexture, new Rectangle(64 * groundX, 64 * groundY, 64, 64), new Rectangle(drawPosX, drawPosY, mapScale, mapScale), Color.White);


                        int refX = World.ReferenceX[pos];
                        int refY = World.ReferenceY[pos];
                        int refPos = pos - refX - refY * World.Width;
                        int typ = World.Typ[refPos];
                        if (gameObject[typ].Ground != null)
                        {
                            int tile = 0;
                            Texture texture = gameObject[typ].Ground[0];
                                int anim = animator[texture.Height / 64 - 1];
                            if (gameObject[typ].GroundMode == 1) tile = World.Tile[pos];
                            drawedTiles++;
                            groundDetailData[groundDetailDataIndex++].Update(texture, new Rectangle(64 * tile, 64 * anim, 64, 64), new Rectangle(drawPosX, drawPosY, mapScale, mapScale), Color.White);
                        }
                        // draw object
                        if (gameObject[typ].Texture != null && Cam.Size >= 8)
                        {
                            int tile = World.Tile[refPos];
                            int version = World.Version[refPos];
                            Texture texture = gameObject[typ].Texture[version, tile];
                            int size = gameObject[typ].Size;
                                int anim = animator[texture.Height / texture.Width - 1];
                            int overdrawSrs = texture.Width - 64 * size;
                            drawedTiles++;
                            if (refX == size - 1 || refY == 0)
                            {
                                int overdrawDst = (int)(overdrawSrs * (mapScale / 64f));
                                
                                objectData[objectDataIndex++].Update(texture,
                                    new Rectangle(64 * refX, 64 * refY + (texture.Width * anim), 64 + overdrawSrs, 64 + overdrawSrs),
                                    new Rectangle(drawPosX, drawPosY - overdrawDst, mapScale + overdrawDst, mapScale + overdrawDst),
                                    Color.White);
                            }
                            else //if (refX == 0 && refY)
                            {
                                objectData[objectDataIndex++].Update(texture,
                                    new Rectangle(64 * refX, 64 * refY + (texture.Width * anim) + overdrawSrs, 64, 64),
                                    new Rectangle(drawPosX, drawPosY, mapScale, mapScale),
                                    Color.White);
                            }
                        }

                    }
                }
            }
                //bo1 = GL2D.GetOffset();
                GL2D.DrawImage(groundDetailData, groundDetailDataIndex);
                
                GL2D.DrawImage(objectData, objectDataIndex);
                bo2 = GL2D.GetOffset();
            }
            else GL2D.DrawImage(highViewMap, new Rectangle(0, 0, highViewMap.Width, highViewMap.Height), new Rectangle(-camPosX*mapScale, -camPosY*mapScale, World.Width*mapScale, World.Height*mapScale), Color.White);

           


            drawtime.Stop();
            Stopwatch rendertime = new Stopwatch();
            rendertime.Start();
            GL2D.UpdateBuffer();
            GL2D.UseShader(basicShader);
            GL2D.Render();
            RenderPreview();
            GL2D.SwapBuffers();
            rendertime.Stop();

            Program.MenuOverlay.debugLabel.Text = "fulltime in ms: " + time.ElapsedMilliseconds + "\ndrawtime in ms: " + drawtime.ElapsedMilliseconds + "\nrendertime in ms: " + rendertime.ElapsedMilliseconds + "\nFPS: " + (int)(1000 / (time.ElapsedMilliseconds + 0.1)) + "\ndrawedTiles: " + drawedTiles + "\nTime: "+date;
            time = null;
            //GL2D.DrawImage(texture, new Rectangle(0, 0, 64, 64), new Rectangle(222, 99, 64, 64), Color.White);

            //GL2D.drawTriangle(texture, new Point[] { new Point(32, 53), new Point(64, 12), new Point(20, 39) }, new Point[] { new Point(32, 53), new Point(64, 12), new Point(20, 39) }, new Color[] { Color.Red, Color.Green, Color.Blue });
            //Console.WriteLine(GL.e);
            isRendering = false;
        }

        //private void RenderGround();
        unsafe private void RenderPreview()
        {
            if (!showCurBuild) return;

            int mapScale = Cam.Size;
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

        private void drawPos(Texture texture,int pos, out int drawPosX, out int drawPosY)
        {
            int mapScale = Cam.Size;
            float factor = mapScale / 64f;
            int width = World.Width;
            int height = World.Height;
            int x = pos % width - Cam.PosX;
            int y = (pos - x) / width - Cam.PosY;
            if (Cam.PosX < 0) y++;

            drawPosX = mapScale * x - Cam.DetailX;
            drawPosY = mapScale * y - Cam.DetailY - (int)((texture.Width % 64) * factor);
        }

        private void drawGroundOnPos(Texture texture, int pos,int tile, Color color)
        {
            int mapScale = Cam.Size;
            int drawPosX;
            int drawPosY;
            drawPos(texture, pos, out drawPosX, out drawPosY);
            GL2D.DrawImage(texture, new Rectangle(64 * tile, 0, 64, 64), new Rectangle(drawPosX, drawPosY, mapScale, mapScale), color);
        }
        private void drawGroundOnPos(Texture texture, int pos, Color color)
        {
            drawGroundOnPos(texture, pos, 0, color);
        }
        private void drawObjectOnPos(int typ, int pos, Color color)
        {
            drawObjectOnPos(typ, 0, 0, pos, color);
        }
        private void drawObjectOnPos(int typ,int version,int tile, int pos, Color color)
        {
            int mapScale = Cam.Size;
            float factor = mapScale / 64f;
            Texture texture = gameObject[typ].Texture[version, tile];
            int drawPosX;
            int drawPosY;
            drawPos(texture, pos, out drawPosX, out drawPosY);
            if ((texture.Width - gameObject[typ].Size * 64) >= 64) drawPosY -= mapScale;
            GL2D.DrawImage(texture, new Rectangle(0, 0, texture.Width, texture.Width), new Rectangle(drawPosX, drawPosY, (int)(texture.Width * factor), (int)(texture.Width * factor)), color);
        }
        private void drawObjectOnPos(Texture texture,int pos,Color color)
        {
            int mapScale = Cam.Size;
            float factor = mapScale / 64f;
            int drawPosX;
            int drawPosY;
            drawPos(texture, pos, out drawPosX, out drawPosY);
            GL2D.DrawImage(texture, new Rectangle(0, 0, texture.Width, texture.Width), new Rectangle(drawPosX, drawPosY, (int)(texture.Width * factor), (int)(texture.Width * factor)), color);
        }
    }
}
