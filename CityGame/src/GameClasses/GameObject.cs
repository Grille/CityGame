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

        public string Name;
        public string Path;
        public string GroundPath;
        
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
        public byte[] GraphicNeighbors;
        public byte[] GroundNeighbors;

        public byte[] UpgradeTyp;
        public byte[] DowngradeTyp;
        public byte[] DemolitionTyp;
        public byte[] DecayTyp;
        public byte[] DestroyTyp;
        public byte[] CanBuiltOnTyp;          //[typ]
        public byte[] ReplaceTyp;          //[typ]

        //effects: 0=up, 1=down, 2=demolition, 3=deacy, 4=destroy 5=entf//
        //importance: 0=canNotWork, 1=canWork//
        public int[] AreaPermanent;      //[[typ,size,value]]
        public int[] AreaDependent;      //[[typ,minValue,maxValue,effects]]
        public int[] ResourcesEffect;    //[[firedEfffect,typ,value]]
        public int[] ResourcesBuild;     //[[typ,value]]
        public int[] ResourcesPermanent; //[[typ,value]]
        public int[] ResourcesMonthly;   //[[typ,value]]
        public int[] ResourcesDependent; //[[typ,minValue,maxValue,effects]]

        public void LoadBasic(string name, string path, string groundPath, int buildMode,int slopeMode, int diversity, int size,int groundMode, int graphicMode, byte[] graphicNeighbors)
        {

            this.Name = name;
            this.Path = path;
            this.GroundPath = groundPath;
            this.diversity = diversity;
            this.size = (byte)size;
            this.BuildMode = buildMode;
            this.groundMode = groundMode;
            this.graphicMode = graphicMode;
            this.GraphicNeighbors = graphicNeighbors;

            if (groundPath != null)
            {
                ground = new Texture[1];
                if (File.Exists(groundPath + "_g.png")) ground[0] = new Texture(groundPath+"_g.png");
                else ground[0] = new Texture(groundPath + ".png"); 
            }
            if (path != null)
            {
                texture = new Texture[diversity, 16];
                for (int i = 0; i < diversity; i++)
                {
                    if (graphicMode == 0)
                    {
                        if (!File.Exists(path + "_" + i + "_0.png")) texture[i, 0] = new Texture(path + "_" + i + ".png"); 
                        else texture[i, 0] = new Texture(path + "_" + i + "_0.png");
                    }
                    else if (graphicMode == 1 || graphicMode == 2)
                    {
                        for (int i2 = 0; i2 < 16; i2++)
                        {
                            if (!File.Exists(path + "_" + i + "_" + i2 + ".png")) texture[i, i2] = texture[i, 0];
                            else texture[i, i2] = new Texture(path + "_" + i + "_" + i2 + ".png");
                        }
                    }
                    else if (graphicMode == 3)
                    {
                        for (int i2 = 0; i2 < 2; i2++)
                        {
                            if (!File.Exists(path + "_" + i + "_" + i2 + ".png")) texture[i, i2] = texture[i, 0];
                            else texture[i, i2] = new Texture(path + "_" + i + "_" + i2 + ".png");
                        }
                    }
                }
            }                     // GGL.LockBitmap lockBitmap1 = new LockBitmap()
        }
        public void LoadTypRefs(byte[] upgradeTyp, byte[] downgradeTyp, byte[] demolitionTyp, byte[] decayTyp, byte[] destroyTyp, byte[] canBuiltOnTyp, byte[] replaceTyp) 
        {
            this.UpgradeTyp = upgradeTyp;
            this.DowngradeTyp = downgradeTyp;
            this.DemolitionTyp = demolitionTyp;
            this.DecayTyp = decayTyp;
            this.DestroyTyp = destroyTyp;
            this.CanBuiltOnTyp = canBuiltOnTyp;
            this.ReplaceTyp = replaceTyp;
        }
        public void LoadSimData(int[] AreaPermanent,int[] AreaDependent,int[] ResourcesEffect,int[] ResourcesBuild,int[] ResourcesPermanent,int[] ResourcesMonthly,int[] ResourcesDependent)
        {
            this.AreaPermanent = AreaPermanent;
            this.AreaDependent = AreaDependent;
            this.ResourcesEffect = ResourcesEffect;
            this.ResourcesBuild = ResourcesBuild;
            this.ResourcesPermanent = ResourcesPermanent;
            this.ResourcesMonthly = ResourcesMonthly;
            this.ResourcesDependent = ResourcesDependent;
        }
    }

}
