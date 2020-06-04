using System;
using System.Collections.Generic;
using System.Drawing;

namespace ColorCount
{
    public class ColorTagged : IComparable
    {
        private List<int> colorTableIndex = new List<int>();
      
        private int colorCount = 0;
        private int colorTag;
        private int oldColorTag;
        private Color rgb;
    

        public int ColorCount { get { return this.colorCount; } set { colorCount = value; } }
        public List<int> ColorTableIndex { get { return this.colorTableIndex; } set { this.colorTableIndex = value; } }
        public int Tag => colorTag;
        public int OldTag => oldColorTag;
        public string Name { get; set; }
        public SolidBrush TextBrush { get; set; }
        public SolidBrush Brush { get; private set; }
        public Color RGB { get { return this.rgb; } set {
                this.rgb = value;
                setBrush();
        } }
        public int SetNewTag { set { oldColorTag = colorTag; colorTag = value; } }
        public string Hex { get
            {
               
                    System.Drawing.Color c = this.RGB;
                    return $"{c.A.ToString("X2")}{c.R.ToString("X2")}{c.G.ToString("X2")}{c.B.ToString("X2")}";
                
            }
            set
            {
                this.RGB = Color.FromArgb(
                int.Parse(value, System.Globalization.NumberStyles.AllowHexSpecifier));
            }
        }
        //public ColorTagged(int colorTableIndex,int tag)
        //{
        //    this.colorTableIndex = colorTableIndex;
        //    colorTag = tag;
        //}
      
        public ColorTagged(int tag)
        {
            colorTag = tag;
        }
        private void setBrush()
        {
            Brush = new SolidBrush(this.rgb);
        }
        public bool Equals(Color color)
        {
            if (color == null)
                return false;
            return color.Equals(RGB);
        }
        public bool Equals(ColorTagged color)
        {
            if (color == null)
                return false;
            return color.RGB.Equals(RGB);
        }
        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object obj)
        {
            ColorTagged c = obj as ColorTagged;
            if (c == null)
                return -1;
            else
                return this.Tag.CompareTo(c.Tag);
        }
    }
   
      
    
}
