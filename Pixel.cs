using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorCount
{
    public class Pixel
    {
        public int x;
        public int y;

        public int color;

        public Pixel(int x, int y,int color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }

    }
}
