﻿using System;
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
    //[System.ComponentModel.DesignerCategory("code")]
    public partial class MainWindow : Form
    {
        [DllImport("user32.dll")]
        public extern static int SetForegroundWindow(IntPtr HWnd);
        
        MouseEventArgs mouse = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);

        //init OpenGL
        public MainWindow()//0.28
        {
            InitializeComponent();

            DoubleBuffered = true;
            GL2D.SetRenderControl(this);
            Resize += new EventHandler(OnRenderControlResize);
            MouseWheel += new MouseEventHandler(MainWindow_MouseWheel);

            Console.WriteLine("initWindow()");
        }

        // init Game
        private void initGame()
        {
            Console.WriteLine("initGame()");
            Program.Game = new Game(this);
            //show loading screen
            this.Location = new Point(SystemInformation.VirtualScreen.Width / 2 - 320, SystemInformation.VirtualScreen.Height / 2 - 200);
            this.Size = new Size(640, 400);
            pictureBoxLoad.Image = new Bitmap("../Data/texture/gui/load.png");
            pictureBoxLoad.Location = new Point(0, 0);
            pictureBoxLoad.Size = this.Size;
            pictureBoxLogo.Image = new Bitmap("../Data/texture/gui/grille.png");
            pictureBoxLogo.Location = new Point(560 + 40 - 128, 320-128);
            pictureBoxLogo.Size = new Size(128,96);
            progressBarLoad.Location = new Point(40, 320);
            progressBarLoad.Size = new Size(560, 40);
            this.Refresh();

            Console.WriteLine("initOpenGL()");
            Console.WriteLine("Renderer: "+ GL.GetString(StringName.Renderer));
            Console.WriteLine("Version: "+ GL.GetString(StringName.Version));
            Console.WriteLine("ShadingLanguageVersion: "+ GL.GetString(StringName.ShadingLanguageVersion));
            Console.WriteLine("Vendor: "+ GL.GetString(StringName.Vendor));
            
            //int game
            Console.WriteLine("initOpenGL()");
            int code;
            if ((code = Program.Game.InitOpenGL()) != 0)
            {
                if (code == 1) MessageBox.Show("Could not initialize OpenGL\n\nMinimum required OpenGL version: 2.0\nOpenGL version detectet: " + GL.GetString(StringName.Version), "Error by init OpenGL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (code == 2) MessageBox.Show("Could not initialize OpenGL\n\nRender form == null\n", "Error by init OpenGL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (code == 3) MessageBox.Show("Could not initialize OpenGL\n\nOpenGL context could not be created\nPlease update graphic drivers", "Error by init OpenGL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (code == 4) MessageBox.Show("Could not initialize OpenGL\n\nShader compilation failed\n" + GL2D.GetError(), "Error by init OpenGL", MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();return;
            }

            Console.WriteLine("loadData()");
            Program.Game.LoadData(progressBarLoad);
            GGL.IO.ByteStream bs = new GGL.IO.ByteStream();
            bs.ResetIndex();


            Program.Game.Cam = new Camera();
            Program.Game.World = new World(Program.Game.objects, Program.Game.resources, Program.Game.Cam, Program.Game.areas);
            Program.Game.World.BuildWorld(32, 32);

    
            //set fullscreen
            this.Visible = false;
            this.Location = new Point(0, 0);
            this.Size = SystemInformation.VirtualScreen.Size;
            pictureBoxLoad.Visible = false;
            pictureBoxLogo.Visible = false;
            progressBarLoad.Visible = false;
            Program.Game.StartRendering();
            SetForegroundWindow(this.Handle);
            BringToFront();
            Focus();
            //RenderMode = 1;
            Visible = true;
            Refresh();

            Console.WriteLine("//startGame");
            //show menu
            Program.MenuWindow.Show(0);
        }

        private void OnRenderControlResize(object sender, EventArgs e)
        {
            if (Program.MenuOverlay != null) Program.MenuOverlay.Size = this.Size;
        }
        private void MainWindow_Shown(object sender, EventArgs e)
        {
            initGame();
        }

        private void MainWindow_MouseWheel(object sender, MouseEventArgs e)
        {
            Program.Game.MouseWheel(e);
        }
        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            Program.Game.MouseMove(e);
        }
        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            Program.Game.MouseDown(e);
        }
        private void MainWindow_MouseUp(object sender, MouseEventArgs e)
        {
            Program.Game.MouseUp(e);
        }
        private void MainWindow_MouseLeave(object sender, EventArgs e)
        {
            Program.Game.HoveredWorldPos = -1;
        }

        public void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            Program.Game.KeyDown(e);
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            Program.Game.KeyUp(e);
        }
    }
}