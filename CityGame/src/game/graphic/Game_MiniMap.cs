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
using GGL.Graphic;

namespace CityGame
{
    public partial class Game
    {
        Texture MiniMapTex;

        private void renderMinimap(byte[] data)
        {
            int size = World.Width * World.Height;
            int iDst = 0;
            for (int iSrc = 0; iSrc < size; iSrc++)
            {
                int pos = iSrc - World.ReferenceX[iSrc] - World.ReferenceY[iSrc] * World.Width;

                int value = (int)World.Data[2,pos];
                data[iDst++] = (byte)(100- Math.Abs(value));//b
                data[iDst++] = (byte)(100-(value < 0 ? (byte)Math.Abs(value) : (byte)0));//g
                data[iDst++] = (byte)(100-(value > 0 ? (byte)Math.Abs(value) : (byte)0));//r
                /*
                if (objects[World.Typ[pos]].Texture == null)
                {
                    if (objects[World.Typ[pos]].Ground == null)
                    {
                        if (World.Ground[pos] == 52)
                        {
                            data[iDst++] = 151;
                            data[iDst++] = 183;
                            data[iDst++] = 199;
                        }
                        else
                        {
                            data[iDst++] = 17;
                            data[iDst++] = 100;
                            data[iDst++] = 38;
                        }
                    }
                    else
                    {
                        Color color = objects[World.Typ[pos]].Ground[0].BaseColor;
                        data[iDst++] = color.B;
                        data[iDst++] = color.G;
                        data[iDst++] = color.R;
                    }

                }
                else
                {
                    Color color = objects[World.Typ[pos]].Texture[World.TileStruct[pos]][World.Version[pos]].BaseColor;
                    float pz = (float)color.A / 255f;
                    data[iDst++] = (byte)(color.B * pz + 17 * (1 - pz));
                    data[iDst++] = (byte)(color.G * pz + 100 * (1 - pz));
                    data[iDst++] = (byte)(color.R * pz + 38 * (1 - pz));

                }
                */
                data[iDst++] = 255;
            }
        }
        public Bitmap GenerateMiniMap()
        {
            LockBitmap lockBitmap = new LockBitmap(World.Width, World.Height);
            byte[] data = lockBitmap.getData();
            renderMinimap(lockBitmap.getData());
            return lockBitmap.returnBitmap();
        }

        public void UpdateGLMiniMap()
        {
            byte[] data = new byte[World.Width * World.Height * 3];
            renderMinimap(data);
            highViewMap.Update(World.Width, World.Height, data);
        }


    }
}