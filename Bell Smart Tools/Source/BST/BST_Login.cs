using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BellLib.Class;
using BellLib.Data;
using BellLib.Class.BSN;

namespace Bell_Smart_Tools.Source.BSS
{
    public partial class BST_Login : Form
    {
        public BST_Login()
        {
            InitializeComponent();
            this.txt_PW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            this.txt_Email.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
        }

        private void Initialize() // 폼 초기화
        {
            string Email;
            string PW;

            RegistryReader rReader = new RegistryReader();

            rReader.Key = "BSN_Email";
            Email = (string)rReader.GetValue();
            rReader.Key = "BSN_Password";
            PW = (string)rReader.GetValue();

            /*
            string Email = Common.RegLoad("BSN_Email"); // 레지스트리에서 저장된 Email 값 로드
            string PW = Common.RegLoad("BSN_Password"); // 레지스트리에서 저장된 PW 값 로드
            string AutoLogin = Common.RegLoad("BSN_AutoLogin"); // 레지스트리에서 저장된 Auto Login 값 로드
            */
            if (Email != null) { // 레지스트리에 Email 값이 존재한다면,
                txt_Email.Text = Email; // 이메일 텍스트 박스에 값 대입
                cb_EmailSave.Checked = true; // 이메일 저장 체크박스 활성화
            }
            if (PW != null) // 레지스트리에 PW 값이 존재한다면,
            {
                txt_PW.Text = PW; // PW 텍스트 박스에 값 대입
                cb_AutoLogin.Checked = true;
                FormEnable(false);
            }
        }

        private void FormEnable(bool value)
        {
            txt_Email.Enabled = value;
            txt_PW.Enabled = value;
            btn_Login.Enabled = value;
            cb_EmailSave.Enabled = value;
            cb_AutoLogin.Enabled = value;
            llb_text.Enabled = value;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            FormEnable(false);

            BSN.Login(txt_Email.Text,txt_PW.Text); // 작업 성공 여부를 반환하는게 목표지 로그인된걸 반환하는게 아냐!

            if (BSN.LoginStatus) // BSN 회원 인증 성공시
            {
                BSN.SaveUserdata(cb_EmailSave.Checked, cb_AutoLogin.Checked);
                BST_Main BSP = new BST_Main(); // BSP_Selector 인스턴스 생성
                BSP.Show(); // BSP_Selector 실행
                this.Hide(); // BST_Login 숨김
            } else {
                WinCom.Message("인증 실패", Base.PROJECT.Bell_Smart_Tools);
            }
            FormEnable(true);
        }

        private void BST_Login_Load(object sender, EventArgs e)
        {
            Initialize(); // 계정 정보 로드 및 초기화
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Login_Click(sender, e); // 로그인 버튼 클릭
            }
        }

        private void llb_text_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Servers.Bell_Soft_Network.WEB_BSN_ROOT);
        }

        private void cb_EmailSave_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_EmailSave.Checked == false)
                cb_AutoLogin.Checked = false;
        }

        private void cb_AutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_AutoLogin.Checked == true)
                cb_EmailSave.Checked = true;
        }

        private void BSP_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            WinCom.End();
        }

        private void BSP_Login_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            if (cb_AutoLogin.Checked) {
                btn_Login_Click(sender,e);
            }
        }
    }
}
