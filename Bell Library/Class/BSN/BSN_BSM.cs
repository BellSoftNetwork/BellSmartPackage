using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace BellLib.Class.BSN
{
    /// <summary>
    /// Bell Smart Manager 제어관련
    /// </summary>
    public class BSN_BSM
    {
        private const string BASEURL = "BSL/management/";

        /// <summary>
        /// 해당 문자열이 UID 작성법을 준수하는지 검사합니다.
        /// </summary>
        /// <param name="UID">검사할 UID 문자열</param>
        /// <returns>UID 값 유효성 여부</returns>
        public static bool CheckUID(string UID)
        {
            return Regex.IsMatch(UID, @"^[a-zA-Z0-9_]+$");
        }

        #region *** 팩 등록 ***

        /// <summary>
        /// 신규 모드팩을 등록합니다.
        /// </summary>
        /// <param name="UID">UID값</param>
        /// <param name="name">모드팩 이름</param>
        /// <param name="baseid">베이스팩 id</param>
        /// <param name="detail">모드팩 상세사항</param>
        /// <returns>등록 성공여부</returns>
        public static bool RegisterModPack(string UID, string name, string baseid, string detail, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = "modpack";
            formData["UID"] = UID;
            formData["name"] = name;
            formData["baseid"] = baseid;
            formData["detail"] = detail;
            formData["member_srl"] = member_srl;

            string result = BSN_Info.SendPOST(BASEURL + "modpack.php", formData);
            switch (result)
            {
                case "모드팩 정보가 정상적으로 등록되었습니다.":
                    return true;

                case "모드팩 등록에 실패하였습니다.":
                    return false;

                case "모드팩 관리자 정보 등록에 실패하였습니다.":
                    return false;

                case "모드팩 등록정보를 로드하는데 실패하였습니다.":
                    return false;

                default:
                    return false;
            }
        }

        /// <summary>
        /// 베이스팩 정보를 등록합니다.
        /// </summary>
        /// <param name="UID">UID값</param>
        /// <param name="MCVer">마인크래프트 버전정보</param>
        /// <returns>등록 성공여부</returns>
        public static bool RegisterBasePack(string UID, string name, string MCVer, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = "basepack";
            formData["UID"] = UID;
            formData["mcversion"] = MCVer;
            formData["name"] = name;
            formData["member_srl"] = member_srl;

            string result = BSN_Info.SendPOST(BASEURL + "basepack.php", formData);
            switch (result)
            {
                case "베이스팩 정보가 정상적으로 등록되었습니다.":
                    return true;

                case "베이스팩 등록에 실패하였습니다.":
                    return false;

                case "베이스팩 관리자 정보 등록에 실패하였습니다.":
                    return false;

                case "베이스팩 등록정보를 로드하는데 실패하였습니다.":
                    return false;

                default:
                    return false;
            }
        }

        /// <summary>
        /// 리소스팩 정보를 등록합니다.
        /// </summary>
        /// <param name="UID">UID값</param>
        /// <param name="type">팩 타입</param>
        /// <param name="name">팩 이름</param>
        /// <param name="mcversion">MC 버전</param>
        /// <param name="detail">팩 상세정보</param>
        /// <returns>등록 성공여부</returns>
        public static bool RegisterResourcePack(string UID, string type, string name, string mcversion, string detail, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = type;
            formData["UID"] = UID;
            formData["name"] = name;
            formData["mcversion"] = mcversion;
            formData["detail"] = detail;
            formData["member_srl"] = member_srl;

            string result = BSN_Info.SendPOST(BASEURL + "resource.php", formData);
            switch (result)
            {
                case "리소스 정보가 정상적으로 등록되었습니다.":
                    return true;

                case "리소스 등록에 실패하였습니다.":
                    return false;

                case "리소스 관리자 정보 등록에 실패하였습니다.":
                    return false;

                case "리소스 등록정보를 로드하는데 실패하였습니다.":
                    return false;

                default:
                    return false;
            }
        }

        public static bool RegisterServer(string UID, string type, string upload, string download, string name, string address, string port, string require_plan)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = "server";
            formData["UID"] = UID;
            formData["type"] = type;
            formData["upload"] = upload;
            formData["download"] = download;
            formData["name"] = name;
            formData["address"] = address;
            formData["port"] = port;
            formData["require_plan"] = require_plan;

            string result = BSN_Info.SendPOST(BASEURL + "servers.php", formData);
            if (result != "서버 등록 성공")
                return false;
            return true;
        }
        #endregion

        #region *** 리스트 로드 ***

        /// <summary>
        /// 팩 리스트를 로드합니다.
        /// </summary>
        /// <param name="kind">팩 타입</param>
        /// <param name="member_srl">(옵션) member_srl의 관리가능 팩</param>
        /// <returns>팩 정보 배열</returns>
        public static string[] LoadPackList(BSN_BSL.PACK kind, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["list"] = "pack";
            formData["type"] = kind.ToString();
            formData["member_srl"] = member_srl;

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            return Common.getElementArray(result, "pack");
        }

        public static string[] loadReviewList(BSN_BSL.PACK kind)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["list"] = "review";
            formData["type"] = kind.ToString();

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            return Common.getElementArray(result, "UID");
        }

        #endregion

        #region *** 팩 수정 ***

        /// <summary>
        /// 검토 대기중인 팩을 검토합니다.
        /// </summary>
        /// <param name="kind">팩 종류</param>
        /// <param name="UID">UID 값</param>
        /// <param name="approval">승인 여부</param>
        /// <returns>작업 성공여부</returns>
        public static bool ApprovalPack(BSN_BSL.PACK kind, string UID, bool approval)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["review"] = "pack";
            formData["UID"] = UID;
            formData["type"] = kind.ToString();
            formData["approval"] = approval.ToString();

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            if (result == "검토사항 변경 성공")
                return true;
            else
                return false;
        }

        /// <summary>
        /// 팩 기본정보를 수정합니다.
        /// </summary>
        /// <param name="kind">팩 종류</param>
        /// <param name="UID">UID 값</param>
        /// <param name="recommended">권장버전</param>
        /// <param name="activate">활성화 여부</param>
        /// <returns>작업 성공여부</returns>
        public static bool ModifyPackBasic(BSN_BSL.PACK kind, string UID, string recommended, bool activate)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["modify"] = "base";
            formData["type"] = kind.ToString();
            formData["UID"] = UID;
            formData["recommended"] = recommended;
            formData["activate"] = activate.ToString();

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            if (result == "정보 수정 성공")
                return true;
            else
                return false;
        }

        #endregion

        #region *** 관리자 ***

        /// <summary>
        /// 관리자를 추가합니다.
        /// </summary>
        /// <param name="kind">팩 타입</param>
        /// <param name="UID">UID 값</param>
        /// <param name="email_address">이메일 주소</param>
        /// <param name="permission">권한</param>
        /// <returns>성공 여부</returns>
        public static bool AddPackManager(BSN_BSL.PACK kind, string UID, string member_srl, string permission)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["insert"] = "manager";
            formData["type"] = kind.ToString();
            formData["UID"] = UID;
            formData["member_srl"] = member_srl;
            formData["permission"] = permission;

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            if (result == "관리자 등록 성공")
                return true;
            else
                return false;
        }

        /// <summary>
        /// 관리자를 삭제합니다.
        /// </summary>
        /// <param name="kind">팩 타입</param>
        /// <param name="UID">UID 값</param>
        /// <param name="member_srl">member_srl 값</param>
        /// <param name="permission">권한</param>
        /// <returns>성공 여부</returns>
        public static bool DelPackManager(BSN_BSL.PACK kind, string UID, string member_srl, string permission)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["delete"] = "manager";
            formData["type"] = kind.ToString();
            formData["UID"] = UID;
            formData["member_srl"] = member_srl;
            formData["permission"] = permission;

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            if (result == "관리자 삭제 성공")
                return true;
            else
                return false;
        }

        #endregion


        public static bool UploadVersion(BSN_BSL.PACK kind, string id, string pw, string UID, string version, string[] server, string[] file, string basePath, string basevid = null)
        {
            // 버전정보 등록
            NameValueCollection formData = new NameValueCollection();
            string serverList = null;
            foreach (string value in server)
                serverList += value + "|";

            formData["type"] = kind.ToString();
            formData["insert"] = "version";
            formData["UID"] = UID;
            formData["version"] = version;
            formData["server"] = serverList;

            if (kind == BSN_BSL.PACK.modpack)
                formData["basevid"] = basevid;

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            if (result.Contains("버전정보 등록실패") || result.Contains("서버정보 등록실패"))
                return false;

            string verid = Common.getElement(result, "verid");

            // 클라이언트 업로드
            string address = Servers.Bell_Soft_Network.WEB_INFO_ROOT + "BSL/management/upload.php";

            foreach (string dumpPath in file)
            {
                Protection Pro = new Protection();
                string hash = Pro.MD5Hash(basePath + dumpPath);

                using (var stream = File.Open(basePath + dumpPath, FileMode.Open))
                {
                    var files = new[]
                    {
                        new UploadFile
                        {
                            Name = "file",
                            Filename = Path.GetFileName(basePath + dumpPath),
                            ContentType = "text/plain",
                            Stream = stream
                        }
                    };

                    var values = new NameValueCollection
                    {
                        { "type", kind.ToString() },
                        { "verid", verid },
                        { "file", dumpPath },
                        { "hash", hash },
                        { "id", id },
                        { "pw", pw },
                    };

                    byte[] uploadResult = UploadFiles(address, files, values);
                    string responsefromserver = Encoding.UTF8.GetString(uploadResult);
                    //WinCom.Message(responsefromserver);
                }
            }
            return true;
        }

        private static byte[] UploadFiles(string address, IEnumerable<UploadFile> files, NameValueCollection values)
        {
            var request = WebRequest.Create(address);
            request.Method = "POST";
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            boundary = "--" + boundary;

            using (var requestStream = request.GetRequestStream())
            {
                // Write the values
                foreach (string name in values.Keys)
                {
                    var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(values[name] + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                // Write the files
                foreach (var file in files)
                {
                    var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", file.Name, file.Filename, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    file.Stream.CopyTo(requestStream);
                    buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                var boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
                requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
            }

            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var stream = new MemoryStream())
            {
                responseStream.CopyTo(stream);
                return stream.ToArray();
            }
        }
        private class UploadFile
        {
            public UploadFile()
            {
                ContentType = "application/octet-stream";
            }
            public string Name { get; set; }
            public string Filename { get; set; }
            public string ContentType { get; set; }
            public Stream Stream { get; set; }
        }
    }
}
