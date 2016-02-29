using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace BellLib.Class
{
    public class SystemInfo
    {
        /// <summary>
        /// 메모리 정보
        /// </summary>
        public struct MemoryInfo
        {
            public int Total_Physical_KB, Free_Physical_KB, Used_Physical_KB;
            public int Total_Physical_MB, Free_Physical_MB, Used_Physical_MB;
            public int Total_Physical_GB, Free_Physical_GB, Used_Physical_GB;
        }

        /// <summary>
        /// 현재 시스템정보를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public static string GetSystemInfo()
        {
            ManagementClass cls = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection instances = cls.GetInstances();

            StringBuilder str = new StringBuilder();

            foreach (ManagementObject instance in instances)
            {
                foreach (PropertyData prop in instance.Properties)
                {
                    str.Append(string.Format("{0} : {1}", prop.Name, prop.Value));
                    str.Append(Environment.NewLine);
                }
            }

            return str.ToString();
        }

        /// <summary>
        /// 현재 시스템 메모리정보를 가져옵니다.
        /// </summary>
        /// <returns>시스템 메모리 정보</returns>
        public static MemoryInfo GetMemoryInfo()
        {
            ManagementClass cls = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection instances = cls.GetInstances();
            MemoryInfo mi = new MemoryInfo();

            foreach (ManagementObject instance in instances)
            {
                mi.Total_Physical_KB = int.Parse(instance["TotalVisibleMemorySize"].ToString());
                mi.Free_Physical_KB = int.Parse(instance["FreePhysicalMemory"].ToString());
            }
            mi.Used_Physical_KB = mi.Total_Physical_KB - mi.Free_Physical_KB; // 사용중인 메모리

            mi.Total_Physical_MB = mi.Total_Physical_KB / 1024; // 총 메모리 MB 단위 변경	
            mi.Free_Physical_MB = mi.Free_Physical_KB / 1024;   // 사용 가능 메모리 MB 단위 변경
            mi.Used_Physical_MB = mi.Total_Physical_MB - mi.Free_Physical_MB; // 사용중인 메모리

            mi.Total_Physical_GB = mi.Total_Physical_MB / 1024; // 총 메모리 GB 단위 변경	
            mi.Free_Physical_GB = mi.Free_Physical_MB / 1024;   // 사용 가능 메모리 GB 단위 변경
            mi.Used_Physical_GB = mi.Total_Physical_GB - mi.Free_Physical_GB; // 사용중인 메모리

            return mi;
        }
    }
}
