using BellLib.Data;
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

namespace Bell_Smart_Manager.Source.Frame
{
    /// <summary>
    /// DebugHelper.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DebugHelper : Window
    {
        public DebugHelper()
        {
            InitializeComponent();
            btnReload_Click(null, null);
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            txtBSN_Email.Text = User.BSN_Email;
            txtBSN_member_srl.Text = User.BSN_member_srl;
            txtBSN_nick_name.Text = User.BSN_nick_name;
            txtBSN_is_admin.Text = User.BSN_is_admin;

            if ((string)btnReload.Content == "Load")
                btnReload.Content = "Reload";
            else
                WPFCom.Message("Debug data reloaded.");
        }
    }
}
