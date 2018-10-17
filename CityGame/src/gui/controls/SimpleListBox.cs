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
    [System.ComponentModel.DesignerCategory("code")]
    public class SimpleListBox : WF.Control
    {
        public event EventHandler ChangeItem;

        private string[] itemsText;
        private int[] itemsValue;
        private int itemIndex;
        private int hoverIndex;
        private int textDownOffset;

        [Browsable(true)]
        public int SelectetIndex { get; set; } = -1;

        [Browsable(true)]
        public int ItemHeight { get; set; }

        [Browsable(true)]
        public Color ForeSelectColor { get; set; }

        [Browsable(true)]
        public Color BackSelectColor { get; set; }

        public SimpleListBox()
        {
            InitializeComponent();
            itemsText = new string[128];
            itemsValue = new int[128];
            itemIndex = 0;
            ItemHeight = 20;
            textDownOffset = 0;
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ListBox
            // 
            this.Name = "ListBox";
            this.ResumeLayout(false);

        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int newIndex = e.Y / ItemHeight;
            if (newIndex != hoverIndex)
            {
                hoverIndex = newIndex;
                this.Invalidate();
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int newIndex = e.Y / ItemHeight;
            if (newIndex != SelectetIndex && itemsValue[newIndex] != -1)
            {
                SelectetIndex = newIndex;
                if (ChangeItem != null) ChangeItem.Invoke(this, new EventArgs());
                this.Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            for (int i = 0; i < itemIndex; i++)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                if (i != SelectetIndex)
                    g.DrawString(itemsText[i], base.Font, new SolidBrush(base.ForeColor), new RectangleF(0, ItemHeight * i, Width, ItemHeight), sf);
                else {
                    g.FillRectangle(new SolidBrush(BackSelectColor), new RectangleF(0, ItemHeight * i, Width, ItemHeight));
                    g.DrawString(itemsText[i], base.Font, new SolidBrush(ForeSelectColor), new RectangleF(textDownOffset, ItemHeight * i + textDownOffset, Width + +textDownOffset, ItemHeight + +textDownOffset), sf);
                }
            }
            //else
            //{
            //    g.DrawRectangle(Pens.Black, new Rectangle(1, 1, this.Width-2, this.Height-2));
            //}
        }

        public void Clear()
        {
            SelectetIndex = -1;
            itemIndex = 0;
        }
        public void Add(string text)
        {
            itemsText[itemIndex] = text;
            itemsValue[itemIndex] = -1;
            itemIndex++;
        }
        public int getValue()
        {
            return itemsValue[SelectetIndex];
        }
        public void Add(string text,int value)
            {
                itemsText[itemIndex] = text;
                itemsValue[itemIndex] = value;
                itemIndex++;
            }
        public int ItemsLenght()
        {
            return itemIndex;
        }
        public void HeightToContent()
        {
            this.Height = itemIndex * ItemHeight;
        }

    }
}
