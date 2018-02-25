using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

using GGL.Control;


namespace CityGame
{
    public partial class MenuOverlay : Form
    {
        public MenuOverlay()
        {

            InitializeComponent();
            imageButton1.LoadImages(new Bitmap("../Data/texture/gui/terrain1.png"),new Bitmap("../Data/texture/gui/terrain3.png"));
            imageButton2.LoadImages(new Bitmap("../Data/texture/gui/traffic1.png"),new Bitmap("../Data/texture/gui/traffic3.png"));
            imageButton3.LoadImages(new Bitmap("../Data/texture/gui/supply1.png"), new Bitmap("../Data/texture/gui/supply3.png"));
            imageButton4.LoadImages(new Bitmap("../Data/texture/gui/zones1.png"), new Bitmap("../Data/texture/gui/zones3.png"));
            imageButton5.LoadImages(new Bitmap("../Data/texture/gui/empty1.png"), new Bitmap("../Data/texture/gui/empty3.png"));
            imageButton6.LoadImages(new Bitmap("../Data/texture/gui/empty1.png"), new Bitmap("../Data/texture/gui/empty3.png"));

            listBox.BackColor = Color.FromArgb(99, 139, 139);
            listBox.ItemHeight = 24;
            listBox.ItemDistance = 4;
            this.DoubleBuffered = true;
            //listBox1.BackSelectColor = Color.FromArgb(0, 0, 0);
        }

        private void imageButton1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void imageButton1_ButtonDown(object sender, EventArgs e)
        {
            imageButton2.ResetButton();
            imageButton3.ResetButton();
            imageButton4.ResetButton();
            imageButton5.ResetButton();
            imageButton6.ResetButton();

            ImageButton senderIB = ((ImageButton)(sender));

            listBox.Location = new Point(senderIB.Location.X- listBox.Size.Width-16, senderIB.Location.Y);
            listBox.Visible = true;
            listBox.Clear();
            listBox.UseColor(Color.FromArgb(0));
            listBox.Add(" - tools - ");
            listBox.UseColor(Color.FromArgb(115, 207, 92));
            listBox.Add("demolish",0);
            listBox.UseColor(Color.FromArgb(0));
            listBox.Add(" - Tree - ");
            listBox.UseColor(Color.FromArgb(115, 207, 92));
            listBox.Add("conifer",3);
            listBox.Add("deciduous",4);
            listBox.Add("palm",5);
            listBox.HeightToContent();
        }
        private void imageButton2_ButtonDown(object sender, EventArgs e)
        {
            imageButton1.ResetButton();
            imageButton3.ResetButton();
            imageButton4.ResetButton();
            imageButton5.ResetButton();
            imageButton6.ResetButton();

            ImageButton senderIB = ((ImageButton)(sender));

           
            listBox.Location = new Point(senderIB.Location.X - listBox.Size.Width - 16, senderIB.Location.Y);
            listBox.Visible = true;
            listBox.Clear();
            listBox.UseColor(Color.FromArgb(0));
            listBox.Add(" - Road - ");
            listBox.UseColor(Color.FromArgb(217, 142, 242));
            listBox.Add("dirt way",21);
            listBox.Add("small road",22);
            listBox.Add("medium road",23);
            listBox.Add("large road",24);
            listBox.HeightToContent();
        }
        private void imageButton3_ButtonDown(object sender, EventArgs e)
        {
            imageButton1.ResetButton();
            imageButton2.ResetButton();
            imageButton4.ResetButton();
            imageButton5.ResetButton();
            imageButton6.ResetButton();

            ImageButton senderIB = ((ImageButton)(sender));

            listBox.Location = new Point(senderIB.Location.X - listBox.Size.Width - 16, senderIB.Location.Y);
            listBox.Visible = true;
            listBox.Clear();
            listBox.UseColor(Color.FromArgb(0));
            listBox.Add(" - Energy - ");
            listBox.UseColor(Color.FromArgb(237, 232, 137));
            listBox.Add("coal power plant",41);
            listBox.Add("gas power plant", 42);
            listBox.Add("nuclear power plant", 43);
            listBox.Add("solar power plant", 44);
            listBox.Add("wind power plant", 45);
            listBox.UseColor(Color.FromArgb(0));
            listBox.Add(" - Water - ");
            listBox.UseColor(Color.FromArgb(125, 143, 232));
            listBox.Add("water pump", 51);
            listBox.Add("water tower", 53);
            listBox.Add("sewage plant", 55);
            listBox.UseColor(Color.FromArgb(0));
            listBox.Add(" - Disposal - ");
            listBox.UseColor(Color.FromArgb(198, 166, 113));
            listBox.Add("landfill", 58);
            listBox.Add("incinerator", 60);
            listBox.HeightToContent();
        }
        private void imageButton4_ButtonDown(object sender, EventArgs e)
        {
            imageButton1.ResetButton();
            imageButton2.ResetButton();
            imageButton3.ResetButton();
            imageButton5.ResetButton();
            imageButton6.ResetButton();

            ImageButton senderIB = ((ImageButton)(sender));

            listBox.Location = new Point(senderIB.Location.X - listBox.Size.Width - 16, senderIB.Location.Y);
            listBox.Visible = true;
            listBox.Clear();
            listBox.UseColor(Color.FromArgb(0));
            listBox.Add(" - Residential - ");
            listBox.UseColor(Color.FromArgb(148, 237, 137));
            listBox.Add("Light residential",61);
            listBox.Add("Medium residential",62);
            listBox.Add("Dense residential",63);
            listBox.UseColor(Color.FromArgb(0));
            listBox.Add(" - Comercial - ");
            listBox.UseColor(Color.FromArgb(137, 198, 237));
            listBox.Add("Light comercial",71);
            listBox.Add("Medium comercial",72);
            listBox.Add("Dense comercial",75);
            listBox.UseColor(Color.FromArgb(0));
            listBox.Add(" - Industrieal - ");
            listBox.UseColor(Color.FromArgb(237, 195, 137));
            listBox.Add("Light industrial",81);
            listBox.Add("Medium industrial",84);
            listBox.Add("Dense industrial",85);
            listBox.HeightToContent();
        }
        private void imageButton5_ButtonDown(object sender, EventArgs e)
        {
            imageButton1.ResetButton();
            imageButton2.ResetButton();
            imageButton3.ResetButton();
            imageButton4.ResetButton();
            imageButton6.ResetButton();

            ImageButton senderIB = ((ImageButton)(sender));

            listBox.Location = new Point(senderIB.Location.X - listBox.Size.Width - 16, senderIB.Location.Y);
            listBox.Visible = true;
            listBox.Clear();
            listBox.UseColor(Color.FromArgb(0));
            listBox.Add(" - Safety - ");
            listBox.UseColor(Color.FromArgb(237, 137, 153));
            listBox.Add("small fire department",91);
            listBox.Add("large fire department",92);
            listBox.UseColor(Color.FromArgb(0));
            listBox.Add(" - Safety - ");
            listBox.UseColor(Color.FromArgb(137, 155, 237));
            listBox.Add("small police department",93);
            listBox.Add("large police department",94);
            listBox.Add("hospital", 102);
            listBox.HeightToContent() ;
        }
        private void imageButton6_ButtonDown(object sender, EventArgs e)
        {
            imageButton1.ResetButton();
            imageButton2.ResetButton();
            imageButton3.ResetButton();
            imageButton4.ResetButton();
            imageButton5.ResetButton();

            ImageButton senderIB = ((ImageButton)(sender));

            listBox.Location = new Point(senderIB.Location.X - listBox.Size.Width - 16, senderIB.Location.Y);
            listBox.Visible = true;
            listBox.Clear();
            listBox.UseColor(Color.FromArgb(0));
            listBox.Add(" - Empty - ");
            listBox.HeightToContent();
        }


        private void imageButton1_ButtonUp(object sender, EventArgs e){listBox.Visible = false;}
        private void imageButton2_ButtonUp(object sender, EventArgs e){listBox.Visible = false;}
        private void imageButton3_ButtonUp(object sender, EventArgs e){listBox.Visible = false;}
        private void imageButton4_ButtonUp(object sender, EventArgs e){listBox.Visible = false;}
        private void imageButton6_ButtonUp(object sender, EventArgs e){listBox.Visible = false;}
        private void imageButton5_ButtonUp(object sender, EventArgs e){listBox.Visible = false;}

        private void listBox1_ChangeItem(object sender, EventArgs e)
        {
            GGL.Control.ListBox senderIB = ((GGL.Control.ListBox)(sender));
            Program.MainWindow.CurBuild = senderIB.getValue();
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


    }
}
