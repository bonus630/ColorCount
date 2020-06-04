using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace ColorCount
{
    public class BmpUnpack
    {
        public  byte[] colorTable = null;
        public  byte[] pixelTable = null;
        //public Dictionary<int, int> colorCount = new Dictionary<int, int>();
        public ColorList colorCountList;
        private int width, height;
        private string filePath;
        public int Width => width;
        public int Height => height;
        public string FilePath => filePath;

        private bool resetCounter = true;
       
        public List<Pixel> pixels = new List<Pixel>();

        public BmpUnpack(ColorList colorList)
        {
            colorCountList = colorList;
        }

        private int getIntFromBitArray(BitArray bitArray)
        {

            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];

        }
        public void Reset()
        {
            //colorCount.Clear();
            colorCountList.Clear();
        }
        public  void Unpack(string packPath, string unpackFolder)
        {
            pixels.Clear();
            this.filePath = packPath;
            //for (int i = 0; i < colorCount.Count; i++)
            //{
            //    colorCount[i] = 0;
            //}
            if (resetCounter)
            {
                colorCountList.ResetCounter();
            }


            byte[] packBuffer = File.ReadAllBytes(packPath);
            int pont = 0;
            
            //Assinatura do arquivo: os caracteres ASCII "BM" ou (42
            // 4D)h.É a identificação de ser realmente BMP.
            char[] BM = Encoding.ASCII.GetChars(packBuffer, pont, 2);
            pont += 2;
            if ((new string(BM)) != "BM")
                throw new Exception("Tente usar arquivos BMP");

            uint fileSize = BitConverter.ToUInt32(packBuffer, pont);
            pont += 4;

            //Reserved
            pont += 2;
            //Reserved
            pont += 2;

            //Especifica o deslocamento, em bytes, para o início da área
            //de dados da imagem, a partir do início deste cabeçalho.
            //- Se a imagem usa paleta, este campo tem
            //tamanho = 14 + 40 + (4 x NumeroDeCores)
            //-Se a imagem for true color, este campo tem
            //tamanho = 14 + 40 = 54


            uint ImagePixelArrayOffset = BitConverter.ToUInt32(packBuffer, pont);
            
            pont += 4;


            uint DIBHeaderSize = BitConverter.ToUInt32(packBuffer, pont);
            pont += 4;

            uint ImageWidth = BitConverter.ToUInt32(packBuffer, pont);
            pont += 4;
            uint ImageHeight = BitConverter.ToUInt32(packBuffer, pont);
            pont += 4;

            this.width = (int)ImageWidth;
            this.height = (int)ImageHeight;

            //Planes
            pont += 2;

            //Quantidade de Bits por pixel(1, 4, 8, 24, 32)
            ushort BitByPixel = BitConverter.ToUInt16(packBuffer, pont);
            pont += 2;
            if (BitByPixel != 8 && BitByPixel !=4)
                throw new Exception("Salve com a profundidade em 4 ou 8 Bits");
            //0 = BI_RGB _ sem compressão
            //1 = BI_RLE8 – compressão RLE 8 bits
            //2 = BI_RLE4 – compressão RLE 4 bits
            uint ImageCompression = BitConverter.ToUInt32(packBuffer, pont);
            pont += 4;
            if (ImageCompression != 0)
                throw new Exception("Não utilize compressão");
            uint ImageByteSize = BitConverter.ToUInt32(packBuffer, pont)-2;
            pont += 4;
            this.pixelTable = new byte[ImageByteSize];

            uint DDPXResolution = BitConverter.ToUInt32(packBuffer, pont);
            pont += 4;

            uint DDPYResolution = BitConverter.ToUInt32(packBuffer, pont);
            pont += 4;

            //Número de cores usadas na imagem. Quando ZERO indica o
            //uso do máximo de cores possível pela quantidade de Bits por
            //pixel , que é 2 elevado a bits por pixel 
            uint NumColors = BitConverter.ToUInt32(packBuffer, pont);
            pont += 4;

            

            uint ColorTableSize = 0;
            //if (NumColors == 0)
            //    ColorTableSize = 4 * Math.Pow(2, BitByPixel);
            //else
                ColorTableSize = 4 * NumColors;
            this.colorTable = new byte[ColorTableSize];
            uint NumImportantColors = BitConverter.ToUInt32(packBuffer, pont);
            pont += 4;

            //Extra bit mask 3 or 4
            // DIBHeaderSize == 40
            //pont += 4;

            ////Move para ColorTable
            // B, G, R, 0x00
            //pont += 40;
            Array.Copy(packBuffer, pont, this.colorTable, 0, ColorTableSize);
            pont += (int)ColorTableSize;

            colorCountList.ColorTable = colorTable;

            uint rowSize = ImageByteSize / ImageHeight;
          

            Array.Copy(packBuffer, ImagePixelArrayOffset, this.pixelTable, 0, ImageByteSize);
            pont += (int)ImageByteSize;


            //Calculo do tamanho da linha
            //RowSize = |(BitsPerPixel * ImageWidth + 31) / 32 |*4
            //Calculo do tamanho do mapa de dados da imagem
            //PixelArraySize = RowSize *|ImageHeight|
            //rowSize = ((BitByPixel * ImageWidth) + 31) / 32;
           // rowSize = rowSize + (rowSize % 4);
            //rowSize = 4;
            if (BitByPixel == 8)
            {
               
                uint i = 0;
                int colCount = 0;
                int rowCount = 0;

                while (i < this.pixelTable.Length)
                {
                    //int value = (int)this.pixelTable[i];
                    //if (colorCount.ContainsKey(value))
                    //    colorCount[value]++;
                    //else
                    //    colorCount.Add(value, 1);
                    int value = (int)this.pixelTable[i];
                    if (colorCountList.ContainsTableIndex(value))
                    {
                        colorCountList.GetColorByIndexTable(value).ColorCount++;
                        
                    }
                    else
                        colorCountList.Add(value,1);


                    pixels.Add(new Pixel(colCount,rowCount, this.pixelTable[i]));



                    i++;
                    colCount++;
                    if(colCount==ImageWidth)
                    {
                        i = i + (rowSize - ImageWidth);
                        colCount = 0;
                        rowCount++;
                    }
                }
            }
           
            if (BitByPixel == 4)
            {

                uint i = 0;
                int colCount = 0;
                int rowCount = 0;

                while (i < this.pixelTable.Length)
                {
                    var LBits = (this.pixelTable[i] & 0xf0) >> 4;
                    var HBits = this.pixelTable[i] & 0x0f;
                    //int value = 0;
                    //int value = getIntFromBitArray(LBits);
                    //if (colorCount.ContainsKey(LBits))
                    //    colorCount[LBits]++;
                    //else
                    //    colorCount.Add(LBits, 1);

                    if (colorCountList.ContainsTableIndex(LBits))
                        colorCountList.GetColorByIndexTable(LBits).ColorCount++;
                    else
                        colorCountList.Add(LBits,1);

                    pixels.Add(new Pixel(colCount, (int)i, LBits));


                    colCount++;
                    if (colCount == ImageWidth)
                    {
                        i = i + (rowSize - ImageWidth / 2);
                        colCount = 0;
                        rowCount++;
                        continue;
                    }
                    //if (colorCount.ContainsKey(HBits))
                    //    colorCount[HBits]++;
                    //else
                    //    colorCount.Add(HBits, 1);
                    if (colorCountList.ContainsTableIndex(HBits))
                        colorCountList.GetColorByIndexTable(HBits).ColorCount++;
                    else
                        colorCountList.Add(HBits,1);

                    pixels.Add(new Pixel(colCount, (int)i, HBits));


                    i++;
                    colCount++;
                    if (colCount == ImageWidth)
                    {
                        i = i + (rowSize - ImageWidth /2);
                        colCount = 0;
                        rowCount++;
                    }
                }
            }

        }
    }
}
