using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using BellLib.Data;

#pragma warning disable 649

namespace BellLib.Class
{
    /// <summary>
    /// 모드팩 정보 분석을 시행합니다.
    /// </summary>
    public class ModAnalysis
    {
        private string _Name, _Recommended, _Latest, _Base, _Option, _News, _Down;
        private string[] _Version;

        #region 생성자

        /// <summary>
        /// MUID로 모드팩.xml을 로드하여 분석합니다.
        /// </summary>
        /// <param name="MUID">Modpack Unique Identifier. 모드팩 고유 식별자</param>
        public ModAnalysis(string MUID)
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            doc.LoadXml(BellLib.Properties.Resources.BellCraft8);
            
            xnList = doc.SelectNodes("/" + MUID + "/Info");

            foreach (XmlNode xn in xnList)
                // foreach문으로 ModAnalysis 클래스의 필드를 모두 구한다.
                foreach (var field in typeof(ModAnalysis).GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                    // 만약 필드 타입이 string이면..! (string[]도 있으니까..)
                    if (field.FieldType == typeof(string))
                        // this(이 클래스)의 필드값을 xn[필드이름에서 _제거].InnerText로 설정한다.]
                        field.SetValue(this, xn[field.Name.Replace("_", String.Empty)].InnerText);

            xnList = doc.SelectNodes("/" + MUID + "/Version/Ver");

            StringBuilder str = new StringBuilder();
            foreach (XmlNode xn in xnList)
            {
                str.AppendLine(xn.InnerText);
            }
            _Version = str.ToString().Split('\n');
        }

        #endregion

        /// <summary>
        /// 모드팩 정보를 반환합니다.
        /// </summary>
        /// <returns>모드팩 정보</returns>
        public string GetModInfo()
        {
            StringBuilder str = new StringBuilder();

            Type _ModAnalysis = typeof(ModAnalysis);
            foreach (var field in _ModAnalysis.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                if (field.FieldType == typeof(string))
                    str.AppendLine((string)field.GetValue(this));

            str.AppendLine();

            // Version Info
            foreach (string v in _Version)
                str.AppendLine(v);

            return str.ToString();
        }
    }
}
