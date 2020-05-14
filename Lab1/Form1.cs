using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lab1
{
    public partial class Form1 : Form
    {
        Bitmap image;
        bool button = false;
        bool x1, x2, x3, x4, x5, x6, x7, x8, x9;
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files| *.png;*.jpg;*.bmp|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvertFilter filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрГауссаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void addBrightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BrightnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void повыситьРезкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (button == true)
            {
                button = false;
                Bitmap newImage = (image);
                Morphological a = new Morphological(newImage);
                a.ChangeMask(x1, x2, x3, x4, x5, x6, x7, x8, x9);
                newImage = a.Morphologicalopenning();
                pictureBox1.Image = newImage;
                image = newImage;
                pictureBox1.Refresh();
            }

            else
            {
                Bitmap newImage = (image);
                Morphological a = new Morphological(newImage);
                newImage = a.Morphologicalopenning();
                pictureBox1.Image = newImage;
                image = newImage;
                pictureBox1.Refresh();
            }
        }

        private void gradToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (button == true)
            {
                button = false;
                Bitmap newImage = (image);
                Morphological a = new Morphological(newImage);
                a.ChangeMask(x1, x2, x3, x4, x5, x6, x7, x8, x9);
                newImage = a.MorphologicalGrad();
                pictureBox1.Image = newImage;
                image = newImage;
                pictureBox1.Refresh();
            }

            else
            {
                Bitmap newImage = (image);
                Morphological a = new Morphological(newImage);
                newImage = a.MorphologicalGrad();
                pictureBox1.Image = newImage;
                image = newImage;
                pictureBox1.Refresh();
            }
        }

        private void медианныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedianFilter filter = new MedianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.ToString() == "") || (textBox2.ToString() == "") || (textBox3.ToString() == "") || (textBox4.ToString() == "") || (textBox5.ToString() == "") || (textBox6.ToString() == "") || (textBox7.ToString() == "") || (textBox8.ToString() == "") || (textBox9.ToString() == ""))
            {
                MessageBox.Show("Вы ввели не все данные");
            }
            else
            {

                if (textBox1.ToString() == "true") x1 = true;
                else x1 = false;
                if (textBox2.ToString() == "true") x2 = true;
                else x2 = false;
                if (textBox3.ToString() == "true") x3 = true;
                else x3 = false;
                if (textBox4.ToString() == "true") x4 = true;
                else x4 = false;
                if (textBox5.ToString() == "true") x5 = true;
                else x5 = false;
                if (textBox5.ToString() == "true") x6 = true;
                else x6 = false;
                if (textBox7.ToString() == "true") x7 = true;
                else x7 = false;
                if (textBox8.ToString() == "true") x8 = true;
                else x8 = false;
                if (textBox9.ToString() == "true") x9 = true;
                else x9 = false;
                button = true;
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayWorldFilter();
            filter.calculateAvg(image);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void линейноеРастяжениеГистограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newImage = (image);
            Filters filter = new LinearHistogram(newImage);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (button == true)
            {
                button = false;
                Bitmap newImage = (image);
                Morphological a = new Morphological(newImage);
                a.ChangeMask(x1, x2, x3, x4, x5, x6, x7, x8, x9);
                newImage = a.Morphologicalclosing();
                pictureBox1.Image = newImage;
                image = newImage;
                pictureBox1.Refresh();
            }

            else
            {
                Bitmap newImage = (image);
                Morphological a = new Morphological(newImage);
                newImage = a.Morphologicalclosing();
                pictureBox1.Image = newImage;
                image = newImage;
                pictureBox1.Refresh();
            }
        }

        private void волныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newImage = (image);
            Filters filter = new WavesFilter(newImage);
            backgroundWorker1.RunWorkerAsync(filter);

        }

        private void эффектстеклаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newImage = (image);
            Filters filter = new GlassFilter(newImage);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            Bitmap newImage = (filter.processImage(image));
            image = newImage;

            filter = new EmbossingFilter(newImage);
            newImage = (filter.processImage(image));
            image = newImage;


            backgroundWorker1.RunWorkerAsync(filter);

        }

        private void резкость1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharpnessFilter2();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void идеальныйОтражательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newImage = (image);
            Filters filter = new PerfectReflectorFilter(image);
            backgroundWorker1.RunWorkerAsync(filter);
        }


    }
}
