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
        
        /// <summary>
        /// 새 프로필을 생성합니다.
        /// </summary>
        public BSL_Profile()
        {
            InitializeComponent();
            txt_Java.Text = User.BSN_Path + "Runtime\\JAVA\\";
            if (Environment.Is64BitOperatingSystem)
            {
                txt_Java.Text += "x64\\";
                txt_Parameter.Text = "-Xmx2G";
            }
            else
            {
                txt_Java.Text += "x86\\";
                txt_Parameter.Text = "-Xmx1G";
            }
            txt_Java.Text += "bin\\java.exe";
        }

        /// <summary>
        /// 프로필 에디터를 선택한 프로필파일로 초기화합니다.
        /// </summary>
        /// <param name="ProfileName">프로필 이름</param>
        public BSL_Profile(string ProfileName)
        {
            InitializeComponent();
            this.ProfileName = ProfileName;
            string[] Data = Protection.ReadBDXFile(User.BSL_Root + "Data\\BSL\\Profile\\" + ProfileName + ".bdx");

            txt_Name.Text = ProfileName;
            foreach (string Value in Data)
            {
                string[] tmp = Value.Split('|');
                switch (tmp[0])
                {
                    case "ID":
                        txt_ID.Text = tmp[1];
                        break;

                    case "PW":
                        txt_PW.Text = tmp[1];
                        cb_SavePW.Checked = true;
                        if (txt_PW.Text == string.Empty)
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

                    case "PARAMETER":
                        cb_Parameter.Checked = true;
                        txt_Parameter.Text = tmp[1];
                        if (txt_Parameter.Text == string.Empty)
                            cb_Parameter.Checked = false;
                        break;
                }
            }
        }

        /// <summary>
        /// 프로필 데이터 종류 리스트
        /// </summary>
        public enum Data
        {
            Name,
            JAVA,
            ID,
            PW,
            Parameter
        }

        /// <summary>
        /// 프로필 데이터를 가져옵니다.
        /// </summary>
        /// <param name="Value">반환할 프로필 데이터 값</param>
        /// <returns>프로필 데이터</returns>
        public string getData(Data Value)
        {
            string strTemp = string.Empty;

            switch (Value)
            {
                case Data.Name:
                    strTemp = ProfileName;
                    break;

                case Data.JAVA:
                    strTemp = txt_Java.Text;
                    break;

                case Data.ID:
                    strTemp = txt_ID.Text;
                    break;

                case Data.PW:
                    strTemp = txt_PW.Text;
                    break;

                case Data.Parameter:
                    strTemp = txt_Parameter.Text;
                    break;
            }

            return strTemp;
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
            // 설정값 정리
            if (txt_PW.Text == string.Empty)
                cb_SavePW.Checked = false;

            // 필드 검사
            List<string> list = new List<string>();
            if (txt_Name.Text == string.Empty)
                list.Add("프로필 이름");
            if (txt_ID.Text == string.Empty)
                list.Add("계정 아이디");
            if (txt_Java.Text == string.Empty)
                list.Add("자바 실행 경로");

            if (list.Count != 0) // 없는 값이 있을경우
            {
                string strTemp = null;
                foreach (string tmp in list)
                {
                    strTemp += tmp;
                    if (list[list.Count - 1] != tmp)
                        strTemp += ", ";
                }
                Common.Message(strTemp + " 값을 입력해주세요.");
                return;
            }

            // 저장
            list.Clear(); // 위에서 한번 썼으니 초기화!

            list.Add("ID|" + txt_ID.Text);
            list.Add("PW|" + txt_PW.Text);
            if (cb_Java.Checked)
            {
                list.Add("CUSTOM_JAVA|TRUE");
            }
            else
            {
                list.Add("CUSTOM_JAVA|FALSE");
            }
            list.Add("JAVA|" + txt_Java.Text);
            /*if (cb_Parameter.Checked)
            {
                list.Add("CUSTOM_PARAMETER|TRUE");
            }
            else
            {
                list.Add("CUSTOM_PARAMETER|FALSE");
            }*/
            list.Add("PARAMETER|" + txt_Parameter.Text);

            if (ProfileName != string.Empty)
                File.Delete(User.BSL_Root + "Data\\BSL\\Profile\\" + ProfileName + ".bdx"); // 열렸던 파일 삭제
            Protection.WriteBDXFile(User.BSL_Root + "Data\\BSL\\Profile\\" + txt_Name.Text + ".bdx", list.ToArray()); // 프로필 파일 저장
            ProfileName = txt_Name.Text;
            
            this.Close();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (ProfileName == string.Empty)
                return;
            File.Delete(User.BSL_Root + "Data\\BSL\\Profile\\" + ProfileName + ".bdx");
            this.Close();
        }
    }
}
