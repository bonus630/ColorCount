using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using  p = Photoshop;

namespace ColorCount
{
    public partial class Form1 : Form
    {
        private Dictionary<Color, int> colors = new Dictionary<Color, int>();
        p.Application app;

        public Form1()
        {
            InitializeComponent();
           // app.ActiveDocument.ColorSamplers
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeColor();
        }
        public void ChangeColor()
        {
            Bitmap bmp = (Bitmap)Image.FromFile("C:\\Users\\Reginaldo\\Desktop\\BANDEIRA MOANA TAQUEADA .jpg");
           
            LockBitmap lockBitmap = new LockBitmap(bmp);
            lockBitmap.LockBits();
            Color temp ;
            //Color compareClr = Color.FromArgb(255, 255, 255, 255);
            for (int y = 0; y < lockBitmap.Height; y++)
            {
                for (int x = 0; x < lockBitmap.Width; x++)
                {
                    temp = lockBitmap.GetPixel(x,y);
                    if (colors.ContainsKey(temp))
                    {
                        colors[temp]++;
                    }
                    else
                        colors.Add(temp, 1);
                    //if (lockBitmap.GetPixel(x, y) == compareClr)
                    //{
                    //    lockBitmap.SetPixel(x, y, Color.Red);
                    //}
                }
            }
            lockBitmap.UnlockBits();
        
          
           // bmp.Save("d:\\result.png");
        }
    }
}
