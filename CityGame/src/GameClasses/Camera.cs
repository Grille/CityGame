using System;
using System.IO;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Drawing;
//using System.Drawing.Imaging;
using System.Diagnostics;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using GGL;
using GGL.Graphic;

namespace CityGame
{
    public class Camera
    {
        private World world;
        private Control sender;
        public int Speed = 30;
        public int Size = 64;
        public int PosX = 0;
        public int PosY = 0;

        public int DetailX = 0;
        public int DetailY = 0;

        public Camera(World world,Control sender)
        {
            this.world = world;
            this.sender = sender;
        }
        public void Move(int x, int y)
        {
            DetailX += x;
            DetailY += y;
            while (DetailX >= Size) { DetailX -= Size; PosX++; }
            while (DetailY >= Size) { DetailY -= Size; PosY++; }
            while (DetailX < 0) { DetailX += Size; PosX--; }
            while (DetailY < 0) { DetailY += Size; PosY--; }
        }
        public int GetCenter()
        {
            int x = sender.Width / Size - PosX;
            int y = sender.Height / Size - PosY;
            return x+y*world.Width;
        }
        public void SetCenter(int pos)
        {
            PosX = pos % world.Width - sender.Width / Size;
            PosY = pos / world.Height - sender.Height / Size;
        }
    }
}
