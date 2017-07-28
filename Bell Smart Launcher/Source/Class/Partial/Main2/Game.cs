using BellLib.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static BellLib.Class.BSN.BSN_BSL2;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// Main 창의 Maps 탭 분할클래스 입니다.
    /// </summary>
    public partial class Main2
    {
        #region *** INITIALIZE ***

        /// <summary>
        /// 게임탭 관련 기능을 사전 초기화 합니다.
        /// </summary>
        public void PreInitGame()
        {

        }

        /// <summary>
        /// 게임탭 관련 기능을 초기화합니다.
        /// </summary>
        public void InitGame()
        {
            AddItem();
        }

        /// <summary>
        /// 게임탭 관련 기능을 최종 초기화합니다.
        /// </summary>
        public void PostInitGame()
        {

        }

        #endregion

        private void AddItem()
        {
            List<PackInfo> listpi = new List<PackInfo>();
            List<string> authors = new List<string>();
            authors.Add("FTB");
            authors.Add("FTB Team");

            listpi.Add(new PackInfo("방울크래프트", "방울", "환상적인 방울크래프트 !", 156900000, Convert.ToDateTime("2017-06-26 16:47"), "1.10.2", @"M:\Programming\Photo Shop\Projects\Request\로고들\__3.png", true));
            listpi.Add(new PackInfo("InfiTech2", authors, "FTB 인피테크2", 506900, Convert.ToDateTime("2017-05-15 13:32"), "1.7.10"));
            listpi.Add(new PackInfo("마인크래프트", "mojang", "마인크래프트 기본 팩", 1315, Convert.ToDateTime("2016-12-03 20:31"), "ALL"));
            listpi.Add(new PackInfo("베리베리 스트로베리 뽠따스띡 어메이징 언빌리버블 어썸 마인크래프트 팩인데 조금만 더 길게 길게 늘려보도록 하자 엄청 길게 한계를 넘어슬 정도로?? 이 정도면 넘었겠지?", "워후 이건 있을 수 없는 길이의 제작자 이름", "와우 엄청난 최고의 있을 수 없는 대단한 오지고 지리는 마인크래프트 팩의 설명구문인데 생각보다 많이 짧네 조금 더 늘려볼까 이 정도면 중간정도 찼겠지 조금 더 늘리면 칸을 넘어가겠지 그러면 어떻게 될까 넘어가는 부분에 대해서도 구현을 해야되는데 어떤식으로 하는게 좋을까 많이 많이 생각 해 보도록 하자", 15681328975313, Convert.ToDateTime("2016-12-03 20:31"), "1.10.2", "http://bc.softbell.net/files/attach/images/162/252/d9981f3ec68fa22121ab0233a4dd02df.png", true));

            Game_Install_lbPack.ItemsSource = listpi;


            Game_Library_lbPack.ItemsSource = listpi;
        }

        private void btnPackIAP_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            Grid parent = (Grid)bt.Parent;
            PackInfo pi = (PackInfo)parent.DataContext;

            if (pi == null)
                return;

            WPFCom.Message(pi.Name + " " + pi.IAP_Button_Content, BellLib.Data.Basic.PROJECT.Bell_Smart_Launcher);
        }

        private void lbInstallPack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Game_Install_lbPack.SelectedIndex == -1)
                return;

            PackInfo pi = (PackInfo)Game_Install_lbPack.SelectedItem;
            Game_Install_lbPack.SelectedIndex = -1;

            WPFCom.Message(pi.Description, BellLib.Data.Basic.PROJECT.Bell_Smart_Launcher); //////////////////////////
            // 다른 창에 pi 값을 넘겨서 팩 상세정보 표시하기
        }
    }
}
