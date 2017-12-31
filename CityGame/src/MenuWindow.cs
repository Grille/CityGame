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

namespace CityGame
{
    public partial class MenuWindow : Form
    {
        private Panel currentPanel;
        private Panel lastPanel;
        private Bitmap mapImage;
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

            Image menuButten1 = new Bitmap("../Data/texture/gui/menuButton1.png");
            Image menuButten2 = new Bitmap("../Data/texture/gui/menuButton2.png");
            Image menuButten3 = new Bitmap("../Data/texture/gui/menuButton3.png");

            imageButton1.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton2.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton3.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton4.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton5.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton6.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton7.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton8.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton9.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton10.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton11.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton12.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton13.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton14.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton15.LoadImages(menuButten1, menuButten2, menuButten3);
            imageButton16.LoadImages(menuButten1, menuButten2, menuButten3);

            this.Size = new Size(640, 400);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer,true);

            currentPanel = mainMenu;
        }
        public new void Show()
        {
			// hallo
            base.Show(Program.MainWindow);
            this.Location = new Point(SystemInformation.VirtualScreen.Width / 2 - 320, SystemInformation.VirtualScreen.Height / 2 - 200);
            this.Size = new Size(640, 400);
            timer.Enabled = true;
        }
        public void Show(int mode)
        {
            base.Show(Program.MainWindow);
            this.Location = new Point(SystemInformation.VirtualScreen.Width / 2 - 320, SystemInformation.VirtualScreen.Height / 2 - 200);
            this.Size = new Size(640, 400);
            timer.Enabled = true;
            switchPanel(mode);
        }
        public new void Hide()
        {
            timer.Enabled = false;
            base.Hide();
        }
        private void switchPanel(int mode)
        {
            this.SuspendLayout();
            //if (currentPanel != null) {
            if (mode >= 0 )lastPanel = currentPanel;
            //}
            switch (mode) {
                case -1: currentPanel = lastPanel; break;
                case 0: currentPanel = mainMenu; break;
                case 1: currentPanel = gameMenu; break;
                case 2: currentPanel = options; break;
                case 3: currentPanel = newGame; break;
                case 4: currentPanel = loadGame; break;
                case 5: currentPanel = saveGame; break;
            }
            currentPanel.Visible = true;
            if (currentPanel != lastPanel)lastPanel.Visible = false;
            if (currentPanel.Location.X != 0)
            {
                currentPanel.Location = new Point(0, 0);
                currentPanel.Size = this.Size;
            }

            this.ResumeLayout(true);
            currentPanel.Refresh();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            switchPanel(0);
        }
        private void buttonMainMenu_Click(object sender, EventArgs e)
        {
            switchPanel(0);
        }
        private void buttonNewGameMenu_Click(object sender, EventArgs e)
        {
            switchPanel(3);
            listBoxNewGame.Items.Clear();

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo("../Data/Maps");

            foreach (System.IO.FileInfo f in ParentDirectory.GetFiles())
            {
                listBoxNewGame.Items.Add(f.Name);
            }
            
        }

        private void buttonLoadGameMenu_Click(object sender, EventArgs e)
        {
            switchPanel(4);
        }

        private void buttonSaveGameMenu_Click(object sender, EventArgs e)
        {
            switchPanel(5);
        }
        private void buttonSaveGame_Click(object sender, EventArgs e)
        {
            Program.MainWindow.World.Save("../saves/game.txt");
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            Hide();
            Program.MainWindow.World.GenerateMap(mapImage);
            Program.MainWindow.StartGame();
            Program.MenuOverlay.Show(Program.MainWindow);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //Program.mainWindow.StartGame();
            Program.MainWindow.Cam.Move(1, 1);
        }

        private void listBoxNewGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            mapImage = new Bitmap("../data/maps/" + (string)listBoxNewGame.SelectedItem);
            //pictureBoxNewGame.Image = bitmap;
            pictureBoxNewGame.Refresh();
        }

        private void renderMapPreview(object sender, PaintEventArgs e)
        {
            PictureBox pbsender = (PictureBox)sender;
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(mapImage, new Rectangle(0, 0, pbsender.Width, pbsender.Height));
            g.DrawString("Size: " + mapImage.Width + "x" + mapImage.Height, new Font(new FontFamily("Franklin Gothic Medium"), 12), new SolidBrush(Color.Black), new Point(0, pbsender.Height-12*2));
        }

    }
}
