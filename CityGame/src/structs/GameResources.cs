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

namespace CityGame
{
    public class GameResources 
    {
        private string name;
        public string Name
        {
            get { return name; }
        }
        private bool canBeNegative;
        private bool storable;
        public double Value;
        public double InitValue;
        public double AddValue;
        private int storeSize;


        public GameResources()
        {
        }
        public void Load(string name, int value, bool canBeNegative, bool storable)
        {
            this.name = name;
            this.canBeNegative = canBeNegative;
            this.storable = storable;
            this.InitValue = value;
        }
        public void Update()
        {
            Update(1);
        }
        public void Update(float divfactor)
        {
            Value += AddValue * divfactor;
            if (!canBeNegative && Value < 0) Value = 0;
        }

    }

}
