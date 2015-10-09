using Bell_Smart_Manager.Source.Frame.BSL;
using BellLib.Class;
using System;
using System.Windows;

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

        private void btnPackUploader_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Feasibility("Bell_Smart_Manager.Source.Frame.BSL.BSL_PackUploader"))
            {
                BSL_PackUploader PU = new BSL_PackUploader();
                PU.Show();
            }
        }

        private void btnPackReview_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Feasibility("Bell_Smart_Manager.Source.Frame.BSL.BSL_PackReview"))
            {
                BSL_PackReview PR = new BSL_PackReview();
                PR.Show();
            }
        }

        private void btnPackControl_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Feasibility("Bell_Smart_Manager.Source.Frame.BSL.BSL_PackControl"))
            {
                BSL_PackControl PC = new BSL_PackControl();
                PC.Show();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            long start = DateTime.Now.Ticks;
            Common.Delay(100);
            long end = DateTime.Now.Ticks;
            WPFCom.Message("소요 시간 : " + ((end - start)/10000));
        }

        private void btnServerMaker_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Feasibility("Bell_Smart_Manager.Source.Frame.BSL.BSL_ServerMaker"))
            {
                BSL_ServerMaker SM = new BSL_ServerMaker();
                SM.Show();
            }
        }
    }
}
