using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.IO;
using BellLib.Data;

namespace BellLib.Class
{
    public struct Info_Modpack
    {
        public string _Name, _Recommended, _Latest, _Base, _Option, _News, _Down;
        public string[] _Version, _List;

        /*public Info_Modpack(string _Name = null, string _Recommended = null, string _Latest = null, string _Base = null,
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
        }*/
    }

    public struct Info_Basepack
    {
        public string _Latest, _Recommended, _Down;
        public string[] _Version, _List;

        /*public Info_Basepack(string _Latest = null, string _Recommended = null, string _Down = null, string[] _Version = null)
        {
            this._Latest = _Latest;
            this._Recommended = _Recommended;
            this._Down = _Down;
            this._Version = _Version;
        }*/
    }

    public struct Info_Optionpack
    {
        public string _Latest, _Recommended, _Down;
        public string[] _Version, _List;

        /*public Info_Optionpack(string _Latest = null, string _Recommended = null, string _Down = null, string[] _Version = null)
        {
            this._Latest = _Latest;
            this._Recommended = _Recommended;
            this._Down = _Down;
            this._Version = _Version;
        }*/
    }

    public struct Info_ClientMod
    {
        public string _ModVersion, _BaseVersion, _OptionVersion;

        public Info_ClientMod(string _ModVersion = null, string _BaseVersion = null, string _OptionVersion = null)
        {
            this._ModVersion = _ModVersion;
            this._BaseVersion = _BaseVersion;
            this._OptionVersion = _OptionVersion;
        }
    }

    public struct Ver_Modpack
    {
        public string Base, Option;
        public string[] Directory, Hash;
    }

    public struct Ver_Basepack
    {
        public string[] Directory, Hash;
    }

    public struct Ver_Optionpack
    {
        public string[] Option, Directory, Hash;
    }

    /// <summary>
    /// 클라이언트 전용
    /// 웹 XML 데이터를 바탕으로 모드팩, 베이스팩, 옵션팩 정보 분석을 시행합니다.
    /// </summary>
    public class ModAnalysisRead
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
        private Info_Optionpack InfoOptionpack = new Info_Optionpack();
        private Info_ClientMod InfoClientMod = new Info_ClientMod();
        private Ver_Modpack VerModpack = new Ver_Modpack();
        private Ver_Basepack VerBasepack = new Ver_Basepack();
        private Ver_Optionpack VerOptionpack = new Ver_Optionpack();
        #pragma warning restore 169

        private string _MUID; // Modpack UID
        private string _BUID; // Basepack UID
        private string _OUID; // Optionpack UID
        private bool _Parsed = false;

        public enum PackType
        {
            Mod,
            Base,
            Option
        }

        #endregion

        #region 생성자

        /// <summary>
        /// 빈 ModAnalysis 인스턴스를 만듭니다. SelectModpack(string MUID) 메서드를 사용하십시오.
        /// </summary>
        public ModAnalysisRead()
        {

        }

        /// <summary>
        /// UID로 지정팩.xml을 로드 및 분석합니다.
        /// </summary>
        /// <param name="UID">Unique Identifier. 고유 식별자</param>
        public ModAnalysisRead(PackType pt, string UID, bool parse = true)
        {
            if (parse) // 공통 로드 부분
            {
                if (!LoadModList()) return;
                if (!LoadBaseList()) return;
                if (!LoadOptionList()) return;
                LoadClient();
            }

            switch (pt)
            {
                case PackType.Mod:
                    _MUID = UID;
                    if (!parse)
                        break;
                    if (!ParseModInfo()) return;
                    if (!ParseBaseInfo()) return;
                    if (!ParseOptionInfo()) return;
                    break;

                case PackType.Base:
                    _BUID = UID;
                    if (!parse)
                        break;
                    if (!ParseBaseInfo()) return;

                    break;

                case PackType.Option:
                    _OUID = UID;
                    if (!parse)
                        break;
                    if (!ParseOptionInfo()) return;

                    break;
            }

            _Parsed = true;
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
        /// ModPack.XML을 로드 및 분석합니다.
        /// </summary>
        private bool ParseModInfo()
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            try
            {
                doc.Load(Base.TOTAL_WEB_URL + "BSL/Pack/" + _MUID + "/" + _MUID + ".xml");
            }
            catch { return false; }
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
            List<string> lst = new List<string>();
            foreach (string tmp in str.ToString().Split('\n'))
                if (tmp != "")
                    lst.Add(tmp.Replace("\r", string.Empty));
            InfoModpack._Version = lst.ToArray();


            _BUID = InfoModpack._Base;
            _OUID = InfoModpack._Option;

            return true;
        }

        /// <summary>
        /// Base.XML을 로드 및 분석합니다.
        /// </summary>
        private bool ParseBaseInfo()
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            try {
            doc.Load(Base.TOTAL_WEB_URL + "BSL/Base/" + _BUID + "/" + _BUID + ".xml");
            } catch { return false; }
            xnList = doc.SelectNodes("/" + _BUID + "/Info");

            TypedReference _tr = __makeref(InfoBasepack);

            foreach (XmlNode xn in xnList)
                // foreach문으로 ModAnalysis 클래스의 필드를 모두 구한다.
                foreach (var fieldInfo in typeof(Info_Basepack).GetFields(BindingFlags.Public | BindingFlags.Instance))
                    // 만약 필드 타입이 string이면..! (string[]도 있으니까..)
                    if (fieldInfo.FieldType == typeof(string))
                        // _tr의 필드값을 xn[필드이름에서 _제거].InnerText로 설정한다.]
                        fieldInfo.SetValueDirect(_tr, xn[fieldInfo.Name.Replace("_", String.Empty)].InnerText);

            xnList = doc.SelectNodes("/" + _BUID + "/Version/Ver");

            StringBuilder str = new StringBuilder();
            foreach (XmlNode xn in xnList)
            {
                str.AppendLine(xn.InnerText);
            }
            List<string> lst = new List<string>();
            foreach (string tmp in str.ToString().Split('\n'))
                if (tmp != "")
                    lst.Add(tmp.Replace("\r", string.Empty));
            InfoBasepack._Version = lst.ToArray();

            return true;
        }

        /// <summary>
        /// 옵션.XML을 로드 및 분석합니다.
        /// </summary>
        private bool ParseOptionInfo()
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            try {
            doc.Load(Base.TOTAL_WEB_URL + "BSL/Option/" + _OUID + "/" + _OUID + ".xml");
            } catch { return false; }
            xnList = doc.SelectNodes("/" + _OUID + "/Info");

            TypedReference _tr = __makeref(InfoOptionpack);

            foreach (XmlNode xn in xnList)
                // foreach문으로 ModAnalysis 클래스의 필드를 모두 구한다.
                foreach (var fieldInfo in typeof(Info_Optionpack).GetFields(BindingFlags.Public | BindingFlags.Instance))
                    // 만약 필드 타입이 string이면..! (string[]도 있으니까..)
                    if (fieldInfo.FieldType == typeof(string))
                        // _tr의 필드값을 xn[필드이름에서 _제거].InnerText로 설정한다.]
                        fieldInfo.SetValueDirect(_tr, xn[fieldInfo.Name.Replace("_", String.Empty)].InnerText);

            xnList = doc.SelectNodes("/" + _OUID + "/Version/Ver");

            StringBuilder str = new StringBuilder();
            foreach (XmlNode xn in xnList)
            {
                str.AppendLine(xn.InnerText);
            }
            List<string> lst = new List<string>();
            foreach (string tmp in str.ToString().Split('\n'))
                if (tmp != "")
                    lst.Add(tmp.Replace("\r", string.Empty));
            InfoOptionpack._Version = lst.ToArray();

            return true;
        }

        private bool LoadModList()
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            try
            {
                doc.Load(Base.TOTAL_WEB_URL + "BSL/Pack/PackList.xml");
            }
            catch { return false; }
            xnList = doc.SelectNodes("/List/Pack");

            StringBuilder str = new StringBuilder();
            foreach (XmlNode xn in xnList)
            {
                str.AppendLine(xn.InnerText);
            }
            List<string> lst = new List<string>();
            foreach (string tmp in str.ToString().Split('\n'))
                if (tmp != "")
                    lst.Add(tmp.Replace("\r", string.Empty));
            InfoModpack._List = lst.ToArray();

            return true;
        }

        private bool LoadBaseList()
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            try
            {
                doc.Load(Base.TOTAL_WEB_URL + "BSL/Base/PackList.xml");
            }
            catch { return false; }
            xnList = doc.SelectNodes("/List/Pack");

            StringBuilder str = new StringBuilder();
            foreach (XmlNode xn in xnList)
            {
                str.AppendLine(xn.InnerText);
            }
            List<string> lst = new List<string>();
            foreach (string tmp in str.ToString().Split('\n'))
                if (tmp != "")
                    lst.Add(tmp.Replace("\r", string.Empty));
            InfoBasepack._List = lst.ToArray();

            return true;
        }

        private bool LoadOptionList()
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            try
            {
                doc.Load(Base.TOTAL_WEB_URL + "BSL/Option/PackList.xml");
            }
            catch { return false; }
            xnList = doc.SelectNodes("/List/Pack");

            StringBuilder str = new StringBuilder();
            foreach (XmlNode xn in xnList)
            {
                str.AppendLine(xn.InnerText);
            }
            List<string> lst = new List<string>();
            foreach (string tmp in str.ToString().Split('\n'))
                if (tmp != "")
                    lst.Add(tmp.Replace("\r", string.Empty));
            InfoOptionpack._List = lst.ToArray();

            return true;
        }

        /// <summary>
        /// 클라이언트 데이터를 로드합니다.
        /// </summary>
        private bool LoadClient()
        {
            string clientPath = User.BSL_Root + "ModPack\\" + _MUID + "\\" + _MUID + ".bsn";
            if (File.Exists(clientPath))
            {
                string Temp = File.ReadAllText(clientPath);
                Protection pt = new Protection();
                Temp = pt.Base64(Temp, Protection.ProtectionType.PROTECTION_DECODE, 2); // 클라이언트 정보파일의 데이터를 2회 복호화
                string[] Data = Temp.Split('\n');

                TypedReference _tr = __makeref(InfoClientMod);
                int i = 0;

                // foreach문으로 ModAnalysis 클래스의 필드를 모두 구한다.
                foreach (var fieldInfo in typeof(Info_ClientMod).GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    string value = Data[i].Split('=')[1].Replace("_", String.Empty);
                    fieldInfo.SetValueDirect(_tr, value);
                    i++;
                }

                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// 지정한 버전의 모드팩 설치데이터를 로드 및 분석합니다.
        /// </summary>
        /// <param name="Version">모드팩 버전</param>
        public void LoadMod(string Version)
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            StringBuilder str = new StringBuilder();
            try
            {
                doc.Load(Base.TOTAL_WEB_URL + "BSL/Pack/" + _MUID + "/Version/" + Version + ".xml");
            }
            catch { return; }
            xnList = doc.SelectNodes("/" + _MUID + "/Version/Base");
            VerModpack.Base = xnList.Item(0).InnerText;

            xnList = doc.SelectNodes("/" + _MUID + "/Version/Option");
            VerModpack.Option = xnList.Item(0).InnerText;


            xnList = doc.SelectNodes("/" + _MUID + "/Directory/Dir");

            foreach (XmlNode xn in xnList)
            {
                str.AppendLine(xn.InnerText);
            }
            List<string> lst = new List<string>();
            foreach (string tmp in str.ToString().Split('\n'))
                if (tmp != "")
                    lst.Add(tmp.Replace("\r", string.Empty));
            VerModpack.Directory = lst.ToArray();
        }
                
        /// <summary>
        /// 지정한 버전의 베이스팩 설치데이터를 로드 및 분석합니다.
        /// </summary>
        /// <param name="Version">베이스팩 버전</param>
        public void LoadBase(string Version)
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            StringBuilder str = new StringBuilder();
            List<string> lst = new List<string>();
            try
            {
                doc.Load(Base.TOTAL_WEB_URL + "BSL/Base/" + _BUID + "/Version/" + Version + ".xml"); // 베이스 버전 데이터 로드
            }
            catch { return; }
            
            xnList = doc.SelectNodes("/" + _BUID + "/Directory/Dir"); // 베이스팩 필요한 디렉토리
            foreach (XmlNode xn in xnList)
            {
                str.AppendLine(xn.InnerText);
            }

            foreach (string tmp in str.ToString().Split('\n'))
                if (tmp != "")
                    lst.Add(tmp.Replace("\r", string.Empty));
            VerBasepack.Directory = lst.ToArray(); // 배열로 올림

            str.Clear(); // 위에서 한번 썼으면 초기화를 해줘야지!
            lst.Clear(); // 이것도!
            xnList = doc.SelectNodes("/" + _BUID + "/Hash/File"); // 베이스팩 필요한 파일
            foreach (XmlNode xn in xnList)
            {
                str.AppendLine(xn.Attributes.GetNamedItem("Loc").InnerText + "|" + xn.InnerText);
            }
            foreach (string tmp in str.ToString().Split('\n'))
                if (tmp != "")
                    lst.Add(tmp.Replace("\r", string.Empty));
            VerBasepack.Hash = lst.ToArray(); // 배열로 올림
        }

        /// <summary>
        /// 지정한 버전의 옵션팩 설치데이터를 로드 및 분석합니다.
        /// </summary>
        /// <param name="Version">옵션팩 버전</param>
        public void LoadOption(string Version)
        {

        }

        /// <summary>
        /// 데이터가 정상적으로 파싱되어 값을 이용할 수 있는지 여부를 반환합니다.
        /// </summary>
        /// <returns>인스턴스 이용 가능 여부</returns>
        public bool Availability()
        {
            return _Parsed;
        }

        /// <summary>
        /// 선택한 팩 타입의 버전 정보를 배열로 반환합니다.
        /// </summary>
        /// <param name="pt">팩 타입</param>
        /// <returns>버전 정보</returns>
        public string[] GetVersion(PackType pt)
        {
            if (!_Parsed)
                return null;

            switch (pt)
            {
                case PackType.Mod:
                    return InfoModpack._Version;

                case PackType.Base:
                    return InfoBasepack._Version;

                case PackType.Option:
                    return InfoOptionpack._Version;
            }
            return null;
        }

        /// <summary>
        /// 선택한 팩 타입의 모든 리스트를 배열로 반환합니다.
        /// </summary>
        /// <param name="pt">팩 타입</param>
        /// <returns>팩 리스트</returns>
        public string[] GetList(PackType pt)
        {
            if (!_Parsed)
                return null;

            switch (pt)
            {
                case PackType.Mod:
                    return InfoModpack._List;

                case PackType.Base:
                    return InfoBasepack._List;

                case PackType.Option:
                    return InfoOptionpack._List;
            }
            return null;
        }


        public string GetInfo(PackType pt, string Name)
        {
            Name = "_" + Name;
            try
            {
                switch (pt)
                {
                    case PackType.Mod:
                        return (string)typeof(Info_Modpack).GetField(Name).GetValue(this.InfoModpack);

                    case PackType.Base:
                        return (string)typeof(Info_Basepack).GetField(Name).GetValue(this.InfoBasepack);

                    case PackType.Option:
                        return (string)typeof(Info_Optionpack).GetField(Name).GetValue(this.InfoOptionpack);
                }
            } catch { }
            return null;
        }

        public string GetInstallInfo(PackType pt, string Name)
        {
            try
            {
                switch (pt)
                {
                    case PackType.Mod:
                        return (string)typeof(Ver_Modpack).GetField(Name).GetValue(this.VerModpack);

                    case PackType.Base:
                        return (string)typeof(Ver_Basepack).GetField(Name).GetValue(this.VerBasepack);

                    case PackType.Option:
                        return (string)typeof(Ver_Optionpack).GetField(Name).GetValue(this.VerOptionpack);
                }
            }
            catch { }
            return null;
        }

        public string[] GetInstallData(PackType pt, string Name)
        {
            switch (pt)
            {
                case PackType.Mod:
                    return (string[])typeof(Ver_Modpack).GetField(Name).GetValue(this.VerModpack);

                case PackType.Base:
                    return (string[])typeof(Ver_Basepack).GetField(Name).GetValue(this.VerBasepack);

                case PackType.Option:
                    return (string[])typeof(Ver_Optionpack).GetField(Name).GetValue(this.VerOptionpack);
            }
            return null;
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

            // ModPack Info
            str.AppendLine("*** ModPack Info ***");
            Type _ModAnalysis = typeof(Info_Modpack);
            foreach (var field in _ModAnalysis.GetFields(BindingFlags.Public | BindingFlags.Instance))
                if (field.FieldType == typeof(string))
                    str.AppendLine((string)field.GetValue(this.InfoModpack));

            str.AppendLine();

            // ModPack Version Info
            str.AppendLine("*** ModPack Version Info ***");
            foreach (string v in InfoModpack._Version)
                str.AppendLine(v);

            str.AppendLine();

            // BasePack Info
            str.AppendLine("*** BasePack Info ***");
            Type _BaseAnalysis = typeof(Info_Basepack);
            foreach (var field in _BaseAnalysis.GetFields(BindingFlags.Public | BindingFlags.Instance))
                if (field.FieldType == typeof(string))
                    str.AppendLine((string)field.GetValue(this.InfoBasepack));

            str.AppendLine();

            // BasePack Version Info
            str.AppendLine("*** BasePack Version Info ***");
            foreach (string v in InfoBasepack._Version)
                str.AppendLine(v);

            str.AppendLine();

            // OptionPack Info
            str.AppendLine("*** OptionPack Info ***");
            Type _OptionAnalysis = typeof(Info_Optionpack);
            foreach (var field in _OptionAnalysis.GetFields(BindingFlags.Public | BindingFlags.Instance))
                if (field.FieldType == typeof(string))
                    str.AppendLine((string)field.GetValue(this.InfoOptionpack));

            str.AppendLine();

            // OptionPack Version Info
            str.AppendLine("*** OptionPack Version Info ***");
            foreach (string v in InfoOptionpack._Version)
                str.AppendLine(v);

            str.AppendLine();

            // ClientMod Info
            str.AppendLine("*** ClientMod Info ***");
            Type _ClientAnalysis = typeof(Info_ClientMod);
            foreach (var field in _ClientAnalysis.GetFields(BindingFlags.Public | BindingFlags.Instance))
                if (field.FieldType == typeof(string))
                    str.AppendLine((string)field.GetValue(this.InfoClientMod));

            return str.ToString();
        }

        #endregion
    }

    public struct Data_Mod
    {
        public string Name, Recommended, Latest, Base, Option, News, Down;
        public string[] Version;
    }
    public struct Data_Base
    {
        public string Recommended, Latest, Down;
        public string[] Version;
    }
    public struct Data_Option
    {
        public string Recommended, Latest, Down;
        public string[] Version;
    }
    /// <summary>
    /// 서버 전용
    /// 모드팩, 베이스팩, 옵션팩 정보 분석 및 XML 작성을 시행합니다.
    /// </summary>
    public class ModAnalysisWrite
    {
        public enum Type
        {
            ModPack,
            BasePack,
            OptionPack
        }
        private string UID;
        private Type Pack;
        private Data_Mod DM = new Data_Mod();
        private Data_Base DB = new Data_Base();
        private Data_Option DO = new Data_Option();

        #region 생성자

        public ModAnalysisWrite()
        {
            // 빈 생성자
        }
        /// <summary>
        /// 모드분석 후 XML 작성합니다.
        /// </summary>
        public ModAnalysisWrite(Type Typ, string UID, string Name, string Latest, string Recommended, string Base, string Option, string News, string Down, string[] Version)
        {
            this.Pack = Typ;
            this.UID = UID;
            switch (Typ)
            {
                case Type.ModPack:
                    DM.Name = Name;
                    DM.Recommended = Recommended;
                    DM.Latest = Latest;
                    DM.Base = Base;
                    DM.Option = Option;
                    DM.News = News;
                    DM.Down = Down;
                    DM.Version = Version;
                    break;
            }
        }
        public ModAnalysisWrite(Type Typ, string UID, string Latest, string Recommended, string Down, string[] Version)
        {
            this.Pack = Typ;
            this.UID = UID;
            switch (Typ)
            {
                case Type.BasePack:
                    DB.Recommended = Recommended;
                    DB.Latest = Latest;
                    DB.Down = Down;
                    DB.Version = Version;
                    break;

                case Type.OptionPack:
                    DO.Recommended = Recommended;
                    DO.Latest = Latest;
                    DO.Down = Down;
                    DO.Version = Version;
                    break;
            }
        }
        #endregion


        public void WriteXML()
        {
            XmlTextWriter XTW;

            switch (Pack)
            {
                case Type.ModPack:
                    XTW = new XmlTextWriter(User.BSN_Path + UID + ".xml", Encoding.UTF8);
                    XTW.Formatting = Formatting.Indented; // 파일 기록시 자동으로 들여씀
                    XTW.WriteStartDocument(); // XML 문서 시작
                    XTW.WriteStartElement(UID); // UID 엘리먼트 시작

                    XTW.WriteStartElement("Info"); // Info 엘리먼트 시작
                    XTW.WriteElementString("Name", DM.Name);
                    XTW.WriteElementString("Latest", DM.Latest);
                    XTW.WriteElementString("Recommended", DM.Recommended);
                    XTW.WriteElementString("Base", DM.Base);
                    XTW.WriteElementString("Option", DM.Option);
                    XTW.WriteElementString("News", DM.News);
                    XTW.WriteElementString("Down", DM.Down);
                    XTW.WriteEndElement(); // Info 엘리먼트 닫음

                    XTW.WriteStartElement("Version");
                    foreach (string tmp in DM.Version)
                        XTW.WriteElementString("Ver", tmp);

                    XTW.WriteEndElement();
                    XTW.WriteEndDocument();
                    XTW.Flush();
                    XTW.Close();
                    break;

                case Type.BasePack:
                    XTW = new XmlTextWriter(User.BSN_Path + UID + ".xml", Encoding.UTF8);
                    XTW.Formatting = Formatting.Indented; // 파일 기록시 자동으로 들여씀
                    XTW.WriteStartDocument(); // XML 문서 시작
                    XTW.WriteStartElement(UID); // UID 엘리먼트 시작

                    XTW.WriteStartElement("Info"); // Info 엘리먼트 시작
                    XTW.WriteElementString("Latest", DB.Latest);
                    XTW.WriteElementString("Recommended", DB.Recommended);
                    XTW.WriteElementString("Down", DB.Down);
                    XTW.WriteEndElement(); // Info 엘리먼트 닫음

                    XTW.WriteStartElement("Version");
                    foreach (string tmp in DB.Version)
                        XTW.WriteElementString("Ver", tmp);

                    XTW.WriteEndElement();
                    XTW.WriteEndDocument();
                    XTW.Flush();
                    XTW.Close();
                    break;

                case Type.OptionPack:
                    XTW = new XmlTextWriter(User.BSN_Path + UID + ".xml", Encoding.UTF8);
                    XTW.Formatting = Formatting.Indented; // 파일 기록시 자동으로 들여씀
                    XTW.WriteStartDocument(); // XML 문서 시작
                    XTW.WriteStartElement(UID); // UID 엘리먼트 시작

                    XTW.WriteStartElement("Info"); // Info 엘리먼트 시작
                    XTW.WriteElementString("Latest", DO.Latest);
                    XTW.WriteElementString("Recommended", DO.Recommended);
                    XTW.WriteElementString("Down", DO.Down);
                    XTW.WriteEndElement(); // Info 엘리먼트 닫음

                    XTW.WriteStartElement("Version");
                    foreach (string tmp in DO.Version)
                        XTW.WriteElementString("Ver", tmp);

                    XTW.WriteEndElement();
                    XTW.WriteEndDocument();
                    XTW.Flush();
                    XTW.Close();
                    break;
            }
        }

        /// <summary>
        /// 모드팩 버전 설치정보를 기록합니다.
        /// </summary>
        /// <param name="RequireBase">필요 베이스팩 버전</param>
        /// <param name="RequireOption">필요 옵션팩 버전</param>
        /// <param name="Directory">필요 디렉토리 리스트</param>
        /// <param name="Hash">파일 리스트 ('파일명|해시' 식으로 값 대입)</param>
        public void WriteVersionXML(string RequireBase, string RequireOption, string[] Directory, string[] Hash)
        {
            /*<?xml version="1.0" encoding="utf-8" ?>
            <BellCraft8>
              <Version>
                <Option>Latest</Option>
                <Base>Recommended</Base>
              </Version>

              <Directory>
                <Dir>config</Dir>
                <Dir>mods</Dir>
                <Dir>flan</Dir>
              </Directory>
              <Hash>
                <File loc="mods\buildcraft.jar">asasdfasdfasdf파일해시</File>
                <File loc="mods\industrialcraft.jar">asasddffasdfasdf파일해시</File>
                <File loc="mods\appliedenergetics2.jar">asasdfasdfaasdfwaesdf파일해시</File>
              </Hash>
            </BellCraft8>*/

        }

        /// <summary>
        /// 베이스팩 버전 설치정보를 기록합니다.
        /// </summary>
        /// <param name="Directory">필요 디렉토리 리스트</param>
        /// <param name="Hash">파일 리스트 ('파일명|해시' 식으로 값 대입)</param>
        public void WriteVersionXML(string[] Directory, string[] Hash)
        {
            /*<?xml version="1.0" encoding="utf-8" ?>
            <BCP_1.7.10>
              <Directory>
                <Dir>assets</Dir>
                <Dir>resource</Dir>
              </Directory>
              <Hash>
                <File Loc="assets\어쩌구저쩌구.txt">파일해시 어쩌구저쩌구.txt</File>
                <File Loc="assets\이러쿵저러쿵.log">파일해시 이러쿵저러쿵.log</File>
                <File Loc="resource\리소스.txt">파일해시 리소스.txt</File>
              </Hash>
            </BCP_1.7.10>*/

        }

        /// <summary>
        /// 옵션팩 버전 설치정보를 기록합니다.
        /// </summary>
        /// <param name="Option">옵션정보 ('이름|UID|기본설치' 식으로 값 대입)</param>
        /// <param name="Directory">필요 디렉토리 리스트</param>
        /// <param name="Hash">파일 리스트 ('파일명|해시' 식으로 값 대입)</param>
        public void WriteVersionXML(string[] Option, string[] Directory, string[] Hash)
        {
            /*<?xml version="1.0" encoding="utf-8" ?>
            <BCO_1.7.10>
                <Option>
                    <Name="NEI" UID="NEI">true</Name>
                    <Name="MapWriter" UID="MiniMap">false</Name>
                    <Name="Rei's MiniMap" UID="MiniMap">false</Name>
                </Option>
                <Directory>
                    <Dir>mods</Dir>
                    <Dir>flan</Dir>
                </Directory>
                <Hash>
                    <Name="NEI" Loc="mods\NEI.jar">파일 해시 asdfasdf</File>
                    <Name="MapWriter" Loc="mods\MapWriter.jar">파일 해시 asdfasdf</File>
                    <Name="Rei's MiniMap" Loc="mods\Rei_MiniMap.jar">파일 해시 asdfasdf</File>
                </Hash>
            </BCO_1.7.10>*/

        }

        /// <summary>
        /// List 배열값을 바탕으로 PackList.xml 파일을 생성합니다.
        /// </summary>
        /// <param name="List">팩 리스트 배열</param>
        public void WriteListXML(string[] List)
        {
            /*<?xml version="1.0" encoding="UTF-8"?>
            <List>
              <Pack>BellCraft8</Pack>
              <Pack>FTB</Pack>
            </List>*/// 모드팩, 베이스팩, 옵션팩 리스트.xml구조는 전부 위와 같음.


        }
    }
}
