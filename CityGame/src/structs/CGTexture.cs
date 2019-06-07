using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GGL.Graphic;
using System.Drawing;
using System.Drawing.Imaging;
using CsGL2D;
using System.Threading;
using System.Runtime.InteropServices;

namespace CityGame
{
    public class CGTexture : Texture
    {
        public Color AverageColor { private set; get; }
        public CGTexture(string path) {
            addToAtlas(new Bitmap(path));
        }

        new void addToAtlas(Image img)
        {
            Bitmap bitmap = (Bitmap)img;
            BitmapData ptr = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] data = new byte[bitmap.Width * bitmap.Height * 4];
            Marshal.Copy(ptr.Scan0, data, 0, data.Length);
            bitmap.UnlockBits(ptr);

            int size = bitmap.Width * bitmap.Height;
            int colorCount = 0;
            int r = 0, g = 0, b = 0, a = 0;
            for (int i = 0; i< size; i++)
            {
                a += data[i * 4 + 3];
                if (a > 128)
                {
                    colorCount++;
                    r += data[i * 4 + 2];
                    g += data[i * 4 + 1];
                    b += data[i * 4 + 0];
                }


            }
            a /= size;
            r /= colorCount;
            g /= colorCount;
            b /= colorCount;
            AverageColor = Color.FromArgb(a,r,g,b);
            base.addToAtlas(img);
        }
    }
}
