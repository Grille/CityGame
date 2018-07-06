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
        Control window;
        Texture texture;
        Texture gui;

        DateTime date;
        Random rnd;
        public GameObject[] objects;
        public GameResources[] resources;
        public World World;
        public Camera Cam;

        public int DownFieldPos = -1;
        public int hoveredWorldPos = -1;
        public int SelectetBuildIndex = 3;
        private int CurBuildVersion = 3;

        Texture groundTexture;
        Texture highViewMap;
        bool buildPreviewEnabled = true;
        Stopwatch animatorTimer;
        Point mouseDownPos;

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

        public Timer timerRender;
        public Timer timerLogic;

        public Game(Control con)
        {
            window = con;
            rnd = new Random();
            animatorTimer = new Stopwatch();
            animatorTimer.Start();
            timerDate = DateTime.Now.Ticks;
            animator = new int[20];
            date = new DateTime(2000, 1, 1);
            Cam = new Camera();

            timerRender = new Timer();
            timerLogic = new Timer();

            timerRender.Interval = 5;
            timerLogic.Interval = 5;

            timerRender.Tick += new System.EventHandler(render);
            timerLogic.Tick += new System.EventHandler(Simulate);
        }


        public void Start()
        {
            timerDate = DateTime.Now.Ticks;
            timerRender.Enabled = true;
            timerLogic.Enabled = true;
        }
        public void StartRendering()
        {
            timerRender.Enabled = true;
        }
        public void StopRendering() 
        {
            timerRender.Enabled = false;
        }
    }
}
