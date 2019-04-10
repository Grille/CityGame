using System;
using System.IO;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using GGL;

namespace CityGame
{
    public partial class Game
    {
        MouseEventArgs mouse = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);

        public void MouseWheel(MouseEventArgs e)
        {
            float posX = -Cam.PosX + (e.X - window.Width / 2) / Cam.Scale;
            float posY = -Cam.PosY + (e.Y - window.Height / 2) / Cam.Scale;

            //if (e.Delta < 0) Cam.Scale /= 2;
            //else Cam.Scale *= 2;
            Cam.Scale += (e.Delta / 500f) * Cam.Scale;

            if (Cam.Scale < 0.03125) Cam.Scale = 0.03125f;
            else if (Cam.Scale > 1f) Cam.Scale = 1f;

            Cam.PosX += (Cam.PosX - (-posX + (window.Width / 2 * (e.X / (float)window.Width * 2 - 1)) / Cam.Scale));
            Cam.PosY += (Cam.PosY - (-posY + (window.Height / 2 * (e.Y / (float)window.Height * 2 - 1)) / Cam.Scale));

        }
        public void MouseMove(MouseEventArgs e)
        {

            int oldWorldPos = HoveredWorldPos;
            mouse = e;

            if (timerLogic.Enabled == false) HoveredWorldPos = -1;
            else
            {
                float size = Cam.Size, scale = Cam.Scale;

                float posX = (((e.X - window.Width / 2f) / scale + Cam.PosX) / size);
                float posY = (((e.Y - window.Height / 2f) / scale + Cam.PosY) / size);

                posX -= 0.5f * (Objects[SelectetBuildIndex.Value].Size - 1f);
                posY -= 0.5f * (Objects[SelectetBuildIndex.Value].Size - 1f);

                if (posX < 0) posX = 0;
                if (posY < 0) posY = 0;
                if (posX > World.Width) posX = World.Width-1;
                if (posY > World.Height) posY = World.Height-1;

                HoveredWorldPos = ((int)posX + (int)posY * World.Width);

                if (oldWorldPos != HoveredWorldPos) buildPreviewEnabled = true;
            }
            if (e.Button == MouseButtons.Left)
            {
                if (SelectetBuildIndex.BuildMode == 1)
                {
                    playerBuild(SelectetBuildIndex, HoveredWorldPos);
                }
            }

        }
        public void MouseDown(MouseEventArgs e)
        {
            mouse = e;
            mouseDownPos = e.Location;
            MouseDownWorldPos = HoveredWorldPos;
            if (SelectetBuildIndex.BuildMode == 1)
            {
                playerBuild(SelectetBuildIndex, HoveredWorldPos);
            }
        }
        public void MouseUp(MouseEventArgs e)
        {
            int buildMode = SelectetBuildIndex.BuildMode;
            int width = World.Width;

            mouse = e;
            if (buildMode == 0)
            {
                playerBuild(SelectetBuildIndex, HoveredWorldPos);
            }
            else
            {

                int pos = HoveredWorldPos;
                int x = pos % width;
                int y = (pos - x) / width;

                int pos2 = MouseDownWorldPos;
                int x2 = pos2 % width;
                int y2 = (pos2 - x2) / width;

                if (buildMode == 2 || buildMode == 3 || buildMode == 4)//line
                {
                    int direction = (Math.Abs(x - x2) > Math.Abs(y - y2)) ? 0 : 1;
                    int dist = (direction == 0) ? x - x2 : y - y2;
                    bool invert = false;
                    if (dist < 0)
                    {
                        dist = -dist;
                        invert = true;
                    }
                    bool live = true;
                    for (int i = 0; i <= dist; i++)
                    {
                        playerBuild(SelectetBuildIndex, pos2);
                        if (!invert)
                            pos2 += (direction == 0) ? 1 : width;
                        else
                            pos2 -= (direction == 0) ? 1 : width;
                        //if (builMode == 4 && !live) break;
                    }
                }
                else if (buildMode == 5 || buildMode == 6)//area
                {
                    int startX = Math.Min(x, x2);
                    int startY = Math.Min(y, y2);
                    int endX = Math.Max(x, x2);
                    int endY = Math.Max(y, y2);

                    int resetX = endX - startX;

                    pos = startX + startY * World.Width;

                    for (int iy = startY; iy <= endY; iy++)
                    {
                        for (int ix = startX; ix <= endX; ix++)
                        {
                            if (ix >= 0 && iy >= 0 && ix < width && iy < World.Height)
                            {
                                pos = ix + iy * width;
                                playerBuild(SelectetBuildIndex, pos);
                            }
                            pos += 1;
                        }
                        pos += width - resetX;

                    }
                }
            }
        }
    }
}