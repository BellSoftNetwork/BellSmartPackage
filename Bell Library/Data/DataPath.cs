using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BellLib.Data
{
    /// <summary>
    /// 데이터 파일 경로
    /// </summary>
    public class DataPath
    {
        /// <summary>
        /// Bell Smart Launcher 관련 데이터 파일 경로
        /// </summary>
        public class BSL
        {
            /// <summary>
            /// 일반 설정 bdx 파일 경로입니다.
            /// </summary>
            public static string General
            {
                get
                {
                    return User.BSN_Path + "DATA\\BSL\\General.bdx";
                }
            }

            /// <summary>
            /// 게임 설정 bdx 파일 경로입니다.
            /// </summary>
            public static string Game_Setting
            {
                get
                {
                    return User.BSN_Path + "DATA\\BSL\\Game.bdx";
                }
            }

            /// <summary>
            /// 디버그 설정 bdx 파일 경로입니다.
            /// </summary>
            public static string Debug_Setting
            {
                get
                {
                    return User.BSN_Path + "DATA\\BSL\\Debug.bdx";
                }
            }

            /// <summary>
            /// 모드팩 데이터 bdx 파일 경로입니다.
            /// </summary>
            public static string Modpacks
            {
                get
                {
                    return User.BSN_Path + "DATA\\BSL\\Modpacks.bdx";
                }
            }

            /// <summary>
            /// 리소스팩 데이터 bdx 파일 경로입니다.
            /// </summary>
            public static string Resources
            {
                get
                {
                    return User.BSN_Path + "DATA\\BSL\\Resources.bdx";
                }
            }

            /// <summary>
            /// 프로필 폴더 경로입니다.
            /// </summary>
            public static string Profiles
            {
                get
                {
                    return User.BSN_Path + "Data\\BSL\\Profile\\";
                }
            }
        }
    }
}
