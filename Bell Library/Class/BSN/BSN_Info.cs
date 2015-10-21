using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace BellLib.Class.BSN
{
    public class BSN_Info
    {
        /// <summary>
        /// PHP 웹페이지에 POST값 전송 후 결과값을 받아옵니다.
        /// </summary>
        /// <param name="adress">정보 주소</param>
        /// <param name="formData">POST 값</param>
        /// <returns>POST 전송 후 결과값</returns>
        public static string SendPOST(string adress, NameValueCollection formData, bool defaultInfoAdress = true)
        {
            WebClient webClient = new WebClient();
            if (defaultInfoAdress)
                adress = Servers.Bell_Soft_Network.WEB_INFO_ROOT + adress;
            string result = null;

            try
            {
                byte[] responseBytes = webClient.UploadValues(adress, "POST", formData);
                result = Encoding.UTF8.GetString(responseBytes);
            }
            catch (Exception e)
            {
                string temp = e.Message;
            }
            webClient.Dispose();

            return result;
        }

        /// <summary>
        /// member_srl 값으로 이메일주소를 가져옵니다.
        /// </summary>
        /// <param name="member_srl">member_srl 값</param>
        /// <returns>이메일 주소</returns>
        public static string GetEmail(string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["member_srl"] = member_srl;

            string data = SendPOST(Servers.Bell_Soft_Network.WEB_INFO_ROOT + "BSN/member_srl.php", formData, false);
            return Common.getElement(data, "email_address");
        }

        /// <summary>
        /// member_srl 값으로 닉네임을 가져옵니다.
        /// </summary>
        /// <param name="member_srl">member_srl 값</param>
        /// <returns>닉네임</returns>
        public static string GetNickName(string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["member_srl"] = member_srl;

            string data = SendPOST(Servers.Bell_Soft_Network.WEB_INFO_ROOT + "BSN/member_srl.php", formData, false);
            return Common.getElement(data, "nick_name");
        }

        /// <summary>
        /// 이메일 주소로 member_srl값을 가져옵니다.
        /// </summary>
        /// <param name="email_address">이메일 주소</param>
        /// <returns>member_srl</returns>
        public static string GetMember_srl(string email_address)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["email_address"] = email_address;

            string data = SendPOST(Servers.Bell_Soft_Network.WEB_INFO_ROOT + "BSN/member_email.php", formData, false);
            return Common.getElement(data, "member_srl");
        }
    }
}
