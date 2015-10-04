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
using System.Windows;

namespace BellLib.Class
{
    delegate void OnEnd();

    /// <summary>
    /// 모든 프로젝트 공통함수
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 해당 웹 텍스트를 전부 가져옵니다.
        /// </summary>
        /// <param name="URL">웹 주소</param>
        /// <param name="UTF8">인코딩 방식 (UTF8, Default)</param>
        /// <returns>웹 텍스트</returns>
        public static string getStringFromWeb(string URL, Encoding Enc = null)
        {
            if (Enc == null)
                Enc = Encoding.Default;
            try
            {
                WebClient wc = new WebClient();
                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705; Combat;)");
                wc.Encoding = Enc;
                return wc.DownloadString(URL);
                /*byte[] bd = wc.DownloadData(URL);
                if (UTF8)
                {
                    return (Encoding.UTF8.GetString(bd));
                }
                else
                {
                    return (Encoding.Default.GetString(bd));
                }*/
            } catch { }

            return null;
            /*try
            {
                return new WebClient().DownloadString(URL);
            }
            catch { return null; }*/
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
        public static bool createFormAndShow(string iName, bool throwException = false)
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
        /// 랜덤 문자열을 반환합니다.
        /// </summary>
        /// <param name="length">필요한 문자열 길이</param>
        /// <returns>랜덤 문자열</returns>
        public static string getRandomString(int length)
        {
            Random rnd = new Random();
            return getRandomString(length, rnd);
        }

        /// <summary>
        /// 랜덤 문자열을 반환합니다.
        /// </summary>
        /// <param name="length">필요한 문자열 길이</param>
        /// <param name="rnd">랜덤 시드</param>
        /// <param name="charPool">랜덤 허용 문자열</param>
        /// <returns>랜덤 문자열</returns>
        public static string getRandomString(int length, Random rnd, string charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvw xyz1234567890")
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
        /// 문자열을 구분자로 잘라냅니다.
        /// </summary>
        /// <param name="input">원본 문자열</param>
        /// <param name="pattern">구분할 단어</param>
        /// <returns>구분된 문자배열</returns>
        public static string[] stringSplit(string input, string pattern)
        {
            try
            {
                return System.Text.RegularExpressions.Regex.Split(input, pattern);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 필요한 요소값 배열을 가져옵니다.
        /// </summary>
        /// <param name="data">전체 데이터</param>
        /// <param name="name">요소 이름</param>
        /// <returns>요소 값 배열</returns>
        public static string[] getElementArray(string data, string name)
        {
            try
            {
                List<string> list = new List<string>();
                string[] temp = Common.stringSplit(data, "<" + name + ">"); // 값 이름 시작 구분
                foreach (string tmp in temp)
                { // 값 이름 끝 구분
                    if (tmp != string.Empty && tmp.Contains("</" + name + ">"))
                    {
                        string value = Common.stringSplit(tmp, "</" + name + ">")[0];
                        if (value != string.Empty)
                            list.Add(value);
                    }
                }

                return list.ToArray();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 필요한 요소의 값을 가져옵니다.
        /// </summary>
        /// <param name="data">전체 데이터</param>
        /// <param name="name">요소 이름</param>
        /// <returns>요소 값</returns>
        public static string getElement(string data, string name)
        {
            try
            {
                string result = Common.stringSplit(Common.stringSplit(data, "<" + name + ">")[1], "</" + name + ">")[0];
                if (result == string.Empty)
                    throw new System.InvalidCastException("해당 요소값이 존재하지 않습니다.");
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
