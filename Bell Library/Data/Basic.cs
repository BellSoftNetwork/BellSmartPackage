using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BellLib.Data
{
    /// <summary>
    /// 기본 정보
    /// </summary>
    public class Basic
    {
        /// <summary>
        /// Bell Library 버전
        /// </summary>
        public const string VERSION = "1.1.0";

        /// <summary>
        /// 호환되는 프로프램 열거형
        /// </summary>
        public enum PROJECT
        {
            Bell_Smart_Package,
            Bell_Smart_Launcher,
            Bell_Smart_Manager,
            Bell_Smart_Server,
            Bell_Smart_Tools
        }
        
        public static string[] LST_ACCENT = { "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
        public static string[] LST_ACCENT_KOR = { "빨강", "초록", "파랑", "보라", "주황", "라임", "에메랄드", "물오리", "하늘", "코발트", "남빛", "제비꽃", "분홍", "짙은 흥", "진홍", "호박", "노랑", "갈", "올리브", "강철", "자주 빛", "짙은 회갈", "시에나" };

        public static string[] LST_THEME = { "BaseLight", "BaseDark" };
        public static string[] LST_THEME_KOR = { "하얀 배경", "검은 배경" };

    }
}
