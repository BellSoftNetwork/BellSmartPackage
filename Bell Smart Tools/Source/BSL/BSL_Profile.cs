using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BellLib.Class;
using BellLib.Data;
using System.IO;

namespace Bell_Smart_Tools.Source.BSL
{
    public partial class BSL_Profile : Form
    {
        private string ProfileName;
        public BSL_Profile()
        {
            InitializeComponent();
        }
        public BSL_Profile(string ProfileName)
        {
            this.ProfileName = ProfileName;
            string[] Data = Common.ReadBDFile(User.BSL_Root + "Data\\BSL\\Profile\\" + ProfileName + ".bd").Split('\n');
            foreach (string Value in Data)
            {
                string[] tmp = Value.Split('=');
                switch (tmp[0])
                {
                    case "ID":
                        txt_ID.Text = tmp[1];
                        break;
                    case "PW":
                        txt_PW.Text = tmp[1];
                        cb_SavePW.Checked = true;
                        if (txt_PW.Text != string.Empty)
                            cb_SavePW.Checked = false;
                        break;
                    case "CUSTOM_JAVA":
                        cb_Java.Checked = false;
                        if (tmp[1] == "TRUE")
                            cb_Java.Checked = true;
                        break;
                    case "JAVA":
                        txt_Java.Text = tmp[1];
                        break;
                    case "CUSTOM_PARAMETER":
                        cb_Parameter.Checked = false;
                        if (tmp[1] == "TRUE")
                            cb_Parameter.Checked = true;
                        break;
                    case "PARAMETER":
                        txt_Parameter.Text = tmp[1];
                        break;
                }
            }
        }

        private void cb_Java_CheckedChanged(object sender, EventArgs e)
        {
            txt_Java.ReadOnly = !cb_Java.Checked;
        }

        private void cb_Parameter_CheckedChanged(object sender, EventArgs e)
        {
            txt_Parameter.ReadOnly = !cb_Parameter.Checked;
        }

        private void cb_SavePW_CheckedChanged(object sender, EventArgs e)
        {
            txt_PW.ReadOnly = !cb_SavePW.Checked;
            txt_PW.Text = string.Empty;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string Data;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID=" + txt_ID.Text);
            sb.AppendLine("PW=" + txt_PW.Text);
            if (cb_Java.Checked)
            {
                sb.AppendLine("CUSTOM_JAVA=TRUE");
            }
            else
            {
                sb.AppendLine("CUSTOM_JAVA=FALSE");
            }
            sb.AppendLine("JAVA=" + txt_Java.Text);
            if (cb_Parameter.Checked)
            {
                sb.AppendLine("CUSTOM_PARAMETER=TRUE");
            }
            else
            {
                sb.AppendLine("CUSTOM_PARAMETER=FALSE");
            }
            sb.AppendLine("PARAMETER=" + txt_Parameter.Text);

            Data = sb.ToString();
            Common.WriteBDFile(User.BSL_Root + "Data\\BSL\\Profile\\" + txt_Name.Text + ".bd", Data);
            this.Close();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (ProfileName == string.Empty)
                return;
            File.Delete(User.BSL_Root + "Data\\BSL\\Profile\\" + ProfileName + ".bd");
            this.Close();
        }
    }
}
