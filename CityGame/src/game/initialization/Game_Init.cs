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
using CsGL2D;
using System.Threading;

namespace CityGame
{
    public partial class Game
    {


        Shader basicShader, glowShader;
        DrawBuffer buffer,groundBuffer;

        public int InitOpenGL()
        {
            Console.WriteLine(GL.GetString(StringName.Renderer));
            Console.WriteLine(GL.GetString(StringName.Version));

            basicShader = new Shader();
            //if (basicShader < 0) return 4;
            glowShader = new Shader(File.ReadAllText("../Data/Shaders/glowFS.glsl"));
            //if (glowShader < 0) return 4;

            buffer = new DrawBuffer(2265536);
            groundBuffer = new DrawBuffer(512 * 512);
            // Other state


            Console.WriteLine("version:" + GL.GetString(StringName.Version));
            
            return 0;
        }
        public void LoadData(ProgressBar bar)
        {
            highViewMap = new Texture(4, 4/*, new byte[] { 255, 0, 0 }*/);
            //effects: 0=not, 1=up, 2=down, 3=break, 4=deacy, 5=destroy 6=entf//
            //AreaPermanent->typ: 0=water, 1=nature, 2=road,3=saltwater
            GGL.IO.Parser parser = new GGL.IO.Parser();
            string[] names;


            Console.WriteLine("//load: gameZonesData");
            parser.ParseFile("../Data/config/Zones.gd");
            names = parser.ObjectNames;
            Zones = new Zone[names.Length];
            for (int i = 0; i < Zones.Length; i++)
            {
                string name = names[i];
                Zones[i] = new Zone();
                Zones[i].Load(i, name,
                    parser.GetAttribute<string>(name, "name"),
                    parser.GetAttribute<byte[]>(name, "color"),
                    parser.GetAttribute<byte[]>(name, "supportTyp"),
                    parser.GetAttribute<byte[]>(name, "canBuildOnTyp")
                    );
            }
            parser.Clear();

            Console.WriteLine("//load: gameResourcesData");
            parser.ParseFile("../Data/config/gameResources.gd");
            names = parser.ObjectNames;
            Resources = new GameResources[names.Length];
            for (int i = 0; i < Resources.Length; i++)
            {
                string name = names[i];
                Resources[i] = new GameResources();
                Resources[i].Load(i, name,
                    parser.GetAttribute<string>(name, "name"),
                    parser.GetAttribute<int>(name, "initValue"),
                    parser.GetAttribute<bool>(name, "physical"), 
                    parser.GetAttribute<bool>(name, "storable")
                    );
            }
            parser.Clear();

            Console.WriteLine("//load: gameAreaData");
            parser.ParseFile("../Data/config/gameArea.gd");
            names = parser.ObjectNames;
            Areas = new GameArea[names.Length];
            for (int i = 0; i < Areas.Length; i++)
            {
                string name = names[i];
                Areas[i] = new GameArea();
                Areas[i].Load(i,name,
                    parser.GetAttribute<string>(name, "name"),
                    parser.GetAttribute<bool>(name, "smooth")
                    );
            }
            parser.Clear();

            for (int i = 0;i< Resources.Length; i++)
                parser.AddEnum("res", Resources[i].Name.ToLower(), i);
            for (int i = 0; i < Areas.Length; i++)
                parser.AddEnum("area", Areas[i].Name.ToLower(), i);
            parser.AddEnum("effect", new string[] { "not", "up", "down","break" ,"deacy" ,"destroy" ,"entf"});
            parser.AddEnum("gmode", new string[] { "not", "all", "foCu", "foEn", "cuEn", "fo", "cu" ,"en","st"});
            parser.AddEnum("bmode", new string[] { "single", "brush", "rnline", "eqline", "cnline", "rnarea", "eqarea" });
            parser.AddEnum("i","min",int.MinValue); parser.AddEnum("i", "max", int.MaxValue);

            parser.ParseFile("../Data/config/gameObject.gd");
            names = parser.ObjectNames;
            Objects = new GameObject[names.Length];
            bar.Value = 0;
            bar.Maximum = Objects.Length;
            for (int i = 0; i < Objects.Length; i++)
            {
                bar.Value++;
                string name = names[i];
                Objects[i] = new GameObject(i);
                Objects[i].LoadBasic(i, name,
                    parser.GetAttribute<string>(name, "name"),
                    parser.GetAttribute<string>(name, "groundPath"),
                    parser.GetAttribute<string>(name, "structPath"),
                    parser.GetAttribute<byte>(name, "size"),
                    parser.GetAttribute<byte>(name, "groundMode"),
                    parser.GetAttribute<byte>(name, "structMode"),
                    parser.GetAttribute<byte[]>(name, "groundNeighbors"),
                    parser.GetAttribute<byte[]>(name, "structNeighbors")
                    );
                Objects[i].LoadTypRefs(
                    parser.GetAttribute<byte[]>(name, "upgradeTyp"),
                    parser.GetAttribute<byte[]>(name, "downgradeTyp"),
                    parser.GetAttribute<byte[]>(name, "demolitionTyp"),
                    parser.GetAttribute<byte[]>(name, "decayTyp"),
                    parser.GetAttribute<byte[]>(name, "destroyTyp"),
                    parser.GetAttribute<byte[]>(name, "canBuiltOn")
                    );
                Objects[i].LoadSimData(
                    parser.GetAttribute<int[]>(name, "AreaPermanent"),
                    parser.GetAttribute<int[]>(name, "AreaDependent"),
                    parser.GetAttribute<int[]>(name, "ResourcesEffect"),
                    parser.GetAttribute<int[]>(name, "ResourcesBuild"),
                    parser.GetAttribute<int[]>(name, "ResourcesPermanent"),
                    parser.GetAttribute<int[]>(name, "ResourcesMonthly"),
                    parser.GetAttribute<int[]>(name, "ResourcesDependent")
                    );
                Objects[i].LoadScriptSource(
                    parser.GetAttribute<string>(name, "onupdate")
                    );
            }
            parser.Clear();
            groundTexture = new Texture("../Data/texture/ground/texture.png");
            zoneTexture = new Texture("../Data/texture/effect/zoon.png");
            gui = new Texture("../Data/texture/gui/aktivField.png");

            compile();

            TextureAtlas._DEBUG();
        }
    }
}