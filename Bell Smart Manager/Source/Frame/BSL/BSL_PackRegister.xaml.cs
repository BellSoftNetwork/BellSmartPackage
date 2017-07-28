using BellLib.Class;
using BellLib.Class.BSN;
using BellLib.Data;
using System;
using System.Linq;
using System.Net;
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
    /// BSL_PackMaker.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BSL_PackRegister : Window
    {
        public BSL_PackRegister()
        {
            InitializeComponent();
            Initialize();
            // 로그인한 사용자가 팩 생성권한이 있는지 확인

            //tiBasePack.Visibility = System.Windows.Visibility.Collapsed; // 베이스팩 생성불가
        }

        private void Initialize()
        {
            //초기화
            cbBasePack.Items.Clear();

            //값 로드
            string[] data = BSN_BSL.LoadPackList(BSN_BSL.PACK.basepack);
            foreach (string value in data)
            {
                cbBasePack.Tag += Common.getElement(value, "id") + "|";
                cbBasePack.Items.Add(Common.getElement(value, "name"));
            }

            //기본값 선택
            cbBasePack.SelectedIndex = 0;

            if (cbBasePack.Items.IsEmpty) // 활성화된 베이스팩이 없다면,
                tiModPack.Visibility = Visibility.Collapsed; // 모드팩 탭을 지움.

            foreach (TabItem ti in tcGlobal.Items)
            {
                if (ti.Visibility == Visibility.Visible)
                {
                    ti.IsSelected = true;
                    return;
                }
            }
        }

        private void btnModRegister_Click(object sender, RoutedEventArgs e)
        {
            //필드 검사
            if (txtModName.Text == "" || txtModNotice.Text == "" || cbBasePack.SelectedIndex == -1)
            {
                WPFCom.Message("항목을 전부 입력해주세요.", Basic.PROJECT.Bell_Smart_Manager);
                return;
            }
            /*if (!BSN_BSM.CheckUID(txtModUID.Text))
            {
                WPFCom.Message("비 정상적인 UID 값입니다.");
                return;
            }*/
            // 공지사항 주소 유효성 검증
            
            //등록 시작
            btnModRegister.IsEnabled = false;
            string baseid = cbBasePack.Tag.ToString().Split('|')[cbBasePack.SelectedIndex];
            if (BSN_BSM.RegisterModPack(txtModName.Text, baseid, txtModDetail.Text, txtModNotice.Text, User.BSN_member_srl))
                WPFCom.Message("정상적으로 신청되었습니다.", Basic.PROJECT.Bell_Smart_Manager);
            else
                WPFCom.Message("등록에 실패하였습니다.", Basic.PROJECT.Bell_Smart_Manager);
            btnModRegister.IsEnabled = true;
        }

        private void btnBaseRegister_Click(object sender, RoutedEventArgs e)
        {
            //필드 검사
            if (txtBaseName.Text == "" || cbBaseMCVer.SelectedIndex == -1)
            {
                WPFCom.Message("항목을 전부 입력해주세요.", Basic.PROJECT.Bell_Smart_Manager);
                return;
            }
            /*if (!BSN_BSM.CheckUID(txtBaseUID.Text))
            {
                WPFCom.Message("비 정상적인 UID 값입니다.");
                return;
            }*/

            //등록 시작
            btnBaseRegister.IsEnabled = false;
            if (BSN_BSM.RegisterBasePack(txtBaseName.Text, (string)((ComboBoxItem)cbBaseMCVer.SelectedItem).Content, User.BSN_member_srl))
                WPFCom.Message("정상적으로 신청되었습니다.", Basic.PROJECT.Bell_Smart_Manager);
            else
                WPFCom.Message("등록에 실패하였습니다.", Basic.PROJECT.Bell_Smart_Manager);
            btnBaseRegister.IsEnabled = true;
        }

        private void btnResRegister_Click(object sender, RoutedEventArgs e)
        {
            // 필드검사
            if (txtResNotice.Text == "" || txtResName.Text == "" || cbResMCVer.SelectedIndex == -1 || txtResDetail.Text == "")
            {
                WPFCom.Message("항목을 전부 입력해주세요.", Basic.PROJECT.Bell_Smart_Manager);
                return;
            }
            /*if (!BSN_BSM.CheckUID(txtResUID.Text))
            {
                WPFCom.Message("비 정상적인 UID 값입니다.");
                return;
            }*/
            // 공지사항 유효성 검증

            // 등록 시작
            btnResRegister.IsEnabled = false;
            string type = "resourcepack";
            if (rbMapPack.IsChecked == true)
                type = "mappack";
            if (BSN_BSM.RegisterResourcePack(type, txtResName.Text, (string)((ComboBoxItem)cbResMCVer.SelectedItem).Content, txtResDetail.Text, txtModNotice.Text, User.BSN_member_srl))
                WPFCom.Message("정상적으로 신청되었습니다.", Basic.PROJECT.Bell_Smart_Manager);
            else
                WPFCom.Message("등록에 실패하였습니다.", Basic.PROJECT.Bell_Smart_Manager);
            btnResRegister.IsEnabled = true;
        }

        private void btnRunRegister_Click(object sender, RoutedEventArgs e)
        {
            // 필드검사
            if (txtRunName.Text == "")
            {
                WPFCom.Message("항목을 전부 입력해주세요.", Basic.PROJECT.Bell_Smart_Manager);
                return;
            }

            // 등록 시작
            btnRunRegister.IsEnabled = false;
            if (BSN_BSM.RegisterRuntime(txtRunName.Text, User.BSN_member_srl))
                WPFCom.Message("정상적으로 신청되었습니다.", Basic.PROJECT.Bell_Smart_Manager);
            else
                WPFCom.Message("등록에 실패하였습니다.", Basic.PROJECT.Bell_Smart_Manager);
            btnRunRegister.IsEnabled = true;
        }
    }
}
