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
        public static string sendPOST(string adress, NameValueCollection formData, bool defaultInfoAdress = true)
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


        public static string getEmail(string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["member_srl"] = member_srl;

            string data = sendPOST(Servers.Bell_Soft_Network.WEB_BSN_ROOT + "info/member_srl.php", formData, false);
            return Common.getElement(data, "email_address");
        }
    }
}
