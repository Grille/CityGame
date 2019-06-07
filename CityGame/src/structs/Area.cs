using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityGame
{
    public class GameArea
    {
        public int ID { get; private set; }
        public string Name;
        public string Title { get; private set; }
        public bool Smooth;
        public void Load(int id,string name,string title, bool smooth)
        {
            this.ID = id;
            Title = title;
            Name = name;
            Smooth = smooth;
        }
    }
}
