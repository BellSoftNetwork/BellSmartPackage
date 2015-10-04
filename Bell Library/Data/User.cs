using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BellLib.Class;

namespace BellLib.Data
{
    public class User
    {
        public static string BSN_Email { get; set; }
        public static string BSN_Password {get; set;}
        public static string BSN_member_srl {get; set;}
        public static string BSN_nick_name {get; set;}
        public static string BSN_is_admin { get; set; }
        public static string[] BSN_group { get; set; }
        public static string MC_NickName {get;set;}
        public static string MC_UUID {get;set;}
        public static string MC_AccessToken { get; set; }

        public static Version BST_Current_Verion { get { return Deploy.CurrentVersion; } }
        public static Version BST_Latest_Version { get { return Deploy.LatestVersion; } }
        /// <summary>
        /// BSN 로컬 경로를 가져옵니다.
        /// 기본값 : C:\BSN\
        /// </summary>
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
        //public static bool MC_Login = false;
        public static bool BSP_AutoUpdate = true;
        public static string BSN_Temp { get { return BSN_Path + "Temp\\"; } }
    }
}
