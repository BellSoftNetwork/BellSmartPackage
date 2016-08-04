using BellLib.Class;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// Main 창의 News 탭 분할클래스 입니다.
    /// </summary>
    public partial class Main
    {
        #region *** INITIALIZE ***

        /// <summary>
        /// 뉴스탭 관련 기능을 초기화합니다.
        /// </summary>
        public void InitNews()
        {
            // 기본값 초기화
            news_wbNews.NavigateToString("<meta charset=\"utf-8\"><p>여러분에게 알려드리고 싶은 소식이 여기저기 흩어져 있어서 찾는데 오래 걸리네요.</p><p>정리되면 보여드릴게요.</p>");

            Task loadNews = new Task(LoadNews);
            loadNews.Start();
        }

        #endregion

        /// <summary>
        /// 뉴스 목록을 불러옵니다.
        /// </summary>
        private void LoadNews()
        {
            while (true)
            {
                // 필드
                List<string> NewsList = new List<string>();
                StringBuilder Newsfeed = new StringBuilder();
                string strData;
                string[] strUnprocessdList;

                try
                {
                    // 데이터 로드
                    // Bell Smart Launcher 공지 분류에 해당하는 공지만 받아옴
                    strData = Common.getStringFromWeb("http://www.softbell.net/index.php?mid=notice&category=1312", Encoding.UTF8);
                    // 문서 srl을 얻기 위한 문구로 파싱
                    strUnprocessdList = Common.stringSplit(strData, "mid=notice&amp;category=1312&amp;document_srl=");


                    // 뉴스 리스트 로드
                    for (int i = 1; i < strUnprocessdList.Length - 1; i++) // 제일 첫 집합은 관련없는 값이므로 버리고 두번째 집합부터 돌림
                    {
                        string strDocsrl = Common.stringSplit(strUnprocessdList[i], "\"")[0]; // 뒤에 잡 내용은 버리고 앞에 숫자만 받아옴
                        try
                        {
                            int intDoc = Convert.ToInt32(strDocsrl);
                            NewsList.Add(strDocsrl); // 문서 번호를 리스트에 등록함
                        }
                        catch { }
                    }
                }
                catch
                {
                    // 뉴스 리스트 로드 에러
                    Dispatcher.Invoke(new Action(() =>
                    {
                        news_wbNews.NavigateToString("<meta charset=\"utf-8\"><p>여러분에게 알려드리고 싶은 소식이 있는데 어디있는지 잃어버린 것 같아요.</p><p>찾게되면 보여드릴게요.</p>");
                    }));

                    return;
                }

                try
                {
                    // 뉴스 내용 로드
                    foreach (string doc in NewsList)
                    {
                        try
                        {
                            string strNotice = Common.getStringFromWeb(Servers.Bell_Soft_Network.WEB_BSN_ROOT + "Notice/" + doc, Encoding.UTF8);
                            string[] strTemp = Common.stringSplit(strNotice, "<article");
                            // 제목 추가
                            strNotice = Common.stringSplit(strTemp[0], "<title>")[1];
                            strNotice = Common.stringSplit(strNotice, " - 공지사항 - 방울소프트네트워크</title>")[0];
                            Newsfeed.Append("<p><a href=\"" + Servers.Bell_Soft_Network.WEB_BSN_ROOT + "Notice/" + doc + "\" target=\"_blank\"><strong><span style=\"font-size: 12pt;\"><font color=\"#009e25\">" + "- " + strNotice + "</font></span></strong></a></p>");

                            // 내용 추가
                            strNotice = strTemp[1];
                            strTemp = Common.stringSplit(strNotice, "article>");
                            Newsfeed.Append("<article" + strTemp[0] + "article>");
                            if (doc != NewsList[NewsList.Count - 1])
                                Newsfeed.Append("<hr style=''border:5px; color:green; width:1024px;''>"); // 공지를 구분하는 라인 html 추가필요
                        }
                        catch { }
                    }

                    // 출력
                    strData = "<meta charset=\"utf-8\">" + Newsfeed.ToString();
                    Dispatcher.Invoke(new Action(() =>
                    {
                        news_wbNews.NavigateToString(strData);
                    }));
                }
                catch
                {
                    // 공지사항 로드 에러
                    Dispatcher.Invoke(new Action(() =>
                    {
                        news_wbNews.NavigateToString("<meta charset=\"utf-8\"><p>여러분에게 알려드리고 싶은 소식을 꺼내는 도중에 잃어버린 것 같아요.</p><p>찾게되면 보여드릴게요.</p>");
                    }));
                }

                Thread.Sleep(5 * 60 * 1000); // 5분 딜레이
            }
        }

    }
}
