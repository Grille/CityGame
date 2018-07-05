﻿using System;
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
        public int Size = 64;
        public int Speed = 30;
        public float Scale = 1;
        public float PosX = 0;
        public float PosY = 0;

        public Camera()
        {
        }
    }
}
