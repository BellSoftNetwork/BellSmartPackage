﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using BellLib.Data;
using RegKind = Microsoft.Win32.RegistryValueKind;

namespace BellLib.Class
{
    /// <summary>
    /// BSN 로그인 관련 메서드와 필드를 가지고 있는 클래스입니다.
    /// </summary>
    public class BSN
    {
        private const string MidURL = "MC";
        private static bool LoggedIn = false;
        private static CookieContainer wCookie = new CookieContainer();

        /// <summary>
        /// BSN에 로그인 된 상태를 반환합니다.
        /// </summary>
        public static bool LoginStatus
        {
            set { LoggedIn = value; }
            get { return LoggedIn; }
        }

        public static void SaveUserdata(bool email, bool password, bool autoLogin)
        {
            using (RegistryManager rManager = new RegistryManager())
            {
                rManager.RegKind = RegKind.String; // 모든 레지스트리 기록에는 String!

                // email
                rManager.RegPair = new KeyValuePair<string, object>("BSN_Email", User.BSN_Email);
                if (email)
                    rManager.SetValue();
                else
                    rManager.DeleteValue();

                // password
                rManager.RegPair = new KeyValuePair<string, object>("BSN_Password", User.BSN_Password);
                if (password)
                    rManager.SetValue();
                else
                    rManager.DeleteValue();

                // autoLogin
                rManager.RegPair = new KeyValuePair<string, object>("BSN_AutoLogin", Boolean.TrueString.ToUpper());
                if (autoLogin)
                    rManager.SetValue();
                else
                    rManager.DeleteValue();
            }

        }

        private static void GetUserdata(string email, string password)
        {
            User.BSN_Email = email;
            User.BSN_Password = password;
        }

        /// <summary>
        /// BSN에 로그인을 수행합니다.
        /// </summary>
        /// <param name="email">로그인에 필요한 패스워드</param>
        /// <param name="password">로그인에 필요한 패스워드</param>
        /// <returns>작업 성공 여부를 반환합니다.</returns>
        public static bool Login(string email, string password)
        {
            ///*HttpWebRequest */wRequestBSN = GetInstance();
            HttpWebRequest wRequestBSN = GetInstance();
            
            // Start to Write Stream.
            StreamWriter sWriter = new StreamWriter(wRequestBSN.GetRequestStream());

            sWriter.Write("error_return_url=%2Findex.php%3Fmid%3D" + MidURL
                + "%26act%3DdispMemberLoginForm&mid=" + MidURL
                + "&vid=&ruleset=%40login&success_return_url=http%3A%2F%2F"
                + Base.SERVER_IP
                + "%2Findex.php%3Fmid%3D" + MidURL
                + "&act=procMemberLogin&xe_validator_id=modules%2Fmember%2Fskin%2Fdefault%2Flogin_form%2F1"
                + "&user_id=" + email
                + "&password=" + password);
            sWriter.Close();

            // Parse Data of Stream
            bool p;

            try
            {
                p = ParseStatus(wRequestBSN.GetResponse().GetResponseStream());
            }
            catch (Exception ex)
            {
                Common.Message(ex.Message);
                return false;
            }

            if (p) { GetUserdata(email, password); }
            LoginStatus = p;
            return p;
        }

        /// <summary>
        /// 로그인을 요청하는 HttpWebRequest 인스턴스를 생성하고 초기화합니다.
        /// </summary>
        /// <param name="email">로그인에 필요한 이메일</param>
        /// <param name="password">로그인에 필요한 이메일</param>
        /// <returns>초기화 된 HttpWebRequest 인스턴스를 반환합니다.</returns>
        private static HttpWebRequest GetInstance()
        {
            HttpWebRequest wRequestBSN = (HttpWebRequest)HttpWebRequest.Create(Base.BSN_WEB_URL + "index.php?mid=" + MidURL + "&act=dispMemberLoginForm");
            wRequestBSN.Method = "POST";
            wRequestBSN.Referer = Base.BSN_WEB_URL;
            wRequestBSN.ContentType = "application/x-www-form-urlencoded";
            wRequestBSN.CookieContainer = wCookie;
            wRequestBSN.UserAgent = ".NET Application (BST Version : " + "4.0.0.0" + ")";

            wRequestBSN.Timeout = 5000;
            wRequestBSN.ReadWriteTimeout = 5000;

            return wRequestBSN;
        }

        /// <summary>
        /// Stream 데이터를 통해 로그인 된 여부를 파싱합니다.
        /// </summary>
        /// <param name="s">파싱 할 Stream 데이터</param>
        /// <returns>로그인 된 여부를 반환합니다.</returns>
        private static bool ParseStatus(Stream s)
        {
            StreamReader sReader = new StreamReader(s, System.Text.Encoding.UTF8);
            string response = sReader.ReadToEnd();
            sReader.Close();

            if (response == null) // 값 에러
                return false;
            else if (response.IndexOf("<div class=\"login-footer\">") != -1) //(ResponseText.IndexOf("<p>여기는 로그인에 성공해야 들어올 수 있는 미지의 공간입니다.</p>") == -1)
                return false;
            else // 로그인 성공
                return true;
        }
    }
}