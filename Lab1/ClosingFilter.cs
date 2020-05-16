using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class ClosingFilter:MatrixFilter
    {
        public ClosingFilter()
        {
            this.kernel = null;
        }
        public ClosingFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }
        public Bitmap ProcessImage(Bitmap sourceImage)
        {
            Dilation dilation;
            Erosion erosion;
            if(kernel != null)
            {
                dilation = new Dilation(this.kernel);
                erosion = new Erosion(this.kernel);
            }
            else
            {
                dilation = new Dilation();
                erosion = new Erosion();
            }
            return erosion.processImage(dilation.processImage(sourceImage));
        }

    }
}
