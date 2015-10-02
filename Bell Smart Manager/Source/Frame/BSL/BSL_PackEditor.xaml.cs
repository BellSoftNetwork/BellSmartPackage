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
using BellLib.Class;
using BellLib.Data;

class auth
{
    public string email { get; set; }
    public string permission { get; set; }
}

namespace Bell_Smart_Manager.Source.Frame.BSL
{
    /// <summary>
    /// BSL_PackEditor.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BSL_PackEditor : Window
    {
        public BSL_PackEditor()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            // 필드 초기화
            cbMUID.Items.Clear();

            // 데이터 로드
            foreach (string tmp in BSN_Info.loadModPack(User.BSN_member_srl))
                cbMUID.Items.Add(tmp);

            // 기본값 선택
            cbMUID.SelectedIndex = 0;
        }

        private void btnModAdd_Click(object sender, RoutedEventArgs e)
        {
            lstModPermission.Items.Add(new auth() { email = "이메일", permission = "권한" });
        }

        private void btnModLoad_Click(object sender, RoutedEventArgs e)
        {
            // 로드 성공시
            gbMod.IsEnabled = false;
            tcMod.IsEnabled = true;
        }

        private void btnBaseLoad_Click(object sender, RoutedEventArgs e)
        {
            // 로드 성공시
            gbBase.IsEnabled = false;
            tcBase.IsEnabled = true;
        }

        private void btnResourceLoad_Click(object sender, RoutedEventArgs e)
        {
            // 로드 성공시
            gbResource.IsEnabled = false;
            tcResource.IsEnabled = true;
        }
    }
}
