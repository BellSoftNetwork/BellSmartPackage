using Bell_Smart_Server.Source.Data;
using BellLib.Class;
using BellLib.Class.Control;
using BellLib.Class.Protection;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Bell_Smart_Server.Source.Frame
{
    /// <summary>
    /// 서버 메인의 세팅 분할 소스파일
    /// </summary>
    public partial class Main
    {
        #region *** INITIALIZE ***

        /// <summary>
        /// 세팅탭을 초기화합니다.
        /// </summary>
        private void InitSetting(int flag = 0x03)
        {
            // 정보 (0x01)
            if ((flag & 0x01) != 0)
            {
                set_lbCurrentVersion.Content = "현재버전 : " + Deploy.CurrentVersion;
                set_lbLatestVersion.Content = "최신버전 : " + Deploy.LatestVersion;

                tmr_Sync.Start();
                Sync_Tick(null, null);
            }

            // 일반 (0x02)
            if ((flag & 0x02) != 0)
            {
                try
                {
                    Server.LimitLogLine = Convert.ToInt32(DataProtect.DataLoad(DataPath.BSS.General, "LimitLogLine"));
                    if (Server.LimitLogLine != -1 && Server.LimitLogLine < 100) // 100라인 미만은 금지
                    {
                        Server.LimitLogLine = 1000; // 기본값
                        DataProtect.DataSave(DataPath.BSS.General, "LimitLogLine", Server.LimitLogLine.ToString());
                    }

                    txtLimitLogLine.Text = Server.LimitLogLine.ToString();
                }
                catch { }
                finally
                {
                    if (string.IsNullOrWhiteSpace(txtLimitLogLine.Text))
                        Server.LimitLogLine = 1000;
                }

                if (DataProtect.DataLoad(DataPath.BSS.General, "StartLogClear") == "True")
                    Server.StartLogClear = true;
                else
                    Server.StartLogClear = false;
                cbStartLogClear.IsChecked = Server.StartLogClear;
            }
        }

        #endregion

        #region *** TIMER ***

        /// <summary>
        /// 온라인 데이터를 로드하여 서버와 싱크를 맞춥니다.
        /// </summary>
        private void Sync_Tick(object sender, EventArgs e)
        {
            try
            {
                set_lbLatestVersion.Content = "최신버전 : " + Deploy.LatestVersion;
                set_lbUpdateLock.Content = "업데이트 잠금 : " + UpdateControl.GetLockFlag()[0];

                if (Deploy.UpdateAvailable())
                {
                    set_lbUpdateLock.ToolTip = "최신버전이 발견되었습니다. 업데이트 잠금이 해제되면 업데이트 할 수 있습니다.";
                    btnUpdate.IsEnabled = false;
                }
            }
            catch
            {
                set_lbUpdateLock.Content = "업데이트 잠금 : 잠금해제";

                if (Deploy.UpdateAvailable())
                    btnUpdate.IsEnabled = true;
            }
        }

        #endregion

        #region *** CONTROL ***

        /// <summary>
        /// 프로그램 업데이트를 진행합니다.
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UpdateControl.UpdateCheck(false))
                {
                    btnUpdate.IsEnabled = false;

                    Updater updater = new Updater();
                    updater.Show();
                }
            }
            catch (Exception ex)
            {
                WPFCom.Message("업데이트 시도 중 문제가 발생하였습니다." + Environment.NewLine + "이 에러메시지가 자주 발생한다면 BSN 홈페이지 이슈트래커 게시판에 이슈를 등록 해 주시기 바랍니다." + Environment.NewLine + ex.Message + Environment.NewLine + "StackTrace : " + ex.StackTrace, Base.PROJECT.Bell_Smart_Server);
            }
        }

        #endregion

        #region *** DATA ***

        /// <summary>
        /// 설정값을 저장합니다.
        /// </summary>
        private void set_btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int line = Convert.ToInt32(txtLimitLogLine.Text);
                // 유효성 검사
                if (line != -1 && line < 100)
                {
                    WPFCom.Message("로그기록 100 라인 미만은 설정하실 수 없습니다.", Base.PROJECT.Bell_Smart_Server);

                    return;
                }

                Server.LimitLogLine = line;
                DataProtect.DataSave(DataPath.BSS.General, "LimitLogLine", txtLimitLogLine.Text);
            }
            catch
            {
                WPFCom.Message("로그 제한 줄 수는 정수만 입력할 수 있습니다.", Base.PROJECT.Bell_Smart_Server);
                return;
            }

            Server.StartLogClear = (bool)cbStartLogClear.IsChecked;
            DataProtect.DataSave(DataPath.BSS.General, "StartLogClear", Server.StartLogClear.ToString());

            WPFCom.Message("저장이 완료되었습니다.", Base.PROJECT.Bell_Smart_Server);
        }

        /// <summary>
        /// 설정값 변경을 취소합니다.
        /// </summary>
        private void set_btnCancel_Click(object sender, RoutedEventArgs e)
        {
            InitSetting(0x02);

            WPFCom.Message("취소되었습니다.", Base.PROJECT.Bell_Smart_Server);
        }

        #endregion
    }
}
