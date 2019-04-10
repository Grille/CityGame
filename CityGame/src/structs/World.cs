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
    public partial class World
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public byte[] TypCount;

        public byte[] Ground;
        public byte[] Zone;
        public byte[] Typ;
        public byte[] Version;
        public byte[] TileGround;
        public byte[] TileStruct;
        public byte[] ReferenceX;
        public byte[] ReferenceY;
        public byte[] VertexHeight;
        public byte[] vertexTexture;
        public float[,] Data;

        public World(int width, int height)
        {
            BuildWorld(width, height);

        }
        public void BuildWorld(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            Ground = new byte[width * height];
            Zone = new byte[width * height];
            Typ = new byte[width * height];
            TileGround = new byte[width * height];
            TileStruct = new byte[width * height];
            ReferenceX = new byte[width * height];
            ReferenceY = new byte[width * height];
            Version = new byte[width * height];
            VertexHeight = new byte[(width + 1) * (height + 1)];
            vertexTexture = new byte[(width + 1) * (height + 1)];
            Data = new float[10, width * height];
            TypCount = new byte[256];
        }
    }
}
