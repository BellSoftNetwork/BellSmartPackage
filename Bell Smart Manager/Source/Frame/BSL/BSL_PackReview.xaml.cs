using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            btnInfoApproval.IsEnabled = false;
            btnInfoRefusal.IsEnabled = false;
            lbInfoBUID.Content = null;
            lbInfoMCVer.Content = null;
            lbInfoName.Content = null;
            lbInfoProNick.Content = null;
            lbInfoType.Content = null;
            lbInfoMade.Content = null;
            txtInfoDetail.Text = null;
            txtInfoProEmail.Text = null;
            lbInfoDetail.Visibility = Visibility.Visible;
            txtInfoDetail.Visibility = Visibility.Visible;
            lbInfoBasePack.Visibility = Visibility.Visible;
            lbInfoBUID.Visibility = Visibility.Visible;
            lbInfoMC.Visibility = Visibility.Collapsed;
            lbInfoMCVer.Visibility = Visibility.Collapsed;


            // 버전 검토
            lstVerList.Items.Clear();
            lstVerServers.Items.Clear();
            lstVerFile.Items.Clear();
            lbVerType.Content = null;
            lbVerName.Content = null;
            lbVerVersion.Content = null;
            lbVerBUID.Content = null;
            lbVerBPVer.Content = null;
            
            /// 데이터 로드
            // 검토 요약


            // 정보 검토
            btnInfoRefresh_Click(null, null);

            // 버전 검토
            btnVerRefresh_Click(null, null);
        }

        private void btnInfoRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstInfoList.Items.Clear();
            lstInfoList.SelectedIndex = -1;
            lstInfoList.Tag = null;

            int TYPE = cbInfoType.SelectedIndex;
            if (TYPE == 0 || TYPE == 1)
                foreach (string tmp in BSN_BSM.LoadReviewList(BSN_BSL.PACK.modpack))
                {
                    lstInfoList.Items.Add(tmp);
                    lstInfoList.Tag += BSN_BSL.PACK.modpack + "|";
                }
            if (TYPE == 0 || TYPE == 2)
                foreach (string tmp in BSN_BSM.LoadReviewList(BSN_BSL.PACK.basepack))
                {
                    lstInfoList.Items.Add(tmp);
                    lstInfoList.Tag += BSN_BSL.PACK.basepack + "|";
                }
            if (TYPE == 0 || TYPE == 3)
                foreach (string tmp in BSN_BSM.LoadReviewList(BSN_BSL.PACK.resource))
                {
                    lstInfoList.Items.Add(tmp);
                    lstInfoList.Tag += BSN_BSL.PACK.resource + "|";
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
            btnInfoApproval.IsEnabled = false;
            btnInfoRefusal.IsEnabled = false;

            // 검사
            if (lstInfoList.Tag == null || lstInfoList.SelectedIndex == -1)
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
            lbInfoName.Content = null;
            lbInfoBUID.Content = null;
            lbInfoMCVer.Content = null;
            lbInfoProNick.Content = null;
            lbInfoMade.Content = null;
            txtInfoDetail.Text = null;
            txtInfoProEmail.Text = null;

            // 로드
            lbInfoName.Content = (string)lstInfoList.SelectedItem;
            string name; //, id, name, latest, recommended, state, plan, detail, start, endtime;
            switch (kind)
            {
                case "modpack":
                    name = (string)lstInfoList.SelectedItem;
                    //string SelBUID;
                    BSN_BSL.ModPack mp = new BSN_BSL.ModPack();
                    mp = BSN_BSL.LoadModPackDetail(name); //, out id, out name, out latest, out recommended, out SelBUID, out state, out plan, out detail, out start, out endtime))
                                                         // 로드 성공시
                    lbInfoType.Content = "모드팩";
                    lbInfoType.Foreground = new SolidColorBrush(Colors.Blue);
                    lbInfoName.Content = mp.name;
                    lbInfoBUID.Content = mp.BaseName;
                    lbInfoMade.Content = mp.made;
                    txtInfoDetail.Text = mp.detail;
                    foreach (BSN_BSL.Manager member in BSN_BSL.LoadPackManager(BSN_BSL.PACK.modpack, name))
                    {
                        if (member.permission == "4")
                        {
                            txtInfoProEmail.Text = member.email;
                            lbInfoProNick.Content = BSN_Info.GetNickName(member.member_srl);
                        }
                    }
                    lbInfoDetail.Visibility = Visibility.Visible;
                    txtInfoDetail.Visibility = Visibility.Visible;
                    lbInfoBasePack.Visibility = Visibility.Visible;
                    lbInfoBUID.Visibility = Visibility.Visible;
                    lbInfoMC.Visibility = Visibility.Collapsed;
                    lbInfoMCVer.Visibility = Visibility.Collapsed;
                    break;

                case "basepack":
                    name = (string)lstInfoList.SelectedItem;
                    //string mcversion;
                    BSN_BSL.BasePack bp = new BSN_BSL.BasePack();

                    bp = BSN_BSL.LoadBasePackDetail(name); //, out id, out latest, out recommended, out state, out mcversion, out plan, out start, out endtime))
                                                          // 로드 성공시
                    lbInfoType.Content = "베이스팩";
                    lbInfoType.Foreground = new SolidColorBrush(Colors.Red);
                    lbInfoName.Content = bp.name;
                    lbInfoMade.Content = bp.made;
                    lbInfoMCVer.Content = bp.mcversion;
                    foreach (BSN_BSL.Manager member in BSN_BSL.LoadPackManager(BSN_BSL.PACK.basepack, name))
                    {
                        if (member.permission == "4")
                        {
                            txtInfoProEmail.Text = member.email;
                            lbInfoProNick.Content = BSN_Info.GetNickName(member.member_srl);
                        }
                    }
                    lbInfoDetail.Visibility = Visibility.Collapsed;
                    txtInfoDetail.Visibility = Visibility.Collapsed;
                    lbInfoBasePack.Visibility = Visibility.Collapsed;
                    lbInfoBUID.Visibility = Visibility.Collapsed;
                    lbInfoMC.Visibility = Visibility.Visible;
                    lbInfoMCVer.Visibility = Visibility.Visible;
                    break;

                case "resource":
                    name = (string)lstInfoList.SelectedItem;
                    //string type;
                    BSN_BSL.Resource res = new BSN_BSL.Resource();

                    res = BSN_BSL.LoadResPackDetail(name); //, out id, out type, out name, out latest, out recommended, out state, out mcversion, out plan, out detail, out start, out endtime))
                    lbInfoType.Foreground = new SolidColorBrush(Colors.Green);
                    lbInfoType.Content = res.type;
                    lbInfoName.Content = res.name;
                    lbInfoMCVer.Content = res.mcversion;
                    lbInfoMade.Content = res.made;
                    txtInfoDetail.Text = res.detail;
                    foreach (BSN_BSL.Manager member in BSN_BSL.LoadPackManager(BSN_BSL.PACK.resource, name))
                    {
                        if (member.permission == "4")
                        {
                            txtInfoProEmail.Text = member.email;
                            lbInfoProNick.Content = BSN_Info.GetNickName(member.member_srl);
                        }
                    }
                    lbInfoDetail.Visibility = Visibility.Visible;
                    txtInfoDetail.Visibility = Visibility.Visible;
                    lbInfoBasePack.Visibility = Visibility.Collapsed;
                    lbInfoBUID.Visibility = Visibility.Collapsed;
                    lbInfoMC.Visibility = Visibility.Visible;
                    lbInfoMCVer.Visibility = Visibility.Visible;
                    break;
            }
            btnInfoApproval.IsEnabled = true;
            btnInfoRefusal.IsEnabled = true;
        }

        private void btnInfoApproval_Click(object sender, RoutedEventArgs e)
        {
            if (lstInfoList.SelectedIndex == -1)
                return;
            BSN_BSL.PACK kind = BSN_BSL.PACK.modpack;
            switch (lstInfoList.Tag.ToString().Split('|')[lstInfoList.SelectedIndex])
            {
                case "modpack":
                    kind = BSN_BSL.PACK.modpack;
                    break;

                case "basepack":
                    kind = BSN_BSL.PACK.basepack;
                    break;

                case "resource":
                    kind = BSN_BSL.PACK.resource;
                    break;

                default:
                    WPFCom.Message("비정상적인 접근입니다.");
                    return;
            }

            if (BSN_BSM.ApprovalPack(kind, (string)lbInfoName.Content, true))
            {
                //btnInfoRefresh_Click(sender, e);
                Initialize();
                WPFCom.Message("성공적으로 활성화 하였습니다.");
            }
            else
                WPFCom.Message("팩 활성화에 실패하였습니다.");
        }

        private void btnInfoRefusal_Click(object sender, RoutedEventArgs e)
        {
            if (lstInfoList.SelectedIndex == -1)
                return;
            BSN_BSL.PACK kind = BSN_BSL.PACK.modpack;
            switch (lstInfoList.Tag.ToString().Split('|')[lstInfoList.SelectedIndex])
            {
                case "modpack":
                    kind = BSN_BSL.PACK.modpack;
                    break;

                case "basepack":
                    kind = BSN_BSL.PACK.basepack;
                    break;

                case "resource":
                    kind = BSN_BSL.PACK.resource;
                    break;

                default:
                    WPFCom.Message("비정상적인 접근입니다.");
                    return;
            }

            if (BSN_BSM.ApprovalPack(kind, (string)lbInfoName.Content, false))
            {
                //btnInfoRefresh_Click(sender, e);
                Initialize();
                WPFCom.Message("성공적으로 비활성화 하였습니다.");
            }
            else
                WPFCom.Message("팩 비활성화에 실패하였습니다.");
        }

        private void btnVerRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstVerList.Items.Clear();
            lstVerList.SelectedIndex = -1;
            lstVerList.Tag = null;

            int TYPE = cbVerType.SelectedIndex;
            if (TYPE == 0 || TYPE == 1)
                foreach (string tmp in BSN_BSM.LoadVersionReviewList(BSN_BSL.PACK.modpack))
                {
                    lstVerList.Items.Add(Common.getElement(tmp, "name") + " - " + Common.getElement(tmp, "version"));
                    lstVerList.Tag += BSN_BSL.PACK.modpack + "-" + Common.getElement(tmp, "name") + "-" + Common.getElement(tmp, "id") + "|";
                }
            if (TYPE == 0 || TYPE == 2)
                foreach (string tmp in BSN_BSM.LoadVersionReviewList(BSN_BSL.PACK.basepack))
                {
                    lstVerList.Items.Add(Common.getElement(tmp, "name") + " - " + Common.getElement(tmp, "version"));
                    lstVerList.Tag += BSN_BSL.PACK.basepack + "-" + Common.getElement(tmp, "name") + "-" + Common.getElement(tmp, "id") + "|";
                }
            if (TYPE == 0 || TYPE == 3)
                foreach (string tmp in BSN_BSM.LoadVersionReviewList(BSN_BSL.PACK.resource))
                {
                    lstVerList.Items.Add(Common.getElement(tmp, "name") + " - " + Common.getElement(tmp, "version"));
                    lstVerList.Tag += BSN_BSL.PACK.resource + "-" + Common.getElement(tmp, "name") + "-" + Common.getElement(tmp, "id") + "|";
                }

            lstVerList.SelectedIndex = 0;
        }

        private void btnVerApproval_Click(object sender, RoutedEventArgs e)
        {
            if (lstVerList.SelectedIndex == -1)
                return;
            BSN_BSL.PACK kind = BSN_BSL.PACK.modpack;
            switch (lstVerList.Tag.ToString().Split('|')[lstVerList.SelectedIndex].Split('-')[0])
            {
                case "modpack":
                    kind = BSN_BSL.PACK.modpack;
                    break;

                case "basepack":
                    kind = BSN_BSL.PACK.basepack;
                    break;

                case "resource":
                    kind = BSN_BSL.PACK.resource;
                    break;

                default:
                    WPFCom.Message("비정상적인 접근입니다.");
                    return;
            }

            if (BSN_BSM.ApprovalVersion(kind, lstVerList.Tag.ToString().Split('|')[lstVerList.SelectedIndex].Split('-')[2], true))
            {
                Initialize();
                WPFCom.Message("성공적으로 활성화 하였습니다.");
            }
            else
                WPFCom.Message("버전 활성화에 실패하였습니다.");
        }

        private void btnVerRefusal_Click(object sender, RoutedEventArgs e)
        {
            if (lstVerList.SelectedIndex == -1)
                return;
            BSN_BSL.PACK kind = BSN_BSL.PACK.modpack;
            switch (lstVerList.Tag.ToString().Split('|')[lstVerList.SelectedIndex].Split('-')[0])
            {
                case "modpack":
                    kind = BSN_BSL.PACK.modpack;
                    break;

                case "basepack":
                    kind = BSN_BSL.PACK.basepack;
                    break;

                case "resource":
                    kind = BSN_BSL.PACK.resource;
                    break;

                default:
                    WPFCom.Message("비정상적인 접근입니다.");
                    return;
            }

            if (BSN_BSM.ApprovalVersion(kind, lstVerList.Tag.ToString().Split('|')[lstVerList.SelectedIndex].Split('-')[2], false))
            {
                Initialize();
                WPFCom.Message("성공적으로 비활성화 하였습니다.");
            }
            else
                WPFCom.Message("버전 비활성화에 실패하였습니다.");
        }

        private void cbVerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbVerType.IsInitialized)
                Initialize();
        }

        private void lstVerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnVerApproval.IsEnabled = false;
            btnVerRefusal.IsEnabled = false;

            // 검사
            if (lstVerList.Tag == null || lstVerList.SelectedIndex == -1)
                return;
            
            string[] tagData;
            try
            {
                tagData = lstVerList.Tag.ToString().Split('|')[lstVerList.SelectedIndex].Split('-');
            }
            catch
            {
                return;
            }
            BSN_BSL.PACK type = new BSN_BSL.PACK();

            // 초기화
            lbVerType.Content = null;
            lbVerName.Content = null;
            lbVerBUID.Content = null;
            lbVerBPVer.Content = null;
            lstVerServers.Items.Clear();
            lstVerFile.Items.Clear();

            // 로드
            string name = tagData[1];
            switch (tagData[0])
            {
                case "modpack":
                    type = BSN_BSL.PACK.modpack;
                    break;

                case "basepack":
                    type = BSN_BSL.PACK.basepack;
                    break;

                case "resource":
                    type = BSN_BSL.PACK.resource;
                    break;
            }

            switch (type)
            {
                case BSN_BSL.PACK.modpack:
                    lbVerType.Content = "모드팩";
                    lbVerType.Foreground = new SolidColorBrush(Colors.Blue);

                    lbVerBasePack.Visibility = Visibility.Visible;
                    lbVerBUID.Visibility = Visibility.Visible;
                    lbVerBPVer.Visibility = Visibility.Visible;
                    lbVerBaseVer.Visibility = Visibility.Visible;
                    break;

                case BSN_BSL.PACK.basepack:
                    lbVerType.Content = "베이스팩";
                    lbVerType.Foreground = new SolidColorBrush(Colors.Red);

                    lbVerBasePack.Visibility = Visibility.Collapsed;
                    lbVerBUID.Visibility = Visibility.Collapsed;
                    lbVerBPVer.Visibility = Visibility.Collapsed;
                    lbVerBaseVer.Visibility = Visibility.Collapsed;
                    break;

                case BSN_BSL.PACK.resource:
                    lbVerType.Content = "리소스";
                    lbVerType.Foreground = new SolidColorBrush(Colors.Green);

                    // 리소스 타입 로드
                    lbVerBasePack.Visibility = Visibility.Collapsed;
                    lbVerBUID.Visibility = Visibility.Collapsed;
                    lbVerBPVer.Visibility = Visibility.Collapsed;
                    lbVerBaseVer.Visibility = Visibility.Collapsed;
                    break;
            }
            // 업로드된 서버 리스트 로드
            foreach (string server in BSN_BSL.LoadVersionServer(type, BSN_BSL.KIND.client, tagData[2]))
            {
                BSN_BSL.Server sv = BSN_BSL.LoadServerDetail(server);
                lstVerServers.Items.Add(sv.name);
            }

            // 업로드된 클라이언트 파일 로드
            foreach (BSN_BSL.Install data in BSN_BSL.LoadVersionFiles(type, BSN_BSL.KIND.client, tagData[2]))
                lstVerFile.Items.Add(data);

            lbVerName.Content = lstVerList.SelectedItem.ToString().Split('-')[0];
            lbVerVersion.Content = lstVerList.SelectedItem.ToString().Split('-')[1];
            btnVerApproval.IsEnabled = true;
            btnVerRefusal.IsEnabled = true;
        }
    }
}
