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

namespace CityGame
{
    public partial class Game
    {
        void playerBuild(BuildOption option,int pos)
        {
            if (option.Typ == 1)
            {
                if (option.Value == 0)
                {
                    if (Objects[World.Typ[pos]].DemolitionTyp.Length == 0) return;
                    byte newTyp = Objects[World.Typ[pos]].DemolitionTyp[0];
                    World.Build(newTyp, pos);
                }
                else
                {
                    byte buildTyp = replaceBuildTyp(World.Typ[pos]);
                    if (World.CanBuild(buildTyp, pos))
                    {
                        World.Build(buildTyp, pos);
                        buildPreviewEnabled = false;
                    }
                }
            }
            else if (option.Typ == 2)
            {
                World.Zone[pos] = (byte)option.Value;
            }
        }
        byte replaceBuildTyp(byte oldTyp)
        {
            byte typ = (byte)SelectetBuildIndex.Value;
            byte[] replace = SelectetBuildIndex.BuildReplace;
            if (replace.Length == 0) return typ;
            for (int i = 0; i < replace.Length; i += 2)
            {
                if (replace[i] == oldTyp) return replace[i+1];
            }
            return typ;
        }
    }
}