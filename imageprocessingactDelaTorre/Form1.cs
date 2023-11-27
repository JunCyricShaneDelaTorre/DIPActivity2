using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebCamLib;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Diagnostics.Tracing;

namespace imageprocessingactDelaTorre
{
    public partial class Form1 : Form
    {
        Bitmap loaded;
        Bitmap result;
        Bitmap imageB, imageA, resultGreen;
        Device camDevice;
        

        public Form1()
        {
            InitializeComponent();
            
           
        }
       

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }


        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }


        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            

        }

        private void greyScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            result = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for(int i = 0;i < loaded.Width;i++)
            {
                for (int j = 0; j < loaded.Height;j++)
                {
                    pixel= loaded.GetPixel(i,j);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    Color c = Color.FromArgb(grey, grey, grey);
                    result.SetPixel(i, j, c);
                }
            }
            pictureBox2.Image = result;
        }

        private void colorIversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            result = new Bitmap(loaded.Width,loaded.Height);
            Color pixel;
            for (int i = 0; i < loaded.Width;i++)
            {
                for(int j = 0;j < loaded.Height;j++)
                {
                    pixel = loaded.GetPixel(i,j);
                    Color c = Color.FromArgb(255 - pixel.R,255-pixel.G,255-pixel.B);
                    result.SetPixel(i, j, c);
                }
            }
            pictureBox2.Image = result;
        }

       

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            result.Save(saveFileDialog1.FileName);
        }

        private void basicCopyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            result = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    result.SetPixel(i, j, pixel);
                }
            }
            pictureBox2.Image = result;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //convert everything to greyscale
            result = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    Color c = Color.FromArgb(grey, grey, grey);
                    result.SetPixel(i, j, c);
                }
            }
            //create histogram data array
            int[] histData = new int[256];
            Color hist;
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    hist = result.GetPixel(i, j);
                    histData[hist.R]++;
                }
            }
            //create bitmap for the graph
            Bitmap diagram = new Bitmap(256,1000);
            //create background for graph
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    diagram.SetPixel(i, j,Color.White);

                }
            }
            //plot data
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < Math.Min(histData[i]/10,1000); j++)
                {
                   diagram.SetPixel(i,999-j,Color.Black);
                }
            }
           
            pictureBox2.Image = diagram;

        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            result = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            int r,g,b;
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    int tr = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    int tg = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    int tb = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);

                    if(tr > 255)
                    {
                        r = pixel.R;
                    }
                    else 
                    {
                        r = tr;
                    }

                    if (tg > 255)
                    {
                        g = pixel.G;
                    }
                    else
                    {
                        g = tg;
                    }

                    if (tb > 255)
                    {
                       b = pixel.B;
                    }
                    else
                    {
                        b = tb;
                    }
                    Color color = Color.FromArgb(r,g,b);
                    result.SetPixel(i, j, color);
                }
            }
            pictureBox2.Image = result;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void loadbgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog3.FileName);
            pictureBox2.Image = imageA;
        }

        private void subtractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //imageA is the bg of imageB
            Color g = Color.FromArgb(0, 0, 255);
            int greyG = (g.R+g.G+g.B)/3;
            int threshold = 10;
            resultGreen = new Bitmap (imageB.Width, imageB.Height);
            

            for(int i = 0; i < imageB.Width; i++)
            {
                for(int j = 0; j < imageB.Height; j++)
                {
                    Color pixel = imageB.GetPixel(i, j);
                    Color bgPixel = imageA.GetPixel(i, j);
                    int grey = (pixel.R + pixel.G + pixel.B)/3;
                    int subtractValue = Math.Abs(grey - greyG);
                    if(subtractValue < threshold)
                    {
                        resultGreen.SetPixel(i, j, bgPixel);
                    }
                    else
                    {
                        resultGreen.SetPixel(i, j, pixel);
                    }
                }
            }
            pictureBox3.Image = resultGreen;
        }

        private void onCamToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Device[] devices = DeviceManager.GetAllDevices();

            if (devices.Length > 0)
            {
                camDevice = devices[0]; // Assuming you want to use the first detected device
                camDevice.ShowWindow(pictureBox1);
            }
            else
            {
                MessageBox.Show("No webcam devices found.");
            }
        }

        private void offCamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            camDevice.Stop();

        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
           

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog2.FileName);
            pictureBox1.Image = imageB;
        }

        /*
        public void brightness(ref Bitmap a, ref Bitmap b)
        {

        }

        public static void Equalisation() 
        {

        }
        */


    }
}
