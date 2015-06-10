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

namespace Bell_Smart_Tools.Source.BSL
{
    public partial class BSL_Preferences : Form
    {
        public BSL_Preferences()
        {
            InitializeComponent();
        }
        private void Initialize()
        {
            RegistryReader RR = new RegistryReader();
            RR.Key = "MC_ID";
            string ID = (string)RR.GetValue();
            RR.Key = "MC_PW";
            string PW = (string)RR.GetValue();

            if (ID != null)
            {
                txt_ID.Text = ID;
            }
            if (PW != null)
            {
                txt_PW.Text = PW;
            }
            if (User.MC_Login)
            {
                FormSet(true);
            }
        }
        private void FormSet(bool Login)
        {
            lb_Client.Enabled = Login;
            lb_Java.Enabled = Login;
            lb_JVM.Enabled = Login;
            btn_MCLogout.Enabled = Login;
            btn_JavaSearch.Enabled = Login;
            cb_Console.Enabled = Login;
            btn_Save.Enabled = Login;
            gb_MCSetting.Enabled = Login;

            lb_ID.Enabled = !Login;
            lb_PW.Enabled = !Login;
            txt_ID.Enabled = !Login;
            txt_PW.Enabled = !Login;
            btn_MCLogin.Enabled = !Login;
            gb_MCAccount.Enabled = !Login;
        }
        private void btn_MCLogin_Click(object sender, EventArgs e)
        {
            if (MCLogin.Login(txt_ID.Text, txt_PW.Text, MCLogin.LoginType.Authenticate))
            {
                FormSet(true);
            }
            else
            {
                Common.Message("계정을 잘못 입력하셨거나, 접속에 오류가 발생하였습니다.");
            }

        }

        private void btn_MCLogout_Click(object sender, EventArgs e)
        {
            User.MC_ID = null;
            User.MC_PW = null;
            User.MC_Login = false;

            FormSet(false);
        }

        private void MC_Preferences_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        private void SaveSetting()
        {
            RegistryManager RM = new RegistryManager("MC_JAVA", txt_JAVAURL.Text);
            RM.SetValue(); // 자바 경로 저장
            RM = new RegistryManager("MC_Parameter", txt_Parameter.Text);
            RM.SetValue(); // JVM Parameter 저장
            if (cb_Console.Checked)
            {
                RM = new RegistryManager("MC_Console", "TRUE");
                RM.SetValue();
            }
            else
            {
                RM = new RegistryManager("MC_Console", "FALSE");
                RM.SetValue();
            }
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveSetting();
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gb_MCAccount_Enter(object sender, EventArgs e)
        {

        }
    }
}
