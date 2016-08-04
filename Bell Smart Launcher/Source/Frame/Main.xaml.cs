using BellLib.Class;
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
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Reflection;
using BellLib.Class.Protection;
using System.Windows.Threading;
using Bell_Smart_Launcher.Class;
using BellLib.Class.Minecraft;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// BSL_Main.xaml에 대한 상호 작용 논리
    /// 소스는 탭에 따라 Source\Class\Partial\Main 폴더 안애 정의하였음.
    /// 메인창에 기능이 많으므로 조금이나마 가독성을 높이기 위해 각 탭에만 해당하는 메소드는 해당 파일에 정의하고,
    /// 공통적으로 사용하는 메소드만 이 파일에 정의하기 바람.
    /// </summary>
    public partial class Main : Window
    {
        #region *** FIELD ***

        private DispatcherTimer tmr_GameCheck;
        private bool GameNormal;

        private Modpack GameInfo;
        private BellSmartController bsc;
        private bool noticeLock = true;

        #endregion

        #region *** INITIALIZE ***

        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 런처창을 보여주기 전에 먼저 1회 초기화합니다.
        /// </summary>
        public void PreInitialize()
        {
            //Common
            // 마지막에 열었던 탭 활성화
            try
            {
                tc_Main.SelectedIndex = Convert.ToInt32(DataProtect.DataLoad(DataPath.BSL.General, "LastSelectedTab"));
            }
            catch { }
            ti_Resources.Visibility = Visibility.Collapsed; // 아직 개발되지 않은 영역이므로 임시로 가려둠
            ti_Maps.Visibility = Visibility.Collapsed; // 아직 개발되지 않은 영역이므로 임시로 가려둠

            //NEWS


            //MODPACKS
            mod_lstPackList.Items.Clear(); // 팩 리스트 초기화!
            mod_lstDetailList.Items.Clear(); // 팩 상세정보 초기화
            mod_cbProfile.Items.Clear(); // 프로필 리스트 초기화
            mod_cbVersion.Items.Clear(); // 팩 버전 리스트 초기화
            mod_cbFilter.Items.Clear(); // 필터 리스트 초기화
            mod_btnForceKill.IsEnabled = false;

            tmr_GameCheck = new DispatcherTimer(); // 게임 체크 타이머 초기화

            //MAPS


            //RESOURCES


            //SETTING

        }
        
        #endregion

        #region *** METHOD ***

        /// <summary>
        /// 자바 스크립트 에러를 숨겨줍니다.
        /// </summary>
        /// <param name="wb"></param>
        public void HideJsScriptErrors(WebBrowser wb)
        {
            // IWebBrowser2 interface
            // Exposes methods that are implemented by the WebBrowser control  
            // Searches for the specified field, using the specified binding constraints.

            FieldInfo fld = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fld == null)
                return;
            object obj = fld.GetValue(wb);
            if (obj == null)
                return;
            // Silent: Sets or gets a value that indicates whether the object can display dialog boxes.
            // HRESULT IWebBrowser2::get_Silent(VARIANT_BOOL *pbSilent);HRESULT IWebBrowser2::put_Silent(VARIANT_BOOL bSilent);
            obj.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, obj, new object[] { true });
        }

        #endregion

        
        #region *** MAIN ***

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //Initialize();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WPFCom.End();
        }

        private void tc_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tc_Main.SelectedIndex != -1)
                DataProtect.DataSave(DataPath.BSL.General, "LastSelectedTab", tc_Main.SelectedIndex.ToString()); // 현재 선택한 탭 인덱스 저장
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!GameInfo.Feasibility())
                if (WPFCom.Message("현재 게임이 실행중입니다." + Environment.NewLine + "런처에 종속성을 가진 게임은 런처종료 후 문제가 발생할 수 있습니다." + Environment.NewLine + "정말로 종료하시겠습니까?", Base.PROJECT.Bell_Smart_Launcher, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
                    e.Cancel = true;
        }

        #endregion
    }
}