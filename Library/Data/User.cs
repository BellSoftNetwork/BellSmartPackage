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
        public static string MC_NickName, MC_UUID;
        public static string MC_ID
        {
            get
            {
                RegistryReader RR = new RegistryReader();
                RR.Key = "MC_ID";

                return (string)RR.GetValue();
            }

            set
            {
                RegistryManager RM = new RegistryManager("MC_ID", value);
                RM.SetValue();
            }
        }
        public static string MC_PW
        {
            get
            {
                RegistryReader RR = new RegistryReader();
                RR.Key = "MC_PW";
                                
                return (string)RR.GetValue();
            }

            set
            {
                RegistryManager RM = new RegistryManager("MC_PW", value);
                RM.SetValue();
            }
        }
        public static bool MC_Login = false;
        public static bool BSP_AutoUpdate = true;
    }
}
