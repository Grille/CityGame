using System;
using System.IO;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Drawing;
//using System.Drawing.Imaging;
using System.Diagnostics;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using GGL;
using GGL.Graphic;

namespace CityGame
{

    public class GameObject
    {
 
        private string name;
        public int BuildMode;
        private Texture[,] texture;
        public Texture[,] Texture { get { return texture; } }
        private int diversity;
        public int Diversity { get { return diversity; } }
        private byte size;
        public byte Size { get { return size; } }
        private Texture[] ground;
        public Texture[] Ground { get { return ground; } }
        private int groundMode;
        public int GroundMode { get { return groundMode; } }

        private int graphicMode;  //0=nothing, 1=self, 2=useArray;
        public int GraphicMode { get { return graphicMode; } }
        public int[] graphicNeighbors;

        public int[] UpgradeTyp;
        public int[] DowngradeTyp;
        public int[] DemolitionTyp;
        public int[] DecayTyp;
        public int[] DestroyTyp;
        public int[] CanBuiltOnTyp;          //[typ]

        //effects: 0=up, 1=down, 2=demolition, 3=deacy, 4=destroy 5=entf//
        //importance: 0=canNotWork, 1=canWork//
        public int[,] AreaPermanent;        //[[typ,size,value]]
        public int[,] AreaDependent;      //[[typ,minValue,maxValue,effects]]
        public int[,] ResourcesBuild;     //[[typ,value,importance]]
        public int[,] ResourcesPermanent; //[[typ,value,importance]]
        public int[,] ResourcesMonthly;   //[[typ,value,importance]]
        public int[,] ResourcesDependent; //[[typ,minValue,maxValue,effects]]

        public void LoadBasic(string name, string path, string groundPath, int buildMode,int slopeMode, int diversity, int size,int groundMode, int graphicMode, int[] graphicNeighbors)
        {

            this.name = name;
            this.diversity = diversity;
            this.size = (byte)size;

            this.BuildMode = buildMode;
            this.groundMode = groundMode;
            this.graphicMode = graphicMode;
            this.graphicNeighbors = graphicNeighbors;

            if (groundPath != "-")
            {
                ground = new Texture[1];
                if (File.Exists(groundPath + "_g.png")) ground[0] = new Texture(groundPath+"_g.png");
                else ground[0] = new Texture(groundPath + ".png"); 
            }
            if (path != "-")
            {
                texture = new Texture[diversity, 16];
                for (int i = 0; i < diversity; i++)
                {
                    if (graphicMode == 0)
                    {
                        if (!File.Exists(path + "_" + i + "_0.png")) texture[i, 0] = new Texture(path + "_" + i + ".png"); 
                        else texture[i, 0] = new Texture(path + "_" + i + "_0.png");
                    }
                    else
                    {
                        for (int i2 = 0; i2 < 16; i2++)
                        {
                            if (!File.Exists(path + "_" + i + "_" + i2 + ".png")) texture[i, i2] = texture[i, 0];
                            else texture[i, i2] = new Texture(path + "_" + i + "_" + i2 + ".png");
                        }
                    }
                }
            }
        }
        public void LoadTypRefs(int[] upgradeTyp, int[] downgradeTyp, int[] demolitionTyp, int[] decayTyp, int[] destroyTyp, int[] CanBuiltOnTyp) 
        {
            this.UpgradeTyp = upgradeTyp;
            this.DowngradeTyp = downgradeTyp;
            this.DemolitionTyp = demolitionTyp;
            this.DecayTyp = decayTyp;
            this.DestroyTyp = destroyTyp;
            this.CanBuiltOnTyp = CanBuiltOnTyp;
        }
        public void LoadSimData(int[,] AreaPermanent,int[,] AreaDependent,int[,] ResourcesBuild,int[,] ResourcesPermanent,int[,] ResourcesMonthly,int[,] ResourcesDependent)
        {
            this.AreaPermanent = AreaPermanent;
            this.AreaDependent = AreaDependent;
            this.ResourcesBuild = ResourcesBuild;
            this.ResourcesPermanent = ResourcesPermanent;
            this.ResourcesMonthly = ResourcesMonthly;
            this.ResourcesDependent = ResourcesDependent;
        }
    }

}
