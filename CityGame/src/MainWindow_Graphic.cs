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
    public partial class MainWindow : Form
    {
        private void updateAnimator()
        {
            for (int i = 0; i < 20; i++)
            {
                if (animator[i] < i) animator[i]++;
                else animator[i] = 0;
            }
        }
        unsafe private void Render()
        {
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
            SquareDrawData[] groundData = new SquareDrawData[20000];
            SquareDrawData[] groundDetailData = new SquareDrawData[20000];
            ImageDrawData[] objectData = new ImageDrawData[20000];
            int groundDetailDataIndex = 0;
            int objectDataIndex = 0;
            //GGL.SquareDrawData[] groundData = new SquareDrawData[10000];

            GL2D.ClearBuffer();


            int camPosX = Cam.PosX;
            int camPosY = Cam.PosY;
            int width = World.Width;
            int height = World.Height;

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
                        Point drawPos = new Point(ix * mapScale - Cam.DetailX, iy * mapScale - Cam.DetailY);

                        // select ground texture
                        int ground = World.Ground[pos];// +World.GroundTile[pos];
                        int groundX = ground % 16;
                        int groundY = (ground - groundX) / 16;
                        // select ground
                        drawedTiles++;
                        GL2D.DrawImage(groundTexture, new Rectangle(64 * groundX, 64 * groundY, 64, 64), new Rectangle(drawPos.X, drawPos.Y, mapScale, mapScale), Color.White);

                        int refX = World.ReferenceX[pos];
                        int refY = World.ReferenceY[pos];
                        int refPos = pos - refX - refY * World.Width;
                        int typ = World.Typ[refPos];
                        if (gameObject[typ].Ground != null)
                        {
                            // square draw positions
                            int groundPos = posX + posY * (World.Width + 1);
                            Point[] groundDrawPos = new Point[4] {
                                new Point(drawPos.X+ World.vertexHeight[groundPos], drawPos.Y+ World.vertexHeight[groundPos]),
                                new Point(drawPos.X + mapScale+ World.vertexHeight[groundPos+1], drawPos.Y+ World.vertexHeight[groundPos+1]),
                                new Point(drawPos.X + mapScale+ World.vertexHeight[groundPos+2+World.Width], drawPos.Y + mapScale+ World.vertexHeight[groundPos+2+World.Width]),
                                new Point(drawPos.X+ World.vertexHeight[groundPos+1+World.Width], drawPos.Y + mapScale+ World.vertexHeight[groundPos+1+World.Width])
                                };

                            int tile = 0;
                            Texture texture = gameObject[typ].Ground[0];
                            int anim = animator[texture.Height / 64 - 1];
                            if (gameObject[typ].GroundMode == 1) tile = World.Tile[pos];
                            drawedTiles++;
                            groundDetailData[groundDetailDataIndex++].Update(texture,
                                new Point[4] { new Point((64 * tile) + 0, 64 * anim), new Point((64 * tile) + 64, 64 * anim), new Point((64 * tile) + 64, 64 + 64 * anim), new Point((64 * tile) + 0, 64 + 64 * anim) },
                                groundDrawPos,
                                Color.White);
                        }
                        // draw object
                        if (gameObject[typ].Texture != null)
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
                                    new Rectangle(drawPos.X, drawPos.Y - overdrawDst, mapScale + overdrawDst, mapScale + overdrawDst),
                                    Color.White);
                            }
                            else //if (refX == 0 && refY)
                            {
                                objectData[objectDataIndex++].Update(texture,
                                    new Rectangle(64 * refX, 64 * refY + (texture.Width * anim) + overdrawSrs, 64, 64),
                                    new Rectangle(drawPos.X, drawPos.Y, mapScale, mapScale),
                                    Color.White);
                            }
                        }
                    }
                }
            }
            GL2D.DrawSquare(groundDetailData, groundDetailDataIndex);
            GL2D.DrawImage(objectData, objectDataIndex);
            drawtime.Stop();
            Stopwatch rendertime = new Stopwatch();
            rendertime.Start();
            GL2D.UpdateBuffer();
            GL2D.UseShader(basicShader);
            GL2D.Render();
            rendertime.Stop();
            RenderPreview();

            GL2D.SwapBuffers();

            Program.MenuOverlay.debugLabel.Text = "fulltime in ms: " + time.ElapsedMilliseconds + "\ndrawtime in ms: " + drawtime.ElapsedMilliseconds + "\nrendertime in ms: " + rendertime.ElapsedMilliseconds + "\nFPS: " + (int)(1000 / (time.ElapsedMilliseconds + 0.1)) + "\ndrawedTiles: " + drawedTiles;
            time = null;
            //GL2D.drawImage(texture, new Rectangle(0, 0, 64, 64), new Rectangle(222, 99, 64, 64), Color.White);

            //GL2D.drawTriangle(texture, new Point[] { new Point(32, 53), new Point(64, 12), new Point(20, 39) }, new Point[] { new Point(32, 53), new Point(64, 12), new Point(20, 39) }, new Color[] { Color.Red, Color.Green, Color.Blue });
            //Console.WriteLine(GL.e);
        }

        unsafe private void RenderPreview()
        {
            if (!showCurBuild) return;

            int mapScale = Cam.Size;
            int size = gameObject[CurBuild].Size;
            int builMode = gameObject[CurBuild].BuildMode;
            int width = World.Width;
            int height = World.Height;

            if (builMode == 0 || builMode == 1)
            {
                int pos = CurField;
                int x = pos % width;
                int y = (pos - x) / width;

                if (x >= 0 && y >= 0 && x < width && y < height)
                {
                    Color color;
                    if (World.CanBuild((byte)CurBuild, pos) && World.TestAreaDependet((byte)CurBuild, pos) == 0) color = Color.FromArgb(150, 0, 255, 0);
                    else color = Color.FromArgb(150, 255, 0, 0);

                    int tile = 0;
                    if (gameObject[CurBuild].GraphicMode != 0) tile = World.AutoTile((byte)CurBuild, pos);
                    if (gameObject[CurBuild].Ground != null) drawGroundOnPos(gameObject[CurBuild].Ground[0], pos, gameObject[CurBuild].GroundMode == 0 ? 0 : tile, color);
                    if (gameObject[CurBuild].Texture != null) drawObjectOnPos(gameObject[CurBuild].Texture[0, 0], pos, color);
                }
            }

            GL2D.UpdateBuffer();
            GL2D.UseShader(glowShader);
            GL2D.Render();
        }

        private void drawGroundOnPos(Texture texture, int pos,int tile, Color color)
        {
            int mapScale = Cam.Size;
            float factor = mapScale / 64f;
            int width = World.Width;
            int height = World.Height;
            int x = pos % width - Cam.PosX;
            int y = (pos - x) / width - Cam.PosY;

                int drawPosX = mapScale * x - Cam.DetailX;
                int drawPosY = mapScale * y - Cam.DetailY - (int)((texture.Width % 64) * factor);
                GL2D.DrawImage(texture, new Rectangle(64 * tile, 0, 64, 64), new Rectangle(drawPosX, drawPosY, mapScale, mapScale), color);
        }
        private void drawGroundOnPos(Texture texture, int pos, Color color)
        {
            drawGroundOnPos(texture, pos, 0, color);
        }
        private void drawObjectOnPos(Texture texture,int pos,Color color)
        {
            int mapScale = Cam.Size;
            float factor = mapScale / 64f;
            int width = World.Width;
            int height = World.Height;
            int x = pos % width-Cam.PosX;
            int y = (pos - x) / width - Cam.PosY;

                int drawPosX = mapScale * x - Cam.DetailX;
                int drawPosY = mapScale * y - Cam.DetailY - (int)((texture.Width % 64) * factor);
                GL2D.DrawImage(texture, new Rectangle(0, 0, texture.Width, texture.Width), new Rectangle(drawPosX, drawPosY, (int)(texture.Width * factor), (int)(texture.Width * factor)), color);
        }
    }
}
