using System;
using System.IO;
using System.Collections.Generic;
//using System.Linq;
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
    [System.ComponentModel.DesignerCategory("code")]
    public partial class MainWindow : Form
    {
        [DllImport("user32.dll")]
        public extern static int SetForegroundWindow(IntPtr HWnd);
        
        MouseEventArgs mouse = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);
        GLControl renderControl;
        Texture texture;
        Texture gui;

        int RenderMode = 0;

        Random rnd;
        GameObject[] gameObject;
        GameResources[] resources;
        Texture groundTexture;
        public World World;
        public Camera Cam;
        public int CurField = -1;
        public int CurBuild = 3;
        private int CurBuildVersion = 3;

        bool showCurBuild = true;
        Stopwatch animatorTimer;
        Point mouseDownPos;
        long date;
        int timer010;
        int timer050;
        int timer100;
        int[] animator;

        int basicShader;
        int glowShader;



        //init OpenGL
        public MainWindow()
        {
            InitializeComponent();
            
            DoubleBuffered = true;
            GL2D.SetRenderControl(this);
            Resize += new EventHandler(OnRenderControlResize);

            rnd = new Random();
            animatorTimer = new Stopwatch();
            animatorTimer.Start();
            date = DateTime.Now.Ticks;
            animator = new int[20];

        }

        // init Game
        private void initGame()
        {
            //show loading screen
            this.Location = new Point(SystemInformation.VirtualScreen.Width / 2 - 320, SystemInformation.VirtualScreen.Height / 2 - 200);
            this.Size = new Size(640, 400);
            pictureBoxLoad.Image = new Bitmap("../Data/texture/gui/load.png");
            pictureBoxLoad.Location = new Point(0, 0);
            pictureBoxLoad.Size = this.Size;
            progressBarLoad.Location = new Point(40, 320);
            progressBarLoad.Size = new Size(560, 40);
            this.Refresh();

            Console.WriteLine("initOpenGL()");
            //int game
            if (initOpenGL() != 0)
            {
                MessageBox.Show("Could not initialize OpenGL\n\nMinimum required OpenGL version: 3.0\nOpenGL version detectet: "+ GL.GetString(StringName.Version), "Error by init OpenGL", MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
            Console.WriteLine("loadData()");
            loadData();
            World = new World(gameObject, resources);
            World.BuildWorld(32, 32);
            Cam = new Camera(World, this);

            //set fullscreen
            this.Visible = false;
            this.Location = new Point(0, 0);
            this.Size = SystemInformation.VirtualScreen.Size;
            pictureBoxLoad.Visible = false;
            progressBarLoad.Visible = false;
            this.timerRender.Enabled = true;
            SetForegroundWindow(this.Handle);
            BringToFront();
            Focus();
            RenderMode = 1;
            Visible = true;
            Refresh();

            Console.WriteLine("//startGame");
            //show menu
            Program.MenuWindow.Show(0);
        }
        private int initOpenGL()
        {
            if (!GL2D.IsRendererReady()) return 1;

            Console.WriteLine(GL.GetString(StringName.Renderer));
            Console.WriteLine(GL.GetString(StringName.Version));
            basicShader = GL2D.CreateShader(File.ReadAllText("../Data/Shaders/basicVS.glsl"), File.ReadAllText("../Data/Shaders/basicFS.glsl"));
            if (basicShader < 0) return 2;
            glowShader = GL2D.CreateShader(File.ReadAllText("../Data/Shaders/glowVS.glsl"), File.ReadAllText("../Data/Shaders/glowFS.glsl"));
            if (glowShader < 0) return 2;
            GL2D.UseShader(basicShader);
            GL2D.CreateBuffer(500000);
            // Other state


            Console.WriteLine("version:" + GL.GetString(StringName.Version));

            return 0;
        }
        private void loadData()
        {
            Console.WriteLine("//load: gameResourcesData");
            object[,] gameResourcesData = LoadObjects.Load("../Data/config/gameResources.gd");
            resources = new GameResources[gameResourcesData.GetLength(0)];
            for (int i = 0; i < gameResourcesData.GetLength(0); i++)
            {
                int index = 0;
                resources[i] = new GameResources();
                resources[i].Load(
                    (string)gameResourcesData[i, index++],
                    (int)gameResourcesData[i, index++],
                    (bool)gameResourcesData[i, index++],
                    (bool)gameResourcesData[i, index++]);
            }
            Console.WriteLine("//load: gameObjectData");
            object[,] gameObjectData = LoadObjects.Load("../Data/config/gameObject.gd");
            gameObject = new GameObject[gameObjectData.GetLength(0)];
            this.progressBarLoad.Maximum = gameObjectData.GetLength(0) + 2;
            for (int i = 0; i < gameObjectData.GetLength(0); i++)
            {
                int index = 0;
                this.progressBarLoad.Value += 1;
                gameObject[i] = new GameObject();
                gameObject[i].LoadBasic(
                    (string)gameObjectData[i, index++], 
                    (string)gameObjectData[i, index++],
                    (string)gameObjectData[i, index++],
                    (int)gameObjectData[i, index++], //buildMode 
                    (int)gameObjectData[i, index++],
                    (int)gameObjectData[i, index++], 
                    (int)gameObjectData[i, index++],
                    (int)gameObjectData[i, index++],
                    (int)gameObjectData[i, index++],
                    (int[])gameObjectData[i, index++]);
                gameObject[i].LoadTypRefs(
                    (int[])gameObjectData[i, index++], 
                    (int[])gameObjectData[i, index++], 
                    (int[])gameObjectData[i, index++], 
                    (int[])gameObjectData[i, index++], 
                    (int[])gameObjectData[i, index++], 
                    (int[])gameObjectData[i, index++] 
                    );
                gameObject[i].LoadSimData(
                    (int[,])gameObjectData[i, index++],
                    (int[,])gameObjectData[i, index++], 
                    (int[,])gameObjectData[i, index++], 
                    (int[,])gameObjectData[i, index++], 
                    (int[,])gameObjectData[i, index++], 
                    (int[,])gameObjectData[i, index++] 
                    );
            }
            texture = new Texture("../Data/texture/texture.png");
            groundTexture = new Texture("../Data/texture/ground/texture.png");
            gui = new Texture("../Data/texture/gui/aktivField.png");
        }

        public void StartGame()
        {
            date = DateTime.Now.Ticks;
            timerRender.Enabled = true;
            timerLogic.Enabled = true;
        }

        private void simulate()
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
                for (int i = 0; i < (World.Width + World.Height) / 8; i++)
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

    }
}
