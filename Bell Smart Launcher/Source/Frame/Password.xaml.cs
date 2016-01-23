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

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// Password.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
        }

        public string getPassword()
        {
            return txtPassword.Password;
        }

        private void btn_Apply_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == string.Empty)
                return;
            this.Close();
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btn_Apply_Click(sender, e);
        }
    }
}
