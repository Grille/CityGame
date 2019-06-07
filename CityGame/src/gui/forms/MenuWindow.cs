using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using CityGame.Control;

namespace CityGame
{
    public enum NextPanel { Nothing,Last,MainMenu,GameMenu,Options,NewGame,LoadGame,SaveGame}
    public partial class MenuWindow : Form
    {
        private Panel currentPanel;
        private Panel lastPanel;
        private Bitmap mapImage;
        private int browserMode;
        private NextPanel lastMode;
        private NextPanel curMode;
        public MenuWindow()
        {


            mapImage = new Bitmap(1, 1);
            //listBox1.Items.
            InitializeComponent();
            Console.WriteLine("imbut: "+imageButton1.Text);
            Bitmap backgroundImage = new Bitmap("../Data/texture/gui/menu.png");

            mainMenu.BackgroundImage = backgroundImage;
            gameMenu.BackgroundImage = backgroundImage;
            newGame.BackgroundImage = backgroundImage;
            loadGame.BackgroundImage = backgroundImage;
            saveGame.BackgroundImage = backgroundImage;

            Image menuButton = new Bitmap("../Data/texture/gui/menuButton.png");
            Image menuButtonDown = new Bitmap("../Data/texture/gui/menuButtonDown.png");


            imageButton1.LoadImages(menuButton, menuButtonDown);
            imageButton2.LoadImages(menuButton, menuButtonDown);
            imageButton3.LoadImages(menuButton, menuButtonDown);
            imageButton4.LoadImages(menuButton, menuButtonDown);
            ibBrowserBack.LoadImages(menuButton, menuButtonDown);
            ibBrowserNext.LoadImages(menuButton, menuButtonDown);
            imageButton7.LoadImages(menuButton, menuButtonDown);
            imageButton8.LoadImages(menuButton, menuButtonDown);
            imageButton9.LoadImages(menuButton, menuButtonDown);
            imageButton10.LoadImages(menuButton, menuButtonDown);
            imageButton11.LoadImages(menuButton, menuButtonDown);
            imageButton12.LoadImages(menuButton, menuButtonDown);
            imageButton13.LoadImages(menuButton, menuButtonDown);
            imageButton14.LoadImages(menuButton, menuButtonDown);
            imageButton15.LoadImages(menuButton, menuButtonDown);
            imageButton16.LoadImages(menuButton, menuButtonDown);

            this.Size = new Size(640, 400);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer,true);

            currentPanel = mainMenu;
            

        }
        public new void Show()
        {
            this.Location = new Point(SystemInformation.VirtualScreen.Width / 2 - 320, SystemInformation.VirtualScreen.Height / 2 - 200);
            this.Size = new Size(640, 400);
            base.Show(Program.MainWindow);
        }
        public void Show(NextPanel mode)
        {
            this.Location = new Point(SystemInformation.VirtualScreen.Width / 2 - 320, SystemInformation.VirtualScreen.Height / 2 - 200);
            this.Size = new Size(640, 400);
            base.Show(Program.MainWindow);
            switchPanel(mode);
        }
        public new void Hide()
        {
            timer.Enabled = false;
            base.Hide();
        }
        private void switchPanel(NextPanel mode)
        {
            if (mode == NextPanel.Last) mode = lastMode;
            lastMode = curMode;
            if (mode != NextPanel.Last) curMode = mode;

            switch (mode) {
                case NextPanel.MainMenu: currentPanel = mainMenu; break;
                case NextPanel.GameMenu: currentPanel = gameMenu; break;
                case NextPanel.Options: currentPanel = options; break;
                case NextPanel.NewGame: currentPanel = newGame; break;
                case NextPanel.LoadGame: currentPanel = loadGame; break;
                case NextPanel.SaveGame: currentPanel = saveGame; break;
            }
            currentPanel.Location = new Point(0, 0);
            currentPanel.Size = this.Size;
            currentPanel.Visible = true;
            if (lastPanel != null && currentPanel != lastPanel)lastPanel.Visible = false;
            //Invalidate();

            lastPanel = currentPanel;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            switchPanel(NextPanel.Last);
        }
        private void buttonMainMenu_Click(object sender, EventArgs e)
        {
            switchPanel(NextPanel.MainMenu);
        }
        private void buttonGoToNewGameMenu_Click(object sender, EventArgs e)
        {
            lbBrowser.Items.Clear();

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo("../Data/Maps");

            foreach (System.IO.FileInfo f in ParentDirectory.GetFiles())
            {
                lbBrowser.Items.Add(f.Name);
            }
            switchPanel(NextPanel.NewGame); browserMode = 0;
            
        }

        private void buttonGoToLoadGameMenu_Click(object sender, EventArgs e)
        {
            lbBrowser.Items.Clear();

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo("../saves");

            foreach (System.IO.FileInfo f in ParentDirectory.GetFiles())
            {
                string[] fileName = f.Name.Split(new char[] { '.' });
                if (fileName[fileName.Length - 1] == "city")
                    lbBrowser.Items.Add(fileName[0]);
            }
            switchPanel(NextPanel.NewGame); browserMode = 1;
        }

        private void buttonGoToSaveGameMenu_Click(object sender, EventArgs e)
        {
           
            lbSaveGame.Items.Clear();

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo("../saves");
            foreach (System.IO.FileInfo f in ParentDirectory.GetFiles())
            {
                string[]  fileName = f.Name.Split(new char[] { '.' });
                if(fileName[fileName.Length-1] == "city")
                lbSaveGame.Items.Add(fileName[0]);
            }
            switchPanel(NextPanel.SaveGame);
        }
        private void buttonSaveGame_Click(object sender, EventArgs e)
        {
            Program.Game.Save("../saves/"+ textBoxSaveName.Text+".city");

            File.Delete("../saves/" + textBoxSaveName.Text + ".png");
            Bitmap bitmap = Program.Game.GenerateMiniMap();
            bitmap.Save("../saves/" + textBoxSaveName.Text + ".png");
            bitmap.Dispose();
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            Hide();
            if (browserMode == 0)
            {
                Program.Game.GenerateMap(mapImage);
                Program.Game.Start();
                Program.Game.NewGame();
                Program.MenuOverlay.Show(Program.MainWindow);
            }
            else
            {
                Console.WriteLine("../saves/" + lbBrowser.SelectedItem);
                Program.Game.Load("../saves/" + lbBrowser.SelectedItem + ".city");
                Program.Game.Start();
                Program.MenuOverlay.Show(Program.MainWindow);
            }
        }
        private void buttonBackToGame_Click(object sender, EventArgs e)
        {
            Hide();
            Program.Game.Start();
            Program.MenuOverlay.Show(Program.MainWindow);
        }

        private void timer_Tick(object sender, EventArgs e)
        {

        }

        private void lbBrowser_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = "";
            if (browserMode == 0) path = ("../data/maps/" + (string)lbBrowser.SelectedItem);
            else path = ("../saves/" + (string)lbBrowser.SelectedItem + ".png");
            if (File.Exists(path))
            {
                using (var bmpTemp = new Bitmap(path))
                {
                    mapImage = new Bitmap(bmpTemp);
                }
                pbBrowser.Refresh();
            }
        }

        private void pbBrowser_Paint(object sender, PaintEventArgs e)
        {
            if (mapImage == null) return;
            PictureBox pbsender = (PictureBox)sender;
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(mapImage, new Rectangle(0, 0, pbsender.Width, pbsender.Height));
            g.DrawString("Size: " + mapImage.Width + "x" + mapImage.Height, new Font(new FontFamily("Franklin Gothic Medium"), 12), new SolidBrush(Color.Black), new Point(0, pbsender.Height-12*2));
        }

        private void listBoxSaveGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxSaveName.Text = (string)lbSaveGame.SelectedItem;
        }

    }
}
