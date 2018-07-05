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
        public readonly int ID;
        public string Name;
        public string StructPath;
        public string GroundPath;
        
        public int BuildMode;
        private Texture[][] texture;
        public Texture[][] Texture { get { return texture; } }
        private byte[] diversity;
        //public byte[] Diversity { get { return diversity; } }
        private byte size;
        public byte Size { get { return size; } }
        private Texture[] ground;
        public Texture[] Ground { get { return ground; } }
        private int groundMode;
        public int GroundMode { get { return groundMode; } }

        private int structMode;  //0=nothing, 1=self, 2=useArray;
        public int StructMode { get { return structMode; } }
        public byte[] StructNeighbors;
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

        public GameObject(int id)
        {
            ID = id;
        }
        private Texture[] loadTiles(string path,int tile)
        {
            int lenght = 0;
            while (File.Exists(path + "_" + lenght + "_" + tile + ".png")|| File.Exists(path + "_" + lenght + ".png")) lenght++;
            Texture[] result = new Texture[lenght];
            for (int i = 0; i < result.Length; i++)
            {
                if (!File.Exists(path + "_" + i + "_" + tile + ".png")) result[i] = new Texture(path + "_" + i + ".png");
                else result[i] = new Texture(path + "_" + i + "_" + tile + ".png");
            }
            return result;
        }
        public void LoadBasic(string name, string groundPath, string path, int buildMode, byte[] replaceTyp, int size,int groundMode, int graphicMode, byte[] groundNeighbors,byte[] structNeighbors)
        {

            this.Name = name;
            this.StructPath = path;
            this.GroundPath = groundPath;
            this.diversity = new byte[] { 0 };
            this.size = (byte)size;
            this.BuildMode = buildMode;
            this.groundMode = groundMode;
            this.structMode = graphicMode;
            this.GroundNeighbors = groundNeighbors;
            this.StructNeighbors = structNeighbors;
            this.ReplaceTyp = replaceTyp;

            if (groundPath != null)
            {
                ground = new Texture[1];
                if (File.Exists(groundPath + "_g.png")) ground[0] = new Texture(groundPath+"_g.png");
                else ground[0] = new Texture(groundPath + ".png"); 
            }
            if (path != null)
            {
                int tiles = 0;
                if (graphicMode == 0) tiles = 1;
                else if (graphicMode == 1) tiles = 16;
                else tiles = 2;
                texture = new Texture[tiles][];

                    if (graphicMode == 0)
                    {
                        texture[0] = loadTiles(path,0);
                    }
                    else if (graphicMode == 1)
                    {
                        for (int iTile = 0; iTile < 16; iTile++)
                            texture[iTile] = loadTiles(path, iTile);
                    }
                    else if (graphicMode == 2)
                    {
                        for (int i2 = 0; i2 < 2; i2++)
                            texture[i2] = loadTiles(path, i2);
                    }
                
            }                     // GGL.LockBitmap lockBitmap1 = new LockBitmap()
        }
        public void LoadTypRefs(byte[] upgradeTyp, byte[] downgradeTyp, byte[] demolitionTyp, byte[] decayTyp, byte[] destroyTyp, byte[] canBuiltOnTyp) 
        {
            this.UpgradeTyp = upgradeTyp;
            this.DowngradeTyp = downgradeTyp;
            this.DemolitionTyp = demolitionTyp;
            this.DecayTyp = decayTyp;
            this.DestroyTyp = destroyTyp;
            this.CanBuiltOnTyp = canBuiltOnTyp;
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
