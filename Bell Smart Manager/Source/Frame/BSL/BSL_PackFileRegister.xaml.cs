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
            lstServer.Items.Clear();

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

            LoadUploadPath(null, null);

            // 데이터 로드
            foreach (BSN_BSL.Server server in BSN_BSL.LoadServerList(BSN_BSL.SERVER.cloud))
                lstServer.Items.Add(server);
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
            // 검사
            if (cbName.SelectedItem == null)
            {
                WPFCom.Message("수정할 팩을 선택해주세요.", Base.PROJECT.Bell_Smart_Manager);
                return;
            }

            // 초기화
            cbVersion.Items.Clear();
            cbVersion.Tag = null;

            // 선택한 팩 버전리스트 로드 (등록되지 않은 버전만 로드하게 구현필요)
            try
            {
                foreach (string value in BSN_BSL.LoadPackVersionList(GetSelectType(), (string)cbName.SelectedItem, BSN_BSL.STATE.PREPARE))
                {
                    cbVersion.Items.Add(Common.getElement(value, "version"));
                    cbVersion.Tag += Common.getElement(value, "id") + "|";
                }
            }
            catch { }

            // 마무으리
            if (GetSelectType() == BSN_BSL.PACK.runtime)
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
            // 업로드할 로컬 파일정보 로드
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

            // 업로드된 파일정보 로드
            lstUploadFile.Items.Clear();
            foreach (BSN_BSL.Install ins in BSN_BSL.LoadVersionFiles(GetSelectType(), GetSelectKind(), GetSelectVerid()))
                lstUploadFile.Items.Add(ins.url);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string[] File = lstFile.Items.Cast<string>().ToArray();

            // 선택한 서버 리스트 로드
            List<string> list = new List<string>();
            foreach (BSN_BSL.Server sv in lstServer.Items)
                if (sv.select)
                    list.Add(sv.id);
            if (list.Count == 0)
            {
                WPFCom.Message("1개 이상의 서버에 클라이언트 파일을 업로드 해야합니다.", Base.PROJECT.Bell_Smart_Manager);
                return;
            }

            if (BSN_BSM.RegisterFile(GetSelectType(), GetSelectKind(), "id", "pw", GetSelectVerid(), (string)lbUploadURL.Content, File, list.ToArray()))
                WPFCom.Message("성공적으로 등록하였습니다.", Base.PROJECT.Bell_Smart_Manager);
            else
                WPFCom.Message("등록에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
            btnRefresh_Click(sender, e); // 업로드 된 파일 상태 재 로드
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Message("정보 수정을 마감하면 더 이상 해당 버전의 정보를 수정할 수 없습니다." + Environment.NewLine + "정말로 마감하시겠습니까?", Base.PROJECT.Bell_Smart_Manager, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
                return;

            if (BSN_BSM.SubmitVersion(GetSelectType(), "id", "pw", GetSelectVerid()))
                WPFCom.Message("검토 요청에 성공하였습니다.", Base.PROJECT.Bell_Smart_Manager);
            else
                WPFCom.Message("검토 요청에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
            btnLoad_Click(sender, e);
        }

        private void cbVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btnRefresh_Click(sender, e); // 업로드 된 파일 상태 재 로드
            }
            catch { }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (BSN_BSM.ResetVersion(GetSelectType(), GetSelectKind(), "id", "pw", GetSelectVerid()))
                WPFCom.Message("초기화에 성공하였습니다.", Base.PROJECT.Bell_Smart_Manager);
            else
                WPFCom.Message("초기화에 실패하였습니다.", Base.PROJECT.Bell_Smart_Manager);
            btnRefresh_Click(sender, e); // 업로드 된 파일 상태 재 로드
        }
    }
}
