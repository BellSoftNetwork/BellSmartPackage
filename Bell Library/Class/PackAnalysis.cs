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
        public string[] _Detail;
        public string[] _Permission;
        public string[] _Server;
        public string[] _Version;
    }

    public struct Info_Basepack
    {
        public string _Latest, _Recommended, _Down;
        public string[] _Permission;
        public string[] _Server;
        public string[] _Version;
    }

    public struct Info_Optionpack
    {
        public string _Latest, _Recommended, _Down;
        public string[] _Permission;
        public string[] _Server;
        public string[] _Version;
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

    [Obsolete]
    /// <summary>
    /// 클라이언트 전용
    /// 웹 XML 데이터를 바탕으로 모드팩, 베이스팩, 옵션팩 정보 분석을 시행합니다.
    /// </summary>
    public class PackAnalysisRead
    {
        #region 필드

        // 사용하거든!
        #pragma warning disable 169
        private Info_Modpack InfoModpack = new Info_Modpack();
        private Info_Basepack InfoBasepack = new Info_Basepack();
        private Info_Optionpack InfoOptionpack = new Info_Optionpack();
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
        public PackAnalysisRead()
        {

        }

        /// <summary>
        /// UID로 지정팩.xml을 로드 및 분석합니다.
        /// </summary>
        /// <param name="UID">Unique Identifier. 고유 식별자</param>
        public PackAnalysisRead(PackType pt, string UID, bool parse = true)
        {
            if (parse) // 공통 로드 부분
            {
                /*if (!LoadModList()) return;
                if (!LoadBaseList()) return;
                if (!LoadOptionList()) return;*/
            }

            switch (pt)
            {
                case PackType.Mod:
                    _MUID = UID;
                    if (!parse)
                        break;
                    if (!ParseModInfo()) { return; }
                    if (!ParseBaseInfo()) { return; }
                    if (!ParseOptionInfo()) { return; }
                    break;

                case PackType.Base:
                    _BUID = UID;
                    if (!parse)
                        break;
                    if (!ParseBaseInfo()) { return; }

                    break;

                case PackType.Option:
                    _OUID = UID;
                    if (!parse)
                        break;
                    if (!ParseOptionInfo()) { return; }

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
                doc.Load(Servers.Bell_Soft_Network.WEB_INFO_BSL + "Pack/" + _MUID + "/" + _MUID + ".xml");
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
                doc.Load(Servers.Bell_Soft_Network.WEB_INFO_BSL + "Base/" + _BUID + "/" + _BUID + ".xml");
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
                doc.Load(Servers.Bell_Soft_Network.WEB_INFO_BSL + "Option/" + _OUID + "/" + _OUID + ".xml");
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


        #region 인스턴스 필요없는 메서드

        public static string[] LoadPackList(PackType Type)
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            string Pack = null;
            switch (Type)
            {
                case PackType.Mod:
                    Pack = "Pack";
                    break;
                    
                case PackType.Base:
                    Pack = "Base";
                    break;

                case PackType.Option:
                    Pack = "Option";
                    break;
            }
            try
            {
                doc.Load(Servers.Bell_Soft_Network.WEB_INFO_BSL + "/" + Pack + "/PackList.xml");
            }
            catch
            {
                throw new System.InvalidOperationException("팩 리스트 로드 실패" + Environment.NewLine + "Type : " + Type); // 데이터 읽기 실패
                //return null;
            }
            xnList = doc.SelectNodes("/List/Pack");

            StringBuilder str = new StringBuilder();
            foreach (XmlNode xn in xnList)
                str.AppendLine(xn.InnerText);

            List<string> lst = new List<string>();
            foreach (string tmp in str.ToString().Split('\n'))
                if (tmp != string.Empty)
                    lst.Add(tmp.Replace("\r", string.Empty));

            return lst.ToArray();
        }
        #endregion

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
                doc.Load(Servers.Bell_Soft_Network.WEB_INFO_BSL + "Pack/" + _MUID + "/Version/" + Version + ".xml");
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

            str.Clear(); // 위에서 한번 썼으면 초기화를 해줘야지!
            lst.Clear(); // 이것도!
            xnList = doc.SelectNodes("/" + _MUID + "/Hash/File"); // 베이스팩 필요한 파일
            foreach (XmlNode xn in xnList)
            {
                str.AppendLine(xn.Attributes.GetNamedItem("Loc").InnerText + "|" + xn.InnerText); // 파일 상대 경로|파일해시
            }
            foreach (string tmp in str.ToString().Split('\n'))
                if (tmp != "")
                    lst.Add(tmp.Replace("\r", string.Empty));
            VerModpack.Hash = lst.ToArray(); // 배열로 올림
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
                doc.Load(Servers.Bell_Soft_Network.WEB_INFO_BSL + "Base/" + _BUID + "/Version/" + Version + ".xml"); // 베이스 버전 데이터 로드
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
                    if (InfoModpack._Version == null)
                        throw new System.InvalidOperationException("Failure to read mod version data."); // 데이터 읽기 실패
                    return InfoModpack._Version;

                case PackType.Base:
                    if (InfoBasepack._Version == null)
                        throw new System.InvalidOperationException("Failure to read base version data."); // 데이터 읽기 실패
                    return InfoBasepack._Version;

                case PackType.Option:
                    if (InfoOptionpack._Version == null)
                        throw new System.InvalidOperationException("Failure to read option version data."); // 데이터 읽기 실패
                    return InfoOptionpack._Version;
            }
            return null;
        }
        
        /// <summary>
        /// 선택된 팩 정보를 바탕으로 저장된 정보를 반환합니다.
        /// </summary>
        /// <param name="pt">팩 타입</param>
        /// <param name="Name">반환 요청 정보</param>
        /// <returns>저장된 정보</returns>
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
            }
            catch
            {
                throw new System.InvalidOperationException("Failure to read info data."); // 데이터 읽기 실패
            }
            return null;
        }

        /// <summary>
        /// 선택한 팩 정보를 바탕으로 설치 정보를 반환합니다.
        /// </summary>
        /// <param name="pt">팩 타입</param>
        /// <param name="Name">필요한 설치정보값</param>
        /// <returns>설치 정보 값</returns>
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
            catch
            {
                throw new System.InvalidOperationException("Failure to read install info data."); // 데이터 읽기 실패
            }
            return null;
        }

        /// <summary>
        /// 선택한 팩 정보를 바탕으로 설치 정보배열을 반환합니다.
        /// </summary>
        /// <param name="pt">팩 타입</param>
        /// <param name="Name">필요한 설치정보값</param>
        /// <returns>설치 정보값 배열</returns>
        public string[] GetInstallData(PackType pt, string Name)
        {
            try
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
            }
            catch
            {
                throw new System.InvalidOperationException("Failure to read install data."); // 데이터 읽기 실패
            }
            return null;
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

    [Obsolete]
    /// <summary>
    /// 서버 전용
    /// 모드팩, 베이스팩, 옵션팩 정보 분석 및 XML 작성을 시행합니다.
    /// </summary>
    public class PackAnalysisWrite
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
        /// <summary>
        /// 호출 시 정보를 제공하는 메서드 사용 시 생성합니다.
        /// 일부 인스턴스 생성시 입력한 정보를 사용하는 메서드는 이 생성자로 작동하지 않습니다.
        /// </summary>
        public PackAnalysisWrite()
        {
            
        }

        /// <summary>
        /// XML 작성을 위한 최소한의 정보를 미리 저장해둡니다.
        /// </summary>
        /// <param name="Typ">팩 타입</param>
        /// <param name="UID">고유이름</param>
        public PackAnalysisWrite(Type Typ, string UID)
        {
            this.Pack = Typ;
            this.UID = UID;
        }
        /// <summary>
        /// 모드분석 후 XML 데이터를 작성합니다.
        /// 모드팩 전용
        /// </summary>
        public PackAnalysisWrite(Type Typ, string UID, string Name, string Latest, string Recommended, string Base, string Option, string News, string Down, string[] Version)
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
        /// <summary>
        /// 모드분석 후 XML 데이터를 작성합니다.
        /// 베이스팩, 옵션팩 전용
        /// </summary>
        /// <param name="Typ"></param>
        /// <param name="UID"></param>
        /// <param name="Latest"></param>
        /// <param name="Recommended"></param>
        /// <param name="Down"></param>
        /// <param name="Version"></param>
        public PackAnalysisWrite(Type Typ, string UID, string Latest, string Recommended, string Down, string[] Version)
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

        /// <summary>
        /// 설정된 팩타입에 맞게 UID.xml 파일을 작성합니다.
        /// </summary>
        public void WriteXML()
        {
            XmlTextWriter XTW;

            switch (Pack)
            {
                case Type.ModPack:
                    XTW = new XmlTextWriter(User.BSN_Temp + "BSU\\Data\\ModPack\\" + UID + ".xml", Encoding.UTF8);
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
                    try
                    {
                        foreach (string tmp in DM.Version)
                            XTW.WriteElementString("Ver", tmp);
                    }
                    catch { }

                    XTW.WriteEndElement();
                    XTW.WriteEndDocument();
                    XTW.Flush();
                    XTW.Close();
                    break;

                case Type.BasePack:
                    XTW = new XmlTextWriter(User.BSN_Temp + "BSU\\Data\\BasePack\\" + UID + ".xml", Encoding.UTF8);
                    XTW.Formatting = Formatting.Indented; // 파일 기록시 자동으로 들여씀
                    XTW.WriteStartDocument(); // XML 문서 시작
                    XTW.WriteStartElement(UID); // UID 엘리먼트 시작

                    XTW.WriteStartElement("Info"); // Info 엘리먼트 시작
                    XTW.WriteElementString("Latest", DB.Latest);
                    XTW.WriteElementString("Recommended", DB.Recommended);
                    XTW.WriteElementString("Down", DB.Down);
                    XTW.WriteEndElement(); // Info 엘리먼트 닫음

                    XTW.WriteStartElement("Version");
                    try
                    {
                        foreach (string tmp in DB.Version)
                            XTW.WriteElementString("Ver", tmp);
                    }
                    catch { }

                    XTW.WriteEndElement();
                    XTW.WriteEndDocument();
                    XTW.Flush();
                    XTW.Close();
                    break;

                case Type.OptionPack:
                    XTW = new XmlTextWriter(User.BSN_Temp + "BSU\\Data\\OptionPack\\" + UID + ".xml", Encoding.UTF8);
                    XTW.Formatting = Formatting.Indented; // 파일 기록시 자동으로 들여씀
                    XTW.WriteStartDocument(); // XML 문서 시작
                    XTW.WriteStartElement(UID); // UID 엘리먼트 시작

                    XTW.WriteStartElement("Info"); // Info 엘리먼트 시작
                    XTW.WriteElementString("Latest", DO.Latest);
                    XTW.WriteElementString("Recommended", DO.Recommended);
                    XTW.WriteElementString("Down", DO.Down);
                    XTW.WriteEndElement(); // Info 엘리먼트 닫음

                    XTW.WriteStartElement("Version");
                    try
                    {
                        foreach (string tmp in DO.Version)
                            XTW.WriteElementString("Ver", tmp);
                    }
                    catch { }

                    XTW.WriteEndElement();
                    XTW.WriteEndDocument();
                    XTW.Flush();
                    XTW.Close();
                    break;
            }
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
            XmlTextWriter XTW = new XmlTextWriter(User.BSN_Temp + "BSU\\Data\\PackList.xml", Encoding.UTF8);
            XTW.Formatting = Formatting.Indented; // 파일 기록시 자동으로 들여씀
            XTW.WriteStartDocument();
            XTW.WriteStartElement("List");
            foreach (string tmp in List)
                XTW.WriteElementString("Pack", tmp);
            XTW.WriteEndElement();
            XTW.WriteEndDocument();
            XTW.Flush();
            XTW.Close();
        }

        /// <summary>
        /// 모드팩 설치 데이터 XML을 작성합니다.
        /// 인스턴스생성시 팩 타입, MUID 값 설정 후 메서드 사용 가능.
        /// </summary>
        /// <param name="Version">모드팩 버전</param>
        /// <param name="RequireBaseVer">실행시 필요한 베이스팩 버전</param>
        /// <param name="RequireOptionVer">실행시 필요한 옵션팩 버전</param>
        /// <param name="Directory">생성이 필요한 디렉토리 배열</param>
        /// <param name="Hash">파일 해시 ('경로|해시')</param>
        public void WriteInstallXML(string Version, string RequireBaseVer, string RequireOptionVer, string[] Directory, string[] Hash)
        {
            if (Pack != Type.ModPack)
                return;
            XmlTextWriter XTW = new XmlTextWriter(User.BSN_Temp + "BSU\\Data\\ModPack\\Version\\" + Version + ".xml", Encoding.UTF8);
            XTW.Formatting = Formatting.Indented;
            XTW.WriteStartDocument();
            XTW.WriteStartElement(UID);

            XTW.WriteStartElement("Version");
            XTW.WriteElementString("Base", RequireBaseVer);
            XTW.WriteElementString("Option", RequireOptionVer);
            XTW.WriteEndElement();

            XTW.WriteStartElement("Directory");
            foreach (string tmp in Directory)
                XTW.WriteElementString("Dir", tmp);
            XTW.WriteEndElement();

            XTW.WriteStartElement("Hash");
            foreach (string tmp in Hash)
            {
                XTW.WriteStartElement("File");
                XTW.WriteAttributeString("Loc", tmp.Split('|')[0]);
                XTW.WriteString(tmp.Split('|')[1]);
                XTW.WriteEndElement();
            }
            XTW.WriteEndElement();
            XTW.WriteEndDocument();
            XTW.Flush();
            XTW.Close();
        }

        /// <summary>
        /// 베이스팩 설치 데이터 XML을 작성합니다.
        /// 인스턴스생성시 팩 타입, MUID 값 설정 후 메서드 사용 가능.
        /// </summary>
        /// <param name="Version">베이스팩 버전</param>
        /// <param name="Directory">생성이 필요한 디렉토리 배열</param>
        /// <param name="Hash">파일 해시 ('경로|해시')</param>
        public void WriteInstallXML(string Version, string[] Directory, string[] Hash)
        {
            if (Pack != Type.BasePack)
                return;
            XmlTextWriter XTW = new XmlTextWriter(User.BSN_Temp + "BSU\\Data\\BasePack\\Version\\" + Version + ".xml", Encoding.UTF8);
            XTW.Formatting = Formatting.Indented;
            XTW.WriteStartDocument();
            XTW.WriteStartElement(UID);
            
            XTW.WriteStartElement("Directory");
            foreach (string tmp in Directory)
                XTW.WriteElementString("Dir", tmp);
            XTW.WriteEndElement();

            XTW.WriteStartElement("Hash");
            foreach (string tmp in Hash)
            {
                XTW.WriteStartElement("File");
                XTW.WriteAttributeString("Loc", tmp.Split('|')[0]);
                XTW.WriteString(tmp.Split('|')[1]);
                XTW.WriteEndElement();
            }
            XTW.WriteEndElement();
            XTW.WriteEndDocument();
            XTW.Flush();
            XTW.Close();
        }

        /// <summary>
        /// 옵션팩 설치 데이터 XML을 작성합니다.
        /// 인스턴스생성시 팩 타입, MUID 값 설정 후 메서드 사용 가능.
        /// </summary>
        /// <param name="Version">옵션팩 버전</param>
        /// <param name="Option">옵션 정보 ('이름|고유이름|기본설치|경로')</param>
        /// <param name="Directory">생성이 필요한 디렉토리 배열</param>
        /// <param name="Hash">파일 해시 ('이름|경로|해시')</param>
        public void WriteInstallXML(string Version, string[] Option, string[] Directory, string[] Hash)
        {
            if (Pack != Type.OptionPack)
                return;
            XmlTextWriter XTW = new XmlTextWriter(User.BSN_Temp + "BSU\\Data\\OptionPack\\Version\\" + Version + ".xml", Encoding.UTF8);
            XTW.Formatting = Formatting.Indented;
            XTW.WriteStartDocument();
            XTW.WriteStartElement(UID);

            XTW.WriteStartElement("Option");
            foreach (string tmp in Option)
            {
                XTW.WriteStartElement("Opt");
                XTW.WriteAttributeString("Name", tmp.Split('|')[0]);
                XTW.WriteAttributeString("UID", tmp.Split('|')[1]);
                XTW.WriteString(tmp.Split('|')[2]);
                XTW.WriteEndElement();
            }
            XTW.WriteEndElement();

            XTW.WriteStartElement("Directory");
            foreach (string tmp in Directory)
                XTW.WriteElementString("Dir", tmp);
            XTW.WriteEndElement();

            XTW.WriteStartElement("Hash");
            foreach (string tmp in Hash)
            {
                XTW.WriteStartElement("File");
                XTW.WriteAttributeString("Name", tmp.Split('|')[0]);
                XTW.WriteAttributeString("Loc", tmp.Split('|')[1]);
                XTW.WriteString(tmp.Split('|')[2]);
                XTW.WriteEndElement();
            }
            XTW.WriteEndElement();
            XTW.WriteEndDocument();
            XTW.Flush();
            XTW.Close();
        }
    }
}
