using Bell_Smart_Launcher.Source.Class;
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
            mod_lst_PackList.Items.Clear(); // 팩 리스트 초기화!
            mod_lst_DetailList.Items.Clear(); // 팩 상세정보 초기화
            mod_cb_Profile.Items.Clear(); // 프로필 리스트 초기화
            mod_cb_Version.Items.Clear(); // 팩 버전 리스트 초기화
            mod_expander_Detail.IsExpanded = false;

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
            mod_lst_PackList.Items.Add("BellCraft9"); // 테스트용 추후 삭제
            mod_lst_PackList.Items.Add("TestPack");
            mod_lst_PackList.Items.Add("FTB Pack");
            mod_lst_PackList.SelectedIndex = 0; // 마지막에 선택했던 팩 자동선택
            Mod_Expand(mod_expander_Detail.IsExpanded); // 모드탭 익스펜더 설정
            mod_cb_Profile.Items.Add("Select Profile");
            mod_cb_Profile.Items.Add("Create Profile");
            mod_cb_Profile.SelectedIndex = 0;
            mod_cb_Version.Items.Add("Recommended");
            mod_cb_Version.Items.Add("Latest");
            mod_cb_Version.SelectedIndex = 0;
            
            //MAPS


            //RESOURCES


            //SETTING

        }

        private void Mod_Expand(bool expand)
        {
            if (expand)
            { // 활성화
                mod_lst_DetailList.Visibility = Visibility.Visible;
                mod_wb_Notice.Height = 284;
            }
            else
            { // 비활성화
                mod_lst_DetailList.Visibility = Visibility.Hidden;
                mod_wb_Notice.Height = 347;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }
        private void mod_expander_Detail_Expanded(object sender, RoutedEventArgs e)
        { // 익스펜더 열음
            Mod_Expand(true);
        }

        private void mod_expander_Detail_Collapsed(object sender, RoutedEventArgs e)
        { // 익스펜더 접음
            Mod_Expand(false);
        }

        private void mod_cb_Profile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mod_cb_Profile.SelectedIndex == 1)
            {
                mod_cb_Profile.SelectedIndex = 0;
                Profile Pro = new Profile();
                Pro.ShowDialog();
            }
        }

        private void mod_btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            Profile pro = new Profile();
            pro.ShowDialog();
        }
    }
}
