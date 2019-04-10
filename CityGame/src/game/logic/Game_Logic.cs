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


namespace CityGame
{
    public partial class Game
    {
        public void Simulate(object sender, EventArgs e)
        {
            //updateLabel();
            //date = new DateTime(1990, 1, 1);
            int ticks = (int)(DateTime.Now.Ticks - timerDate);
            timerDate = DateTime.Now.Ticks;

            timer010 += ticks;
            while (timer010 > 10 * TimeSpan.TicksPerMillisecond)
            {

                int v = (int)(Cam.Speed / Cam.Scale);
                timer010 -= (int)(10 * TimeSpan.TicksPerMillisecond);

                if (mouse.X < 1 && Cam.PosX > 0) Cam.PosX -= v;
                if (mouse.Y < 1 && Cam.PosY > 0) Cam.PosY -= v;
                if (mouse.X >= window.Width - 1 && Cam.PosX < World.Width * Cam.Size) Cam.PosX += v;
                if (mouse.Y >= window.Height - 1 && Cam.PosY < World.Height * Cam.Size) Cam.PosY += v;

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
                //GL2D.UniformInt(0, date.Hour);

            }
            timer500 += ticks;
            while (timer500 > 500 * TimeSpan.TicksPerMillisecond)
            {
                timer500 -= (int)(500 * TimeSpan.TicksPerMillisecond);
                Program.MenuOverlay.pictureBoxMinimap.Image = GenerateMiniMap();
                Program.MenuOverlay.pictureBoxMinimap.Refresh();
                Program.MenuOverlay.label5.Text = "" + (int)Resources[0].Value + ",-";
                Program.MenuOverlay.label8.Text = "" + Resources[0].AddValue;
            }
            timer1000 += ticks;
            while (timer1000 > 1000 * TimeSpan.TicksPerMillisecond)
            {
                timer1000 -= (int)(1000 * TimeSpan.TicksPerMillisecond);
                for (int i = 0; i < Resources.Length; i++)
                {
                    if (Resources[i] == null) continue;
                    Resources[i].Update(1f/30f);
                }
                for (int i = 0; i < (World.Width + World.Height) / 1; i++)
                {
                    UpdateField((int)(World.Width * World.Height * rnd.NextDouble()));
                }
                date = date.AddDays(1);
                //UpdateMiniMap();
            }
        }
    }
}