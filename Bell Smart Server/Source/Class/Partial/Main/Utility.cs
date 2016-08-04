using Bell_Smart_Server.Source.Class;
using BellLib.Class;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bell_Smart_Server.Source.Frame
{
    /// <summary>
    /// 서버 메인의 유틸리티 분할 소스파일
    /// </summary>
    public partial class Main
    {
        #region *** PROFILE ***

        /// <summary>
        /// 프로필 수정모드로 엽니다.
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (cbServer.SelectedIndex < 2)
                return;
            ServerEditor pro = new ServerEditor((string)cbServer.SelectedItem);
            pro.ShowDialog();
            ServerLoad(); // 값이 바뀌었을테니 프로필 다시 로드!
        }

        /// <summary>
        /// 프로필 리스트 변경을 검사 후 선택값을 저장합니다.
        /// </summary>
        private void cbServer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 유효성 검증
            if (cbServer.IsInitialized)
                if (cbServer.SelectedIndex > 1)
                    btnStart.IsEnabled = true;
                else
                    btnStart.IsEnabled = false;

            // 프로필 제어
            if (cbServer.SelectedIndex == 1)
            {
                cbServer.SelectedIndex = 0;
                ServerEditor Pro = new ServerEditor();
                Pro.ShowDialog();
                ServerLoad(); // 값이 바뀌었을테니 프로필 다시 로드!

                if (Pro.GetSaveName() != null) // 프로필 이름이 null이 아니라면, (프로필을 정상적으로 생성했다면,
                    cbServer.SelectedItem = Pro.GetSaveName(); // 방금 생성한 따끈따끈한 프로필파일을 선택
            }

            // 선택값 저장
            if (cbServer.IsInitialized && cbServer.SelectedIndex > -1)
                ServerProfile.SetLastProfile((string)cbServer.SelectedItem); // 선택 프로필이 바뀌었으므로 설정값 저장!
        }

        #endregion

        #region *** COMMAND ***

        /// <summary>
        /// 명령어를 서버에 전송합니다.
        /// </summary>
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            // 필드
            string Command = txtCommand.Text;

            // 유효성 검증
            if (Command == string.Empty)
                return;

            // 추가 옵션
            if (cbSay.IsChecked == true)
                Command = "say " + Command;

            // 명령어 전송
            SendCommand(Command);

            // 마무으리
            txtCommand.Clear();
            btnSend.IsEnabled = false;

            // 명령어 유지기능
            try
            {
                string[] temp = Command.Split(' ');

                // 귓속말 유지기능
                if (temp[0] == "tell")
                    txtCommand.Text = "tell " + temp[1] + " ";

                // 마지막 위치로 입력 설정
                txtCommand.CaretIndex = txtCommand.Text.Length;
            }
            catch { }
        }

        /// <summary>
        /// 명령어 입력시 키보드를 통한 간편명령기능을 지원합니다.
        /// </summary>
        private void txtCommand_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    btnSend_Click(sender, e);

                    break;

                case Key.Up:
                    // 이전에 입력한 명령어 로드

                    break;

                case Key.Down:
                    // 이후에 입력한 명령어 로드

                    break;
            }
        }

        /// <summary>
        /// 명령어 입력값을 확인합니다.
        /// </summary>
        private void txtCommand_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtCommand.Text == string.Empty)
                btnSend.IsEnabled = false;
            else
                btnSend.IsEnabled = true;
        }

        /// <summary>
        /// 접속자수를 새로고침합니다.
        /// </summary>
        private void btnPlayerRefresh_Click(object sender, RoutedEventArgs e)
        {
            SendCommand("list");
            SendCommand("tps");
            lstPlayers.Items.Refresh();
        }

        /// <summary>
        /// 서버 저장 명령어를 전송합니다.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SendCommand("save-all");
            SetState("서버 저장 요청 전송");
        }

        /// <summary>
        /// 선택한 플레이어를 추방합니다.
        /// </summary>
        private void btnKick_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (Player player in lstPlayers.Items)
                if (player.select)
                {
                    list.Add(player.nickname);
                    sb.Append(player.nickname + ", ");
                }
            try
            {
                sb.Remove(sb.Length - 2, 2);
            }
            catch { }

            if (list.Count > 0)
            {
                if (WPFCom.Message(sb.ToString() + " 플레이어를 추방하시겠습니까?", Base.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                    return;

                foreach (string nickname in list)
                    SendCommand("kick " + nickname + " " + txtMessage.Text);
                txtMessage.Clear();
            }
            else
                WPFCom.Message("선택된 플레이어가 없습니다.", Base.PROJECT.Bell_Smart_Server);
        }

        /// <summary>
        /// 선택한 플레이어를 영구정지합니다.
        /// </summary>
        private void btnBan_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (Player player in lstPlayers.Items)
                if (player.select)
                {
                    list.Add(player.nickname);
                    sb.Append(player.nickname + ", ");
                }
            try
            {
                sb.Remove(sb.Length - 2, 2);
            }
            catch { }

            if (list.Count > 0)
            {
                if (WPFCom.Message(sb.ToString() + " 플레이어를 영구정지 하시겠습니까?", Base.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                    return;

                foreach (string nickname in list)
                    SendCommand("ban " + nickname + " " + txtMessage.Text);
                txtMessage.Clear();
            }
            else
                WPFCom.Message("선택된 플레이어가 없습니다.", Base.PROJECT.Bell_Smart_Server);
        }

        /// <summary>
        /// 선택한 플레이어에게 귓속말합니다.
        /// </summary>
        private void btnWhispers_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();

            foreach (Player player in lstPlayers.Items)
                if (player.select)
                {
                    list.Add(player.nickname);
                }

            if (list.Count != 1)
            {
                WPFCom.Message("귓속말은 한명에게만 할 수 있습니다.", Base.PROJECT.Bell_Smart_Server);
                return;
            }

            cbSay.IsChecked = false;
            txtCommand.Text = "tell " + list[0] + " ";
        }

        /// <summary>
        /// 선택한 플레이어에게 경고를 부여합니다.
        /// </summary>
        private void btnWarn_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (Player player in lstPlayers.Items)
                if (player.select)
                {
                    list.Add(player.nickname);
                    sb.Append(player.nickname + ", ");
                }
            try
            {
                sb.Remove(sb.Length - 2, 2);
            }
            catch { }

            if (list.Count > 0)
            {
                if (WPFCom.Message(sb.ToString() + " 플레이어에게 경고하시겠습니까?", Base.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                    return;
                ItemCollection playerList = lstPlayers.Items;
                foreach (string nickname in list)
                    try
                    {
                        foreach (Player player in playerList)
                        {
                            if (player.nickname == nickname)
                            {
                                int index = lstPlayers.Items.IndexOf(player);
                                lstPlayers.Items.Remove(player);
                                player.suspects = (Convert.ToInt32(player.suspects) + 1).ToString();
                                lstPlayers.Items.Insert(index, player);
                                SendCommand("say " + nickname + "님이 '" + txtMessage.Text + "' 사유로 경고가 누적되었습니다.");
                                txtMessage.Clear();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        AddLog("경고 누적중 에러 발생" + Environment.NewLine + ex.Message, LOG.ERROR);
                    }
            }
            else
                WPFCom.Message("선택된 플레이어가 없습니다.", Base.PROJECT.Bell_Smart_Server);
        }

        /// <summary>
        /// 선택한 플레이어에게 아이템을 증정합니다.
        /// </summary>
        private void btnGive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Convert.ToInt32(txtItemAmount.Text);
            }
            catch
            {
                WPFCom.Message("아이템 수량은 숫자만 입력할 수 있습니다.", Base.PROJECT.Bell_Smart_Server);
                return;
            }

            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (Player player in lstPlayers.Items)
                if (player.select)
                {
                    list.Add(player.nickname);
                    sb.Append(player.nickname + ", ");
                }
            try
            {
                sb.Remove(sb.Length - 2, 2);
            }
            catch { }

            if (list.Count > 0)
            {
                if (WPFCom.Message(sb.ToString() + " 플레이어에게 " + txtItemID.Text + " 아이템을 " + txtItemAmount.Text + "개 지급하시겠습니까?", Base.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                    return;

                foreach (string nickname in list)
                    SendCommand("give " + nickname + " " + txtItemID.Text + " " + txtItemAmount.Text);
                txtItemID.Clear();
                txtItemAmount.Text = "1";
            }
            else
                WPFCom.Message("선택된 플레이어가 없습니다.", Base.PROJECT.Bell_Smart_Server);
        }

        #endregion

        #region *** ADDITIONAL ***

        /// <summary>
        /// 서버 로그를 지웁니다.
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(GetLogBox(GetCurrentLogType()).Text))
                if (WPFCom.Message("정말로 모든탭의 로그를 초기화하시겠습니까?", Base.PROJECT.Bell_Smart_Server, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    foreach (LOG log in LogList())
                        GetLogBox(log).Clear();

                    WPFCom.Message("모든 로그 초기화에 성공했습니다.", Base.PROJECT.Bell_Smart_Server);
                }

            try
            {
                GetLogBox(GetCurrentLogType()).Clear();
            }
            catch (Exception ex)
            {
                AddLog("로그 초기화중 오류가 발생하였습니다." + Environment.NewLine + "에러내용 : " + ex.Message, LOG.ERROR);
            }
        }

        /// <summary>
        /// Log Limit에 따라 오래된 로그를 삭제합니다.
        /// </summary>
        private void btnOldLogRemove_Click(object sender, RoutedEventArgs e)
        {
            btnOldLogRemove.IsEnabled = false; // 중복클릭 방지

            int OldCriteria = 1000;

            foreach (LOG log in LogList())
                RemoveOldLog(log, OldCriteria);

            btnOldLogRemove.IsEnabled = true;
            WPFCom.Message("모든탭의 오래된로그를 전부 삭제했습니다!", Base.PROJECT.Bell_Smart_Server);
        }

        /// <summary>
        /// 서버 설정창을 실행합니다.
        /// </summary>
        private void btnServerSetting_Click(object sender, RoutedEventArgs e)
        {
            if (cbServer.SelectedIndex < 2)
                return;

            ServerSetting ss = new ServerSetting((string)cbServer.SelectedItem);
            ss.ShowDialog();
        }

        /// <summary>
        /// 리스트에서 모든 플레이어를 선택합니다.
        /// </summary>
        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (Player player in lstPlayers.Items)
                player.select = true;
            lstPlayers.Items.Refresh();
        }

        /// <summary>
        /// 리스트에서 모든 플레이어를 선택해제합니다.
        /// </summary>
        private void btnSelectCancelAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (Player player in lstPlayers.Items)
                player.select = false;
            lstPlayers.Items.Refresh();
        }

        #endregion
    }
}
