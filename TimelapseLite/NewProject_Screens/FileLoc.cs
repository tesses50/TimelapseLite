using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mjpeg_handler.NewProject_Screens
{
    public partial class FileLoc : UserControl
    {
        public FileLoc()
        {
         
            InitializeComponent();
        }
        public void CenterPanel()
        {
            panel1.Location = new Point((this.Width / 2) - (panel1.Width / 2),
                (this.Height / 2) - (panel1.Height / 2));
        }
        public string get_project_path()
        {
            ProjectLocation p = (ProjectLocation)comboBox1.SelectedItem;
           return System.IO.Path.Combine(p.GetLocation(), textBox1.Text);
        }
        private void FileLoc_Load(object sender, EventArgs e)
        {
            CenterPanel();
        }

        private void FileLoc_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }
        public void Hitme()
        {
            comboBox1.Items.Clear();
            foreach (var drive in System.IO.DriveInfo.GetDrives())
            {
                comboBox1.Items.Add(new ProjectLocation(drive));
            }
            comboBox1.SelectedIndex = 0;
        }
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                ProjectLocation p = (ProjectLocation)comboBox1.SelectedItem;
                p.ShowLocation();
            }
        }
    }
}
