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
    /// Loader.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Loader : Window
    {
        public Loader()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Main login = new Main(); // 추후 로그인창으로 수정
            login.Show();
            this.Close();
        }
    }
}
