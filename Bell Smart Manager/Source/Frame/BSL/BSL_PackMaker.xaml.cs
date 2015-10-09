﻿using BellLib.Class;
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
    public partial class BSL_PackMaker : Window
    {
        public BSL_PackMaker()
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
                cbBasePack.Items.Add(Common.getElement(value, "UID"));
            }

            //기본값 선택
            cbBasePack.SelectedIndex = 0;
        }

        private void btnModRegister_Click(object sender, RoutedEventArgs e)
        {
            //필드 검사
            if (txtModUID.Text == "" || txtModName.Text == "" || cbBasePack.SelectedIndex == -1)
            {
                WPFCom.Message("항목을 전부 입력해주세요.");
                return;
            }
            if (!BSN_BSM.CheckUID(txtModUID.Text))
            {
                WPFCom.Message("비 정상적인 UID 값입니다.");
                return;
            }
            
            //등록 시작
            btnModRegister.IsEnabled = false;
            string baseid = cbBasePack.Tag.ToString().Split('|')[cbBasePack.SelectedIndex];
            if (BSN_BSM.RegisterModPack(txtModUID.Text, txtModName.Text, baseid, txtModDetail.Text, User.BSN_member_srl))
                WPFCom.Message("정상적으로 신청되었습니다.");
            else
                WPFCom.Message("등록에 실패하였습니다.");
            btnModRegister.IsEnabled = true;
        }

        private void btnBaseRegister_Click(object sender, RoutedEventArgs e)
        {
            //필드 검사
            if (txtBaseUID.Text == "" || txtBaseName.Text == "" || txtBaseMCVer.Text == "")
            {
                WPFCom.Message("항목을 전부 입력해주세요.");
                return;
            }
            if (!BSN_BSM.CheckUID(txtBaseUID.Text))
            {
                WPFCom.Message("비 정상적인 UID 값입니다.");
                return;
            }

            //등록 시작
            btnBaseRegister.IsEnabled = false;
            if (BSN_BSM.RegisterBasePack(txtBaseUID.Text, txtBaseName.Text, txtBaseMCVer.Text, User.BSN_member_srl))
                WPFCom.Message("정상적으로 신청되었습니다.");
            else
                WPFCom.Message("등록에 실패하였습니다.");
            btnBaseRegister.IsEnabled = true;
        }

        private void btnResRegister_Click(object sender, RoutedEventArgs e)
        {
            // 필드검사
            if (txtResUID.Text == "" || txtResName.Text == "" || txtResMCVer.Text == "" || txtResDetail.Text == "")
            {
                WPFCom.Message("항목을 전부 입력해주세요.");
                return;
            }
            if (!BSN_BSM.CheckUID(txtResUID.Text))
            {
                WPFCom.Message("비 정상적인 UID 값입니다.");
                return;
            }

            // 등록 시작
            btnResRegister.IsEnabled = false;
            string type = "resourcepack";
            if (rbMapPack.IsChecked == true)
                type = "mappack";
            if (BSN_BSM.RegisterResourcePack(txtResUID.Text, type, txtResName.Text, txtResMCVer.Text, txtResDetail.Text, User.BSN_member_srl))
                WPFCom.Message("정상적으로 신청되었습니다.");
            else
                WPFCom.Message("등록에 실패하였습니다.");
            btnResRegister.IsEnabled = true;
        }
    }
}
