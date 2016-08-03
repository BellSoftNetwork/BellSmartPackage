using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BellLib.Class.Control
{
    public class UpdateControl
    {

        /// <summary>
        /// 잠금 플래그 설정을 위한 잠금 비트 열거형.
        /// </summary>
        [Flags]
        public enum LockBit
        {
            UnLock = 0x0,
            AllLock = ~UnLock,
            Running_Game = 0x1,
            Install_Game = 0x2,
            Install_Runtime = 0x4
        }

    }
}
