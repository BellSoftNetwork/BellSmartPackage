using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Bell_Smart_Tools.Class
{
    public class MCLogin
    {
        public enum LoginType : byte
        {
            Authenticate = 0,
            Refresh,
            Validate,
            Signout,
            Invalidate
        }

        public string[] EndPoint = {
		"/authenticate",
		"/refresh",
		"/validate",
		"/signout",
		"/invalidate"

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


        public object MCLogin(string MCID, string MCPW, LoginType LoginType)
        {

            //setting json file for sending.
            Json_LoginInfo loginInfo = new Json_LoginInfo();
            loginInfo.agent.name = "Minecraft";
            loginInfo.agent.version = 1;
            loginInfo.username = MCID;
            loginInfo.password = MCPW;

            string output = JsonConvert.SerializeObject(loginInfo);
            //Debug.Message(Debug.Level.High, output);

            //requesting preparation.
            HttpWebRequest wRequest = HttpWebRequest.Create("https://authserver.mojang.com" + EndPoint(LoginType));
            byte[] byteBuffer = Encoding.UTF8.GetBytes(output);
            wRequest.Method = "POST";
            wRequest.ContentType = "application/json";
            wRequest.UserAgent = ".NET Framework 4.5 Client Application";

            //make datastream, and send it to minecraft authserver.
            Stream dataStream = wRequest.GetRequestStream();
            dataStream.Write(byteBuffer, 0, byteBuffer.Length);

            dataStream.Close();

            //get response from server.
            HttpWebResponse response = default(HttpWebResponse);
            try
            {
                response = wRequest.GetResponse();
                //403 에러.
            }
            catch (WebException ex)
            {
                return false;
            }
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            //parsing UUID, playername to save from responsed data
            //Debug.Message(Debug.Level.High, responseFromServer);

            JsonTextReader jReader = new JsonTextReader(new StringReader(responseFromServer));
            byte[] jArray = {
			false,
			false,
			false,
			false,
			false
		};
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

                //jArray(4)이 True면 값은 name의 값. name을 읽어 BC_NickName에 저장한다. 재진입 방지는 필수!
                if (jArray(4))
                {
                    var _with1 = jReader;
                    if (_with1.TokenType == JsonToken.String)
                    {
                        Data.User.MC_NickName = _with1.Value.ToString();
                    }
                    jArray(4) = false;
                }

                //jArray(3)이 True면 값은 name, jArray(4)을 True로 하고 재진입 방지를 위해 jArray(3)는 False
                if (jArray(3))
                {
                    var _with2 = jReader;
                    if (_with2.TokenType == JsonToken.PropertyName & _with2.Value.ToString() == "name")
                        jArray(4) = true;
                    jArray(3) = false;
                }

                //jArray(2)이 True면 값은 id의 값. id를 읽어 MC_UUID에 저장한다. 재진입 방지를 위해 jArray(2)은 False
                if (jArray(2))
                {
                    var _with3 = jReader;
                    if (_with3.TokenType == JsonToken.String)
                    {
                        Data.User.MC_UUID = _with3.Value.ToString();
                        jArray(3) = true;
                    }
                    jArray(2) = false;
                }

                //jArray(1)이 True면 값은 id, jArray(2)을 True로 하고 재진입 방지를 위해 jArray(1)은 False
                if (jArray(1) & jReader.Value != null)
                {
                    var _with4 = jReader;
                    if (_with4.TokenType == JsonToken.PropertyName & _with4.Value.ToString() == "id")
                        jArray(2) = true;
                    jArray(1) = false;
                }

                //jArray(0)이 True면 값은 accessToken의 값, 재진입 방지를 위해 jArray(0)은 False
                if (jArray(0))
                {
                    var _with5 = jReader;
                    if (_with5.TokenType == JsonToken.String)
                        MC_AccessToken = _with5.Value.ToString();
                    jArray(0) = false;
                }


                if (jReader.Value != null)
                {
                    //계속 읽다가 selectedProfile읽히면 jArray(1)이 True
                    if (jReader.TokenType == JsonToken.PropertyName & jReader.Value.ToString() == "selectedProfile")
                        jArray(1) = true;

                    //계속 읽다가 accessToken읽히면 jArray(0)이 True
                    if (jReader.TokenType == JsonToken.PropertyName & jReader.Value.ToString() == "accessToken")
                        jArray(0) = true;

                }
            }

            //Not은 True를 False로, False를 True로 변환함. error가 포함되어 있으면 False 반환. 아니면 True.
            bool RT = !responseFromServer.Contains("error");
            //로그인 성공시 True
            //BST_Main.btn_BCLaunch.Enabled = RT;
            Data.User.MC_Login = RT;
            if (RT)
            {
                Class.Common.RegSave("MC_ID", MCID);
                Data.User.MC_ID = MCID;
                Class.Common.RegSave("MC_PW", MCPW);
                Data.User.MC_PW = MCPW;

                //BST_Manager.BST_Status("마인크래프트 계정 로그인 성공");
            }
            else
            {
                Class.Common.RegSave("MC_ID", null);
                Data.User.MC_ID = null;
                Class.Common.RegSave("MC_PW", null);
                Data.User.MC_PW = null;

                //BST_Manager.BST_Status("마인크래프트 계정 로그인 실패");
            }

            return RT;
        }
    }

    public class Json_LoginInfo
    {
        public Json_LoginInfo_Agent agent = new Json_LoginInfo_Agent();
        public string username;
        public string password;
        public string clientToken;
    }

    public class Json_LoginInfo_Agent
    {
        public string name;
        public int version;
    }
}