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
            cbModUID.Items.Clear();
            cbBaseUID.Items.Clear();
            cbResUID.Items.Clear();

            // 모드팩 데이터 로드
            foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.modpack, User.BSN_member_srl))
                cbModUID.Items.Add(Common.getElement(tmp, "UID"));
            // 베이스팩 데이터 로드
            foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.basepack, User.BSN_member_srl))
                cbBaseUID.Items.Add(Common.getElement(tmp, "UID"));
            // 리소스 데이터 로드
            foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.resource, User.BSN_member_srl))
                cbResUID.Items.Add(Common.getElement(tmp, "UID"));

            // 기본값 선택
            cbModUID.SelectedIndex = 0;
            cbBaseUID.SelectedIndex = 0;
            cbResUID.SelectedIndex = 0;

            // 검사
            if (cbModUID.Items.IsEmpty && cbBaseUID.Items.IsEmpty && cbResUID.Items.IsEmpty)
            {
                Hide();
                WPFCom.Message("수정 가능한 데이터가 없습니다.");
            }
            if (cbModUID.Items.IsEmpty)
            {
                tiModPack.Visibility = Visibility.Collapsed;
                tiBasePack.IsSelected = true;
            }
            if (cbBaseUID.Items.IsEmpty)
            {
                tiBasePack.Visibility = Visibility.Collapsed;
                tiResource.IsSelected = true;
            }
            if (cbResUID.Items.IsEmpty)
            {
                tiResource.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (cbModUID.Items.IsEmpty && cbBaseUID.Items.IsEmpty && cbResUID.Items.IsEmpty)
                this.Close();
        }

        private void btnModLoad_Click(object sender, RoutedEventArgs e)
        {
            // 검사

            // 필드
            string UID = (string)cbModUID.SelectedItem;
            BSN_BSL.ModPack mp = new BSN_BSL.ModPack();

            // 초기화
            tcMod.IsEnabled = false;
            cbModActivate.IsChecked = false;
            lstModVersion.Items.Clear();
            lstModVersion.Tag = null;
            cbModRecommended.Items.Clear();

            // 기본정보 로드
            mp = BSN_BSL.LoadModPackDetail(UID);
            lbModName.Content = mp.name;
            lbModLatest.Content = mp.latest;
            foreach (string tmp in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.modpack, UID))
                cbModRecommended.Items.Add(Common.getElement(tmp, "version"));
            cbModRecommended.SelectedItem = mp.recommended; // 권장버전 선택
            lbModBUID.Content = mp.BUID;
            lbModState.Content = mp.state;
            lbModPlan.Content = mp.plan;
            if (mp.state == "활성화")
                cbModActivate.IsChecked = true;

            // 버전정보 로드
            foreach (string data in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.modpack, UID, true))
            {
                lstModVersion.Items.Add(Common.getElement(data, "version"));
                lstModVersion.Tag += Common.getElement(data, "id") + "|";
            }
            lstModVersion.SelectedIndex = 0;

            // 관리자 정보 로드
            btnModAuthRefresh_Click(sender, e);

            if (mp.numState == BSN_BSL.STATE.BANNED || mp.numState == BSN_BSL.STATE.PENDING)
                return;
            //gbMod.IsEnabled = false;
            tcMod.IsEnabled = true;
        }

        private void btnBaseLoad_Click(object sender, RoutedEventArgs e)
        {
            // 필드
            string UID = (string)cbBaseUID.SelectedItem;
            BSN_BSL.BasePack bp = new BSN_BSL.BasePack();

            // 초기화
            tcBase.IsEnabled = false;
            cbBaseActivate.IsChecked = false;
            lstBaseVersion.Items.Clear();
            lstBaseVersion.Tag = null;
            cbBaseRecommended.Items.Clear();

            // 기본정보 로드
            bp = BSN_BSL.LoadBasePackDetail(UID);
            lbBaseName.Content = bp.name;
            lbBaseLatest.Content = bp.latest;
            foreach (string tmp in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.basepack, UID))
                cbBaseRecommended.Items.Add(Common.getElement(tmp, "version"));
            cbBaseRecommended.SelectedItem = bp.recommended; // 권장버전 선택
            lbBaseState.Content = bp.state;
            lbBasePlan.Content = bp.plan;
            lbBaseMCVer.Content = bp.mcversion;
            if (bp.state == "활성화")
                cbBaseActivate.IsChecked = true;

            // 버전정보 로드
            foreach (string data in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.basepack, UID, true))
            {
                lstBaseVersion.Items.Add(Common.getElement(data, "version"));
                lstBaseVersion.Tag += Common.getElement(data, "id") + "|";
            }
            lstBaseVersion.SelectedIndex = 0;

            // 관리자 정보 로드
            btnBaseAuthRefresh_Click(sender, e);

            if (bp.numState == BSN_BSL.STATE.BANNED || bp.numState == BSN_BSL.STATE.PENDING)
                return;
            //gbBase.IsEnabled = false;
            tcBase.IsEnabled = true;
        }

        private void btnResLoad_Click(object sender, RoutedEventArgs e)
        {
            // 필드
            string UID = (string)cbResUID.SelectedItem;
            BSN_BSL.Resource res = new BSN_BSL.Resource();

            // 초기화
            tcResource.IsEnabled = false;
            cbResActivate.IsChecked = false;
            lstResVersion.Items.Clear();
            lstResVersion.Tag = null;
            cbResRecommended.Items.Clear();

            // 기본정보 로드
            res = BSN_BSL.LoadResPackDetail(UID);
            lbResName.Content = res.name;
            lbResLatest.Content = res.latest;
            foreach (string tmp in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.resource, UID))
                cbResRecommended.Items.Add(Common.getElement(tmp, "version"));
            cbResRecommended.SelectedItem = res.recommended; // 권장버전 선택
            lbResState.Content = res.state;
            lbResPlan.Content = res.plan;
            lbResMCVer.Content = res.mcversion;
            lbResType.Content = res.type;
            if (res.state == "활성화")
                cbResActivate.IsChecked = true;

            // 버전정보 로드
            foreach (string data in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.resource, UID, true))
            {
                lstResVersion.Items.Add(Common.getElement(data, "version"));
                lstResVersion.Tag += Common.getElement(data, "id") + "|";
            }
            lstResVersion.SelectedIndex = 0;

            // 관리자 정보 로드
            btnResAuthRefresh_Click(sender, e);

            if (res.numState == BSN_BSL.STATE.BANNED || res.numState == BSN_BSL.STATE.PENDING)
                return;
            //gbResource.IsEnabled = false;
            tcResource.IsEnabled = true;
        }

        private void btnModSave_Click(object sender, RoutedEventArgs e)
        {
            if (BSN_BSM.ModifyPackBasic(BSN_BSL.PACK.modpack, (string)cbModUID.SelectedItem, (string)cbModRecommended.SelectedItem, (bool)cbModActivate.IsChecked))
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
            if (BSN_BSM.ModifyPackVersion(BSN_BSL.PACK.modpack, lstModVersion.Tag.ToString().Split('|')[lstModVersion.SelectedIndex], (bool)cbModSelActivate.IsChecked, cbBaseVer.Tag.ToString().Split('|')[cbBaseVer.SelectedIndex]))
            {
                WPFCom.Message("버전정보가 성공적으로 수정되었습니다.");
            }
            else
                WPFCom.Message("버전정보 수정에 실패하였습니다.");
        }

        private void btnModAuthAdd_Click(object sender, RoutedEventArgs e)
        {
            string member_srl = BSN_Info.GetMember_srl(txtModEmail.Text);
            if (member_srl == null)
            {
                WPFCom.Message("존재하지 않는 회원입니다.");
                return;
            }

            if (BSN_BSM.AddPackManager(BSN_BSL.PACK.modpack, (string)cbModUID.SelectedItem, member_srl, cbModPermission.SelectedIndex.ToString()))
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

            if (BSN_BSM.DelPackManager(BSN_BSL.PACK.modpack, (string)cbModUID.SelectedItem, member_srl, permission))
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
            foreach (BSN_BSL.Manager auth in BSN_BSL.LoadPackManager(BSN_BSL.PACK.modpack, (string)cbModUID.SelectedItem))
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
            if (BSN_BSM.ModifyPackBasic(BSN_BSL.PACK.basepack, (string)cbBaseUID.SelectedItem, (string)cbBaseRecommended.SelectedItem, (bool)cbBaseActivate.IsChecked))
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
            if (BSN_BSM.ModifyPackVersion(BSN_BSL.PACK.basepack, lstBaseVersion.Tag.ToString().Split('|')[lstBaseVersion.SelectedIndex], (bool)cbBaseSelActivate.IsChecked))
            {
                WPFCom.Message("버전정보가 성공적으로 수정되었습니다.");
            }
            else
                WPFCom.Message("버전정보 수정에 실패하였습니다.");
        }

        private void btnBaseSelDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBaseAuthAdd_Click(object sender, RoutedEventArgs e)
        {
            string member_srl = BSN_Info.GetMember_srl(txtBaseEmail.Text);
            if (member_srl == null)
            {
                WPFCom.Message("존재하지 않는 회원입니다.");
                return;
            }

            if (BSN_BSM.AddPackManager(BSN_BSL.PACK.basepack, (string)cbBaseUID.SelectedItem, member_srl, cbBasePermission.SelectedIndex.ToString()))
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

            if (BSN_BSM.DelPackManager(BSN_BSL.PACK.basepack, (string)cbBaseUID.SelectedItem, member_srl, permission))
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
            foreach (BSN_BSL.Manager auth in BSN_BSL.LoadPackManager(BSN_BSL.PACK.basepack, (string)cbBaseUID.SelectedItem))
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
            if (BSN_BSM.ModifyPackBasic(BSN_BSL.PACK.resource, (string)cbResUID.SelectedItem, (string)cbResRecommended.SelectedItem, (bool)cbResActivate.IsChecked))
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
            if (BSN_BSM.ModifyPackVersion(BSN_BSL.PACK.resource, lstResVersion.Tag.ToString().Split('|')[lstResVersion.SelectedIndex], (bool)cbResSelActivate.IsChecked))
            {
                WPFCom.Message("버전정보가 성공적으로 수정되었습니다.");
            }
            else
                WPFCom.Message("버전정보 수정에 실패하였습니다.");
        }

        private void btnResSelDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResAuthAdd_Click(object sender, RoutedEventArgs e)
        {
            string member_srl = BSN_Info.GetMember_srl(txtResEmail.Text);
            if (member_srl == null)
            {
                WPFCom.Message("존재하지 않는 회원입니다.");
                return;
            }

            if (BSN_BSM.AddPackManager(BSN_BSL.PACK.resource, (string)cbResUID.SelectedItem, member_srl, cbResPermission.SelectedIndex.ToString()))
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

            if (BSN_BSM.DelPackManager(BSN_BSL.PACK.resource, (string)cbResUID.SelectedItem, member_srl, permission))
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
            foreach (BSN_BSL.Manager auth in BSN_BSL.LoadPackManager(BSN_BSL.PACK.resource, (string)cbResUID.SelectedItem))
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

        private void LoadVersion(BSN_BSL.PACK type, ListBox Version, CheckBox SelectActivate, out string outState, out CheckBox outSelectActivate)
        {
            string verid;
            try
            {
                verid = Version.Tag.ToString().Split('|')[Version.SelectedIndex];
            }
            catch
            {
                throw;
            }
            string data = BSN_BSL.LoadVersionDetail(type, verid);
            BSN_BSL.STATE state = (BSN_BSL.STATE)Convert.ToInt32(Common.getElement(data, "state"));

            outState = BSN_BSL.GetStateName(state);

            outSelectActivate = SelectActivate;
            outSelectActivate.IsEnabled = false;
            outSelectActivate.IsChecked = false;
            if (state == BSN_BSL.STATE.HIDDEN)
            {
                outSelectActivate.IsEnabled = true;
                outSelectActivate.IsChecked = false;
            }
            else if (state == BSN_BSL.STATE.ACTIVATE)
            {
                outSelectActivate.IsEnabled = true;
                outSelectActivate.IsChecked = true;
            }
            else if (state == BSN_BSL.STATE.BANNED)
            {
                outSelectActivate.IsEnabled = false;
                outSelectActivate.IsChecked = false;
            }

            if (type == BSN_BSL.PACK.modpack)
            {
                cbBaseVer.IsEnabled = false;
                if (state == BSN_BSL.STATE.ACTIVATE || state == BSN_BSL.STATE.HIDDEN)
                    cbBaseVer.IsEnabled = true;
                cbBaseVer.Tag = null;
                cbBaseVer.Items.Clear();
                string basevid = Common.getElement(BSN_BSL.LoadVersionDetail(BSN_BSL.PACK.modpack, verid), "basevid");
                foreach (string value in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.basepack, (string)lbModBUID.Content))
                {
                    string version = Common.getElement(value, "version");
                    string id = Common.getElement(value, "id");

                    cbBaseVer.Items.Add(version);
                    cbBaseVer.Tag += id + "|";
                    if (id == basevid)
                        cbBaseVer.SelectedItem = version;
                }
            }
        }

        private void lstModVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string state;
                LoadVersion(BSN_BSL.PACK.modpack, lstModVersion, cbModSelActivate, out state, out cbModSelActivate);
                lbModVerState.Content = state;
            }
            catch { }
        }

        private void lstBaseVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string state;
                LoadVersion(BSN_BSL.PACK.basepack, lstBaseVersion, cbBaseSelActivate, out state, out cbBaseSelActivate);
                lbBaseVerState.Content = state;
            }
            catch { }
        }

        private void lstResVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string state;
                LoadVersion(BSN_BSL.PACK.resource, lstResVersion, cbResSelActivate, out state, out cbResSelActivate);
                lbResVerState.Content = state;
            }
            catch { }
        }
    }
}
