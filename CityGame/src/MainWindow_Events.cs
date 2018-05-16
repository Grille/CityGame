using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
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
    public partial class MainWindow : Form // Event
    {
        private void timerRender_Tick(object sender, EventArgs e)
        {
            Render();
            //if (!isRendering)
            //{
            //    renderTask = new Task(() => Render());
            //    renderTask.Start();
            //}

        }
        private void timerLogic_Tick(object sender, EventArgs e)
        {
            simulate();
        }

        private void OnRenderControlResize(object sender, EventArgs e)
        {
            if (Program.MenuOverlay != null)Program.MenuOverlay.Size = this.Size;
        }
        private void MainWindow_Shown(object sender, EventArgs e)
        {
            initGame();
        }

        private void MainWindow_MouseWheel(object sender, MouseEventArgs e)
        {

            float posX = -Cam.PosX + (e.X - Width / 2) / Cam.Scale;
            float posY = -Cam.PosY + (e.Y - Height / 2) / Cam.Scale;

            Cam.Scale += (e.Delta / 500f) * Cam.Scale;

            if (Cam.Scale < 0.01) Cam.Scale = 0.01f;
            else if (Cam.Scale > 1f) Cam.Scale = 1f;

            Cam.PosX += (Cam.PosX - (-posX + (Width / 2 * (e.X / (float)Width * 2 - 1)) / Cam.Scale));
            Cam.PosY += (Cam.PosY - (-posY + (Height / 2 * (e.Y / (float)Height * 2 - 1)) / Cam.Scale));
            
        }
        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            int oldPos = CurFieldPos;
            mouse = e;
            
            if (timerLogic.Enabled == false) CurFieldPos = -1;
            else
            {
                float size = Cam.Size, scale = Cam.Scale;

                float posX = (int)(((e.X - Width / 2f) / scale + Cam.PosX) / size);
                float posY = (int)(((e.Y - Height / 2f) / scale + Cam.PosY) / size);

                posX -= 0.5f * gameObject[CurBuildIndex].Size - 1f;
                posY -= 0.5f * gameObject[CurBuildIndex].Size - 1f;

                CurFieldPos = ((int)posX + (int)posY * World.Width);

                if (oldPos!=CurFieldPos) showCurBuild = true;
            }
            if (e.Button == MouseButtons.Left)
            {
                if (World.CanBuild((byte)CurBuildIndex, CurFieldPos) && gameObject[CurBuildIndex].BuildMode == 1)
                {
                    World.Build((byte)CurBuildIndex, CurFieldPos);
                    showCurBuild = false;
                }
            }
            
        }
        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            mouse = e;
            mouseDownPos = e.Location;
            DownFieldPos = CurFieldPos;
            if (World.CanBuild((byte)CurBuildIndex, CurFieldPos) && (gameObject[CurBuildIndex].BuildMode == 0 || gameObject[CurBuildIndex].BuildMode == 1))
            {
                World.Build((byte)CurBuildIndex, CurFieldPos);
                showCurBuild = false;
            }
        }
        private void MainWindow_MouseUp(object sender, MouseEventArgs e)
        {
            mouse = e;
            if (World.CanBuild((byte)CurBuildIndex, CurFieldPos) && gameObject[CurBuildIndex].BuildMode == 2)
            {
                World.Build((byte)CurBuildIndex, CurFieldPos);
                showCurBuild = false;
            }
        }
        private void MainWindow_MouseLeave(object sender, EventArgs e)
        {
            CurFieldPos = -1;
        }

        public void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                timerLogic.Enabled = false;
                Program.MenuWindow.Show(NextPanel.GameMenu);
                Program.MenuOverlay.Hide();
            }
            /*
            else if (e.KeyCode == Keys.Subtract)
            {
                int pos = Cam.GetCenter();
                if (Cam.Size >= 4) Cam.Size /= 2;
                //Cam.SetCenter(pos);
            }
            else if (e.KeyCode == Keys.Add)
            {
                int pos = Cam.GetCenter();
                if (Cam.Size < 64) Cam.Size *= 2;
                //Cam.SetCenter(pos);
            }
            */
        }
        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            Console.WriteLine("mouse");
        }
    }
}
