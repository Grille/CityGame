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
using GGL.IO;
using GGL.Graphic;

namespace CityGame
{
    public partial class World
    {
        public void Save(string path)
        {
            ByteStream byteStream = new ByteStream();
            byteStream.WriteString("name");
            byteStream.WriteString("player");
            int i = 0;
            byteStream.WriteInt((int)resources[i++].Value);
            byteStream.WriteInt((int)resources[i++].Value);
            byteStream.WriteInt((int)resources[i++].Value);
            byteStream.WriteInt((int)resources[i++].Value);

            byteStream.WriteInt(width);
            byteStream.WriteInt(height);
            byteStream.WriteByteArray(Ground);
            byteStream.WriteByteArray(Typ);
            byteStream.WriteByteArray(Version, 2);

            byteStream.Save(path);
        }
        public void Load(string path)
        {
            loadMode = true;

            ByteStream byteStream = new ByteStream(path);
            byteStream.ResetIndex();

            byteStream.ReadString();//name
            byteStream.ReadString();//player

            int ir = 0;
            resources[ir++].Value = byteStream.ReadInt();
            resources[ir++].Value = byteStream.ReadInt();
            resources[ir++].Value = byteStream.ReadInt();
            resources[ir++].Value = byteStream.ReadInt();

            BuildWorld(byteStream.ReadInt(), byteStream.ReadInt());

            Ground = byteStream.ReadByteArray();
            byte[] newTyp = byteStream.ReadByteArray();
            for (int i = 0; i < width * height; i++) if (newTyp[i]!=0)Build(newTyp[i], i);
            Version = byteStream.ReadByteArray();

            loadMode = false;
        }
    }
}