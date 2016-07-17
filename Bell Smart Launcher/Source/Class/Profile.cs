using BellLib.Class.Protection;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bell_Smart_Launcher.Class
{
    /// <summary>
    /// 마인크래프트 프로필 파일 제어 클래스
    /// </summary>
    public class Profile
    {
        // 필드
        private string ProfileName;
        private string ID;
        private string PW;
        
        /// <summary>
        /// 프로필 데이터 종류 열거형
        /// </summary>
        public enum Data
        {
            Name,
            ID,
            PW
        }


        /// <summary>
        /// 프로필 에디터를 선택한 프로필파일로 초기화합니다.
        /// </summary>
        /// <param name="ProfileName">프로필 이름</param>
        public Profile(string ProfileName)
        {
            // 필드
            string[] Data;

            // 유효성 검증
            if (ProfileName == "프로필 선택")
                return;

            // 초기화
            this.ProfileName = ProfileName;
            
            try
            {
                Data = Protect.ReadBDXFile(DataPath.BSL.Profiles + ProfileName + ".bdx");
            }
            catch
            {
                return; // 데이터 로드중 문제 나오면 파일 없는겨~ 새로 생성
            }
            
            // 프로필 정보 로드
            foreach (string Value in Data)
            {
                string[] tmp = Value.Split('|');
                switch (tmp[0])
                {
                    case "ID":
                        ID = tmp[1];
                        break;

                    case "PW":
                        PW = tmp[1];
                        break;
                }
            }
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
                string[] Default = { "프로필 선택", "프로필 생성" };

                foreach (string value in Default)
                    list.Add(value); // 기본값 추가
            }

            // 로드
            try
            {
                string[] ProfileList = Directory.GetFiles(DataPath.BSL.Profiles, "*.bdx"); // .bd 파일 리스트를 불러옴.

                foreach (string tmp in ProfileList)
                    list.Add(tmp.Replace(DataPath.BSL.Profiles, string.Empty).Replace(".bdx", string.Empty)); // 프로필 파일을 전부 로드함.
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(DataPath.BSL.Profiles);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 마지막에 선택한 프로필 이름을 가져옵니다.
        /// </summary>
        /// <returns>프로필 이름</returns>
        public static string GetLastProfile()
        {
            return DataProtect.DataLoad(DataPath.BSL.Modpacks, "Profile");
        }

        /// <summary>
        /// 마지막 선택 프로필을 저장합니다.
        /// </summary>
        /// <param name="Name">프로필 이름</param>
        /// <returns>저장 성공 여부</returns>
        public static bool SetLastProfile(string Name)
        {
            return DataProtect.DataSave(DataPath.BSL.Modpacks, "Profile", Name);
        }

        /// <summary>
        /// 프로필 데이터를 가져옵니다.
        /// </summary>
        /// <param name="Value">반환할 프로필 데이터 값</param>
        /// <returns>프로필 데이터</returns>
        public string GetData(Data Value)
        {
            string strTemp = null;

            switch (Value)
            {
                case Data.Name:
                    strTemp = ProfileName;
                    break;

                case Data.ID:
                    strTemp = ID;
                    break;

                case Data.PW:
                    strTemp = PW;
                    break;
            }

            return strTemp;
        }
        
        /// <summary>
        /// 프로필 데이터를 설정합니다.
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="PW">비밀번호</param>
        public void SetData(string Name, string ID, string PW)
        {
            this.ProfileName = Name;
            this.ID = ID;
            this.PW = PW;
        }

        /// <summary>
        /// 프로필 파일을 저장합니다.
        /// </summary>
        /// <returns>저장 성공 여부</returns>
        public bool Save(bool Overwrite = true)
        {
            // 필드 검사
            List<string> list = new List<string>();
            if (ProfileName == string.Empty || ID == string.Empty)
                return false;

            // 저장
            list.Clear(); // 위에서 한번 썼으니 초기화!

            list.Add("ID|" + ID);
            if (PW != string.Empty)
                list.Add("PW|" + PW);

            if (Overwrite)
                File.Delete(DataPath.BSL.Profiles + ProfileName + ".bdx"); // 열렸던 파일 삭제
            else
                if (File.Exists(DataPath.BSL.Profiles + ProfileName + ".bdx"))
                    return false; // 덮어쓰기 사용안할시 파일이 이미 있으면 저장안함

            Protect.WriteBDXFile(DataPath.BSL.Profiles + ProfileName + ".bdx", list.ToArray()); // 프로필 파일 저장

            return true;
        }

        /// <summary>
        /// 프로필 파일을 삭제합니다.
        /// </summary>
        /// <returns>삭제 성공여부</returns>
        public bool RemoveData()
        {
            if (ProfileName == string.Empty)
                return false;

            if (File.Exists(DataPath.BSL.Profiles + ProfileName + ".bdx"))
                File.Delete(DataPath.BSL.Profiles + ProfileName + ".bdx");
            else
                return false;

            return true;
        }
    }
}
