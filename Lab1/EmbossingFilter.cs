using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab1
{
    class EmbossingFilter : MatrixFilter
    {
        public EmbossingFilter(Bitmap sourceImage)
        {
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];
        }
        protected int calculateEmbossing(int[,] A)
        {
            int res = 0;
            res = (A[0, 1] + A[1, 0]) - (A[1, 2] + A[2, 1]) + 255;
            return res;
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
            return Color.FromArgb((Clamp(calculateEmbossing(G1) / 2, 0, 255)), (Clamp(calculateEmbossing(G2) / 2, 0, 255)), (Clamp(calculateEmbossing(G3) / 2, 0, 255)));
        }
    }
}
