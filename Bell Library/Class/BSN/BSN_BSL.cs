using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace BellLib.Class.BSN
{
    /// <summary>
    /// Bell Smart Launcher 제어관련
    /// </summary>
    public class BSN_BSL
    {
        /// <summary>
        /// 팩 매니저 공통 데이터
        /// </summary>
        public class Manager
        {
            public string member_srl { get; set; }
            public string email { get; set; }
            public string permission { get; set; }
            public string start { get; set; }
        }

        /// <summary>
        /// 팩 서버 공통 데이터
        /// </summary>
        public class Server
        {
            public bool select { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string upload { get; set; }
            public string download { get; set; }
            public string address { get; set; }
            public string port { get; set; }
            public string require_plan { get; set; }
            public string state { get; set; }
        }

        /// <summary>
        /// 설치 데이터
        /// </summary>
        public class Install
        {
            public string verid { get; set; }
            public string url { get; set; }
            public string hash { get; set; }
            public string size { get; set; }
        }

        /// <summary>
        /// 모드팩 상세 데이터
        /// </summary>
        public struct ModPack
        {
            public string id, name, recommended, baseid, state, plan, detail, made, modification, notice; // modpack 테이블
            public string latest, BaseName;
            public STATE numState;
            public string[] version;
            public int like;
        }

        /// <summary>
        /// 베이스팩 상세 데이터
        /// </summary>
        public struct BasePack
        {
            public string id, name, recommended, state, mcversion, plan, made, modification; // basepack 테이블
            public string latest;
            public STATE numState;
            public string[] version;
            public int like;
        }

        /// <summary>
        /// 리소스 상세 데이터
        /// </summary>
        public struct Resource
        {
            public string id, type, name, recommended, state, mcversion, plan, detail, made, modification, notice; // resource 테이블
            public string latest;
            public STATE numState;
            public string[] version;
            public int like;
        }
        
        /// <summary>
        /// 런타임 상세 데이터
        /// </summary>
        public struct Runtime
        {
            public string id, name, recommended, made, modification, state;
            public string latest;
            public STATE numState;
            public string[] version;
        }

        /// <summary>
        /// 팩 종류 열거형
        /// </summary>
        public enum PACK
        {
            modpack,
            basepack,
            resource,
            runtime
        }

        /// <summary>
        /// 팩 서버/클라이언트 열거형
        /// </summary>
        public enum KIND
        {
            server,
            client
        }

        /// <summary>
        /// 서버 종류 열거형
        /// </summary>
        public enum SERVER
        {
            cloud,
            info
        }

        /// <summary>
        /// 상태 열거형
        /// </summary>
        public enum STATE
        {
            BANNED = -1,
            PREPARE = 0,
            PENDING = 1,
            HIDDEN = 2,
            ACTIVATE = 10
        }

        /// <summary>
        /// 요금제 열거형
        /// </summary>
        public enum PLAN
        {
            Basic = 0,
            Premium = 1,
            Partner = 2,
            BSN_Special = 10
        }

        private const string BASEURL = "BSL/";

        /// <summary>
        /// 상태 코드에 따른 메시지를 출력합니다.
        /// </summary>
        /// <param name="state">상태값</param>
        /// <returns>상태 내용 텍스트</returns>
        public static string GetStateName(STATE state)
        {
            switch (state)
            {
                case STATE.BANNED:
                    return "사용 정지";

                case STATE.PENDING:
                    return "검토 대기중";

                case STATE.PREPARE:
                    return "준비중";

                case STATE.HIDDEN:
                    return "비활성화";

                case STATE.ACTIVATE:
                    return "활성화";

                default:
                    return null;
            }
        }

        /// <summary>
        /// 요금제 코드에 따른 메시지를 출력합니다.
        /// </summary>
        /// <param name="plan">요금제 값</param>
        /// <returns>요금제 내용 텍스트</returns>
        public static string GetPlanName(PLAN plan)
        {
            switch (plan)
            {
                case PLAN.Basic:
                    return "Basic";

                case PLAN.Premium:
                    return "Premium";

                case PLAN.Partner:
                    return "Partner";

                case PLAN.BSN_Special:
                    return "BSN Special";

                default:
                    return null;
            }
        }

        #region *** 리스트 로드 ***

        /// <summary>
        /// 팩 리스트를 로드합니다.
        /// </summary>
        /// <param name="kind">팩 타입</param>
        /// <param name="member_srl">(옵션) member_srl의 관리가능 팩</param>
        /// <returns>팩 정보 배열</returns>
        public static string[] LoadPackList(PACK kind)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["list"] = "pack";
            formData["type"] = kind.ToString();

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            return Common.getElementArray(result, "pack");
        }

        /// <summary>
        /// 팩 버전 리스트를 로드합니다.
        /// </summary>
        /// <param name="type">팩 타입</param>
        /// <param name="name">이름</param>
        /// <param name="allState">모든 상태 로드여부</param>
        /// <param name="LatestOrder">최신버전 순서로 출력 여부</param>
        /// <returns>버전 배열</returns>
        public static string[] LoadPackVersionList(PACK type, string name, bool allState = false, bool LatestOrder = true)
        {
            NameValueCollection formData = new NameValueCollection();
            string[] output;

            formData["list"] = "version";
            formData["name"] = name;
            formData["type"] = type.ToString();
            if (allState)
                formData["state"] = "all";

            string data = BSN_Info.SendPOST(BASEURL + "compack.php", formData);

            if (data != string.Empty)
                output = Common.getElementArray(data, "ver");
            else
                return null;

            if (LatestOrder)
                Array.Reverse(output);

            return output;
        }

        /// <summary>
        /// 팩 버전 리스트를 로드합니다.
        /// </summary>
        /// <param name="type">팩 타입</param>
        /// <param name="name">이름</param>
        /// <param name="state">로드할 상태값</param>
        /// <param name="LatestOrder">최신버전 순서로 출력 여부</param>
        /// <returns>버전 배열</returns>
        public static string[] LoadPackVersionList(PACK type, string name, STATE state, bool LatestOrder = true)
        {
            NameValueCollection formData = new NameValueCollection();
            string[] output;

            formData["list"] = "version";
            formData["name"] = name;
            formData["type"] = type.ToString();
            formData["state"] = Convert.ToInt32(state).ToString();

            string data = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            if (data != string.Empty)
                output = Common.getElementArray(data, "ver");
            else
                return null;

            if (LatestOrder)
                Array.Reverse(output);

            return output;
        }

        /// <summary>
        /// 팩 관리자 리스트를 로드합니다.
        /// </summary>
        /// <param name="type">팩 타입</param>
        /// <param name="name">이름</param>
        /// <returns>매니저 배열</returns>
        public static Manager[] LoadPackManager(PACK type, string name)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["list"] = "manager";
            formData["type"] = type.ToString();
            formData["name"] = name;

            string data = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            List<Manager> list = new List<Manager>();
            foreach (string tmp in Common.getElementArray(data, "manager"))
            {
                Manager mg = new Manager();
                mg.member_srl = Common.getElement(tmp, "member_srl");
                mg.email = BSN_Info.GetEmail(mg.member_srl);
                mg.permission = Common.getElement(tmp, "permission");
                mg.start = Common.getElement(tmp, "start");
                list.Add(mg);
            }

            return list.ToArray<Manager>();
        }

        /// <summary>
        /// 서버 리스트를 로드합니다.
        /// </summary>
        /// <param name="server">서버 형식</param>
        /// <returns>서버 리스트 배열</returns>
        public static Server[] LoadServerList(SERVER server)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["list"] = "server";
            formData["type"] = server.ToString();

            string result = BSN_Info.SendPOST(BASEURL + "server.php", formData);
            List<Server> list = new List<Server>();
            foreach (string tmp in Common.getElementArray(result, "server"))
            {
                Server sv = new Server();
                sv.id = Common.getElement(tmp, "id");
                sv.name = Common.getElement(tmp, "name");
                //sv.upload = Common.getElement(tmp, "upload");
                switch (Common.getElement(tmp, "upload"))
                {
                    case "0":
                        sv.upload = "None";
                        break;

                    case "1":
                        sv.upload = "PHP";
                        break;

                    case "2":
                        sv.upload = "FTP";
                        break;
                }
                //sv.download = Common.getElement(tmp, "download");
                switch (Common.getElement(tmp, "download"))
                {
                    case "0":
                        sv.download = "None";
                        break;

                    case "1":
                        sv.download = "Direct";
                        break;

                    case "2":
                        sv.download = "PHP";
                        break;
                }
                //sv.plan = Common.getElement(tmp, "require_plan");
                switch (Common.getElement(tmp, "require_plan"))
                {
                    case "0":
                        sv.require_plan = "Basic";
                        break;

                    case "1":
                        sv.require_plan = "Premium";
                        break;

                    case "2":
                        sv.require_plan = "Partner";
                        break;

                    case "10":
                        sv.require_plan = "BSN Special";
                        break;
                }
                list.Add(sv);
            }

            return list.ToArray<Server>();
        }

        #endregion

        #region *** 팩 상세정보 로드 ***

        /// <summary>
        /// 이름에 맞는 모드팩의 상세정보를 로드합니다.
        /// </summary>
        /// <param name="name">이름</param>
        /// <returns>모드팩 상세정보</returns>
        public static ModPack LoadModPackDetail(string name)
        {
            NameValueCollection formData = new NameValueCollection();
            List<string> list = new List<string>();

            formData["detail"] = "modpack";
            formData["name"] = name;

            string data = BSN_Info.SendPOST(BASEURL + "modpack.php", formData);
            ModPack mp = new ModPack();
            mp.state = GetStateName((STATE)Convert.ToInt32(Common.getElement(data, "state")));
            mp.numState = (STATE)Convert.ToInt32(Common.getElement(data, "state"));
            mp.plan = GetPlanName((PLAN)Convert.ToInt32(Common.getElement(data, "plan")));

            mp.id = Common.getElement(data, "id");
            mp.name = Common.getElement(data, "name");
            try
            {
                foreach (string value in LoadPackVersionList(PACK.modpack, name))
                    list.Add(Common.getElement(value, "version"));
            }
            catch { }
            mp.version = list.ToArray();
            try
            {
                mp.latest = mp.version[0];
            }
            catch
            {
                mp.latest = "0.0.0";
            }
            mp.recommended = Common.getElement(data, "recommended");
            mp.detail = Common.getElement(data, "detail");
            mp.made = Common.getElement(data, "made");
            mp.modification = Common.getElement(data, "modification");
            mp.notice = Common.getElement(data, "notice");
            mp.baseid = Common.getElement(data, "baseid");
            mp.plan = Common.getElement(data, "plan");
            try
            {
                mp.like = Convert.ToInt32(Common.getElement(data, "like"));
            }
            catch
            {
                mp.like = 0;
            }

            formData = new NameValueCollection();

            formData["detail"] = "basepack";
            formData["id"] = mp.baseid;
            formData["state"] = "all";

            data = BSN_Info.SendPOST(BASEURL + "basepack.php", formData);
            mp.BaseName = Common.getElement(data, "name");
            
            return mp;
        }

        /// <summary>
        /// 이름에 맞는 베이스팩 상세정보를 로드합니다.
        /// </summary>
        /// <param name="input">이름 또는 id 값</param>
        /// <param name="FindID">id로 검색할지 여부 (기본 이름검색)</param>
        /// <returns>베이스팩 상세정보</returns>
        public static BasePack LoadBasePackDetail(string input, bool FindID = false)
        {
            NameValueCollection formData = new NameValueCollection();
            List<string> list = new List<string>();

            formData["detail"] = "basepack";
            if (!FindID)
                formData["name"] = input;
            else
                formData["id"] = input;

            string data = BSN_Info.SendPOST(BASEURL + "basepack.php", formData);
            BasePack bp = new BasePack();
            bp.state = GetStateName((STATE)Convert.ToInt32(Common.getElement(data, "state")));
            bp.numState = (STATE)Convert.ToInt32(Common.getElement(data, "state"));
            bp.plan = GetPlanName((PLAN)Convert.ToInt32(Common.getElement(data, "plan")));
            bp.id = Common.getElement(data, "id");
            bp.name = Common.getElement(data, "name");
            bp.mcversion = Common.getElement(data, "mcversion");
            try
            {
                foreach (string value in LoadPackVersionList(PACK.basepack, bp.name))
                    list.Add(Common.getElement(value, "version"));
            }
            catch { }
            bp.version = list.ToArray();
            try
            {
                bp.latest = bp.version[0];
            }
            catch
            {
                bp.latest = "0.0.0";
            }
            bp.recommended = Common.getElement(data, "recommended");
            bp.made = Common.getElement(data, "made");
            bp.modification = Common.getElement(data, "modification");
            bp.plan = Common.getElement(data, "plan");
            try
            {
                bp.like = Convert.ToInt32(Common.getElement(data, "like"));
            }
            catch
            {
                bp.like = 0;
            }

            return bp;
        }

        /// <summary>
        /// 이름에 맞는 리소스 상세정보를 로드합니다.
        /// </summary>
        /// <param name="name">이름</param>
        /// <returns>리소스 상세정보</returns>
        public static Resource LoadResPackDetail(string name)
        {
            NameValueCollection formData = new NameValueCollection();
            List<string> list = new List<string>();

            formData["detail"] = "resource";
            formData["name"] = name;

            string data = BSN_Info.SendPOST(BASEURL + "resource.php", formData);
            Resource res = new Resource();
            res.state = GetStateName((STATE)Convert.ToInt32(Common.getElement(data, "state")));
            res.numState = (STATE)Convert.ToInt32(Common.getElement(data, "state"));
            res.plan = GetPlanName((PLAN)Convert.ToInt32(Common.getElement(data, "plan")));
            res.type = "리소스팩";
            switch (Common.getElement(data, "type"))
            {
                case "res":
                    res.type = "리소스팩";
                    break;

                case "map":
                    res.type = "맵팩";
                    break;
            }

            res.id = Common.getElement(data, "id");
            res.mcversion = Common.getElement(data, "mcversion");
            foreach (string value in LoadPackVersionList(PACK.resource, name))
                list.Add(Common.getElement(value, "version"));
            res.version = list.ToArray();
            try
            {
                res.latest = res.version[0];
            }
            catch
            {
                res.latest = "0.0.0";
            }
            res.recommended = Common.getElement(data, "recommended");
            res.made = Common.getElement(data, "made");
            res.modification = Common.getElement(data, "modification");
            res.notice = Common.getElement(data, "notice");
            res.name = Common.getElement(data, "name");
            res.detail = Common.getElement(data, "detail");
            res.plan = Common.getElement(data, "plan");
            try
            {
                res.like = Convert.ToInt32(Common.getElement(data, "like"));
            }
            catch
            {
                res.like = 0;
            }

            return res;
        }

        /// <summary>
        /// 이름에 맞는 런타임 상세정보를 로드합니다.
        /// </summary>
        /// <param name="name">이름</param>
        /// <returns>런타임 상세정보</returns>
        public static Runtime LoadRuntimeDetail(string name)
        {
            NameValueCollection formData = new NameValueCollection();
            List<string> list = new List<string>();

            formData["detail"] = "runtime";
            formData["name"] = name;

            string data = BSN_Info.SendPOST(BASEURL + "runtime.php", formData);
            Runtime run = new Runtime();
            run.state = GetStateName((STATE)Convert.ToInt32(Common.getElement(data, "state")));
            run.numState = (STATE)Convert.ToInt32(Common.getElement(data, "state"));
            run.id = Common.getElement(data, "id");
            run.name = Common.getElement(data, "name");
            foreach (string value in LoadPackVersionList(PACK.runtime, name))
                list.Add(Common.getElement(value, "version"));
            run.version = list.ToArray();
            try
            {
                run.latest = run.version[0];
            }
            catch
            {
                run.latest = "0.0.0";
            }
            run.recommended = Common.getElement(data, "recommended");
            run.made = Common.getElement(data, "made");
            run.modification = Common.getElement(data, "modification");

            return run;
        }

        /// <summary>
        /// 서버 id에 맞는 서버 상세정보를 로드합니다.
        /// </summary>
        /// <param name="serverid">서버 id</param>
        /// <returns>서버 상세정보</returns>
        public static Server LoadServerDetail(string serverid)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["detail"] = "server";
            formData["id"] = serverid;

            string data = BSN_Info.SendPOST(BASEURL + "server.php", formData);
            Server sv = new Server();
            sv.id = Common.getElement(data, "id");
            sv.name = Common.getElement(data, "name");
            sv.type = Common.getElement(data, "type");
            sv.upload = Common.getElement(data, "upload");
            sv.download = Common.getElement(data, "download");
            sv.address = Common.getElement(data, "address");
            sv.port = Common.getElement(data, "port");
            sv.require_plan = Common.getElement(data, "require_plan");
            sv.state = Common.getElement(data, "state");

            return sv;
        }

        /// <summary>
        /// 버전 상세정보를 로드합니다.
        /// </summary>
        /// <param name="kind">팩 종류</param>
        /// <param name="verid">버전 id</param>
        /// <returns>버전 상세정보</returns>
        public static string LoadVersionDetail(PACK kind, string verid)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["type"] = kind.ToString();
            formData["detail"] = "version";
            formData["id"] = verid;
            
            return BSN_Info.SendPOST(BASEURL + "compack.php", formData);
        }

        #endregion

        #region *** 설치 정보 로드 ***

        /// <summary>
        /// 선택한 팩 버전의 파일들이 업로드되어있는 서버리스트를 반환합니다.
        /// </summary>
        /// <param name="type">팩 종류</param>
        /// <param name="verid">버전 id</param>
        /// <returns>서버id 배열</returns>
        public static string[] LoadVersionServer(PACK type, KIND kind, string verid)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["type"] = type.ToString();
            formData["kind"] = kind.ToString();
            formData["install"] = "upload";
            formData["verid"] = verid;

            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            return Common.getElementArray(result, "serverid");
        }

        /// <summary>
        /// 선택한 팩 버전의 설치 요구 파일리스트를 반환합니다.
        /// </summary>
        /// <param name="kind">팩 종류</param>
        /// <param name="verid">버전 id</param>
        /// <returns>설치데이터 배열 (file, hash)</returns>
        public static Install[] LoadVersionFiles(PACK type, KIND kind, string verid)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["type"] = type.ToString();
            formData["kind"] = kind.ToString();
            formData["install"] = "files";
            formData["verid"] = verid;
            
            List<Install> insList = new List<Install>();
            string result = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            string[] data = Common.getElementArray(result, "install");
            foreach (string @value in data)
            {
                string temp = @value;
                Install ins = new Install();
                ins.verid = verid;
                ins.url = @Common.getElement(value, "url");
                ins.hash = Common.getElement(value, "hash");
                ins.size = Common.getElement(value, "size");
                insList.Add(ins);
            }
            return insList.ToArray();
        }

        #endregion

        #region *** 좋아요 ***

        public static bool isLikedPack(PACK type, string packid)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["type"] = type.ToString();
            formData["like"] = "load";
            formData["packid"] = packid;

            string strReturn = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            if (Common.getElement(strReturn, "packid") == packid)
                return true;
            else
                return false;
        }

        public static bool setLikePack(PACK type, string packid)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["type"] = type.ToString();
            formData["like"] = "set";
            formData["packid"] = packid;

            string strReturn = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            if (strReturn.Contains("좋아합니다."))
                return true;
            else
                return false;
        }

        public static bool delLikePack(PACK type, string packid)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["type"] = type.ToString();
            formData["like"] = "delete";
            formData["packid"] = packid;

            string strReturn = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            if (strReturn.Contains("좋아요 취소 성공."))
                return true;
            else
                return false;
        }

        #endregion
    }
}
