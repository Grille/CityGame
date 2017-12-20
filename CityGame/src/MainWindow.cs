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
            
            this.DoubleBuffered = true;
            
            renderControl = new OpenTK.GLControl(
                GraphicsMode.Default,3, 0,
                GraphicsContextFlags.ForwardCompatible | GraphicsContextFlags.Default);
            //m_renderControl.Dock = DockStyle.Fill;
            renderControl.BackColor = Color.Black;
            renderControl.BorderStyle = BorderStyle.FixedSingle;
            renderControl.Enabled = false;
            this.Controls.Add(renderControl);
            //renderControl.IsHandleCreated
            //renderControl.CreateHandle();


            //m_renderControl.HandleDestroyed += new EventHandler(OnRenderControlHandleDestroyed);
            this.Resize += new EventHandler(OnRenderControlResize);

            rnd = new Random();

            animatorTimer = new Stopwatch();
            animatorTimer.Start();
            date = DateTime.Now.Ticks;
            animator = new int[20];

        }

        // init Game
        private void initGame()
        {
            Console.WriteLine("show");

            renderControl.Visible = false;

            this.Location = new Point(SystemInformation.VirtualScreen.Width / 2 - 320, SystemInformation.VirtualScreen.Height / 2 - 200);
            this.Size = new Size(640, 400);
            pictureBoxLoad.Image = new Bitmap("../Data/texture/gui/load.png");
            pictureBoxLoad.Location = new Point(0, 0);
            pictureBoxLoad.Size = this.Size;

            progressBarLoad.Location = new Point(40, 320);
            progressBarLoad.Size = new Size(560, 40);


            this.Refresh();

            if (initOpenGL() != 0)
            {
                MessageBox.Show("Could not initialize OpenGL\n\nMinimum required OpenGL version: 3.0\nOpenGL version detectet: "+ GL.GetString(StringName.Version), "Error by init OpenGL", MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
            loadData();
            World = new World(gameObject, resources);
            World.BuildWorld(512, 512);
            Cam = new Camera(World, renderControl);

            this.Visible = false;
            this.Location = new Point(0, 0);
            this.Size = SystemInformation.VirtualScreen.Size;
            pictureBoxLoad.Visible = false;
            progressBarLoad.Visible = false;
            renderControl.Visible = true;
            this.timerRender.Enabled = true;

            SetForegroundWindow(this.Handle);
            this.BringToFront();
            this.Focus();
            RenderMode = 1;

            Program.MenuWindow.Show(0);
            this.Visible = true;
            this.Refresh();
        }
        private int initOpenGL()
        {
            if (!renderControl.IsHandleCreated) return 1;
            
            Console.WriteLine(GL.GetString(StringName.Renderer));
            Console.WriteLine(GL.GetString(StringName.Version));
            basicShader = GL2D.CreateShaders("../Data/Shaders/basicVS.glsl", "../Data/Shaders/basicFS.glsl");
            if (basicShader == -1) return 2;
            glowShader = GL2D.CreateShaders("../Data/Shaders/glowVS.glsl", "../Data/Shaders/glowFS.glsl");
            if (glowShader == -1) return 2;
            GL2D.UseShader(basicShader);
            GL2D.CreateBuffer(500000);
            // Other state

            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.ClearColor(System.Drawing.Color.MidnightBlue);

            Console.WriteLine("version:" + GL.GetString(StringName.Version));

            return 0;
        }
        private void loadData()
        {
            object[,] gameResourcesData = LoadObjects.Load("../Data/config/gameResources.gd");
            resources = new GameResources[gameResourcesData.GetLength(0)];
            for (int i = 0; i < gameResourcesData.GetLength(0); i++)
            {
                int index = 0;
                resources[i] = new GameResources();
                resources[i].Load(
                    (string)gameResourcesData[i, index++],
                    (bool)gameResourcesData[i, index++],
                    (bool)gameResourcesData[i, index++]);
            }

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


    }
}
