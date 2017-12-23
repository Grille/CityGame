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
            int textureScale = 64;
            if (animatorTimer.ElapsedMilliseconds > 250)
            {
                animatorTimer.Restart();
                updateAnimator();
            }

            Stopwatch drawtime = new Stopwatch();
            drawtime.Start();

            GGL.SquareDrawData[] groundData = new SquareDrawData[20000];
            GGL.SquareDrawData[] groundDetailData = new SquareDrawData[20000];
            int groundDetailDataIndex = 0;
            GGL.ImageDrawData[] objectData = new ImageDrawData[20000];
            int objectDataIndex = 0;
            SquareDrawData selectFieldGround = new SquareDrawData();
            ImageDrawData selectField = new ImageDrawData();
            //GGL.SquareDrawData[] groundData = new SquareDrawData[10000];

            GL2D.ClearBuffer();

            for (int ix = Width / mapScale + 2; ix >= -2; ix--)
            {
                for (int iy = 0; iy <= Height / mapScale + 1 + 2; iy++)
                {
                    // find map position
                    int posX = ix + Cam.PosX;
                    int posY = iy + Cam.PosY;
                    int pos = posX + posY * World.Width;
                    // position exists?
                    if (posX >= 0 && posY >= 0 && posX < World.Width && posY < World.Height)
                    {
                        // draw positions
                        Point drawPos = new Point(ix * mapScale - Cam.DetailX, iy * mapScale - Cam.DetailY);

                        // select ground texture
                        int ground = World.Ground[pos];// +World.GroundTile[pos];
                        int groundX = ground % 16;
                        int groundY = (ground - groundX) / 16;
                        // select ground
                        drawedTiles++;
                        //GL2D.drawSquare(groundTexture,
                        //    new Point[4] {
                        //        new Point((64 * groundX) + 0+World.vertexHeight[pos], (64 * groundY) + 0),
                        //        new Point((64 * groundX) + 64, (64 * groundY) + 0),
                        //        new Point((64 * groundX) + 64, (64 * groundY) + 64),
                        //        new Point((64 * groundX) + 0, (64 * groundY) + 64) },
                        //    groundDrawPos,
                        //    Color.White);

                        GL2D.drawImage(groundTexture, new Rectangle(64 * groundX, 64 * groundY, 64, 64), new Rectangle(drawPos.X, drawPos.Y, mapScale, mapScale),Color.White);

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
                        if (CurField == pos)
                        {
                            //drawedTiles++;
                            //GL2D.drawSquare(gui,
                            //new Point[4] { new Point(0, 0), new Point(64, 0), new Point(64, 64), new Point(0, 64) },
                            //groundDrawPos,
                            //Color.Lime);
                            int size = gameObject[CurBuild].Size;

                            if ((gameObject[CurBuild].Ground != null || gameObject[CurBuild].Texture != null) && showCurBuild)
                            {
                                Color color;
                                if (World.CanBuild((byte)CurBuild, CurField) && World.TestTyp((byte)CurBuild, CurField) == 0) color = Color.FromArgb(150, 0, 255, 0);
                                else color = Color.FromArgb(150, 255, 0, 0);

                                if (gameObject[CurBuild].Ground != null)
                                {
                                    //int tile = 1;
                                    //Texture texture = gameObject[CurBuild].Ground[0];
                                    //int anim = animator[texture.Height / 64 - 1];
                                    //if (gameObject[CurBuild].GroundMode == 1) tile = World.AutoTile((byte)CurBuild,CurField);
                                    //drawedTiles++;
                                    //selectFieldGround.Update(texture,
                                    //    new Point[4] { new Point((64 * tile) + 0, 64 * anim), new Point((64 * tile) + 64, 64 * anim), new Point((64 * tile) + 64, 64 + 64 * anim), new Point((64 * tile) + 0, 64 + 64 * anim) },
                                    //    groundDrawPos,
                                    //    color);
                                }
                                if (gameObject[CurBuild].Texture != null)
                                {
                                    Texture texture = gameObject[CurBuild].Texture[0, 0];
                                    int overdrawSrs = texture.Width - 64 * size;
                                    int overdrawDst = (int)(overdrawSrs * (mapScale / 64f));
                                    selectField.Update(texture,
                                        new Rectangle(0, 0, texture.Width, texture.Width),
                                        new Rectangle(drawPos.X, drawPos.Y - overdrawDst, (int)(texture.Width * (mapScale / 64f)), (int)(texture.Width * (mapScale / 64f))),
                                        color);
                                }
                            }
                            else if(showCurBuild)
                            {
                                Color color;
                                if (World.CanBuild((byte)CurBuild, CurField)) color = Color.FromArgb(200, 0, 255, 0);
                                else color = Color.FromArgb(200, 255, 0, 0);
                                Texture texture = gui;
                                selectField.Update(texture,
                                new Rectangle(0, 0, texture.Width, texture.Width),
                                new Rectangle(drawPos.X, drawPos.Y, (int)(texture.Width * (mapScale / 64f)), (int)(texture.Width * (mapScale / 64f))),
                                color);
                            }
                        }
                    }
                }
            }
            GL2D.drawSquare(groundDetailData, groundDetailDataIndex);
            GL2D.drawImage(objectData, objectDataIndex);
            drawtime.Stop();
            Stopwatch rendertime = new Stopwatch();
            rendertime.Start();
            GL2D.UseShader(basicShader);
            GL2D.UpdateBuffer();
            GL2D.Render();
            rendertime.Stop();

            if (selectField.Texture != null) GL2D.drawImage(selectField);
            if (selectFieldGround.Texture != null) GL2D.drawSquare(selectFieldGround);
            GL2D.UseShader(glowShader);
            GL2D.UpdateBuffer();
            GL2D.Render();

            Program.MenuOverlay.debugLabel.Text = "fulltime in ms: " + time.ElapsedMilliseconds + "\ndrawtime in ms: " + drawtime.ElapsedMilliseconds + "\nrendertime in ms: " + rendertime.ElapsedMilliseconds + "\nFPS: " + (int)(1000 / (time.ElapsedMilliseconds + 0.1)) + "\ndrawedTiles: " + drawedTiles;
            time = null;
            //GL2D.drawImage(texture, new Rectangle(0, 0, 64, 64), new Rectangle(222, 99, 64, 64), Color.White);

            //GL2D.drawTriangle(texture, new Point[] { new Point(32, 53), new Point(64, 12), new Point(20, 39) }, new Point[] { new Point(32, 53), new Point(64, 12), new Point(20, 39) }, new Color[] { Color.Red, Color.Green, Color.Blue });
            //Console.WriteLine(GL.e);
        }
    }
}
