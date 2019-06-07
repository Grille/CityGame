using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CityGame
{
    public class Zone
    {
        public int ID;
        public string Name;
        public string Title;
        public Color Color;
        public byte[] Types;
        public byte[] CanBuildOn;

        public void Load(int id,string name,string title, byte[] color, byte[] types,byte[] canBuildOn)
        {
            this.ID = id;
            this.Name = name;
            this.Title = title;
            this.Color = Color.FromArgb(255, color[0], color[1], color[2]);
            this.Types = types;
            this.CanBuildOn = canBuildOn;
        }
    }
}
