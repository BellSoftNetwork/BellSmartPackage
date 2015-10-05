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
using BellLib.Class.BSN;
using BellLib.Class;

namespace Bell_Smart_Manager.Source.Frame.BSL
{
    /// <summary>
    /// BSL_PackReview.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BSL_PackReview : Window
    {
        public BSL_PackReview()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// 검토기를 초기화합니다.
        /// </summary>
        private void Initialize()
        {
            /// 데이터 초기화
            // 검토 요약


            // 정보 검토
            lbInfoBUID.Content = null;
            lbInfoMCVer.Content = null;
            lbInfoName.Content = null;
            lbInfoProNick.Content = null;
            lbInfoType.Content = null;
            lbInfoUID.Content = null;
            txtInfoDetail.Text = null;
            txtInfoProEmail.Text = null;
            

            // 버전 검토
            lstVerList.Items.Clear();


            /// 데이터 로드
            // 검토 요약


            // 정보 검토
            btnInfoRefresh_Click(null, null);

            // 버전 검토

        }

        private void btnInfoRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstInfoList.Items.Clear();
            lstInfoList.Tag = null;

            int TYPE = cbInfoType.SelectedIndex;
            if (TYPE == 0 || TYPE == 1)
                foreach (string tmp in BSN_BSM.loadReviewList(BSN_BSL.PACK.ModPack))
                {
                    lstInfoList.Items.Add(tmp);
                    lstInfoList.Tag += BSN_BSL.PACK.ModPack + "|";
                }
            if (TYPE == 0 || TYPE == 2)
                foreach (string tmp in BSN_BSM.loadReviewList(BSN_BSL.PACK.BasePack))
                {
                    lstInfoList.Items.Add(tmp);
                    lstInfoList.Tag += BSN_BSL.PACK.BasePack + "|";
                }
            if (TYPE == 0 || TYPE == 3)
                foreach (string tmp in BSN_BSM.loadReviewList(BSN_BSL.PACK.Resource))
                {
                    lstInfoList.Items.Add(tmp);
                    lstInfoList.Tag += BSN_BSL.PACK.Resource + "|";
                }

            lstInfoList.SelectedIndex = 0;
        }

        private void cbInfoType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbInfoType.IsInitialized)
                Initialize();
        }

        private void lstInfoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 검사
            if (lstInfoList.Tag == null)
                return;

            string kind;
            try
            {
                kind = lstInfoList.Tag.ToString().Split('|')[lstInfoList.SelectedIndex];
            }
            catch
            {
                return;
            }

            // 초기화
            lbInfoType.Content = null;
            lbInfoUID.Content = null;
            lbInfoName.Content = null;
            lbInfoBUID.Content = null;
            lbInfoMCVer.Content = null;
            lbInfoProNick.Content = null;
            lbInfoStart.Content = null;
            txtInfoDetail.Text = null;
            txtInfoProEmail.Text = null;

            // 로드
            lbInfoUID.Content = (string)lstInfoList.SelectedItem;
            switch (kind)
            {
                case "ModPack":
                    string MUID = (string)lstInfoList.SelectedItem;
                    string id, name, latest, recommended, SelBUID, state, plan, detail, start, endtime;
                    BSN_BSL.loadModPackDetail(MUID, out id, out name, out latest, out recommended, out SelBUID, out state, out plan, out detail, out start, out endtime);
                    lbInfoType.Content = "모드팩";
                    lbInfoType.Foreground = new SolidColorBrush(Colors.Blue);
                    lbInfoName.Content = name;
                    lbInfoBUID.Content = SelBUID;
                    lbInfoStart.Content = start;
                    txtInfoDetail.Text = detail;
                    /*foreach (BSN_BSL.Manager member in BSN_BSL.loadPackManager(BSN_BSL.PACK.ModPack, MUID))
                    {
                        if (member.permission == "4")
                        {
                            txtInfoProEmail.Text = member.email;
                            lbInfoProNick.Content = BSN_Info.getNickName(member.member_srl);
                        }
                    }*/ // 렉이 심해서 임시로 주석처리
                    break;

                case "BasePack":
                    string BUID = (string)lstInfoList.SelectedItem;

                    lbInfoType.Content = "베이스팩";
                    lbInfoType.Foreground = new SolidColorBrush(Colors.Red);
                    lbInfoStart.Content = "시작일";
                    lbInfoProNick.Content = "제작자 닉네임";
                    txtInfoProEmail.Text = "제작자 이메일";
                    break;

                case "Resource":
                    string RUID = (string)lstInfoList.SelectedItem;

                    switch ("리소스타입")
                    {
                        case "리소스팩":
                            lbInfoType.Content = "리소스팩";
                            break;

                        case "맵팩":
                            lbInfoType.Content = "맵팩";
                    }
                    lbInfoType.Foreground = new SolidColorBrush(Colors.Green);
                    lbInfoName.Content = "리소스 이름";
                    lbInfoMCVer.Content = "마크 버전";
                    lbInfoStart.Content = "생성일";
                    lbInfoProNick.Content = "제작자 닉네임";
                    txtInfoProEmail.Text = "제작자 이메일";
                    txtInfoDetail.Text = "상세정보";
                    break;
            }
        }

        private void btnInfoApproval_Click(object sender, RoutedEventArgs e)
        {
            BSN_BSL.PACK kind = BSN_BSL.PACK.ModPack;
            switch (lstInfoList.Tag.ToString().Split('|')[lstInfoList.SelectedIndex])
            {
                case "ModPack":
                    kind = BSN_BSL.PACK.ModPack;
                    break;

                case "BasePack":
                    kind = BSN_BSL.PACK.BasePack;
                    break;

                case "Resource":
                    kind = BSN_BSL.PACK.Resource;
                    break;

                default:
                    WPFCom.Message("비정상적인 접근입니다.");
                    return;
            }

            if (BSN_BSM.approvalPack(kind, (string)lbInfoUID.Content, true))
            {
                btnInfoRefresh_Click(sender, e);
                WPFCom.Message("성공적으로 활성화 하였습니다.");
            }
            else
                WPFCom.Message("팩 활성화에 실패하였습니다.");
        }

        private void btnInfoRefusal_Click(object sender, RoutedEventArgs e)
        {
            BSN_BSL.PACK kind = BSN_BSL.PACK.ModPack;
            switch (lstInfoList.Tag.ToString().Split('|')[lstInfoList.SelectedIndex])
            {
                case "ModPack":
                    kind = BSN_BSL.PACK.ModPack;
                    break;

                case "BasePack":
                    kind = BSN_BSL.PACK.BasePack;
                    break;

                case "Resource":
                    kind = BSN_BSL.PACK.Resource;
                    break;

                default:
                    WPFCom.Message("비정상적인 접근입니다.");
                    return;
            }

            if (BSN_BSM.approvalPack(kind, (string)lbInfoUID.Content, false))
            {
                btnInfoRefresh_Click(sender, e);
                WPFCom.Message("성공적으로 비활성화 하였습니다.");
            }
            else
                WPFCom.Message("팩 비활성화에 실패하였습니다.");
        }
    }
}
