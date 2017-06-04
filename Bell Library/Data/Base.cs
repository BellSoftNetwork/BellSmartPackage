using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BellLib.Data
{
    /// <summary>
    /// 기본 정보
    /// </summary>
    public class Base
    {
        /// <summary>
        /// Bell Library 버전
        /// </summary>
        public const string VERSION = "1.0.0";

        /// <summary>
        /// 호환되는 프로프램 열거형
        /// </summary>
        public enum PROJECT
        {
            Bell_Smart_Package,
            Bell_Smart_Launcher,
            Bell_Smart_Manager,
            Bell_Smart_Server,
            Bell_Smart_Tools
        }
    }
}
