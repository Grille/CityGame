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
        private int value;
        public int Value
        {
            set
            {
                this.value += value;
            }
            get
            {
                int result;
                if (storable) result = value;
                else result =  addValue;
                if (!canBeNegative && result < 0) result = 0;
                return result;
            }
        }
        private int addValue;
        public int AddValue
        {
            set
            {
                addValue += value;
            }
            get
            {
                return addValue;
            }
        }
        private int storeSize;


        public GameResources()
        {
        }
        public void Load(string name, int value, bool canBeNegative, bool storable)
        {
            this.name = name;
            this.canBeNegative = canBeNegative;
            this.storable = storable;
            this.value = value;
        }
        public void Update()
        {
            value += AddValue;
        }

    }

}
