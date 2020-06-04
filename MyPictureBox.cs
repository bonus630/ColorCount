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
        //private int zoomLevel = 0;
        //private bool zoomIn = true;
        //private bool zoomOut = false;
        //private bool firstZoom = true;
        //private Image originalImage;



        private int width, height;
        public Image Image { get { return this.pictureBox1.Image; } set {
                this.pictureBox1.Image = value; } }
        public int Width { get { return pictureBox1.Size.Width; } set { this.width = value; } }
        public int Height { get { return pictureBox1.Size.Height; } set { this.height = value; } }
        public string ImageLocation { get { return this.pictureBox1.ImageLocation; } set {
                this.pictureBox1.ImageLocation = value; } }
        private Color ignoredColor;
        private GraphicsUnit graphicsUnit = GraphicsUnit.Display;

        public event Action<Color> IgnoredColorChange;

        bool eyedropper = false;

        public bool Eyedropper { get { return this.eyedropper; } set { this.eyedropper = value; if (value) pictureBox1.Cursor = new Cursor(new MemoryStream(Properties.Resources.Eyedropper)); else pictureBox1.Cursor = Cursors.Default; } }



        public MyPictureBox()
        {
            InitializeComponent();
           
        }

       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (eyedropper)
            {
                Point p = Cursor.Position;
                Bitmap b = new Bitmap(pictureBox1.Image, pictureBox1.Size);

                MouseEventArgs m = e as MouseEventArgs;

                Debug.WriteLine(p);
                Debug.WriteLine(pictureBox1.Image.GetBounds(ref graphicsUnit));
                ignoredColor = b.GetPixel(m.X, m.Y);
                if (IgnoredColorChange != null)
                    IgnoredColorChange(ignoredColor);
                Eyedropper = false;
            }
            //if (zoomIn)
            //{
            //    zoomLevel++;
            //    MouseEventArgs m = e as MouseEventArgs;
            //    Zoom(m.Location);
            //}
        }
        //private void Zoom(Point point)
        //{
        //    if (firstZoom)
        //    {
        //        originalImage = pictureBox1.Image;
        //        firstZoom = false;
        //    }
        //    Bitmap bitmap = new Bitmap(originalImage.Width + (originalImage.Width * zoomLevel / 100), originalImage.Height + (originalImage.Height * zoomLevel / 100));
        //    Graphics g = Graphics.FromImage(bitmap);
        //    Rectangle dest = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
        //    int zoomSize = 10 * zoomLevel;
        //    int newWidth = bitmap.Width - 2*zoomSize;
        //    int newHeight = bitmap.Height - 2 * zoomSize;
        //    if (newWidth < 0) {
        //        newWidth = 1;
        //        zoomLevel--;
        //            }
        //    if (newHeight < 0)
        //    {
        //        newHeight = 1;
        //        zoomLevel--;
        //    }

        //    Rectangle source = new Rectangle(zoomSize,zoomSize,newWidth,newHeight);
        //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //    g.DrawImage(originalImage, 0,0, source, GraphicsUnit.Pixel);
        //    pictureBox1.Image = bitmap;
        //}
    }
}
