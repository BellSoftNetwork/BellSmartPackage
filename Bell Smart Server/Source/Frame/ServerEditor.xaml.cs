using Bell_Smart_Server.Source.Class;
using BellLib.Class;
using System;
using System.Collections.Generic;
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

namespace Bell_Smart_Server.Source.Frame
{
    /// <summary>
    /// ServerEditor.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ServerEditor : Window
    {
        // 필드
        ServerProfile profile;

        /// <summary>
        /// 새 프로필을 생성합니다.
        /// </summary>
        public ServerEditor()
        {
            InitializeComponent();

            profile = new ServerProfile(null);

            if (txtJavaPath.Text == string.Empty)
                txtJavaPath.Text = "java";

            if (txtParameter.Text == string.Empty)
                txtParameter.Text = "-Xms1G -Xmx1G";
        }

        /// <summary>
        /// 프로필 에디터를 선택한 프로필파일로 초기화합니다.
        /// </summary>
        /// <param name="ProfileName">프로필 이름</param>
        public ServerEditor(string ProfileName) : this()
        {
            // 필드 검사
            if (ProfileName == "프로필 선택")
                return;

            // 초기화
            profile = new ServerProfile(ProfileName);

            txtName.Text = profile.GetData(ServerProfile.Data.Name);
            txtPath.Text = profile.GetData(ServerProfile.Data.ServerPath);
            txtFile.Text = profile.GetData(ServerProfile.Data.ServerFile);
            txtJavaPath.Text = profile.GetData(ServerProfile.Data.JavaPath);
            txtParameter.Text = profile.GetData(ServerProfile.Data.Parameter);
        }

        /// <summary>
        /// 방금 저장한 프로필 이름을 반환합니다.
        /// </summary>
        /// <returns>프로필 이름</returns>
        public string GetSaveName()
        {
            return profile.GetData(ServerProfile.Data.Name);
        }

        private void btnSetPath_Click(object sender, RoutedEventArgs e)
        {
            // Initialize an Open File Dialog
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Title = "마인크래프트 서버 검색";
            dialog.Filter = "마인크래프트 서버 JAR 파일|*.jar";
            if (dialog.ShowDialog() != true)
                return;
            
            txtPath.Text = System.IO.Path.GetDirectoryName(dialog.FileName);
            txtFile.Text = System.IO.Path.GetFileName(dialog.FileName);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // 필드 검사
            List<string> list = new List<string>();
            if (txtName.Text == string.Empty)
                list.Add("서버 이름");
            if (txtPath.Text == string.Empty)
                list.Add("서버 경로");
            if (txtFile.Text == string.Empty)
                list.Add("서버 파일");

            if (list.Count != 0) // 없는 값이 있을경우
            {
                string strTemp = null;
                foreach (string tmp in list)
                {
                    strTemp += tmp;
                    if (list[list.Count - 1] != tmp)
                        strTemp += ", ";
                }
                WPFCom.Message(strTemp + " 값을 입력해주세요.", BellLib.Data.Base.PROJECT.Bell_Smart_Server);
                return;
            }

            // 저장
            profile.RemoveData();
            profile.SetData(txtName.Text, txtPath.Text, txtFile.Text, txtJavaPath.Text, txtParameter.Text);
            profile.Save();

            this.Close();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            profile.RemoveData();

            this.Close();
        }
    }
}
