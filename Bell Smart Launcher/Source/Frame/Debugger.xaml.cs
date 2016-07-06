using Bell_Smart_Launcher.Source.Data;
using BD = BellLib.Class.Analysis;
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
using BellLib.Data;
using BellLib.Class;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// Debugger.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Debugger : Window
    {
        private string DebugDataPath = User.BSN_Path + "DATA\\BSL\\Debug.bdx";

        public Debugger()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 세팅값을 로드합니다.
        /// </summary>
        private void SettingLoad()
        {
            cbPWV.IsChecked = DebugCategory.PWV;
        }

        public bool SaveDebug()
        {
            DebugCategory.PWV = (bool)cbPWV.IsChecked;

            BD.Data.DataSave(DebugDataPath, "PWV", DebugCategory.PWV.ToString());

            SettingLoad();

            return true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveDebug();
            WPFCom.Message("디버그설정 저장에 성공하였습니다.");
        }
    }
}
