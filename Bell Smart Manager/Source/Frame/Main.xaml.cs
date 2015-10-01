using Bell_Smart_Manager.Source.Frame.BSL;
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

namespace Bell_Smart_Manager.Source.Frame
{
    /// <summary>
    /// Main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }
        
        private void Window_Closed(object sender, EventArgs e)
        {
            WPFCom.End();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string[] data = BSN_Info.loadPackList(BSN_Info.PACK.BasePack);
            foreach (string tmp in data)
            {
                WPFCom.Message(Common.getElement(tmp, "BUID"));
            }
        }

        private void btnPackMaker_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Feasibility("Bell_Smart_Manager.Source.Frame.BSL.BSL_PackMaker"))
            {
                BSL_PackMaker PM = new BSL_PackMaker();
                PM.Show();
            }
        }

        private void btnPackEditor_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Feasibility("Bell_Smart_Manager.Source.Frame.BSL.BSL_PackEditor"))
            {
                BSL_PackEditor PE = new BSL_PackEditor();
                PE.Show();
            }
        }
    }
}
