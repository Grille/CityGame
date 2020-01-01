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
using GGL.IO;
using GGL.Graphic;

namespace CityGame
{
    public partial class Game
    {
        public void Save(string path)
        {
            using (var binary = new BinaryView(path, true))
            {
                byte saveV = 0;
                binary.WriteByte(saveV);

                binary.WriteString("name");
                binary.WriteString("player");
                binary.WriteSingle(Cam.PosX);
                binary.WriteSingle(Cam.PosY);
                binary.WriteSingle(Cam.Scale);

                int i = 0;
                binary.WriteInt32((int)Resources[i++].Value);
                binary.WriteInt32((int)Resources[i++].Value);
                binary.WriteInt32((int)Resources[i++].Value);
                binary.WriteInt32((int)Resources[i++].Value);

                binary.WriteInt32(World.Width);
                binary.WriteInt32(World.Height);
                binary.WriteArray(World.Ground);
                binary.WriteArray(World.Typ);
                binary.WriteArray(World.Version);
                binary.WriteArray(World.Zone);

                binary.Compress();
            }

        }
        public void Load(string path)
        {
            using (var binary = new BinaryView(path, false))
            {
                binary.Decompress();

                byte saveV = binary.ReadByte();//v

                binary.ReadString();//name
                binary.ReadString();//player
                Cam.PosX = binary.ReadSingle();
                Cam.PosY = binary.ReadSingle();
                Cam.Scale = binary.ReadSingle();

                int ir = 0;
                Resources[ir++].Value = binary.ReadInt32();
                Resources[ir++].Value = binary.ReadInt32();
                Resources[ir++].Value = binary.ReadInt32();
                Resources[ir++].Value = binary.ReadInt32();

                int width = binary.ReadInt32(), height = binary.ReadInt32();
                World = new World(width, height);

                World.Ground = binary.ReadArray<byte>();
                byte[] newTyp = binary.ReadArray<byte>();
                for (int i = 0; i < World.Width * World.Height; i++) if (newTyp[i] != 0) World.Typ[i] = newTyp[i];
                //World.Build(newTyp[i], i);
                World.Version = binary.ReadArray<byte>();
                World.Zone = binary.ReadArray<byte>();
            }
        }
        public void GenerateMap(Image map)
        {
            Random rnd = new Random(1000);
            World = new World(map.Width, map.Height);
            LockBitmap data = new LockBitmap((Bitmap)map, true);
            byte[] rgbData = data.getData();
            Console.WriteLine("Bitmap: " + (int)(map.Width * map.Height) + " Map: " + rgbData.Length / 4);
            //loadMode = true;
            for (int i = 0; i < rgbData.Length / 4; i++)
            {
                if (rnd.NextDouble() > 0.5)
                {
                    World.Ground[i] = 1;
                    if (rnd.NextDouble() < 0.5)
                    {
                        World.Ground[i] = 2;
                    }
                }

                if (rgbData[i * 4 + 0] == 255)
                {
                    Build(1, i);
                    World.Ground[i] = 49;
                }
                if (rgbData[i * 4 + 0] == 112)
                {
                    Build(10, i);
                }
                if (rgbData[i * 4 + 0] == 151)
                {
                    World.Ground[i] = 52;
                }
                if (rgbData[i * 4 + 0] == 77)
                {
                    World.Ground[i] = 49;
                }
                else if (rgbData[i * 4 + 0] == 254)
                {
                    Build(2, i);
                    World.Ground[i] = 52;
                }
                else if (rgbData[i * 4 + 1] == 80)
                {
                    Build(3, i);
                    World.Ground[i] = 3;
                }
                else if (rgbData[i * 4 + 1] == 100)
                {
                    Build(4, i);
                    World.Ground[i] = 3;
                }
                else if (rgbData[i * 4 + 1] == 160)
                {
                    Build(5, i);
                    World.Ground[i] = 3;
                }

                if (rnd.NextDouble() < 0.001) World.Ground[i] = 5;
                //if (rgbData[i * 4 + 1] == 128) Build(1, i);
            }
            //autoGround(52);
            //autoGround(49);


            //loadMode = false;
        }
    }
}