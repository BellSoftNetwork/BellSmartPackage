using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BellLib.Data;

namespace BellLib.Class
{
    public class RuntimeAnalysis
    {
        public enum RunType
        {
            JAVA
        }
        public enum JAVAType
        {
            x64,
            x86
        }
    }

    /// <summary>
    /// 런타임팩 데이터 읽기
    /// 1. 인스턴스 생성
    /// 2. 런타임팩 세부정보 설정 (SetJAVA)
    /// 3. 설치 데이터 로드 (LoadInstallData)
    /// 4. 
    /// </summary>
    public class RuntimeAnalysisRead
    {
        private bool ReadAvailable = false;
        private bool GetAvailable = false;
        private RuntimeAnalysis.RunType Runtime;
        private string xmlPath;
        private string xmlName;

        /// <summary>
        /// 런타임팩 설치 데이터
        /// </summary>
        private string[] Directory, Files;

        public enum DataType
        {
            Directory,
            File
        }

        public RuntimeAnalysisRead(RuntimeAnalysis.RunType Runtime)
        {
            string TempPath = Servers.Bell_Soft_Network.WEB_INFO_ROOT + "BSP/Runtime/";
            
            this.Runtime = Runtime;
            switch (Runtime)
            {
                case RuntimeAnalysis.RunType.JAVA:
                    xmlPath = TempPath + "JAVA/";
                    break;
            }
        }

        public void SetJAVA(RuntimeAnalysis.JAVAType Architecture)
        {
            if (Architecture == RuntimeAnalysis.JAVAType.x64)
            {
                xmlName = "x64.xml";
            }
            else if (Architecture == RuntimeAnalysis.JAVAType.x86)
            {
                xmlName = "x86.xml";
            }
            else
            {
                throw new System.InvalidCastException("잘못된 설정입니다.");
            }

            ReadAvailable = true;
        }

        public bool LoadInstallData()
        {
            if (!ReadAvailable)
                throw new System.MethodAccessException("런타임팩 상세설정이 필요합니다.");

            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            List<string> lst = new List<string>();
            try
            {
                doc.Load(xmlPath + xmlName); // 베이스 버전 데이터 로드
            }
            catch (Exception ex)
            {
                throw ex;
            }

            xnList = doc.SelectNodes("/JAVA/Directory/Dir"); // 베이스팩 필요한 디렉토리
            foreach (XmlNode xn in xnList)
                lst.Add (xn.InnerText);

            this.Directory = lst.ToArray(); // 배열로 올림

            lst.Clear(); // 위에서 썼으니 초기화
            xnList = doc.SelectNodes("/JAVA/File/loc"); // 베이스팩 필요한 파일
            foreach (XmlNode xn in xnList)
                lst.Add(xn.InnerText);

            this.Files = lst.ToArray(); // 배열로 올림

            GetAvailable = true;
            return GetAvailable;
        }

        public void CreateDirectory(string LocalPath)
        {
            if (!GetAvailable)
                throw new System.MethodAccessException("설치 데이터가 로드되지 않았습니다.");

            foreach (string tmp in Directory)
                Common.CreateFolder(LocalPath + tmp);
        }

        public string[] GetInstallData(DataType DType)
        {
            if (!GetAvailable)
                throw new System.MethodAccessException("설치 데이터가 로드되지 않았습니다.");

            if (DType == DataType.Directory)
            {
                return this.Directory;
            }
            else if (DType == DataType.File)
            {
                return this.Files;
            }

            throw new System.InvalidCastException("존재하지 않는 데이터 타입입니다.");
        }
    }

    public class RuntimeAnalysisWrite
    {
        private bool WriteAvailable = false;
        private RuntimeAnalysis.RunType Runtime;
        private string xmlPath;
        private string xmlName;

        /// <summary>
        /// 런타임 작성 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="Runtime">작성할 런타임 종류</param>
        public RuntimeAnalysisWrite(RuntimeAnalysis.RunType Runtime)
        {
            string TempPath = User.BSN_Temp + "BSU\\Data\\Runtime\\";

            this.Runtime = Runtime;
            switch (Runtime)
            {
                case RuntimeAnalysis.RunType.JAVA:
                    xmlPath = TempPath + "JAVA\\";
                    break;
            }
        }

        /// <summary>
        /// 자바 런타임팩 세부설정
        /// </summary>
        /// <param name="Architecture">자바 아키텍쳐</param>
        public void SetJAVA(RuntimeAnalysis.JAVAType Architecture)
        {
            if (Architecture == RuntimeAnalysis.JAVAType.x64)
            {
                xmlName = "x64.xml";
            }
            else if (Architecture == RuntimeAnalysis.JAVAType.x86)
            {
                xmlName = "x86.xml";
            }
            else
            {
                throw new System.InvalidCastException("잘못된 설정입니다.");
            }

            WriteAvailable = true;
        }

        /// <summary>
        /// 런타임팩 설치 데이터를 작성합니다.
        /// </summary>
        /// <param name="Directory">생성이 필요한 디렉토리 배열</param>
        /// <param name="Files">다운로드 필요 파일</param>
        /// <returns></returns>
        public bool WriteInstallData(string[] Directory, string[] Files)
        {
            if (!WriteAvailable)
                throw new System.MethodAccessException("런타임팩 상세설정이 필요합니다.");

            try
            {
                Common.CreateFolder(xmlPath);
                XmlTextWriter XTW = new XmlTextWriter(xmlPath + xmlName, Encoding.UTF8);
                XTW.Formatting = Formatting.Indented;
                XTW.WriteStartDocument();
                XTW.WriteStartElement("JAVA");
                XTW.WriteStartElement("Directory");
                foreach (string tmp in Directory)
                    XTW.WriteElementString("Dir", tmp);
                XTW.WriteEndElement(); // Directory 요소 끝냄
                XTW.WriteStartElement("File");
                foreach (string tmp in Files)
                    XTW.WriteElementString("loc", tmp);
                XTW.WriteEndElement(); // File 요소 끝냄
                XTW.WriteEndElement(); // JAVA 요소 끝냄
                XTW.WriteEndDocument();
                XTW.Flush();
                XTW.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public string xmlFilePath
        {
            get { return this.xmlPath; }
        }

        public string xmlFileName
        {
            get { return this.xmlName; }
        }
    }
}
