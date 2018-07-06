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
    public partial class World
    {
        private GameObject[] gameObjects;
        private GameResources[] resources;
        private Camera camera;
        private Random rnd;
        private int width;
        public int Width { get { return width; } }
        private int height;
        public int Height { get { return height; } }
        public byte[] Ground;
        public byte[] Zone;
        public byte[] Typ;
        public byte[] Version;
        public byte[] TileGround;
        public byte[] TileStruct;
        public byte[] ReferenceX;
        public byte[] ReferenceY;
        public byte[] VertexHeight;
        public byte[] vertexTexture;
        public int[,] Data;

        private int[] objectCounter;
        public int[] ObjectCounter { get { return objectCounter; } }

        private bool loadMode = false;

        public World(GameObject[] gameObjects, GameResources[] resources,Camera camera)
        {
            this.gameObjects = gameObjects;
            this.resources = resources;
            this.camera = camera;

        }
        public void BuildWorld(int width, int height)
        {

            this.width = width;
            this.height = height;
            Ground = new byte[width * height];
            Typ = new byte[width * height];
            TileGround = new byte[width * height];
            TileStruct = new byte[width * height];
            ReferenceX = new byte[width * height];
            ReferenceY = new byte[width * height];
            Version = new byte[width * height];
            VertexHeight = new byte[(width + 1) * (height + 1)];
            vertexTexture = new byte[(width + 1) * (height + 1)];
            Data = new int[10, width * height];
            objectCounter = new int[256];
            rnd = new Random(1000);

            for (int i = 0; i < VertexHeight.Length;i++)
            {
                //VertexHeight[i] = (byte)(rnd.NextDouble()*1);
            }
            //rndMap();
        }
        private void rndMap()
        {
            rnd = new Random(1000);

            for (int i = 0; i < width * height; i++)
            {
                //vertexHeight[i] = (byte)(rnd.NextDouble() * 1);
                if (rnd.NextDouble() > 0.5)
                {
                    Ground[i] = 1;
                    if (rnd.NextDouble() < 0.5)
                    {
                        Ground[i] = 2;
                    }
                }
                if (rnd.NextDouble() < 0.01)
                {
                    if (rnd.NextDouble() < 0.55) Build(3, i);
                    else Build(4, i);
                    Ground[i] = 4;
                }
            }
            for (int ix = 1; ix < width - 1; ix++)
            {
                for (int iy = 1; iy < height - 1; iy++)
                {
                    if (rnd.NextDouble() < 0.6)
                    {
                        int offset = ix + iy * width;
                        if (Typ[offset] == 0 && (Typ[offset + 1] == 4 || Typ[offset - 1] == 4 || Typ[offset + width] == 4 || Typ[offset - width] == 4))
                        {
                            Build(4, offset);
                            Ground[offset] = 4;
                        }
                        else if (Typ[offset] == 0 && (Typ[offset + 1] == 3 || Typ[offset - 1] == 3 || Typ[offset + width] == 3 || Typ[offset - width] == 3))
                        {
                            Build(3, offset);
                            Ground[offset] = 4;
                        }
                    }
                }
            }
            for (int ix = 1; ix < width - 1; ix++)
            {
                for (int iy = 1; iy < height - 1; iy++)
                {
                    int offset = ix + iy * width;
                    if (Ground[offset] != 4 && (Ground[offset + 1] == 4 || Ground[offset - 1] == 4 || Ground[offset + width] == 4 || Ground[offset - width] == 4))
                    {
                        if (Typ[offset] == 0 && (Typ[offset + 1] == 4 || Typ[offset - 1] == 4 || Typ[offset + width] == 4 || Typ[offset - width] == 4))
                        {
                            Build(4, offset);
                            Ground[offset] = 3;
                        }
                        else if (Typ[offset] == 0 && (Typ[offset + 1] == 3 || Typ[offset - 1] == 3 || Typ[offset + width] == 3 || Typ[offset - width] == 3))
                        {
                            Build(3, offset);
                            Ground[offset] = 3;
                        }
                    }
                }
            }
            for (int ix = 1; ix < width - 1; ix++)
            {
                for (int iy = 1; iy < height - 1; iy++)
                {
                    int offset = ix + iy * width;
                    //if (rnd.NextDouble() < 0.05) Build(3, offset);
                    if (rnd.NextDouble() < 0.001) Ground[offset] = 5;
                    if (rnd.NextDouble() < 0.00002)
                    {
                        //Build(1, offset);

                        float posX = ix;
                        float posY = iy;
                        float addX = 0f;
                        float addY = 0;
                        for (int i = 0; i < 2000; i++)
                        {
                            addX += (float)(rnd.NextDouble() * 0.5 - 0.25f);
                            addY += (float)(rnd.NextDouble() * 0.5 - 0.25f);
                            if (Math.Abs(addX) > 1) addX *= 0.75f;
                            if (Math.Abs(addY) > 1) addY *= 0.75f;
                            posX += addX;
                            posY += addY;
                            if ((posX > 1 && posY > 1 && posX < width - 2 && posY < height - 2))
                            {
                                Build(1, (int)posX, (int)posY);

                                Build(1, (int)posX + 1, (int)posY);
                                Build(1, (int)posX - 1, (int)posY);
                                Build(1, (int)posX, (int)posY + 1);
                                Build(1, (int)posX, (int)posY - 1);
                            }
                            else break;
                        }

                    }

                }
            }
        }

        public bool CanBuild(byte typ, int x, int y)
        {
            return CanBuild(typ, x + y * width);
        }
        public bool CanBuild(byte typ, int pos)
        {

            int x = pos % width;
            int y = (pos - x) / width;

            int size = gameObjects[typ].Size;
            if (x < 0 || y < 0 || x > width - size || y > height - size) return false;//is on map
            if (!loadMode&&!TestResourcesBuild(typ)) return false;
            if (TestResourcesDependet(typ) > 1) return false;
            if (TestAreaDependet(typ, pos) > 1) return false;
            if (gameObjects[typ].CanBuiltOnTyp == null || gameObjects[typ].CanBuiltOnTyp.Length == 0) return true;//can build on all
            bool returnValue = true;



                
                for (int ix = 0; ix < size; ix++)
                {
                    for (int iy = 0; iy < size; iy++)
                    {
                        int curPos = pos + ix + iy * width;
                        curPos = curPos - ReferenceX[curPos] - ReferenceY[curPos] * width;
                        bool matching = false;
                        for (int i = 0; gameObjects[typ].CanBuiltOnTyp != null && i < gameObjects[typ].CanBuiltOnTyp.Length; i++)
                        {
                            matching = matching || Typ[curPos] == gameObjects[typ].CanBuiltOnTyp[i];
                        }
                        returnValue = returnValue && matching;
                    }
                }
                

            return returnValue;
        }

        private void applyBuildResourceCosts(byte typ)
        {
            //resources
                for (int i = 0; gameObjects[typ].ResourcesBuild != null && i < gameObjects[typ].ResourcesBuild.Length/2; i++)
                {
                    int dataTyp = gameObjects[typ].ResourcesBuild[i*2+ 0];
                    int dataValue = gameObjects[typ].ResourcesBuild[i*2+1];
                    resources[dataTyp].Value += dataValue;
                }
        }
        private void applyBuildAreaEffects(byte typ,int pos,bool add)
        {
            int x = pos % width;
            int y = (pos - x) / width;

            if (add) objectCounter[typ]++;
            else objectCounter[typ]--;

            //resources
            for (int i = 0; gameObjects[typ].ResourcesPermanent != null && i < gameObjects[typ].ResourcesPermanent.Length/2; i++)
            {
                int dataTyp = gameObjects[typ].ResourcesPermanent[i*2+ 0];
                int dataValue = gameObjects[typ].ResourcesPermanent[i*2+ 1];
                if (!add) dataValue = -dataValue;
                resources[dataTyp].Value += dataValue;
            }
            for (int i = 0; gameObjects[typ].ResourcesMonthly != null && i < gameObjects[typ].ResourcesMonthly.Length/2; i++)
            {
                int dataTyp = gameObjects[typ].ResourcesMonthly[i*2+ 0];
                int dataValue = gameObjects[typ].ResourcesMonthly[i*2+ 1];
                if (!add) dataValue = -dataValue;
                resources[dataTyp].AddValue += dataValue;
            }

            //area
            for (int i = 0; gameObjects[typ].AreaPermanent != null && i < gameObjects[typ].AreaPermanent.Length/3; i++)
            {
                int typSize = gameObjects[typ].Size;
                int dataTyp = gameObjects[typ].AreaPermanent[i*3+0];
                int dataSize = gameObjects[typ].AreaPermanent[i*3+ 1];
                int dataValue = gameObjects[typ].AreaPermanent[i*3+ 2];
                if (!add) dataValue = -dataValue;

                int startX = x - dataSize;
                if (startX < 0) startX = 0;
                int startY = y - dataSize;
                if (startY < 0) startY = 0;
                int endX = x + dataSize + typSize;
                if (endX > width) endX = width;
                int endY = y + dataSize + typSize;
                if (endY > height) endY = height;

                for (int ix = startX; ix < endX; ix++)
                {
                    for (int iy = startY; iy < endY; iy++)
                    {
                        int curPos = ix + iy * width;
                        Data[dataTyp, curPos] += dataValue;
                    }
                }
            }
        }

        public void Build(byte typ, int x, int y)
        {
            Build(typ, x + y * width);
        }
        public void Build(byte typ, int pos)
        {

            int x = pos % width;
            int y = (pos - x) / width; 

            int size = gameObjects[typ].Size;

            if (x < 0 || y < 0 || x > width - size || y > height - size) return;

            // clear & ref
            for (int ix = 0; ix < size; ix++)
            {
                for (int iy = 0; iy < size; iy++)
                {
                    int curPos = pos + ix + iy * width;
                    Clear(curPos);
                    ReferenceX[curPos] = (byte)ix;
                    ReferenceY[curPos] = (byte)iy;
                }
            }

            //build
            Typ[pos] = typ;
            Version[pos] = 0;
            if (!loadMode) applyBuildResourceCosts(typ);
            applyBuildAreaEffects(typ, pos, true);

            //autoTile
            for (int ix = -1; ix <= size; ix++)
            {
                for (int iy = -1; iy <= size; iy++)
                {
                    autoTile(pos + ix + iy * width);
                }
            }

            //set Data
        }

        public int AutoTileStruct(byte typ, int pos)
        {
            return (byte)applyAutoTile(typ, pos, gameObjects[typ].StructMode, gameObjects[typ].StructNeighbors,0,0,0,0);
        }
        public int AutoTileGround(byte typ, int pos)
        {
            return (byte)applyAutoTile(typ, pos, gameObjects[typ].GroundMode, gameObjects[typ].GroundNeighbors,0,0,0,0);
        }
        private void autoTile(int pos)
        {
            int x = pos % width;
            int y = (pos - x) / width;
            if (x >= 0 && y >= 0 && x <= width - 1 && y <= height - 1)
            {
                byte typ = Typ[pos];
                TileStruct[pos] = (byte)AutoTileStruct(typ, pos);
                TileGround[pos] = (byte)AutoTileGround(typ, pos);
                if (gameObjects[typ].Texture != null)Version[pos] = (byte)(rnd.NextDouble() * gameObjects[typ].Texture[TileStruct[pos]].Length);
            }
        }
        public int applyAutoTile(byte typ,int pos,int graphicMode,byte[] graphicNeighbors,byte l, byte u, byte r, byte o)
        {
            if (graphicMode == 0 || graphicNeighbors.Length == 0) return 0;

            int x = pos % width;
            int y = (pos - x) / width;


            byte code = 0;

            //byte l = 0, u = 0, r = 0, o = 0;
            if (graphicNeighbors.Length == 1 && graphicNeighbors[0] == typ)
            {
                if (Typ[pos - 1] == typ) l= 1;//l
                if (Typ[pos + Width] == typ) u=1;//u
                if (Typ[pos + 1] == typ) r=1;//r
                if (Typ[pos - Width] == typ) o=1;//o
            }

            else
            {
                for (int i = 0; graphicNeighbors != null && i < graphicNeighbors.Length; i++)
                {
                    if (x > 0 && Typ[pos - 1] == graphicNeighbors[i]) l = 1;//l
                    if (x+1 < width && Typ[pos + 1] == graphicNeighbors[i]) r = 1;//r
                    if (y > 0 && Typ[pos - Width] == graphicNeighbors[i]) o = 1;//o
                    if (y+1 < height && Typ[pos + Width] == graphicNeighbors[i]) u = 1;//u
                }
            }
            code = (byte)(1 * l + 4 * r + 8 * o + 2 * u);
            //gmode{ not=0, all=1, focu=2, foen=3, cuen=4, fo=5, cu=6, en=7, st=8}
            switch (graphicMode)
            {
                case 1:return code;
                case 2:
                    switch (code)
                    {
                        default: return 0;
                    }
                case 3:
                    switch (code)
                    {
                        default: return 0;
                    }
                case 4:
                    switch (code)
                    {
                        default: return 0;
                    }
                    break;
                case 5:
                    switch (code)
                    {
                        default: return 0;
                    }
                case 6:
                    switch (code)
                    {
                        default: return 0;
                    }
                case 7:
                    switch (code)
                    {
                        default:return 0;
                    }
                case 8:
                    switch (code)
                    {
                        case 01: case 04: case 05: case 07: case 13: return 1;
                        case 02: case 08: case 10: case 11: case 14: return code = 2;
                        default: return code = 0;
                    }
            }
            return 0;
        }
        public void Clear(int pos) 
        {
            pos = pos - ReferenceX[pos] - ReferenceY[pos] * width;
            int size = gameObjects[Typ[pos]].Size;
            applyBuildAreaEffects(Typ[pos], pos, false);
            for (int ix = 0; ix < size; ix++)
            {
                for (int iy = 0; iy < size; iy++)
                {
                    int curPos = pos + ix + iy * width;
                    Typ[curPos] = 0;
                    ReferenceX[curPos] = 0;
                    ReferenceY[curPos] = 0;
                }
            }
            //return pos;
        }

        public bool TestResourcesBuild(byte typ)
        {
            if (gameObjects[typ].ResourcesBuild == null) return true;
            for (int i = 0; i < gameObjects[typ].ResourcesBuild.Length/2; i++)
            {
                int dataTyp = gameObjects[typ].ResourcesBuild[i*2+ 0];
                int dataValue = gameObjects[typ].ResourcesBuild[i*2+ 1];
                if (resources[dataTyp].Value + dataValue < 0) return false;
            }
            return true;

        }
        public int TestResourcesDependet(byte typ)
        {
            if (gameObjects[typ].ResourcesDependent == null) return 0;
            int retValue = 0;
            for (int i = 0; i < gameObjects[typ].ResourcesDependent.Length/5; i++)
            {
                int dataTyp = gameObjects[typ].ResourcesDependent[i*5+ 0];
                int dataMin = gameObjects[typ].ResourcesDependent[i*5+ 1];
                int dataMax = gameObjects[typ].ResourcesDependent[i*5+ 2];
                int dataEffects = gameObjects[typ].ResourcesDependent[i*5+ 3];
                int dataInvert = gameObjects[typ].ResourcesDependent[i*5+ 4];

                int curValue = (int)resources[dataTyp].Value;
                bool fire = false;
                fire = curValue >= dataMin && curValue <= dataMax;
                if (dataInvert == 1) fire = !fire;
                if (fire && retValue < dataEffects) retValue = dataEffects;
            }
            return retValue;

        }
        public int TestAreaDependet(byte typ, int pos)
        {
            if (gameObjects[typ].AreaDependent == null) return 0;
            int retValue = 0;

            int size = gameObjects[typ].Size;
            for (int i = 0; i < gameObjects[typ].AreaDependent.Length/5; i++)
            {
                int dataTyp = gameObjects[typ].AreaDependent[i*5+0];
                int dataMin = gameObjects[typ].AreaDependent[i*5+1];
                int dataMax = gameObjects[typ].AreaDependent[i*5+ 2];
                int dataEffects = gameObjects[typ].AreaDependent[i*5+ 3];
                int dataInvert = gameObjects[typ].AreaDependent[i*5+ 4];

                bool result = true;
                for (int ix = 0; ix < size; ix++)
                {
                    for (int iy = 0; iy < size; iy++)
                    {
                        int curPos = pos + ix + iy * width;

                        int curValue = Data[dataTyp,curPos];

                        if (dataInvert == 0)
                        {
                            result = result & !(curValue < dataMin || curValue > dataMax);
                        }
                        else
                        {
                            result = result & (curValue < dataMin || curValue > dataMax);
                        }
                    }
                }
                if (result && retValue < dataEffects) retValue = dataEffects;
            }
            return retValue;
        }
        public void UpdateField(int pos)
        {
            if (ReferenceX[pos] != 0 || ReferenceY[pos] != 0) return;
            byte typ = Typ[pos];
            int result = TestAreaDependet(typ, pos);
            int result2 = TestResourcesDependet(typ);
            if (result < result2) result = result2;
            if (result == 0) return;
            else if (result == 1) replaceTyp(typ, pos, gameObjects[typ].UpgradeTyp);
            else if (result == 2) replaceTyp(typ, pos, gameObjects[typ].DowngradeTyp);
            else if (result == 3) replaceTyp(typ, pos, gameObjects[typ].DemolitionTyp);
            else if (result == 4) replaceTyp(typ, pos, gameObjects[typ].DecayTyp);
            else if (result == 5) replaceTyp(typ, pos, gameObjects[typ].DestroyTyp);
            else if (result == 6) Clear(pos);
            for (int i = 0; gameObjects[typ].ResourcesEffect != null && i < gameObjects[typ].ResourcesEffect.Length/3; i++)
            {
                if (gameObjects[typ].ResourcesEffect[i*3+ 0] == result)
                {
                    resources[gameObjects[typ].ResourcesEffect[i*3+ 1]].Value += gameObjects[typ].ResourcesEffect[i*3+ 2];
                }
            }
            return;
        }

        private void replaceTyp(byte typ, int pos,byte[] replace)
        {
            int newTyp = (int)(replace.Length * rnd.NextDouble());
            if (gameObjects[typ].Size == gameObjects[replace[newTyp]].Size)
            {
                Build((byte)replace[0], pos);
            }
            else if (gameObjects[replace[newTyp]].Size == 1)
            {
                int size = gameObjects[typ].Size;
                for (int ix = 0;ix < size; ix++)
                {
                    for (int iy = 0; iy < size; iy++)
                    {    
                        Build((byte)replace[newTyp], pos + ix + iy * width);
                    }
                }
            }
            return;
        }

        private void autoGround(byte ground, int pos)
        {
            if (Ground[pos] == ground) return;
            byte envCode = 0;
            bool envO = false;
            if (Ground[pos - width] == ground) {envO = true;envCode++;}
            bool envU = false;
            if (Ground[pos + width] == ground) {envU = true;envCode++;}
            bool envR = false;
            if (Ground[pos + 1] == ground) {envR = true;envCode++;}
            bool envL = false;
            if (Ground[pos - 1] == ground) {envL = true;envCode++;}

            if (envCode == 0) { return; }
            else if (envCode == 3 || envCode == 4 || (envO && envU) || (envL && envR))
            {
                Ground[pos] = ground;
                return;
            }
            else if (envO & envR) Ground[pos] = (byte)(ground - 16 + 1);
            else if (envR & envU) Ground[pos] = (byte)(ground + 16 + 1);
            else if (envU & envL) Ground[pos] = (byte)(ground - 1 + 16);
            else if (envL & envO) Ground[pos] = (byte)(ground - 1 - 16);

            else if (envO) Ground[pos] = (byte)(ground - 16);
            else if (envU) Ground[pos] = (byte)(ground + 16);
            else if (envL) Ground[pos] = (byte)(ground - 1);
            else if (envR) Ground[pos] = (byte)(ground + 1);
        }
        private void autoGround(byte ground)
        {
            for (int ix = 1; ix < width-1; ix++)
            {
                for (int iy = 1; iy < height-1; iy++)
                {
                    int pos = ix + iy * width;
                    autoGround(ground, ix + iy * width);
                }
            }
            //if (!envO
        }

        public void GenerateMap(Image map)
        {
            Random rnd = new Random(1000);
            Console.WriteLine(map.Width);
            BuildWorld(map.Width, map.Height);
            LockBitmap data = new LockBitmap((Bitmap)map, true);
            byte[] rgbData = data.getData();
            Console.WriteLine("Bitmap: " + (int)(map.Width * map.Height) + " Map: " + rgbData.Length / 4);
            loadMode = true;
            for (int i = 0; i < rgbData.Length / 4; i++)
            {
                if (rnd.NextDouble() > 0.5)
                {
                    Ground[i] = 1;
                    if (rnd.NextDouble() < 0.5)
                    {
                        Ground[i] = 2;
                    }
                }

                if (rgbData[i * 4 + 0] == 255)
                {
                    Build(1, i);
                    Ground[i] = 49;
                }
                if (rgbData[i * 4 + 0] == 112)
                {
                    Build(10, i);
                }
                if (rgbData[i * 4 + 0] == 151)
                {
                    Ground[i] = 52;
                }
                if (rgbData[i * 4 + 0] == 77)
                {
                    Ground[i] = 49;
                }
                else if (rgbData[i * 4 + 0] == 254)
                {
                    Build(2, i);
                    Ground[i] = 52;
                }
                else if (rgbData[i * 4 + 1] == 80)
                {
                    Build(3, i);
                    Ground[i] = 3;
                }
                else if (rgbData[i * 4 + 1] == 100)
                {
                    Build(4, i);
                    Ground[i] = 3;
                }
                else if (rgbData[i * 4 + 1] == 160)
                {
                    Build(5, i);
                    Ground[i] = 3;
                }

                if (rnd.NextDouble() < 0.001) Ground[i] = 5;
                //if (rgbData[i * 4 + 1] == 128) Build(1, i);
            }
            autoGround(52);
            autoGround(52);
            autoGround(49);
            autoGround(49);


            loadMode = false;
        }
    }
}
