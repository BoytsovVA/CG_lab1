using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class LinearHistogram : Filters
    {
        int Rmin = 255, Gmin = 255, Bmin = 255;
        int Rmax = 0, Gmax = 0, Bmax = 0;
        public LinearHistogram(Bitmap scr)
        {

            Color sourseColor;
            for (int i = 0; i < scr.Width; i++)
            {
                for (int j = 0; j < scr.Height; j++)
                {
                    sourseColor = scr.GetPixel(i, j);
                    if (Rmin > sourseColor.R)
                        Rmin = sourseColor.R;
                    if (Gmin > sourseColor.G)
                        Gmin = sourseColor.G;
                    if (Bmin > sourseColor.B)
                        Bmin = sourseColor.B;
                    if (Rmax < sourseColor.R)
                        Rmax = sourseColor.R;
                    if (Gmax < sourseColor.G)
                        Gmax = sourseColor.G;
                    if (Bmax < sourseColor.B)
                        Bmax = sourseColor.B;

                }
            }
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourseColor = sourceImage.GetPixel(x, y);
            return Color.FromArgb(
                Clamp((int)((sourseColor.R - Rmin) * 255 / (Rmax - Rmin)), 0, 255),
                Clamp((int)((sourseColor.G - Gmin) * 255 / (Gmax - Gmin)), 0, 255),
                Clamp((int)((sourseColor.B - Bmin) * 255 / (Bmax - Bmin)), 0, 255));
        }
    }
}
