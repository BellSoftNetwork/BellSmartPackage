using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BellLib.Class.BSN
{
    /// <summary>
    /// Bell Smart Launcher 2 제어 관련
    /// </summary>
    public class BSN_BSL2
    {
        /// <summary>
        /// 팩 간략 정보 클래스
        /// </summary>
        public class PackInfo
        {
            /*
             * [설치 리스트]
                썸네일
                이름
                제작자 목록
                팩 설명
                다운로드수
                마지막 수정일
                게임 버전
                설치 & 플레이 버튼

                [라이브러리 리스트]
                썸네일
                이름
                제작자 목록
                버전
                플레이 시간
                마지막 플레이 날짜
                업데이트 버튼
                플레이 버튼
             */
            /*private string name;
            private List<string> authors;
            private StringBuilder description;
            private int downloads;
            private DateTime updated;
            private string game_version;
            private string thumbnail_url;
            private bool installed;
            private string iap_button_content;*/


            public PackInfo(string name, List<string> authors, string description, ulong downloads, DateTime updated, string game_version, string thumbnail_url = @"M:\Programming\Photo Shop\Projects\Objects\빨간민무늬방울.png", bool installed = false)
            {
                this.Name = name;
                this.Authors = authors;
                this.Description = description;
                this.Downloads = downloads;
                this.Updated = updated;
                this.Game_Version = game_version;
                this.Thumbnail_URL = thumbnail_url;
                this.Installed = installed;
            }

            public PackInfo(string name, string author, string description, ulong downloads, DateTime updated, string game_version, string thumbnail_url = @"M:\Programming\Photo Shop\Projects\Objects\빨간민무늬방울.png", bool installed = false)
            {
                List<string> list = new List<string>();
                list.Add(author);

                this.Name = name;
                this.Authors = list;
                this.Description = description;
                this.Downloads = downloads;
                this.Updated = updated;
                this.Game_Version = game_version;
                this.Thumbnail_URL = thumbnail_url;
                this.Installed = installed;
            }

            /*public PackInfo(string name, string author, string description, string downloads, string updated, string game_version, string thumbnail_uri, string iap_button)
            {
                this.name = name;
                this.author = author;
                this.description = description;
                this.downloads = downloads;
                this.updated = updated;
                this.game_version = game_version;
                this.thumbnail_uri = thumbnail_uri;
                this.iap_button = iap_button;
            }*/

            public string Name { get; set; }
            public List<string> Authors { get; set; }
            public string Description { get; set; }
            public ulong Downloads { get; set; }
            public DateTime Updated { get; set; }
            public string Game_Version { get; set; }
            public string Thumbnail_URL { get; set; }
            public bool Installed { get; set; }

            public string Authors_Content
            {
                get
                {
                    StringBuilder sb = new StringBuilder(100);

                    sb.Append("by ");
                    for (int i = 0; i < Authors.Count; i++)
                    {
                        sb.Append(Authors[i]);

                        if (i < Authors.Count - 1)
                            sb.Append(", ");
                    }

                    return sb.ToString();
                }
            }
            public string Downloads_Content
            {
                get
                {
                    NumberManager nm = new NumberManager();
                    NumberManager.NumberUnit nu = nm.CalculateNumber(this.Downloads);

                    return "다운로드: " + nu.number + nu.unit;
                }
            }
            public string Updated_Content
            {
                get
                {
                    return "마지막 수정일: " + this.Updated;
                }
            }
            public string Game_Version_Content
            {
                get
                {
                    return "게임 버전: " + this.Game_Version;
                }
            }
            public string IAP_Button_Content
            {
                get
                {
                    if (this.Installed)
                        return "플레이";
                    else
                        return "설치";
                }
            }
            public string Update_Button_Content
            {
                get
                {
                    return "업데이트";
                }
            }
            public string Play_Button_Content
            {
                get
                {
                    return "플레이";
                }
            }

            

            /*
            public string name { get; set; }
            public List<string> authors { get; set; }
            public string author { get; set; }
            public string description { get; set; }
            public string downloads { get; set; }
            public string updated { get; set; }
            public string game_version { get; set; }
            public string thumbnail_uri { get; set; }
            public string iap_button { get; set; }*/
        }

    }
}
