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
using GGL.Graphic;

namespace CityGame
{
    public partial class Game
    {
        MouseEventArgs mouse = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);

        public void MouseWheel(MouseEventArgs e)
        {
            float posX = -Cam.PosX + (e.X - window.Width / 2) / Cam.Scale;
            float posY = -Cam.PosY + (e.Y - window.Height / 2) / Cam.Scale;

            Cam.Scale += (e.Delta / 500f) * Cam.Scale;

            if (Cam.Scale < 0.01) Cam.Scale = 0.01f;
            else if (Cam.Scale > 1f) Cam.Scale = 1f;

            Cam.PosX += (Cam.PosX - (-posX + (window.Width / 2 * (e.X / (float)window.Width * 2 - 1)) / Cam.Scale));
            Cam.PosY += (Cam.PosY - (-posY + (window.Height / 2 * (e.Y / (float)window.Height * 2 - 1)) / Cam.Scale));

        }
        public void MouseMove(MouseEventArgs e)
        {

            int oldPos = CurFieldPos;
            mouse = e;

            if (timerLogic.Enabled == false) CurFieldPos = -1;
            else
            {
                float size = Cam.Size, scale = Cam.Scale;

                float posX = (int)(((e.X - window.Width / 2f) / scale + Cam.PosX) / size);
                float posY = (int)(((e.Y - window.Height / 2f) / scale + Cam.PosY) / size);

                posX -= 0.5f * objects[CurBuildIndex].Size - 1f;
                posY -= 0.5f * objects[CurBuildIndex].Size - 1f;

                CurFieldPos = ((int)posX + (int)posY * World.Width);

                if (oldPos != CurFieldPos) buildPreviewEnabled = true;
            }
            if (e.Button == MouseButtons.Left)
            {
                if (World.CanBuild((byte)CurBuildIndex, CurFieldPos) && objects[CurBuildIndex].BuildMode == 1)
                {
                    World.Build((byte)CurBuildIndex, CurFieldPos);
                    buildPreviewEnabled = false;
                }
            }

        }
        public void MouseDown(MouseEventArgs e)
        {
            mouse = e;
            mouseDownPos = e.Location;
            DownFieldPos = CurFieldPos;
            if (World.CanBuild((byte)CurBuildIndex, CurFieldPos) && (objects[CurBuildIndex].BuildMode == 0 || objects[CurBuildIndex].BuildMode == 1))
            {
                World.Build((byte)CurBuildIndex, CurFieldPos);
                buildPreviewEnabled = false;
            }
        }
        public void MouseUp(MouseEventArgs e)
        {
            mouse = e;
            if (World.CanBuild((byte)CurBuildIndex, CurFieldPos) && objects[CurBuildIndex].BuildMode == 2)
            {
                World.Build((byte)CurBuildIndex, CurFieldPos);
                buildPreviewEnabled = false;
            }
        }
    }
}