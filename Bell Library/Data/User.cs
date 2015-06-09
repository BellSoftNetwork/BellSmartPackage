using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BellLib.Class;

namespace BellLib.Data
{
    public class User
    {
        public static string BSN_Email, BSN_Password;
        public static string MC_NickName, MC_UUID;

        public static Version BST_Current_Verion { get { return Deployment.CurrentVersion; } }
        public static Version BST_Latest_Version { get { return Deployment.LatestVersion; } }
        public static string BSN_Path
        {
            set
            {
                RegistryManager RM = new RegistryManager("BSN_Path", value);
                RM.SetValue();
                RM.Dispose();
            }

            get
            {
                RegistryReader RR = new RegistryReader("BSN_Path");
                string Temp = (string)RR.GetValue();

                if (Temp != null)
                {
                    return Temp;
                }
                else
                {
                    return Environment.GetEnvironmentVariable("SYSTEMDRIVE") + "\\BSN\\";
                }
            }
        }

        /// <summary>
        /// 모드팩 설치 경로를 변경할 수 있도록 만든 경로.
        /// 기본값 : BSN_Path
        /// </summary>
        public static string BSL_Root
        {
            set
            {
                RegistryManager RM = new RegistryManager("BSL_Root", value);
                RM.SetValue();
                RM.Dispose();
            }

            get
            {
                RegistryReader RR = new RegistryReader("BSL_Root");
                string Temp = (string)RR.GetValue();

                if (Temp != null)
                {
                    return Temp;
                }
                else
                {
                    return BSN_Path;
                }
            }
        }
        
        public static string MC_ID
        {
            get
            {
                RegistryReader RR = new RegistryReader("MC_ID");
                return (string)RR.GetValue();
            }

            set
            {
                RegistryManager RM = new RegistryManager("MC_ID", value);
                RM.SetValue();
                RM.Dispose();
            }
        }
        public static string MC_PW
        {
            get
            {
                RegistryReader rReader = new RegistryReader("MC_PW");
                return (string)rReader.GetValue();
            }

            set
            {
                RegistryManager RM = new RegistryManager("MC_PW", value);
                RM.SetValue();
                RM.Dispose();
            }
        }
        public static bool MC_Login = false;
        public static bool BSP_AutoUpdate = true;
    }
}
