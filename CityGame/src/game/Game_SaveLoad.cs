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
            byteStream.WriteByteArray(World.Ground, 0);
            byteStream.WriteByteArray(World.Typ, 0);
            byteStream.WriteByteArray(World.Version, 0);
            byteStream.WriteByteArray(World.Zone, 0);

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
            for (int i = 0; i < World.Width * World.Height; i++) if (newTyp[i] != 0) World.Build(newTyp[i], i);
            World.Version = byteStream.ReadByteArray();
            World.Zone = byteStream.ReadByteArray();
        }
    }
}