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
            cbName.Items.Clear();
            lstServer.Items.Clear();

            // 데이터 기본값
            if (cbName.Tag == null)
                cbName.Tag = "modpack";

            // 데이터 로드
            foreach (BSN_BSL.Server server in BSN_BSL.LoadServerList(BSN_BSL.SERVER.cloud))
                lstServer.Items.Add(server);

            switch (cbName.Tag.ToString())
            {
                case "modpack":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.modpack, User.BSN_member_srl))
                        cbName.Items.Add(Common.getElement(tmp, "name"));
                    lbBaseVer.Visibility = Visibility.Visible;
                    cbBaseVer.Visibility = Visibility.Visible;
                    lbUploadURL.Content = User.BSN_Path + "Upload\\ModPack\\";
                    break;

                case "basepack":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.basepack, User.BSN_member_srl))
                        cbName.Items.Add(Common.getElement(tmp, "name"));
                    lbBaseVer.Visibility = Visibility.Collapsed;
                    cbBaseVer.Visibility = Visibility.Collapsed;
                    lbUploadURL.Content = User.BSN_Path + "Upload\\BasePack\\";
                    break;

                case "resource":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.resource, User.BSN_member_srl))
                        cbName.Items.Add(Common.getElement(tmp, "name"));
                    lbBaseVer.Visibility = Visibility.Collapsed;
                    cbBaseVer.Visibility = Visibility.Collapsed;
                    lbUploadURL.Content = User.BSN_Path + "Upload\\Resource\\";
                    break;
            }
            cbName.SelectedIndex = 0;
            btnRefresh_Click(null, null);
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

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
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
                    cbBaseVer.SelectedIndex = 0;
                    break;

                case BSN_BSL.PACK.basepack:

                    break;

                case BSN_BSL.PACK.resource:

                    break;
            }
            gbPack.IsEnabled = false;
            gbUpload.IsEnabled = true;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstFile.Items.Clear();
            try
            {
                foreach (string value in FileSystem.GetFileArray((string)lbUploadURL.Content, true))
                    lstFile.Items.Add(value);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                FileSystem.CreateFolder((string)lbUploadURL.Content);
                btnRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                WPFCom.Message("업로드 폴더 로드중 문제가 발생하였습니다." + Environment.NewLine + ex.Message);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // 검사
            if (txtVersion.Text == string.Empty)
            {
                WPFCom.Message("모든 필드에 값을 입력 해 주세요.");
                return;
            }

            // 업로드
            string[] File = lstFile.Items.Cast<string>().ToArray();
            string basevid = null;

            // 선택한 서버리스트 배열로 올리기
            List<string> list = new List<string>();
            foreach (BSN_BSL.Server sv in lstServer.Items)
                if (sv.select)
                    list.Add(sv.id);
            if (list.Count == 0)
            {
                WPFCom.Message("1개 이상의 서버에 클라이언트 파일을 업로드 해야합니다.");
                return;
            }

            if (GetSelectType() == BSN_BSL.PACK.modpack)
                basevid = cbBaseVer.Tag.ToString().Split('|')[cbBaseVer.SelectedIndex];

            if (BSN_BSM.UploadVersion(GetSelectType(), "id", "pw", (string)cbName.SelectedItem, txtVersion.Text, list.ToArray(), File, (string)lbUploadURL.Content, basevid))
                WPFCom.Message("성공적으로 업로드했습니다.");
            else
                WPFCom.Message("업로드에 실패하였습니다.");
        }
    }
}
