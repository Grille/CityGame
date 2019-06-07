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
using CsGL2D;

namespace CityGame
{
    //[System.ComponentModel.DesignerCategory("code")]
    public partial class MainWindow : Form
    {
        [DllImport("user32.dll")]
        public extern static int SetForegroundWindow(IntPtr HWnd);
        
        MouseEventArgs mouse = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);

        public Context Ctx;
        Texture tex = new Texture("../Data/texture/ground/texture.png");
        //init OpenGL
        public MainWindow()//0.28
        {
            InitializeComponent();

            
            DoubleBuffered = true;

            Resize += new EventHandler(OnRenderControlResize);
            MouseWheel += new MouseEventHandler(MainWindow_MouseWheel);

            Console.WriteLine("initWindow()");
            initGame();
            
        }

        // init Game
        private void initGame()
        {
            Console.WriteLine("initGame()");
            //show loading screen
            this.Size = new Size(640, 400);
            pictureBoxLoad.Image = new Bitmap("../Data/texture/gui/load2.png");
            pictureBoxLoad.Location = new Point(0, 0);
            pictureBoxLoad.Size = this.Size;
            pictureBoxLogo.Image = new Bitmap("../Data/texture/gui/grille logo 3.png");
            pictureBoxLogo.Location = new Point(560 + 40 - 128, 320-128);
            pictureBoxLogo.Size = new Size(128,96);
            progressBarLoad.Location = new Point(40, 320);
            progressBarLoad.Size = new Size(560, 40);
            progressBarLoad.Value = 0;
            this.StartPosition = FormStartPosition.CenterScreen;
            Show();
            //this.Location = new Point(SystemInformation.VirtualScreen.Width / 2 - 320, SystemInformation.VirtualScreen.Height / 2 - 200);
            this.Refresh();
            Program.Game = new Game(this);

            //int game

            Console.WriteLine("initOpenGL()");
            //GL2D.SetRenderControl(this);

            Console.WriteLine("loadData()");
            Program.Game.LoadData(progressBarLoad);
            GGL.IO.ByteStream bs = new GGL.IO.ByteStream();
            bs.ResetIndex();

            Program.Game.Cam = new Camera();
            Program.Game.World = new World(32, 32);

            //set fullscreen
            pictureBoxLoad.Visible = false;
            pictureBoxLogo.Visible = false;
            progressBarLoad.Visible = false;
            this.BackColor = Color.Black;
            this.Location = new Point(0, 0);
            this.Size = SystemInformation.VirtualScreen.Size;
            SetForegroundWindow(this.Handle);
            BringToFront();
            Focus();
            Visible = false;
            Visible = true;
            Refresh();

            Ctx = new Context(this);
            int code;
            if ((code = Program.Game.InitOpenGL()) != 0)
            {
                if (code == 1) MessageBox.Show("Could not initialize OpenGL\n\nMinimum required OpenGL version: 2.0\nOpenGL version detectet: " + GL.GetString(StringName.Version), "Error by init OpenGL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (code == 2) MessageBox.Show("Could not initialize OpenGL\n\nRender form == null\n", "Error by init OpenGL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (code == 3) MessageBox.Show("Could not initialize OpenGL\n\nOpenGL context could not be created\nPlease update graphic drivers", "Error by init OpenGL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //else if (code == 4) MessageBox.Show("Could not initialize OpenGL\n\nShader compilation failed\n" + GL2D.GetError(), "Error by init OpenGL", MessageBoxButtons.OK,MessageBoxIcon.Error);
                //Program.MenuWindow.Close();
                //Application.Exit();
            }
            Program.Game.StartRendering();

            Console.WriteLine("//startGame");
            Program.MenuWindow.Show(NextPanel.MainMenu);
        }

        private void OnRenderControlResize(object sender, EventArgs e)
        {
            if (Program.MenuOverlay != null) Program.MenuOverlay.Size = this.Size;
        }
        private void MainWindow_Shown(object sender, EventArgs e)
        {

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
