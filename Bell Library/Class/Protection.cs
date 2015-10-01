using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace BellLib.Class
{
    /// <summary>
    /// 암호화 메서드들의 집합입니다.
    /// </summary>
    public class Protection
    {
        public enum ProtectionType
        {
            PROTECTION_ENCODE,
            PROTECTION_DECODE
        }

        public string Base64(string Content, ProtectionType pType, byte Count = 1)
        {
            byte TC = 1;
            string contect = Content;

            switch (pType)
            {
                case ProtectionType.PROTECTION_ENCODE:
                    for (TC = 1; TC <= Count; TC++)
                    {
                        contect = (string)Base64Encode(contect);
                    }
                    break;
                case ProtectionType.PROTECTION_DECODE:
                    for (TC = 1; TC <= Count; TC++)
                    {
                        contect = (string)Base64Decode(contect);
                    }
                    break;
                default:
                    return null;
            }

            return contect;
        }

        private object Base64Encode(string Content)
        {
            try
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(Content));
                // 그리고 Enco를 Base64 문자 형태로 인코딩하여 Return(반환)하게 됩니다. 
                //예외 발견 시
            }
            catch // (Exception ex)
            {
                return Content;
                //콘텐츠 반환
            }
        }

        private object Base64Decode(string Content)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(Content));
                //예외 발견 시
            }
            catch // (Exception ex)
            {
                return Content;
                //콘텐츠 반환
            }
        }

        /*public string MD5(string Content)
        {
            MD5Cng MD = new MD5Cng();
            //MD.ComputeHash()
        }*/
        public string MD5Hash(string filename)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (int i in md5.ComputeHash(stream))
                            sb.Append(i.ToString("X2"));
                        return sb.ToString();
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        /*private Dictionary<string, FileStream> StrDic = new Dictionary<string, FileStream>();
        private string CaluateMD5(string key)
        {
            System.Security.Cryptography.MD5Cng a = new System.Security.Cryptography.MD5Cng();
            
            byte[] b = a.ComputeHash(StrDic(key));
            StringBuilder c = new StringBuilder();
            foreach (int i in b)
            {
                c.Append(i.ToString("X2"));
            }
            return c.ToString.Replace("-", "");
        }*/

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
            FileSystem.WriteTextFile(Path, WriteBDText(Text), Append); // 데이터 작성
        }

        /// <summary>
        /// 텍스트를 Bell Data 방식으로 암호화합니다.
        /// </summary>
        /// <param name="Text">작성할 텍스트</param>
        /// <returns>암호화된 텍스트</returns>
        public static string WriteBDText(string Text)
        {
            Protection Pro = new Protection();
            Text += "BELL" + Common.getRandomString(2) + "DATA"; // 원본 텍스트에 10자리의 쓰레기값을 추가함.
            Text = Pro.Base64(Text, Protection.ProtectionType.PROTECTION_ENCODE); // 암호화
            Text += Common.getRandomString(6) + "BELL"; // 암호화된 텍스트에 10자리의 쓰레기값을 추가함.

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
    }
}
