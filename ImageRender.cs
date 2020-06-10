using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace ColorCount
{
    public class ImageRender
    {
        int retSize = 80;
        int padding = 30;
        int colTableCount = 3;

        SolidBrush transparentBrush;


        public Color IgnoredColor { get; set; }
        public int Divider { get { return this.divider; } set { this.divider = value; } }

        Font font;
        Font tableFont = new Font(FontFamily.GenericSansSerif, 40, FontStyle.Regular);
        int vPixelSize = 10;
        int rtSize = 0;
        int divider = 1;
        bool grid = false;
        bool tags = false;
        private BmpUnpack bmp;
        public BmpUnpack BMP
        {
            get { return this.bmp; }
            set
            {
                this.bmp = value;

                this.colorList = this.bmp.colorCountList;
                this.colTableCount = 3;
            }
        }
        //ColorTag colorTag;
        ColorList colorList;

        public bool Grid { get { return this.grid; } set { this.grid = value; } }
        public bool Tags { get { return this.tags; } set { this.tags = value; } }
        public event Action<int> ProgressChange;
        public event Action<Bitmap> ImageRenderFinish;
        public int TotalSize { get { return bmp.pixels.Count; } }
        public int VPixelSize
        {
            get { return this.vPixelSize; }
            set
            {
                this.vPixelSize = value;
                CalcVPixelSize();
            }
        }

        private void CalcVPixelSize()
        {
            if (grid)
            {
                this.rtSize = (this.vPixelSize - this.vPixelSize % 10) / 10;
                this.vPixelSize += 2 * rtSize;
            }
            else
            {
                this.rtSize = 0;
            }
        }

        //public ImageRender(BmpUnpack bmp,ColorTag colorTag)
        //{
        //    this.bmp = bmp;
        //    this.colorTag = colorTag;

        //}
        //public ImageRender(BmpUnpack bmp)
        public ImageRender()
        {
            //this.bmp = bmp;
            // this.colorList = bmp.colorCountList;

        }
        public void Reset()
        {
            if (bmp != null)
            {
                bmp.Reset();

            }
            colorList.Reset();
            IgnoredColor = Color.Empty;
        }
        private int CalcNumRow(int numCols)
        {
            int numItens = colorList.Count;
            int emptyColor = 0;
            for (int i = 0; i < colorList.Count; i++)
            {
                if (colorList[i].Equals(IgnoredColor) || colorList[i].ColorCount == 0)
                    emptyColor++;
            }
            numItens -= emptyColor;
            int modRow = numItens % numCols;
            int numRows = (numItens - modRow) / numCols;
            if (modRow > 0)
                numRows++;
            while (numRows * numCols < colorList.Count - emptyColor)
                numRows++;
            return numRows;
        }

        public Bitmap GeneratePixelTableCount(int width)
        {
            //Descubro a maior largura
            float colorNameBigWidth = 0;
            float colorCountBigWidth = 0;
            SizeF colorNameTextSize = SizeF.Empty;
            SizeF colorCountTextSize = SizeF.Empty;

            int halfRetSize = retSize / 2;
            int halfOneRetSize = 3 * retSize / 2;



            using (Bitmap bitmap = new Bitmap(width, retSize))
            {

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    for (int i = 0; i < colorList.Count; i++)
                    {
                        if (!colorList[i].Equals(IgnoredColor) && colorList[i].ColorCount > 0)
                        {
                            int colorTagIndex = colorList[i].Tag;
                            string text = colorList[i].Name;
                            string text2 = (bmp.colorCountList[i].ColorCount / divider).ToString("0.0");
                            colorNameTextSize = g.MeasureString(text, tableFont);
                            if (colorNameTextSize.Width > colorNameBigWidth)
                                colorNameBigWidth = colorNameTextSize.Width;
                            colorCountTextSize = g.MeasureString(text2, tableFont);
                            if (colorCountTextSize.Width > colorCountBigWidth)
                                colorCountBigWidth = colorCountTextSize.Width;
                        }
                    }


                }

            }

            int tempWidth = (int)(3 * retSize + colorNameBigWidth + colorCountBigWidth);
            colTableCount = ((width - halfRetSize) - ((width- halfRetSize) % tempWidth)) / tempWidth;
            if (colTableCount < 1)
                colTableCount = 1;
            int compHeight = (halfOneRetSize) * CalcNumRow(colTableCount)+ halfRetSize;
            //Desenho a tabela

            int numRows = CalcNumRow(colTableCount);

            if (width < tempWidth)
                width = tempWidth;
            Bitmap bitmapTable = new Bitmap(width, compHeight);
            using (Graphics g = Graphics.FromImage(bitmapTable))
            {

                int col = 0;
                int row = 0;
                Rectangle rt = new Rectangle(0, 0, retSize, retSize);
                for (int colorIndex = 0; colorIndex < colorList.Count; colorIndex++)
                {


                    if (!colorList[colorIndex].Equals(IgnoredColor) && colorList[colorIndex].ColorCount > 0)
                    {

                        //Desenha o retângulo com a cor 
                        rt.X = col * tempWidth + (halfRetSize);
                        rt.Y = row * (halfOneRetSize) + halfRetSize;
                        g.FillRectangle(colorList[colorIndex].Brush, rt);
                        g.DrawRectangle(new Pen(colorList[colorIndex].TextBrush.Color), rt);
                        {


                            int colorTagIndex = colorList[colorIndex].Tag;
                            string t = colorTagIndex.ToString();
                            SizeF tSize = g.MeasureString(t, tableFont);
                            //Desenha o nome da cor
                            string text = colorList[colorIndex].Name;
                            g.DrawString(text, tableFont, colorList.BlackBrush, rt.Right + halfRetSize, rt.Top + (rt.Height  - tSize.Height) / 2);

                            //Desenha a quantidade de cores
                            text = (bmp.colorCountList[colorIndex].ColorCount / divider).ToString("0.0");
                            g.DrawString(text, tableFont, colorList.BlackBrush, rt.Right + retSize + colorNameBigWidth, rt.Top + (rt.Height - tSize.Height) / 2);
                            //desenha a tag da cor dentro do retângulo da cor
                            g.DrawString(t, tableFont, colorList[colorIndex].TextBrush, rt.Left + (rt.Width  - tSize.Width) / 2, rt.Top + (rt.Height  - tSize.Height) / 2);
                        }
                        if (col < colTableCount)
                        {
                            col++;
                        }
                        if (col == colTableCount)
                        {
                            row++;
                            col = 0;
                        }
                    }

                }



            }
            return bitmapTable;
        }
        public void DrawCounter()
        {
            try
            {
                font = new Font(FontFamily.GenericSansSerif, 3 * (vPixelSize - (rtSize * 2)) / 5, FontStyle.Regular);
                List<Pixel> pixels = bmp.pixels;
                //byte[] colorTableBuffer = bmp.colorTable;
                //colorTag.SetColorTable(colorTableBuffer);
                //List<Color> colors = new List<Color>();

                //colorTag.CurrentColors = colors;
                //int compHeight = vPixelSize * 2 + colors.Count * (vPixelSize + vPixelSize / 2);
                //int compHeight = (retSize + VPixelSize) * CalcNumRow(colorTag.Count,3);

                int width = bmp.Width * VPixelSize;
                Bitmap colorTable = GeneratePixelTableCount(width);

                if (colorTable.Width > width)
                    width = colorTable.Width;
                Bitmap bitmap = new Bitmap(width, bmp.Height * vPixelSize + colorTable.Height);

                Graphics g = Graphics.FromImage(bitmap);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(colorList.WhiteBrush, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                g.DrawImage(colorTable, 0, 0);
                colorTable.Dispose();
                //for (int i = 0; i < colors.Count; i++)
                //{
                //    g.FillRectangle(new SolidBrush(colors[i]), new Rectangle(10 * i, 0, 10, 10));

                //}
                Rectangle rt = new Rectangle();
                Rectangle gr = new Rectangle();
                Pixel p;
                ColorTagged color;
                int colorTagIndex;
                string text;
                SizeF textSize;
                for (int i = 0; i < pixels.Count; i++)
                {
                    //Debug.WriteLine(i);

                    p = pixels[i];
                    //if (!colorTag.CurrentColors[p.color].Equals(IgnoredColor))
                    color = colorList.GetColorByIndexTable(p.color);
                    if (!color.Equals(IgnoredColor))
                    {


                        rt.X = p.x * vPixelSize;
                        rt.Y = -p.y * vPixelSize + (bitmap.Height - vPixelSize);
                        rt.Width = vPixelSize;
                        rt.Height = vPixelSize;
                        //System.Drawing.Imaging.BitmapData bitmapData = bitmap.LockBits(rt, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                        //string text = p.color.ToString("00");
                        // int colorTagIndex = colorTag.GetTagIndex(colorTag.CurrentColors[p.color]);
                        colorTagIndex = color.Tag;
                       
                        //g.FillRectangle(new SolidBrush(colorTag.CurrentColors[p.color]), rt);
                        g.FillRectangle(color.Brush, rt);
                        //g.DrawString(text, font, colorTag.CorrectBrush(colorTagIndex), rt.Left + (rt.Width / 2 - textSize.Width / 2), rt.Top + (rt.Height / 2 - textSize.Height / 2));
                        if (tags)
                        {
                            text = colorTagIndex.ToString();
                            textSize = g.MeasureString(text, font);
                            g.DrawString(text, font, color.TextBrush, rt.Left + (rt.Width / 2 - textSize.Width / 2), rt.Top + (rt.Height / 2 - textSize.Height / 2));
                        }

                        if (grid)
                        {
                            //retangulo do topo
                            gr.X = rt.X; gr.Y = rt.Y; gr.Width = rt.Width; gr.Height = rtSize;
                            g.FillRectangle(colorList.BlackBrush, gr);
                            //retangulo da esquerda
                            gr.X = rt.X; gr.Y = rt.Y; gr.Width = rtSize; gr.Height = rt.Height;
                            g.FillRectangle(colorList.BlackBrush, gr);
                            //Retangulo da base
                            gr.X = rt.X; gr.Y = rt.Y + rt.Height - rtSize; gr.Width = rt.Width; gr.Height = rtSize;
                            g.FillRectangle(colorList.WhiteBrush, gr);
                            //Retangulo da direito
                            gr.X = rt.X + rt.Width - rtSize; gr.Y = rt.Y; gr.Width = rtSize; gr.Height = rt.Height;
                            g.FillRectangle(colorList.WhiteBrush, gr);
                        }
                        // bitmap.UnlockBits(bitmapData);
                    }
                    if (i % 1000 == 0 && ProgressChange != null)
                    {
                        ProgressChange(i);
                    }
                }

                //GeneratePixelTableCount(colorTag.CurrentColors, g);

                ImageRenderFinish?.Invoke(bitmap);


            }
            catch (Exception e)
            {
                throw e;
            }


        }
        public void DrawColor(int index)
        {

            try
            {

                List<Pixel> pixels = bmp.pixels;
                Bitmap bitmap = new Bitmap(bmp.Width, bmp.Height);
                Graphics g = Graphics.FromImage(bitmap);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(colorList.WhiteBrush, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

                Rectangle rt = new Rectangle();

                Pixel p;
                ColorTagged color;
                int cont = 0;
                for (int i = 0; i < pixels.Count; i++)
                {
                    p = pixels[i];
                    color = colorList.GetColorByIndexTable(p.color);
                    if (color.Tag == index)
                    {
                        rt.X = p.x;
                        rt.Y = -p.y + (bitmap.Height - 1);
                        rt.Width = 1;
                        rt.Height = 1;
                        g.FillRectangle(color.Brush, rt);
                        cont++;
                    }
                    //if (i % 1000 == 0 && ProgressChange != null)
                    //{
                    //    ProgressChange(i);
                    //}
                }

                ImageRenderFinish?.Invoke(bitmap);
            }
            catch (Exception e)
            {
                throw e;
            }


        }


    }
}
