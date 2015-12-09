using BellLib.Class;
using BellLib.Class.BSN;
using BellLib.Data;
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

namespace Bell_Smart_Manager.Source.Frame.BSL
{
    /// <summary>
    /// BSL_PackFileRegister.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BSL_PackFileRegister : Window
    {
        public BSL_PackFileRegister()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            // 데이터 초기화
            btnLoad.IsEnabled = false;
            cbName.IsEnabled = false;
            cbName.Items.Clear();

            // 데이터 기본값
            if (cbName.Tag == null)
                cbName.Tag = "modpack";

            switch (cbName.Tag.ToString())
            {
                case "modpack":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.modpack, User.BSN_member_srl))
                        cbName.Items.Add(Common.getElement(tmp, "name"));
                    break;

                case "basepack":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.basepack, User.BSN_member_srl))
                        cbName.Items.Add(Common.getElement(tmp, "name"));
                    break;

                case "resource":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.resource, User.BSN_member_srl))
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
            // 검사
            if (cbName.SelectedItem == null)
            {
                WPFCom.Message("수정할 팩을 선택해주세요.");
                return;
            }

            // 초기화
            cbVersion.Items.Clear();
            cbVersion.Tag = null;

            // 선택한 팩 버전리스트 로드 (등록되지 않은 버전만 로드하게 구현필요)
            foreach (string value in BSN_BSL.LoadPackVersionList(GetSelectType(), (string)cbName.SelectedItem, true))
            {
                cbVersion.Items.Add(Common.getElement(value,"version"));
                cbVersion.Tag += Common.getElement(value, "id") + "|";
            }

            // 마무으리
            cbVersion.SelectedIndex = 0;
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

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
