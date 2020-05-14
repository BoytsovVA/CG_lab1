using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab1
{
    class MedianFilter : MatrixFilter
    {
        public MedianFilter()
        {
            kernel = new float[3, 3];
        }
        public MedianFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }
        private int countMedian(int[] A)
        {
            Array.Sort(A);
            return A[5];
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int[] G1 = new int[9];
            int[] G2 = new int[9];
            int[] G3 = new int[9];
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            int m = 0;
            for (int k = -radiusX; k <= radiusX; k++)
                for (int l = -radiusY; l <= radiusY; l++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighbourColor = sourceImage.GetPixel(idX, idY);
                    G1[m] = neighbourColor.R;
                    G2[m] = neighbourColor.G;
                    G3[m] = neighbourColor.B;
                    m++;
                }
            return Color.FromArgb(countMedian(G1), countMedian(G2), countMedian(G3));
        }
    }
}
