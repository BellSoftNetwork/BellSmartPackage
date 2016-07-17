using BellLib.Class;
using BellLib.Class.BSN;
using BellLib.Data;
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

namespace Bell_Smart_Manager.Source.Frame.BSL
{
    /// <summary>
    /// BSL_ServerMaker.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BSL_ServerRegister : Window
    {
        public BSL_ServerRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtAddress.Text == string.Empty || txtName.Text == string.Empty || txtPort.Text == string.Empty)
            {
                WPFCom.Message("모든 항목을 입력해주세요.", Base.PROJECT.Bell_Smart_Manager);
                return;
            }

            string require_plan = cbPlan.SelectedIndex.ToString();
            switch (cbPlan.SelectedIndex)
            {
                case 3:
                    require_plan = "10";
                    break;
            }
            if (BSN_BSM.RegisterServer(txtName.Text, cbType.SelectedIndex.ToString(), cbUpload.SelectedIndex.ToString(), cbDownload.SelectedIndex.ToString(), txtAddress.Text, txtPort.Text, require_plan, User.BSN_member_srl))
                WPFCom.Message("서버정보가 정상적으로 등록되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            else
                WPFCom.Message("서버정보 등록에 실패했습니다.", Base.PROJECT.Bell_Smart_Manager);
        }
    }
}
