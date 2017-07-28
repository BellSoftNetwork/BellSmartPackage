using BellLib.Class;
using MahApps.Metro.Controls;
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
using static BellLib.Class.BSN.BSN_BSL2;

namespace Bell_Smart_Launcher.Source.Frame
{
    /*
     * 메인창에 많은 컨트롤이 배치되기 때문에 아래의 컨트롤 명명 규칙을 지켜주시기 바랍니다.
     * 
     * 컨트롤 명명 규칙
     * [최상위 탭 아이템 이름]_[하위 탭 아이템 이름]_[컨트롤 약자][컨트롤 이름]
     * ex. Setting_General_cbTheme
     */

    /// <summary>
    /// Bell Smart Launcher 2.0 제어 소스
    /// </summary>
    public partial class Main2 : MetroWindow
    {
        #region *** FIELD ***



        #endregion
        
        #region *** INITIALIZE ***

        public Main2()
        {
            InitializeComponent();
            PreInitialize();
            Initialize();
            PostInitialize();
        }

        private void PreInitialize()
        {
            PreInitHome();
            PreInitNews();
            PreInitGame();
            PreInitSetting();
        }

        private void Initialize()
        {
            InitHome();
            InitNews();
            InitGame();
            InitSetting();

            tcMain.SelectedIndex = 2;
            tcGame.SelectedIndex = 1;
        }

        private void PostInitialize()
        {
            PostInitHome();
            PostInitNews();
            PostInitGame();
            PostInitSetting();
        }


        #endregion

        #region *** MAIN ***



        #endregion
    }
}
