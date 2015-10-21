using BellLib.Class;
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
using BellLib.Class.BSN;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// BSL_Main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            PreInitialize();
        }

        /// <summary>
        /// 런처창을 보여주기 전에 먼저 1회 초기화합니다.
        /// </summary>
        private void PreInitialize()
        {
            //Common
            tc_Main.SelectedIndex = 1; // 마지막에 열었던 탭 활성화

            //NEWS


            //MODPACKS
            mod_lstPackList.Items.Clear(); // 팩 리스트 초기화!
            mod_lstPackList.Tag = null; // 팩 태그 초기화!
            mod_lstDetailList.Items.Clear(); // 팩 상세정보 초기화
            mod_cbProfile.Items.Clear(); // 프로필 리스트 초기화
            mod_cbVersion.Items.Clear(); // 팩 버전 리스트 초기화
            mod_expanderDetail.IsExpanded = false;

            //MAPS


            //RESOURCES


            //SETTING

        }

        /// <summary>
        /// 런처창이 로드된 후 사용할 수 있게 초기화합니다.
        /// </summary>
        private void Initialize()
        {
            //Common


            //NEWS


            //MODPACKS
            foreach (string value in BSN_BSL.LoadPackList(BSN_BSL.PACK.modpack))
            {
                mod_lstPackList.Items.Add(Common.getElement(value, "name"));
                mod_lstPackList.Tag += Common.getElement(value, "UID") + "|";
            }
            mod_lstPackList.SelectedIndex = 0; // 마지막에 선택했던 팩 자동선택
            Mod_Expand(mod_expanderDetail.IsExpanded); // 모드탭 익스펜더 설정
            mod_cbProfile.Items.Add("Select Profile");
            mod_cbProfile.Items.Add("Create Profile");
            mod_cbProfile.SelectedIndex = 0;
            /*mod_cbVersion.Items.Add("Recommended");
            mod_cbVersion.Items.Add("Latest");
            mod_cbVersion.SelectedIndex = 0;*/
            
            //MAPS


            //RESOURCES


            //SETTING

        }

        private void Mod_Expand(bool expand)
        {
            if (expand)
            { // 활성화
                mod_lstDetailList.Visibility = Visibility.Visible;
                mod_wbNotice.Height = 284;
            }
            else
            { // 비활성화
                mod_lstDetailList.Visibility = Visibility.Hidden;
                mod_wbNotice.Height = 347;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }
        private void mod_expanderDetail_Expanded(object sender, RoutedEventArgs e)
        { // 익스펜더 열음
            Mod_Expand(true);
        }

        private void mod_expanderDetail_Collapsed(object sender, RoutedEventArgs e)
        { // 익스펜더 접음
            Mod_Expand(false);
        }

        private void mod_cbProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mod_cbProfile.SelectedIndex == 1)
            {
                mod_cbProfile.SelectedIndex = 0;
                Profile Pro = new Profile();
                Pro.ShowDialog();
            }
        }

        private void mod_btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Profile pro = new Profile();
            pro.ShowDialog();
        }

        private void mod_lstPackList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mod_lstPackList.SelectedIndex < 0)
                return;
            string UID = mod_lstPackList.Tag.ToString().Split('|')[mod_lstPackList.SelectedIndex];
            mod_cbVersion.Items.Clear();
            mod_cbVersion.Items.Add("Latest");
            mod_cbVersion.Items.Add("Recommended");
            foreach (string value in BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.modpack, UID))
                mod_cbVersion.Items.Add(Common.getElement(value, "version"));

            mod_cbVersion.SelectedIndex = 1;
        }
    }
}
