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
        private string[] Version = null;
        #region 생성자

        /// <summary>
        /// MUID로 모드팩.xml을 로드하여 분석합니다.
        /// </summary>
        /// <param name="MUID">Modpack Unique Identifier. 모드팩 고유 식별자</param>
        public ModAnalysis(string MUID)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(BellLib.Properties.Resources.BellCraft8);
            XmlNodeList xnList = doc.SelectNodes("/" + MUID + "/Info");

            foreach (XmlNode xn in xnList)
            {
                Name = xn["Name"].InnerText;
                Recommended = xn["Recommended"].InnerText;
                Latest = xn["Latest"].InnerText;
                Base = xn["Base"].InnerText;
                Option = xn["Option"].InnerText;
                News = xn["News"].InnerText;
                Down = xn["Down"].InnerText;
            }

            string Temp = null;
            xnList = doc.SelectNodes("/" + MUID + "/Version/Ver");
            foreach (XmlNode xn in xnList)
            {
                Temp += xn.InnerText + Environment.NewLine;
                //Version[i] += xn.InnerText;
            }
            Version = Temp.Split('\n');
        }

        #endregion

        public string ModInfo()
        {
            string Temp = Name + Environment.NewLine;
            Temp += Recommended + Environment.NewLine;
            Temp += Latest + Environment.NewLine;
            Temp += Base + Environment.NewLine;
            Temp += Option + Environment.NewLine;
            Temp += News + Environment.NewLine;
            Temp += Down + Environment.NewLine;
            Temp += Environment.NewLine;
            foreach (string tmp in Version)
            {
                Temp += tmp + Environment.NewLine;
            }

            return Temp;
        }
    }
}
