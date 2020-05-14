using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab1
{
    class GlassFilter : Filters
    {
        Bitmap clone;
        Random rand = new Random();
        public GlassFilter(Bitmap scr)
        {
            clone = new Bitmap(scr.Width, scr.Height);
            for (int i = 0; i < scr.Width; i++)
            {
                for (int j = 0; j < scr.Height; j++)
                {
                    clone.SetPixel(i, j, scr.GetPixel(i, j));

                }

            }

        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourseColor = clone.GetPixel(x, y);
            double rands = rand.NextDouble();
            x = rand.Next(x - 3, x + 3);
            y = rand.Next(y - 3, y + 3);

            if (((x < sourceImage.Width) && (y < sourceImage.Height)) && ((x >= 0) && (y >= 0))) sourseColor = clone.GetPixel(x, y);



            return sourseColor;


        }
    }
}
