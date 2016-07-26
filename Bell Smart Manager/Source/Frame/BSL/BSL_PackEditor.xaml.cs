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
            cbModName.Items.Clear();
            cbBaseName.Items.Clear();
            cbResName.Items.Clear();

            // 모드팩 데이터 로드
            foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.modpack, User.BSN_member_srl))
                cbModName.Items.Add(Common.getElement(tmp, "name"));
            // 베이스팩 데이터 로드
            foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.basepack, User.BSN_member_srl))
                cbBaseName.Items.Add(Common.getElement(tmp, "name"));
            // 리소스 데이터 로드
            foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.resource, User.BSN_member_srl))
                cbResName.Items.Add(Common.getElement(tmp, "name"));
            // 런타임 데이터 로드
            foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.runtime, User.BSN_member_srl))
                cbRunName.Items.Add(Common.getElement(tmp, "name"));

            // 기본값 선택
            cbModName.SelectedIndex = cbModName.Items.Count - 1; // 마지막에 생성한 모드팩 선택 (추후 마지막에 '선택'한 모드팩을 선택하도록 수정)
            cbBaseName.SelectedIndex = cbBaseName.Items.Count - 1;
            cbResName.SelectedIndex = cbResName.Items.Count - 1;
            cbRunName.SelectedIndex = cbRunName.Items.Count - 1;

            // 검사
            if (cbModName.Items.IsEmpty && cbBaseName.Items.IsEmpty && cbResName.Items.IsEmpty && cbRunName.Items.IsEmpty)
            {
                Hide();
                WPFCom.Message("수정 가능한 데이터가 없습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            if (cbModName.Items.IsEmpty)
                tiModPack.Visibility = Visibility.Collapsed;
            if (cbBaseName.Items.IsEmpty)
                tiBasePack.Visibility = Visibility.Collapsed;
            if (cbResName.Items.IsEmpty)
                tiResource.Visibility = Visibility.Collapsed;
            if (cbRunName.Items.IsEmpty)
                tiRuntime.Visibility = Visibility.Collapsed;

            foreach (TabItem ti in tcGlobal.Items)
            {
                if (ti.Visibility == Visibility.Visible)
                {
                    ti.IsSelected = true;
                    return;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (cbModName.Items.IsEmpty && cbBaseName.Items.IsEmpty && cbResName.Items.IsEmpty)
                this.Close();
        }

        private void btnModLoad_Click(object sender, RoutedEventArgs e)
        {
            // 검사

            // 필드
            string name = (string)cbModName.SelectedItem;
            BSN_BSL.ModPack mp = new BSN_BSL.ModPack();

            // 초기화
            tcMod.IsEnabled = false;
            cbModActivate.IsChecked = false;
            lstModVersion.Items.Clear();
            lstModVersion.Tag = null;
            cbModRecommended.Items.Clear();

            // 기본정보 로드
            mp = BSN_BSL.LoadModPackDetail(name);
            lbModName.Content = mp.name;
            lbModLatest.Content = mp.latest;
            foreach (string tmp in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.modpack, name))
                cbModRecommended.Items.Add(Common.getElement(tmp, "version"));
            cbModRecommended.SelectedItem = mp.recommended; // 권장버전 선택
            lbModBaseName.Content = mp.BaseName;
            lbModState.Content = mp.state;

            string planName = mp.plan;
            try
            {
                planName = BSN_BSL.GetPlanName((BSN_BSL.PLAN)Convert.ToInt32(mp.plan));
            }
            catch { }
            lbModPlan.Content = planName;
            if (mp.state == "활성화")
                cbModActivate.IsChecked = true;

            // 버전정보 로드
            foreach (string data in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.modpack, name, true))
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
            string name = (string)cbBaseName.SelectedItem;
            BSN_BSL.BasePack bp = new BSN_BSL.BasePack();

            // 초기화
            tcBase.IsEnabled = false;
            cbBaseActivate.IsChecked = false;
            lstBaseVersion.Items.Clear();
            lstBaseVersion.Tag = null;
            cbBaseRecommended.Items.Clear();

            // 기본정보 로드
            bp = BSN_BSL.LoadBasePackDetail(name);
            lbBaseName.Content = bp.name;
            lbBaseLatest.Content = bp.latest;
            foreach (string tmp in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.basepack, name))
                cbBaseRecommended.Items.Add(Common.getElement(tmp, "version"));
            cbBaseRecommended.SelectedItem = bp.recommended; // 권장버전 선택
            lbBaseState.Content = bp.state;
            lbBasePlan.Content = bp.plan;
            lbBaseMCVer.Content = bp.mcversion;
            if (bp.state == "활성화")
                cbBaseActivate.IsChecked = true;

            // 버전정보 로드
            foreach (string data in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.basepack, name, true))
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
            string name = (string)cbResName.SelectedItem;
            BSN_BSL.Resource res = new BSN_BSL.Resource();

            // 초기화
            tcResource.IsEnabled = false;
            cbResActivate.IsChecked = false;
            lstResVersion.Items.Clear();
            lstResVersion.Tag = null;
            cbResRecommended.Items.Clear();

            // 기본정보 로드
            res = BSN_BSL.LoadResPackDetail(name);
            lbResName.Content = res.name;
            lbResLatest.Content = res.latest;
            foreach (string tmp in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.resource, name))
                cbResRecommended.Items.Add(Common.getElement(tmp, "version"));
            cbResRecommended.SelectedItem = res.recommended; // 권장버전 선택
            lbResState.Content = res.state;
            lbResPlan.Content = res.plan;
            lbResMCVer.Content = res.mcversion;
            lbResType.Content = res.type;
            if (res.state == "활성화")
                cbResActivate.IsChecked = true;

            // 버전정보 로드
            foreach (string data in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.resource, name, true))
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

        private void btnRunLoad_Click(object sender, RoutedEventArgs e)
        {
            // 필드
            string name = (string)cbRunName.SelectedItem;
            BSN_BSL.Runtime run = new BSN_BSL.Runtime();

            // 초기화
            tcRuntime.IsEnabled = false;
            cbRunActivate.IsChecked = false;
            lstRunVersion.Items.Clear();
            lstRunVersion.Tag = null;
            cbRunRecommended.Items.Clear();

            // 기본정보 로드
            run = BSN_BSL.LoadRuntimeDetail(name);
            lbRunName.Content = run.name;
            lbRunLatest.Content = run.latest;
            foreach (string tmp in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.runtime, name))
                cbRunRecommended.Items.Add(Common.getElement(tmp, "version"));
            cbRunRecommended.SelectedItem = run.recommended; // 권장버전 선택
            lbRunState.Content = run.state;
            if (run.state == "활성화")
                cbRunActivate.IsChecked = true;

            // 버전정보 로드
            foreach (string data in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.runtime, name, true))
            {
                lstRunVersion.Items.Add(Common.getElement(data, "version"));
                lstRunVersion.Tag += Common.getElement(data, "id") + "|";
            }
            lstRunVersion.SelectedIndex = 0;

            // 관리자 정보 로드
            btnRunAuthRefresh_Click(sender, e);

            if (run.numState == BSN_BSL.STATE.BANNED || run.numState == BSN_BSL.STATE.PENDING)
                return;
            //gbBase.IsEnabled = false;
            tcRuntime.IsEnabled = true;
        }

        private void btnModSave_Click(object sender, RoutedEventArgs e)
        {
            if (BSN_BSM.ModifyPackBasic(BSN_BSL.PACK.modpack, (string)cbModName.SelectedItem, (string)cbModRecommended.SelectedItem, (bool)cbModActivate.IsChecked))
            {
                btnModLoad_Click(sender, e);
                WPFCom.Message("기본정보가 성공적으로 수정되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("기본정보 수정에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void btnModDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModSelSave_Click(object sender, RoutedEventArgs e)
        {
            if (BSN_BSM.ModifyPackVersion(BSN_BSL.PACK.modpack, lstModVersion.Tag.ToString().Split('|')[lstModVersion.SelectedIndex], (bool)cbModSelActivate.IsChecked, cbBaseVer.Tag.ToString().Split('|')[cbBaseVer.SelectedIndex]))
            {
                WPFCom.Message("버전정보가 성공적으로 수정되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("버전정보 수정에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void btnModAuthAdd_Click(object sender, RoutedEventArgs e)
        {
            string member_srl = BSN_Info.GetMember_srl(txtModEmail.Text);
            if (member_srl == null)
            {
                WPFCom.Message("존재하지 않는 회원입니다.", Base.PROJECT.Bell_Smart_Manager);
                return;
            }

            if (BSN_BSM.AddPackManager(BSN_BSL.PACK.modpack, (string)cbModName.SelectedItem, member_srl, cbModPermission.SelectedIndex.ToString()))
            {
                btnModAuthRefresh_Click(sender, e);
                WPFCom.Message("정상적으로 등록되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("관리자 정보등록에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
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
                WPFCom.Message("제작자권한은 삭제할 수 없습니다.", Base.PROJECT.Bell_Smart_Manager);
                return;
            }

            if (BSN_BSM.DelPackManager(BSN_BSL.PACK.modpack, (string)cbModName.SelectedItem, member_srl, permission))
            {
                btnModAuthRefresh_Click(sender, e);
                WPFCom.Message("정상적으로 삭제되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("관리자 삭제에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void btnModAuthRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstModPermission.Items.Clear();
            lstModPermission.Tag = null;
            foreach (BSN_BSL.Manager auth in BSN_BSL.LoadPackManager(BSN_BSL.PACK.modpack, (string)cbModName.SelectedItem))
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
            if (BSN_BSM.ModifyPackBasic(BSN_BSL.PACK.basepack, (string)cbBaseName.SelectedItem, (string)cbBaseRecommended.SelectedItem, (bool)cbBaseActivate.IsChecked))
            {
                btnBaseLoad_Click(sender, e);
                WPFCom.Message("기본정보가 성공적으로 수정되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("기본정보 수정에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void btnBaseDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBaseSelSave_Click(object sender, RoutedEventArgs e)
        {
            if (BSN_BSM.ModifyPackVersion(BSN_BSL.PACK.basepack, lstBaseVersion.Tag.ToString().Split('|')[lstBaseVersion.SelectedIndex], (bool)cbBaseSelActivate.IsChecked))
            {
                WPFCom.Message("버전정보가 성공적으로 수정되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("버전정보 수정에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void btnBaseSelDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBaseAuthAdd_Click(object sender, RoutedEventArgs e)
        {
            string member_srl = BSN_Info.GetMember_srl(txtBaseEmail.Text);
            if (member_srl == null)
            {
                WPFCom.Message("존재하지 않는 회원입니다.", Base.PROJECT.Bell_Smart_Manager);
                return;
            }

            if (BSN_BSM.AddPackManager(BSN_BSL.PACK.basepack, (string)cbBaseName.SelectedItem, member_srl, cbBasePermission.SelectedIndex.ToString()))
            {
                btnBaseAuthRefresh_Click(sender, e);
                WPFCom.Message("정상적으로 등록되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("관리자 정보등록에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void btnBaseAuthDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] data = lstBasePermission.Tag.ToString().Split('\n')[lstBasePermission.SelectedIndex].Split('|');
                string member_srl = data[0];
                string permission = data[1];

                if (permission == "4")
                {
                    WPFCom.Message("제작자권한은 삭제할 수 없습니다.", Base.PROJECT.Bell_Smart_Manager);
                    return;
                }

                if (BSN_BSM.DelPackManager(BSN_BSL.PACK.basepack, (string)cbBaseName.SelectedItem, member_srl, permission))
                {
                    btnBaseAuthRefresh_Click(sender, e);
                    WPFCom.Message("정상적으로 삭제되었습니다.", Base.PROJECT.Bell_Smart_Manager);
                }
                else
                    WPFCom.Message("관리자 삭제에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            catch
            {
                WPFCom.Message("권한을 삭제할 회원을 선택 해 주세요.", Base.PROJECT.Bell_Smart_Manager);
            }
        }

        private void btnBaseAuthRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstBasePermission.Items.Clear();
            lstBasePermission.Tag = null;
            foreach (BSN_BSL.Manager auth in BSN_BSL.LoadPackManager(BSN_BSL.PACK.basepack, (string)cbBaseName.SelectedItem))
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
            if (BSN_BSM.ModifyPackBasic(BSN_BSL.PACK.resource, (string)cbResName.SelectedItem, (string)cbResRecommended.SelectedItem, (bool)cbResActivate.IsChecked))
            {
                btnResLoad_Click(sender, e);
                WPFCom.Message("기본정보가 성공적으로 수정되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("기본정보 수정에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void btnResDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResSelSave_Click(object sender, RoutedEventArgs e)
        {
            if (BSN_BSM.ModifyPackVersion(BSN_BSL.PACK.resource, lstResVersion.Tag.ToString().Split('|')[lstResVersion.SelectedIndex], (bool)cbResSelActivate.IsChecked))
            {
                WPFCom.Message("버전정보가 성공적으로 수정되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("버전정보 수정에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void btnResSelDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResAuthAdd_Click(object sender, RoutedEventArgs e)
        {
            string member_srl = BSN_Info.GetMember_srl(txtResEmail.Text);
            if (member_srl == null)
            {
                WPFCom.Message("존재하지 않는 회원입니다.", Base.PROJECT.Bell_Smart_Manager);
                return;
            }

            if (BSN_BSM.AddPackManager(BSN_BSL.PACK.resource, (string)cbResName.SelectedItem, member_srl, cbResPermission.SelectedIndex.ToString()))
            {
                btnResAuthRefresh_Click(sender, e);
                WPFCom.Message("정상적으로 등록되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("관리자 정보등록에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void btnResAuthDel_Click(object sender, RoutedEventArgs e)
        {
            string[] data = lstResPermission.Tag.ToString().Split('\n')[lstResPermission.SelectedIndex].Split('|');
            string member_srl = data[0];
            string permission = data[1];

            if (permission == "4")
            {
                WPFCom.Message("제작자권한은 삭제할 수 없습니다.", Base.PROJECT.Bell_Smart_Manager);
                return;
            }

            if (BSN_BSM.DelPackManager(BSN_BSL.PACK.resource, (string)cbResName.SelectedItem, member_srl, permission))
            {
                btnResAuthRefresh_Click(sender, e);
                WPFCom.Message("정상적으로 삭제되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("관리자 삭제에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void btnResAuthRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstResPermission.Items.Clear();
            lstResPermission.Tag = null;
            foreach (BSN_BSL.Manager auth in BSN_BSL.LoadPackManager(BSN_BSL.PACK.resource, (string)cbResName.SelectedItem))
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
                foreach (string value in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.basepack, (string)lbModBaseName.Content))
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

        private void lstRunVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string state;
                LoadVersion(BSN_BSL.PACK.runtime, lstRunVersion, cbRunSelActivate, out state, out cbRunSelActivate);
                lbRunVerState.Content = state;
            }
            catch { }
        }

        private void btnRunDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRunSave_Click(object sender, RoutedEventArgs e)
        {
            if (BSN_BSM.ModifyPackBasic(BSN_BSL.PACK.runtime, (string)cbRunName.SelectedItem, (string)cbRunRecommended.SelectedItem, (bool)cbRunActivate.IsChecked))
            {
                btnRunLoad_Click(sender, e);
                WPFCom.Message("기본정보가 성공적으로 수정되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("기본정보 수정에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void btnRunSelDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRunSelSave_Click(object sender, RoutedEventArgs e)
        {
            if (BSN_BSM.ModifyPackVersion(BSN_BSL.PACK.runtime, lstRunVersion.Tag.ToString().Split('|')[lstRunVersion.SelectedIndex], (bool)cbRunSelActivate.IsChecked))
            {
                WPFCom.Message("버전정보가 성공적으로 수정되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("버전정보 수정에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void btnRunAuthRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstRunPermission.Items.Clear();
            lstRunPermission.Tag = null;
            foreach (BSN_BSL.Manager auth in BSN_BSL.LoadPackManager(BSN_BSL.PACK.runtime, (string)cbRunName.SelectedItem))
            {
                lstRunPermission.Tag += auth.member_srl + "|" + auth.permission + "\n";
                if (auth.permission != "4")
                {
                    ComboBoxItem cbi = (ComboBoxItem)cbRunPermission.Items.GetItemAt(Convert.ToInt32(auth.permission));
                    auth.permission = (string)cbi.Content;
                    lstRunPermission.Items.Add(auth);
                }
                else
                {
                    auth.permission = "제작자";
                    lstRunPermission.Items.Add(auth);
                }
            }
        }

        private void btnRunAuthAdd_Click(object sender, RoutedEventArgs e)
        {
            string member_srl = BSN_Info.GetMember_srl(txtRunEmail.Text);
            if (member_srl == null)
            {
                WPFCom.Message("존재하지 않는 회원입니다.", Base.PROJECT.Bell_Smart_Manager);
                return;
            }

            if (BSN_BSM.AddPackManager(BSN_BSL.PACK.runtime, (string)cbRunName.SelectedItem, member_srl, cbRunPermission.SelectedIndex.ToString()))
            {
                btnRunAuthRefresh_Click(sender, e);
                WPFCom.Message("정상적으로 등록되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("관리자 정보등록에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void btnRunAuthDel_Click(object sender, RoutedEventArgs e)
        {
            string[] data = lstRunPermission.Tag.ToString().Split('\n')[lstRunPermission.SelectedIndex].Split('|');
            string member_srl = data[0];
            string permission = data[1];

            if (permission == "4")
            {
                WPFCom.Message("제작자권한은 삭제할 수 없습니다.", Base.PROJECT.Bell_Smart_Manager);
                return;
            }

            if (BSN_BSM.DelPackManager(BSN_BSL.PACK.runtime, (string)cbRunName.SelectedItem, member_srl, permission))
            {
                btnRunAuthRefresh_Click(sender, e);
                WPFCom.Message("정상적으로 삭제되었습니다.", Base.PROJECT.Bell_Smart_Manager);
            }
            else
                WPFCom.Message("관리자 삭제에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
        }
    }
}
