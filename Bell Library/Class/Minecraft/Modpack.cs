using BellLib.Class.BSN;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using BellLib.Class.Protection;
using System.Windows.Controls;

namespace BellLib.Class.Minecraft
{
    public class Modpack
    {
        #region *** FIELD ***
        
        /// <summary>
        /// 모드팩 리스트 집합
        /// </summary>
        private class ModpackList
        {
            public string[] list;
            public string[] filteredList;
            public FILTER filter;
        }

        /// <summary>
        /// 모드팩정보 집합
        /// </summary>
        private class ModpackInfo
        {
            public BSN_BSL.ModPack modpack;
            
            public string Name;
            public string Version;
            public string ConvertedVersion;
            public string VersionID;
            public bool? Like;


            public BSN_BSL.Manager[] ManagerList;

            public string[] VersionList;
        }

        /// <summary>
        /// 베이스팩정보 집합
        /// </summary>
        private class BaseInfo
        {
            public BSN_BSL.BasePack basepack;

            public string Name;
            public string Version;
            public string VersionID;
        }

        /// <summary>
        /// 경로정보 집합
        /// </summary>
        private class Path
        {
            public string Install;
            public string BasePack;
            public string BaseVersion;
            public string ModPack;
            public string ModpackVersion;
            public string Java;
        }

        /// <summary>
        /// 옵션정보 집합
        /// </summary>
        private class Option
        {
            public string BaseParameter;
            public string CustomParameter;

            public bool ConsoleRun;
        }

        /// <summary>
        /// 계정정보 집합
        /// </summary>
        private class Account
        {
            public string MC_ID;
            public string MC_PW;
            public string NickName;
            public string UUID;
            public string AccessToken;
        }

        /// <summary>
        /// 설치정보 집합
        /// </summary>
        private class Install
        {
            public string[] baseServerList;
            public string[] modpackServerList;
        }

        /// <summary>
        /// 설치 데이터 집합
        /// </summary>
        public class InstallData
        {
            public BSN_BSL.PACK pack;
            public BSN_BSL.Install[] File;
            public BSN_BSL.Server Server;
            public string Name;
            public string Version;
            public string VersionID;
            public string PathVersion;
            public string PathPack;
            public double FullCapacity;
        }

        private bool ExceptionThrow;

        private Process GameProcess;

        private ModpackList ml;
        private ModpackInfo mi;
        private BaseInfo bi;
        private Path path;
        private Option option;
        private Account account;
        private Install install;

        #endregion

        #region *** ERROR CODE ***

        public enum ERR_LAUNCH
        {
            Success = 0,
            Already_Running,
            No_Input_Data,
            Java_Not_Found,
            Not_Installed,
            Error
        }

        public enum ERR_LOAD
        {
            Success = 0,
            Version_Load_Fail,
            Not_Input_Version,
            Error
        }

        public enum ERR_PATH
        {
            Success = 0,
            Not_Load_Data,
            Error
        }

        public enum ERR_LOGIN
        {
            Success = 0,
            No_Input_ID,
            No_Input_PW,
            Login_Fail,
            Error
        }

        #endregion

        #region *** CATEGORY ***

        public enum FILTER
        {
            All,
            Standard
        }

        #endregion

        #region *** 생성자 ***

        /// <summary>
        /// 모드팩 리스트 정보를 얻기 위한 최소한의 초기화
        /// </summary>
        /// <param name="ExceptionThrow"></param>
        public Modpack(bool ExceptionThrow = false)
        {
            ml = new ModpackList();
            mi = new ModpackInfo();
            bi = new BaseInfo();
            path = new Path();
            option = new Option();
            account = new Account();
            install = new Install();
            
            this.ExceptionThrow = ExceptionThrow;
        }

        /// <summary>
        /// 모드팩 사용준비를 위한 최소한의 초기화
        /// </summary>
        /// <param name="Name">모드팩 이름</param>
        /// <param name="ExceptionThrow">예외 던지기 여부</param>
        public Modpack(string Name, bool ExceptionThrow = false) : this(ExceptionThrow)
        {
            mi.Name = Name;
        }

        /// <summary>
        /// 모드팩 사용준비를 위한 초기화
        /// 사용방법 : LoadModpackDetail -> SetPath -> SetOption -> SetAccount -> Login -> Launch
        /// </summary>
        /// <param name="Name">모드팩 이름</param>
        /// <param name="Version">모드팩 버전</param>
        /// <param name="ExceptionThrow">예외 던지기 여부</param>
        public Modpack(string Name, string Version, bool ExceptionThrow = false) : this(Name, ExceptionThrow)
        {
            mi.Version = Version;
        }

        #endregion


        #region *** DATA ***

        #region ** LOAD **

        /// <summary>
        /// 모드팩 리스트를 로드합니다.
        /// </summary>
        /// <returns>로드 에러코드</returns>
        public ERR_LOAD LoadModpackList()
        {
            ml.list = BSN_BSL.LoadPackList(BSN_BSL.PACK.modpack);

            return ERR_LOAD.Success;
        }

        /// <summary>
        /// 모드팩 기본정보를 로드합니다.
        /// </summary>
        /// <returns>로드 에러코드</returns>
        public ERR_LOAD LoadModpackBase()
        {
            // 필드


            // 로드
            mi.VersionList = BSN_BSL.LoadPackVersionList(BSN_BSL.PACK.modpack, mi.Name, BSN_BSL.STATE.ACTIVATE); // 모드팩 버전 리스트
            mi.modpack = BSN_BSL.LoadModPackDetail(mi.Name); // 모드팩 정보 로드
            mi.ManagerList = BSN_BSL.LoadPackManager(BSN_BSL.PACK.modpack, mi.Name); // 모드팩 매니저 리스트 로드

            return ERR_LOAD.Success;
        }

        /// <summary>
        /// 모드팩 상세정보를 로드합니다.
        /// </summary>
        /// <returns>로드 에러코드</returns>
        public ERR_LOAD LoadModpackDetail(bool loadBase = true)
        {
            if (loadBase)
                LoadModpackBase();

            if (mi.Version == string.Empty) // 버전정보가 없으면 상세정보 로드 불가
                return ERR_LOAD.Not_Input_Version;

            // 필드
            string modVerData, baseVerData;
            
            // 버전정보 검증
            mi.ConvertedVersion = mi.Version; // 변환할 버전값을 넣음
            if (mi.ConvertedVersion == "Recommended") // 권장버전을 선택했을경우,
                mi.ConvertedVersion = mi.modpack.recommended; // 공식 권장버전을 대입
            if (mi.ConvertedVersion == "Latest") // 최신버전을 선택했을경우,
                mi.ConvertedVersion = mi.modpack.latest; // 공식 최신버전을 대입

            foreach (string verData in mi.VersionList)
                if (mi.ConvertedVersion == Common.getElement(verData, "version")) // 루프를 돌다가 선택버전과 서버버전이 일치할경우,
                {
                    mi.VersionID = Common.getElement(verData, ("id")); // 해당 버전 id를 로드한다.

                    break; // 원하는걸 구했으니 빠져나온다
                }

            // 데이터 유효성 검증
            if (mi.VersionID == null) // 예상치 못한 오류로 모드 버전 id를 받지 못하였을경우 실행 중단
                return ERR_LOAD.Version_Load_Fail;

            // 선행 로드가 끝난 후 추가정보 로드
            modVerData = BSN_BSL.LoadVersionDetail(BSN_BSL.PACK.modpack, mi.VersionID); // 모드팩 버전 상세정보 로드
            bi.VersionID = Common.getElement(modVerData, "basevid"); // 베이스팩 버전id
            baseVerData = BSN_BSL.LoadVersionDetail(BSN_BSL.PACK.basepack, bi.VersionID); // 베이스팩 버전 상세정보 로드
            bi.Version = Common.getElement(baseVerData, "version"); // 베이스팩 버전 로드
            option.BaseParameter = Common.getElement(baseVerData, "parameter"); // 베이스팩 파라메터 로드
            bi.basepack = BSN_BSL.LoadBasePackDetail(mi.modpack.baseid, true);
            bi.Name = bi.basepack.name;

            mi.Like = BSN_BSL.isLikedPack(BSN_BSL.PACK.modpack, mi.modpack.id); // 모드팩 좋아요 정보 로드
            

            return ERR_LOAD.Success;
        }

        #endregion

        #region ** GET **

        /// <summary>
        /// 마지막에 선택한 모드팩 이름을 반환합니다.
        /// </summary>
        /// <returns>모드팩 이름</returns>
        public static string GetLastModpack()
        {
            return DataProtect.DataLoad(DataPath.BSL.Modpacks, "Modpack"); // 마지막에 선택했던 팩
        }

        /// <summary>
        /// 마지막에 선택한 필터 이름을 반환합니다.
        /// </summary>
        /// <returns>필터 이름</returns>
        public static string GetLastFilter()
        {
            return DataProtect.DataLoad(DataPath.BSL.Modpacks, "Filter");
        }

        /// <summary>
        /// 마지막에 실행한 버전을 가져옵니다.
        /// </summary>
        /// <returns>모드팩 버전</returns>
        public string GetLastVersion()
        {
            if (path.ModPack == null)
                if (ExceptionThrow)
                    throw new Exception("경로 설정 안됨");

            return DataProtect.DataLoad(path.ModPack + "data.bdx", "LastVersion");
        }

        /// <summary>
        /// 필터링된 모드팩 리스트를 반환합니다.
        /// </summary>
        /// <returns>모드팩 리스트</returns>
        public string[] GetModpackList()
        {
            if (ExceptionThrow)
                if (ml.filteredList == null)
                    throw new Exception("리스트가 필터링되지 않음.");

            return ml.filteredList;
        }

        /// <summary>
        /// 모드팩 경로를 반환합니다.
        /// </summary>
        /// <param name="PackPath">팩 폴더 반환여부 (default : 버전 폴더)</param>
        /// <returns>경로</returns>
        public string GetPath(bool PackPath = false)
        {
            if (PackPath)
                return path.ModPack;
            else
                return path.ModpackVersion;
        }

        /// <summary>
        /// 현재 모드팩 좋아요 여부를 반환합니다.
        /// </summary>
        /// <returns>좋아요 여부</returns>
        public bool GetLike()
        {
            if (mi.Like == null)
            {
                if (ExceptionThrow)
                    throw new Exception("좋아요 정보가 로드되지 않음.");
                return false;
            }

            return (bool)mi.Like;
        }

        /// <summary>
        /// 모드팩 공지사항을 가져옵니다.
        /// </summary>
        /// <returns>모드팩 공지사항 HTML</returns>
        public string GetNotice()
        {
            try
            {
                string strNotice = Common.getStringFromWeb(mi.modpack.notice, Encoding.UTF8);
                string[] strTemp = Common.stringSplit(strNotice, "<article");
                strNotice = strTemp[1];
                strTemp = Common.stringSplit(strNotice, "article>");
                strNotice = "<meta charset=\"utf-8\"><article" + strTemp[0].Replace("target=\"_blank\"", "") + "article >";

                return strNotice;
            }
            catch
            {
                // 공지사항 로드 에러
                return "<meta charset=\"utf-8\"><strong abp=\"4668\"><span style=\"font-family: 돋움; \"abp=\"4670\"><font color=\"#ff0000\">공지사항이 존재하지 않거나 불러오는 중 문제가 발생하였습니다.</font></span></strong>";
            }
        }

        /// <summary>
        /// 버전 리스트를 가져옵니다.
        /// </summary>
        /// <returns>버전 리스트</returns>
        public string[] GetVersionList()
        {
            // 유효성 검증
            if (mi.VersionList == null)
                if (ExceptionThrow)
                    throw new Exception("버전 리스트가 로드되지 않음.");

            return mi.VersionList;
        }

        /// <summary>
        /// 정리된 모드팩 상세정보를 가져옵니다.
        /// </summary>
        /// <returns>모드팩 상세정보 배열</returns>
        public string[] GetDetailInfo()
        {
            // 필드
            List<string> list = new List<string>();

            // 로드
            list.Add("Detail : " + mi.modpack.detail);
            list.Add("Like : " + mi.modpack.like.ToString());
            list.Add("Recommended : " + mi.modpack.recommended);
            list.Add("Latest : " + mi.modpack.latest);
            list.Add("Basepack : " + mi.modpack.BaseName);
            if (bi.Version != string.Empty)
                list.Add("Basepack Version : " + bi.Version);

            foreach (BSN_BSL.Manager member in mi.ManagerList)
                if (member.permission == "4")
                {
                    list.Add("Producer : " + member.email); // 팩 제작자

                    break; // 필요한 정보 구했으니 빠져나옴
                }

            list.Add("Made : " + mi.modpack.made);
            list.Add("Modification : " + mi.modpack.modification);
            try
            {
                list.Add("Plan : " + BSN_BSL.GetPlanName((BSN_BSL.PLAN)Convert.ToInt32(mi.modpack.plan)));
            }
            catch
            {
                list.Add("Plan : " + BSN_BSL.GetPlanName(BSN_BSL.PLAN.Basic));
            }

            return list.ToArray();
        }

        #endregion

        #region ** SET **

        /// <summary>
        /// 마지막에 선택한 모드팩을 저장합니다.
        /// </summary>
        /// <param name="Name">모드팩 이름</param>
        /// <returns></returns>
        public static bool SetLastModpack(string Name)
        {
            return DataProtect.DataSave(DataPath.BSL.Modpacks, "Modpack", Name);
        }

        /// <summary>
        /// 마지막 선택한 필터를 저장합니다.
        /// </summary>
        /// <param name="Name">필터 이름</param>
        /// <returns>저장 성공 여부</returns>
        public static bool SetLastFilter(string Name)
        {
            return DataProtect.DataSave(DataPath.BSL.Modpacks, "Filter", Name);
        }

        /// <summary>
        /// 모드팩 마지막 실행 버전을 저장합니다.
        /// </summary>
        /// <returns>저장 성공 여부</returns>
        public bool SetLastVersion()
        {
            if (path.ModPack == null)
                if (ExceptionThrow)
                    throw new Exception("경로 설정 안됨");

            return DataProtect.DataSave(path.ModPack + "data.bdx", "LastVersion", mi.Version);
        }

        /// <summary>
        /// 필터를 설정합니다.
        /// </summary>
        /// <param name="filter">필터 옵션</param>
        /// <returns>성공 여부</returns>
        public bool SetFilter(FILTER filter)
        {
            // 필드
            Dictionary<string, int> BasicPlan = new Dictionary<string, int>();
            Dictionary<string, int> PremiumPlan = new Dictionary<string, int>();
            Dictionary<string, int> PartnerPlan = new Dictionary<string, int>();
            Dictionary<string, int> BSN_SpecialPlan = new Dictionary<string, int>();

            List<string> list = new List<string>();
            object[] plans;

            //초기화
            BasicPlan.Clear();
            PremiumPlan.Clear();
            PartnerPlan.Clear();
            BSN_SpecialPlan.Clear();

            ml.filter = filter;

            // 요금제별 분류
            foreach (string value in ml.list)
            {
                switch (Common.getElement(value, "plan"))
                {
                    case "0":
                        BasicPlan.Add(value, Convert.ToInt32(Common.getElement(value, "like")));
                        break;

                    case "1":
                        PremiumPlan.Add(value, Convert.ToInt32(Common.getElement(value, "like")));
                        break;

                    case "2":
                        PartnerPlan.Add(value, Convert.ToInt32(Common.getElement(value, "like")));
                        break;

                    case "10":
                        BSN_SpecialPlan.Add(value, Convert.ToInt32(Common.getElement(value, "like")));
                        break;

                    default:
                        BasicPlan.Add(value, Convert.ToInt32(Common.getElement(value, "like")));
                        break;
                }
            }

            // 필터 옵션 설정
            switch (ml.filter)
            {
                case FILTER.All:
                    plans = new object[] { BSN_SpecialPlan, PartnerPlan, PremiumPlan, BasicPlan };
                    break;

                case FILTER.Standard:
                    plans = new object[] { BSN_SpecialPlan, PartnerPlan, PremiumPlan };
                    break;

                /*case "BSN_Special":
                    plans = new object[] { BSN_SpecialPlan };
                    break;

                case "Partner":
                    plans = new object[] { PartnerPlan };
                    break;

                case "Premium":
                    plans = new object[] { PremiumPlan };
                    break;

                case "Basic":
                    plans = new object[] { BasicPlan };
                    break;*/

                default:
                    plans = new object[] { BSN_SpecialPlan, PartnerPlan, PremiumPlan };
                    break;
            }

            // 최종 리스트 로드
            foreach (Dictionary<string, int> plan in plans)
            {
                var plan_desc = from pack in plan orderby pack.Value descending select pack;

                foreach (var plan_value in plan_desc)
                    list.Add(Common.getElement(plan_value.Key, "name"));
            }

            // 필터링된 리스트
            ml.filteredList = list.ToArray();

            return true;
        }

        /// <summary>
        /// 모드팩 버전을 설정합니다.
        /// </summary>
        /// <param name="Version">버전</param>
        /// <returns>설정 성공여부</returns>
        public bool SetVersion(string Version)
        {
            mi.Version = Version;

            return true;
        }

        /// <summary>
        /// 추가 정보를 설정합니다.
        /// </summary>
        /// <param name="AllocateMemory">메모리 할당량 (GB)</param>
        /// <param name="CustomParameter">유저 커스텀 매개변수</param>
        /// <param name="ConsoleRun">콘솔창 사용여부</param>
        /// <returns>설정 성공여부</returns>
        public bool SetOption(double AllocateMemory, string CustomParameter, bool ConsoleRun)
        {
            if (AllocateMemory <= 0)
                return false;

            option.CustomParameter = "-Xmx" + (AllocateMemory * 1024) + "M " + CustomParameter;
            option.ConsoleRun = ConsoleRun;

            return true;
        }

        /// <summary>
        /// 모드팩 경로를 설정합니다.
        /// </summary>
        /// <param name="InstallPath">설치 경로</param>
        /// <returns>에러코드</returns>
        public ERR_PATH SetPath(string InstallPath)
        {
            if (mi.Name == string.Empty)
                return ERR_PATH.Not_Load_Data;

            path.Install = InstallPath;
            path.ModPack = path.Install + "\\ModPack\\" + mi.Name.Replace(" ", "_") + "\\";

            return ERR_PATH.Success;
        }

        /// <summary>
        /// 전체 경로를 설정합니다.
        /// </summary>
        /// <param name="InstallPath">설치 경로</param>
        /// <param name="JavaPath">자바 경로</param>
        /// <returns>에러코드</returns>
        public ERR_PATH SetPath(string InstallPath, string JavaPath)
        {
            if (mi.Name == string.Empty || mi.ConvertedVersion == string.Empty || bi.Name == string.Empty || bi.VersionID == string.Empty)
                return ERR_PATH.Not_Load_Data;

            path.Install = InstallPath;
            path.BasePack = path.Install + "\\Base\\" + bi.Name.Replace(" ", "_") + "\\";
            path.BaseVersion = path.BasePack + bi.Version.Replace(" ", "_") + "\\";
            path.ModPack = path.Install + "\\ModPack\\" + mi.Name.Replace(" ", "_") + "\\";
            path.ModpackVersion = path.ModPack + mi.ConvertedVersion.Replace(" ", "_") + "\\";
            path.Java = JavaPath + "\\bin\\java.exe";

            return ERR_PATH.Success;
        }

        /// <summary>
        /// 계정 정보를 설정합니다.
        /// </summary>
        /// <param name="MC_ID">마인크래프트 계정 ID</param>
        /// <param name="MC_PW">마인크래프트 계정 PW</param>
        /// <returns>설정 성공여부</returns>
        public bool SetAccount(string MC_ID, string MC_PW = null)
        {
            if (MC_ID == string.Empty)
                return false;

            account.MC_ID = MC_ID;
            account.MC_PW = MC_PW;

            return true;
        }

        /// <summary>
        /// 좋아요 정보를 설정합니다.
        /// </summary>
        /// <param name="Like">좋아요 등록/제거</param>
        /// <returns>좋아요 성공 여부</returns>
        public bool SetLike(bool Like)
        {
            if (Like)
            {
                if (BSN_BSL.setLikePack(BSN_BSL.PACK.modpack, mi.modpack.id))
                {
                    mi.Like = BSN_BSL.isLikedPack(BSN_BSL.PACK.modpack, mi.modpack.id); // 모드팩 좋아요 정보 로드
                    mi.modpack.like += 1;

                    return true;
                }
            }
            else
            {
                if (BSN_BSL.delLikePack(BSN_BSL.PACK.modpack, mi.modpack.id))
                {
                    mi.Like = BSN_BSL.isLikedPack(BSN_BSL.PACK.modpack, mi.modpack.id); // 모드팩 좋아요 정보 로드
                    mi.modpack.like -= 1;

                    return true;
                }
            }

            return false;
        }

        #endregion

        #endregion


        #region *** VALIDITY ***

        /// <summary>
        /// 설정된 정보를 바탕으로 해당 타입의 팩이 정상적으로 설치되어있는지 확인합니다.
        /// </summary>
        /// <param name="pack">팩 타입</param>
        /// <returns>설치 여부</returns>
        public bool GetInstalled(BSN_BSL.PACK pack)
        {
            // 필드 검사
            if (path.Install == string.Empty)
                return false;

            // 필드
            Protect pro = new Protect();
            string PathPackData;
            string PathVersionData;
            string Name, Version;

            // 초기화
            switch (pack)
            {
                case BSN_BSL.PACK.modpack:
                    Name = mi.Name;
                    Version = mi.ConvertedVersion;

                    PathPackData = path.Install + "\\ModPack\\" + Name.Replace(" ", "_") + "\\";

                    break;

                case BSN_BSL.PACK.basepack:
                    Name = bi.Name;
                    Version = bi.Version;

                    PathPackData = path.Install + "\\Base\\" + Name.Replace(" ", "_") + "\\";

                    break;

                default:
                    return false;
            }
            if (Name == string.Empty || Version == string.Empty)
                return false; // 이름과 버전값이 없으면 로드 에러
            
            PathVersionData = PathPackData + Version.Replace(" ", "_") + "\\config.bdx";
            PathPackData += "data.bdx";
            
            // 유효성 검사
            if (!File.Exists(PathPackData) || !File.Exists(PathVersionData)) // 설정파일이 없으면,
                return false; // 미설치로 간주

            if (DataProtect.DataLoad(PathPackData, "Name") != Name || DataProtect.DataLoad(PathVersionData, "Version") != Version)
                return false; // 팩 데이터파일에 이름 요소가 팩 이름과 같지 않거나, 버전파일에 버전 요소가 버전명과 같지 않으면 

            if (DataProtect.DataLoad(PathVersionData, "State") == "Setup")
                return false; // 팩 설치중 문제가 발생한걸로 간주.

            return true;
        }

        #endregion


        #region *** INSTALL ***

        /// <summary>
        /// 설치 데이터를 로드합니다.
        /// </summary>
        /// <param name="pack">팩 타입</param>
        /// <returns>에러코드</returns>
        public ERR_LOAD LoadInstallData(BSN_BSL.PACK pack)
        {
            switch (pack)
            {
                case BSN_BSL.PACK.basepack:
                    install.baseServerList = BSN_BSL.LoadVersionServer(BSN_BSL.PACK.basepack, BSN_BSL.KIND.client, bi.VersionID); // 베이스팩이 업로드되어있는 서버리스트
                    
                    break;

                case BSN_BSL.PACK.modpack:
                    install.modpackServerList = BSN_BSL.LoadVersionServer(BSN_BSL.PACK.modpack, BSN_BSL.KIND.client, mi.VersionID); // 모드팩이 업로드되어있는 서버리스트

                    break;
            }

            return ERR_LOAD.Success;
        }

        /// <summary>
        /// 팩이 업로드 되어있는 최적의 서버를 탐색합니다.
        /// </summary>
        /// <param name="pack">팩 타입</param>
        /// <returns>최적 서버 정보</returns>
        private BSN_BSL.Server GetOptimalServer(BSN_BSL.PACK pack)
        {
            string[] ServerList;

            switch (pack)
            {
                case BSN_BSL.PACK.basepack:
                    ServerList = install.baseServerList;
                    
                    break;

                case BSN_BSL.PACK.modpack:
                    ServerList = install.modpackServerList;

                    break;

                default:
                    if (ExceptionThrow)
                        throw new NotSupportedException("이 타입의 팩은 지원하지 않습니다.");
                    return null;
            }

            foreach (string serverid in ServerList)
            { // 루프돌면서 최적의 서버 탐색 (추후 개발예정)
                BSN_BSL.Server server = BSN_BSL.LoadServerDetail(serverid);

                return server;
            }

            if (ExceptionThrow)
                throw new Exception("서버 정보가 탐색되지 않았습니다.");
            return null;
        }

        /// <summary>
        /// 설정된 정보를 바탕으로 해당 타입의 팩을 설치합니다.
        /// </summary>
        /// <param name="pack">팩 타입</param>
        /// <returns>설치정보집합</returns>
        public InstallData GetInstallData(BSN_BSL.PACK pack)
        {
            // 필드
            InstallData id = new InstallData();

            // 팩 정보 설정
            id.pack = pack;
            id.Server = GetOptimalServer(id.pack); // 서버정보 로드

            // 버전 id 로드
            switch (id.pack)
            {
                case BSN_BSL.PACK.basepack:
                    id.VersionID = bi.VersionID;
                    id.PathVersion = path.BaseVersion;
                    id.PathPack = path.BasePack;
                    id.Name = bi.Name;
                    id.Version = bi.Version;

                    break;

                case BSN_BSL.PACK.modpack:
                    id.VersionID = mi.VersionID;
                    id.PathVersion = path.ModpackVersion;
                    id.PathPack = path.ModPack;
                    id.Name = mi.Name;
                    id.Version = mi.ConvertedVersion;

                    break;
            }

            id.File = BSN_BSL.LoadVersionFiles(id.pack, BSN_BSL.KIND.client, id.VersionID); // 설치파일정보 로드

            // 전체 파일 용량 로드
            try
            {
                foreach (BSN_BSL.Install value in id.File)
                    id.FullCapacity += Convert.ToDouble(value.size);
            }
            catch
            {
                if (ExceptionThrow)
                    throw;
            }

            return id;
        }

        #endregion


        #region *** GAME CONTROL ***

        /// <summary>
        /// 게임을 실행합니다.
        /// </summary>
        /// <returns>실행 에러코드</returns>
        public ERR_LAUNCH Launch()
        {
            // 필드 검사
            if (mi.ConvertedVersion == null || bi.Version == null || path.BaseVersion == null || path.ModpackVersion == null || account.UUID == null || account.AccessToken == null)
                return ERR_LAUNCH.No_Input_Data;

            if (!GetInstalled(BSN_BSL.PACK.basepack) || !GetInstalled(BSN_BSL.PACK.modpack))
                return ERR_LAUNCH.Not_Installed;

            if (GameProcess != null)
                try
                {
                    if (!GameProcess.HasExited) // 게임이 실행중이라면,
                        return ERR_LAUNCH.Already_Running;
                }
                catch { }

            try
            {
                //Directory.SetCurrentDirectory(PathModpack); //런처 실행경로를 방울크래프트 클라이언트 경로로 수정.
                GameProcess = new Process();
                if (!option.ConsoleRun)
                    path.Java = path.Java.Replace("java.exe", "javaw.exe");
                GameProcess.StartInfo.FileName = path.Java;
                GameProcess.StartInfo.Arguments = GetArguments(); // 파라메터 설정
                GameProcess.StartInfo.WorkingDirectory = path.ModpackVersion; //런처 실행경로를 방울크래프트 클라이언트 경로로 수정.
                GameProcess.Start();
            }
            catch (FileNotFoundException)
            {
                if (ExceptionThrow)
                    throw;
                else
                    return ERR_LAUNCH.Java_Not_Found;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                if (ExceptionThrow)
                    throw;
                else
                    return ERR_LAUNCH.Java_Not_Found;
            }
            catch (Exception)
            {
                if (ExceptionThrow)
                    throw;
                else
                    return ERR_LAUNCH.Error;
            }
            finally
            {
                // 실행 정보 저장
                SetLastModpack(mi.Name); // 선택 모드팩이 바뀌었으므로 설정값 저장!
                //DataProtect.DataSave(DataPath.BSL.Modpacks, "Version", mi.Version);
            }

            return ERR_LAUNCH.Success;
        }

        /// <summary>
        /// 게임 실행에 필요한 인자를 정리합니다.
        /// </summary>
        /// <returns>실행인자</returns>
        private string GetArguments()
        {
            StringBuilder sb = new StringBuilder(1024); //기본 문자열을 JAVA 변수, 기본 캐피시터를 1024로 하여 StringBuilder 선언.

            sb.Append(option.CustomParameter); // 유저 커스텀 파라메터 추가

            sb.Append(" -Djava.library.path=");
            sb.Append(path.BaseVersion);
            sb.Append("natives"); // 라이브러리 dll 폴더

            sb.Append(" -cp ");
            sb.Append(path.BaseVersion);
            sb.Append("*"); // 라이브러리 jar 폴더

            sb.Append(" net.minecraft.launchwrapper.Launch "); // 실행 클래스

            sb.Append(ReplaceParameter(option.BaseParameter, account.NickName, mi.ConvertedVersion, path.ModpackVersion, path.BaseVersion, account.UUID, account.AccessToken));

            return sb.ToString();
        }

        /// <summary>
        /// 매개변수에 치환자를 적용합니다.
        /// </summary>
        /// <param name="data">원본 매개변수</param>
        /// <param name="username">닉네임</param>
        /// <param name="version">게임 버전</param>
        /// <param name="gameDir">게임 경로</param>
        /// <param name="assetsDir">에셋 경로</param>
        /// <param name="uuid">계정 UUID</param>
        /// <param name="accessToken">계정 accessToken</param>
        /// <returns>치환자가 정보로 치환된 매개변수</returns>
        private static string ReplaceParameter(string data, string username, string version, string gameDir, string assetsDir, string uuid, string accessToken)
        {
            /*
             * 1.7, 1.9 Common : --username ${auth_player_name} --version ${version_name} --gameDir ${game_directory} --assetsDir ${assets_root} --assetIndex ${assets_index_name} --uuid ${auth_uuid} --accessToken ${auth_access_token} --userType ${user_type}
             * 
             * 1.9 : --tweakClass net.minecraftforge.fml.common.launcher.FMLTweaker --versionType Forge
             * 1.7 : --userProperties ${user_properties} --tweakClass cpw.mods.fml.common.launcher.FMLTweaker
             */

            string output = data;
            string[,] conversion = new string[,]
            { { "${auth_player_name}", "${version_name}","${game_directory}", "${assets_root}", "${assets_index_name}", "${auth_uuid}", "${auth_access_token}", "${user_type}", "${user_properties}" },
                                                { username, version, gameDir, assetsDir + "assets", "BSN", uuid, accessToken, "mojang", "{}" } };


            for (int i = 0; i < conversion.Length / 2; i++)
            {
                output = output.Replace(conversion[0, i], conversion[1, i]);
            }

            return output;
        }

        /// <summary>
        /// 게임 실행 가능여부를 반환합니다.
        /// 게임 실행중 : False
        /// 게임 종료 : True
        /// </summary>
        /// <returns>게임 실행 가능여부</returns>
        public bool Feasibility()
        {
            if (GameProcess != null)
                return GameProcess.HasExited;
            return true;
        }

        /// <summary>
        /// 실행중인 게임을 강제종료합니다.
        /// </summary>
        /// <returns>종료 성공 여부</returns>
        public bool Kill()
        {
            if (!Feasibility()) // 게임이 실행중일때
                try
                {
                    GameProcess.Kill(); // 강제종료

                    return true;
                }
                catch
                {
                    if (ExceptionThrow)
                        throw;
                }

            return false;
        }

        #endregion

        /// <summary>
        /// 마인크래프트 계정정보를 받아오기 위해 로그인합니다.
        /// </summary>
        /// <param name="Password">비밀번호 저장기능 해제시 입력</param>
        /// <returns>로그인 에러코드</returns>
        public ERR_LOGIN Login(string Password = null)
        {
            // 유효성 검사
            if (account.MC_ID == string.Empty || account.MC_ID == null) // ID 정보가 저장되어있지 않으면,
                return ERR_LOGIN.No_Input_ID; // ID 정보 요청

            // 필드
            string strPassword = account.MC_PW;
            MCLogin MCL = new MCLogin();
            MCLogin.MC_Account MCA;

            // 초기화
            if (account.MC_PW == string.Empty || account.MC_PW == null) // 비밀번호 저장을 하지 않았을 경우,
                if (Password == null) // 비밀번호 값이 없을경우,
                    return ERR_LOGIN.No_Input_PW; // 비밀번호 정보 요청
                else
                    strPassword = Password; // 방금 입력받은 따끈따끈한 비밀번호를 변수에 담음

            // 계정 로그인
            if (!MCL.Login(account.MC_ID, strPassword, MCLogin.LoginType.Authenticate)) // 로그인 실패시
                return ERR_LOGIN.Login_Fail;

            // 계정정보 로드
            MCA = MCL.GetLoginData();
            account.NickName = MCA.MC_NickName;
            account.UUID = MCA.MC_UUID;
            account.AccessToken = MCA.MC_AccessToken;

            return ERR_LOGIN.Success;
        }
    }
}
