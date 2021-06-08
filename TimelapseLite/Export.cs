using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace mjpeg_handler
{
    public partial class Export : UserControl
    {
        public Export()
        {
            InitializeComponent();
        }
        private bool ReadTXT(string txt,ref string ff)
        {

            if (System.IO.File.Exists(txt))
            {
               ff= System.IO.File.ReadAllText(txt);
               return System.IO.File.Exists(ff);
            }
            return false;
        }
        public void CenterPanel()
        {
            panel1.Location = new Point((this.Width / 2) - (panel1.Width / 2),
                (this.Height / 2) - (panel1.Height / 2));
        }
        string ff = "ffmpeg";
        public bool Hitme()
        {
            CenterPanel();
            try
            {

                using (System.Diagnostics.Process p = new System.Diagnostics.Process())
                {
                    p.StartInfo = new ProcessStartInfo(ff) { CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden };
                    p.Start();
                }
                
            }
            catch (Exception ex)
            {
                string ffmpegtxt = System.IO.Path.Combine(NewProject_Screens.ProjectLocation.InternalStore, "ffmpeg.txt");
                
                if (ReadTXT(ffmpegtxt,ref ff))
                {
                    return true;
                }
                else
                {
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Filter = "FFmpeg|ffmpeg;ffmpeg.exe;avconv;avconv.exe|All EXEs(Windows Only)|*.exe|All Files|*.*";
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            ff = ofd.FileName;
                            System.IO.File.WriteAllText(ffmpegtxt, ff);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            textBox1.Text = "1920";
            textBox2.Text = "1080";
            textBox3.Text = "";
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ffmpegFilter ="MPEG-4 Part 10|*.mp4";
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = ffmpegFilter;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    textBox3.Text = sfd.FileName;
                }
            }
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {

                string pathForVideo = textBox3.Text;
                if (string.IsNullOrWhiteSpace(textBox3.Text) )
                {
                    MessageBox.Show("You can not create file without name", "TimelapseLite",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                    return;
                }
                if (!Path.HasExtension(pathForVideo))
                {
                    pathForVideo = pathForVideo.TrimEnd('.') + ".mp4";
                }
                if (!Path.IsPathRooted(pathForVideo))
                {
                    pathForVideo = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos, Environment.SpecialFolderOption.Create), pathForVideo);

                }
                bool oktoWrite = true;
                oktoWrite = !System.IO.File.Exists(pathForVideo);
                if (!oktoWrite)
                {
                    if (MessageBox.Show(string.Format("Video \"{0}\" exists overwrite?", pathForVideo), "TimelapseLite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        return;
                    }
                }
                int files = System.IO.Directory.GetFiles(Form1.Instance.Project.dirpath, "*.jpg").Length;
                label4.Text = "/" + files.ToString();
                label4.Refresh();
                int w;
                int h;
                if (!int.TryParse(textBox1.Text, out w))
                {
                    w = 1920;

                }
                if (!int.TryParse(textBox2.Text, out h))
                {
                    h = 1080;

                }
                Image2Mp4 i2mp4 = new Image2Mp4(ff, pathForVideo, new Size(w, h), 10);
                button2.Text = "Cancel";
                backgroundWorker1.RunWorkerAsync(new object[] { files, i2mp4, pathForVideo });
            }
            else
            {
                backgroundWorker1.CancelAsync();
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label3.Text = e.UserState.ToString();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            int files = (int)args[0];
            Image2Mp4 i2mp4 = (Image2Mp4)args[1];
            //string res = args[2];
            for (int i = 0; i < files; i++)
            {
                int per=(int)(((double)i / (double) files) * 100.0);
                if(per > 100){
                    per=100;
                }
                if(per < 0){
                    per=0;
                }
                backgroundWorker1.ReportProgress(per, i);
                string src = System.IO.Path.Combine(Form1.Instance.Project.dirpath, i.ToString() + ".jpg");
                using (var img = Image.FromFile(src))
                {
                    i2mp4.ImageAdd(img);
                }
                if (backgroundWorker1.CancellationPending)
                {
                    break;
                }
            }
            i2mp4.Dispose();
            e.Result = args[2];
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (MessageBox.Show("Video Creation Complete, Play?","TimelapseLite", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string res = (string)e.Result;
                if (Environment.OSVersion.Platform == PlatformID.Win32Windows || Environment.OSVersion.Platform == PlatformID.Win32NT || Environment.OSVersion.Platform == PlatformID.Win32S || Environment.OSVersion.Platform == PlatformID.WinCE)
                {
                    Process.Start(res);
                }
                else
                {
                    try
                    {
                        Process.Start("xdg-open", "\"" + res + "\"");
                    }
                    catch (System.IO.FileLoadException ex)
                    {

                        System.Windows.Forms.MessageBox.Show("xdg-open not found, perhaps you are using mac", "TimelapseLite");

                    }
                    catch (Exception ex)
                    {

                    }
                }
                
            }
            button2.Text = "Create";
        }

        private void Export_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }
        
    }
}
