using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BellLib.Class;

namespace BellLib.Data
{
    public class User
    {
        public static Version BST_Current_Verion { get { return Deployment.CurrentVersion; } }
        public static Version BST_Latest_Version { get { return Deployment.LatestVersion; } }
        public static string BSN_Path = Environment.GetEnvironmentVariable("SYSTEMDRIVE") + "\\BSN\\";
        public static string BSN_Email, BSN_Password;
        public static string MC_NickName, MC_UUID, MC_ID, MC_PW = null;
        public static bool MC_Login = false;
        public static bool BSP_AutoUpdate = true;
    }
}
