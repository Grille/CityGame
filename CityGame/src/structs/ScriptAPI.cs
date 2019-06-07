using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityGame
{
    public class ScriptAPI
    {
        Random rnd;
        public Game Game;
        public int Pos;
        public ScriptAPI(Game game)
        {
            rnd = new Random();
            Game = game;
        }
        public void ChangeTypTo(GameObject obj)
        {
            Game.Build((byte)obj.ID, Pos);
        }
        public void ChangeTypTo(byte[] obj)
        {
            Game.Build((byte)obj[(int)(rnd.NextDouble()*obj.Length)], Pos);
        }
        public float GetAreaValue(GameArea area)
        {
            return Game.World.Data[area.ID,Pos];
        }
    }
}
