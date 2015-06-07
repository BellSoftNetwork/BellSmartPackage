using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Server.Source.BSS
{
    public partial class BSS_Loader : Form
    {
        public BSS_Loader()
        {
            InitializeComponent();
        }

        private bool Initialize()
        {
            Application.DoEvents();
            var actions = new Dictionary<Action, int> 
            {
                {InitServer, 1},
            };

            pb_Load.Minimum = 0;
            pb_Load.Maximum = actions.Select(kvp => kvp.Value).Sum();
            pb_Load.Value = 0;
            foreach (var action in actions)
            {
                action.Key();
                pb_Load.Value += action.Value;
                Application.DoEvents();
            }

            return true;
        }
        private void InitServer()
        {

        }
        private void BSS_Loader_Shown(object sender, EventArgs e)
        {
            if (Initialize())
            {
                BSS_Main Main = new BSS_Main();
                Main.Show();
                this.Close();
            }
        }
    }
}
