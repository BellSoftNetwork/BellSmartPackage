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
            mod_PackList.Items.Clear(); // 팩 리스트 초기화!
            
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
            mod_PackList.Items.Add("테스트1");
            mod_PackList.Items.Add("테스트2");
            mod_PackList.Items.Add("테스트3");

            //MAPS


            //RESOURCES


            //SETTING

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }
    }
}
