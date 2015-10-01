using Bell_Smart_Launcher.Source.Frame;
using BellLib.Class;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Bell_Smart_Launcher.Source.Management
{
    public class Controller
    {
        private DispatcherTimer tmr_Update = new DispatcherTimer();

        /// <summary>
        /// 런처 컨트롤러를 생성합니다.
        /// </summary>
        public Controller()
        {
            tmr_Update.Interval = TimeSpan.FromSeconds(30);
            tmr_Update.Tick += new EventHandler(Update_Tick);
        }

        /// <summary>
        /// 런처 컨트롤러를 초기화합니다.
        /// </summary>
        public void Initialize()
        {
            tmr_Update.Start();
            Update_Tick(null, null);
        }

        private int errCount = 0;
        private void Update_Tick(object sender, EventArgs e)
        {
            if (User.BSP_AutoUpdate)
            {
                try
                {
                    if (Deploy.UpdateAvailable())
                    {
                        Updater Upd = new Updater();
                        Upd.Show();

                        tmr_Update.Stop(); // 업데이트 실행 후 타이머 중단
                    }
                }
                catch (Exception ex)
                {
                    if (errCount > 2)
                    {
                        WPFCom.Message("자동 업데이트 시스템 동작 중 문제가 발생하였습니다." + Environment.NewLine + "이 에러메시지가 자주 발생한다면 BSN 홈페이지에 피드백을 올려주시기 바랍니다." + Environment.NewLine + "errCount = " + errCount + Environment.NewLine + ex.Message + Environment.NewLine + "StackTrace : " + ex.StackTrace);
                        errCount = 0;
                    }
                    else
                    {
                        errCount += 1;
                    }

                    return;
                }
                errCount = 0;
            }
        }
    }
}
