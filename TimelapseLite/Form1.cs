using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mjpeg_handler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            NewProject = new NewProject();
            OpenProject = new OpenProject();
            TimelapsePage = new TimelapseControl();
            Vidplayer = new VideoPlayer();
            Instance = this;
            Export = new Export();
        }
        NewProject NewProject;
        OpenProject OpenProject;
        TimelapseControl TimelapsePage;
        VideoPlayer Vidplayer;
        Export Export;
        private void button1_Click(object sender, EventArgs e)
        {
          //  string[] v = parse(textBox1.Text);
            //string parsed = string.Format("Scheme: {0}\nUsername: {1}\nPassword: {2}\nHost: {3}\nPort: {4}\nPath: {5}", v);
           //StartReading(textBox1.Text,new_frame);
        }
     
         // bye bye my own code that doesnt work
        /*private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            Uri u = new Uri(Project.addr);

        
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                client.Connect(u.Host, u.Port);
            try
            {
                //MessageBox.Show(client.Connected.ToString());
                using (var s2 = client.GetStream())
                {
                    //my request

                    string req = string.Format("GET {0} HTTP/1.0\r\n", u.PathAndQuery);

                    if (!string.IsNullOrWhiteSpace(u.UserInfo) && u.UserInfo.Contains(':'))
                    {

                        req += "Authorization: Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(u.UserInfo));
                    }
                    req += "\r\n";
                    //MessageBox.Show(req + "MessageStart");
                    s2.Write(Encoding.UTF8.GetBytes(req), 0, req.Length);
                    bool inhdr = true;
                    bool inminiheader = false;
                    bool readfirstpart = false;
                    int sz = 0;
                    string boundary = "--dcmjpeg";
                    int jpegoff = 0;
                    byte[] jpeg = null;
                    do
                    {
                        if (client.Available > 0)
                        {
                            byte[] respose = new byte[4096];

                            int read = s2.Read(respose, 0, respose.Length);

                            

                            string v22 = Encoding.UTF8.GetString(respose, 0, read);
                            if (inhdr)
                            {
                                if (v22.StartsWith("HTTP/1.1 200 OK"))
                                {
                                    int contype = v22.IndexOf("Content-Type: ") + "Content-Type: ".Length;
                                    int cend = v22.IndexOf('\r', contype);
                                    string content_type = v22.Substring(contype, cend - contype);
                                    string b = "multipart/x-mixed-replace;boundary=";
                                    if (content_type.StartsWith(b))
                                    {
                                        boundary = content_type.Substring(b.Length);
                                        inhdr = false;
                                        inminiheader = true;
                                        continue;
                                    }

                                }
                            }
                            else
                            {
                                int offset = 0;
                                if (inminiheader)
                                {
                                    if (!readfirstpart)
                                    {
                                        if (v22.StartsWith(boundary))
                                        {


                                            if (v22.Contains("\r\n\r\n"))
                                            {
                                                int offset22 = v22.IndexOf("\r\n\r\n");
                                                offset = offset22 + 4;
                                                string data = v22.Remove(offset22);
                                                string ctl = "Content-Length: ";
                                                int ctlpos = data.IndexOf(ctl) + ctl.Length;

                                                int len = data.IndexOf('\r', ctlpos);
                                                if(len >= ctlpos){
                                                    
                                                sz = int.Parse(data.Substring(ctlpos, len- ctlpos));
                                                }else{
                                                sz = int.Parse(data.Substring(ctlpos));
                                                }
                                                inminiheader = false;
                                                jpeg = new byte[sz];
                                                jpegoff = 0;

                                            }
                                            else
                                            {
                                                readfirstpart = true;
                                                continue;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int offset22 = v22.IndexOf("\r\n\r\n");
                                        if (offset22 == -1)
                                        {

                                            sz = int.Parse(v22);
                                            readfirstpart = false;
                                            inminiheader = false;
                                            jpeg = new byte[sz];
                                            jpegoff = 0;
                                        }
                                        else
                                        {
                                            offset = offset22 + 4;
                                            sz = int.Parse(v22.Substring(0, offset22));
                                            readfirstpart = false;
                                            inminiheader = false;
                                            jpeg = new byte[sz];
                                            jpegoff = 0;
                                        }
                                    }
                                }



                                if (!inminiheader)
                                {
                                    int readBytes = read - offset;
                                    int newread = Math.Min(readBytes, sz - jpegoff);
                                    for (int i = 0; i < newread; i++)
                                    {
                                        jpeg[i + jpegoff] = respose[i + offset];
                                    }
                                    jpegoff += newread;
                                    if (newread + jpegoff == sz)
                                    {
                                        inminiheader = true;
                                       
                                            frame_new(jpeg);
                                       
                                    }
                                }
                            }
                        }
                        if (backgroundWorker1.CancellationPending)
                        {
                            Console.WriteLine("Stoping Stream");
                            break;
                        }
                    } while (true);
                }
            }
                
         
           catch (Exception ex)
            {

            }
        }*/
        public string[] parse(string url)
        {
            int loc=url.IndexOf(':');
            string[] ar = new string[6]; //http u p ip port path
            ar[0] = url.Remove(loc);
            string[] a = url.Substring(loc + 3).Split(new char[] { '/' }, 2);
            ar[5] = '/' + a[1];
            if (a[0].Contains('@'))
            {
                string[] splitOnAt = a[0].Split('@');
                if (splitOnAt[0].Contains(':'))
                {
                    string[] splitoncolonbeforeat = splitOnAt[0].Split(':');
                    ar[1] = splitoncolonbeforeat[0];
                    ar[2] = splitoncolonbeforeat[1];
                }
                else
                {
                    ar[1] = splitOnAt[0];
                }
                if (splitOnAt[1].Contains(':'))
                {
                    string[] splitoncolonafterat = splitOnAt[1].Split(':');
                    ar[3] = splitoncolonafterat[0];
                    ar[4] = splitoncolonafterat[1];
                }
                else
                {
                    ar[3] = splitOnAt[1];
                }
            }
            else
            {
                if (a[0].Contains(':'))
                {
                    string[] splitoncolonafterat = a[0].Split(':');
                    ar[3] = splitoncolonafterat[0];
                    ar[4] = splitoncolonafterat[1];
                }
                else
                {
                    ar[3] = a[0];
                }
            }
            
            return ar;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

      /*  private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    menuitem++;
                    if(menuitem >= nomenuitems)
                    {
                        menuitem = 0;
                    }
                    pictureBox1.Refresh();
                    //tabControl1.SelectedIndex = menuitem;
           
                    break;
                case Keys.Up:
                    menuitem--;
                    
                    if(menuitem < 0)
                    {
                        menuitem = nomenuitems - 1;
                    }
                    pictureBox1.Refresh();
                    //
                 
                    break;
            }

         
        }*/

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        int last = -2;
        int menuitem = -1;
        int nomenuitems = 5;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(pictureBox1.BackColor);
            if (menuitem == 0 || menuitem < 0)
            {

                g.DrawLine(new Pen(Color.Red, 4), new Point(2, 0), new Point(2, 64));
            }
            g.DrawLine(new Pen(Color.White, 4), new Point(12, 12), new Point(60, 12));
            g.DrawLine(new Pen(Color.White, 4), new Point(12, 26), new Point(60, 26));
            g.DrawLine(new Pen(Color.White, 4), new Point(12, 26+14), new Point(60, 26+14));
            g.DrawLine(new Pen(Color.White, 4), new Point(12, 26 + 28), new Point(60, 26 + 28));
            if (menuitem == 1 || menuitem < 0)
            {
                g.DrawLine(new Pen(Color.Violet, 4), new Point(2, 64), new Point(2, 64 + 64));
            }
            g.DrawLine(new Pen(Color.White, 4), new Point(12, 4+64), new Point(12, 60+64));
            g.DrawLine(new Pen(Color.White, 4), new Point(60, 64+36), new Point(60, 64+60));
            g.DrawLine(new Pen(Color.White, 4), new Point(12+24, 4+64), new Point(12, 4+64));
            g.DrawLine(new Pen(Color.White, 4), new Point(12, 64+60), new Point(60, 60 + 64));
            g.DrawLine(new Pen(Color.White, 4), new Point(12+24, 34 + 64), new Point(12+24, 4 + 64));
            g.DrawLine(new Pen(Color.White, 4), new Point(12 + 24, 34 + 64), new Point(60, 34 + 64));
            g.DrawLine(new Pen(Color.White, 4), new Point(12 + 24, 4 + 64), new Point(60, 34 + 64));
            if (menuitem == 2 || menuitem < 0)
            {
                g.DrawLine(new Pen(Color.Yellow, 4), new Point(2, 64 + 64), new Point(2, 64 + 64 + 64));
            }
            
            g.FillRectangle(new SolidBrush(Color.FromArgb(210,220,0)),new Rectangle(new Point(12,4+64+64),new Size(60-12,56)));
            g.DrawRectangle(Pens.White, new Rectangle(new Point(12, 4 + 64 + 64), new Size(60-12, 56)));
            g.DrawRectangle(Pens.White, new Rectangle(new Point(12+23, 4 + 64 + 64), new Size(24, 14)));
            g.FillRectangle(new SolidBrush(pictureBox1.BackColor), new Rectangle(new Point(13 + 23, 4 + 64 + 64), new Size(25, 14)));

            g.FillPolygon(Brushes.Lime, new Point[] { new Point(12, 4 + (64 * 3)), new Point(60, (64 * 3) + 30), new Point(12, 60 + (64 * 3)), new Point(12, 4 + (64 * 3)) });

            g.FillPolygon(Brushes.Blue, new Point[] { new Point(12, 4 + 64 + 64 + 64 + 64), new Point(12 + 46, 4 + 64 + 64 + 64 + 64), new Point(12+54, 15 + 64 + 64 + 64 + 64),new Point(12+54,64+64+64+64+60), new Point(12, 64 + 64 + 64 + 64 + 60), new Point(12, 4 + 64 + 64 + 64 + 64) });
            g.DrawPolygon(Pens.Black, new Point[] { new Point(12, 4 + 64 + 64 + 64 + 64), new Point(12 + 46, 4 + 64 + 64 + 64 + 64), new Point(12 + 54, 15 + 64 + 64 + 64 + 64), new Point(12 + 54, 64 + 64 + 64 + 64 + 60), new Point(12, 64 + 64 + 64 + 64 + 60), new Point(12, 4 + 64 + 64 + 64 + 64) });
            g.FillRectangle(Brushes.Black, new Rectangle(20, 4 + 64 + 64 + 64 + 64, 30, 20));
            g.FillRectangle(Brushes.White, new Rectangle(21, 5 + 64 + 64 + 64 + 64, 28, 18));
            g.FillEllipse(Brushes.Black, new Rectangle(40, 9 + 64 + 64 + 64 + 64, 4,9));
            g.FillRectangle(Brushes.Black, new Rectangle(20, 4+16+16 + 64 + 64 + 64 + 64, 34, 56-32));
            g.FillRectangle(Brushes.White, new Rectangle(21, 5+16+16 + 64 + 64 + 64 + 64, 32, 55-32));
           
            
            
            if (menuitem == 3 || menuitem < 0)
            {
                g.DrawLine(new Pen(Color.Lime, 4), new Point(2, 64 * 3), new Point(2, 64 * 4));
            }
            if (menuitem == 4 || menuitem < 0)
            {
                g.DrawLine(new Pen(Color.Blue, 4), new Point(2, 64 * 4), new Point(2, 64 *5));
            }
            
            g.Flush();
            if (last != menuitem)
            {
                SetPage(menuitem);
                last = menuitem;
            }
            

        }
        public static void SetPage(Control coll, Control c)
        {
            coll.Controls.Clear();
            coll.SuspendLayout();
            c.SuspendLayout();
            c.Dock = DockStyle.Fill;
            coll.Controls.Add(c);
            coll.ResumeLayout();
            c.ResumeLayout();
            
        }

        public  void SetNoProjectLoadedPage()
        {
            label1.Text = "No Project Loaded";
            panel1.Controls.Clear();
            panel1.SuspendLayout();
            panel1.Controls.AddRange(new Control[] {label1,linkLabel1,linkLabel2});
            panel1.ResumeLayout();
        }
        public bool ProjectLoaded { get; set; }
        public project_file Project { get; set; }
        private void SetPage(int ye)
        {
            switch (ye)
            {
                case 0:
                    if (ProjectLoaded)
                    {
                        SetPage(panel1, TimelapsePage);
                    }
                    else
                    {
                        last = -3;
                        menuitem = -3;
                        SetNoProjectLoadedPage();

                        pictureBox1.Invalidate();
                    }
                    break;
                case 1:
                    SetPage(panel1, NewProject);
                    NewProject.Hitme();
                    break;
                case 2:
                    SetPage(panel1,OpenProject);
                    OpenProject.Hitme();
                    break;
                case 3:
                    if (ProjectLoaded)
                    {
                        SetPage(panel1, Vidplayer);
                        Vidplayer.Hitme();
                    }
                    else
                    {
                        last = -3;
                        menuitem = -3;
                        SetNoProjectLoadedPage();
                        
                        pictureBox1.Invalidate();
                    }
                    break;
                case 4:
                    if (ProjectLoaded)
                    {
                        if (Export.Hitme())
                        {
                            SetPage(panel1, Export);    
                        }
                        else
                        {
                            menuitem = last;
                            pictureBox1.Invalidate();
                            last = menuitem;
                        }
                    }
                    else
                    {
                        last = -3;
                        menuitem = -3;

                        SetNoProjectLoadedPage();
                        pictureBox1.Invalidate();

                    }
                    break;
                case -3:
                    
                    break;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
           int y= e.Y / 64;
           if (y < nomenuitems)
           {
               menuitem = y;
               pictureBox1.Refresh();
           }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            menuitem = 1;
            pictureBox1.Refresh();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            menuitem = 2;
            pictureBox1.Refresh();
        }
        AForge.Video.MJPEGStream mjpeg;
       public  static Form1 Instance { get; private set; }
        internal void LoadProject(project_file pr)
        {
            if (mjpeg !=null && mjpeg.IsRunning)
            {
                
                TimelapsePage.timer1.Enabled = false;

                mjpeg.Stop();
                mjpeg = null;
            }
            Vidplayer.timer1.Enabled = false;
            ProjectLoaded = true;
            Project = pr;
            menuitem = 0;
            pictureBox1.Refresh();
            Uri uri = new Uri(pr.addr);
            string url = string.Format("{0}://{1}:{2}{3}", uri.Scheme, uri.Host, uri.Port, uri.PathAndQuery);

            string user = "";

            string pass = "";
            if (!string.IsNullOrWhiteSpace(uri.UserInfo))
            {
                if (uri.UserInfo.Contains(':'))
                {
                    string[] uandp = uri.UserInfo.Split(':');
                    user = uandp[0];
                    pass = uandp[1];
                }
                else
                {
                    user = uri.UserInfo;
                }
            }
            mjpeg = new AForge.Video.MJPEGStream(url);
            mjpeg.Login = user;
            mjpeg.Password = pass;
            mjpeg.NewFrame += new AForge.Video.NewFrameEventHandler(mjpeg_NewFrame);
            mjpeg.Start();
        }

        void mjpeg_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap b = (Bitmap)eventArgs.Frame.Clone();
            
            TimelapsePage.SetPicture(b);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mjpeg != null)
            {
                mjpeg.Stop();
            }
        }

       

        
    }
}
