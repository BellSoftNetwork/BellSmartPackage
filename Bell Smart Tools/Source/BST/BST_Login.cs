using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Bell_Smart_Tools.Source.BST
{
    public partial class BST_Login : Form
    {
        public BST_Login()
        {
            InitializeComponent();
        }

        private void Initialize() // 폼 초기화
        {
            string Email = Class.Common.RegLoad("Email"); // 레지스트리에서 저장된 Email 값 로드
            string PW = Class.Common.RegLoad("PW"); // 레지스트리에서 저장된 PW 값 로드

            if (Email != null) { // 레지스트리에 Email 값이 존재한다면,
                txt_Email.Text = Email; // 이메일 텍스트 박스에 값 대입
                cb_EmailSave.Checked = true; // 이메일 저장 체크박스 활성화
            }
            if (PW != null) // 레지스트리에 PW 값이 존재한다면,
            {
                txt_PW.Text = PW; // PW 텍스트 박스에 값 대입
                cb_PWSave.Checked = true; // PW 저장 체크박스 활성화
            }
        }
        private void FormEnable(bool value)
        {
            txt_Email.Enabled = value;
            txt_PW.Enabled = value;
            btn_Login.Enabled = value;
            cb_EmailSave.Enabled = value;
            cb_PWSave.Enabled = value;
            cb_AutoLogin.Enabled = value;
            llb_text.Enabled = value;
        }
        private void btn_Login_Click(object sender, EventArgs e)
        {
            FormEnable(false);
            if (Class.BSN.BSNLogin(txt_Email.Text,txt_PW.Text)) // BSN 회원 인증 성공시
            {
                BST_Main BST = new BST_Main(); // BST_Main 인스턴스 생성
                BST.Show(); // BST_Main 실행
                this.Hide(); // BST_Login 숨김
            }else{
                Class.Common.Message("인증 실패");
            }
            FormEnable(true);
        }

        private void BST_Login_Load(object sender, EventArgs e)
        {
            Initialize(); // 계정 정보 로드 및 초기화
        }

        private void txt_Email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Login_Click(sender, e); // 로그인 버튼 클릭
            }
        }

        private void txt_PW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Login_Click(sender, e); // 로그인 버튼 클릭
            }
        }

        private void llb_text_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Data.Base.BSN_WEB_URL);
        }

        private void cb_EmailSave_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_EmailSave.Checked == false)
            {
                cb_PWSave.Checked = false;
                cb_AutoLogin.Checked = false;
            }
        }

        private void cb_PWSave_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_PWSave.Checked == true)
            {
                cb_EmailSave.Checked = true;
            }
        }

        private void cb_AutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_AutoLogin.Checked == true)
            {
                cb_EmailSave.Checked = true;
                cb_PWSave.Checked = true;
            }
        }

        private void BST_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Class.Common.End();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BST_Main BST = new BST_Main(); // BST_Main 인스턴스 생성
            BST.Show(); // BST_Main 실행
            this.Hide(); // BST_Login 숨김
        }
    }
}
