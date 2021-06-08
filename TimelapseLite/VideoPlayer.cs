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
    public partial class VideoPlayer : UserControl
    {
        public VideoPlayer()
        {
            InitializeComponent();
        }
        public void Hitme()
        {
            
            if (System.IO.Directory.Exists(Form1.Instance.Project.dirpath))
            {
                
                trackBar1.Maximum = System.IO.Directory.GetFiles(Form1.Instance.Project.dirpath, "*.jpg").Length;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int value = trackBar1.Value +1;
            if (value > trackBar1.Maximum)
            {
                trackBar1.Value = 0;
                timer1.Enabled = checkBox1.Checked;
                button1.Text = timer1.Enabled ? "Pause" : "Play";
            }
            else
            {
                trackBar1.Value = value;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int frame = trackBar1.Value;
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "JPEG|*.jpg";
              
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                   string src= System.IO.Path.Combine(Form1.Instance.Project.dirpath, frame.ToString() + ".jpg");
                   System.IO.File.Copy(src, sfd.FileName);
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            button1.Text = timer1.Enabled ? "Pause" : "Play";
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            string src = System.IO.Path.Combine(Form1.Instance.Project.dirpath,trackBar1.Value.ToString() + ".jpg");
            if (pictureBox1.BackgroundImage != null)
            {
                pictureBox1.BackgroundImage.Dispose();
            }
            if (System.IO.File.Exists(src))
            {
                pictureBox1.BackgroundImage = new Bitmap(src);
            }
        }
    }
}
