using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bell_Smart_Tools.Data
{
    class User
    {
        public static Version BST_Current_Verion = new Version(4, 0, 0, 0);
        public static Version BST_Latest_Version = new Version(4, 0, 0, 0);
        public static string BSN_Path = Environment.SystemDirectory + "\\BSN\\";
        public static string MC_NickName, MC_UUID, MC_Login, MC_ID, MC_PW;
    }
}
