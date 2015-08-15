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

        /// <summary>
        /// 해당 웹 텍스트를 전부 가져옵니다.
        /// </summary>
        /// <param name="URL">웹 주소</param>
        /// <param name="UTF8">인코딩 방식 (UTF8, Default)</param>
        /// <returns>웹 텍스트</returns>
        public static string GetStringFromWeb(string URL, Encoding Enc = null)
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
