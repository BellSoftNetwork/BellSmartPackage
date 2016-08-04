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
    /// 서버 상세정보 제어 클래스
    /// </summary>
    public class ServerDetail
    {
        // 필드
        private string ServerName;
        private bool AutoRestart;
        private int AutoRestartTime;

        /// <summary>
        /// 데이터 타입 열거형
        /// </summary>
        public enum Data
        {
            ServerName,
            AutoRestart,
            AutoRestartTime
        }

        /// <summary>
        /// 서버를 선택한 설정으로 초기화합니다.
        /// </summary>
        /// <param name="ServerName"></param>
        public ServerDetail(string ServerName)
        {
            this.ServerName = ServerName;
            if (DataProtect.DataLoad(DataPath.BSS.ServerProfiles + this.ServerName + ".bdx", "AutoRestart") == "True")
                this.AutoRestart = true;
            else
                this.AutoRestart = false;

            try
            {
                this.AutoRestartTime = Convert.ToInt32(DataProtect.DataLoad(DataPath.BSS.ServerProfiles + this.ServerName + ".bdx", "AutoRestartTime"));
            }
            catch { }
            if (this.AutoRestartTime < 1)
                this.AutoRestartTime = 3;

        }

        /// <summary>
        /// 프로필 데이터를 string형으로 가져옵니다.
        /// </summary>
        /// <param name="Value">반환할 프로필 데이터 값</param>
        /// <returns>프로필 데이터 (string)</returns>
        public string GetDataString(Data Value)
        {
            switch (Value)
            {
                case Data.ServerName:
                    return ServerName;

                case Data.AutoRestart:
                    return AutoRestart.ToString();

                case Data.AutoRestartTime:
                    return AutoRestartTime.ToString();

                default:
                    return null;
            }
        }

        /// <summary>
        /// 프로필 데이터를 int형으로 가져옵니다.
        /// </summary>
        /// <param name="Value">반환할 프로필 데이터 값</param>
        /// <returns>프로필 데이터 (int)</returns>
        public int GetDataInt(Data Value)
        {
            switch (Value)
            {
                case Data.AutoRestartTime:
                    return AutoRestartTime;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// 프로필 데이터를 설정합니다.
        /// </summary>
        /// <param name="AutoRestart">자동 재시작</param>
        /// <param name="AutoRestartTime">자동 재시작 대기시간 (초)</param>
        public void SetData(bool AutoRestart, int AutoRestartTime)
        {
            this.AutoRestart = AutoRestart;
            this.AutoRestartTime = AutoRestartTime;
        }

        /// <summary>
        /// 프로필 파일을 저장합니다.
        /// </summary>
        /// <returns>저장 성공 여부</returns>
        public bool Save()
        {
            // 필드 검사
            if (ServerName == string.Empty)
                return false;

            // 저장
            DataProtect.DataSave(DataPath.BSS.ServerProfiles + ServerName + ".bdx", "AutoRestart", AutoRestart.ToString());
            DataProtect.DataSave(DataPath.BSS.ServerProfiles + ServerName + ".bdx", "AutoRestartTime", AutoRestartTime.ToString());

            return true;
        }
    }
}
