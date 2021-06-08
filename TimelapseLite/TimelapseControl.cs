using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mjpeg_handler
{
    public partial class TimelapseControl : UserControl
    {
        public TimelapseControl()
        {
            InitializeComponent();
        }
        internal void SetPicture(Bitmap ms)
        {
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke((MethodInvoker)delegate
                {
                    SetPictureA(ms);
                });
            }
            else
            {
 SetPictureA(ms);
            }
        }
        internal void SetPictureA(Bitmap ms)
        {
            
            if(pictureBox1.BackgroundImage != null)
            {
                pictureBox1.BackgroundImage.Dispose();
            }
            pictureBox1.BackgroundImage = ms;
            using (var g = Graphics.FromImage(pictureBox1.BackgroundImage))
            {
                g.DrawString(DateTime.Now.ToString("ddd MMM dd, yyyy HH:mm:ss"), new Font("Arial", 24, FontStyle.Bold), Brushes.White, new PointF(20, 20));

            }
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = onex ? 100 : GetInterval();
            timer1.Enabled = !timer1.Enabled;

            button1.Text = timer1.Enabled ? "Stop" : "Record";
        }
        bool onex = false;
        public int GetInterval()
        {
            if (Form1.Instance.ProjectLoaded)
            {
                if (Form1.Instance.Project.is_est)
                {
                    double estprojlen = Form1.Instance.Project.estprojlen.TotalSeconds;
                    double estvidlen = Form1.Instance.Project.estprojlen.TotalSeconds;

                    if (estvidlen == 0)
                    {

                        return 100;
                    }
                    else
                    {
                        return (int)(estprojlen / estvidlen) * 100;
                    }
                }
                else
                {
                    return Form1.Instance.Project.interval * 100;
                }
            }
            return 100;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            onex = !onex;
            button2.Text = onex ? "Disable 1x" : "Enable 1x";
            timer1.Interval = onex ? 100 : GetInterval();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.BackgroundImage != null)
            {
                try
                {
                    string f=Form1.Instance.Project.get_file();
                    pictureBox1.BackgroundImage.Save(f, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
