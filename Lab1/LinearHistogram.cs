using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lab1
{
    class LinearHistogram : Filters
    {

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color color = sourceImage.GetPixel(x,y);
            Color resultColor = Color.FromArgb(Clamp((color.R-minR)*(255-0)/(maxR-minR),0,255), Clamp((color.G - minG) * (255-0) / (maxG - minG), 0, 255), Clamp((color.B - minB) * (255-0) / (maxB - minB), 0, 255));
            return resultColor;
        }
    }
}
