using BellLib.Class;
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
    public partial class Profile : Window
    {
        private string ProfileName;

        /// <summary>
        /// 새 프로필을 생성합니다.
        /// </summary>
        public Profile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 프로필 에디터를 선택한 프로필파일로 초기화합니다.
        /// </summary>
        /// <param name="ProfileName">프로필 이름</param>
        public Profile(string ProfileName)
        {
            InitializeComponent();
            if (ProfileName == "프로필 선택")
                return;
            this.ProfileName = ProfileName;
            string[] Data = Protection.ReadBDXFile(User.BSL_Root + "Data\\BSL\\Profile\\" + ProfileName + ".bdx");

            txtName.Text = ProfileName;
            foreach (string Value in Data)
            {
                string[] tmp = Value.Split('|');
                switch (tmp[0])
                {
                    case "ID":
                        txtID.Text = tmp[1];
                        break;

                    case "PW":
                        txtPW.Password = tmp[1];
                        cbSavePW.IsChecked = true;
                        if (txtPW.Password == string.Empty)
                            cbSavePW.IsChecked = false;
                        break;
                }
            }
        }

        /// <summary>
        /// 프로필 데이터 종류 리스트
        /// </summary>
        public enum Data
        {
            Name,
            ID,
            PW
        }

        /// <summary>
        /// 프로필 데이터를 가져옵니다.
        /// </summary>
        /// <param name="Value">반환할 프로필 데이터 값</param>
        /// <returns>프로필 데이터</returns>
        public string getData(Data Value)
        {
            string strTemp = string.Empty;

            switch (Value)
            {
                case Data.Name:
                    strTemp = ProfileName;
                    break;

                case Data.ID:
                    strTemp = txtID.Text;
                    break;

                case Data.PW:
                    strTemp = txtPW.Password;
                    break;
            }

            return strTemp;
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
                WinCom.Message(strTemp + " 값을 입력해주세요.");
                return;
            }

            // 저장
            list.Clear(); // 위에서 한번 썼으니 초기화!

            list.Add("ID|" + txtID.Text);
            list.Add("PW|" + txtPW.Password);

            if (ProfileName != string.Empty)
                File.Delete(User.BSL_Root + "Data\\BSL\\Profile\\" + ProfileName + ".bdx"); // 열렸던 파일 삭제
            Protection.WriteBDXFile(User.BSL_Root + "Data\\BSL\\Profile\\" + txtName.Text + ".bdx", list.ToArray()); // 프로필 파일 저장
            ProfileName = txtName.Text;

            this.Close();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileName == string.Empty)
                return;
            File.Delete(User.BSL_Root + "Data\\BSL\\Profile\\" + ProfileName + ".bdx");
            this.Close();
        }
    }
}
