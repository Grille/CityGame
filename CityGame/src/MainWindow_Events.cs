﻿using System;
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
            Cam.Size += e.Delta;
        }
        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            int oldPos = CurFieldPos;
            mouse = e;
            if (timerLogic.Enabled == false) CurFieldPos = -1;
            else
            {
                float posX = e.X + Cam.DetailX;
                float posY = e.Y + Cam.DetailY;
                posX = (int)(posX / Cam.Size + Cam.PosX - 0.5 * (gameObject[CurBuildIndex].Size - 1f));
                posY = (int)(posY / Cam.Size + Cam.PosY - 0.5 * (gameObject[CurBuildIndex].Size - 1f));
                CurFieldPos = (int)(posX + posY * World.Width);

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
                Program.MenuWindow.Show(1);
                Program.MenuOverlay.Hide();
            }
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
        }
        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            Console.WriteLine("mouse");
        }
    }
}
