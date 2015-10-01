using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using BellLib.Data;
using RegKind = Microsoft.Win32.RegistryValueKind;
using System.Text;
using System.Collections.Specialized;

namespace BellLib.Class
{
    /// <summary>
    /// Bell Soft Network 공식 홈페이지 회원정보 관련
    /// </summary>
    public static class BSN
    {
        private const string MidURL = "MC";
        private static CookieContainer wCookie = new CookieContainer();

        /// <summary>
        /// BSN에 로그인 된 상태를 반환합니다.
        /// </summary>
        public static bool LoginStatus { get; set; }

        public static void SaveUserdata(bool email, bool password, bool autoLogin)
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
                SetOrDeleteValue(password, rManager);

                // autoLogin
                rManager.RegPair = new KeyValuePair<string, object>("BSN_AutoLogin", Boolean.TrueString.ToUpper());
                SetOrDeleteValue(autoLogin, rManager);
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
                WinCom.Message(ex.Message);
                return false;
            }

            if (p) { GetUserdata(email, password); }
            LoginStatus = p;
            return true;
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

            if (response == null) // 값 에러
                return false;
            else if (response.IndexOf("<div class=\"login-footer\">") != -1) //(ResponseText.IndexOf("<p>여기는 로그인에 성공해야 들어올 수 있는 미지의 공간입니다.</p>") == -1)
                return false;
            else // 로그인 성공
                return true;
        }
    }

    /// <summary>
    /// Bell Soft Network 정보서버 제어관련
    /// </summary>
    public class BSN_Info
    {
        private static string baseURL = Servers.Bell_Soft_Network.WEB_INFO_ROOT + "BSL/";

        public enum PACK
        {
            ModPack,
            BasePack
        }

        public static string[] loadPackList(PACK kind)
        {
            NameValueCollection formData = new NameValueCollection();
            string result = null;

            switch (kind)
            {
                case PACK.ModPack:
                    formData["list"] = "modpack";
                    result = sendPOST(baseURL + "modpack.php", formData);
                    break;

                case PACK.BasePack:
                    formData["list"] = "basepack";
                    result = sendPOST(baseURL + "basepack.php", formData);
                    break;
            }

            return Common.getElementArray(result, "pack");
        }

        /// <summary>
        /// 신규 모드팩을 등록합니다.
        /// </summary>
        /// <param name="MUID">MUID값</param>
        /// <param name="name">모드팩 이름</param>
        /// <param name="baseid">베이스팩 id</param>
        /// <param name="detail">모드팩 상세사항</param>
        /// <returns>등록 성공여부</returns>
        public static bool registerModPack(string MUID, string name, string baseid, string detail)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = "modpack";
            formData["MUID"] = MUID;
            formData["name"] = name;
            formData["baseid"] = baseid;
            formData["detail"] = detail;

            string result = sendPOST(baseURL + "management/modpack.php", formData);
            switch (result)
            {
                case "모드팩 정보가 정상적으로 등록되었습니다.":
                    return true;

                case "모드팩 등록에 실패하였습니다.":
                    return false;

                default:
                    return false;
            }
        }

        /// <summary>
        /// 베이스팩 정보를 등록합니다.
        /// </summary>
        /// <param name="BUID">BUID값</param>
        /// <param name="MCVer">마인크래프트 버전정보</param>
        /// <returns>등록 성공여부</returns>
        public static bool registerBasePack(string BUID, string MCVer)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = "basepack";
            formData["BUID"] = BUID;
            formData["mcversion"] = MCVer;

            string result = sendPOST(baseURL + "management/basepack.php", formData);
            switch (result)
            {
                case "베이스팩 정보가 정상적으로 등록되었습니다.":
                    return true;

                case "베이스팩 등록에 실패하였습니다.":
                    return false;

                default:
                    return false;
            }
        }

        /// <summary>
        /// PHP 웹페이지에 POST값 전송 후 결과값을 받아옵니다.
        /// </summary>
        /// <param name="adress">웹 주소</param>
        /// <param name="formData">POST 값</param>
        /// <returns>POST 전송 후 결과값</returns>
        private static string sendPOST(string adress, NameValueCollection formData)
        {
            WebClient webClient = new WebClient();
            byte[] responseBytes = webClient.UploadValues(adress, "POST", formData);
            string result = Encoding.UTF8.GetString(responseBytes);
            webClient.Dispose();

            return result;
        }
    }
}
