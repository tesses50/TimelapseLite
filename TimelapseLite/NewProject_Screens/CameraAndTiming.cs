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
    public partial class CameraAndTiming : UserControl
    {
        public CameraAndTiming()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CameraAndTiming_Load(object sender, EventArgs e)
        {
            textBox2.TabIndex = 4;
            textBox3.TabIndex = 5;
            textBox4.TabIndex = 6;
            textBox5.TabIndex = 7;
            textBox6.TabIndex = 8;

            textBox7.TabIndex = 9;
            textBox8.TabIndex = 10;
            textBox9.TabIndex = 11;
            textBox10.TabIndex = 12;

            Selecter();
            CenterPanel();
        }
        public void Selecter()
        {
            if (radioButton1.Checked)
            {
                panel3.Visible = false;
                panel2.Location = new Point(8, 85);
                radioButton2.Location = new Point(8, 198);
                panel2.Visible = true;
            }
            else
            {
                panel2.Visible = false;
                panel3.Location = new Point(8, 104);
                radioButton2.Location = new Point(8, 85);
                panel3.Visible = true;
            }
        }
        public int Parse2(string v,int def=0)
        {
            int v2;
            if (!int.TryParse(v, out v2))
            {
                v2 = def;
            }
            return v2;
        }
        public void GetData(out TimeSpan _proj, out TimeSpan _vid,out string address,out int frames,out bool estimatedSel)
        {
            _proj = new TimeSpan((Parse2(textBox2.Text) * 365) + Parse2(textBox3.Text), Parse2(textBox4.Text), Parse2(textBox5.Text), Parse2(textBox6.Text));
            _vid = new TimeSpan(Parse2(textBox7.Text), Parse2(textBox8.Text), Parse2(textBox9.Text), Parse2(textBox10.Text));
            frames = Parse2(textBox11.Text, 1);
            address = textBox1.Text;
            estimatedSel = radioButton1.Checked;
        }
        public void SetData(TimeSpan _proj, TimeSpan _vid, string address, int frames,bool estimatedSel)
        {
            int years = _proj.Days / 365;
            int days = _proj.Days % 365;
            textBox2.Text = years.ToString();

            textBox3.Text = days.ToString();
            textBox4.Text = _proj.Hours.ToString();
            textBox5.Text = _proj.Minutes.ToString();
            textBox6.Text = _proj.Seconds.ToString();

            textBox7.Text = _vid.Days.ToString();
            textBox8.Text = _vid.Hours.ToString();
            textBox9.Text = _vid.Minutes.ToString();
            textBox10.Text = _vid.Seconds.ToString();

            textBox11.Text = frames.ToString();
            textBox1.Text = address;
            radioButton1.Checked = estimatedSel;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Selecter();
        }
        public void CenterPanel()
        {
            panel1.Location = new Point((this.Width / 2) - (panel1.Width / 2),
                (this.Height / 2) - (panel1.Height / 2));
        }

        private void CameraAndTiming_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }

    }
}
