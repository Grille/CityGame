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
    public class ButtonList : WF.Control
    {
        public event EventHandler ChangeItem;

        private string[] itemsText;
        public Color UsedBackColor;
        public Color UsedForeColor;
        private Color[] itemsBackColor;
        private Color[] itemsForeColor;
        private object[] itemsValue;
        private int itemIndex;
        private int textDownOffset;
        private Bitmap processedButtonImage, processedButtonDownImage;

        [Browsable(true)]
        public Image ButtonDownBorderImage { get; set; } = null;
        [Browsable(true)]
        public Image ButtonBorderImage { get; set; } = null;
        [Browsable(true)]
        public Image BorderImage { get; set; } = null;
        [Browsable(true)]
        public int BorderSize { get; set; } = 8;
        [Browsable(true)]
        public int SelectetIndex { get; set; } = -1;
        [Browsable(true)]
        public int ItemDistance { get; set; }
        [Browsable(true)]
        public int ItemHeight { get; set; }
        [Browsable(true)]
        public Color ForeSelectColor { get; set; }
        [Browsable(true)]
        public Color BackSelectColor { get; set; }

        public ButtonList()
        {
            UsedForeColor = base.ForeColor;
            DoubleBuffered = true;
            itemsText = new string[128];
            itemsValue = new object[128];
            itemsBackColor = new Color[128];
            itemsForeColor = new Color[128];
            itemIndex = 0;
            ItemHeight = 20;
            textDownOffset = 1;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int newIndex = (e.Y-4) / (ItemHeight+ ItemDistance);
            /*
            if (newIndex != hoverIndex)
            {
                hoverIndex = newIndex;
                this.Invalidate();
            }
            */
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int newIndex = (e.Y - 4) / (ItemHeight + ItemDistance);
            if (newIndex != SelectetIndex && itemsValue[newIndex] != null)
            {
                SelectetIndex = newIndex;
                if (ChangeItem != null) ChangeItem.Invoke(this, new EventArgs());
                this.Invalidate();
            }
        }

        private void processButtonImage()
        {
            int width = Width - 8, height = ItemHeight;
            processedButtonImage = new Bitmap(width, height);
            processedButtonDownImage = new Bitmap(width, height);
            if (ButtonBorderImage != null)
                processImage(Graphics.FromImage(processedButtonImage), ButtonBorderImage, width, height);
            if (ButtonDownBorderImage != null)
                processImage(Graphics.FromImage(processedButtonDownImage), ButtonDownBorderImage, width, height);

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            
            Rectangle hurrig = new Rectangle(BorderSize, BorderSize, this.Width - BorderSize * 2, this.Height - BorderSize * 2);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            if (BorderImage == null) g.DrawRectangle(Pens.Black, new Rectangle(1, 1, this.Width - 2, this.Height - 2));
            else processImage(g, BorderImage, Width, Height);

            processButtonImage();
            for (int i = 0; i < itemIndex; i++)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.FillRectangle(new SolidBrush(itemsBackColor[i]), new RectangleF(4, (ItemHeight + ItemDistance) * i + 4, Width - 8, ItemHeight));
                if (i != SelectetIndex)
                {
                    g.DrawString(itemsText[i], base.Font, new SolidBrush(itemsForeColor[i]), new RectangleF(0, (ItemHeight + ItemDistance) * i + 4, Width, ItemHeight), sf);
                    if (itemsBackColor[i] != Color.Transparent)
                        g.DrawImage(processedButtonImage, new Point(4, (ItemHeight + ItemDistance) * i + 4));
                }
                else
                {
                    g.DrawString(itemsText[i], base.Font, new SolidBrush(itemsForeColor[i]), new RectangleF(textDownOffset, (ItemHeight + ItemDistance) * i + textDownOffset + 4, Width + +textDownOffset, ItemHeight + +textDownOffset), sf);
                    if (itemsBackColor[i] != Color.Transparent)
                        g.DrawImage(processedButtonDownImage, new Point(4, (ItemHeight + ItemDistance) * i + 4));
                }
            }
            
            
            //else
            //{
            //    g.DrawRectangle(Pens.Black, new Rectangle(1, 1, this.Width-2, this.Height-2));
            //}
        }

        private void processImage(Graphics g, Image src, int width, int height)
        {
            int bSize = src.Width / 3;

            for (int iy = 0; iy < 3; iy++)
            {
                for (int ix = 0; ix < 3; ix++)
                {
                    Rectangle dstRect, srcRect = new Rectangle(bSize*ix, bSize*iy, bSize, bSize);
                    switch (ix + iy * 3)
                    {
                        case 0: dstRect = new Rectangle(0, 0, bSize, bSize); break;
                        case 1: dstRect = new Rectangle(bSize, 0, width - bSize * 2, bSize); break;
                        case 2: dstRect = new Rectangle(width - bSize, 0, bSize, bSize); break;

                        case 3: dstRect = new Rectangle(0, bSize, bSize, Height - bSize*2+1); break;

                        case 4: dstRect = new Rectangle(bSize, bSize, width-bSize, Height - bSize*2+1); break;

                        case 5: dstRect = new Rectangle(width - bSize, bSize, bSize, height - bSize*2+1); break;

                        case 6: dstRect = new Rectangle(0, height - bSize, bSize, bSize); break;
                        case 7: dstRect = new Rectangle(bSize, height - bSize, width - bSize * 2, bSize); break;
                        case 8: dstRect = new Rectangle(width - bSize, height - bSize, bSize, bSize); break;

                        default: dstRect = new Rectangle(0, 0, 0, 0); break;

                    }
                    g.DrawImage(src, dstRect, srcRect,GraphicsUnit.Point);

                }
            }
        }

        public void Clear()
        {
            SelectetIndex = -1;
            itemIndex = 0;
        }
        public object SelectetValue{get => itemsValue[SelectetIndex];}
        public void Add(string text)
        {
            Add(text, null);
        }
        public void Add(string text, object value)
        {
            itemsText[itemIndex] = text;
            itemsValue[itemIndex] = value;
            itemsBackColor[itemIndex] = UsedBackColor;
            itemsForeColor[itemIndex] = UsedForeColor;
            itemIndex++;
        }
        public int ItemsLenght()
        {
            return itemIndex;
        }
        public void HeightToContent()
        {
            this.Height = (itemIndex * (ItemHeight + ItemDistance)) + 8 - ItemDistance;
            Refresh();
        }
    }
}
