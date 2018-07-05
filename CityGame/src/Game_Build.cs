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
        void playerBuild(byte typ,int pos)
        {
            byte buildTyp = replaceBuildTyp(SelectetBuildIndex, World.Typ[hoveredWorldPos]);
            if (World.CanBuild(buildTyp, hoveredWorldPos))
            {

                World.Build(buildTyp, hoveredWorldPos);
                buildPreviewEnabled = false;
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