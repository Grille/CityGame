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
            ByteStream byteStream = new ByteStream();

            byte saveV = 0;
            byteStream.WriteByte(saveV);

            byteStream.WriteString("name");
            byteStream.WriteString("player");
            byteStream.WriteFloat(Cam.PosX);
            byteStream.WriteFloat(Cam.PosY);
            byteStream.WriteFloat(Cam.Scale);


            int i = 0;
            byteStream.WriteInt((int)Resources[i++].Value);
            byteStream.WriteInt((int)Resources[i++].Value);
            byteStream.WriteInt((int)Resources[i++].Value);
            byteStream.WriteInt((int)Resources[i++].Value);

            byteStream.WriteInt(World.Width);
            byteStream.WriteInt(World.Height);
            byteStream.WriteByteArray(World.Ground, CompressMode.Auto);
            byteStream.WriteByteArray(World.Typ, CompressMode.Auto);
            byteStream.WriteByteArray(World.Version, CompressMode.Auto);
            byteStream.WriteByteArray(World.Zone, CompressMode.Auto);

            byteStream.Save(path);
        }
        public void Load(string path)
        {
            ByteStream byteStream = new ByteStream(path);

            byte saveV = byteStream.ReadByte();//v

            byteStream.ReadString();//name
            byteStream.ReadString();//player
            Cam.PosX = byteStream.ReadFloat();
            Cam.PosY = byteStream.ReadFloat();
            Cam.Scale = byteStream.ReadFloat();

            int ir = 0;
            Resources[ir++].Value = byteStream.ReadInt();
            Resources[ir++].Value = byteStream.ReadInt();
            Resources[ir++].Value = byteStream.ReadInt();
            Resources[ir++].Value = byteStream.ReadInt();

            World.BuildWorld(byteStream.ReadInt(), byteStream.ReadInt());


            World.Ground = byteStream.ReadByteArray();
            byte[] newTyp = byteStream.ReadByteArray();
            for (int i = 0; i < World.Width * World.Height; i++) if (newTyp[i] != 0) World.Typ[i] = newTyp[i];
                    //World.Build(newTyp[i], i);
            World.Version = byteStream.ReadByteArray();
            World.Zone = byteStream.ReadByteArray();
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