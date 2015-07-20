using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Deployment.Application;
using System.ComponentModel;

namespace BellLib.Class
{
    public class Deployment
    {
        /// <summary>
        /// 업데이트 이용 가능 여부를 반환합니다.
        /// </summary>
        /// <returns>업데이트 이용 가능 여부</returns>
        public static bool UpdateAvailable()
        {
            try
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    return ApplicationDeployment.CurrentDeployment.CheckForUpdate();
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Version CurrentVersion
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    return ApplicationDeployment.CurrentDeployment.CurrentVersion;
                }
                else
                {
                    return new Version(0, 0, 0, 0);
                }
            }
        }

        public static Version LatestVersion
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    try
                    {
                        return ApplicationDeployment.CurrentDeployment.CheckForDetailedUpdate().AvailableVersion;
                    }
                    catch // 최신버전이 가져와지지 않는다면 현재버전이 최신버전.
                    {
                        return CurrentVersion;
                    }
                }
                else
                {
                    return new Version(0, 0, 0, 0);
                }
            }
        }
    }
}
