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
        private byte[] compressByte(byte[] input, int mode)
        {
            //[ui8 data]
            if (mode == 0)
            {
                return input;
            }
            //[ui8 lenght,ui8 data]
            else if (mode == 1)
            {
                byte[] saveData = new byte[input.Length * 2];
                int iDst = 0;
                byte lastData = input[0];
                int dataLenght = 0;
                for (int iSrc = 1; iSrc < input.Length; iSrc++)
                {
                    if (input[iSrc] == lastData && dataLenght < 255)
                    {
                        dataLenght++;
                    }
                    else
                    {
                        saveData[iDst] = (byte)dataLenght;
                        saveData[iDst + 1] = lastData;
                        dataLenght = 0;
                        iDst += 2;
                        lastData = input[iSrc];
                    }
                }
                byte[] returnData = new byte[iDst];
                for (int i = 0; i < returnData.Length; i++)
                {
                    returnData[i] = saveData[i];
                }
                return returnData;
            }
            //[ui4 data + ui4 data]
            else if (mode == 2)
            {
                byte[] returnData = new byte[input.Length / 2];
                int iSrc = 0;
                for (int iDst = 0; iDst < returnData.Length; iDst++)
                {
                    returnData[iDst] = (byte)(input[iSrc] << 4 | input[iSrc]);
                    iSrc += 2;
                }
                return returnData;
            }
            //[ui4 lenght + ui4 data]
            else if (mode == 3)
            {
                byte[] saveData = new byte[input.Length];
                int iDst = 0;
                byte lastData = input[0];
                int dataLenght = 0;
                for (int iSrc = 1; iSrc < input.Length; iSrc++)
                {
                    if (input[iSrc] == lastData && dataLenght < 255)
                    {
                        dataLenght++;
                    }
                    else
                    {
                        saveData[iDst] = (byte)(dataLenght << 4 | lastData);
                        saveData[iDst + 1] = lastData;
                        dataLenght = 0;
                        iDst += 1;
                        lastData = input[iSrc];
                    }
                }

                byte[] returnData = new byte[iDst];

                for (int i = 0; i < returnData.Length; i++)
                {
                    returnData[i] = saveData[i];
                }
                return returnData;
            }
            else
            {
                return new byte[0];
            }


        }
        private byte[] combineByte(byte[] input1, byte[] input2)
        {
            byte[] returnData = new byte[input1.Length + input2.Length];
            int index = 0;
            for (int i = 0; i < input1.Length; i++)
            {
                returnData[index++] = input1[i];
            }
            for (int i = 0; i < input2.Length; i++)
            {
                returnData[index++] = input2[i];
            }
            return returnData;
        }
        private void saveInt(byte[] data,int input)
        {

        }
        private int loadInt(byte[] data,int index,out int newIndex)
        {
            newIndex = index+4;
            return  12 << data[index+0] | 8 << data[index+1] | 4 << data[index+2] | data[index+3];
        }

        public void Save(string path)
        {
            ByteStream byteStream = new ByteStream();
            byteStream.Write(width);
            byteStream.Write(height);
            byteStream.Write(Ground,2);
            byteStream.Write(Typ);
            byteStream.Write(Version,2);

            byteStream.Save(path);
        }
        public void Load(string path)
        {
            ByteStream byteStream = new ByteStream(path);
            byteStream.ResetIndex();

            BuildWorld(byteStream.ReadInt(), byteStream.ReadInt());

            Ground = byteStream.ReadByteArray();
            byte[] newTyp = byteStream.ReadByteArray();

            loadMode = true;
            for (int i = 0; i < width * height; i++)
            {
                if (newTyp[i]!=0)Build(newTyp[i], i);
                //autoTile(i);
                //buildEffects(Typ[i], i, true);
            }
            loadMode = false;
            Version = byteStream.ReadByteArray();
        }
    }
}