using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Bell_Smart_Launcher.Source.Class
{
    /// <summary>
    /// Common 으로하면 BellLib 클래스 이름과 겹쳐서 Common 을 줄여서 Com으로..
    /// </summary>
    public class Com
    {
        public static MessageBoxResult Message(string messageBoxText, string caption = "Bell Smart Launcher", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Information, MessageBoxResult defaultResult = MessageBoxResult.OK, MessageBoxOptions options = MessageBoxOptions.None)
        {
            return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);
        }

        public static void End(bool Restart = false)
        {
            if (!Restart)
            {
                Environment.Exit(0);
            }
            else
            {
                // 리스타트 구현필요
            }
        }
    }
}
