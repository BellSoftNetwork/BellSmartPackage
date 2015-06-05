using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Bell_Smart_Tools.Class
{
    class BSN
    {
        private static bool g_Login = false;
        private static HttpWebRequest g_BSNhttp;
        private static CookieContainer g_BSNCookie = new CookieContainer();
        public static bool BSNLogin(string Email, string PW)
        {
            const string MidURL = "MC";

            g_BSNhttp = (HttpWebRequest) HttpWebRequest.Create("http://bellsoft.iptime.org/" + "index.php?mid=" + MidURL + "&act=dispMemberLoginForm");
            g_BSNhttp.Method = "POST";
            g_BSNhttp.Referer = "http://bellsoft.iptime.org/";
            g_BSNhttp.ContentType = "application/x-www-form-urlencoded";
            g_BSNhttp.CookieContainer = g_BSNCookie;
            g_BSNhttp.UserAgent = ".NET Application (BST Version : " + "4.0.0.0" + ")";

            g_BSNhttp.Timeout = 5000;
            g_BSNhttp.ReadWriteTimeout = 5000;

            //StreamWriter sWriter = new StreamWriter(g_BSNhttp.GetRequestStream);

            //sWriter.Write("error_return_url=%2Findex.php%3Fmid%3D" + MidURL + "%26act%3DdispMemberLoginForm&mid=" + MidURL + "&vid=&ruleset=%40login&success_return_url=http%3A%2F%2F" + "bellsoft.iptime.org" + "%2Findex.php%3Fmid%3D" + MidURL + "&act=procMemberLogin&xe_validator_id=modules%2Fmember%2Fskin%2Fdefault%2Flogin_form%2F1&user_id=" + Email + "&password=" + PW);
            //sWriter.Close();

            string ResponseText = null;

            try
            {
                StreamReader sReader = new StreamReader(g_BSNhttp.GetResponse().GetResponseStream(), System.Text.Encoding.UTF8);
                ResponseText = sReader.ReadToEnd();
                sReader.Close();
            }
            catch (Exception)
            {
                //작업시간이 초과되었습니다.
                return false;
            }

            if (ResponseText == null)
            {
                return false;
                //비로그인 상태일때
            }
            //else if (Convert.ToBoolean(Strings.InStr(ResponseText, "<div class=\"login-footer\">")))
            {
                //g_Login = False

                return false;
            }
            g_Login = true;

            return true;
        }
    }
}
