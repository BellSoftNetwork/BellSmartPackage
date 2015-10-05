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
using BellLib.Class.BSN;

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
            cbBUID.Items.Clear();
            cbRUID.Items.Clear();

            // 모드팩 데이터 로드
            foreach (string tmp in BSN_BSM.loadPackList(BSN_BSL.PACK.ModPack, User.BSN_member_srl))
                cbMUID.Items.Add(Common.getElement(tmp, "MUID"));
            // 베이스팩 데이터 로드
            foreach (string tmp in BSN_BSM.loadPackList(BSN_BSL.PACK.BasePack, User.BSN_member_srl))
                cbBUID.Items.Add(Common.getElement(tmp, "BUID"));
            // 리소스 데이터 로드
            foreach (string tmp in BSN_BSM.loadPackList(BSN_BSL.PACK.Resource, User.BSN_member_srl))
                cbRUID.Items.Add(Common.getElement(tmp, "RUID"));

            // 기본값 선택
            cbMUID.SelectedIndex = 0;
            cbBUID.SelectedIndex = 0;
            cbRUID.SelectedIndex = 0;
        }
        
        private void btnModLoad_Click(object sender, RoutedEventArgs e)
        {
            // 필드
            string MUID = (string)cbMUID.SelectedItem;
            string id, name, latest, recommended, BUID, state, plan, detail, start, endtime;

            // 초기화
            tcMod.IsEnabled = false;
            cbModActivate.IsChecked = false;

            // 로드
            if (BSN_BSL.loadModPackDetail(MUID, out id, out name, out latest, out recommended, out BUID, out state, out plan, out detail, out start, out endtime))
            {
                // 로드 성공시
                lbModName.Content = name;
                lbModLatest.Content = latest;
                foreach (string tmp in BSN_BSL.loadPackVersion(BSN_BSL.PACK.ModPack, MUID, true))
                    cbModRecommended.Items.Add(tmp);
                cbModRecommended.SelectedItem = recommended; //cbModRecommended.Items.Add(recommended); // 업로드된 모든 버전 추가 (수정필요)
                //cbModRecommended.SelectedIndex = 0; // 콤보박스에서 권장버전 선택 (수정필요)
                lbModBUID.Content = BUID;
                lbModState.Content = state;
                lbModPlan.Content = plan;
                if (state == "활성화")
                    cbModActivate.IsChecked = true;

                btnModAuthRefresh_Click(sender, e);

                if (state == "사용불가" || state == "검토 요청")
                    return;
                //gbMod.IsEnabled = false;
                tcMod.IsEnabled = true;
            }
        }

        private void btnBaseLoad_Click(object sender, RoutedEventArgs e)
        {
            // 필드
            string BUID = (string)cbBUID.SelectedItem;
            string id, latest, recommended, state, plan, mcversion, start, endtime;

            // 초기화
            tcBase.IsEnabled = false;
            cbBaseActivate.IsChecked = false;

            // 로드
            if (BSN_BSL.loadBasePackDetail(BUID, out id, out latest, out recommended, out state, out mcversion, out plan, out start, out endtime))
            {
                // 로드 성공시
                lbBaseLatest.Content = latest;
                foreach (string tmp in BSN_BSL.loadPackVersion(BSN_BSL.PACK.BasePack, BUID, true))
                    cbBaseRecommended.Items.Add(tmp);
                cbBaseRecommended.SelectedItem = recommended; //cbModRecommended.Items.Add(recommended); // 업로드된 모든 버전 추가 (수정필요)
                //cbModRecommended.SelectedIndex = 0; // 콤보박스에서 권장버전 선택 (수정필요)
                lbBaseState.Content = state;
                lbBasePlan.Content = plan;
                lbBaseMCVer.Content = mcversion;
                if (state == "활성화")
                    cbBaseActivate.IsChecked = true;

                btnBaseAuthRefresh_Click(sender, e);

                if (state == "사용불가" || state == "검토 요청")
                    return;
                //gbBase.IsEnabled = false;
                tcBase.IsEnabled = true;
            }
        }

        private void btnResLoad_Click(object sender, RoutedEventArgs e)
        {
            // 필드
            string RUID = (string)cbRUID.SelectedItem;
            string id, type, name, latest, recommended, state, mcversion, plan, detail, start, endtime;

            // 초기화
            tcResource.IsEnabled = false;
            cbResActivate.IsChecked = false;

            // 로드
            if (BSN_BSL.loadResPackDetail(RUID, out id, out type, out name, out latest, out recommended, out state, out mcversion, out plan, out detail, out start, out endtime))
            {
                // 로드 성공시
                lbResName.Content = name;
                lbResLatest.Content = latest;
                foreach (string tmp in BSN_BSL.loadPackVersion(BSN_BSL.PACK.Resource, RUID, true))
                    cbResRecommended.Items.Add(tmp);
                cbResRecommended.SelectedItem = recommended; //cbModRecommended.Items.Add(recommended); // 업로드된 모든 버전 추가 (수정필요)
                //cbModRecommended.SelectedIndex = 0; // 콤보박스에서 권장버전 선택 (수정필요)
                lbResState.Content = state;
                lbResPlan.Content = plan;
                lbResMCVer.Content = mcversion;
                lbResType.Content = type;
                if (state == "활성화")
                    cbResActivate.IsChecked = true;

                btnResAuthRefresh_Click(sender, e);

                if (state == "사용불가" || state == "검토 요청")
                    return;
                //gbResource.IsEnabled = false;
                tcResource.IsEnabled = true;
            }
        }

        private void btnModSave_Click(object sender, RoutedEventArgs e)
        {
            if (BSN_BSM.modifyPackBasic(BSN_BSL.PACK.ModPack, (string)cbMUID.SelectedItem, (string)cbModRecommended.SelectedItem, (bool)cbModActivate.IsChecked))
            {
                btnModLoad_Click(sender, e);
                WPFCom.Message("기본정보가 성공적으로 수정되었습니다.");
            }
            else
                WPFCom.Message("기본정보 수정에 실패하였습니다.");
        }

        private void btnModDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModSelSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModAuthAdd_Click(object sender, RoutedEventArgs e)
        {
            string member_srl = BSN_Info.getMember_srl(txtModEmail.Text);
            if (member_srl == null)
            {
                WPFCom.Message("존재하지 않는 회원입니다.");
                return;
            }

            if (BSN_BSM.addPackManager(BSN_BSL.PACK.ModPack, (string)cbMUID.SelectedItem, member_srl, cbModPermission.SelectedIndex.ToString()))
            {
                btnModAuthRefresh_Click(sender, e);
                WPFCom.Message("정상적으로 등록되었습니다.");
            }
            else
                WPFCom.Message("관리자 정보등록에 실패하였습니다.");
        }

        private void btnModSelDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModAuthDel_Click(object sender, RoutedEventArgs e)
        {
            string[] data = lstModPermission.Tag.ToString().Split('\n')[lstModPermission.SelectedIndex].Split('|');
            string member_srl = data[0];
            string permission = data[1];

            if (permission == "4")
            {
                WPFCom.Message("제작자권한은 삭제할 수 없습니다.");
                return;
            }

            if (BSN_BSM.delPackManager(BSN_BSL.PACK.ModPack, (string)cbMUID.SelectedItem, member_srl, permission))
            {
                btnModAuthRefresh_Click(sender, e);
                WPFCom.Message("정상적으로 삭제되었습니다.");
            }
            else
                WPFCom.Message("관리자 삭제에 실패하였습니다.");
        }

        private void btnModAuthRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstModPermission.Items.Clear();
            lstModPermission.Tag = null;
            foreach (BSN_BSL.Manager auth in BSN_BSL.loadPackManager(BSN_BSL.PACK.ModPack, (string)cbMUID.SelectedItem))
            {
                lstModPermission.Tag += auth.member_srl + "|" + auth.permission + "\n";
                if (auth.permission != "4")
                {
                    ComboBoxItem cbi = (ComboBoxItem)cbModPermission.Items.GetItemAt(Convert.ToInt32(auth.permission));
                    auth.permission = (string)cbi.Content;
                    lstModPermission.Items.Add(auth);
                }
                else
                {
                    auth.permission = "제작자";
                    lstModPermission.Items.Add(auth);
                }
            }
        }

        private void btnModNews_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModDetail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBaseSave_Click(object sender, RoutedEventArgs e)
        {
            if (BSN_BSM.modifyPackBasic(BSN_BSL.PACK.BasePack, (string)cbBUID.SelectedItem, (string)cbBaseRecommended.SelectedItem, (bool)cbBaseActivate.IsChecked))
            {
                btnBaseLoad_Click(sender, e);
                WPFCom.Message("기본정보가 성공적으로 수정되었습니다.");
            }
            else
                WPFCom.Message("기본정보 수정에 실패하였습니다.");
        }

        private void btnBaseDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBaseSelSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBaseSelDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBaseAuthAdd_Click(object sender, RoutedEventArgs e)
        {
            string member_srl = BSN_Info.getMember_srl(txtBaseEmail.Text);
            if (member_srl == null)
            {
                WPFCom.Message("존재하지 않는 회원입니다.");
                return;
            }

            if (BSN_BSM.addPackManager(BSN_BSL.PACK.BasePack, (string)cbBUID.SelectedItem, member_srl, cbBasePermission.SelectedIndex.ToString()))
            {
                btnBaseAuthRefresh_Click(sender, e);
                WPFCom.Message("정상적으로 등록되었습니다.");
            }
            else
                WPFCom.Message("관리자 정보등록에 실패하였습니다.");
        }

        private void btnBaseAuthDel_Click(object sender, RoutedEventArgs e)
        {
            string[] data = lstBasePermission.Tag.ToString().Split('\n')[lstBasePermission.SelectedIndex].Split('|');
            string member_srl = data[0];
            string permission = data[1];

            if (permission == "4")
            {
                WPFCom.Message("제작자권한은 삭제할 수 없습니다.");
                return;
            }

            if (BSN_BSM.delPackManager(BSN_BSL.PACK.BasePack, (string)cbBUID.SelectedItem, member_srl, permission))
            {
                btnBaseAuthRefresh_Click(sender, e);
                WPFCom.Message("정상적으로 삭제되었습니다.");
            }
            else
                WPFCom.Message("관리자 삭제에 실패하였습니다.");
        }

        private void btnBaseAuthRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstBasePermission.Items.Clear();
            lstBasePermission.Tag = null;
            foreach (BSN_BSL.Manager auth in BSN_BSL.loadPackManager(BSN_BSL.PACK.BasePack, (string)cbBUID.SelectedItem))
            {
                lstBasePermission.Tag += auth.member_srl + "|" + auth.permission + "\n";
                if (auth.permission != "4")
                {
                    ComboBoxItem cbi = (ComboBoxItem)cbBasePermission.Items.GetItemAt(Convert.ToInt32(auth.permission));
                    auth.permission = (string)cbi.Content;
                    lstBasePermission.Items.Add(auth);
                }
                else
                {
                    auth.permission = "제작자";
                    lstBasePermission.Items.Add(auth);
                }
            }
        }

        private void btnResSave_Click(object sender, RoutedEventArgs e)
        {
            if (BSN_BSM.modifyPackBasic(BSN_BSL.PACK.Resource, (string)cbRUID.SelectedItem, (string)cbResRecommended.SelectedItem, (bool)cbResActivate.IsChecked))
            {
                btnResLoad_Click(sender, e);
                WPFCom.Message("기본정보가 성공적으로 수정되었습니다.");
            }
            else
                WPFCom.Message("기본정보 수정에 실패하였습니다.");
        }

        private void btnResDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResSelSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResSelDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResAuthAdd_Click(object sender, RoutedEventArgs e)
        {
            string member_srl = BSN_Info.getMember_srl(txtResEmail.Text);
            if (member_srl == null)
            {
                WPFCom.Message("존재하지 않는 회원입니다.");
                return;
            }

            if (BSN_BSM.addPackManager(BSN_BSL.PACK.Resource, (string)cbRUID.SelectedItem, member_srl, cbResPermission.SelectedIndex.ToString()))
            {
                btnResAuthRefresh_Click(sender, e);
                WPFCom.Message("정상적으로 등록되었습니다.");
            }
            else
                WPFCom.Message("관리자 정보등록에 실패하였습니다.");
        }

        private void btnResAuthDel_Click(object sender, RoutedEventArgs e)
        {
            string[] data = lstResPermission.Tag.ToString().Split('\n')[lstResPermission.SelectedIndex].Split('|');
            string member_srl = data[0];
            string permission = data[1];

            if (permission == "4")
            {
                WPFCom.Message("제작자권한은 삭제할 수 없습니다.");
                return;
            }

            if (BSN_BSM.delPackManager(BSN_BSL.PACK.Resource, (string)cbRUID.SelectedItem, member_srl, permission))
            {
                btnResAuthRefresh_Click(sender, e);
                WPFCom.Message("정상적으로 삭제되었습니다.");
            }
            else
                WPFCom.Message("관리자 삭제에 실패하였습니다.");
        }

        private void btnResAuthRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstResPermission.Items.Clear();
            lstResPermission.Tag = null;
            foreach (BSN_BSL.Manager auth in BSN_BSL.loadPackManager(BSN_BSL.PACK.Resource, (string)cbRUID.SelectedItem))
            {
                lstResPermission.Tag += auth.member_srl + "|" + auth.permission + "\n";
                if (auth.permission != "4")
                {
                    ComboBoxItem cbi = (ComboBoxItem)cbResPermission.Items.GetItemAt(Convert.ToInt32(auth.permission));
                    auth.permission = (string)cbi.Content;
                    lstResPermission.Items.Add(auth);
                }
                else
                {
                    auth.permission = "제작자";
                    lstResPermission.Items.Add(auth);
                }
            }
        }

        private void btnResNews_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResDetail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
