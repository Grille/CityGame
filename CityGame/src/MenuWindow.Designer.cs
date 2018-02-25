namespace CityGame
{
    partial class MenuWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.Panel();
            this.imageButton4 = new GGL.Control.ImageButton();
            this.imageButton3 = new GGL.Control.ImageButton();
            this.imageButton2 = new GGL.Control.ImageButton();
            this.imageButton1 = new GGL.Control.ImageButton();
            this.label1 = new System.Windows.Forms.Label();
            this.gameMenu = new System.Windows.Forms.Panel();
            this.imageButton13 = new GGL.Control.ImageButton();
            this.imageButton12 = new GGL.Control.ImageButton();
            this.imageButton11 = new GGL.Control.ImageButton();
            this.imageButton10 = new GGL.Control.ImageButton();
            this.label2 = new System.Windows.Forms.Label();
            this.newGame = new System.Windows.Forms.Panel();
            this.imageButton6 = new GGL.Control.ImageButton();
            this.imageButton5 = new GGL.Control.ImageButton();
            this.listBoxNewGame = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBoxNewGame = new System.Windows.Forms.PictureBox();
            this.loadGame = new System.Windows.Forms.Panel();
            this.imageButton9 = new GGL.Control.ImageButton();
            this.imageButton8 = new GGL.Control.ImageButton();
            this.imageButton7 = new GGL.Control.ImageButton();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.listBoxLoadGame = new System.Windows.Forms.ListBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.options = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button17 = new System.Windows.Forms.Button();
            this.saveGame = new System.Windows.Forms.Panel();
            this.imageButton16 = new GGL.Control.ImageButton();
            this.imageButton15 = new GGL.Control.ImageButton();
            this.imageButton14 = new GGL.Control.ImageButton();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.listBoxSaveGame = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.mainMenu.SuspendLayout();
            this.gameMenu.SuspendLayout();
            this.newGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNewGame)).BeginInit();
            this.loadGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.options.SuspendLayout();
            this.saveGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.Color.Transparent;
            this.mainMenu.Controls.Add(this.imageButton4);
            this.mainMenu.Controls.Add(this.imageButton3);
            this.mainMenu.Controls.Add(this.imageButton2);
            this.mainMenu.Controls.Add(this.imageButton1);
            this.mainMenu.Controls.Add(this.label1);
            this.mainMenu.Location = new System.Drawing.Point(9, 9);
            this.mainMenu.Margin = new System.Windows.Forms.Padding(0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(640, 400);
            this.mainMenu.TabIndex = 4;
            // 
            // imageButton4
            // 
            this.imageButton4.DefaultImage = null;
            this.imageButton4.DownImage = null;
            this.imageButton4.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton4.HoverImage = null;
            this.imageButton4.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton4.Location = new System.Drawing.Point(215, 75);
            this.imageButton4.Name = "imageButton4";
            this.imageButton4.Size = new System.Drawing.Size(200, 50);
            this.imageButton4.SwitchMode = false;
            this.imageButton4.TabIndex = 8;
            this.imageButton4.Text = "New Game";
            this.imageButton4.TextDownOffset = 1;
            this.imageButton4.Click += new System.EventHandler(this.buttonNewGameMenu_Click);
            // 
            // imageButton3
            // 
            this.imageButton3.DefaultImage = null;
            this.imageButton3.DownImage = null;
            this.imageButton3.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton3.HoverImage = null;
            this.imageButton3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton3.Location = new System.Drawing.Point(215, 131);
            this.imageButton3.Name = "imageButton3";
            this.imageButton3.Size = new System.Drawing.Size(200, 50);
            this.imageButton3.SwitchMode = false;
            this.imageButton3.TabIndex = 7;
            this.imageButton3.Text = "Load Game";
            this.imageButton3.TextDownOffset = 1;
            this.imageButton3.Click += new System.EventHandler(this.buttonLoadGameMenu_Click);
            // 
            // imageButton2
            // 
            this.imageButton2.BackColor = System.Drawing.Color.Transparent;
            this.imageButton2.DefaultImage = null;
            this.imageButton2.DownImage = null;
            this.imageButton2.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton2.HoverImage = null;
            this.imageButton2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton2.Location = new System.Drawing.Point(215, 227);
            this.imageButton2.Name = "imageButton2";
            this.imageButton2.Size = new System.Drawing.Size(200, 50);
            this.imageButton2.SwitchMode = false;
            this.imageButton2.TabIndex = 6;
            this.imageButton2.Text = "Option";
            this.imageButton2.TextDownOffset = 1;
            // 
            // imageButton1
            // 
            this.imageButton1.DefaultImage = null;
            this.imageButton1.DownImage = null;
            this.imageButton1.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton1.HoverImage = null;
            this.imageButton1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton1.Location = new System.Drawing.Point(215, 283);
            this.imageButton1.Name = "imageButton1";
            this.imageButton1.Size = new System.Drawing.Size(200, 50);
            this.imageButton1.SwitchMode = false;
            this.imageButton1.TabIndex = 5;
            this.imageButton1.Text = "Close";
            this.imageButton1.TextDownOffset = 1;
            this.imageButton1.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(634, 39);
            this.label1.TabIndex = 4;
            this.label1.Text = "Main Menu";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameMenu
            // 
            this.gameMenu.BackColor = System.Drawing.Color.Transparent;
            this.gameMenu.Controls.Add(this.imageButton13);
            this.gameMenu.Controls.Add(this.imageButton12);
            this.gameMenu.Controls.Add(this.imageButton11);
            this.gameMenu.Controls.Add(this.imageButton10);
            this.gameMenu.Controls.Add(this.label2);
            this.gameMenu.Location = new System.Drawing.Point(649, 9);
            this.gameMenu.Margin = new System.Windows.Forms.Padding(0);
            this.gameMenu.Name = "gameMenu";
            this.gameMenu.Size = new System.Drawing.Size(640, 400);
            this.gameMenu.TabIndex = 5;
            // 
            // imageButton13
            // 
            this.imageButton13.DefaultImage = null;
            this.imageButton13.DownImage = null;
            this.imageButton13.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton13.HoverImage = null;
            this.imageButton13.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton13.Location = new System.Drawing.Point(218, 283);
            this.imageButton13.Name = "imageButton13";
            this.imageButton13.Size = new System.Drawing.Size(200, 50);
            this.imageButton13.SwitchMode = false;
            this.imageButton13.TabIndex = 11;
            this.imageButton13.Text = "Main Menu";
            this.imageButton13.TextDownOffset = 1;
            this.imageButton13.Click += new System.EventHandler(this.buttonMainMenu_Click);
            // 
            // imageButton12
            // 
            this.imageButton12.DefaultImage = null;
            this.imageButton12.DownImage = null;
            this.imageButton12.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton12.HoverImage = null;
            this.imageButton12.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton12.Location = new System.Drawing.Point(218, 187);
            this.imageButton12.Name = "imageButton12";
            this.imageButton12.Size = new System.Drawing.Size(200, 50);
            this.imageButton12.SwitchMode = false;
            this.imageButton12.TabIndex = 10;
            this.imageButton12.Text = "Load Game";
            this.imageButton12.TextDownOffset = 1;
            this.imageButton12.Click += new System.EventHandler(this.buttonLoadGameMenu_Click);
            // 
            // imageButton11
            // 
            this.imageButton11.DefaultImage = null;
            this.imageButton11.DownImage = null;
            this.imageButton11.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton11.HoverImage = null;
            this.imageButton11.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton11.Location = new System.Drawing.Point(218, 131);
            this.imageButton11.Name = "imageButton11";
            this.imageButton11.Size = new System.Drawing.Size(200, 50);
            this.imageButton11.SwitchMode = false;
            this.imageButton11.TabIndex = 9;
            this.imageButton11.Text = "Save Game";
            this.imageButton11.TextDownOffset = 1;
            this.imageButton11.Click += new System.EventHandler(this.buttonSaveGameMenu_Click);
            // 
            // imageButton10
            // 
            this.imageButton10.DefaultImage = null;
            this.imageButton10.DownImage = null;
            this.imageButton10.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton10.HoverImage = null;
            this.imageButton10.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton10.Location = new System.Drawing.Point(218, 75);
            this.imageButton10.Name = "imageButton10";
            this.imageButton10.Size = new System.Drawing.Size(200, 50);
            this.imageButton10.SwitchMode = false;
            this.imageButton10.TabIndex = 9;
            this.imageButton10.Text = "Back to Game";
            this.imageButton10.TextDownOffset = 1;
            this.imageButton10.Click += new System.EventHandler(this.buttonBackToGame_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(634, 39);
            this.label2.TabIndex = 5;
            this.label2.Text = "Game Menu";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // newGame
            // 
            this.newGame.BackColor = System.Drawing.Color.Transparent;
            this.newGame.Controls.Add(this.imageButton6);
            this.newGame.Controls.Add(this.imageButton5);
            this.newGame.Controls.Add(this.listBoxNewGame);
            this.newGame.Controls.Add(this.label3);
            this.newGame.Controls.Add(this.pictureBoxNewGame);
            this.newGame.Location = new System.Drawing.Point(9, 423);
            this.newGame.Margin = new System.Windows.Forms.Padding(0);
            this.newGame.Name = "newGame";
            this.newGame.Size = new System.Drawing.Size(640, 400);
            this.newGame.TabIndex = 6;
            // 
            // imageButton6
            // 
            this.imageButton6.DefaultImage = null;
            this.imageButton6.DownImage = null;
            this.imageButton6.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton6.HoverImage = null;
            this.imageButton6.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton6.Location = new System.Drawing.Point(113, 288);
            this.imageButton6.Name = "imageButton6";
            this.imageButton6.Size = new System.Drawing.Size(200, 50);
            this.imageButton6.SwitchMode = false;
            this.imageButton6.TabIndex = 16;
            this.imageButton6.Text = "Next";
            this.imageButton6.TextDownOffset = 1;
            this.imageButton6.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // imageButton5
            // 
            this.imageButton5.DefaultImage = null;
            this.imageButton5.DownImage = null;
            this.imageButton5.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton5.HoverImage = null;
            this.imageButton5.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton5.Location = new System.Drawing.Point(319, 288);
            this.imageButton5.Name = "imageButton5";
            this.imageButton5.Size = new System.Drawing.Size(200, 50);
            this.imageButton5.SwitchMode = false;
            this.imageButton5.TabIndex = 15;
            this.imageButton5.Text = "Back";
            this.imageButton5.TextDownOffset = 1;
            this.imageButton5.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // listBoxNewGame
            // 
            this.listBoxNewGame.BackColor = System.Drawing.Color.CadetBlue;
            this.listBoxNewGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxNewGame.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.listBoxNewGame.FormattingEnabled = true;
            this.listBoxNewGame.IntegralHeight = false;
            this.listBoxNewGame.ItemHeight = 21;
            this.listBoxNewGame.Items.AddRange(new object[] {
            "erh",
            "dsh",
            "dsfh"});
            this.listBoxNewGame.Location = new System.Drawing.Point(115, 75);
            this.listBoxNewGame.Name = "listBoxNewGame";
            this.listBoxNewGame.ScrollAlwaysVisible = true;
            this.listBoxNewGame.Size = new System.Drawing.Size(198, 200);
            this.listBoxNewGame.TabIndex = 6;
            this.listBoxNewGame.SelectedIndexChanged += new System.EventHandler(this.listBoxNewGame_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(634, 39);
            this.label3.TabIndex = 4;
            this.label3.Text = "Browser";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxNewGame
            // 
            this.pictureBoxNewGame.BackColor = System.Drawing.Color.Black;
            this.pictureBoxNewGame.Location = new System.Drawing.Point(319, 75);
            this.pictureBoxNewGame.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxNewGame.Name = "pictureBoxNewGame";
            this.pictureBoxNewGame.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxNewGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxNewGame.TabIndex = 5;
            this.pictureBoxNewGame.TabStop = false;
            this.pictureBoxNewGame.Paint += new System.Windows.Forms.PaintEventHandler(this.renderMapPreview);
            // 
            // loadGame
            // 
            this.loadGame.BackColor = System.Drawing.Color.Transparent;
            this.loadGame.Controls.Add(this.imageButton9);
            this.loadGame.Controls.Add(this.imageButton8);
            this.loadGame.Controls.Add(this.imageButton7);
            this.loadGame.Controls.Add(this.textBox4);
            this.loadGame.Controls.Add(this.pictureBox2);
            this.loadGame.Controls.Add(this.label9);
            this.loadGame.Controls.Add(this.listBoxLoadGame);
            this.loadGame.Controls.Add(this.textBox6);
            this.loadGame.Controls.Add(this.label10);
            this.loadGame.Controls.Add(this.label4);
            this.loadGame.Location = new System.Drawing.Point(649, 423);
            this.loadGame.Margin = new System.Windows.Forms.Padding(0);
            this.loadGame.Name = "loadGame";
            this.loadGame.Size = new System.Drawing.Size(640, 400);
            this.loadGame.TabIndex = 5;
            // 
            // imageButton9
            // 
            this.imageButton9.DefaultImage = null;
            this.imageButton9.DownImage = null;
            this.imageButton9.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton9.HoverImage = null;
            this.imageButton9.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton9.Location = new System.Drawing.Point(114, 263);
            this.imageButton9.Name = "imageButton9";
            this.imageButton9.Size = new System.Drawing.Size(200, 50);
            this.imageButton9.SwitchMode = false;
            this.imageButton9.TabIndex = 20;
            this.imageButton9.Text = "Delete Game";
            this.imageButton9.TextDownOffset = 1;
            // 
            // imageButton8
            // 
            this.imageButton8.DefaultImage = null;
            this.imageButton8.DownImage = null;
            this.imageButton8.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton8.HoverImage = null;
            this.imageButton8.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton8.Location = new System.Drawing.Point(113, 319);
            this.imageButton8.Name = "imageButton8";
            this.imageButton8.Size = new System.Drawing.Size(200, 50);
            this.imageButton8.SwitchMode = false;
            this.imageButton8.TabIndex = 17;
            this.imageButton8.Text = "Load Game";
            this.imageButton8.TextDownOffset = 1;
            this.imageButton8.Click += new System.EventHandler(this.buttonLoadGame_Click);
            // 
            // imageButton7
            // 
            this.imageButton7.DefaultImage = null;
            this.imageButton7.DownImage = null;
            this.imageButton7.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton7.HoverImage = null;
            this.imageButton7.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton7.Location = new System.Drawing.Point(319, 319);
            this.imageButton7.Name = "imageButton7";
            this.imageButton7.Size = new System.Drawing.Size(200, 50);
            this.imageButton7.SwitchMode = false;
            this.imageButton7.TabIndex = 19;
            this.imageButton7.Text = "Back";
            this.imageButton7.TextDownOffset = 1;
            this.imageButton7.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.CadetBlue;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.textBox4.Location = new System.Drawing.Point(399, 288);
            this.textBox4.Margin = new System.Windows.Forms.Padding(0);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(120, 27);
            this.textBox4.TabIndex = 18;
            this.textBox4.Text = "1000000";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(319, 75);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 183);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.renderMapPreview);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(320, 288);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 27);
            this.label9.TabIndex = 17;
            this.label9.Text = "Capital";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxLoadGame
            // 
            this.listBoxLoadGame.BackColor = System.Drawing.Color.CadetBlue;
            this.listBoxLoadGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxLoadGame.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.listBoxLoadGame.FormattingEnabled = true;
            this.listBoxLoadGame.IntegralHeight = false;
            this.listBoxLoadGame.ItemHeight = 21;
            this.listBoxLoadGame.Items.AddRange(new object[] {
            "erh",
            "dsh",
            "dsfh"});
            this.listBoxLoadGame.Location = new System.Drawing.Point(113, 75);
            this.listBoxLoadGame.Name = "listBoxLoadGame";
            this.listBoxLoadGame.ScrollAlwaysVisible = true;
            this.listBoxLoadGame.Size = new System.Drawing.Size(200, 183);
            this.listBoxLoadGame.TabIndex = 8;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.CadetBlue;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox6.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.textBox6.Location = new System.Drawing.Point(399, 261);
            this.textBox6.Margin = new System.Windows.Forms.Padding(0);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(120, 27);
            this.textBox6.TabIndex = 16;
            this.textBox6.Text = "Neustadt";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(320, 261);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 27);
            this.label10.TabIndex = 15;
            this.label10.Text = "Name";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(634, 39);
            this.label4.TabIndex = 4;
            this.label4.Text = "Load Game";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // options
            // 
            this.options.BackColor = System.Drawing.Color.Transparent;
            this.options.Controls.Add(this.label8);
            this.options.Controls.Add(this.label7);
            this.options.Controls.Add(this.label5);
            this.options.Controls.Add(this.button17);
            this.options.Location = new System.Drawing.Point(1291, 9);
            this.options.Margin = new System.Windows.Forms.Padding(0);
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(640, 400);
            this.options.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(320, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(318, 39);
            this.label8.TabIndex = 6;
            this.label8.Text = "Game";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(318, 39);
            this.label7.TabIndex = 5;
            this.label7.Text = "Graphic";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(634, 39);
            this.label5.TabIndex = 4;
            this.label5.Text = "Options";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.Color.Green;
            this.button17.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button17.Location = new System.Drawing.Point(215, 283);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(200, 50);
            this.button17.TabIndex = 3;
            this.button17.Text = "Back";
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // saveGame
            // 
            this.saveGame.BackColor = System.Drawing.Color.Transparent;
            this.saveGame.Controls.Add(this.imageButton16);
            this.saveGame.Controls.Add(this.imageButton15);
            this.saveGame.Controls.Add(this.imageButton14);
            this.saveGame.Controls.Add(this.label14);
            this.saveGame.Controls.Add(this.textBox5);
            this.saveGame.Controls.Add(this.listBoxSaveGame);
            this.saveGame.Controls.Add(this.label6);
            this.saveGame.Location = new System.Drawing.Point(1291, 423);
            this.saveGame.Margin = new System.Windows.Forms.Padding(0);
            this.saveGame.Name = "saveGame";
            this.saveGame.Size = new System.Drawing.Size(640, 400);
            this.saveGame.TabIndex = 8;
            // 
            // imageButton16
            // 
            this.imageButton16.DefaultImage = null;
            this.imageButton16.DownImage = null;
            this.imageButton16.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton16.HoverImage = null;
            this.imageButton16.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton16.Location = new System.Drawing.Point(364, 75);
            this.imageButton16.Name = "imageButton16";
            this.imageButton16.Size = new System.Drawing.Size(200, 50);
            this.imageButton16.SwitchMode = false;
            this.imageButton16.TabIndex = 22;
            this.imageButton16.Text = "Delete Game";
            this.imageButton16.TextDownOffset = 1;
            // 
            // imageButton15
            // 
            this.imageButton15.DefaultImage = null;
            this.imageButton15.DownImage = null;
            this.imageButton15.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton15.HoverImage = null;
            this.imageButton15.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton15.Location = new System.Drawing.Point(365, 131);
            this.imageButton15.Name = "imageButton15";
            this.imageButton15.Size = new System.Drawing.Size(200, 50);
            this.imageButton15.SwitchMode = false;
            this.imageButton15.TabIndex = 12;
            this.imageButton15.Text = "Save Game";
            this.imageButton15.TextDownOffset = 1;
            this.imageButton15.Click += new System.EventHandler(this.buttonSaveGame_Click);
            // 
            // imageButton14
            // 
            this.imageButton14.DefaultImage = null;
            this.imageButton14.DownImage = null;
            this.imageButton14.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.imageButton14.HoverImage = null;
            this.imageButton14.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageButton14.Location = new System.Drawing.Point(364, 281);
            this.imageButton14.Name = "imageButton14";
            this.imageButton14.Size = new System.Drawing.Size(200, 50);
            this.imageButton14.SwitchMode = false;
            this.imageButton14.TabIndex = 21;
            this.imageButton14.Text = "Back";
            this.imageButton14.TextDownOffset = 1;
            this.imageButton14.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(365, 188);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 27);
            this.label14.TabIndex = 21;
            this.label14.Text = "Name";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.CadetBlue;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.textBox5.Location = new System.Drawing.Point(444, 188);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(120, 27);
            this.textBox5.TabIndex = 15;
            this.textBox5.Text = "Neustadt";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // listBoxSaveGame
            // 
            this.listBoxSaveGame.BackColor = System.Drawing.Color.CadetBlue;
            this.listBoxSaveGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxSaveGame.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.listBoxSaveGame.FormattingEnabled = true;
            this.listBoxSaveGame.IntegralHeight = false;
            this.listBoxSaveGame.ItemHeight = 21;
            this.listBoxSaveGame.Items.AddRange(new object[] {
            "erh",
            "dsh",
            "dsfh"});
            this.listBoxSaveGame.Location = new System.Drawing.Point(107, 75);
            this.listBoxSaveGame.Name = "listBoxSaveGame";
            this.listBoxSaveGame.Size = new System.Drawing.Size(190, 256);
            this.listBoxSaveGame.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(634, 39);
            this.label6.TabIndex = 4;
            this.label6.Text = "Save Game";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            this.timer.Interval = 50;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MenuWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(192)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1932, 1092);
            this.Controls.Add(this.saveGame);
            this.Controls.Add(this.options);
            this.Controls.Add(this.loadGame);
            this.Controls.Add(this.newGame);
            this.Controls.Add(this.gameMenu);
            this.Controls.Add(this.mainMenu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuWindow";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.mainMenu.ResumeLayout(false);
            this.gameMenu.ResumeLayout(false);
            this.newGame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNewGame)).EndInit();
            this.loadGame.ResumeLayout(false);
            this.loadGame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.options.ResumeLayout(false);
            this.saveGame.ResumeLayout(false);
            this.saveGame.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel mainMenu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel gameMenu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel newGame;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel loadGame;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel options;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Panel saveGame;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBoxNewGame;
        private System.Windows.Forms.ListBox listBoxNewGame;
        private System.Windows.Forms.ListBox listBoxLoadGame;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ListBox listBoxSaveGame;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label10;
        private GGL.Control.ImageButton imageButton1;
        private GGL.Control.ImageButton imageButton4;
        private GGL.Control.ImageButton imageButton3;
        private GGL.Control.ImageButton imageButton2;
        private GGL.Control.ImageButton imageButton6;
        private GGL.Control.ImageButton imageButton5;
        private GGL.Control.ImageButton imageButton9;
        private GGL.Control.ImageButton imageButton8;
        private GGL.Control.ImageButton imageButton7;
        private GGL.Control.ImageButton imageButton13;
        private GGL.Control.ImageButton imageButton12;
        private GGL.Control.ImageButton imageButton11;
        private GGL.Control.ImageButton imageButton10;
        private GGL.Control.ImageButton imageButton16;
        private GGL.Control.ImageButton imageButton15;
        private GGL.Control.ImageButton imageButton14;
        private System.Windows.Forms.Label label14;
    }
}