using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bell_Smart_Server.Source.Data
{
    public class Server
    {
        // 일반
        public static bool AutoRestart { get; set; }
        public static bool RemoveOldLog { get; set; }
        //public static bool LimitLogLine { get; set; }
        public static int LimitLogLine { get; set; }
        public static bool StartLogClear { get; set; }
    }
}
