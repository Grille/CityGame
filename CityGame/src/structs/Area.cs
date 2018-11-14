using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityGame
{
    public class GameArea
    {
        public string Name;
        public bool Smooth;
        public void Load(string name, bool smooth)
        {
            Name = name;
            Smooth = smooth;
        }
    }
}
