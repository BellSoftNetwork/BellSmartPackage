using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using BellLib.Data;
using RegKind = Microsoft.Win32.RegistryValueKind;
using System.Text;
using System.Collections.Specialized;

namespace BellLib.Class.BSN
{
    /// <summary>
    /// Bell Soft Network 공식 홈페이지 회원정보 관련
    /// </summary>
    public static class BSN
    {
        private const string MidURL = "MemberInfo";
        private static CookieContainer wCookie = new CookieContainer();

        /// <summary>
        /// BSN에 로그인 된 상태를 반환합니다.
        /// </summary>
        public static bool LoginStatus { get; set; }

        public static void SaveUserdata(bool email, bool autoLogin)
        {
            // 단순 반복엔 액션!
            Action<bool, RegistryManager> SetOrDeleteValue = delegate(bool b, RegistryManager rm)
            {
                if (b)
                    rm.SetValue();
                else
                    rm.DeleteValue();
            };

            using (RegistryManager rManager = new RegistryManager())
            {
                rManager.RegKind = RegKind.String; // 모든 레지스트리 기록에는 String!

                // email
                rManager.RegPair = new KeyValuePair<string, object>("BSN_Email", User.BSN_Email);
                SetOrDeleteValue(email, rManager);

                // password
                rManager.RegPair = new KeyValuePair<string, object>("BSN_Password", User.BSN_Password);
                SetOrDeleteValue(autoLogin, rManager);

                /*// autoLogin
                rManager.RegPair = new KeyValuePair<string, object>("BSN_AutoLogin", Boolean.TrueString.ToUpper());
                SetOrDeleteValue(autoLogin, rManager);*/
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
                + "&vid=&ruleset=%40login&success_return_url=http%3A%2F%2F" + "www." // 스바 "www." 때매 삽질 ㅡㅡ
                + Servers.Bell_Soft_Network.SERVER_IP
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
                WPFCom.Message(ex.Message, Base.PROJECT.Bell_Smart_Package);
                return false;
            }

            if (p)
                GetUserdata(email, password);
            LoginStatus = p;
            return true;
        }

        private static void GetMember(string originalData)
        {
            string data = Common.getElement(originalData, "member_data");
            User.BSN_member_srl = Common.getElement(data, "member_srl");
            User.BSN_nick_name = Common.getElement(data, "nick_name");
            User.BSN_is_admin = Common.getElement(data, "is_admin");
            User.BSN_group = Common.getElementArray(data, "group");
        }

        /// <summary>
        /// 로그인을 요청하는 HttpWebRequest 인스턴스를 생성하고 초기화합니다.
        /// </summary>
        /// <param name="email">로그인에 필요한 이메일</param>
        /// <param name="password">로그인에 필요한 이메일</param>
        /// <returns>초기화 된 HttpWebRequest 인스턴스를 반환합니다.</returns>
        private static HttpWebRequest GetInstance()
        {
            HttpWebRequest wRequestBSN = (HttpWebRequest)HttpWebRequest.Create(Servers.Bell_Soft_Network.WEB_BSN_ROOT + "index.php?mid=" + MidURL + "&act=dispMemberLoginForm");
            wRequestBSN.Method = "POST";
            wRequestBSN.Referer = Servers.Bell_Soft_Network.WEB_BSN_ROOT;
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

            if (response.Contains("<BSN>이 곳은 Bell Soft Network 홈페이지에 로그인해야 접속할 수 있는 미지의 공간입니다.</BSN>"))
            {
                GetMember(response);
                return true;
            }

            if (response == null) // 값 에러
                return false;
            else if (response.Contains("<div class=\"login-footer\">"))
                return false;
            else if (response.Contains("요청한 기능을 실행할 수 있는 권한이 없습니다."))
                return false;
            else if (response.Contains("기본 URL 설정이 안 되어 있습니다.")) // XE문제로 실패할경우 예외처리
                return false;
            /*else // 로그인 성공
            {
                getMember(response);
                return true;
            }*/

            return false;
        }
    }
}
