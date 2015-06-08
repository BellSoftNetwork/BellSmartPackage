using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BellLib.Data;

namespace BellLib.Class
{
    /// <summary>
    /// 모드팩 정보 분석을 시행합니다.
    /// </summary>
    public class ModAnalysis
    {

        private string Name, Recommended, Latest, Base, Option, News, Down;
        private string[] Version;
        #region 생성자

        /// <summary>
        /// MUID로 모드팩.xml을 로드하여 분석합니다.
        /// </summary>
        /// <param name="MUID">Modpack Unique Identifier. 모드팩 고유 식별자</param>
        public ModAnalysis(string MUID)
        {
            XmlDocument XD = new XmlDocument();
            XD.LoadXml(BellLib.Properties.Resources.BellCraft8);
            XmlNode root = XD;

            foreach (XmlNode no in root.ChildNodes)
            {
                if (no.Name == MUID) // 이름이 모드팩 이름과 같을시
                {
                    foreach (XmlNode child in no) // 루트 노드의 자식 노드 로드
                    {
                        if (child.Name == "Info")
                        {
                            foreach (XmlNode data in child)
                            {
                                switch (data.Name)
                                {
                                    case "Name":
                                        Name = data.InnerText;
                                        break;

                                    case "Recommended":
                                        Recommended = data.InnerText;
                                        break;

                                    case "Latest":
                                        Latest = data.InnerText;
                                        break;

                                    case "Base":
                                        Base = data.InnerText;
                                        break;

                                    case "Option":
                                        Option = data.InnerText;
                                        break;

                                    case "News":
                                        News = data.InnerText;
                                        break;

                                    case "Down":
                                        Down = data.InnerText;
                                        break;
                                }
                            }
                        }

                        if (child.Name == "Version")
                        {
                            int i = 0;
                            foreach (XmlNode data in child)
                            {
                                Version[i] = data.InnerText; // 배열에 값이 안들어감 수정대기
                                i++;
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
