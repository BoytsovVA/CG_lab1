using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Erosion:MatrixFilter
    {
        public Erosion()
        {
            kernel = new float[3, 3];
            kernel[0, 0] = 0.0f; kernel[0, 1] = 1.0f; kernel[0, 2] = 0.0f;
            kernel[1, 0] = 1.0f; kernel[1, 1] = 1.0f; kernel[1, 2] = 1.0f;
            kernel[2, 0] = 0.0f; kernel[2, 1] = 1.0f; kernel[2, 2] = 0.0f;
        }
        public Erosion(float[,] kernel)
        {
            this.kernel = kernel;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            //определяем радиус действия по оси Х
            int radiusX = kernel.GetLength(0) / 2;
            //определяем радиус действия по оси Y
            int radiusY = kernel.GetLength(0) / 2;

            Color resultColor = Color.White;

            byte min = 255;
            for(int l=-radiusY;l<=radiusY;l++)
                for(int k=-radiusX;k<=radiusX;k++)
                {
                    int idX = Clamp(x+k,0,sourceImage.Width-1);
                    int idY = Clamp(y + k, 0, sourceImage.Height - 1);
                    Color color = sourceImage.GetPixel(idX,idY);
                    int intensity = color.R;
                    if(color.R != color.G || color.R != color.B || color.G != color.B)
                    {
                        intensity = (int)(0.36*color.R+0.53*color.G+0.11*color.B);
                    }
                    if(kernel[k+radiusX,l+radiusY]>0 && intensity < min)
                    {
                        min = (byte)intensity;
                        resultColor = color;
                    }
                }
            return resultColor;
        }
    }
}
