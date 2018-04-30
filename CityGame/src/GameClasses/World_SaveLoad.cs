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

            byte saveV = 0;
            byteStream.WriteByte(saveV);

            byteStream.WriteString("name");
            byteStream.WriteString("player");
            byteStream.WriteInt(camera.PosX);
            byteStream.WriteInt(camera.PosY);
            byteStream.WriteInt((int)camera.Size);


            int i = 0;
            byteStream.WriteInt((int)resources[i++].Value);
            byteStream.WriteInt((int)resources[i++].Value);
            byteStream.WriteInt((int)resources[i++].Value);
            byteStream.WriteInt((int)resources[i++].Value);

            byteStream.WriteInt(width);
            byteStream.WriteInt(height);
            byteStream.WriteByteArray(Ground,1);
            byteStream.WriteByteArray(Typ,1);
            byteStream.WriteByteArray(Version, 1);

            byteStream.Save(path);
        }
        public void Load(string path)
        {
            loadMode = true;
            ByteStream byteStream = new ByteStream(path);

            byte saveV = byteStream.ReadByte();//v

            byteStream.ReadString();//name
            byteStream.ReadString();//player
            camera.PosX = byteStream.ReadInt();
            camera.PosY = byteStream.ReadInt();
            camera.Size = byteStream.ReadInt();

            int ir = 0;
            resources[ir++].Value = byteStream.ReadInt();
            resources[ir++].Value = byteStream.ReadInt();
            resources[ir++].Value = byteStream.ReadInt();
            resources[ir++].Value = byteStream.ReadInt();

            BuildWorld(byteStream.ReadInt(), byteStream.ReadInt());

            try
            {
                Ground = byteStream.ReadByteArray();
                byte[] newTyp = byteStream.ReadByteArray();
                for (int i = 0; i < width * height; i++) if (newTyp[i] != 0) Build(newTyp[i], i);
                Version = byteStream.ReadByteArray();
            }
            catch
            {
            }

            loadMode = false;
        }
    }
}