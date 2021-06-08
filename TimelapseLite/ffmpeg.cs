using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;

namespace mjpeg_handler
{
    class Image2Mp4 : IDisposable
    {
        Stream s;
        Process p;
        Size res;

        public Image2Mp4(string ffmpeg, string videoFile, Size res, double fps)
        {
            string command = "-f image2pipe -y -framerate " + fps.ToString() + " -i - -c:v libx264 -vf format=yuv420p -r 25 -movflags +faststart \"" + videoFile + "\"";
            this.res = res;
            p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(ffmpeg, command);
            psi.UseShellExecute = false;
            psi.RedirectStandardInput = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;


            p.StartInfo = psi;
            if (p.Start())
            {
                s = p.StandardInput.BaseStream;
            }
        }
        public void ImageAdd(Image img)
        {
            byte[] buffer = new byte[10000];
            using (var ms = new MemoryStream())
            {
                using (var bmp = new Bitmap(img, res))
                {
                    bmp.Save(ms, ImageFormat.Png);

                }
                int read = 0;
                ms.Position = 0;
                do
                {
                    read = ms.Read(buffer, 0, buffer.Length);
                    s.Write(buffer, 0, read);
                } while (read != 0);
            }
        }
        public void Dispose()
        {
            s.Dispose();
            if (!p.HasExited)
            {
                p.WaitForExit();

            }
            try
            {
                p.Dispose();
            }
            catch (Exception e)
            {

            }
        }
    }
   
}
