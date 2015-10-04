using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace BellLib.Class.BSN
{
    /// <summary>
    /// Bell Soft Network 정보서버 제어관련
    /// </summary>
    public class BSN_BSL
    {
        public class Manager
        {
            public string email { get; set; }
            public string permission { get; set; }
        }

        public enum PACK
        {
            ModPack,
            BasePack,
            Resource
        }

        private const string baseURL = "BSL/";

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

            string result = BSN_Info.sendPOST(baseURL + "compack.php", formData);
            return Common.getElementArray(result, "pack");
        }

        #region *** 팩 등록 ***

        /// <summary>
        /// 신규 모드팩을 등록합니다.
        /// </summary>
        /// <param name="MUID">MUID값</param>
        /// <param name="name">모드팩 이름</param>
        /// <param name="baseid">베이스팩 id</param>
        /// <param name="detail">모드팩 상세사항</param>
        /// <returns>등록 성공여부</returns>
        public static bool registerModPack(string MUID, string name, string baseid, string detail, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = "modpack";
            formData["MUID"] = MUID;
            formData["name"] = name;
            formData["baseid"] = baseid;
            formData["detail"] = detail;
            formData["member_srl"] = member_srl;

            string result = BSN_Info.sendPOST(baseURL + "management/modpack.php", formData);
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
        /// <param name="BUID">BUID값</param>
        /// <param name="MCVer">마인크래프트 버전정보</param>
        /// <returns>등록 성공여부</returns>
        public static bool registerBasePack(string BUID, string MCVer, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = "basepack";
            formData["BUID"] = BUID;
            formData["mcversion"] = MCVer;
            formData["member_srl"] = member_srl;

            string result = BSN_Info.sendPOST(baseURL + "management/basepack.php", formData);
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
        /// <param name="RUID">RUID값</param>
        /// <param name="type">팩 타입</param>
        /// <param name="name">팩 이름</param>
        /// <param name="mcversion">MC 버전</param>
        /// <param name="detail">팩 상세정보</param>
        /// <returns>등록 성공여부</returns>
        public static bool registerResourcePack(string RUID, string type, string name, string mcversion, string detail, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = type;
            formData["RUID"] = RUID;
            formData["name"] = name;
            formData["mcversion"] = mcversion;
            formData["detail"] = detail;
            formData["member_srl"] = member_srl;

            string result = BSN_Info.sendPOST(baseURL + "management/resource.php", formData);
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
        #endregion

        #region *** 팩 리스트 로드 ***

        /// <summary>
        /// 관리권한이 있는 모드팩을 로드합니다.
        /// </summary>
        /// <param name="member_srl">멤버 고유번호</param>
        /// <returns>MUID 배열</returns>
        public static string[] loadModPackList(string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["load"] = "modpack";
            formData["member_srl"] = member_srl;

            string result = BSN_Info.sendPOST(baseURL + "management/modpack.php", formData);
            string[] data = Common.getElementArray(result, "MUID");

            return data;
        }

        #endregion

        #region *** 팩 상세정보 로드 ***

        /// <summary>
        /// MUID에 맞는 모드팩의 상세정보를 로드합니다.
        /// </summary>
        /// <param name="MUID">MUID 값</param>
        /// <param name="name">모드팩 이름</param>
        /// <param name="recommended">모드팩 권장버전</param>
        /// <param name="BUID">선택된 BUID</param>
        /// <param name="state">상태</param>
        /// <param name="plan">플랜</param>
        /// <returns>로드 성공 여부</returns>
        public static bool loadModPackDetail(string MUID, out string name, out string latest, out string recommended, out string BUID, out string state, out string plan)//, out string detail, out string start, out string endtime)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["detail"] = "modpack";
            formData["MUID"] = MUID;

            string data = BSN_Info.sendPOST(baseURL + "modpack.php", formData);
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

            name = Common.getElement(data, "name");
            latest = "0.0.0"; // 활성화된 버전리스트를 로드해서 최신버전을 구한다.
            recommended = Common.getElement(data, "recommended");
            string baseid = Common.getElement(data, "baseid");

            formData = new NameValueCollection();

            formData["detail"] = "basepack";
            formData["baseid"] = baseid;
            formData["state"] = "all";

            data = BSN_Info.sendPOST(baseURL + "basepack.php", formData);
            BUID = Common.getElement(data, "BUID");
            /*detail = Common.getElement(data, "detail");
            start = Common.getElement(data, "start");
            endtime = Common.getElement(data, "endtime");*/

            return true;
        }

        #endregion

        #region *** 팩 버전정보 로드 ***

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

            data = BSN_Info.sendPOST(baseURL + "compack.php", formData);
            return Common.getElementArray(data, "version");
        }

        #endregion

        #region *** 팩 매니저 로드 ***

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

            string data = BSN_Info.sendPOST(baseURL + "compack.php", formData);
            List<Manager> list = new List<Manager>();
            foreach (string tmp in Common.getElementArray(data, "manager"))
            {
                Manager mg = new Manager();
                string member_srl = Common.getElement(tmp, "member_srl");
                mg.email = BSN_Info.getEmail(member_srl);
                mg.permission = Common.getElement(tmp, "permission");
                list.Add(mg);
            }

            return list.ToArray<Manager>();
        }
        #endregion
    }
}
