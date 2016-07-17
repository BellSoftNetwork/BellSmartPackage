using Bell_Smart_Launcher.Class;
using Bell_Smart_Launcher.Source.Data;
using BellLib.Class;
using BellLib.Class.Protection;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// Profile.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProfileEditor : Window
    {
        // 필드
        Profile profile;


        /// <summary>
        /// 새 프로필을 생성합니다.
        /// </summary>
        public ProfileEditor()
        {
            InitializeComponent();

            if (profile == null)
                profile = new Profile(null);
        }

        /// <summary>
        /// 프로필 에디터를 선택한 프로필파일로 초기화합니다.
        /// </summary>
        /// <param name="ProfileName">프로필 이름</param>
        public ProfileEditor(string ProfileName) : this()
        {
            // 필드 검사
            if (ProfileName == "프로필 선택")
                return;

            // 초기화
            profile = new Profile(ProfileName);

            txtName.Text = profile.GetData(Profile.Data.Name);
            txtID.Text = profile.GetData(Profile.Data.ID);
            txtPW.Password = profile.GetData(Profile.Data.PW);
            if (txtPW.Password == string.Empty)
                cbSavePW.IsChecked = false;
        }

        /// <summary>
        /// 방금 저장한 프로필 이름을 반환합니다.
        /// </summary>
        /// <returns>프로필 이름</returns>
        public string GetSaveName()
        {
            return profile.GetData(Profile.Data.Name);
        }

        private void cb_SavePW_CheckedChanged(object sender, RoutedEventArgs e)
        {
            txtPW.IsEnabled = (bool)cbSavePW.IsChecked;
            txtPW.Password = string.Empty;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // 설정값 정리
            if (txtPW.Password == string.Empty)
                cbSavePW.IsChecked = false;

            // 필드 검사
            List<string> list = new List<string>();
            if (txtName.Text == string.Empty)
                list.Add("프로필 이름");
            if (txtID.Text == string.Empty)
                list.Add("계정 아이디");

            if (list.Count != 0) // 없는 값이 있을경우
            {
                string strTemp = null;
                foreach (string tmp in list)
                {
                    strTemp += tmp;
                    if (list[list.Count - 1] != tmp)
                        strTemp += ", ";
                }
                WPFCom.Message(strTemp + " 값을 입력해주세요.", Base.PROJECT.Bell_Smart_Launcher);
                return;
            }

            // 저장
            profile.RemoveData();
            profile.SetData(txtName.Text, txtID.Text, txtPW.Password);
            profile.Save();
            
            this.Close();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            profile.RemoveData();

            this.Close();
        }

        private void txtPW_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Game.DebugMode && DebugCategory.PWV)
                txtPW.ToolTip = txtPW.Password;
            else
                txtPW.ToolTip = null;
        }
    }
}
