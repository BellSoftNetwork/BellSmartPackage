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
        /// ~ 000* : 게임 실행 여부
        /// ~ 00*0 : 게임 설치중
        /// ~ 0*00 : 
        /// </summary>
        private static int LockFlag;
        private DispatcherTimer tmr_Update = new DispatcherTimer();

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
        /// 런처 컨트롤러를 생성합니다.
        /// </summary>
        public Controller()
        {
            tmr_Update.Interval = TimeSpan.FromSeconds(30);
            tmr_Update.Tick += new EventHandler(Update_Tick);
        }

        /// <summary>
        /// 런처 컨트롤러를 초기화합니다.
        /// </summary>
        public void Initialize()
        {
            tmr_Update.Start();
            //Update_Tick(null, null);
        }

        private int errCount = 0;
        private void Update_Tick(object sender, EventArgs e)
        {
            if (LockFlag > 0)
                return;

            if (User.BSP_AutoUpdate)
            {
                try
                {
                    if (UpdateCheck())
                        tmr_Update.Stop(); // 업데이트 실행 후 타이머 중단
                }
                catch (Exception ex)
                {
                    if (errCount > 2)
                    {
                        WPFCom.Message("자동 업데이트 시스템 동작 중 문제가 발생하였습니다." + Environment.NewLine + "이 에러메시지가 자주 발생한다면 BSN 홈페이지에 피드백을 올려주시기 바랍니다." + Environment.NewLine + "errCount = " + errCount + Environment.NewLine + ex.Message + Environment.NewLine + "StackTrace : " + ex.StackTrace);
                        errCount = 0;
                    }
                    else
                    {
                        errCount += 1;
                    }

                    return;
                }
                errCount = 0;
            }
        }

        public static bool UpdateCheck()
        {
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
