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
    [System.ComponentModel.DesignerCategory("code")]
    public partial class MainWindow : Form
    {
        [DllImport("user32.dll")]
        public extern static int SetForegroundWindow(IntPtr HWnd);
        
        MouseEventArgs mouse = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);
        Texture texture;
        Texture gui;

        int RenderMode = 0;

        DateTime date;
        Random rnd;
        public GameObject[] gameObject;
        public GameResources[] resources;
        Texture groundTexture;
        Texture highViewMap;
        public World World;
        public Camera Cam;
        public int CurField = -1;
        public int CurBuild = 3;
        private int CurBuildVersion = 3;

        bool isRendering = false;
        bool showCurBuild = true;
        Stopwatch animatorTimer;
        Point mouseDownPos;
        Task renderTask;

        long timerDate;
        int timer010;
        int timer050;
        int timer100;
        int timer200;
        int timer500;
        int timer1000;
        int[] animator;

        int basicShader;
        int glowShader;



        //init OpenGL
        public MainWindow()
        {
            saveMiMap();
            InitializeComponent();

            DoubleBuffered = true;
            GL2D.SetRenderControl(this);
            Resize += new EventHandler(OnRenderControlResize);

            highViewMap = new Texture(1, 1, new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255 });
            rnd = new Random();
            animatorTimer = new Stopwatch();
            animatorTimer.Start();
            timerDate = DateTime.Now.Ticks;
            animator = new int[20];
            date = new DateTime(2000, 1, 1);
        }

        public void saveMiMap()
        {
            Random rnd = new Random();
            GGL.IO.ByteStream bs = new GGL.IO.ByteStream();
            bs.ResetIndex();
            byte mode = 1;
            bs.WriteInt(mode);//mode
            if (mode == 0)
            {
                for (int i = 0; i < 255; i++)
                {
                    bs.WriteByte((byte)(255 * rnd.NextDouble()));//r
                    bs.WriteByte((byte)(255 * rnd.NextDouble()));//g
                    bs.WriteByte((byte)(255 * rnd.NextDouble()));//b
                }
            }
            else if (mode == 1)
            {
                //
                bs.WriteByte((byte)(255 * rnd.NextDouble()));//r
                bs.WriteByte((byte)(255 * rnd.NextDouble()));//g
                bs.WriteByte((byte)(255 * rnd.NextDouble()));//b
                //
                bs.WriteByte((byte)(255 * rnd.NextDouble()));//r
                bs.WriteByte((byte)(255 * rnd.NextDouble()));//g
                bs.WriteByte((byte)(255 * rnd.NextDouble()));//b
                //
                bs.WriteByte((byte)(255 * rnd.NextDouble()));//r
                bs.WriteByte((byte)(255 * rnd.NextDouble()));//g
                bs.WriteByte((byte)(255 * rnd.NextDouble()));//b
                for (int i = 0; i < 255; i++)
                {
                    bs.WriteByte((byte)(255 * rnd.NextDouble()));//r
                    bs.WriteByte((byte)(255 * rnd.NextDouble()));//a
                }
            }
            bs.Save("../Data/minimaps/default.mpd");
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
            Console.WriteLine("Renderer: "+ GL.GetString(StringName.Renderer));
            Console.WriteLine("Version: "+ GL.GetString(StringName.Version));
            Console.WriteLine("ShadingLanguageVersion: "+ GL.GetString(StringName.ShadingLanguageVersion));
            Console.WriteLine("Vendor: "+ GL.GetString(StringName.Vendor));
            
            //int game
            int code;
            if ((code = initOpenGL()) != 0)
            {
                if (code == 1) MessageBox.Show("Could not initialize OpenGL\n\nMinimum required OpenGL version: 2.0\nOpenGL version detectet: " + GL.GetString(StringName.Version), "Error by init OpenGL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (code == 2) MessageBox.Show("Could not initialize OpenGL\n\nRender form == null\n", "Error by init OpenGL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (code == 3) MessageBox.Show("Could not initialize OpenGL\n\nOpenGL context could not be created\nPlease update graphic drivers", "Error by init OpenGL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (code == 4) MessageBox.Show("Could not initialize OpenGL\n\nShader compilation failed\n" + GL2D.GetError(), "Error by init OpenGL", MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();return;
            }
            Console.WriteLine("loadData()");
            loadData();
            GGL.IO.ByteStream bs = new GGL.IO.ByteStream();
            bs.ResetIndex();
            for (int i = 0; i < gameObject.Length; i++)
            {
                if (gameObject[i].Name == "-") continue;
                bs.WriteByte((byte)i);
                bs.WriteString(gameObject[i].Name);
                bs.WriteString(gameObject[i].Path);
                bs.WriteByte((byte)gameObject[i].BuildMode);
                bs.WriteByte((byte)gameObject[i].BuildMode);
                bs.WriteByte((byte)gameObject[i].Diversity);
                bs.WriteByte((byte)gameObject[i].Size);
                bs.WriteByte((byte)gameObject[i].GroundMode);
                bs.WriteByte((byte)gameObject[i].GraphicMode);
                bs.WriteByteArray(gameObject[i].GraphicNeighbors);

                bs.WriteByteArray(gameObject[i].UpgradeTyp);
                bs.WriteByteArray(gameObject[i].DowngradeTyp);
                bs.WriteByteArray(gameObject[i].DemolitionTyp);
                bs.WriteByteArray(gameObject[i].DecayTyp);
                bs.WriteByteArray(gameObject[i].DestroyTyp);
                bs.WriteByteArray(gameObject[i].CanBuiltOnTyp);

               
                //bs.WriteByteArray(gameObject[i].graphicNeighbors);
            }
            bs.Save("../test.txt");

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
            int code;
            if ((code=GL2D.IsRendererReady())>0) return code;

            Console.WriteLine(GL.GetString(StringName.Renderer));
            Console.WriteLine(GL.GetString(StringName.Version));
            basicShader = GL2D.CreateShader(File.ReadAllText("../Data/Shaders/basicVS.glsl"), File.ReadAllText("../Data/Shaders/basicFS.glsl"));
            if (basicShader < 0) return 4;
            glowShader = GL2D.CreateShader(File.ReadAllText("../Data/Shaders/glowVS.glsl"), File.ReadAllText("../Data/Shaders/glowFS.glsl"));
            if (glowShader < 0) return 4;
            GL2D.UseShader(basicShader);
            GL2D.CreateBuffer(2265536);
            // Other state


            Console.WriteLine("version:" + GL.GetString(StringName.Version));

            return 0;
        }
        private void loadData()
        {
            //effects: 0=not, 1=up, 2=down, 3=break, 4=deacy, 5=destroy 6=entf//
            //AreaPermanent->typ: 0=water, 1=nature, 2=road,3=saltwater
            Pharse.ReplaceList = new string[] {
                "effect.not", "0","effect.up", "1","effect.down", "2","effect.break", "3","effect.deacy", "4","effect.destroy", "5","effect.entf", "6",
                "res.money", "0", "res.energy", "1", "res.water", "2", "res.waste", "3",
                "area.water", "0","area.pollution", "1","area.road", "2","area.saltwater", "3"
            };
            Console.WriteLine("//load: gameResourcesData");
            object[,] gameResourcesData = Pharse.Load("../Data/config/gameResources.gd");
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
            object[,] gameObjectData = Pharse.Load("../Data/config/gameObject.gd");
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
                    (int[])gameObjectData[i, index++],
                    (int[,])gameObjectData[i, index++]
                    );
                gameObject[i].LoadSimData(
                    (int[,])gameObjectData[i, index++],
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
            timerDate = DateTime.Now.Ticks;
            timerRender.Enabled = true;
            timerLogic.Enabled = true;
        }

        private void simulate()
        {
            updateLabel();
            //date = new DateTime(1990, 1, 1);
            int ticks = (int)(DateTime.Now.Ticks - timerDate);
            timerDate = DateTime.Now.Ticks;
            timer010 += ticks;
            while (timer010 > 10 * TimeSpan.TicksPerMillisecond)
            {
                timer010 -= (int)(10 * TimeSpan.TicksPerMillisecond);
                if (mouse.X < 1) Cam.Move(-32, 0);
                if (mouse.Y < 1) Cam.Move(0, -32);
                if (mouse.X >= this.Width - 1) Cam.Move(+32, 0);
                if (mouse.Y >= this.Height - 1) Cam.Move(0, +32);
            }
            ticks *= 1;
            timer050 += ticks;
            while (timer050 > 50 * TimeSpan.TicksPerMillisecond)
            {
                timer050 -= (int)(50 * TimeSpan.TicksPerMillisecond);
            }
            timer100 += ticks;
            while (timer100 > 100 * TimeSpan.TicksPerMillisecond)
            {
                timer100 -= (int)(100 * TimeSpan.TicksPerMillisecond);
            }
            timer200 += ticks;
            while (timer200 > 200 * TimeSpan.TicksPerMillisecond)
            {
                timer200 -= (int)(200 * TimeSpan.TicksPerMillisecond);
                date = date.AddMinutes(10);

            }
            timer500 += ticks;
            while (timer500 > 500 * TimeSpan.TicksPerMillisecond)
            {
                timer500 -= (int)(500 * TimeSpan.TicksPerMillisecond);
                Program.MenuOverlay.pictureBoxMinimap.Image = RenderMinimap();
            }
            timer1000 += ticks;
            while (timer1000 > 1000 * TimeSpan.TicksPerMillisecond)
            {
                timer1000 -= (int)(1000 * TimeSpan.TicksPerMillisecond);
                for (int i = 0; i < resources.Length; i++)
                {
                    resources[i].Update(30);
                }
                for (int i = 0; i < (World.Width + World.Height) / 1; i++)
                {
                    World.UpdateField((int)(World.Width * World.Height * rnd.NextDouble()));
                }
                date = date.AddDays(1);
            }
        }
        private void updateLabel() 
        {
            Program.MenuOverlay.label5.Text = "" + (int)resources[0].Value + ",-";
            Program.MenuOverlay.label8.Text = "" + resources[0].AddValue;
        }
    }
}
