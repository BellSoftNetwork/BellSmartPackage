﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bell_Smart_Tools.Source.Frame
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

        private void btnBSC_Click(object sender, RoutedEventArgs e)
        {
            BSC bsc = new BSC();
            bsc.Show();
        }

        private void btnProtection_Click(object sender, RoutedEventArgs e)
        {
            BellProtection bp = new BellProtection();
            bp.Show();
        }
    }
}
