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
    /// PackSetting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PackSetting : Window
    {

        public PackSetting()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            // 초기화
            lstModPack.Items.Clear();
            lstVersion.Items.Clear();

        }
        
        private void lstModPack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAllVerRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModPackEditor_Click(object sender, RoutedEventArgs e)
        {
            ModPackEditor mpe = new ModPackEditor();
            mpe.ShowDialog();
        }

        private void btnIntegrityCheck_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReinstall_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVerRemove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
