﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using BellLib.Data;

namespace BellLib.Class
{
    public struct Info_Modpack
    {
        public string _Name, _Recommended, _Latest, _Base, _Option, _News, _Down;
        public string[] _Version;

        public Info_Modpack(string _Name = null, string _Recommended = null, string _Latest = null, string _Base = null,
                            string _Option = null, string _News = null, string _Down = null, string[] _Version = null)
        {
            this._Name = _Name;
            this._Recommended = _Recommended;
            this._Latest = _Latest;
            this._Base = _Base;
            this._Option = _Option;
            this._News = _News;
            this._Down = _Down;
            this._Version = _Version;
        }
    }

    public struct Info_Basepack
    {
        public string _Latest, _Recommended, _Down;
        public string[] _Version;

        public Info_Basepack(string _Latest = null, string _Recommended = null, string _Down = null, string[] _Version = null)
        {
            this._Latest = _Latest;
            this._Recommended = _Recommended;
            this._Down = _Down;
            this._Version = _Version;
        }
    }

    public struct Info_OptionPack
    {
        public string _Latest, _Recommended, _Down;
        public string[] _Version;

        public Info_OptionPack(string _Latest = null, string _Recommended = null, string _Down = null, string[] _Version = null)
        {
            this._Latest = _Latest;
            this._Recommended = _Recommended;
            this._Down = _Down;
            this._Version = _Version;
        }
    }

    /// <summary>
    /// 모드팩 정보 분석을 시행합니다.
    /// </summary>
    public class ModAnalysis
    {
        /* 임시로 생성한 xml 파일 웹 주소. 분석시 사용.
         * Base.TOTAL_WEB_URL + "BSL/Pack/PackList.xml" // 팩 리스트
         * Base.TOTAL_WEB_URL + "BSL/Pack/BellCraft8/BellCraft8.xml" // 팩 정보
         * Base.TOTAL_WEB_URL + "BSL/Pack/BellCraft8/Version/8.8.0.xml" // 팩 설치 정보
         * 
         * Base.TOTAL_WEB_URL + "BSL/Option/BCO_1.7.10/BCO_1.7.10.xml" // 옵션 정보
         * Base.TOTAL_WEB_URL + "BSL/Option/BCO_1.7.10/Version/1.0.0.xml" // 옵션 설치 정보
         * 
         * Base.TOTAL_WEB_URL + "BSL/Base/BCP_1.7.10/BCP_1.7.10.xml" // 베이스 정보
         * Base.TOTAL_WEB_URL + "BSL/Base/BCP_1.7.10/Version/1.0.0.xml" // 베이스 설치 정보
         */
        #region 필드

        // 사용하거든!
        #pragma warning disable 169
        private Info_Modpack InfoModpack = new Info_Modpack();
        private Info_Basepack InfoBasepack = new Info_Basepack();
        private Info_OptionPack InfoOptionpack = new Info_OptionPack();
        #pragma warning restore 169

        private string _MUID; // Modpack UID
        private string _BUID; // Basepack UID
        private string _OUID; // Optionpack UID
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
            doc.Load(Base.TOTAL_WEB_URL + "BSL/Pack/" + _MUID + "/" + _MUID + ".xml");
            //doc.LoadXml(BellLib.Properties.Resources.BellCraft8); // 추후 이 구문을 지우고 웹에서 MUID.xml 파일을 받아와서 로드함.

            xnList = doc.SelectNodes("/" + _MUID + "/Info");

            TypedReference _tr = __makeref(InfoModpack);

            foreach (XmlNode xn in xnList)
                // foreach문으로 ModAnalysis 클래스의 필드를 모두 구한다.
                foreach (var fieldInfo in typeof(Info_Modpack).GetFields(BindingFlags.Public | BindingFlags.Instance))
                    // 만약 필드 타입이 string이면..! (string[]도 있으니까..)
                    if (fieldInfo.FieldType == typeof(string))
                        // _tr의 필드값을 xn[필드이름에서 _제거].InnerText로 설정한다.]
                        fieldInfo.SetValueDirect(_tr, xn[fieldInfo.Name.Replace("_", String.Empty)].InnerText);

            xnList = doc.SelectNodes("/" + _MUID + "/Version/Ver");

            StringBuilder str = new StringBuilder();
            foreach (XmlNode xn in xnList)
            {
                str.AppendLine(xn.InnerText);
            }
            InfoModpack._Version = str.ToString().Split('\n');

            // [CLASS] BCP(Base.xml) 로드 및 분석 후 변수에 대입 (Info 노드, Version 노드)
            // [CLASS] BCO(Option.xml) 로드 및 분석 후 변수에 대입 (Info 노드, Version 노드)
            // [BSL]   클라이언트 설정파일 로드 및 분석 (선택된 모드팩 MUID, 설치된 버전, 유지 희망 버전 (권장 or 최신 or 직접 선택)
            //            분석된 MUID를 바탕으로 서버와 클라 버전 대조
            // [CLASS] 클라이언트 설정파일이 로드되지 않았을경우(미설치), ModPack {버전}.xml 로드 및 분석 후 설치 진행

            _Parsed = true;
        }

        /// <summary>
        /// 모드팩 정보를 반환합니다. 개발 중 테스트용 (반환값 확인)
        /// </summary>
        /// <returns>모드팩 정보</returns>
        public string GetModInfo()
        {
            if (!_Parsed)
                return "Not Parsed";

            StringBuilder str = new StringBuilder();

            Type _ModAnalysis = typeof(Info_Modpack);
            foreach (var field in _ModAnalysis.GetFields(BindingFlags.Public | BindingFlags.Instance))
                if (field.FieldType == typeof(string))
                    str.AppendLine((string)field.GetValue(this.InfoModpack));

            str.AppendLine();

            // Version Info
            foreach (string v in InfoModpack._Version)
                str.AppendLine(v);

            return str.ToString();
        }

        #endregion
    }
}
