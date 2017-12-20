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
        private void timerRender_Tick(object sender, EventArgs e)
        {
            Render();
            renderControl.SwapBuffers();
        }
        private void timerLogic_Tick(object sender, EventArgs e)
        {
            int ticks = (int)(DateTime.Now.Ticks - date);
            date = DateTime.Now.Ticks;
            timer010 += ticks;
            while (timer010 > 10 * TimeSpan.TicksPerMillisecond)
            {
                timer010 -= (int)(10 * TimeSpan.TicksPerMillisecond);
                if (mouse.X < 1) Cam.Move(-32, 0);
                if (mouse.Y < 1) Cam.Move(0, -32);
                if (mouse.X >= this.Width - 1) Cam.Move(+32, 0);
                if (mouse.Y >= this.Height - 1) Cam.Move(0, +32);
            }
            timer050 += ticks;
            while (timer050 > 50 * TimeSpan.TicksPerMillisecond)
            {
                timer050 -= (int)(50 * TimeSpan.TicksPerMillisecond);
                for (int i = 0; i < (World.Width + World.Height)/8; i++)
                {
                   World.UpdateField((int)(World.Width * World.Height * rnd.NextDouble()));
                }
            }
            timer100 += ticks;
            while (timer100 > 100 * TimeSpan.TicksPerMillisecond)
            {
                timer100 -= (int)(100 * TimeSpan.TicksPerMillisecond);
            }
        }

        private void OnRenderControlResize(object sender, EventArgs e)
        {
            Program.MenuOverlay.Size = this.Size;
            renderControl.Size = this.Size;
            GL2D.UpdateSize(this.Size);
        }
        private void MainWindow_Shown(object sender, EventArgs e)
        {
            initGame();
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            int oldPos = CurField;
            mouse = e;
            if (timerLogic.Enabled == false) CurField = -1;
            else
            {
                float posX = e.X + Cam.DetailX;
                float posY = e.Y + Cam.DetailY;
                posX = (int)(posX / Cam.Size + Cam.PosX - 0.5 * (gameObject[CurBuild].Size - 1f));
                posY = (int)(posY / Cam.Size + Cam.PosY - 0.5 * (gameObject[CurBuild].Size - 1f));
                CurField = (int)(posX + posY * World.Width);

                if (oldPos!=CurField) showCurBuild = true;
            }
            if (e.Button == MouseButtons.Left)
            {
                if (World.CanBuild((byte)CurBuild, CurField) && gameObject[CurBuild].BuildMode == 1)
                {
                    World.Build((byte)CurBuild, CurField);
                    showCurBuild = false;
                }
            }
        }
        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            mouse = e;
            mouseDownPos = e.Location;
            if (World.CanBuild((byte)CurBuild, CurField) && (gameObject[CurBuild].BuildMode == 0 || gameObject[CurBuild].BuildMode == 1))
            {
                World.Build((byte)CurBuild, CurField);
                showCurBuild = false;
            }
        }
        private void MainWindow_MouseUp(object sender, MouseEventArgs e)
        {
            mouse = e;
            if (World.CanBuild((byte)CurBuild, CurField) && gameObject[CurBuild].BuildMode == 2)
            {
                World.Build((byte)CurBuild, CurField);
                showCurBuild = false;
            }
        }
        private void MainWindow_MouseLeave(object sender, EventArgs e)
        {
            CurField = -1;
        }

        public void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                timerLogic.Enabled = false;
                Program.MenuWindow.Show(1);
                Program.MenuOverlay.Hide();
            }
            else if (e.KeyCode == Keys.Subtract)
            {
                int pos = Cam.GetCenter();
                if (Cam.Size >= 16) Cam.Size /= 2;
                //Cam.SetCenter(pos);
            }
            else if (e.KeyCode == Keys.Add)
            {
                int pos = Cam.GetCenter();
                if (Cam.Size < 64) Cam.Size *= 2;
                //Cam.SetCenter(pos);
            }
        }
        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            Console.WriteLine("mouse");
        }
    }
}
