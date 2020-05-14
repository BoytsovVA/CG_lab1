﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace Lab1
{
    abstract class Filters
    {
        public int sumR = 0;
        public int sumG = 0;
        public int sumB = 0;
        public int avg = 0;
        public Bitmap processImage(Bitmap sourceImage)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
               // worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }
        protected abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y);

        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        public void calculateAvg(Bitmap sourceImage)
        {
            for (int i = 0; i < sourceImage.Width; i++)
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color sourceColor = sourceImage.GetPixel(i, j);
                    sumR += sourceColor.R;
                    sumG += sourceColor.G;
                    sumB += sourceColor.B;
                }
            sumR /= (sourceImage.Width * sourceImage.Height);
            sumG /= (sourceImage.Width * sourceImage.Height);
            sumB /= (sourceImage.Width * sourceImage.Height);
            if (sumR == 0)
                sumR = 255;
            if (sumG == 0)
                sumG = 255;
            if (sumB == 0)
                sumB = 255;
            avg = (sumR + sumG + sumB) / 3;

        }
    }
}