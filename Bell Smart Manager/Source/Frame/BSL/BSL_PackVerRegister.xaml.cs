using System.Windows;
using BellLib.Class.BSN;
using BellLib.Data;
using BellLib.Class;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Bell_Smart_Manager.Source.Frame.BSL
{
    /// <summary>
    /// BSL_PackVerRegister.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BSL_PackVerRegister : Window
    {
        public BSL_PackVerRegister()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            // 데이터 초기화
            lbParameter.Visibility = Visibility.Collapsed;
            txtParameter.Visibility = Visibility.Collapsed;
            lbBaseVer.Visibility = Visibility.Collapsed;
            cbBaseVer.Visibility = Visibility.Collapsed;
            btnLoad.IsEnabled = false;
            cbName.IsEnabled = false;
            cbName.Items.Clear();

            // 데이터 기본값
            if (cbName.Tag == null)
                cbName.Tag = "modpack";
            if (txtParameter.Text == string.Empty)
                txtParameter.Text = "--username ${auth_player_name} --version ${version_name} --gameDir ${game_directory} --assetsDir ${assets_root} --assetIndex ${assets_index_name} --uuid ${auth_uuid} --accessToken ${auth_access_token}";


            switch (cbName.Tag.ToString())
            {
                case "modpack":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.modpack, User.BSN_member_srl))
                        cbName.Items.Add(Common.getElement(tmp, "name"));
                    lbBaseVer.Visibility = Visibility.Visible;
                    cbBaseVer.Visibility = Visibility.Visible;
                    break;

                case "basepack":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.basepack, User.BSN_member_srl))
                        cbName.Items.Add(Common.getElement(tmp, "name"));
                    lbParameter.Visibility = Visibility.Visible;
                    txtParameter.Visibility = Visibility.Visible;
                    break;

                case "resource":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.resource, User.BSN_member_srl))
                        cbName.Items.Add(Common.getElement(tmp, "name"));
                    break;

                case "runtime":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.runtime, User.BSN_member_srl))
                        cbName.Items.Add(Common.getElement(tmp, "name"));
                    break;
            }
            if (cbName.Items.Count != 0)
            {
                cbName.IsEnabled = true;
                btnLoad.IsEnabled = true;
            }
            cbName.SelectedIndex = 0;
        }

        /// <summary>
        /// 현재 선택되어있는 팩을 반환합니다.
        /// </summary>
        /// <returns>선택된 팩 종류</returns>
        private BSN_BSL.PACK GetSelectType()
        {
            BSN_BSL.PACK pack = BSN_BSL.PACK.modpack;
            if ((bool)rbBasePack.IsChecked)
                pack = BSN_BSL.PACK.basepack;
            if ((bool)rbResource.IsChecked)
                pack = BSN_BSL.PACK.resource;
            if ((bool)rbRuntime.IsChecked)
                pack = BSN_BSL.PACK.runtime;

            return pack;
        }

        private void rbModPack_Checked(object sender, RoutedEventArgs e)
        {
            if (!rbModPack.IsInitialized)
                return;
            cbName.Tag = "modpack";
            Initialize();
        }

        private void rbBasePack_Checked(object sender, RoutedEventArgs e)
        {
            if (!rbBasePack.IsInitialized)
                return;
            cbName.Tag = "basepack";
            Initialize();
        }

        private void rbResource_Checked(object sender, RoutedEventArgs e)
        {
            if (!rbResource.IsInitialized)
                return;
            cbName.Tag = "resource";
            Initialize();
        }

        private void rbRuntime_Checked(object sender, RoutedEventArgs e)
        {
            if (!rbRuntime.IsInitialized)
                return;
            cbName.Tag = "runtime";
            Initialize();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (cbName.SelectedItem == null)
            {
                WPFCom.Message("수정할 팩을 선택해주세요.", Basic.PROJECT.Bell_Smart_Manager);
                return;
            }

            switch (GetSelectType())
            {
                case BSN_BSL.PACK.modpack:
                    cbBaseVer.Items.Clear();
                    cbBaseVer.Tag = null;
                    BSN_BSL.ModPack mp = BSN_BSL.LoadModPackDetail((string)cbName.SelectedItem);
                    foreach (string value in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.basepack, mp.BaseName))
                    {
                        cbBaseVer.Items.Add(Common.getElement(value, "version"));
                        cbBaseVer.Tag += Common.getElement(value, "id") + "|";
                    }
                    cbBaseVer.SelectedIndex = cbBaseVer.Items.Count - 1;
                    break;

                case BSN_BSL.PACK.basepack:

                    break;

                case BSN_BSL.PACK.resource:

                    break;

                case BSN_BSL.PACK.runtime:

                    break;
            }
            gbPack.IsEnabled = false;
            gbUpload.IsEnabled = true;
        }

        

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // 검사
            if (txtVersion.Text == string.Empty)
            {
                WPFCom.Message("모든 필드에 값을 입력 해 주세요.", Basic.PROJECT.Bell_Smart_Manager);
                return;
            }
            
            // 모드팩을 선택했을경우,
            string option = null;
            if (GetSelectType() == BSN_BSL.PACK.modpack)
                option = cbBaseVer.Tag.ToString().Split('|')[cbBaseVer.SelectedIndex];
            else if (GetSelectType() == BSN_BSL.PACK.basepack)
                option = txtParameter.Text;

            // 버전정보 등록
            if (BSN_BSM.RegisterVersion(GetSelectType(), "id", "pw", (string)cbName.SelectedItem, txtVersion.Text, option))
                WPFCom.Message("버전정보를 성공적으로 등록했습니다.", Basic.PROJECT.Bell_Smart_Manager);
            else
                WPFCom.Message("버전 추가에 실패하였습니다.", Basic.PROJECT.Bell_Smart_Manager);
        }
    }
}
