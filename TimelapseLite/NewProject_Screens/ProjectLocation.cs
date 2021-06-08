using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace mjpeg_handler.NewProject_Screens
{
    public class ProjectLocation
    {
        public static string InternalStore = getInternal();
        private static string getInternal()
        {
            string path;
            if (Environment.OSVersion.Platform == PlatformID.Win32Windows || Environment.OSVersion.Platform == PlatformID.Win32NT || Environment.OSVersion.Platform == PlatformID.Win32S || Environment.OSVersion.Platform == PlatformID.WinCE)
            {
                path= System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TimelapseLite");
            }

            else
            {
                path= System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "TimelapseLite");
            }
            System.IO.Directory.CreateDirectory(path);
            return path;
        }
        public override string ToString()
        {
            //what should be in combobox
            return fmt;
        }
        public void ShowLocation()
        {
            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32Windows || Environment.OSVersion.Platform == PlatformID.Win32NT || Environment.OSVersion.Platform == PlatformID.Win32S || Environment.OSVersion.Platform == PlatformID.WinCE)
                {
                    Process.Start(GetLocation());
                }
                else
                {
                    try
                    {
                        Process.Start("xdg-open", "\"" + GetLocation() + "\"");
                    }
                    catch (System.IO.FileLoadException ex)
                    {
                           
                        System.Windows.Forms.MessageBox.Show("TimelapseLite", "xdg-open not found, perhaps you are using mac");
                  
                    }
                    catch (Exception ex)
                    {

                     }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public string GetLocation()
        {
            System.IO.Directory.CreateDirectory(Location);
            return Location;
        }
        string fmt = "";
        string Location;
        private string conv(System.IO.DriveType dt,long sz)
        {
            switch(dt)
            {
                case System.IO.DriveType.Removable:
                    if(sz >= 2000000){
                         return "USB Drive";
                    }else{
                        return "Floppy Drive";
                    }
                
                case System.IO.DriveType.Fixed:
                    return "Local Disk";
                 
                case System.IO.DriveType.Network:
                    return "Network Drive";
                  
                case System.IO.DriveType.Ram:
                    return "Ram Disk";
                    
                
            }
            return "Disk";
        }
        public ProjectLocation(System.IO.DriveInfo i)
        {
            if(i.IsReady){
            if (Environment.OSVersion.Platform == PlatformID.Win32Windows || Environment.OSVersion.Platform == PlatformID.Win32NT || Environment.OSVersion.Platform == PlatformID.Win32S || Environment.OSVersion.Platform == PlatformID.WinCE)
            {
                string myUser =System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

                if(System.IO.Path.GetPathRoot(myUser) == i.ToString())
                
                {
                    Location = InternalStore;
                     fmt="Internal Storage";
                }
                else{
                    
                    string localDiskString = (!string.IsNullOrWhiteSpace(i.VolumeLabel) ? i.VolumeLabel : conv(i.DriveType,i.TotalSize));
                    fmt= localDiskString + " ("+ i.ToString().TrimEnd('\\') + ")";
                    Location =System.IO.Path.Combine(i.ToString(),"TimelapseLite");
                }
                
            }
            else
            {
                //is root
                if (i.ToString() == "/"){
                    fmt = "Internal Storage";
                    Location = InternalStore;
                }
                else{
                     Location =System.IO.Path.Combine(i.ToString(),"TimelapseLite");
                }
            }
            }
        }

        internal bool CanWork()
        {
            try
            {
                GetLocation();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
