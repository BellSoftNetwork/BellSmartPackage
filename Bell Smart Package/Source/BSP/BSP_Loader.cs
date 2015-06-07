using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bell_Smart_Package.Source.BSP;


namespace Bell_Smart_Package.Source.BSP
{
    public partial class BSP_Loader : Form
    {
        public BSP_Loader()
        {
            InitializeComponent();
        }

        private void BSP_Loader_Shown(object sender, EventArgs e)
        {
            BSP_Login BSP = new BSP_Login();
            BSP.Show();

            this.Hide();

        }
    }
}
