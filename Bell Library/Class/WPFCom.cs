using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace BellLib.Class
{
    /// <summary>
    /// WPF 프로젝트 공통함수
    /// </summary>
    public class WPFCom
    {
        /// <summary>
        /// WPF 프로젝트 공통 메시지박스 함수입니다.
        /// </summary>
        /// <param name="messageBoxText">메시지박스 내용</param>
        /// <param name="project">프로젝트</param>
        /// <param name="button">버튼</param>
        /// <param name="icon">아이콘</param>
        /// <param name="defaultResult">기본 포커스</param>
        /// <param name="options">옵션</param>
        /// <returns>선택값</returns>
        public static MessageBoxResult Message(string messageBoxText, Basic.PROJECT project, MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Information, MessageBoxResult defaultResult = MessageBoxResult.OK, MessageBoxOptions options = MessageBoxOptions.None)
        {
            string title = "Bell Smart Package";

            switch(project)
            {
                case Basic.PROJECT.Bell_Smart_Launcher:
                    title = "Bell Smart Launcher";
                    break;

                case Basic.PROJECT.Bell_Smart_Manager:
                    title = "Bell Smart Manager";
                    break;

                case Basic.PROJECT.Bell_Smart_Server:
                    title = "Bell Smart Server";
                    break;

                case Basic.PROJECT.Bell_Smart_Tools:
                    title = "Bell Smart Tools";
                    break;
            }

            return WPFCom.Message(messageBoxText, title, button, icon, defaultResult, options);
        }

        /// <summary>
        /// WPF 프로젝트 공통 메시지박스 함수입니다.
        /// 캡션을 직접 설정할 수 있습니다.
        /// </summary>
        /// <param name="messageBoxText">메시지박스 내용</param>
        /// <param name="caption">캡션</param>
        /// <param name="button">버튼</param>
        /// <param name="icon">아이콘</param>
        /// <param name="defaultResult">기본 포커스</param>
        /// <param name="options">옵션</param>
        /// <returns>선택값</returns>
        public static MessageBoxResult Message(string messageBoxText, string caption, MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Information, MessageBoxResult defaultResult = MessageBoxResult.OK, MessageBoxOptions options = MessageBoxOptions.None)
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

        private static DispatcherOperationCallback exitFrameCallback = new DispatcherOperationCallback(ExitFrame);
        
        /// <summary>
        /// Processes all UI messages currently in the message queue.
        /// </summary>
        public static void DoEvents()
        {
            // Create new nested message pump.
            DispatcherFrame nestedFrame = new DispatcherFrame();
            
            // Dispatch a callback to the current message queue, when getting called, 
            // this callback will end the nested message loop.
            // note that the priority of this callback should be lower than the that of UI event messages.
            DispatcherOperation exitOperation = Dispatcher.CurrentDispatcher.BeginInvoke(
                                                  DispatcherPriority.Background, exitFrameCallback, nestedFrame);
           
            // pump the nested message loop, the nested message loop will 
            // immediately process the messages left inside the message queue.
            Dispatcher.PushFrame(nestedFrame);
            
            // If the "exitFrame" callback doesn't get finished, Abort it.
            if (exitOperation.Status != DispatcherOperationStatus.Completed)
            {
                exitOperation.Abort();
            }
        }
        
        private static Object ExitFrame(Object state)
        {
            DispatcherFrame frame = state as DispatcherFrame;
            
            // Exit the nested message loop.
            frame.Continue = false;
            return null;
        }
    }
}
