using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace ColorCount
{
    public class AcoUnpacker
    {
        private int version = 1;
        private int colorCount = 0;
        public string FilePath { get; set; }
        private byte[] colors;
        public List<Color> Colors = new List<Color>();
        public List<string> ColorsName = new List<string>();

        public void Unpack()
        {
            using (Stream stream = File.OpenRead(FilePath))
            {
                this.version = ReadInt16(stream);
                this.colorCount = ReadInt16(stream);
                for (int n = 0; n < this.colorCount; n++)
                {
                    int colorID = ReadInt16(stream);
                    Debug.WriteLine(colorID);
                    int R = ReadInt16(stream);
                    int G = ReadInt16(stream);
                    int B = ReadInt16(stream);
                    int A = ReadInt16(stream);

                    Colors.Add(DecodeColor(colorID,R,G,B,A));
                   
                }
                stream.Position +=16;
                while (stream.Position < stream.Length)
                {
                    int lenght = ReadInt16(stream);

                    Debug.WriteLine($"P:{stream.Position}L:{lenght}");

                    string name = ReadString(stream, lenght);
                    Debug.WriteLine($"Nome:{name}");
                    stream.Position += 12;
                    ColorsName.Add(name);
                }
                //stream.Position = 82;
                //int lenght = ReadInt16(stream);
                
                //

            }
            return;

                byte[] buffer = File.ReadAllBytes(FilePath);

            for (int y = 0; y < buffer.Length - 4; y++)
            {
                Debug.WriteLine("{0} - {1} - {2}", y, ByteToBinaryString(buffer[y]), BitConverter.ToInt32(buffer, y));
            }



            short version = BitConverter.ToInt16(RevertByte(buffer, 0, 2), 0);
            short colorCount = BitConverter.ToInt16(RevertByte(buffer, 2, 2), 0);



            byte[] colorBuffer = new byte[colorCount * 10];
            Array.Copy(buffer, 4, colorBuffer, 0, colorBuffer.Length);

            byte[] colorName = new byte[buffer.Length - (colorBuffer.Length + 4)];
            Array.Copy(buffer, colorBuffer.Length + 4, colorName, 0, colorName.Length);

            int i = 0;
            while (i < colorBuffer.Length)
            {
                //colorID - 2 byte
                short colorID = BitConverter.ToInt16(colorBuffer, i);
                i += 2;

                //ColorData - 8 byte
                //RGB R - 2 byte G- 2 byte B - 2 byte

                ushort R = BitConverter.ToUInt16(colorBuffer, i);
                i += 2;
                ushort G = BitConverter.ToUInt16(colorBuffer, i);
                i += 2;
                ushort B = BitConverter.ToUInt16(colorBuffer, i);
                i += 2;
                i += 2;
                //Colors.Add(Color.FromArgb(R,G,B));
                Colors.Add(Color.FromArgb((int)(R / 256), (int)(G / 256), (int)(B / 256)));

            }
            i = 0;
            while (i < colorName.Length)
            {
                int nameLength = BitConverter.ToInt32(colorName, i);
                i += 4;
                string name = Encoding.Unicode.GetString(colorName, i, nameLength * 2);
                ColorsName.Add(name);
                i += nameLength * 2;
            }


            string yourByteString = ByteToBinaryString(buffer[1]);

            //nome - 4 byte tamanho do nome - string.length
            //2 bytes por caracter
        }

        private Color DecodeColor(int colorID,int value1,int value2,int value3,int value4)
        {
            switch(colorID)
            {
                case 0:
                    return Color.FromArgb((int)(value1 / 256), (int)(value2 / 256), (int)(value3 / 256));
                case 1:
                    return Color.Empty;
                case 2:
                    return Color.Empty;
                case 7:
                    return Color.Empty;
                case 8:
                    return Color.Empty;

            }
            return Color.Empty;
        }

        private int ReadInt16(Stream stream)
        {
            return (stream.ReadByte() << 8) | stream.ReadByte();
        }
        private int ReadInt32(Stream stream)
        {
            return ((byte)stream.ReadByte() << 24) | ((byte)stream.ReadByte() << 16) | ((byte)stream.ReadByte() << 8) | ((byte)stream.ReadByte());
        }
        private string ReadString(Stream stream, int length)
        {
            byte[] buffer;

            buffer = new byte[length * 2];

            stream.Read(buffer, 0, buffer.Length);

            return Encoding.BigEndianUnicode.GetString(buffer);
        }

        public byte[] RevertByte(byte[] original,int start, int length)
        {
            byte[] temp = new byte[length];
            int i = 0;
            int j = length -1;

            while(i<length)
            {
                temp[i] = original[start + j];
                i++;
                j--;
            }


            
            return temp;
        }

        public string ByteToBinaryString(byte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }
    }
}
