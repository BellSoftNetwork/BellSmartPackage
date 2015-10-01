using BellLib.Class;
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
    public partial class BSL_PackMaker : Window
    {
        public BSL_PackMaker()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            //초기화
            cbBasePack.Items.Clear();

            //값 로드
            string[] data = BSN_Info.loadPackList(BSN_Info.PACK.BasePack);
            foreach (string value in data)
            {
                cbBasePack.Tag += Common.getElement(value, "id") + "|";
                cbBasePack.Items.Add(Common.getElement(value, "BUID"));
            }

            //기본값 선택
            cbBasePack.SelectedIndex = 0;
        }

        private void btnModRegister_Click(object sender, RoutedEventArgs e)
        {
            //필드 검사
            if (txtMUID.Text == "" && txtName.Text == "" && cbBasePack.SelectedIndex == -1)
            {
                WPFCom.Message("항목을 전부 입력해주세요.");
                return;
            }

            //등록 시작
            btnModRegister.IsEnabled = false;
            string baseid = cbBasePack.Tag.ToString().Split('|')[cbBasePack.SelectedIndex];
            if (BSN_Info.registerModPack(txtMUID.Text, txtName.Text, baseid, txtDetail.Text))
                WPFCom.Message("정상적으로 신청되었습니다.");
            else
                WPFCom.Message("등록에 실패하였습니다.");
            btnModRegister.IsEnabled = true;
        }

        private void btnBaseRegister_Click(object sender, RoutedEventArgs e)
        {
            //필드 검사
            if (txtBUID.Text == "" && txtMCVer.Text == "")
            {
                WPFCom.Message("항목을 전부 입력해주세요.");
                return;
            }

            //등록 시작
            btnBaseRegister.IsEnabled = false;
            if (BSN_Info.registerBasePack(txtBUID.Text, txtMCVer.Text))
                WPFCom.Message("정상적으로 신청되었습니다.");
            else
                WPFCom.Message("등록에 실패하였습니다.");
            btnBaseRegister.IsEnabled = true;
        }
    }
}
