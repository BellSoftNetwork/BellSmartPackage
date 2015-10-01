using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BellLib.Class
{
    /// <summary>
    /// WPF 프로젝트 공통함수
    /// </summary>
    public class WPFCom
    {
        public static MessageBoxResult Message(string messageBoxText, string caption = "Bell Smart Launcher", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Information, MessageBoxResult defaultResult = MessageBoxResult.OK, MessageBoxOptions options = MessageBoxOptions.None)
        {
            return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);
        }

        public static void End(bool Restart = false)
        {
            if (Restart)
                System.Windows.Forms.Application.Restart();
            Environment.Exit(0);
        }

        /// <summary>
        /// 해당 창 이름으로 창이 실행되어있는지 여부를 판단합니다.
        /// 창이 실행되어있을경우, 창을 활성화 시킵니다.
        /// </summary>
        /// <param name="windowName">창 이름 (대소문자 구별)</param>
        /// <returns>창 실행 가능 여부</returns>
        public static bool Feasibility(string windowName)
        {
            Window window = GetWindow(windowName);
            if (window == null)
                return true; // 폼이 실행되어 있지 않을경우 폼 실행 가능

            window.Activate();
            return false;
        }

        /// <summary>
        /// 해당 창 이름으로 창이 실행되어 있으면, 해당 창의 Window 값을 반환합니다.
        /// </summary>
        /// <param name="className">창 클래스 이름 (대소문자 구별)</param>
        /// <returns>Window 정보</returns>
        public static Window GetWindow(string className)
        {
            foreach (Window window in Application.Current.Windows)
                if (window.ToString() == className)
                    return window;

            return null;
        }
    }
}
