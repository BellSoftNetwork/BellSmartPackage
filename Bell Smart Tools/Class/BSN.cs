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
        /*
        public enum MSG : int
        {
            ERR = -1,
            SUCCESS,
            FAIL
        }

        public enum OPT : int
        {
            None = -1,
            EmailSave,
            EmailPWSave,
            AutoLogin
        }

        private static bool g_Login = false;
        private static Net.HttpWebRequest g_BSNhttp;

        private static Net.CookieContainer g_BSNCookie = new Net.CookieContainer();
        public static object BSNLogin(string Email, string PW, sbyte SaveOPT = OPT.None)
        {
            const string MidURL = "MC";

            //필드에 아무것도 없다면
            if (string.IsNullOrEmpty(Email) | string.IsNullOrEmpty(PW))
            {
                return MSG.FAIL;
            }

            //g_BSNhttp.Open("POST", DATA_BASE.BSNWEBURL & "index.php?mid=" & MidURL & "&act=dispMemberLoginForm") '회원로그인폼주소를 입력.
            //g_BSNhttp.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded")
            //g_BSNhttp.Send("error_return_url=%2Findex.php%3Fmid%3D" & MidURL & "%26act%3DdispMemberLoginForm&mid=" & MidURL & "&vid=&ruleset=%40login&success_return_url=http%3A%2F%2F" & DATA_BASE.SERVERIP & "%2Findex.php%3Fmid%3D" & MidURL & "&act=procMemberLogin&xe_validator_id=modules%2Fmember%2Fskin%2Fdefault%2Flogin_form%2F1&user_id=" & ID & "&password=" & PW)
            //g_BSNhttp.WaitForResponse()

            g_BSNhttp = Net.HttpWebRequest.Create(DATA_BASE.BSNWEBURL + "index.php?mid=" + MidURL + "&act=dispMemberLoginForm");
            g_BSNhttp.Method = "POST";
            g_BSNhttp.Referer = DATA_BASE.BSNWEBURL;
            g_BSNhttp.ContentType = "application/x-www-form-urlencoded";
            g_BSNhttp.CookieContainer = g_BSNCookie;
            g_BSNhttp.UserAgent = ".NET Application (BST Version : " + DATA_USER.BST_Current_Version.ToString + ")";
            //BST_Manager.Message(g_BSNhttp.Timeout & vbCrLf & g_BSNhttp.ReadWriteTimeout)
            g_BSNhttp.Timeout = 5000;
            g_BSNhttp.ReadWriteTimeout = 5000;

            StreamWriter sWriter = new StreamWriter(g_BSNhttp.GetRequestStream);

            sWriter.Write("error_return_url=%2Findex.php%3Fmid%3D" + MidURL + "%26act%3DdispMemberLoginForm&mid=" + MidURL + "&vid=&ruleset=%40login&success_return_url=http%3A%2F%2F" + DATA_BASE.SERVERIP + "%2Findex.php%3Fmid%3D" + MidURL + "&act=procMemberLogin&xe_validator_id=modules%2Fmember%2Fskin%2Fdefault%2Flogin_form%2F1&user_id=" + Email + "&password=" + PW);
            sWriter.Close();

            string ResponseText = null;

            try
            {
                StreamReader sReader = new StreamReader(g_BSNhttp.GetResponse.GetResponseStream, System.Text.Encoding.UTF8);
                ResponseText = sReader.ReadToEnd();
                sReader.Close();
            }
            catch (Exception ex)
            {
                //작업시간이 초과되었습니다.
                //BST_Manager.Message(ex.Message) '####################################################################
                return ex.Message;
            }

            if (ResponseText == null)
            {
                return MSG.ERR;
                //비로그인 상태일때
            }
            else if (Convert.ToBoolean(Strings.InStr(ResponseText, "<div class=\"login-footer\">")))
            {
                //g_Login = False

                return MSG.FAIL;
            }
            else
            {
                //SaveOPT 값이 None이 아니고 옵션 최대치인 오토로그인 값 이하일때
                if (!(SaveOPT == OPT.None) & SaveOPT <= OPT.AutoLogin)
                {
                    //Email 저장 요청시
                    if (SaveOPT >= OPT.EmailPWSave)
                    {
                        RegSave("BSN_Email", Protection.Base64(Email, true));
                    }
                    else
                    {
                        RegSave("BSN_Email", null);
                    }

                    //계정 저장 요청시
                    if (SaveOPT >= OPT.EmailPWSave)
                    {
                        RegSave("BSN_PW", Protection.Base64(PW, true));
                    }
                    else
                    {
                        RegSave("BSN_PW", null);
                    }

                    //자동로그인 요청시
                    if (SaveOPT == OPT.AutoLogin)
                    {
                        RegSave("BSN_AutoLogin", "TRUE");
                    }
                    else
                    {
                        RegSave("BSN_AutoLogin", "FALSE");
                    }

                    DATA_USER.BSN_Email = Email;
                    DATA_USER.BSN_PW = PW;
                }
                g_Login = true;

                return MSG.SUCCESS;
            }
        }*/
    }
}
