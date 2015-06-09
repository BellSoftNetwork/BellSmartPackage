using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using BellLib.Data;

namespace BellLib.Class
{
    /// <summary>
    /// 모드팩 정보 분석을 시행합니다.
    /// </summary>
    public class ModAnalysis
    {
        #region 필드

        // 사용하거든!
        #pragma warning disable 169
        private string _Name, _Recommended, _Latest, _Base, _Option, _News, _Down;
        #pragma warning restore 169

        private string[] _Version;
        private string _MUID;
        private bool _Parsed = false;

        #endregion

        #region 생성자

        /// <summary>
        /// 빈 ModAnalysis 인스턴스를 만듭니다. SelectModpack(string MUID) 메서드를 사용하십시오.
        /// </summary>
        public ModAnalysis()
        {

        }

        /// <summary>
        /// MUID로 모드팩.xml을 로드하여 분석합니다.
        /// </summary>
        /// <param name="MUID">Modpack Unique Identifier. 모드팩 고유 식별자</param>
        public ModAnalysis(string MUID, bool parse = true)
        {
            _MUID = MUID;
            if (parse)
                ParseModInfo();
        }

        #endregion

        #region 메서드

        /// <summary>
        /// 파싱할 모드팩을 선택합니다.
        /// </summary>
        /// <param name="MUID"></param>
        public void SelectModpack(string MUID)
        {
            _MUID = MUID;
            _Parsed = false;
        }
        
        /// <summary>
        /// 모드팩 정보를 파싱합니다.
        /// </summary>
        private void ParseModInfo()
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            doc.Load(Base.TOTAL_WEB_URL + "BSL/Pack/BellCraft8.xml");
            //doc.LoadXml(BellLib.Properties.Resources.BellCraft8); // 추후 이 구문을 지우고 웹에서 MUID.xml 파일을 받아와서 로드함.

            xnList = doc.SelectNodes("/" + _MUID + "/Info");

            foreach (XmlNode xn in xnList)
                // foreach문으로 ModAnalysis 클래스의 필드를 모두 구한다.
                foreach (var field in typeof(ModAnalysis).GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                    // 만약 필드 타입이 string이면..! (string[]도 있으니까..)
                    if (field.FieldType == typeof(string))
                        // this(이 클래스)의 필드값을 xn[필드이름에서 _제거].InnerText로 설정한다.]
                        field.SetValue(this, xn[field.Name.Replace("_", String.Empty)].InnerText);

            xnList = doc.SelectNodes("/" + _MUID + "/Version/Ver");

            StringBuilder str = new StringBuilder();
            foreach (XmlNode xn in xnList)
            {
                str.AppendLine(xn.InnerText);
            }
            _Version = str.ToString().Split('\n');

            // [CLASS] BCP(Base.xml) 로드 및 분석 후 변수에 대입 (Info 노드, Version 노드)
            // [CLASS] BCO(Option.xml) 로드 및 분석 후 변수에 대입 (Info 노드, Version 노드)
            // [BSL]   클라이언트 설정파일 로드 및 분석 (선택된 모드팩 MUID, 설치된 버전, 유지 희망 버전 (권장 or 최신 or 직접 선택)
            //            분석된 MUID를 바탕으로 서버와 클라 버전 대조
            // [CLASS] 클라이언트 설정파일이 로드되지 않았을경우(미설치), ModPack {버전}.xml 로드 및 분석 후 설치 진행

            _Parsed = true;
        }

        /// <summary>
        /// 모드팩 정보를 반환합니다.
        /// </summary>
        /// <returns>모드팩 정보</returns>
        public string GetModInfo()
        {
            if (!_Parsed)
                return "Not Parsed";

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

        #endregion
    }
}
