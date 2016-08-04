using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BellLib.Class.Control
{
    public class UpdateControl
    {
        /// <summary>
        /// 비트 단위로 잠금 여부 설정
        /// 0000 0000 0000 0000 0000 0000 0000 0000
        /// ~ 000* : 게임 실행 여부
        /// ~ 00*0 : 게임 설치중
        /// ~ 0*00 : 
        /// </summary>
        private static int LockFlag { get; set; }
        private static int errCount { get; set; }

        /// <summary>
        /// 잠금 플래그 설정을 위한 잠금 비트 열거형.
        /// </summary>
        [Flags]
        public enum LockBit
        {
            UnLock = 0x00,
            AllLock = ~UnLock,
            Running_Server = 0x01,
            Running_Game = 0x02,
            Install_Game = 0x04,
            Install_Runtime = 0x08
        }

        /// <summary>
        /// 잠금 플래그를 설정합니다.
        /// </summary>
        /// <param name="lb">잠금 비트</param>
        /// <param name="Lock">잠금 여부</param>
        public static void SetLockFlag(LockBit lb, bool Lock = true)
        {
            if (Lock)
            {
                // 잠금비트 설정
                LockFlag |= (int)lb; // 락 비트를 OR 연산
            }
            else
            {
                // 잠금비트 해제
                LockFlag &= (int)~lb; // 락 비트를 반전시켜서 AND 연산
            }
        }

        /// <summary>
        /// 잠금 플래그 설정을 가져옵니다.
        /// </summary>
        /// <returns>잠금 비트</returns>
        public static LockBit[] GetLockFlag()
        {
            List<LockBit> currentLock = new List<LockBit>();
            int temp = 1;

            // 한비트씩 이동하면서 AND 연산으로 잠금여부를 가져옴
            for (int i = 1; i < 32; i++)
            {
                if ((temp & LockFlag) == temp)
                    currentLock.Add((LockBit)temp);
                temp <<= 1;
            }

            return currentLock.ToArray();
        }

        /// <summary>
        /// 업데이트 잠금여부를 반환합니다.
        /// </summary>
        /// <returns>업데이트 잠금여부</returns>
        public static bool IsLock()
        {
            if (LockFlag > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 최신버전 유무를 검사합니다.
        /// </summary>
        /// <param name="loop">업데이트 발견시까지 반복여부</param>
        /// <param name="Delay">딜레이 시간 ms</param>
        /// <returns>업데이트 가능유무</returns>
        public static bool UpdateCheck(bool loop = true, int Delay = 30000)
        {
            if (!loop)
                return Deploy.UpdateAvailable();

            while (true)
            {
                if (User.BSP_AutoUpdate && !IsLock())
                {
                    try
                    {
                        if (Deploy.UpdateAvailable()) // 신규버전 발견
                            return true;
                    }
                    catch (Exception ex)
                    {
                        if (errCount > 2)
                        {
                            WPFCom.Message("자동 업데이트 시스템 동작 중 문제가 발생하였습니다." + Environment.NewLine + "이 에러메시지가 자주 발생한다면 BSN 홈페이지에 피드백을 올려주시기 바랍니다." + Environment.NewLine + ex.Message + Environment.NewLine + "StackTrace : " + ex.StackTrace, Base.PROJECT.Bell_Smart_Package);

                            return false;
                        }
                        else
                            errCount += 1;
                    }
                }

                Thread.Sleep(30000);
            }
        }
    }
}
