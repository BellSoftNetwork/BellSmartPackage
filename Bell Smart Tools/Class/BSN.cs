﻿using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Bell_Smart_Tools.Class
{
    /// <summary>
    /// BSN 로그인 관련 메서드와 필드를 가지고 있는 클래스입니다.
    /// </summary>
    class BSN
    {
        private const string MidURL = "MC";
        private static bool LoggedIn = false;
        private static HttpWebRequest wRequestBSN = null;
        private static CookieContainer wCookie = new CookieContainer();

        /// <summary>
        /// BSN에 로그인 된 상태를 반환합니다.
        /// </summary>
        public static bool getLoginStatus
        {
            get { return LoggedIn; }
        }

        /// <summary>
        /// BSN에 로그인을 수행합니다.
        /// </summary>
        /// <param name="email">로그인에 필요한 패스워드</param>
        /// <param name="password">로그인에 필요한 패스워드</param>
        /// <returns>작업 성공 여부를 반환합니다.</returns>
        public static bool BSNLogin(string email, string password)
        {
            ///*HttpWebRequest */wRequestBSN = GetInstance();
            WebRequest Temp = HttpWebRequest.Create(Data.Base.BSN_WEB_URL + "index.php?mid=" + MidURL + "&act=dispMemberLoginForm");
            
            wRequestBSN = (HttpWebRequest)Temp;
            wRequestBSN.Method = "POST";
            wRequestBSN.Referer = Data.Base.BSN_WEB_URL;
            wRequestBSN.ContentType = "application/x-www-form-urlencoded";
            wRequestBSN.CookieContainer = wCookie;
            wRequestBSN.UserAgent = ".NET Application (BST Version : " + "4.0.0.0" + ")";

            wRequestBSN.Timeout = 5000;
            wRequestBSN.ReadWriteTimeout = 5000;
            
            // Start to Write Stream.
            StreamWriter sWriter = new StreamWriter(wRequestBSN.GetRequestStream());

            sWriter.Write("error_return_url=%2Findex.php%3Fmid%3D" + MidURL
                + "%26act%3DdispMemberLoginForm&mid=" + MidURL
                + "&vid=&ruleset=%40login&success_return_url=http%3A%2F%2F"
                + Data.Base.SERVER_IP
                + "%2Findex.php%3Fmid%3D" + MidURL
                + "&act=procMemberLogin&xe_validator_id=modules%2Fmember%2Fskin%2Fdefault%2Flogin_form%2F1"
                + "&user_id=" + email
                + "&password=" + password);
            sWriter.Close();

            // Start to Read Stream.
            string ResponseText = null;

            try
            {
                StreamReader sReader = new StreamReader(wRequestBSN.GetResponse().GetResponseStream(), System.Text.Encoding.UTF8);
                ResponseText = sReader.ReadToEnd();
                sReader.Close();
            }
            catch (Exception ex)
            {
                Class.Common.Message(ex.Message);
                //작업시간이 초과되었습니다.
                return false;
            }
            //Class.Common.Message(ResponseText);
            if (ResponseText == null)
            {
                // 값 에러
                return false;
            }
            else if (ResponseText.IndexOf("<div class=\"login-footer\">") != -1) //(ResponseText.IndexOf("<p>여기는 로그인에 성공해야 들어올 수 있는 미지의 공간입니다.</p>") == -1)
            {
                // 비 로그인 상태
                LoggedIn = false;

                return false;
            }
            else
            {
                // 로그인 성공
                LoggedIn = true;

                return true;
            }
        }

        /// <summary>
        /// 로그인을 요청하는 HttpWebRequest 인스턴스를 생성하고 초기화합니다.
        /// </summary>
        /// <param name="email">로그인에 필요한 이메일</param>
        /// <param name="password">로그인에 필요한 이메일</param>
        /// <returns>초기화 된 HttpWebRequest 인스턴스를 반환합니다.</returns>
        private static HttpWebRequest GetInstance()
        {
            //HttpWebRequest wRequestBSN;

            /*wRequestBSN = (HttpWebRequest)HttpWebRequest.Create(Data.Base.BSN_WEB_URL + "index.php?mid=" + MidURL + "&act=dispMemberLoginForm");
            wRequestBSN.Method = "POST";
            wRequestBSN.Referer = Data.Base.BSN_WEB_URL;
            wRequestBSN.ContentType = "application/x-www-form-urlencoded";
            wRequestBSN.CookieContainer = wCookie;
            wRequestBSN.UserAgent = ".NET Application (BST Version : " + "4.0.0.0" + ")";

            wRequestBSN.Timeout = 5000;
            wRequestBSN.ReadWriteTimeout = 5000;

            return wRequestBSN;*/
            return null;
        }
    }
}
