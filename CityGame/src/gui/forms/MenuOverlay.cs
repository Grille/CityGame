using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

//using GGL.Control;
using CityGame.Control;


namespace CityGame
{
    public partial class MenuOverlay : Form
    {
        Control.ListBox listBox;
        BuildOption[][] buildOptions;
        ImageButton[] imageButtons;
        public MenuOverlay()
        {
            listBox = new Control.ListBox();
            listBox.BackColor = Color.FromArgb(99, 139, 139);
            listBox.ItemHeight = 26;
            listBox.ItemDistance = 4;
            listBox.Font = new Font("Franklin Gothic Medium", 11.25f);
            listBox.ChangeItem += new System.EventHandler(listBox_ChangeItem);
            listBox.Enabled = true;
            listBox.BorderImage = new Bitmap("../Data/texture/gui/border.png");
            listBox.ButtonBorderImage = new Bitmap("../Data/texture/gui/border2.png");
            listBox.ButtonDownBorderImage = new Bitmap("../Data/texture/gui/border3.png");
            Controls.Add(listBox);

            imageButtons = new ImageButton[10];
            buildOptions = new BuildOption[10][];
            InitializeComponent();
            GGL.IO.Parser parser = new GGL.IO.Parser();
            parser.AddAttribute("string", "name", "");
            parser.AddAttribute("int", "value", "0");
            parser.AddAttribute("int", "typ", "1");
            parser.AddAttribute("int", "mode", "0");
            parser.AddAttribute("byte[]", "replace", "[]");
            parser.AddAttribute("byte[]", "color", "[]");
            parser.AddEnum("mode", new string[] { "single", "brush", "line", "equalLine", "cnline", "area", "equalArea" });
            parser.AddEnum("typ", new string[] { "label", "build", "zone" });
            Console.WriteLine("//load: gui");
            parser.ParseFile("../Data/config/guiBuildMenu.gd");

            for (int i = 0; i < 10; i++)
            {
                if (parser.Exists("button_" + i))
                {
                    imageButtons[i] = new ImageButton();
                    imageButtons[i].Size = new Size(64, 64);
                    imageButtons[i].Anchor = AnchorStyles.Right;
                    imageButtons[i].Location = new Point(Width - 72, 72 * i);
                    imageButtons[i].Visible = true;
                    string btnname = parser.GetAttribute<string>("button_" + i, "name");
                    imageButtons[i].LoadImages(new Bitmap("../Data/texture/gui/"+ btnname+"1.png"), new Bitmap("../Data/texture/gui/" + btnname + "3.png"));
                    Controls.Add(imageButtons[i]);

                    int size = -1;
                    while (parser.Exists(btnname+"_" + ++size));
                    buildOptions[i] = new BuildOption[size];
                    for (int i2=0;i2< buildOptions[i].Length; i2++)
                    {
                        string name = btnname + "_" + i2;
                        int typ = parser.GetAttribute<int>(name, "typ");
                        int value = parser.GetAttribute<int>(name, "value");
                        int mode = parser.GetAttribute<int>(name, "mode");
                        var text = parser.GetAttribute<string>(name, "name");
                        var replace = parser.GetAttribute<byte[]>(name, "replace");
                        var bytes = parser.GetAttribute<byte[]>(name, "color");
                        if (bytes.Length == 3)
                            buildOptions[i][i2] = new BuildOption(text,typ, value, mode, replace,Color.FromArgb(bytes[0], bytes[1], bytes[2]));
                        else
                            buildOptions[i][i2] = new BuildOption(text, typ);
                    }
                    imageButtons[i].SwitchMode = true;
                    imageButtons[i].ButtonDown += new System.EventHandler(imageButton_ButtonDown);
                    imageButtons[i].ButtonUp += new System.EventHandler(imageButton_ButtonUp);
                }
            }
            this.DoubleBuffered = true;
        }

        private void imageButton_ButtonDown(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            int id = 0;
            for (int i = 0; i < imageButtons.Length; i++)
            {
                if (imageButtons[i] != null)
                    if (imageButtons[i] != btn)
                        imageButtons[i].ResetButton();
                    else id = i;
            }
            updateListBox(btn,id);
        }

        private void updateListBox(ImageButton btn,int id)
        {
            listBox.Clear();
            listBox.Visible = true;
            listBox.Location = new Point(btn.Left - 200, btn.Top);
            listBox.Size = new Size(200, 200);

            for (int i = 0; i < buildOptions[id].Length; i++)
            {
                listBox.UseColor(buildOptions[id][i].Color);
                if (buildOptions[id][i].Color != Color.Transparent)
                    listBox.Add(buildOptions[id][i].Text, buildOptions[id][i]);
                else
                    listBox.Add(buildOptions[id][i].Text);
            }
            listBox.HeightToContent();
        }
        private void imageButton_ButtonUp(object sender, EventArgs e){
            panelConect.Visible = listBox.Visible = false;
        }


        private void listBox_ChangeItem(object sender, EventArgs e)
        {
            Program.Game.SelectetBuildIndex = (BuildOption)((CityGame.Control.ListBox)sender).getValue();
        }
        private void MenuOverlay_Enter(object sender, EventArgs e)
        {
            Console.WriteLine("Focus");
            Program.MainWindow.Focus();
        }

        private void MenuOverlay_KeyDown(object sender, KeyEventArgs e)
        {
            Program.MainWindow.MainWindow_KeyDown(sender,e);
        }

        private void pictureBoxMinimap_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pbsender = (PictureBox)sender;
            if (pbsender.Image == null) return;
            Graphics g = e.Graphics;
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(pbsender.Image, new RectangleF(0, 0, pbsender.Width, pbsender.Height));
        }
    }
}
