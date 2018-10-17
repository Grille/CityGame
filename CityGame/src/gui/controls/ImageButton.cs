using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WF = System.Windows.Forms;
using System.Media;

namespace CityGame.Control
{
    /// <summary>
    /// Image rendered button
    /// </summary>
    /// 
    [System.ComponentModel.DesignerCategory("code"), DefaultEvent("Click")]
    public class ImageButton : WF.Control
    {
        public object Value;
        public event EventHandler ButtonDown;
        public event EventHandler ButtonUp;

        private bool switchMode;
        private bool buttenDown;
        private String text;
        private int mode = 0;

        [Description("default image"), Browsable(true)]
        public Image DefaultImage { get; set; }
        [Description("mouse hover image"), Browsable(true)]
        public Image HoverImage { get; set; }
        [Description("moude down image"), Browsable(true)]
        public Image DownImage { get; set; }

        [Description("Offset of the text when pressed"), Browsable(true)]
        public int TextDownOffset { get; set; }
        [Description("Offset of the text when pressed"), Browsable(true)]
        public bool SwitchMode
        {
            get { return switchMode; }
            set { buttenDown = false; switchMode = value; }
        }

        [Browsable(true),DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),EditorBrowsable(EditorBrowsableState.Always),Bindable(true)]
        public override String Text
        {
            get { return text; }
            set
            {
                text = value;
                //if (DesignMode)
                    Invalidate();
            }
        }
        
        public ImageButton()
        {
            //TextAligen = ToolBarTextAlign.Right
            TextDownOffset = 1;
            switchMode = false;
            buttenDown = false;
            this.ImeMode = ImeMode.On;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
             ControlStyles.UserPaint |
             ControlStyles.AllPaintingInWmPaint, true);
        }
        public void LoadImages(Image defaultImage, Image downImage)
        {
            this.DefaultImage = defaultImage;
            this.DownImage = downImage;
        }
        public void LoadImages(Image defaultImage, Image hoverImage, Image downImage)
        {
            this.DefaultImage = defaultImage;
            this.HoverImage = hoverImage;
            this.DownImage = downImage;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            mode = 1;
            this.Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mode = 0;
            this.Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mode = 2;

            base.OnMouseClick(e);
            if (switchMode)
            {
                buttenDown = !buttenDown;
                if (buttenDown == true && ButtonDown != null) ButtonDown.Invoke(this, new EventArgs());
                else if (ButtonUp != null) ButtonUp.Invoke(this, new EventArgs());
            }

            this.Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mode = 1;
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            //base.OnPaint(e);

            System.Drawing.Drawing2D.GraphicsContainer cstate = e.Graphics.BeginContainer();
            e.Graphics.TranslateTransform(-this.Left, -this.Top);
            Rectangle clip = e.ClipRectangle;
            clip.Offset(this.Left, this.Top);
            PaintEventArgs pe = new PaintEventArgs(e.Graphics, clip);

            //paint the container's bg
            InvokePaintBackground(this.Parent, pe);
            //paints the container fg
            InvokePaint(this.Parent, pe);
            //restores graphics to its original state
            e.Graphics.EndContainer(cstate);

            Graphics g = e.Graphics;
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            Image image = HoverImage;
            if (mode == 2 || buttenDown) image = DownImage;
            else if (HoverImage == null || mode == 0) image = DefaultImage;
            else if (mode == 1) image = HoverImage;

            if (image != null) g.DrawImage(image, new Rectangle(0, 0, this.Width, this.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            else g.DrawRectangle(Pens.Black, new Rectangle(1, 1, this.Width-2, this.Height-2));

            if (text != "") {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                int add = 0;
                if (mode == 2 || buttenDown) add = TextDownOffset;
                g.DrawString(text, base.Font, new SolidBrush(base.ForeColor), new RectangleF(add, add, Width + add, Height + add), sf);
            }
        }

        public void ResetButton()
        {
            if (switchMode && buttenDown == true) {
                if (ButtonUp != null) ButtonUp.Invoke(this, new EventArgs());
                buttenDown = false;
                this.Invalidate();
            }
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ImageButton
            // 
            this.Name = "ImageButton";
            this.Size = new System.Drawing.Size(120, 114);
            this.ResumeLayout(false);

        }
    }
}
