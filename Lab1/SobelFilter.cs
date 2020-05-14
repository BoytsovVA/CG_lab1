
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab1
{
    class SobelFilter : MatrixFilter
    {
        public SobelFilter()
        {
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];
        }
        protected int calculateSobel(int[,] A)
        {
            float res1 = 0;
            float res2 = 0;
            res1 = (float)Math.Pow(((A[2, 0] + A[2, 1] + A[2, 2]) - (A[0, 0] + A[0, 1] + A[0, 2])), 2);
            res2 = (float)Math.Pow(((A[0, 2] + A[1, 2] + A[2, 2]) - (A[0, 0] + A[1, 0] + A[2, 0])), 2);
            return (int)Math.Sqrt(Math.Abs(res1 + res2));
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int[,] G1 = new int[3, 3];
            int[,] G2 = new int[3, 3];
            int[,] G3 = new int[3, 3];
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            int i = 0, j = 0;
            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighbourColor = sourceImage.GetPixel(idX, idY);
                    G1[i, j] = neighbourColor.R;
                    G2[i, j] = neighbourColor.G;
                    G3[i, j] = neighbourColor.B;
                    j++;
                }
                i++;
                j = 0;
            }

            return Color.FromArgb(Clamp(calculateSobel(G1), 0, 255), Clamp(calculateSobel(G2), 0, 255), Clamp(calculateSobel(G3), 0, 255));
        }
    }
}
