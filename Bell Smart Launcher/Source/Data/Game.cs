using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bell_Smart_Launcher.Source.Data
{
    public class Game
    {
        /// <summary>
        /// 모드팩 설치 경로를 변경할 수 있도록 만든 경로.
        /// 기본값 : BSN_Path
        /// </summary>
        public static string BSL_Root { get; set; }

        public static double Memory_Allocate { get; set; }
        public static string JAVA_Path { get; set; }
        public static string JAVA_Parameter { get; set; }
        public static string Language { get; set; }
        public static bool ConsoleRun { get; set; }
        public static bool KeepOpen { get; set; }
    }
}
