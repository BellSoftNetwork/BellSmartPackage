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
            if (WPFCom.Feasibility("Bell_Smart_Manager.Source.Frame.BSL.BSL_PackRegister"))
            {
                BSL_PackRegister PR = new BSL_PackRegister();
                PR.Show();
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
            if (WPFCom.Feasibility("Bell_Smart_Manager.Source.Frame.BSL.BSL_ServerRegister"))
            {
                BSL_ServerRegister SR = new BSL_ServerRegister();
                SR.Show();
            }
        }

        private void btnPackVerRegister_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Feasibility("Bell_Smart_Manager.Source.Frame.BSL.BSL_PackVerRegister"))
            {
                BSL_PackVerRegister PVR = new BSL_PackVerRegister();
                PVR.Show();
            }
        }

        private void btnPackFileRegister_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Feasibility("Bell_Smart_Manager.Source.Frame.BSL.BSL_PackFileRegister"))
            {
                BSL_PackFileRegister PFR = new BSL_PackFileRegister();
                PFR.Show();
            }
        }

        private void btnServerReview_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCom.Feasibility("Bell_Smart_Manager.Source.Frame.BSL.BSL_ServerReview"))
            {
                BSL_ServerReview SR = new BSL_ServerReview();
                SR.Show();
            }
        }
    }
}
