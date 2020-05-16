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

        float[,] tmp = null;
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
                tmp = new float[3, 3];
                if (float.Parse(textBox1.Text) == 1) tmp[0,0] = 1;
                else tmp[0, 0] = 0;
                if (float.Parse(textBox2.Text) == 1) tmp[0, 1] = 1;
                else tmp[0, 1] = 0;
                if (float.Parse(textBox3.Text) == 1) tmp[0, 2] = 1;
                else tmp[0, 2] = 0;
                if (float.Parse(textBox4.Text) == 1) tmp[1, 0] = 1;
                else tmp[1, 0] = 0;
                if (float.Parse(textBox5.Text) == 1) tmp[1, 1] = 1;
                else tmp[1, 1] = 0;
                if (float.Parse(textBox6.Text) == 1) tmp[1, 2] = 1;
                else tmp[1, 2] = 0;
                if (float.Parse(textBox7.Text) == 1) tmp[2, 0] = 1;
                else tmp[2, 0] = 0;
                if (float.Parse(textBox8.Text) == 1) tmp[2, 1] = 1;
                else tmp[2, 1] = 0;
                if (float.Parse(textBox9.Text) == 1) tmp[2, 2] = 1;
                else tmp[2, 2] = 0;
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dilationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Dilation();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Erosion();
            backgroundWorker1.RunWorkerAsync(filter);
        }


        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayWorldFilter();
            filter.calculateAvg(image);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void линейноеРастяжениеГистограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new LinearHistogram();
            filter.calculateMinMax(image);
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpeningFilter filter = new OpeningFilter(tmp);
            Bitmap newImage;
            if (tmp == null)
                newImage = filter.ProcessImage(image);
            else
                newImage = filter.processImage(image); 
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
            pictureBox1.Image = image;
            pictureBox1.Refresh();
        }

        private void gradToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GradFilter filter = new GradFilter();
            Bitmap newImage = filter.ProcessImage(image);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
            pictureBox1.Image = image;
            pictureBox1.Refresh();
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClosingFilter filter = new ClosingFilter(tmp);
            Bitmap newImage;
            if (tmp == null)
                 newImage = filter.ProcessImage(image);
            else
                 newImage = filter.processImage(image);


            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
            pictureBox1.Image = image;
            pictureBox1.Refresh();
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
