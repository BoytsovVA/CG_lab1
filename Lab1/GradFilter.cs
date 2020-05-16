using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class GradFilter : Filters
    {
        Bitmap mMinuedImage = null;
        public Bitmap ProcessImage(Bitmap sourceImage)
        {
            Dilation dilation = new Dilation();
            Erosion erosion = new Erosion();
            HelperClass subtraction = new HelperClass(dilation.processImage(sourceImage));
            return subtraction.processImage(erosion.processImage(sourceImage));
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
