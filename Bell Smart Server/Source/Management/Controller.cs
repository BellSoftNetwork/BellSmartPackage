using Bell_Smart_Server.Source.Frame;
using BellLib.Class;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Bell_Smart_Server.Source.Management
{
    public class Controller
    {
        /// <summary>
        /// 비트 단위로 잠금 여부 설정
        /// 0000 0000 0000 0000 0000 0000 0000 0000
        /// ~ 000* : 서버 가동 여부
        /// </summary>
        private static int LockFlag;

        /// <summary>
        /// 잠금 플래그 설정을 위한 잠금 비트 열거형.
        /// </summary>
        public enum LockBit
        {
            UnLock = 0x0,
            AllLock = ~UnLock,
            Running_Server = 0x1
        }

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
        /// 업데이트가 가능하면 업데이트를 시행합니다.
        /// </summary>
        /// <returns>업데이트 진행여부</returns>
        public static bool UpdateCheck()
        {
            if (LockFlag > 0)
                return false;

            try
            {
                if (Deploy.UpdateAvailable())
                {
                    Updater Upd = new Updater();
                    Upd.Show();

                    return true;
                }
            }
            catch
            {
                throw;
            }

            return false;
        }
    }
}
