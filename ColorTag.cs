using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ColorCount
{
    public class ColorTag2
    {
        private Color whiteColor = Color.FromArgb(255,255,255);
        public SolidBrush whiteBrush, blackBrush;
        public void SetColorTable(byte[] colorTableBytes)
        {
            this.NewCurrentColor();
            int f = 0;
            while (f < colorTableBytes.Length)
            {
                this.AddColor(Color.FromArgb(255, colorTableBytes[f + 2], colorTableBytes[f + 1], colorTableBytes[f]));
                f += 4;
            }
        }

        public ColorTag(SolidBrush whiteBrush, SolidBrush blackBrush)
        {
            this.whiteBrush = whiteBrush;
            this.blackBrush = blackBrush;
        }

        public SolidBrush CorrectBrush(int index)
        {
            return this.brushes[index];
        }
        public string ColorName(int index)
        {
            return this.colorsName[index];
        }
        //public bool IsDrawed(int index)
        //{
        //    return DrawedColors.Contains(this.currentColors[index]);
        //}
        public int Count => currentColors.Count;
        private Dictionary<int, Color> allColors = new Dictionary<int, Color>();
        private List<string> colorsName = new List<string>();
        //public List<Color> DrawedColors = new List<Color>();
        private List<Color> currentColors = new List<Color>();
        private List<SolidBrush> brushes = new List<SolidBrush>();
        public List<Color> CurrentColors { get { return this.currentColors; }private set{
                this.currentColors = value;
                for (int i = 0; i < value.Count; i++)
                {
                    AddColor(value[i]);
                }

                ; } }
        private int tag = 0;

      
        public int GetTagIndex(Color color)
        {
            for (int i = 0; i < allColors.Count; i++)
            {
                if (allColors[i].Equals(color))
                    return i;
            }
            return -1;
        }
        private void NewCurrentColor()
        {
            this.currentColors.Clear();
            //DrawedColors.Clear();
        }
        public void AddColor(Color color, string colorName = "")
        {
            if(!allColors.ContainsValue(color))
            {
                allColors.Add(tag, color);
                
                colorsName.Add(colorName);
                if(GetColorDistance(whiteColor, color) > 300 )
                    brushes.Add(whiteBrush);
                else
                    brushes.Add(blackBrush);
                tag++;
            }
            currentColors.Add(color);
        }
        public void Reset()
        {
            this.allColors.Clear();
            this.brushes.Clear();
            this.currentColors.Clear();
            this.colorsName.Clear();
            tag = 0;
        }
        /// <summary>
        /// Executa o cálculo da distância de uma cor para outra
        /// </summary>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <returns></returns>
        private double GetColorDistance(Color color1, Color color2)
        {
            double res = 0;

            res = Math.Sqrt(((color1.R - color2.R) * (color1.R - color2.R)) + ((color1.G - color2.G) * (color1.G - color2.G)) + ((color1.B - color2.B) * (color1.B - color2.B)));


            return res;
        }


    }
}
