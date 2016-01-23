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
        /// <param name="name">모드팩 이름</param>
        /// <param name="baseid">베이스팩 id</param>
        /// <param name="detail">모드팩 상세사항</param>
        /// <returns>등록 성공여부</returns>
        public static bool RegisterModPack(string name, string baseid, string detail, string notice, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = "modpack";
            formData["name"] = name;
            formData["baseid"] = baseid;
            formData["detail"] = detail;
            formData["member_srl"] = member_srl;
            formData["notice"] = notice;

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
        /// <param name="name">이름</param>
        /// <param name="MCVer">마인크래프트 버전정보</param>
        /// <returns>등록 성공여부</returns>
        public static bool RegisterBasePack(string name, string MCVer, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = "basepack";
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
        /// <param name="type">팩 타입</param>
        /// <param name="name">팩 이름</param>
        /// <param name="mcversion">MC 버전</param>
        /// <param name="detail">팩 상세정보</param>
        /// <returns>등록 성공여부</returns>
        public static bool RegisterResourcePack(string type, string name, string mcversion, string detail, string notice, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = type;
            formData["name"] = name;
            formData["mcversion"] = mcversion;
            formData["detail"] = detail;
            formData["notice"] = notice;
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

        /// <summary>
        /// 서버 정보를 등록합니다.
        /// </summary>
        /// <param name="name">서버 이름</param>
        /// <param name="type">서버 형태</param>
        /// <param name="upload">업로드 방식</param>
        /// <param name="download">다운로드 방식</param>
        /// <param name="address">서버 주소</param>
        /// <param name="port">서버 포트</param>
        /// <param name="require_plan">업로드시 최소 요금제</param>
        /// <param name="member_srl">BSN 홈페이지 member_srl</param>
        /// <returns>등록 성공여부</returns>
        public static bool RegisterServer(string name, string type, string upload, string download, string address, string port, string require_plan, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = "server";
            formData["name"] = name;
            formData["type"] = type;
            formData["upload"] = upload;
            formData["download"] = download;
            formData["address"] = address;
            formData["port"] = port;
            formData["require_plan"] = require_plan;
            formData["member_srl"] = member_srl;

            string result = BSN_Info.SendPOST(BASEURL + "server.php", formData);
            switch (result)
            {
                case "서버 정보가 정상적으로 등록되었습니다.":
                    return true;

                case "서버 관리자 정보 등록에 실패하였습니다.":
                    return false;

                case "서버 정보 등록에 실패하였습니다.":
                    return false;

                default:
                    return false;
            }
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

        /// <summary>
        /// 검토가 필요한 팩 리스트를 로드합니다.
        /// </summary>
        /// <param name="kind">팩 종류</param>
        /// <returns>팩 이름 배열</returns>
        public static string[] LoadReviewList(BSN_BSL.PACK kind)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["list"] = "pack_review";
            formData["type"] = kind.ToString();

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            return Common.getElementArray(result, "name");
        }

        /// <summary>
        /// 검토가 필요한 버전 리스트를 로드합니다.
        /// </summary>
        /// <param name="kind">팩 종류</param>
        /// <returns>ver 배열</returns>
        public static string[] LoadVersionReviewList(BSN_BSL.PACK kind)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["list"] = "version_review";
            formData["type"] = kind.ToString();

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            return Common.getElementArray(result, "ver");
        }

        #endregion

        #region *** 팩 수정 ***

        /// <summary>
        /// 검토 대기중인 팩을 검토합니다.
        /// </summary>
        /// <param name="kind">팩 종류</param>
        /// <param name="name">이름</param>
        /// <param name="approval">승인 여부</param>
        /// <returns>작업 성공여부</returns>
        public static bool ApprovalPack(BSN_BSL.PACK kind, string name, bool approval)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["review"] = "pack";
            formData["name"] = name;
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
        /// <param name="name">이름</param>
        /// <param name="recommended">권장버전</param>
        /// <param name="activate">활성화 여부</param>
        /// <returns>작업 성공여부</returns>
        public static bool ModifyPackBasic(BSN_BSL.PACK kind, string name, string recommended, bool activate)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["modify"] = "base";
            formData["type"] = kind.ToString();
            formData["name"] = name;
            formData["recommended"] = recommended;
            formData["activate"] = activate.ToString();

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            if (result == "정보 수정 성공")
                return true;
            else
                return false;
        }


        public static bool ModifyPackVersion(BSN_BSL.PACK kind, string verid, bool activate, string basevid = null)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["modify"] = "version";
            formData["type"] = kind.ToString();
            formData["verid"] = verid;
            formData["activate"] = activate.ToString();
            if (basevid != null)
                formData["basevid"] = basevid;

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            if (result == "정보 수정 성공")
                return true;
            else
                return false;
        }

        #endregion

        #region *** 버전 수정 ***

        /// <summary>
        /// 검토 대기중인 버전을 검토합니다.
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="verid"></param>
        /// <param name="approval"></param>
        /// <returns></returns>
        public static bool ApprovalVersion(BSN_BSL.PACK kind, string verid, bool approval)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["review"] = "version";
            formData["id"] = verid;
            formData["type"] = kind.ToString();
            formData["approval"] = approval.ToString();

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            if (result == "검토사항 변경 성공")
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
        /// <param name="name">이름</param>
        /// <param name="email_address">이메일 주소</param>
        /// <param name="permission">권한</param>
        /// <returns>성공 여부</returns>
        public static bool AddPackManager(BSN_BSL.PACK kind, string name, string member_srl, string permission)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["insert"] = "manager";
            formData["type"] = kind.ToString();
            formData["name"] = name;
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
        /// <param name="name">이름</param>
        /// <param name="member_srl">member_srl 값</param>
        /// <param name="permission">권한</param>
        /// <returns>성공 여부</returns>
        public static bool DelPackManager(BSN_BSL.PACK kind, string name, string member_srl, string permission)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["delete"] = "manager";
            formData["type"] = kind.ToString();
            formData["name"] = name;
            formData["member_srl"] = member_srl;
            formData["permission"] = permission;

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            if (result == "관리자 삭제 성공")
                return true;
            else
                return false;
        }

        #endregion

        /// <summary>
        /// 버전정보를 등록합니다.
        /// </summary>
        /// <param name="type">팩 타입</param>
        /// <param name="id">관리자 아이디</param>
        /// <param name="pw">관리자 비밀번호</param>
        /// <param name="name">팩 이름</param>
        /// <param name="version">추가할 버전</param>
        /// <param name="basevid">(옵션) 베이스팩 id</param>
        /// <returns>버전 추가 성공여부</returns>
        public static bool RegisterVersion(BSN_BSL.PACK type, string id, string pw, string name, string version, string basevid = null)
        {
            // 버전정보 등록
            NameValueCollection formData = new NameValueCollection();
            
            formData["type"] = type.ToString();
            formData["id"] = id;
            formData["pw"] = pw;
            formData["insert"] = "version";
            formData["name"] = name;
            formData["version"] = version;

            if (type == BSN_BSL.PACK.modpack)
                formData["basevid"] = basevid;

            string result = BSN_Info.SendPOST(BASEURL + "install.php", formData);
            if (result.Contains("등록 성공"))
                return true;
            return false;
        }


        public static bool RegisterFile(BSN_BSL.PACK type, BSN_BSL.KIND kind, string id, string pw, string verid, string basePath, string[] files, string[] server)
        {
            NameValueCollection formData = new NameValueCollection();
            Protection pro = new Protection();
            string serverList = null;
            string fileList = null;

            foreach (string value in server)
                serverList += value + "|";
            foreach (string value in files)
            {
                FileInfo fi = new FileInfo(basePath + value);
                fileList += value + "|" + pro.MD5Hash(basePath + value) + "|" + fi.Length + "||";
            }

            formData["type"] = type.ToString();
            formData["kind"] = kind.ToString();
            formData["id"] = id;
            formData["pw"] = pw;
            formData["insert"] = "file";
            formData["verid"] = verid;
            formData["server"] = serverList;
            formData["files"] = fileList;
            

            string result = BSN_Info.SendPOST(BASEURL + "install.php", formData);
            if (result.Contains("등록 성공"))
                return true;
            return false;
        }

        /// <summary>
        /// 버전정보를 초기화 합니다.
        /// </summary>
        /// <param name="type">팩 타입</param>
        /// <param name="kind">파일 종류</param>
        /// <param name="id">관리자 id</param>
        /// <param name="pw">관리자 pw</param>
        /// <param name="verid">버전 id</param>
        /// <returns>초기화 성공여부</returns>
        public static bool ResetVersion(BSN_BSL.PACK type, BSN_BSL.KIND kind, string id, string pw, string verid)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["type"] = type.ToString();
            formData["kind"] = kind.ToString();
            formData["id"] = id;
            formData["pw"] = pw;
            formData["reset"] = "version";
            formData["verid"] = verid;


            string result = BSN_Info.SendPOST(BASEURL + "install.php", formData);
            if (result.Contains("초기화 성공"))
                return true;
            return false;
        }

        /// <summary>
        /// 버전 정보를 검토요청합니다.
        /// </summary>
        /// <param name="type">팩 타입</param>
        /// <param name="id">관리자 id</param>
        /// <param name="pw">관리자 pw</param>
        /// <param name="verid">버전 id</param>
        /// <returns>검토요청 성공여부</returns>
        public static bool SubmitVersion(BSN_BSL.PACK type, string id, string pw, string verid)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["type"] = type.ToString();
            formData["id"] = id;
            formData["pw"] = pw;
            formData["submit"] = "version";
            formData["verid"] = verid;


            string result = BSN_Info.SendPOST(BASEURL + "install.php", formData);
            if (result.Contains("등록 성공"))
                return true;
            return false;
        }


        public static bool UploadVersion(BSN_BSL.PACK type, BSN_BSL.KIND kind, string id, string pw, string verid, string serverid, string[] file, string basePath)
        {
            // 서버id를 이용해 서버 세부정보 로드
            // 서버 세부정보를 이용하여 업로드 방식 판단

            // 파일 업로드
            string address = Servers.Bell_Soft_Network.WEB_INFO_ROOT + "BSL/management/upload.php";

            foreach (string dumpPath in file)
            {
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
                        { "type", type.ToString() },
                        { "kind", kind.ToString() },
                        { "url", dumpPath },
                        { "verid", verid },
                        { "id", id },
                        { "pw", pw },
                    };
                    byte[] uploadResult = UploadFiles(address, files, values);
                    string responsefromserver = Encoding.UTF8.GetString(uploadResult);
                    //WinCom.Message(responsefromserver);
                    if (!responsefromserver.Contains("파일 업로드 성공"))
                        return false;
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
