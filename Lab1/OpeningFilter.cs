using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;


namespace Lab1
{
    class OpeningFilter:MatrixFilter
    {
        //protected new float[,] kernel = null;
        public OpeningFilter()
        {
            this.kernel = null;
        }

        public OpeningFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }
        public Bitmap ProcessImage(Bitmap sourceImage)
        {
            Dilation dilation;
            Erosion erosion;
            if (kernel != null)
            {
                dilation = new Dilation(this.kernel);
                erosion = new Erosion(this.kernel);
            }
            else
            {
                dilation = new Dilation();
                erosion = new Erosion();
            }
            return dilation.processImage(erosion.processImage(sourceImage));
        }
    }
}
