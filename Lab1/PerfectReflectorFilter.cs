using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab1
{
    class PerfectReflectorFilter : Filters
    {
        int Rmax = 0, Gmax = 0, Bmax = 0;
        public PerfectReflectorFilter(Bitmap scr)
        {
            Color sourseColor;
            for (int i = 0; i < scr.Width; i++)
            {
                for (int j = 0; j < scr.Height; j++)
                {
                    sourseColor = scr.GetPixel(i, j);
                    if (Rmax <= sourseColor.R)
                        Rmax = sourseColor.R;
                    if (Gmax <= sourseColor.G)
                        Gmax = sourseColor.G;
                    if (Bmax <= sourseColor.B)
                        Bmax = sourseColor.B;

                }
            }
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourseColor = sourceImage.GetPixel(x, y);
            int resultR = (int)(sourseColor.R * 255 / Rmax);
            int resultG = (int)(sourseColor.G * 255 / Gmax);
            int resultB = (int)(sourseColor.B * 255 / Bmax);

            return Color.FromArgb(
                 Clamp((int)resultR, 0, 255),
                 Clamp((int)resultG, 0, 255),
                 Clamp((int)resultB, 0, 255)
                 );
        }
    }
}
