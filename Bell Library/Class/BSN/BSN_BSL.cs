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
            public string UID { get; set; }
            public string name { get; set; }
            public string upload { get; set; }
            public string download { get; set; }
            public string plan { get; set; }
        }

        /// <summary>
        /// 모드팩 상세 데이터
        /// </summary>
        public struct ModPack
        {
            public string id, UID, name, recommended, baseid, state, plan, detail, start, endtime; // modpack 테이블
            public string latest, BUID;
            public STATE numState;
            public string[] version;
        }

        /// <summary>
        /// 베이스팩 상세 데이터
        /// </summary>
        public struct BasePack
        {
            public string id, UID, name, recommended, state, mcversion, plan, start, endtime; // basepack 테이블
            public string latest;
            public STATE numState;
            public string[] version;
        }

        /// <summary>
        /// 리소스 상세 데이터
        /// </summary>
        public struct Resource
        {
            public string id, UID, type, name, recommended, state, mcversion, plan, detail, start, endtime; // resource 테이블
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
            resource
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
            PENDING = 0,
            HIDDEN = 1,
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
        /// <param name="UID">팩 고유ID</param>
        /// <param name="allState">모든 상태 로드여부</param>
        /// <returns>버전 배열</returns>
        public static string[] LoadPackVersion(PACK type, string UID, bool allState = false)
        {
            NameValueCollection formData = new NameValueCollection();
            
            formData["list"] = "version";
            formData["UID"] = UID;
            formData["type"] = type.ToString();
            if (allState)
                formData["state"] = "all";

            string data = BSN_Info.SendPOST(BASEURL + "compack.php", formData);
            return Common.getElementArray(data, "version");
        }

        /// <summary>
        /// 팩 관리자 리스트를 로드합니다.
        /// </summary>
        /// <param name="type">팩 타입</param>
        /// <param name="UID">팩 고유ID</param>
        /// <returns>매니저 배열</returns>
        public static Manager[] LoadPackManager(PACK type, string UID)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["list"] = "manager";
            formData["type"] = type.ToString();
            formData["UID"] = UID;

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

            string result = BSN_Info.SendPOST(BASEURL + "servers.php", formData);
            List<Server> list = new List<Server>();
            foreach (string tmp in Common.getElementArray(result, "server"))
            {
                Server sv = new Server();
                sv.id = Common.getElement(tmp, "id");
                sv.UID = Common.getElement(tmp, "UID");
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
                        sv.plan = "Basic";
                        break;

                    case "1":
                        sv.plan = "Premium";
                        break;

                    case "2":
                        sv.plan = "Partner";
                        break;

                    case "10":
                        sv.plan = "BSN Special";
                        break;
                }
                list.Add(sv);
            }

            return list.ToArray<Server>();
        }

        #endregion

        #region *** 팩 상세정보 로드 ***

        /// <summary>
        /// MUID에 맞는 모드팩의 상세정보를 로드합니다.
        /// </summary>
        /// <param name="UID">UID 값</param>
        /// <param name="id">id 값</param>
        /// <param name="name">모드팩 이름</param>
        /// <param name="recommended">모드팩 권장버전</param>
        /// <param name="BUID">선택된 BUID</param>
        /// <param name="state">상태</param>
        /// <param name="plan">플랜</param>
        /// <param name="detail">상세정보</param>
        /// <param name="start">생성일</param>
        /// <param name="endtime">요금제 종료시간</param>
        /// <returns>로드 성공 여부</returns>
        public static ModPack LoadModPackDetail(string UID) //, out string id, out string name, out string latest, out string recommended, out string BUID, out string state, out string plan, out string detail, out string start, out string endtime)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["detail"] = "modpack";
            formData["UID"] = UID;

            string data = BSN_Info.SendPOST(BASEURL + "modpack.php", formData);
            ModPack mp = new ModPack();
            mp.state = "사용불가";
            switch (Common.getElement(data, "state"))
            {
                case "-1":
                    mp.state = "사용불가";
                    break;

                case "0":
                    mp.state = "검토 요청";
                    break;

                case "1":
                    mp.state = "비활성화";
                    break;

                case "10":
                    mp.state = "활성화";
                    break;
            }

            mp.numState = (STATE)Convert.ToInt32(Common.getElement(data, "plan"));
            mp.plan = "Basic";
            switch (Common.getElement(data, "plan"))
            {
                case "0":
                    mp.plan = "Basic";
                    break;

                case "1":
                    mp.plan = "Premium";
                    break;

                case "2":
                    mp.plan = "Partner";
                    break;

                case "10":
                    mp.plan = "BSN Special";
                    break;
            }

            mp.id = Common.getElement(data, "id");
            mp.name = Common.getElement(data, "name");
            mp.latest = "0.0.0"; // 활성화된 버전리스트를 로드해서 최신버전을 구한다.
            mp.recommended = Common.getElement(data, "recommended");
            mp.detail = Common.getElement(data, "detail");
            mp.start = Common.getElement(data, "start");
            mp.endtime = Common.getElement(data, "endtime");
            mp.baseid = Common.getElement(data, "baseid");

            formData = new NameValueCollection();

            formData["detail"] = "basepack";
            formData["baseid"] = mp.baseid;
            formData["state"] = "all";

            data = BSN_Info.SendPOST(BASEURL + "basepack.php", formData);
            mp.BUID = Common.getElement(data, "UID");
            
            return mp;
        }

        /// <summary>
        /// BUID에 맞는 베이스팩 상세정보를 로드합니다.
        /// </summary>
        /// <param name="UID">UID 값</param>
        /// <param name="id">id 값</param>
        /// <param name="recommended">권장버전</param>
        /// <param name="state">팩 상태</param>
        /// <param name="mcversion">마크 버전</param>
        /// <param name="plan">요금제</param>
        /// <param name="start">생성일</param>
        /// <param name="endtime">요금제 종료시간</param>
        /// <returns></returns>
        public static BasePack LoadBasePackDetail(string UID) //, out string id, out string latest, out string recommended, out string state, out string mcversion, out string plan, out string start, out string endtime)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["detail"] = "basepack";
            formData["UID"] = UID;

            string data = BSN_Info.SendPOST(BASEURL + "basepack.php", formData);
            BasePack bp = new BasePack();
            bp.state = "사용불가";
            switch (Common.getElement(data, "state"))
            {
                case "-1":
                    bp.state = "사용불가";
                    break;

                case "0":
                    bp.state = "검토 요청";
                    break;

                case "1":
                    bp.state = "비활성화";
                    break;

                case "10":
                    bp.state = "활성화";
                    break;
            }

            bp.plan = "Basic";
            switch (Common.getElement(data, "plan"))
            {
                case "0":
                    bp.plan = "Basic";
                    break;

                case "1":
                    bp.plan = "Premium";
                    break;

                case "2":
                    bp.plan = "Partner";
                    break;

                case "10":
                    bp.plan = "BSN Special";
                    break;
            }

            bp.id = Common.getElement(data, "id");
            bp.mcversion = Common.getElement(data, "mcversion");
            bp.latest = "0.0.0"; // 활성화된 버전리스트를 로드해서 최신버전을 구한다.
            bp.recommended = Common.getElement(data, "recommended");
            bp.start = Common.getElement(data, "start");
            bp.endtime = Common.getElement(data, "endtime");

            return bp;
        }

        /// <summary>
        /// RUID에 맞는 리소스 상세정보를 로드합니다.
        /// </summary>
        /// <param name="UID">UID 값</param>
        /// <param name="id">id</param>
        /// <param name="type">타입</param>
        /// <param name="name">이름</param>
        /// <param name="latest">최신버전</param>
        /// <param name="recommended">권장버전</param>
        /// <param name="state">팩 상태</param>
        /// <param name="mcversion">마크 버전</param>
        /// <param name="plan">요금제</param>
        /// <param name="detail">상세 정보</param>
        /// <param name="start">생성일</param>
        /// <param name="endtime">요금제 종료일</param>
        /// <returns>성공 여부</returns>
        public static Resource LoadResPackDetail(string UID) //, out string id, out string type, out string name, out string latest, out string recommended, out string state, out string mcversion, out string plan, out string detail, out string start, out string endtime)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["detail"] = "resource";
            formData["UID"] = UID;

            string data = BSN_Info.SendPOST(BASEURL + "resource.php", formData);
            Resource res = new Resource();
            res.state = "사용불가";
            switch (Common.getElement(data, "state"))
            {
                case "-1":
                    res.state = "사용불가";
                    break;

                case "0":
                    res.state = "검토 요청";
                    break;

                case "1":
                    res.state = "비활성화";
                    break;

                case "10":
                    res.state = "활성화";
                    break;
            }

            res.plan = "Basic";
            switch (Common.getElement(data, "plan"))
            {
                case "0":
                    res.plan = "Basic";
                    break;

                case "1":
                    res.plan = "Premium";
                    break;

                case "2":
                    res.plan = "Partner";
                    break;

                case "10":
                    res.plan = "BSN Special";
                    break;
            }

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
            res.latest = "0.0.0"; // 활성화된 버전리스트를 로드해서 최신버전을 구한다.
            res.recommended = Common.getElement(data, "recommended");
            res.start = Common.getElement(data, "start");
            res.endtime = Common.getElement(data, "endtime");
            res.name = Common.getElement(data, "name");
            res.detail = Common.getElement(data, "detail");

            return res;
        }

        #endregion

    }
}
