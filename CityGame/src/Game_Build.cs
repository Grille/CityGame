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
        void playerBuild(int typ,int pos)
        {
            switch (typ)
            {
                case 0:
                    Console.WriteLine("fgh");
                    if (objects[World.Typ[pos]].DemolitionTyp.Length == 0) return;
                    byte newTyp = objects[World.Typ[pos]].DemolitionTyp[0];
                    World.Build(newTyp, pos);
                    break;
                    /*
                case 61:
                    World.Zone[pos] = 1;
                    break;
                case 62:
                    World.Zone[pos] = 2;
                    break;
                case 63:
                    World.Zone[pos] = 3;
                    break;
                case 71:
                    World.Zone[pos] = 4;
                    break;
                case 72:
                    World.Zone[pos] = 5;
                    break;
                case 73:
                    World.Zone[pos] = 6;
                    break;
                case 81:
                    World.Zone[pos] = 7;
                    break;
                case 82:
                    World.Zone[pos] = 8;
                    break;
                case 83:
                    World.Zone[pos] = 9;
                    break;
                    */
                default:
                    byte buildTyp = replaceBuildTyp((byte)SelectetBuildIndex, World.Typ[pos]);
                    if (World.CanBuild(buildTyp, pos))
                    {
                        World.Build(buildTyp, pos);
                        buildPreviewEnabled = false;
                    }
                    break;
            }
        }
        byte replaceBuildTyp(byte typ, byte oldTyp)
        {
            if (objects[typ].ReplaceTyp.Length == 0) return typ;
            for (int i = 0; i < objects[typ].ReplaceTyp.Length; i += 2)
            {
                if (objects[typ].ReplaceTyp[i] == oldTyp) return objects[typ].ReplaceTyp[i+1];
            }
            return typ;
        }
    }
}