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
            foreach (string tmp in BSN_BSL.loadModPackList(User.BSN_member_srl))
                cbMUID.Items.Add(tmp);
            // 베이스팩 데이터 로드
            //foreach (string tmp in BSN_Info.loadModPackList(User.BSN_member_srl))
                //cbBUID.Items.Add(tmp);
            // 리소스 데이터 로드
            //foreach (string tmp in BSN_Info.loadModPackList(User.BSN_member_srl))
                //cbRUID.Items.Add(tmp);

            // 기본값 선택
            cbMUID.SelectedIndex = 0;
            cbBUID.SelectedIndex = 0;
            cbRUID.SelectedIndex = 0;
        }
        
        private void btnModLoad_Click(object sender, RoutedEventArgs e)
        {
            string MUID = (string)cbMUID.SelectedItem;
            string name, latest, recommended, BUID, state, plan;
            if (BSN_BSL.loadModPackDetail(MUID, out name, out latest, out recommended, out BUID, out state, out plan))
            {
                // 로드 성공시
                txtModName.Text = name;
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
                
                gbMod.IsEnabled = false;
                tcMod.IsEnabled = true;
            }
        }

        private void btnBaseLoad_Click(object sender, RoutedEventArgs e)
        {
            // 로드 성공시
            gbBase.IsEnabled = false;
            tcBase.IsEnabled = true;
        }

        private void btnResLoad_Click(object sender, RoutedEventArgs e)
        {
            // 로드 성공시
            gbResource.IsEnabled = false;
            tcResource.IsEnabled = true;
        }

        private void btnModSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModSelSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModAdd_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnModSelDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModAuthDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModAuthRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstModPermission.Items.Clear();
            foreach (BSN_BSL.Manager auth in BSN_BSL.loadPackManager(BSN_BSL.PACK.ModPack, (string)cbMUID.SelectedItem))
                lstModPermission.Items.Add(auth);
        }

        private void btnModNews_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModDetail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBaseSave_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private void btnBaseAuthDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBaseAuthRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResSave_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private void btnResAuthDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResAuthRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResNews_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResDetail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
