using System;
using System.Windows.Forms;

namespace CityGame
{
    public partial class Game
    {
        private bool[] isKeyDown = new bool[256];
        public void KeyDown(KeyEventArgs e)
        {
            isKeyDown[e.KeyValue] = true;

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    StopRendering();
                    Program.MenuWindow.Show(NextPanel.GameMenu);
                    Program.MenuOverlay.Hide();
                    break;
                case Keys.Q:
                    if (e.Control) Application.Exit();
                    break;
            }
        }
        public void KeyUp(KeyEventArgs e)
        {
            isKeyDown[e.KeyValue] = false;
        }
    }
}