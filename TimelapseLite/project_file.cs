using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mjpeg_handler
{
    public class project_file
    {
        public static Dictionary<string,string> Read(string path)
        {
            Dictionary<string, string> dict=new Dictionary<string,string>();
           string[] data= System.IO.File.ReadAllLines(path);

           foreach (var item in data)
           {
               string[] d = item.Split(new char[] { ':' }, 2);
               dict.Add(d[0], d[1].Remove(0, 1));
           }

           return dict;
        }
        public static void Write(string path,Dictionary<string, string> dict)
        {
            List<string> vals = new List<string>();
            foreach (var item in dict)
            {
                vals.Add(string.Format("{0}: {1}", item.Key, item.Value));
            }
            System.IO.File.WriteAllLines(path, vals);
        }
        public static project_file Open(string path)
        {
            //estprojlen estvidlen addr fps is_est
            project_file pf = new project_file();
           var p= Read(path);
            if(p.ContainsKey("estprojlen")){
               pf.estprojlen=TimeSpan.FromSeconds( double.Parse(p["estprojlen"]));
            }
            if (p.ContainsKey("estvidlen"))
            {
                pf.estvidlen = TimeSpan.FromSeconds(double.Parse(p["estvidlen"]));
            }
            if (p.ContainsKey("addr"))
            {
                pf.addr=p["addr"];
            }
            if (p.ContainsKey("interval"))
            {
                pf.interval = int.Parse(p["interval"]);
            }
            if (p.ContainsKey("is_est"))
            {
                pf.is_est = bool.Parse(p["is_est"]);
            }
            pf.path = path;
            return pf;
        }
        public string path {
            get { return _intpath; }

            set { _intpath = value; dirpath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(_intpath), System.IO.Path.GetFileNameWithoutExtension(_intpath)); }
        }
        private string _intpath;
        public string dirpath;
        public TimeSpan estprojlen;
        public TimeSpan estvidlen;
        public string addr;
        public int interval;
        public bool is_est;
        public string _stordev;
       
        public string get_file()
        {
            
            System.IO.Directory.CreateDirectory(dirpath);
          return System.IO.Path.Combine(dirpath,  System.IO.Directory.GetFiles(dirpath,"*.jpg").Length.ToString() + ".jpg");

        }
        public override string ToString()
        {
            return System.IO.Path.GetFileNameWithoutExtension(path) + " [" + _stordev + "]";
        }
        public void Save()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("estprojlen", estprojlen.TotalSeconds.ToString());
            dict.Add("estvidlen", estvidlen.TotalSeconds.ToString());
            dict.Add("addr", addr);
            dict.Add("interval", interval.ToString());
            dict.Add("is_est", is_est.ToString());
            Write(path, dict);
        }

    }

}
