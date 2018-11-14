using System.Drawing;
namespace CityGame
{
    public struct BuildOption
    {
        public int Typ;
        public string Text;
        public Color Color;
        public int Value;
        public int BuildMode;
        public byte[] BuildReplace;

        public BuildOption(string text, int typ)
        {
            Text = text; Typ = typ; Value = 0; Color = Color.Transparent; BuildMode = 0; BuildReplace = null;
        }
        public BuildOption(string text, int typ, int value, int buildMode, byte[] buildReplace, Color color)
        {
            Text = text; Typ = typ; Value = value; Color = color; BuildMode = buildMode; BuildReplace = buildReplace;
        }
    }
}