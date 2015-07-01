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

            public const string FTP_PATH_BSL = "HDD1/Info/BSL/";
            public const string FTP_Port = "21";
            public const string FTP_Info_ID = "BSNInfo";
            public const string FTP_Info_PW = "bellsoftnetwork";
        }

        /// <summary>
        /// SangDolE 서버정보
        /// </summary>
        public struct SangDolE
        {
            public const string SERVER_IP = "sangdole.ipdisk.co.kr";

            public const string FTP_PATH_BSL = "HDD3/BSN/Cloud/BSL/";
            public const string FTP_Port = "21";
            public const string FTP_Data_ID = "BSL"; //"BSL_Data";
            public const string FTP_Data_PW = "bellsoftnetwork";
        }
    }
}