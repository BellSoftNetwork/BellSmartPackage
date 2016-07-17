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
    /// BSL_PackUploader.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BSL_PackUploader : Window
    {
        public BSL_PackUploader()
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
            /*foreach (BSN_BSL.Server server in BSN_BSL.LoadServerList(BSN_BSL.SERVER.cloud))
                lstServer.Items.Add(server);*/

            switch (cbName.Tag.ToString())
            {
                case "modpack":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.modpack, User.BSN_member_srl))
                        cbName.Items.Add(Common.getElement(tmp, "name"));
                    lbUploadURL.Content = User.BSN_Path + "Upload\\ModPack\\";
                    break;

                case "basepack":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.basepack, User.BSN_member_srl))
                        cbName.Items.Add(Common.getElement(tmp, "name"));
                    lbUploadURL.Content = User.BSN_Path + "Upload\\BasePack\\";
                    break;

                case "resource":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.resource, User.BSN_member_srl))
                        cbName.Items.Add(Common.getElement(tmp, "name"));
                    lbUploadURL.Content = User.BSN_Path + "Upload\\Resource\\";
                    break;

                case "runtime":
                    foreach (string tmp in BSN_BSM.LoadPackList(BSN_BSL.PACK.runtime, User.BSN_member_srl))
                        cbName.Items.Add(Common.getElement(tmp, "name"));
                    lbUploadURL.Content = User.BSN_Path + "Upload\\Runtime\\";
                    break;
            }
            cbName.SelectedIndex = 0;
            LoadUploadPath(null, null);
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

        /// <summary>
        /// 현재 선택되어있는 범주를 반환합니다.
        /// </summary>
        /// <returns>선택된 파일 종류</returns>
        private BSN_BSL.KIND GetSelectKind()
        {
            BSN_BSL.KIND kind = BSN_BSL.KIND.client;
            if ((bool)rbClient.IsChecked)
                kind = BSN_BSL.KIND.client;
            if ((bool)rbServer.IsChecked)
                kind = BSN_BSL.KIND.server;

            return kind;
        }

        /// <summary>
        /// 현재 선택되어있는 버전 id를 반환합니다.
        /// </summary>
        /// <returns>선택된 버전의 id</returns>
        private string GetSelectVerid()
        {
            try
            {
                return cbVersion.Tag.ToString().Split('|')[cbVersion.SelectedIndex];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 업로드 폴더 경로를 로드합니다.
        /// </summary>
        private void LoadUploadPath(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!lbUploadURL.IsInitialized)
                    return;


                lbUploadURL.Content = User.BSN_Path + "Upload\\";

                switch (GetSelectType())
                {
                    case BSN_BSL.PACK.modpack:
                        lbUploadURL.Content += "ModPack\\";
                        break;

                    case BSN_BSL.PACK.basepack:
                        lbUploadURL.Content += "BasePack\\";
                        break;

                    case BSN_BSL.PACK.resource:
                        lbUploadURL.Content += "Resource\\";
                        break;

                    case BSN_BSL.PACK.runtime:
                        lbUploadURL.Content += "Runtime\\";
                        break;
                }

                if (GetSelectType() != BSN_BSL.PACK.runtime)
                    switch (GetSelectKind())
                    {
                        case BSN_BSL.KIND.client:
                            lbUploadURL.Content += "Client\\";
                            break;

                        case BSN_BSL.KIND.server:
                            lbUploadURL.Content += "Server\\";
                            break;
                    }

                btnRefresh_Click(sender, e);
            }
            catch
            {
                return;
            }
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
            BSN_BSL.PACK type = GetSelectType();
            foreach (string ver in BSN_BSL.LoadPackVersionList(type, (string)cbName.SelectedItem, BSN_BSL.STATE.HIDDEN))
            {
                cbVersion.Items.Add(Common.getElement(ver, "version"));
                cbVersion.Tag += Common.getElement(ver, "id") + "|";
            }
            if (type == BSN_BSL.PACK.runtime)
            {
                rbClient.IsChecked = false;
                rbClient.IsEnabled = false;
                rbServer.IsChecked = false;
                rbServer.IsEnabled = false;
            }
            cbVersion.SelectedIndex = 0;
            gbPack.IsEnabled = false;
            gbUpload.IsEnabled = true;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // 로컬 정보 로드
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
                WPFCom.Message("업로드 폴더 로드중 문제가 발생하였습니다." + Environment.NewLine + ex.Message, Base.PROJECT.Bell_Smart_Manager);
            }

            // 서버정보 로드
            lstServer.Items.Clear();
            foreach (string value in BSN_BSL.LoadVersionServer(GetSelectType(), GetSelectKind(), GetSelectVerid()))
            {
                BSN_BSL.Server server = BSN_BSL.LoadServerDetail(value);
                server.select = true;
                server.require_plan = BSN_BSL.GetPlanName((BSN_BSL.PLAN)Convert.ToInt32(server.require_plan));

                lstServer.Items.Add(server);
            }
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            string[] file = lstFile.Items.Cast<string>().ToArray();
            List<string> list = new List<string>();
            foreach (BSN_BSL.Server sv in lstServer.Items)
                if (sv.select)
                    if (BSN_BSM.UploadVersion(GetSelectType(), GetSelectKind(), "id", "pw", GetSelectVerid(), sv.id, file, (string)lbUploadURL.Content))
                    {
                        //sv.select = false;
                    }
                    else
                    {
                        WPFCom.Message(sv.name + "에 업로드를 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
                    }

            WPFCom.Message("업로드를 마쳤습니다.", Base.PROJECT.Bell_Smart_Manager);
        }

        private void cbVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRefresh_Click(sender, e);
        }
    }
}
