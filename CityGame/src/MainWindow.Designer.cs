namespace CityGame
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.timerRender = new System.Windows.Forms.Timer(this.components);
            this.progressBarLoad = new System.Windows.Forms.ProgressBar();
            this.pictureBoxLoad = new System.Windows.Forms.PictureBox();
            this.timerLogic = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // timerRender
            // 
            this.timerRender.Interval = 5;
            this.timerRender.Tick += new System.EventHandler(this.timerRender_Tick);
            // 
            // progressBarLoad
            // 
            this.progressBarLoad.ForeColor = System.Drawing.Color.Lime;
            this.progressBarLoad.Location = new System.Drawing.Point(43, 95);
            this.progressBarLoad.Name = "progressBarLoad";
            this.progressBarLoad.Size = new System.Drawing.Size(98, 45);
            this.progressBarLoad.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarLoad.TabIndex = 4;
            this.progressBarLoad.Value = 2;
            // 
            // pictureBoxLoad
            // 
            this.pictureBoxLoad.Location = new System.Drawing.Point(43, 40);
            this.pictureBoxLoad.Name = "pictureBoxLoad";
            this.pictureBoxLoad.Size = new System.Drawing.Size(98, 52);
            this.pictureBoxLoad.TabIndex = 5;
            this.pictureBoxLoad.TabStop = false;
            // 
            // timerLogic
            // 
            this.timerLogic.Interval = 5;
            this.timerLogic.Tick += new System.EventHandler(this.timerLogic_Tick);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Location = new System.Drawing.Point(147, 40);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(98, 52);
            this.pictureBoxLogo.TabIndex = 6;
            this.pictureBoxLogo.TabStop = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1034, 425);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.progressBarLoad);
            this.Controls.Add(this.pictureBoxLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainWindow";
            this.Text = "CityGame";
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            this.MouseLeave += new System.EventHandler(this.MainWindow_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBarLoad;
        private System.Windows.Forms.PictureBox pictureBoxLoad;
        public System.Windows.Forms.Timer timerRender;
        public System.Windows.Forms.Timer timerLogic;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
    }
}