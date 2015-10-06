using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
        public static bool checkUID(string UID)
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
        public static bool registerModPack(string UID, string name, string baseid, string detail, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = "modpack";
            formData["UID"] = UID;
            formData["name"] = name;
            formData["baseid"] = baseid;
            formData["detail"] = detail;
            formData["member_srl"] = member_srl;

            string result = BSN_Info.sendPOST(BASEURL + "modpack.php", formData);
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
        public static bool registerBasePack(string UID, string MCVer, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = "basepack";
            formData["UID"] = UID;
            formData["mcversion"] = MCVer;
            formData["member_srl"] = member_srl;

            string result = BSN_Info.sendPOST(BASEURL + "basepack.php", formData);
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
        public static bool registerResourcePack(string UID, string type, string name, string mcversion, string detail, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();

            formData["insert"] = type;
            formData["UID"] = UID;
            formData["name"] = name;
            formData["mcversion"] = mcversion;
            formData["detail"] = detail;
            formData["member_srl"] = member_srl;

            string result = BSN_Info.sendPOST(BASEURL + "resource.php", formData);
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

        #region *** 리스트 로드 ***

        /// <summary>
        /// 팩 리스트를 로드합니다.
        /// </summary>
        /// <param name="kind">팩 타입</param>
        /// <param name="member_srl">(옵션) member_srl의 관리가능 팩</param>
        /// <returns>팩 정보 배열</returns>
        public static string[] loadPackList(BSN_BSL.PACK kind, string member_srl)
        {
            NameValueCollection formData = new NameValueCollection();
            
            switch (kind)
            {
                case BSN_BSL.PACK.ModPack:
                    formData["type"] = "modpack";
                    break;

                case BSN_BSL.PACK.BasePack:
                    formData["type"] = "basepack";
                    break;

                case BSN_BSL.PACK.Resource:
                    formData["type"] = "resource";
                    break;
            }
            formData["list"] = "pack";
            formData["member_srl"] = member_srl;

            string result = BSN_Info.sendPOST(BASEURL + "compack.php", formData);
            return Common.getElementArray(result, "pack");
        }

        public static string[] loadReviewList(BSN_BSL.PACK kind)
        {
            NameValueCollection formData = new NameValueCollection();
            
            switch (kind)
            {
                case BSN_BSL.PACK.ModPack:
                    formData["type"] = "modpack";
                    break;

                case BSN_BSL.PACK.BasePack:
                    formData["type"] = "basepack";
                    break;

                case BSN_BSL.PACK.Resource:
                    formData["type"] = "resource";
                    break;
            }
            formData["list"] = "review";

            string result = BSN_Info.sendPOST(BASEURL + "compack.php", formData);
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
        public static bool approvalPack(BSN_BSL.PACK kind, string UID, bool approval)
        {
            NameValueCollection formData = new NameValueCollection();

            switch (kind)
            {
                case BSN_BSL.PACK.ModPack:
                    formData["type"] = "modpack";
                    break;

                case BSN_BSL.PACK.BasePack:
                    formData["type"] = "basepack";
                    break;

                case BSN_BSL.PACK.Resource:
                    formData["type"] = "resource";
                    break;
            }
            formData["review"] = "pack";
            formData["UID"] = UID;
            formData["approval"] = approval.ToString();

            string result = BSN_Info.sendPOST(BASEURL + "compack.php", formData);
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
        public static bool modifyPackBasic(BSN_BSL.PACK kind, string UID, string recommended, bool activate)
        {
            NameValueCollection formData = new NameValueCollection();

            switch (kind)
            {
                case BSN_BSL.PACK.ModPack:
                    formData["type"] = "modpack";
                    break;

                case BSN_BSL.PACK.BasePack:
                    formData["type"] = "basepack";
                    break;

                case BSN_BSL.PACK.Resource:
                    formData["type"] = "resource";
                    break;
            }
            formData["modify"] = "base";
            formData["UID"] = UID;
            formData["recommended"] = recommended;
            formData["activate"] = activate.ToString();

            string result = BSN_Info.sendPOST(BASEURL + "compack.php", formData);
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
        public static bool addPackManager(BSN_BSL.PACK kind, string UID, string member_srl, string permission)
        {
            NameValueCollection formData = new NameValueCollection();

            switch (kind)
            {
                case BSN_BSL.PACK.ModPack:
                    formData["type"] = "modpack";
                    break;

                case BSN_BSL.PACK.BasePack:
                    formData["type"] = "basepack";
                    break;

                case BSN_BSL.PACK.Resource:
                    formData["type"] = "resource";
                    break;
            }
            formData["insert"] = "manager";
            formData["UID"] = UID;
            formData["member_srl"] = member_srl;
            formData["permission"] = permission;

            string result = BSN_Info.sendPOST(BASEURL + "compack.php", formData);
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
        public static bool delPackManager(BSN_BSL.PACK kind, string UID, string member_srl, string permission)
        {
            NameValueCollection formData = new NameValueCollection();

            switch (kind)
            {
                case BSN_BSL.PACK.ModPack:
                    formData["type"] = "modpack";
                    break;

                case BSN_BSL.PACK.BasePack:
                    formData["type"] = "basepack";
                    break;

                case BSN_BSL.PACK.Resource:
                    formData["type"] = "resource";
                    break;
            }
            formData["delete"] = "manager";
            formData["UID"] = UID;
            formData["member_srl"] = member_srl;
            formData["permission"] = permission;

            string result = BSN_Info.sendPOST(BASEURL + "compack.php", formData);
            if (result == "관리자 삭제 성공")
                return true;
            else
                return false;
        }

        #endregion
    }
}
