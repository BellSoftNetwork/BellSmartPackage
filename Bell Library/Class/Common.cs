using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Threading;

namespace BellLib.Class
{
    delegate void OnEnd();

    public class Common
    {
        static bool onExit = false;

        static OnEnd onEnd;

        /// <summary>
        /// 메시지 박스를 띄웁니다.
        /// </summary>
        /// <param name="Text">메시지박스 내용</param>
        /// <param name="Caption">메시지박스 제목</param>
        /// <param name="buttons">메시지박스 버튼</param>
        /// <param name="icon">메시지박스 아이콘</param>
        /// <param name="defaultButton">메시지박스 기본 버튼</param>
        /// <returns>선택한 버튼값</returns>
        public static DialogResult Message(string Text, string Caption = "Bell Smart Package", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            Debug.Message(Debug.Level.Log, Text, Caption, buttons, icon, defaultButton, "MessageBox");
            return MessageBox.Show(Text, Caption, buttons, icon, defaultButton);
        }
        
        /// <summary>
        /// 프로그램을 종료합니다.
        /// </summary>
        /// <param name="Restart">프로그램 재시작 여부</param>
        public static void End(bool Restart = false)
        {
            // 콜백에 프로그램 종료를 알린다.
            onEnd += new OnEnd(() => {
                if (!onExit)
                    Debug.Message(Debug.Level.Log, "End point of program. Exiting.");
            });

            // 콜백이 한번만 실행되도록 한다.
            onEnd += new OnEnd(() => onExit = true);

            // 콜백에 프로그램 종료를 체인한다.
            if (Restart)
                onEnd += Application.Restart;
            else
                onEnd += Application.Exit;

            // 콜백을 호출한다.
            onEnd();
        }

        [Obsolete("RegistryManager 클래스를 사용하십시오.")]
        public static void RegSave(string name, object value)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("BSN");
            reg.SetValue(name, value, RegistryValueKind.String);
        }

        [Obsolete("RegistryReader 클래스를 사용하십시오.")]
        public static string RegLoad(string name, object defaultValue = null, bool throwException = false)
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("BSN");
                return (string)reg.GetValue(name, null);
            }
            catch { }

            return null;
        }

        [Obsolete("RegistryManager 클래스를 사용하십시오.")]
        public static void RegDelete(string name)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("BSN");
            try
            {
                reg.DeleteValue(name); // 레지스트리 삭제. 이미 삭제되어있을경우 catch문 실행.
            }
            catch { }
        }

        /// <summary>
        /// 텍스트 파일을 작성합니다.
        /// </summary>
        /// <param name="localFilePath">저장할 파일 경로</param>
        /// <param name="data">저장할 텍스트 값</param>
        /// <param name="append">이어쓸지 여부</param>
        public static void WriteTextFile(string localFilePath, string data, bool append = false)
        {
            bool written = false;
            while (!written) // 이 구문 현재 LocalFilePath 경로가 존재하지 않으면(폴더가 생성되있지 않으면), 무한루프 도는 구조. 수정 요망
            {
                try
                {
                    if (append)
                        File.AppendAllText(localFilePath, data);
                    else
                        File.WriteAllText(localFilePath, data);

                    written = true;
                }
                catch (Exception ex)
                {
                    Debug.Message(Debug.Level.High, "WriteTextFile" + Environment.NewLine + ex.Message);
                    Debug.Message(Debug.Level.High, "Trying to create folder.");
                    Directory.CreateDirectory(localFilePath);
                }
            }
        }

        /// <summary>
        /// 해당 웹 텍스트를 전부 가져옵니다.
        /// </summary>
        /// <param name="URL">웹 주소</param>
        /// <param name="UTF8">인코딩 방식 (UTF8, Default)</param>
        /// <returns>웹 텍스트</returns>
        public static string GetStringFromWeb(string URL, bool UTF8 = true)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705; Combat;)");
                byte[] bd = wc.DownloadData(URL);
                if (UTF8)
                {
                    return (Encoding.UTF8.GetString(bd));
                }
                else
                {
                    return (Encoding.Default.GetString(bd));
                }
            } catch { }

            return null;
            /*try
            {
                return new WebClient().DownloadString(URL);
            }
            catch { return null; }*/
        }

        /// <summary>
        /// 기본적으로 존재해야하는 디렉토리를 생성합니다.
        /// </summary>
        public static void CreateDefaultForder()
        {
            CreateFolder(Data.User.BSN_Path);
            CreateFolder(Data.User.BSN_Path + "logs");
            CreateFolder(Data.User.BSN_Path + "Temp");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data\\ModPack");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data\\ModPack\\Version");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data\\BasePack");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data\\BasePack\\Version");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data\\OptionPack");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data\\OptionPack\\Version");

            CreateFolder(Data.User.BSN_Path + "Upload");
            CreateFolder(Data.User.BSN_Path + "Upload\\ModPack");
            CreateFolder(Data.User.BSN_Path + "Upload\\BasePack");
            CreateFolder(Data.User.BSN_Path + "Upload\\OptionPack");

            CreateFolder(Data.User.BSN_Path + "Data");

            
            CreateFolder(Data.User.BSL_Root + "Data");
            CreateFolder(Data.User.BSL_Root + "Data\\BSL");
            CreateFolder(Data.User.BSL_Root + "Data\\BSL\\Profile");

            CreateFolder(Data.User.BSL_Root + "ModPack");
            CreateFolder(Data.User.BSL_Root + "Base");
            CreateFolder(Data.User.BSL_Root + "Runtime");
        }

        private static void CreateFolder(string folderPath)
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }

        /// <summary>
        /// 새로운 폼의 인스턴스 생성하고 보여줍니다.
        /// </summary>
        /// <param name="iName">폼 클래스 인스턴스의 이름입니다.</param>
        /// <param name="asm">
        /// 폼 클래스 인스턴스가 포함된 어셈블리입니다.
        /// 불분명한 경우는 Assembly.GetExecutingAssembly()를 사용하십시오.
        /// Bell Library 내부에서 호출 할 경우에는 값을 할당하지 않아도 됩니다.
        /// </param>
        /// <returns>성공 여부를 반환합니다.</returns>
        public static bool CreateFormAndShow(string iName, bool throwException = false)
        {
            try
            {
                Type type = Assembly.GetCallingAssembly().GetTypes().First(t => t.Name == iName);

                object instance = Activator.CreateInstance(type);

                try
                {
                    type.GetMethod("Show", BindingFlags.Public | BindingFlags.Instance).Invoke(instance, null);
                }
                catch (AmbiguousMatchException)
                {
                    foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                        if (method.Name == "Show" && method.GetParameters().Length == 0)
                            method.Invoke(instance, null);
                }

                return true;
            }
            catch
            {
                if (throwException)
                    throw;

                return false;
            }
        }

        /// <summary>
        /// 해당 디렉토리의 파일 포맷을 가진 모든 파일을 삭제합니다.
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="Format"></param>
        /// <returns>파일 존재 여부</returns>
        public static bool DeleteDirectoryFile(string dirPath, string format = "*.*")
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            System.IO.FileInfo[] files;

            files = dir.GetFiles(format, SearchOption.AllDirectories);
            if (files.Length <= 0)
            {
                return false;
            }

            foreach (System.IO.FileInfo file in files)
            {
                // 만약 ReadOnly 속성이 있는 파일이 있다면 지울때 에러가 나므로 속성을 Normal로 바꿔놓는다.
                if (file.Attributes == FileAttributes.ReadOnly)
                    file.Attributes = FileAttributes.Normal;
                file.Delete();
            }

            return true;
        }

        /// <summary>
        /// 디렉토리 배열을 반환합니다.
        /// </summary>
        /// <param name="strFilePath">최상위 경로</param>
        /// <param name="Replace">최상위 경로를 제거한 뒤 반환할지 여부</param>
        /// <returns>디렉토리 리스트</returns>
        public static string[] GetDirectoryArray(string strFilePath, bool Replace)
        {
            if (strFilePath[strFilePath.Length - 1] == '\\')
                strFilePath = strFilePath.Substring(0, strFilePath.Length - 1);
            List<string> list = new List<string>();
            foreach (FileInfo File in (new DirectoryInfo(strFilePath)).GetFiles("*", SearchOption.AllDirectories))
            {
                string Path = File.DirectoryName;
                if (Replace)
                {
                    Path = Path.Replace(strFilePath + '\\', string.Empty);
                    Path = Path.Replace(strFilePath, string.Empty);
                }
                if (!list.Contains(Path) && Path != string.Empty)
                    list.Add(Path);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 파일 배열을 반환합니다.
        /// </summary>
        /// <param name="strFilePath">최상위 폴더</param>
        /// <param name="Replace">최상위 경로를 제거할지 여부</param>
        /// <returns>파일 리스트 배열</returns>
        public static string[] GetFileArray(string strFilePath, bool Replace)
        {
            List<string> list = new List<string>();
            foreach (FileInfo File in (new DirectoryInfo(strFilePath)).GetFiles("*", SearchOption.AllDirectories))
            {
                string Path = File.FullName;
                if (Replace)
                    Path = Path.Replace(strFilePath, string.Empty);
                if (!list.Contains(Path))
                    list.Add(Path);
            }

            return list.ToArray();
        }

        /// <summary>
        /// .bdx (Bell Data Xml) 데이터 파일을 작성합니다.
        /// </summary>
        /// <param name="Path">작성할 .bdx파일 경로</param>
        /// <param name="Data">작성할 데이터 ('데이터명|데이터값' 형식으로 전달)</param>
        public static void WriteBDXFile(string Path, string[] Data)
        {
            /*string result;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (MemoryStream output = new MemoryStream())
            {
                using (XmlWriter Writer = XmlWriter.Create(output, settings))
                {
                    Writer.WriteStartDocument(); // 문서 시작
                    Writer.WriteStartElement("BDX"); // BDX 시작 <BDX>
                    foreach (string tmp in Data)
                    {
                        string[] Value = tmp.Split('|');
                        Writer.WriteElementString(Value[0], Value[1]); // <값이름>값</값이름>
                    }
                    Writer.WriteEndElement(); // BDX 끝냄 </BDX>
                    Writer.WriteEndDocument(); // 문서 끝냄
                    Writer.Flush();
                    Writer.Close();
                }
                result = Encoding.UTF8.GetString(output.ToArray());
            }
            WriteBDFile(Path, result); // 데이터 파일 작성*/

            StringBuilder sb = new StringBuilder();
            XmlWriterSettings xmlSetting = new XmlWriterSettings();
            xmlSetting.Encoding = Encoding.UTF8;
            xmlSetting.Indent = true;

            XmlWriter Writer = XmlWriter.Create(sb, xmlSetting);
            Writer.WriteStartDocument(); // 문서 시작
            Writer.WriteStartElement("BDX"); // BDX 시작 <BDX>
            foreach (string tmp in Data)
            {
                string[] Value = tmp.Split('|');
                Writer.WriteStartElement("DATA");
                Writer.WriteAttributeString("NAME", Value[0]);
                Writer.WriteString(Value[1]); // <DATA NAME="값이름">값</DATA>
                Writer.WriteEndElement();
            }
            Writer.WriteEndElement(); // BDX 끝냄 </BDX>
            Writer.WriteEndDocument(); // 문서 끝냄
            Writer.Flush();
            Writer.Close();

            string xmlString = sb.ToString();
            WriteBDFile(Path, xmlString); // 데이터 파일 작성
        }

        /// <summary>
        /// .bdx (Bell Data Xml) 데이터 파일을 읽습니다.
        /// </summary>
        /// <param name="Path">읽을 .bdx파일 경로</param>
        /// <returns>복호화 된 데이터 ('데이터이름|데이터값' 형식으로 반환)</returns>
        public static string[] ReadBDXFile(string Path)
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            try
            {
                string data = ReadBDFile(Path);
                doc.LoadXml(data);
            }
            catch
            {
                throw new System.InvalidOperationException("Failure to read data."); // 데이터 읽기 실패
            }
            xnList = doc.SelectNodes("/BDX/DATA");

            StringBuilder str = new StringBuilder();
            foreach (XmlNode xn in xnList)
            {
                str.Append(xn.Attributes.GetNamedItem("NAME").InnerText);
                str.Append("|");
                str.AppendLine(xn.InnerText);
            }
            List<string> lst = new List<string>();
            foreach (string tmp in str.ToString().Split('\n'))
                if (tmp != "")
                    lst.Add(tmp.Replace("\r", string.Empty));

            return lst.ToArray();
        }

        /// <summary>
        /// .bd (Bell Data) 파일을 작성합니다.
        /// </summary>
        /// <param name="Path">작성할 .bd파일 경로</param>
        /// <param name="Text">작성할 텍스트</param>
        /// <param name="Append">내용 추가여부</param>
        public static void WriteBDFile(string Path, string Text, bool Append = false)
        {
            WriteTextFile(Path, WriteBDText(Text), Append); // 데이터 작성
        }

        /// <summary>
        /// 텍스트를 Bell Data 방식으로 암호화합니다.
        /// </summary>
        /// <param name="Text">작성할 텍스트</param>
        /// <returns>암호화된 텍스트</returns>
        public static string WriteBDText(string Text)
        {
            Protection Pro = new Protection();
            Text += "BELL" + GetRandomString(2) + "DATA"; // 원본 텍스트에 10자리의 쓰레기값을 추가함.
            Text = Pro.Base64(Text, Protection.ProtectionType.PROTECTION_ENCODE); // 암호화
            Text += GetRandomString(6) + "BELL"; // 암호화된 텍스트에 10자리의 쓰레기값을 추가함.
            
            return Text;
        }

        /// <summary>
        /// .bd (Bell Data) 파일을 읽습니다.
        /// </summary>
        /// <param name="Path">.bd파일 경로</param>
        /// <returns>복호화된 bd파일 값</returns>
        public static string ReadBDFile(string Path)
        {
            return ReadBDText(File.ReadAllText(Path));
        }

        /// <summary>
        /// Bell Data 텍스트를 읽습니다.
        /// </summary>
        /// <param name="Text">복호화할 BD 텍스트</param>
        /// <returns>복호화된 텍스트</returns>
        public static string ReadBDText(string Text)
        {
            Protection Pro = new Protection();
            Text = Text.Substring(0, Text.Length - 10); // 암호화된 데이터의 끝 10자리 쓰레기값을 제거함.
            Text = Pro.Base64(Text, Protection.ProtectionType.PROTECTION_DECODE); // 복호화
            Text = Text.Substring(0, Text.Length - 10); // 복호화된 데이터의 끝 10자리 쓰레기값을 제거함.

            return Text;
        }

        /// <summary>
        /// 랜덤 문자열을 반환합니다.
        /// </summary>
        /// <param name="length">필요한 문자열 길이</param>
        /// <returns>랜덤 문자열</returns>
        public static string GetRandomString(int length)
        {
            Random rnd = new Random();
            return GetRandomString(length, rnd);
        }

        /// <summary>
        /// 랜덤 문자열을 반환합니다.
        /// </summary>
        /// <param name="length">필요한 문자열 길이</param>
        /// <param name="rnd">랜덤 시드</param>
        /// <param name="charPool">랜덤 허용 문자열</param>
        /// <returns>랜덤 문자열</returns>
        public static string GetRandomString(int length, Random rnd, string charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvw xyz1234567890")
        {
            StringBuilder rs = new StringBuilder();

            while (length != rs.Length)
                rs.Append(charPool[(int)(rnd.NextDouble() * charPool.Length)]);

            return rs.ToString();
        }

        /// <summary>
        /// 호출한 스레드를 잠시 멈춥니다.
        /// </summary>
        /// <param name="millisecondsTimeout">멈출 시간</param>
        public static void Delay(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }

        /// <summary>
        /// 해당 폼 이름으로 폼이 실행되어있는지 여부를 판단합니다.
        /// 폼이 실행되어있을경우, 폼을 활성화 시킵니다.
        /// </summary>
        /// <param name="formName">폼 이름 (대소문자 구별)</param>
        /// <returns>폼 실행 가능 여부</returns>
        public static bool Feasibility(string formName)
        {
            Form frm = GetForm(formName);
            if (frm == null)
                return true; // 폼이 실행되어 있지 않을경우 폼 실행 가능
            
            frm.Activate();
            return false;
        }

        /// <summary>
        /// 해당 폼 이름으로 폼이 실행되어 있으면, 해당 폼의 Form 값을 반환합니다.
        /// </summary>
        /// <param name="formName">폼 이름 (대소문자 구별)</param>
        /// <returns>Form 정보</returns>
        public static Form GetForm(string formName)
        {
            foreach (Form frm in Application.OpenForms)
                if (frm.Name == formName)
                    return frm;

            return null;
        }
    }
}
