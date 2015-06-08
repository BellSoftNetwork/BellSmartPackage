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

namespace Bell_Smart_Tools.Source.BST
{
    public partial class BST_Preferences : Form
    {
        public BST_Preferences()
        {
            InitializeComponent();
        }
        /// <summary>
        /// BST_Preferences 폼 내용을 초기화 합니다.
        /// </summary>
        private void Initialize()
        {
            lb_email.Text = "이메일 저장 : Loading..";
            lb_PW.Text = "비밀번호 저장 : Loading";
            btn_DisAuto.Enabled = false;
            // 기본 설정으로 초기화 끝

            RegistryReader rReader = new RegistryReader();

            rReader.Key = "BSN_Email";
            string email = (string)rReader.GetValue();

            rReader.Key = "BSN_Password";
            string pw = (string)rReader.GetValue();

            rReader.Key = "BSN_AutoLogin";
            string auto = (string)rReader.GetValue();

            if (email != null)
            {
                lb_email.Text = "이메일 저장 : " + email;
            } 
            else 
            {
                lb_email.Text = "이메일 저장 : 사용안함";
            }
            if (pw != null) {
                lb_PW.Text = "비밀번호 저장 : " + pw;
            }
            else
            {
                lb_PW.Text = "비밀번호 저장 : 사용안함";
            }
            if (auto == "TRUE")
            {
                btn_DisAuto.Enabled = true;
            }
            cb_AutoUpdate.Checked = User.BSP_AutoUpdate;
        }
        private void btn_DisAuto_Click(object sender, EventArgs e)
        {
            using (RegistryManager rm = new RegistryManager("BSN_AutoLogin"))
                rm.DeleteValue();
            Initialize(); // 폼 새로고침
            //Common.Message("자동 로그인 설정이 정상적으로 해제되었습니다.");
        }

        private void BST_Preferences_Load(object sender, EventArgs e)
        {
            Initialize(); // 폼 초기화
        }

        private void cb_AutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            User.BSP_AutoUpdate = cb_AutoUpdate.Checked;
        }
    }
}
