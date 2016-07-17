using BellLib.Class.Protection;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bell_Smart_Server.Source.Class
{
    /// <summary>
    /// 서버 프로필 제어 클래스
    /// </summary>
    public class ServerProfile
    {
        // 필드
        private string ServerName;
        private string ServerPath;
        private string ServerFile;
        private string JavaPath;
        private string Parameter;

        /// <summary>
        /// 서버 프로필 데이터 종류 열거형
        /// </summary>
        public enum Data
        {
            Name,
            ServerPath,
            ServerFile,
            JavaPath,
            Parameter
        }

        /// <summary>
        /// 서버 에디터를 선택한 서버 프로필로 초기화합니다.
        /// </summary>
        /// <param name="ServerName">서버 이름</param>
        public ServerProfile(string ServerName)
        {
            // 유효성 검증
            if (ServerName == "서버 선택")
                return;

            // 초기화
            this.ServerName = ServerName;

            // 프로필 정보 로드
            this.ServerPath = DataProtect.DataLoad(DataPath.BSS.ServerProfiles + ServerName + ".bdx", "ServerPath");
            this.ServerFile = DataProtect.DataLoad(DataPath.BSS.ServerProfiles + ServerName + ".bdx", "ServerFile");
            this.JavaPath = DataProtect.DataLoad(DataPath.BSS.ServerProfiles + ServerName + ".bdx", "JavaPath");
            this.Parameter = DataProtect.DataLoad(DataPath.BSS.ServerProfiles + ServerName + ".bdx", "Parameter");
        }

        /// <summary>
        /// 프로필 리스트를 가져옵니다.
        /// </summary>
        /// <returns>프로필 리스트</returns>
        public static string[] GetProfileList(bool IncludeDefaultField = false)
        {
            // 필드
            List<string> list = new List<string>();

            // 기본값 추가
            if (IncludeDefaultField)
            {
                string[] Default = { "서버 선택", "서버 생성" };

                foreach (string value in Default)
                    list.Add(value); // 기본값 추가
            }

            // 로드
            try
            {
                string[] ProfileList = Directory.GetFiles(DataPath.BSS.ServerProfiles, "*.bdx"); // .bd 파일 리스트를 불러옴.

                foreach (string tmp in ProfileList)
                    list.Add(tmp.Replace(DataPath.BSS.ServerProfiles, string.Empty).Replace(".bdx", string.Empty)); // 프로필 파일을 전부 로드함.
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(DataPath.BSS.ServerProfiles);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 마지막에 선택한 프로필 이름을 가져옵니다.
        /// </summary>
        /// <returns>프로필 이름</returns>
        public static string GetLastProfile()
        {
            return DataProtect.DataLoad(DataPath.BSS.Server, "Profile");
        }

        /// <summary>
        /// 마지막 선택 프로필을 저장합니다.
        /// </summary>
        /// <param name="Name">프로필 이름</param>
        /// <returns>저장 성공 여부</returns>
        public static bool SetLastProfile(string Name)
        {
            return DataProtect.DataSave(DataPath.BSS.Server, "Profile", Name);
        }

        /// <summary>
        /// 프로필 데이터를 가져옵니다.
        /// </summary>
        /// <param name="Value">반환할 프로필 데이터 값</param>
        /// <returns>프로필 데이터</returns>
        public string GetData(Data Value)
        {
            switch (Value)
            {
                case Data.Name:
                    return ServerName;

                case Data.ServerPath:
                    return ServerPath;

                case Data.ServerFile:
                    return ServerFile;

                case Data.JavaPath:
                    return JavaPath;

                case Data.Parameter:
                    return Parameter;

                default:
                    return null;
            }
        }

        /// <summary>
        /// 프로필 데이터를 설정합니다.
        /// </summary>
        /// <param name="ServerName">서버 이름</param>
        /// <param name="ServerPath">서버 경로</param>
        /// <param name="ServerFile">서버 파일</param>
        /// <param name="JavaPath">자바 경로</param>
        /// <param name="Parameter">매개변수</param>
        public void SetData(string ServerName, string ServerPath, string ServerFile, string JavaPath, string Parameter)
        {
            this.ServerName = ServerName;
            this.ServerPath = ServerPath;
            this.ServerFile = ServerFile;
            this.JavaPath = JavaPath;
            this.Parameter = Parameter;
        }

        /// <summary>
        /// 프로필 파일을 저장합니다.
        /// </summary>
        /// <returns>저장 성공 여부</returns>
        public bool Save(bool Overwrite = true)
        {
            // 필드 검사
            if (ServerName == string.Empty || ServerPath == string.Empty || ServerFile == string.Empty || JavaPath == string.Empty || Parameter == string.Empty)
                return false;

            // 저장
            if (Overwrite)
                File.Delete(DataPath.BSS.ServerProfiles + ServerName + ".bdx"); // 열렸던 파일 삭제
            else
                if (File.Exists(DataPath.BSS.ServerProfiles + ServerName + ".bdx"))
                return false; // 덮어쓰기 사용안할시 파일이 이미 있으면 저장안함

            DataProtect.DataSave(DataPath.BSS.ServerProfiles + ServerName + ".bdx", "ServerPath", ServerPath);
            DataProtect.DataSave(DataPath.BSS.ServerProfiles + ServerName + ".bdx", "ServerFile", ServerFile);
            DataProtect.DataSave(DataPath.BSS.ServerProfiles + ServerName + ".bdx", "JavaPath", JavaPath);
            DataProtect.DataSave(DataPath.BSS.ServerProfiles + ServerName + ".bdx", "Parameter", Parameter);

            return true;
        }

        /// <summary>
        /// 프로필 파일을 삭제합니다.
        /// </summary>
        /// <returns>삭제 성공여부</returns>
        public bool RemoveData()
        {
            if (ServerName == string.Empty)
                return false;

            if (File.Exists(DataPath.BSS.ServerProfiles + ServerName + ".bdx"))
                File.Delete(DataPath.BSS.ServerProfiles + ServerName + ".bdx");
            else
                return false;

            return true;
        }
    }
}
