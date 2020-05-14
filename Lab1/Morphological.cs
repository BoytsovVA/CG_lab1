using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Morphological
    {
        bool[,] Mask = new bool[,]  {{ false, true, true},
               { true, true,false},
               { true, false, true } };
        Bitmap res;
        int width;
        int height;
        int[,] allPixR;
        int[,] allPixG;
        int[,] allPixB;
        int[,] resPixR;
        int[,] resPixG;
        int[,] resPixB;

        int[,] allPixR1;
        int[,] allPixG1;
        int[,] allPixB1;
        int[,] resPixR1;
        int[,] resPixG1;
        int[,] resPixB1;



        int[,] allPixR2;
        int[,] allPixG2;
        int[,] allPixB2;
        int[,] resPixR2;
        int[,] resPixG2;
        int[,] resPixB2;



        public Morphological(Bitmap scr)
        {
            res = new Bitmap(scr);

            width = scr.Width;
            height = scr.Height;

            Mask = new bool[,]  {{ false, true, false},
               { false, true,false},
               { true, false, true } };

            allPixR = new int[width, height];
            allPixG = new int[width, height];
            allPixB = new int[width, height];
            resPixR = new int[width, height];
            resPixG = new int[width, height];
            resPixB = new int[width, height];

            allPixR1 = new int[width, height];
            allPixG1 = new int[width, height];
            allPixB1 = new int[width, height];
            resPixR1 = new int[width, height];
            resPixG1 = new int[width, height];
            resPixB1 = new int[width, height];

            allPixR2 = new int[width, height];
            allPixG2 = new int[width, height];
            allPixB2 = new int[width, height];
            resPixR2 = new int[width, height];
            resPixG2 = new int[width, height];
            resPixB2 = new int[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    allPixR[i, j] = scr.GetPixel(i, j).R;
                    allPixG[i, j] = scr.GetPixel(i, j).G;
                    allPixB[i, j] = scr.GetPixel(i, j).B;

                    allPixR1[i, j] = scr.GetPixel(i, j).R;
                    allPixG1[i, j] = scr.GetPixel(i, j).G;
                    allPixB1[i, j] = scr.GetPixel(i, j).B;

                    allPixR2[i, j] = scr.GetPixel(i, j).R;
                    allPixG2[i, j] = scr.GetPixel(i, j).G;
                    allPixB2[i, j] = scr.GetPixel(i, j).B;
                }
            }

        }

        public void ChangeMask(bool x1, bool x2, bool x3, bool x4, bool x5, bool x6, bool x7, bool x8, bool x9)
        {
            Mask = new bool[,] { { x1, x2, x3 },
               { x4, x5, x6 },
               { x7, x8, x9} };
        }
        public Bitmap Morphologicalclosing()
        {

            // Применяем расширение
            Dilation(allPixR, allPixG, allPixB, Mask, resPixR, resPixG, resPixB, width, height);
            Dilation(allPixR, allPixG, allPixB, Mask, resPixR, resPixG, resPixB, width, height);

            // Применияем сужение
            Erosion(resPixR, resPixG, resPixB, Mask, allPixR, allPixG, allPixB, width, height);
            Erosion(resPixR, resPixG, resPixB, Mask, allPixR, allPixG, allPixB, width, height);


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                    res.SetPixel(i, j, Color.FromArgb(allPixR[i, j], allPixG[i, j], allPixB[i, j]));
            }

            return res;
        }

        public Bitmap Morphologicalopenning()
        {
            // Применияем сужение
            Erosion(allPixR, allPixG, allPixB, Mask, resPixR, resPixG, resPixB, width, height);
            // Применяем расширение
            Dilation(resPixR, resPixG, resPixB, Mask, allPixR, allPixG, allPixB, width, height);



            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                    res.SetPixel(i, j, Color.FromArgb(allPixR[i, j], allPixG[i, j], allPixB[i, j]));
            }

            return res;
        }
        public Bitmap MorphologicalGrad()
        {

            Dilation(allPixR1, allPixG1, allPixB1, Mask, resPixR1, resPixG1, resPixB1, width, height);
            Erosion(allPixR2, allPixG2, allPixB2, Mask, resPixR2, resPixG2, resPixB2, width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    resPixR[x, y] = resPixR1[x, y] - resPixR2[x, y];
                    resPixG[x, y] = resPixG1[x, y] - resPixG2[x, y];
                    resPixB[x, y] = resPixB1[x, y] - resPixB2[x, y];
                    allPixR[x, y] = allPixR1[x, y] - allPixR2[x, y];
                    allPixG[x, y] = allPixG1[x, y] - allPixG2[x, y];
                    allPixB[x, y] = allPixB1[x, y] - allPixB2[x, y];

                }
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color color1 = Color.FromArgb(resPixR[i, j], resPixG[i, j], resPixB[i, j]);
                    res.SetPixel(i, j, color1);
                }
            }

            return res;
        }
        public void Dilation(int[,] srcR, int[,] srcG, int[,] srcB, bool[,] mask, int[,] ResR, int[,] ResG, int[,] ResB, int width, int height)
        {
            int MW = 2, MH = 2;

            for (int y = MH / 2; y < height - MH / 2; y++)
            {
                for (int x = MW / 2; x < width - MW / 2; x++)
                {
                    int maxR = 0;
                    int maxG = 0;
                    int maxB = 0;

                    for (int j = -MH / 2; j <= MH / 2; j++)
                    {
                        for (int i = -MW / 2; i <= MW / 2; i++)
                        {
                            if ((mask[i + 1, j + 1]) && (srcR[x + i, y + j] > maxR))
                                maxR = srcR[x + i, y + j];
                            if ((mask[i + 1, j + 1]) && (srcG[x + i, y + j] > maxG))
                                maxG = srcG[x + i, y + j];
                            if ((mask[i + 1, j + 1]) && (srcB[x + i, y + j] > maxB))
                                maxB = srcB[x + i, y + j];

                        }
                    }

                    ResR[x, y] = maxR;
                    ResG[x, y] = maxG;
                    ResB[x, y] = maxB;
                }
            }
        }
        void Erosion(int[,] srcR, int[,] srcG, int[,] srcB, bool[,] mask, int[,] ResR, int[,] ResG, int[,] ResB, int width, int height)
        {
            int MW = 3, MH = 3;

            for (int y = MH / 2; y < height - MH / 2; y++)
            {
                for (int x = MW / 2; x < width - MW / 2; x++)
                {
                    int minR = 255;
                    int minG = 255;
                    int minB = 255;
                    for (int j = -MH / 2; j <= MH / 2; j++)
                    {
                        for (int i = -MW / 2; i <= MW / 2; i++)
                        {
                            if ((mask[i + 1, j + 1]) && (srcR[x + i, y + j] < minR))
                                minR = srcR[x + i, y + j];
                            if ((mask[i + 1, j + 1]) && (srcG[x + i, y + j] < minG))
                                minG = srcG[x + i, y + j];
                            if ((mask[i + 1, j + 1]) && (srcB[x + i, y + j] < minB))
                                minB = srcB[x + i, y + j];

                        }
                    }

                    ResR[x, y] = minR;
                    ResG[x, y] = minG;
                    ResB[x, y] = minB;
                }
            }
        }
    }
}
