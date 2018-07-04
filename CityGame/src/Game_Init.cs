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
        public int InitOpenGL()
        {
            int code;
            //GL2D.WaitRendererReady();
            if ((code = GL2D.IsRendererReady()) > 0) return code;

            Console.WriteLine(GL.GetString(StringName.Renderer));
            Console.WriteLine(GL.GetString(StringName.Version));

            basicShader = GL2D.CreateShader(File.ReadAllText("../Data/Shaders/basicVS.glsl"), File.ReadAllText("../Data/Shaders/basicFS.glsl"));
            if (basicShader < 0) return 4;
            glowShader = GL2D.CreateShader(File.ReadAllText("../Data/Shaders/glowVS.glsl"), File.ReadAllText("../Data/Shaders/glowFS.glsl"));
            if (glowShader < 0) return 4;

            GL2D.UseShader(basicShader);
            GL2D.CreateBuffer(2265536);
            // Other state


            Console.WriteLine("version:" + GL.GetString(StringName.Version));

            return 0;
        }
        public void LoadData(ProgressBar bar)
        {
            bar.Value = 0;
            bar.Maximum = 256;
            highViewMap = new Texture(4, 4, new byte[] { 255, 0, 0 });
            //effects: 0=not, 1=up, 2=down, 3=break, 4=deacy, 5=destroy 6=entf//
            //AreaPermanent->typ: 0=water, 1=nature, 2=road,3=saltwater
            GGL.IO.Parser parser = new GGL.IO.Parser();
            resources = new GameResources[256];
            objects = new GameObject[256];

            Console.WriteLine("//load: gameResourcesData");
            parser.LoadData("../Data/config/gameResources.gd");
            parser.Parse();
            for (int i = 0; i < resources.Length; i++)
            {
                resources[i] = new GameResources();
                resources[i].Load(
                    parser.GetAttribute<string>(i, "name"),
                    parser.GetAttribute<int>(i, "value"),
                    false, false
                    );
            }

            Console.WriteLine("//load: gameObjectData");
            parser.LoadData("../Data/config/gameObject.gd");
            parser.Parse();
            for (int i = 0; i < objects.Length; i++)
            {
                bar.Value++;
                objects[i] = new GameObject(i);
                if (!parser.IDUsed(i)) continue;
                objects[i].LoadBasic(
                    parser.GetAttribute<string>(i, "name"),
                    parser.GetAttribute<string>(i, "groundPath"),
                    parser.GetAttribute<string>(i, "structPath"),
                    parser.GetAttribute<byte>(i, "buildMode"), 0,
                    parser.GetAttribute<byte>(i, "diversity"),
                    parser.GetAttribute<byte>(i, "size"),
                    parser.GetAttribute<byte>(i, "groundMode"),
                    parser.GetAttribute<byte>(i, "structMode"),
                    parser.GetAttribute<byte[]>(i, "groundNeighbors"),
                    parser.GetAttribute<byte[]>(i, "structNeighbors")
                    );
                objects[i].LoadTypRefs(
                    parser.GetAttribute<byte[]>(i, "upgradeTyp"),
                    parser.GetAttribute<byte[]>(i, "downgradeTyp"),
                    parser.GetAttribute<byte[]>(i, "demolitionTyp"),
                    parser.GetAttribute<byte[]>(i, "decayTyp"),
                    parser.GetAttribute<byte[]>(i, "destroyTyp"),
                    parser.GetAttribute<byte[]>(i, "canBuiltOn"),
                    parser.GetAttribute<byte[]>(i, "RepalceTyp")
                    );
                objects[i].LoadSimData(
                    parser.GetAttribute<int[]>(i, "AreaPermanent"),
                    parser.GetAttribute<int[]>(i, "AreaDependent"),
                    parser.GetAttribute<int[]>(i, "ResourcesEffect"),
                    parser.GetAttribute<int[]>(i, "ResourcesBuild"),
                    parser.GetAttribute<int[]>(i, "ResourcesPermanent"),
                    parser.GetAttribute<int[]>(i, "ResourcesMonthly"),
                    parser.GetAttribute<int[]>(i, "ResourcesDependent")
                    );
            }

            groundTexture = new Texture("../Data/texture/ground/texture.png");
            gui = new Texture("../Data/texture/gui/aktivField.png");
        }
    }
}