using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data
{
    public class User
    {
        public static Version BST_Current_Verion = new Version(4, 0, 0, 0);
        public static Version BST_Latest_Version = new Version(4, 0, 0, 0);
        public static string BSN_Path = Environment.SystemDirectory + "\\BSN\\";
        public static string BSN_Email, BSN_Password;
        public static string MC_NickName, MC_UUID, MC_ID, MC_PW = null;
        public static bool MC_Login;
    }
}
