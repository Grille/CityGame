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
    public partial class Game
    {
        System.Windows.Forms.Control window;
        Texture texture;
        Texture gui;
   

        DateTime date;
        Random rnd;
        public GameObject[] Objects;
        public Zone[] Zones;
        public GameArea[] Areas;
        public GameResources[] Resources;
        public World World;
        public Camera Cam;

        public int MouseDownWorldPos = -1;
        public int HoveredWorldPos = -1;
        public BuildOption SelectetBuildIndex;
        private int CurBuildVersion = 3;

        Texture zoneTexture;
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

        public Timer timerRender;
        public Timer timerLogic;

        public Game(System.Windows.Forms.Control con)
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
        public void NewGame()
        {
            for (int i = 0; i < Resources.Length; i++)
                if (Resources[i]!=null)Resources[i].Value = Resources[i].InitValue;
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
