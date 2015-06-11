using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
    }
}
