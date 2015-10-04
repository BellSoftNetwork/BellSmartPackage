using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BellLib.Data
{
    public class Servers
    {
        /// <summary>
        /// BSN 서버정보
        /// </summary>
        public struct Bell_Soft_Network
        {
            public const string SERVER_IP = "softbell.net";

            public const string FTP_PATH_CLOUD_ROOT = "HDD1/Cloud/";
            public const string FTP_PATH_INFO_ROOT = "HDD1/Info/";
            public const string FTP_PATH_INFO_BSL = "HDD1/Info/BSP/BSL/";
            public const string FTP_PORT = "21";
            public const string FTP_INFO_ID = "BSNInfo";
            public const string FTP_INFO_PW = "bellsoftnetwork";
            public const string FTP_CLOUD_ID = "BSNCloud";
            public const string FTP_CLOUD_PW = "bellsoftnetwork";

            public const string WEB_BLOG_URL = "http://blog." + SERVER_IP + "/";
            public const string WEB_INFO_BSL = "http://info." + SERVER_IP + "/BSP/BSL/";
            public const string WEB_CLOUD_ROOT = "http://cloud." + SERVER_IP + "/";
            public const string WEB_BSN_ROOT = "http://www." + SERVER_IP + "/";
            public const string WEB_BC_ROOT = "http://bc." + SERVER_IP + "/";
            public const string WEB_INFO_ROOT = "http://info." + SERVER_IP + "/";

            public const int BC_SERVER_PORT = 25555;
        }

        /// <summary>
        /// SangDolE 서버정보
        /// </summary>
        public struct SangDolE
        {
            public const string SERVER_IP = "sangdole.softbell.net"; //"sangdole.ipdisk.co.kr";

            public const string FTP_PATH_ROOT = "HDD3/BSN/Cloud/";
            public const string FTP_PATH_BSL = "HDD3/BSN/Cloud/BSL/";
            public const string FTP_PORT = "21";
            public const string FTP_DATA_ID = "BSL";
            public const string FTP_DATA_PW = "bellsoftnetwork";

            public const string WEB_CLOUD_BSL = "http://sangdole.softbell.net/Cloud/HDD3/BSN/Cloud/BSL/";
        }
    }
}