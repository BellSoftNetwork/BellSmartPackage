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

namespace Bell_Smart_Package.Source.BSP
{
    /// <summary>
    /// Test_Loader.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Test_Loader : Window
    {
        public Test_Loader()
        {
            InitializeComponent();
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            BSP_Loader BSPL = new BSP_Loader();
            BSPL.Show();
        }
    }
}
