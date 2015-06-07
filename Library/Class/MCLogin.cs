using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using BellLib.Class;
using BellLib.Data;

namespace BellLib.Class
{
    /// <summary>
    /// 마인크래프트 로그인 관련 메서드와 필드, 열거형 목록을 가지고 있는 클래스입니다.
    /// </summary>
    public static class MCLogin
    {
        /// <summary>
        /// Minecraft에 로그인할 수 있는 타입이 열거되어 있습니다.
        /// </summary>
        public enum LoginType : int
        {
            Authenticate = 0,
            Refresh,
            Validate,
            Signout,
            Invalidate
        }

        /// <summary>
        /// LoginType을 Key로 사용하여 Value인 Endpoint를 제공합니다.
        /// </summary>
        private static Dictionary<LoginType, string> EndPoint = new Dictionary<LoginType, string>()
        {
            {LoginType.Authenticate, "/authenticate"},
            {LoginType.Refresh, "/refresh"},
            {LoginType.Validate, "/validate"},
            {LoginType.Signout, "/signout"},
            {LoginType.Invalidate, "/invalidate"},
        };

        //------------------------------------------------------------------------------
        //author : prownill
        //errored : 405 method not allowed.
        //------------------------------------------------------------------------------
        //Private UUID As Integer = 0
        //Private MinecraftID As String = ""
        //Private MinecraftPW As String = ""
        //Private jsonfile As String
        //Private jsonChared As Char() = ""

        /// <summary>
        /// 지정된 계정으로 Minecraft에 로그인합니다.
        /// </summary>
        /// <param name="id">Minecraft 계정</param>
        /// <param name="pw">Minecraft 비밀번호</param>
        /// <param name="type">로그인 할 LoginType</param>
        /// <returns>성공 여부를 반환합니다.</returns>
        public static bool Login(string id, string pw, LoginType type)
        {
            //setting json file for sending.
            JsonLoginData loginInfo = new JsonLoginData();
            loginInfo.agent.name = "Minecraft";
            loginInfo.agent.version = 1;
            loginInfo.username = id;
            loginInfo.password = pw;

            string output = JsonConvert.SerializeObject(loginInfo);
            //Debug.Message(Debug.Level.High, output);

            //requesting preparation.
            HttpWebRequest wRequest = (HttpWebRequest)HttpWebRequest.Create("https://authserver.mojang.com" + EndPoint[type]);
            byte[] byteBuffer = Encoding.UTF8.GetBytes(output);
            wRequest.Method = "POST";
            wRequest.ContentType = "application/json";
            wRequest.UserAgent = ".NET Framework 4.5 Client Application";

            //make datastream, and send it to minecraft authserver.
            Stream dataStream = wRequest.GetRequestStream();
            dataStream.Write(byteBuffer, 0, byteBuffer.Length);

            dataStream.Close();

            //get response from server.
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)wRequest.GetResponse();
                //403 에러.
            }
            catch (WebException)
            {
                return false;
            }

            dataStream = response.GetResponseStream();
            string responseFromServer = new StreamReader(dataStream).ReadToEnd();

            //parsing UUID, playername to save from responsed data
            //Debug.Message(Debug.Level.High, responseFromServer);

            JsonTextReader jReader = new JsonTextReader(new StringReader(responseFromServer));
            bool[] jArray = { false, false, false, false, false };
            //is accessToken Readed
            //is selectedProfile Readed
            //is id Readed
            //is id value Readed
            //is name Readed

            while ((jReader.Read()))
            {
                if (jReader.Value != null)
                {
                    //Debug.Message(Debug.Level.High, "Token: " + jReader.TokenType + ", Value: " + jReader.Value.ToString());
                }

                //jArray[4]이 True면 값은 name의 값. name을 읽어 BC_NickName에 저장한다. 재진입 방지는 필수!
                if (jArray[4])
                {
                    if (jReader.TokenType == JsonToken.String)
                    {
                        User.MC_NickName = jReader.Value.ToString();
                    }
                    jArray[4] = false;
                }

                //jArray(3)이 True면 값은 name, jArray(4)을 True로 하고 재진입 방지를 위해 jArray(3)는 False
                if (jArray[3])
                {
                    if (jReader.TokenType == JsonToken.PropertyName & jReader.Value.ToString() == "name")
                        jArray[4] = true;
                    jArray[3] = false;
                }

                //jArray(2)이 True면 값은 id의 값. id를 읽어 MC_UUID에 저장한다. 재진입 방지를 위해 jArray(2)은 False
                if (jArray[2])
                {
                    if (jReader.TokenType == JsonToken.String)
                    {
                        User.MC_UUID = jReader.Value.ToString();
                        jArray[3] = true;
                    }
                    jArray[2] = false;
                }

                //jArray(1)이 True면 값은 id, jArray(2)을 True로 하고 재진입 방지를 위해 jArray(1)은 False
                if (jArray[1] & jReader.Value != null)
                {
                    if (jReader.TokenType == JsonToken.PropertyName & jReader.Value.ToString() == "id")
                        jArray[2] = true;
                    jArray[1] = false;
                }

                //jArray(0)이 True면 값은 accessToken의 값, 재진입 방지를 위해 jArray(0)은 False
                if (jArray[0])
                {
                    if (jReader.TokenType == JsonToken.String)
                        //MC_AccessToken = jReader.Value.ToString();
                    // 이게 뭡니까? 어디있는겨
                    jArray[0] = false;
                }


                if (jReader.Value != null)
                {
                    //계속 읽다가 selectedProfile읽히면 jArray(1)이 True
                    if (jReader.TokenType == JsonToken.PropertyName & jReader.Value.ToString() == "selectedProfile")
                        jArray[1] = true;

                    //계속 읽다가 accessToken읽히면 jArray(0)이 True
                    if (jReader.TokenType == JsonToken.PropertyName & jReader.Value.ToString() == "accessToken")
                        jArray[0] = true;

                }
            }

            //Not은 True를 False로, False를 True로 변환함. error가 포함되어 있으면 False 반환. 아니면 True.
            bool value = !responseFromServer.Contains("error");
            //로그인 성공시 True
            //BST_Main.btn_BCLaunch.Enabled = RT;
            User.MC_Login = value;

            RegistryManager rmID = new RegistryManager("MC_ID", value ? id : null);
            RegistryManager rmPW = new RegistryManager("MC_PW", value ? pw : null);

            if (value)
            {
                rmID.SetValue();
                rmID.Dispose();
                User.MC_ID = id;

                rmPW.SetValue();
                rmPW.Dispose();
                User.MC_PW = pw;

                //BST_Manager.BST_Status("마인크래프트 계정 로그인 성공");
            }
            else
            {
                rmID.SetValue();
                rmID.Dispose();
                User.MC_ID = null;

                rmPW.SetValue();
                rmPW.Dispose();
                User.MC_PW = null;

                //BST_Manager.BST_Status("마인크래프트 계정 로그인 실패");
            }

            return value;
        }
    }

    /// <summary>
    /// Minecraft 로그인 데이터입니다.
    /// </summary>
    public class JsonLoginData
    {
        /// <summary>
        /// 내부 클래스인 JsonLoginDataAgent를 초기화하고 agent로 정의합니다.
        /// </summary>
        public JsonLoginDataAgent agent = new JsonLoginDataAgent();

        /// <summary>
        /// Minecraft에 로그인 할 계정입니다.
        /// </summary>
        public string username;

        /// <summary>
        /// Minecraft에 로그인 할 비밀번호입니다.
        /// </summary>
        public string password;

        public string clientToken;
    }

    /// <summary>
    /// Agent에 대한 데이터입니다.
    /// </summary>
    public class JsonLoginDataAgent
    {
        /// <summary>
        /// 로그인을 시도할 Agent를 나타냅니다.
        /// </summary>
        public string name;

        /// <summary>
        /// 로그인을 시도할 Agent의 버전을 나타냅니다.
        /// </summary>
        public int version;
    }
}