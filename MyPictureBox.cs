using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ColorCount
{
    public partial class MyPictureBox : UserControl
    {
        private float zoomLevel = 1;
        private int offsetX = 0;
        private int offsetY = 0;
        private int prevX = 0;
        private int prevY = 0;
        private readonly float zoomC = 1;
        private float zoomIncrement = 1;

        private Rectangle srcRect, destRect;

        private Image image;
        private string imagePath;
        private bool drag = false;
        //private bool zoomIn = true;
        //private bool zoomOut = false;
        //private bool firstZoom = true;
        //private Image originalImage;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            destRect = new Rectangle(0, 0, this.Width, this.Height);
            Invalidate();
        }

        private int width, height;
        public Image Image { get { return image; } 
            set {
                //this.pictureBox1.Image = value;
                image = value;
                if (value != null)
                {
                    srcRect = new Rectangle(0, 0, value.Width, value.Height);
                    destRect = new Rectangle(0, 0, this.Width, this.Height);
                    zoomIncrement = zoomC * value.Width / this.Width / 10;
                   // zoomLevel = startZoom;
                    update();
                    Invalidate();
                }
            } }

        //public int Width { get { return pictureBox1.Size.Width; } set { this.width = value; } }
        //public int Height { get { return pictureBox1.Size.Height; } set { this.height = value; } }
        public string ImageLocation
        {
            get { return this.imagePath; }
            set
            {
                this.imagePath = value;
                if (value != null)
                {
                    
                    Bitmap b = new Bitmap(value);
                    this.Image = new Bitmap(b, this.Size);
                }

            }
        }
        private Color ignoredColor;
        private GraphicsUnit graphicsUnit = GraphicsUnit.Display;

        public event Action<Color> IgnoredColorChange;

        bool eyedropper = false;

        public bool Eyedropper 
        { 
            get 
            { 
                return this.eyedropper; 
            } 
            set 
            { 
                this.eyedropper = value; 
                if (value) 
                    this.Cursor = new Cursor(new MemoryStream(Properties.Resources.Eyedropper)); 
                else 
                    this.Cursor = Cursors.Default; 
            } 
        }
        private bool pan = false;
        public bool Pan
        {
            get
            {
                return this.pan;
            }
            set
            {
                this.pan = value;
                if (value)
                    this.Cursor = new Cursor(new MemoryStream(Properties.Resources.pan));
                else
                    this.Cursor = Cursors.Default;
            }
        }
        public MyPictureBox()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Image == null)
                return;
            Graphics g = e.Graphics;
            g.DrawImage(Image, destRect, srcRect, GraphicsUnit.Pixel);

        }
        private void move(int x, int y)
        {
            offsetX -= x;
            offsetY -= y;
            Debug.WriteLine("x: "+offsetX+" y:"+offsetY+" SR:"+srcRect.Right+" SL:"+srcRect.Left+" ST:"+srcRect.Top+" SB:"+srcRect.Bottom);
            update();
            Invalidate();
        }
        private void zoom(Point point,float level)
        {
            float increment = zoomIncrement * level;
            zoomLevel += increment;
            if (zoomLevel < 1)
                zoomLevel = 1;
            if (zoomLevel > zoomIncrement * 100)
                zoomLevel = zoomIncrement * 100;
            if (zoomLevel > 1)
                Pan = true;
            else
                Pan = false;
            
           // move(x, y);
            update();

            Invalidate();
        }
        private void update()
        {
            int l = 0, t = 0, w = 0, h = 0;

            if (offsetX < 0)
                offsetX = 0;
            if (offsetY < 0)
                offsetY = 0;
            

            l = offsetX;
            t = offsetY;
            w = (int)(image.Width / zoomLevel);
            if ((offsetX + w) > image.Width)
                offsetX = image.Width - w;
            h = (int)(image.Height / zoomLevel);
            if (offsetY + h > image.Height)
                offsetY = image.Height - h;
            srcRect = new Rectangle(l, t,w, h);
        }
        private void MyPictureBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Keys.Up:
            //        zoom(0.001f);
            //        break;
            //    case Keys.Down:
            //        zoom(-0.001f);
            //        break;
            //}
        }

   
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            //move(e.Delta, 0);
            if (e.Delta > 0)
                zoom(e.Location,1);
            else
                zoom(e.Location,-1);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if(drag)
            {
                //Debug.WriteLine(e.X - prevX);
                move(e.X - prevX, e.Y - prevY);
                prevX = e.X;
                prevY = e.Y;
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (pan)
            {
                drag = true;
                prevX = e.X;
                prevY = e.Y;
            }

        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            drag = false;
            if(eyedropper)
            {
                Point p = Cursor.Position;
                MouseEventArgs m = e as MouseEventArgs;
                Bitmap b = new Bitmap(destRect.Width, destRect.Height);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.DrawImage(this.Image, destRect, srcRect, GraphicsUnit.Pixel);
                    ignoredColor = b.GetPixel(m.X, m.Y);
                }

                // Debug.WriteLine(p);
                //Debug.WriteLine(pictureBox1.Image.GetBounds(ref graphicsUnit));
                //ignoredColor = b.GetPixel(m.X, m.Y);
                if (IgnoredColorChange != null)
                    IgnoredColorChange(ignoredColor);
                Eyedropper = false;
               // b.Dispose();
                //c.Dispose();
                //d.Dispose();
            }
        }
    
   
    }
}
