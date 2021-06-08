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
    public partial class OpenProject : UserControl
    {
        public OpenProject()
        {
            InitializeComponent();
        }

        private void OpenProject_Load(object sender, EventArgs e)
        {

        }
        public void Hitme()
        {
            listBox1.Items.Clear();
            foreach (var drive in System.IO.DriveInfo.GetDrives())
            {
                NewProject_Screens.ProjectLocation p = new NewProject_Screens.ProjectLocation(drive);
                if (p.CanWork())
                {
                    foreach (var files in System.IO.Directory.GetFiles(p.GetLocation(), "*.tll"))
                    {
                        var p2 = project_file.Open(files);
                        p2._stordev = p.ToString();
                        listBox1.Items.Add(p2);

                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                Form1.Instance.LoadProject((project_file)listBox1.SelectedItem);
            }
        }
        
    }
}
