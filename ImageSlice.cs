using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorCount
{
    public class ImageSlice
    {
        public Color IgnoredColor { get; set; }

        ColorList colorList;

        public List<List<Pixel>> Slices = new List<List<Pixel>>();

        private BmpUnpack bmp;

        public BmpUnpack BMP
        {
            get { return bmp; }
            set { 
                bmp = value;
                this.colorList = this.bmp.colorCountList;
                
            }
        }
        private int count =1;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        public ImageSlice()
        {
            Slices.Add(new List<Pixel>());
        }
        public void Slice()
        {
            int ignoredCount = 0;
            bool nextLine = false;
            Pixel p;
            ColorTagged color;
            for (int i = 0; i < bmp.pixels.Count; i++)
            {
                p = bmp.pixels[i];
                color = colorList.GetColorByIndexTable(p.color);
                if(color.Equals(IgnoredColor))
                {
                    ignoredCount++;
                }
                else
                {
                    nextLine = true;
                    ignoredCount = 0;
                    if (Slices[count -1]!=null)
                    {
                        Slices[count - 1].Add(bmp.pixels[i]);
                    }
                }
                if(ignoredCount==bmp.Width)
                {
                    
                    if (nextLine)
                    {
                        count++;
                        Slices.Add(new List<Pixel>());
                    }
                    nextLine = false;

                }
            }
        }

    }
}
