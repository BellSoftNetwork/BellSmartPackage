using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BellLib.Data;
using BellLib.Class;

namespace Bell_Smart_Tools.Source.BSL
{
    public partial class BSL_Main : Form
    {
        public BSL_Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// BSL을 초기화합니다.
        /// </summary>
        private void Initialize()
        {
            ListLoad(); // 팩 리스트 로드
        }

        /// <summary>
        /// 팩 리스트를 로드합니다.
        /// </summary>
        private void ListLoad()
        {
            lst_ModPack.Items.Clear(); // 리스트 전체 초기화!

            ModAnalysisRead MAR = new ModAnalysisRead();
            lst_ModPack.Items.AddRange(MAR.GetList(ModAnalysisRead.PackType.Mod)); // 팩 리스트 로드!
        }
        private void wb_PackNews_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void lst_ModPack_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_Version.Items.Clear(); // 반복 클릭하면 값이 중복되니 초기화!
            string[] Default = { "Latest", "Recommended" };
            cb_Version.Items.AddRange(Default);
            cb_Version.SelectedItem = "Recommended"; // 선택값을 권장버전으로 설정!

            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Mod, (string)lst_ModPack.SelectedItem);
            cb_Version.Items.AddRange(MAR.GetVersion(ModAnalysisRead.PackType.Mod));
        }
        
        private void BSL_Main_Load(object sender, EventArgs e)
        {
            Initialize(); // 초기화
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ms_BSL_PreferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BSL_Preferences BSLP = new BSL_Preferences();
            BSLP.ShowDialog();
        }

        private void btn_Launch_Click(object sender, EventArgs e)
        {

        }

        private void lb_modlist_Click(object sender, EventArgs e)
        {

        }
        //밑에있는게 뭐였는지 까먹음 ㅋㅋㅋㅋㅋ 혹시나 알게되면 주석좀 달아봐 by UI관계자
        private void pb_Click(object sender, EventArgs e)
        {

        }

        private void lb_status_log_lable_Click(object sender, EventArgs e)
        {

        }

        private void cb_auto_update_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cb_profile_list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bt_MCpreference_Click(object sender, EventArgs e)
        {
            BSL_Preferences BSLP = new BSL_Preferences();
            BSLP.ShowDialog();
        }
    }
}
