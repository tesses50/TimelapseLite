using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using mjpeg_handler.NewProject_Screens;
namespace mjpeg_handler
{
    public partial class NewProject : UserControl
    {
        NewProject_Screens.FileLoc fileloc;
        NewProject_Screens.CameraAndTiming camera_timing;
        public NewProject()
        {
            InitializeComponent();
            fileloc = new NewProject_Screens.FileLoc();
            camera_timing = new NewProject_Screens.CameraAndTiming();
        }
        int pgid = 0;
        private void NewProject_Load(object sender, EventArgs e)
        {
          
            Form1.SetPage(groupBox1, fileloc);
           // Hitme();
        }
        public void Hitme()
        {
            pgid = 0;
            Form1.SetPage(groupBox1, fileloc);
            fileloc.Hitme();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            pgid--;

            CallPg();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pgid++;
            CallPg();
        }
        private void LookForProject(Action<TimeSpan, TimeSpan, string, int, bool> ac)
        {
            //estprojlen estvidlen addr fps is_est
            string p = fileloc.get_project_path() + ".tll";
            if (System.IO.File.Exists(p))
            {
                project_file pr = project_file.Open(p);
                ac(pr.estprojlen, pr.estvidlen, pr.addr, pr.interval, pr.is_est);
            }

        }
       
        private void CallPg()
        {
            if (pgid < 0)
            {
                pgid = 0;
            }
            if (pgid > 1)
            {
                project_file pr = new project_file();
                camera_timing.GetData(out pr.estprojlen, out pr.estvidlen, out pr.addr,out pr.interval, out pr.is_est);
                pr.path = fileloc.get_project_path() + ".tll";
                pr.Save();
                Form1.Instance.LoadProject(pr);
            }
            else
            {
                switch (pgid)
                {
                    case 0:
                        Form1.SetPage(groupBox1, fileloc);
                        break;
                    case 1:
                        
                        Form1.SetPage(groupBox1, camera_timing);
                        LookForProject(camera_timing.SetData);
                        break;
                }
            }
        }
    }
}
