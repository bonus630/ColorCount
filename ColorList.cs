using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ColorCount
{
    
    public class ColorList
    {
        public event Action<ColorTagged> ColorTagChangeEvent;
        private List<ColorTagged> internalList = new List<ColorTagged>();
        

        private List<Color> colorTable = new List<Color>();
        private int tag = 0;

        public int Count => internalList.Count;

        public SolidBrush WhiteBrush { get; set; }
        public SolidBrush BlackBrush { get; set; }

        public ColorTagged this[int index]
        {
            get {
                return this.internalList[index];
            }
            set { this.internalList[index] = value; }
        }
        public ColorTagged GetColorByIndexTable(int colorTableIndex)
        {
            for (int i = 0; i < this.internalList.Count; i++)
            {
                if (this.internalList[i].ColorTableIndex.Contains(colorTableIndex))
                    return this.internalList[i];
            }
            return null;
        }
        public ColorList(SolidBrush white,SolidBrush black)
        {
            WhiteBrush = white;
            BlackBrush = black;
        }
        public int Sun()
        {
            int sun = 0;
            for (int i = 0; i < this.internalList.Count; i++)
            {
                sun += this.internalList[i].ColorCount;
            }
            return sun;
        }
        public void Add(Color color,string name)
        {
            int index = containsColor(color);
            if(index > -1)
            {
                this.internalList[index].Name = name;
            }
            else
            {
                 ColorTagged t = new ColorTagged(tag);
                t.RGB = color;
                t.TextBrush = GetColorDistance(Color.White, t.RGB) > 300 ? WhiteBrush : BlackBrush;
                t.Name = name;
                this.internalList.Add(t);
                this.tag = GetLastTag();
            }
        }
        public void SetNewColorTag(ColorTagged colorTagged,int newTag)
        {
            if (colorTagged.Tag == newTag)
                return;
            ColorTagged color = null;
            try
            {
                color = this.internalList.Single(c => c.Tag == newTag);
            }
            catch { }
            colorTagged.SetNewTag = newTag;
            
            if(color != null)
                color.SetNewTag = colorTagged.OldTag;
            this.internalList.Sort();
            ColorTagChangeEvent?.Invoke(color);
        }
        public void Sort()
        {
            this.internalList.Sort();
        }
        //public void Add(ColorTagged color)
        //{
        //    this.internalList.Add(color);
        //}
        public void Add(int colorTableIndex,int colorCount = -1)
        {
            //int index = containsColor(colorTable[colorTableIndex]);
            //if (index == -1)
            //{
            ColorTagged t = null;
                //= new ColorTagged(colorTableIndex, tag);
            //t.RGB = this.colorTable[colorTableIndex];
            //t.Brush = GetColorDistance(Color.White, t.RGB) > 300 ? WhiteBrush : BlackBrush;
            Color color = colorTable[colorTableIndex];
            int index = containsColor(color);
            if (index > -1)
            {

                t = this.internalList[index];
                if(!t.ColorTableIndex.Contains(colorTableIndex))
                    t.ColorTableIndex.Add(colorTableIndex);
                if (colorCount > -1)
                    t.ColorCount = colorCount;
            }
            else
            {
                t = new ColorTagged(this.tag);
                t.ColorTableIndex.Add(colorTableIndex);
                t.RGB = this.colorTable[colorTableIndex];
                t.TextBrush = GetColorDistance(Color.White, t.RGB) > 300 ? WhiteBrush : BlackBrush;
                if (colorCount > -1)
                    t.ColorCount = colorCount;
                else
                    t.ColorCount = 1;
                this.internalList.Add(t);
                this.tag = GetLastTag();
            }
            
            //}
                
        }
        private int GetLastTag()
        {
            int t = 0;
            for (int i = 0; i < internalList.Count; i++)
            {
                if (internalList[i].Tag > t)
                    t = internalList[i].Tag;
            }
            return t+1;
        }
        public void Add(ColorTagged colorTagged)
        {
            if (!this.Contains(colorTagged))
            {
                this.internalList.Add(colorTagged);
                colorTagged.TextBrush = GetColorDistance(Color.White, colorTagged.RGB) > 300 ? WhiteBrush : BlackBrush;
                tag = GetLastTag();
            }
        }
        public bool Contains(ColorTagged color)
        {
            return this.internalList.Contains(color);

        }
        private int containsColor(Color color)
        {
            for (int i = 0; i < this.internalList.Count; i++)
            {
                if (this.internalList[i].RGB.Equals(color))
                    return i;
            }
            return -1;
        }
        public bool ContainsTableIndex(int index)
        {
            return this.internalList.Exists(r => r.ColorTableIndex.Contains(index));
        }
        public void Clear()
        {
            this.internalList.Clear();
        }
        public void Reset()
        {
            internalList.Clear();
            colorTable.Clear();
            tag = 0;
        }
        public void ResetCounter()
        {
            for (int i = 0; i < internalList.Count; i++)
            {
                internalList[i].ColorCount = 0;
            }
        }
        /// <summary>
        /// Executa o cálculo da distância de uma cor para outra
        /// </summary>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <returns></returns>
        public static double GetColorDistance(Color color1, Color color2)
        {
            double res = 0;

            res = Math.Sqrt(((color1.R - color2.R) * (color1.R - color2.R)) + ((color1.G - color2.G) * (color1.G - color2.G)) + ((color1.B - color2.B) * (color1.B - color2.B)));


            return res;
        }
        public byte[] ColorTable
        {
            set
            {
                for (int i = 0; i < internalList.Count; i++)
                {
                    internalList[i].ColorTableIndex.Clear();
                }
                this.colorTable.Clear();
                int f = 0;
                while (f < value.Length)
                {
                    this.colorTable.Add(Color.FromArgb(255, value[f + 2], value[f + 1], value[f]));
                    f += 4;
                }
            }
        }
    }
}
