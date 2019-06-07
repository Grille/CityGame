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
                    applyBuildResourceCosts(newTyp);
                    Build(newTyp, pos);
                }
                else
                {
                    byte buildTyp = replaceBuildTyp(World.Typ[pos]);
                    if (CanBuild(buildTyp, pos))
                    {
                        applyBuildResourceCosts(buildTyp);
                        Build(buildTyp, pos);
                        buildPreviewEnabled = false;
                    }
                }
            }
            else if (option.Typ == 2)
            {
                World.Zone[pos] = (byte)option.Value;
            }
            
        }
        public void Build(byte typ, int x, int y)
        {
            Build(typ, x + y * World.Width);
        }
        public void Build(byte typ, int pos)
        {

            int x = pos % World.Width;
            int y = (pos - x) / World.Width;

            int size = Objects[typ].Size;

            if (x < 0 || y < 0 || x > World.Width - size || y > World.Height - size) return;

            // clear & ref
            for (int ix = 0; ix < size; ix++)
            {
                for (int iy = 0; iy < size; iy++)
                {
                    int curPos = pos + ix + iy * World.Width;
                    Clear(curPos);
                    World.ReferenceX[curPos] = (byte)ix;
                    World.ReferenceY[curPos] = (byte)iy;
                }
            }
            Random rnd = new Random();
            //build
            World.Typ[pos] = typ;
            World.Version[pos] = genVersion(pos);
            //if (!loadMode) applyBuildResourceCosts(typ);
            applyBuildAreaEffects(typ, pos, true);

            //autoTile
            for (int ix = -1; ix <= size; ix++)
            {
                for (int iy = -1; iy <= size; iy++)
                {
                    autoTile(pos, ix, iy);
                }
            }
            //set Data
        }
        public void Clear(int pos)
        {
            pos = pos - World.ReferenceX[pos] - World.ReferenceY[pos] * World.Width;
            int size = Objects[World.Typ[pos]].Size;
            applyBuildAreaEffects(World.Typ[pos], pos, false);
            for (int ix = 0; ix < size; ix++)
            {
                for (int iy = 0; iy < size; iy++)
                {
                    int curPos = pos + ix + iy * World.Width;
                    World.Typ[curPos] = 0;
                    World.ReferenceX[curPos] = 0;
                    World.ReferenceY[curPos] = 0;
                }
            }
            //return pos;
        }
        private void autoTile(int pos, int offsetX, int offsetY)
        {
            pos = pos + offsetX + offsetY * World.Width;
            int posX = pos % World.Width, posY = (pos - posX) / World.Width;
            if (posX >= 0 && posY >= 0 && posX <= World.Width - 1 && posY <= World.Height - 1)
            {
                byte typ = World.Typ[pos];
                var obj = Objects[typ];
                World.TileStruct[pos] = applyAutoTile(typ, pos, obj.StructMode, obj.StructNeighbors);
                World.TileGround[pos] = applyAutoTile(typ, pos, obj.GroundMode, obj.GroundNeighbors);
                if (obj.Texture != null && (obj.Texture[World.TileStruct[pos]].Length < World.Version[pos]))
                {
                    World.Version[pos] = genVersion(pos);
                }
            }
        }
        private byte genVersion(int pos)
        {
            var obj = Objects[World.Typ[pos]];
            if (obj.Texture == null) return 0;
            return (byte)(rnd.NextDouble() * obj.Texture[World.TileStruct[pos]].Length);
        }
        private byte applyAutoTile(byte typ, int pos, int graphicMode, byte[] graphicNeighbors)
        {
            return (byte)applyAutoTile(typ, pos, graphicMode, graphicNeighbors, 0, 0, 0, 0);
        }
        private byte applyAutoTile(byte typ, int pos, int graphicMode, byte[] graphicNeighbors, byte l, byte u, byte r, byte o)
        {
            if (graphicMode == 0 || graphicNeighbors.Length == 0) return 0;

            int x = pos % World.Width;
            int y = (pos - x) / World.Width;


            byte code = 0;

            //byte l = 0, u = 0, r = 0, o = 0;


            for (int i = 0; graphicNeighbors != null && i < graphicNeighbors.Length; i++)
            {
                if (x > 0 && World.Typ[pos - 1] == graphicNeighbors[i]) l = 1;//l
                if (x + 1 < World.Width && World.Typ[pos + 1] == graphicNeighbors[i]) r = 1;//r
                if (y > 0 && World.Typ[pos - World.Width] == graphicNeighbors[i]) o = 1;//o
                if (y + 1 < World.Height && World.Typ[pos + World.Width] == graphicNeighbors[i]) u = 1;//u
            }

            code = (byte)(1 * l + 4 * r + 8 * o + 2 * u);
            //gmode{ not=0, all=1, foCu=2, foEn=3, cuEn=4, fo=5, cu=6, en=7, st=8}
            switch (graphicMode)
            {
                case 1: return code;
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
                case 7://en
                    switch (code)
                    {
                        case 01: case 04: case 05: case 07: case 13: return 1;
                        case 02: case 08: case 10: case 11: case 14: return code = 2;
                        default: return code = 0;
                    }
                case 8://st
                    switch (code)
                    {
                        case 01: case 04: case 05: case 07: case 13: return 1;
                        case 02: case 08: case 10: case 11: case 14: return code = 2;
                        default: return code = 0;
                    }
            }
            return 0;
        }
        public bool CanBuild(byte typ, int x, int y)
        {
            return CanBuild(typ, x + y * World.Width);
        }
        public bool CanBuild(byte typ, int pos)
        {

            int x = pos % World.Width;
            int y = (pos - x) / World.Width;

            int size = Objects[typ].Size;
            if (x < 0 || y < 0 || x > World.Width - size || y > World.Height - size) return false;//is on map
            //if (!loadMode && !TestResourcesBuild(typ)) return false;
            if (TestResourcesDependet(typ) > 1) return false;
            if (TestAreaDependet(typ, pos) > 1) return false;
            if (Objects[typ].CanBuiltOnTyp == null || Objects[typ].CanBuiltOnTyp.Length == 0) return true;//can build on all
            bool returnValue = true;




            for (int ix = 0; ix < size; ix++)
            {
                for (int iy = 0; iy < size; iy++)
                {
                    int curPos = pos + ix + iy * World.Width;
                    curPos = curPos - World.ReferenceX[curPos] - World.ReferenceY[curPos] * World.Width;
                    bool matching = false;
                    for (int i = 0; Objects[typ].CanBuiltOnTyp != null && i < Objects[typ].CanBuiltOnTyp.Length; i++)
                    {
                        matching = matching || World.Typ[curPos] == Objects[typ].CanBuiltOnTyp[i];
                    }
                    returnValue = returnValue && matching;
                }
            }


            return returnValue;
        }
        private void applyBuildResourceCosts(byte typ)
        {
            //resources
            var obj = Objects[typ];
            for (int i = 0; obj.ResourcesBuild != null && i < obj.ResourcesBuild.Length / 2; i++)
            {
                int dataTyp = obj.ResourcesBuild[i * 2 + 0];
                int dataValue = obj.ResourcesBuild[i * 2 + 1];
                Resources[dataTyp].Value += dataValue;
            }
        }
        public bool TestResourcesBuild(byte typ)
        {
            var obj = Objects[typ];
            if (obj.ResourcesBuild == null) return true;
            for (int i = 0; i < obj.ResourcesBuild.Length / 2; i++)
            {
                int dataTyp = obj.ResourcesBuild[i * 2 + 0];
                int dataValue = obj.ResourcesBuild[i * 2 + 1];
                if (Resources[dataTyp].Value + dataValue < 0) return false;
            }
            return true;

        }
        public int TestResourcesDependet(byte typ)
        {
            var obj = Objects[typ];
            if (obj.ResourcesDependent == null) return 0;
            int retValue = 0;
            for (int i = 0; i < obj.ResourcesDependent.Length / 5; i++)
            {
                int dataTyp = obj.ResourcesDependent[i * 5 + 0];
                int dataMin = obj.ResourcesDependent[i * 5 + 1];
                int dataMax = obj.ResourcesDependent[i * 5 + 2];
                int dataEffects = obj.ResourcesDependent[i * 5 + 3];
                int dataInvert = obj.ResourcesDependent[i * 5 + 4];

                int curValue = (int)Resources[dataTyp].Value;
                bool fire = false;
                fire = curValue >= dataMin && curValue <= dataMax;
                if (dataInvert == 1) fire = !fire;
                if (fire && retValue < dataEffects) retValue = dataEffects;
            }
            return retValue;

        }
        public int TestAreaDependet(byte typ, int pos)
        {
            var obj = Objects[typ];
            if (obj.AreaDependent == null) return 0;
            int retValue = 0;

            int size = obj.Size;
            for (int i = 0; i < obj.AreaDependent.Length / 5; i++)
            {
                int dataTyp = obj.AreaDependent[i * 5 + 0];
                int dataMin = obj.AreaDependent[i * 5 + 1];
                int dataMax = obj.AreaDependent[i * 5 + 2];
                int dataEffects = obj.AreaDependent[i * 5 + 3];
                int dataInvert = obj.AreaDependent[i * 5 + 4];

                bool result = true;
                for (int ix = 0; ix < size; ix++)
                {
                    for (int iy = 0; iy < size; iy++)
                    {
                        int curPos = pos + ix + iy * World.Width;

                        int curValue = (int)World.Data[dataTyp, curPos];

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

        private void applyBuildAreaEffects(byte typ, int pos, bool add)
        {
            int x = pos % World.Width;
            int y = (pos - x) / World.Width;

            if (add) World.TypCount[typ]++;
            else World.TypCount[typ]--;

            var obj = Objects[typ];

            //resources
            for (int i = 0; obj.ResourcesPermanent != null && i < obj.ResourcesPermanent.Length / 2; i++)
            {
                int dataTyp = obj.ResourcesPermanent[i * 2 + 0];
                int dataValue = obj.ResourcesPermanent[i * 2 + 1];
                if (!add) dataValue = -dataValue;
                Resources[dataTyp].Value += dataValue;
            }
            for (int i = 0; obj.ResourcesMonthly != null && i < obj.ResourcesMonthly.Length / 2; i++)
            {
                int dataTyp = obj.ResourcesMonthly[i * 2 + 0];
                int dataValue = obj.ResourcesMonthly[i * 2 + 1];
                if (!add) dataValue = -dataValue;
                Resources[dataTyp].AddValue += dataValue;
            }

            //area
            for (int i = 0; obj.AreaPermanent != null && i < obj.AreaPermanent.Length / 3; i++)
            {
                int typSize = obj.Size;
                int dataTyp = obj.AreaPermanent[i * 3 + 0];
                int dataSize = obj.AreaPermanent[i * 3 + 1];
                int dataValue = obj.AreaPermanent[i * 3 + 2];
                if (!add) dataValue = -dataValue;

                int startX = x - dataSize;
                if (startX < 0) startX = 0;
                int startY = y - dataSize;
                if (startY < 0) startY = 0;
                int endX = x + dataSize + typSize;
                if (endX > World.Width) endX = World.Width;
                int endY = y + dataSize + typSize;
                if (endY > World.Height) endY = World.Height;

                for (int ix = startX; ix < endX; ix++)
                {
                    for (int iy = startY; iy < endY; iy++)
                    {
                        int curPos = ix + iy * World.Width;

                        if (Areas[dataTyp].Smooth)
                        {
                            int dstX = Math.Abs(ix - x), dstY = Math.Abs(iy - y);
                            float dst = (float)(Math.Sqrt(dstX * dstX + dstY * dstY));
                            float p;
                            if (dst > dataSize + 1) p = 0;
                            else p = 1 - (dst / (dataSize + 1));
                            float value = (float)dataValue * p;
                            World.Data[dataTyp, curPos] += value;
                        }
                        else World.Data[dataTyp, curPos] += dataValue;
                    }
                }
            }
        }
        private void replaceTyp(byte typ, int pos, byte[] replace)
        {
            int newTyp = (int)(replace.Length * rnd.NextDouble());
            var oldObj = Objects[typ];
            var newObj = Objects[replace[newTyp]];
            if (oldObj.Size == newObj.Size)
            {
                Build((byte)replace[0], pos);
            }
            else if (newObj.Size == 1)
            {
                int size = oldObj.Size;
                for (int ix = 0; ix < size; ix++)
                {
                    for (int iy = 0; iy < size; iy++)
                    {
                        Build((byte)replace[newTyp], pos + ix + iy * World.Width);
                    }
                }
            }
            return;
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