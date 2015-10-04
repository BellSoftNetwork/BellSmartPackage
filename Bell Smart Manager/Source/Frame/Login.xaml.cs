using BellLib.Class;
using BellLib.Class.BSN;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Bell_Smart_Manager.Source.Frame
{
    /// <summary>
    /// Login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            Initialize();
        }

        private void lbBSNLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Servers.Bell_Soft_Network.WEB_BSN_ROOT);
        }

        private void Initialize() // 폼 초기화
        {
            string Email;
            string PW;

            RegistryReader rReader = new RegistryReader();

            rReader.Key = "BSN_Email";
            Email = (string)rReader.GetValue();
            rReader.Key = "BSN_Password";
            PW = (string)rReader.GetValue();

            if (Email != null)
            { // 레지스트리에 Email 값이 존재한다면,
                txtEmail.Text = Email; // 이메일 텍스트 박스에 값 대입
                cbEmail.IsChecked = true; // 이메일 저장 체크박스 활성화
            }
            if (PW != null) // 레지스트리에 PW 값이 존재한다면, (자동로그인 활성화)
            {
                txtPass.Password = PW; // PW 텍스트 박스에 값 대입
                cbAuto.IsChecked = true;
                FormEnable(false);
            }
        }

        private void FormEnable(bool value)
        {
            txtEmail.IsEnabled = value;
            txtPass.IsEnabled = value;
            btnLogin.IsEnabled = value;
            cbEmail.IsEnabled = value;
            cbAuto.IsEnabled = value;
            lbBSNLogin.IsEnabled = value;
        }

        private void loginBSN()
        {
            FormEnable(false);

            BSN.Login(txtEmail.Text, txtPass.Password); // 작업 성공 여부를 반환하는게 목표지 로그인된걸 반환하는게 아냐!

            if (BSN.LoginStatus) // BSN 회원 인증 성공시
            {
                BSN.SaveUserdata((bool)cbEmail.IsChecked, (bool)cbAuto.IsChecked);
                Main main = new Main();
                main.Show();
                this.Hide(); // BST_Login 숨김
            }
            else
            {
                WinCom.Message("인증 실패");
            }
            FormEnable(true);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            loginBSN();
        }

        private void txtAuth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnLogin_Click(sender, e);
        }

        private void cbAuto_Checked(object sender, RoutedEventArgs e)
        {
            cbEmail.IsChecked = true;
        }

        private void cbEmail_Unchecked(object sender, RoutedEventArgs e)
        {
            cbAuto.IsChecked = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (cbAuto.IsChecked == true)
                loginBSN();
        }
    }
}
