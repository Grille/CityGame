using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CityGame
{
    public class Zone
    {
        public string Name;
        public Color Color;
        public byte[] Types;
        public byte[] CanBuildOn;

        public void Load(string name, byte[] color, byte[] types,byte[] canBuildOn)
        {
            this.Name = name;
            this.Color = Color.FromArgb(255, color[0], color[1], color[2]);
            this.Types = types;
            this.CanBuildOn = canBuildOn;
        }
    }
}
