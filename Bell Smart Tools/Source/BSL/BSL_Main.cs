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

            cb_Version.Items.Clear(); // 버전정보 리스트 초기화!
            string[] Default = { "Latest", "Recommended" }; // 기본값 임시 저장
            cb_Version.Items.AddRange(Default); // 기본값 삽입!
            cb_Version.SelectedItem = "Recommended"; // 선택값을 권장버전으로 설정!
        }

        private void lst_ModPack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_ModPack.SelectedItem != null)
            {
                cb_Version.Items.Clear(); // 반복 클릭하면 값이 중복되니 초기화!
                string[] Default = { "Latest", "Recommended" }; // 기본값 임시 저장
                cb_Version.Items.AddRange(Default); // 기본값 삽입!
                cb_Version.SelectedItem = "Recommended"; // 선택값을 권장버전으로 설정!
                cb_Version.Enabled = true; // 버전정보 선택 활성화!

                ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Mod, (string)lst_ModPack.SelectedItem); // 선택된 버전정보로 인스턴스 생성
                cb_Version.Items.AddRange(MAR.GetVersion(ModAnalysisRead.PackType.Mod)); // 선택 모드팩 버전정보 삽입!
                wb_PackNews.AllowNavigation = true; // 뉴스페이지를 바꿔야되니 잠시 페이지 이동 허용해주고!
                string News = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "News");
                Uri URI = new Uri(News);
                wb_PackNews.Url = URI; // 선택 모드팩 뉴스페이지 로드!
                while (wb_PackNews.ReadyState != WebBrowserReadyState.Complete)
                { // 아직 로딩중일 때 페이지 변경 비허용을 시키면 페이지 로드가 안되니 로드가 완료될때까지 대기
                    Application.DoEvents(); // 무한 루프만 돌리면 UI가 렉먹으니 UI 메시지 큐 처리.
                }
                wb_PackNews.AllowNavigation = false; // 다시 페이지 변경 비허용!
            }
            else
            {
                cb_Version.Enabled = false; // 암것도 선택 안됬으면 버전 선택해야 의미없음!
            }
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

        private void cb_auto_update_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void btn_Preferences_Click(object sender, EventArgs e)
        {
            BSL_Preferences BSLP = new BSL_Preferences();
            BSLP.ShowDialog();
        }
    }
}
