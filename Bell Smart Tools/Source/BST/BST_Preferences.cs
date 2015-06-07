using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BellLib.Class;

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
            
            string email = Common.RegLoad("BSN_Email");
            string pw = Common.RegLoad("BSN_Password");
            string auto = Common.RegLoad("BSN_AutoLogin");

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
        }
        private void btn_DisAuto_Click(object sender, EventArgs e)
        {
            Common.RegDelete("BSN_AutoLogin");
            Initialize(); // 폼 새로고침
            //Common.Message("자동 로그인 설정이 정상적으로 해제되었습니다.");
        }

        private void BST_Preferences_Load(object sender, EventArgs e)
        {
            Initialize(); // 폼 초기화
        }
    }
}
