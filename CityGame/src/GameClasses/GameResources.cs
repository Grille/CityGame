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
    public class GameResources 
    {
        private string name;
        private bool canBeNegative;
        private bool storable;
        public int Value;
        public int AddValue;
        private int storeSize;


        public GameResources()
        {
        }
        public void Load(string name, int value, bool canBeNegative, bool storable)
        {
            this.name = name;
            this.canBeNegative = canBeNegative;
            this.storable = storable;
            this.Value = value;
        }
        public void Update()
        {
            Value += AddValue;
        }

    }

}
