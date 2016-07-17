using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Deployment.Application;
using System.ComponentModel;

namespace BellLib.Class
{
    public class Deploy
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
            catch (InvalidDeploymentException dex)
            { // 배포 매니페스트가 손상된 경우, 이 문제를 해결하려면 응용 프로그램을 다시 배포해야 합니다.
                WPFCom.Message("배포 매니페스트가 손상되었습니다." + Environment.NewLine + "이 에러메시지가 지속적으로 발생한다면 프로그램을 재 설치하시기 바랍니다." + Environment.NewLine + dex.Message, Data.Base.PROJECT.Bell_Smart_Package);
                throw dex;
            }
            catch (InvalidOperationException oex)
            { // 업데이트가 이미 진행 중일 때 CheckForUpdate 메서드를 호출하면 ClickOnce에서 즉시 이 예외를 throw합니다.
                WPFCom.Message("업데이트가 이미 진행중 입니다." + Environment.NewLine + "이 에러가 업데이트도중 지속적으로 발생한다면 관리자에게 문의하시기 바랍니다.", Data.Base.PROJECT.Bell_Smart_Package);
                throw oex;
            }
            catch (DeploymentDownloadException ddex)
            { // 배포 매니페스트를 다운로드할 수 없는 경우.
                WPFCom.Message("배포 매니페스트를 다운로드할 수 없습니다." + Environment.NewLine + "배포서버 점검중이 아닐때 이 에러가 발생한다면 관리자에게 문의하시기 바랍니다.", Data.Base.PROJECT.Bell_Smart_Package);
                throw ddex;
            }
            catch (Exception ex)
            { // 위 예외를 제외한 다른 모든 예외
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
