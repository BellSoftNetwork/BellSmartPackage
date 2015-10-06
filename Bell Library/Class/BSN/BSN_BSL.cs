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
        public class Manager
        {
            public string member_srl { get; set; }
            public string email { get; set; }
            public string permission { get; set; }
            public string start { get; set; }
        }

        /// <summary>
        /// 팩 종류 열거형
        /// </summary>
        public enum PACK
        {
            ModPack,
            BasePack,
            Resource
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
        public static string[] loadPackList(PACK kind)
        {
            NameValueCollection formData = new NameValueCollection();
            
            switch (kind)
            {
                case PACK.ModPack:
                    formData["type"] = "modpack";
                    break;

                case PACK.BasePack:
                    formData["type"] = "basepack";
                    break;

                case PACK.Resource:
                    formData["type"] = "resource";
                    break;
            }
            formData["list"] = "pack";

            string result = BSN_Info.sendPOST(BASEURL + "compack.php", formData);
            return Common.getElementArray(result, "pack");
        }

        /// <summary>
        /// 팩 버전 리스트를 로드합니다.
        /// </summary>
        /// <param name="type">팩 타입</param>
        /// <param name="UID">팩 고유ID</param>
        /// <param name="allState">모든 상태 로드여부</param>
        /// <returns>버전 배열</returns>
        public static string[] loadPackVersion(PACK type, string UID, bool allState = false)
        {
            NameValueCollection formData = new NameValueCollection();
            string data, idType = null;

            switch (type)
            {
                case PACK.ModPack:
                    idType = "MUID";
                    break;

                case PACK.BasePack:
                    idType = "BUID";
                    break;

                case PACK.Resource:
                    idType = "RUID";
                    break;

                default:
                    return null;
            }
            formData["list"] = "version";
            formData[idType] = UID;
            if (allState)
                formData["state"] = "all";

            data = BSN_Info.sendPOST(BASEURL + "compack.php", formData);
            return Common.getElementArray(data, "version");
        }

        /// <summary>
        /// 팩 관리자 리스트를 로드합니다.
        /// </summary>
        /// <param name="type">팩 타입</param>
        /// <param name="UID">팩 고유ID</param>
        /// <returns>매니저 배열</returns>
        public static Manager[] loadPackManager(PACK type, string UID)
        {
            NameValueCollection formData = new NameValueCollection();
            string dbType = null;

            switch (type)
            {
                case PACK.ModPack:
                    dbType = "modpack";
                    break;

                case PACK.BasePack:
                    dbType = "basepack";
                    break;

                case PACK.Resource:
                    dbType = "resource";
                    break;
            }

            formData["list"] = "manager";
            formData["type"] = dbType;
            formData["UID"] = UID;

            string data = BSN_Info.sendPOST(BASEURL + "compack.php", formData);
            List<Manager> list = new List<Manager>();
            foreach (string tmp in Common.getElementArray(data, "manager"))
            {
                Manager mg = new Manager();
                mg.member_srl = Common.getElement(tmp, "member_srl");
                mg.email = BSN_Info.getEmail(mg.member_srl);
                mg.permission = Common.getElement(tmp, "permission");
                mg.start = Common.getElement(tmp, "start");
                list.Add(mg);
            }

            return list.ToArray<Manager>();
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
        public static bool loadModPackDetail(string UID, out string id, out string name, out string latest, out string recommended, out string BUID, out string state, out string plan, out string detail, out string start, out string endtime)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["detail"] = "modpack";
            formData["UID"] = UID;

            string data = BSN_Info.sendPOST(BASEURL + "modpack.php", formData);
            state = "사용불가";
            switch (Common.getElement(data, "state"))
            {
                case "-1":
                    state = "사용불가";
                    break;

                case "0":
                    state = "검토 요청";
                    break;

                case "1":
                    state = "비활성화";
                    break;

                case "10":
                    state = "활성화";
                    break;
            }

            plan = "Basic";
            switch (Common.getElement(data, "plan"))
            {
                case "0":
                    plan = "Basic";
                    break;

                case "1":
                    plan = "Premium";
                    break;

                case "2":
                    plan = "Partner";
                    break;

                case "10":
                    plan = "BSN Special";
                    break;
            }

            id = Common.getElement(data, "id");
            name = Common.getElement(data, "name");
            latest = "0.0.0"; // 활성화된 버전리스트를 로드해서 최신버전을 구한다.
            recommended = Common.getElement(data, "recommended");
            detail = Common.getElement(data, "detail");
            start = Common.getElement(data, "start");
            endtime = Common.getElement(data, "endtime");
            string baseid = Common.getElement(data, "baseid");

            formData = new NameValueCollection();

            formData["detail"] = "basepack";
            formData["baseid"] = baseid;
            formData["state"] = "all";

            data = BSN_Info.sendPOST(BASEURL + "basepack.php", formData);
            BUID = Common.getElement(data, "UID");
            
            return true;
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
        public static bool loadBasePackDetail(string UID, out string id, out string latest, out string recommended, out string state, out string mcversion, out string plan, out string start, out string endtime)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["detail"] = "basepack";
            formData["UID"] = UID;

            string data = BSN_Info.sendPOST(BASEURL + "basepack.php", formData);
            state = "사용불가";
            switch (Common.getElement(data, "state"))
            {
                case "-1":
                    state = "사용불가";
                    break;

                case "0":
                    state = "검토 요청";
                    break;

                case "1":
                    state = "비활성화";
                    break;

                case "10":
                    state = "활성화";
                    break;
            }

            plan = "Basic";
            switch (Common.getElement(data, "plan"))
            {
                case "0":
                    plan = "Basic";
                    break;

                case "1":
                    plan = "Premium";
                    break;

                case "2":
                    plan = "Partner";
                    break;

                case "10":
                    plan = "BSN Special";
                    break;
            }

            id = Common.getElement(data, "id");
            mcversion = Common.getElement(data, "mcversion");
            latest = "0.0.0"; // 활성화된 버전리스트를 로드해서 최신버전을 구한다.
            recommended = Common.getElement(data, "recommended");
            start = Common.getElement(data, "start");
            endtime = Common.getElement(data, "endtime");

            return true;
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
        public static bool loadResPackDetail(string UID, out string id, out string type, out string name, out string latest, out string recommended, out string state, out string mcversion, out string plan, out string detail, out string start, out string endtime)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["detail"] = "resource";
            formData["UID"] = UID;

            string data = BSN_Info.sendPOST(BASEURL + "resource.php", formData);
            state = "사용불가";
            switch (Common.getElement(data, "state"))
            {
                case "-1":
                    state = "사용불가";
                    break;

                case "0":
                    state = "검토 요청";
                    break;

                case "1":
                    state = "비활성화";
                    break;

                case "10":
                    state = "활성화";
                    break;
            }

            plan = "Basic";
            switch (Common.getElement(data, "plan"))
            {
                case "0":
                    plan = "Basic";
                    break;

                case "1":
                    plan = "Premium";
                    break;

                case "2":
                    plan = "Partner";
                    break;

                case "10":
                    plan = "BSN Special";
                    break;
            }

            type = "리소스팩";
            switch (Common.getElement(data, "type"))
            {
                case "res":
                    type = "리소스팩";
                    break;

                case "map":
                    type = "맵팩";
                    break;
            }

            id = Common.getElement(data, "id");
            mcversion = Common.getElement(data, "mcversion");
            latest = "0.0.0"; // 활성화된 버전리스트를 로드해서 최신버전을 구한다.
            recommended = Common.getElement(data, "recommended");
            start = Common.getElement(data, "start");
            endtime = Common.getElement(data, "endtime");
            name = Common.getElement(data, "name");
            detail = Common.getElement(data, "detail");

            return true;
        }

        #endregion

    }
}
