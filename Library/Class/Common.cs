using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Microsoft.Win32;

namespace BellLib.Class
{
    delegate void OnEnd();

    public class Common
    {
        static OnEnd onEnd;

        public static DialogResult Message(string Text, string Caption = "Bell Smart Tools", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return MessageBox.Show(Text, Caption, buttons, icon, defaultButton);
        }

        public static void End(bool Restart = false)
        {
            if (Restart)
                onEnd += Application.Restart;
            else
                onEnd += Application.Exit;

            onEnd();
        }

        [Obsolete("RegistryManager 클래스를 사용하십시오.")]
        public static void RegSave(string name, object value)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("BSN");
            reg.SetValue(name, value, RegistryValueKind.String);
        }

        [Obsolete("RegistryReader 클래스를 사용하십시오.")]
        public static string RegLoad(string name, object defaultValue = null, bool throwException = false)
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("BSN");
                return (string)reg.GetValue(name, null);
            }
            catch { }

            return null;
        }

        [Obsolete("RegistryManager 클래스를 사용하십시오.")]
        public static void RegDelete(string name)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("BSN");
            try
            {
                reg.DeleteValue(name); // 레지스트리 삭제. 이미 삭제되어있을경우 catch문 실행.
            }
            catch { }
        }

        public static void WriteTextFile(string LocalFilePath, string Data, bool Append = false)
        {
            bool boolWritten = false;

            while (!boolWritten) // 이 구문 현재 LocalFilePath 경로가 존재하지 않으면(폴더가 생성되있지 않으면), 무한루프 도는 구조. 수정 요망
            {
                try
                {
                    if (Append)
                        File.AppendAllText(LocalFilePath, Data);
                    else
                        File.WriteAllText(LocalFilePath, Data);

                    boolWritten = true;
                }
                catch (Exception ex)
                {
                    Debug.Message(Debug.Level.High, "WriteTextFile" + Environment.NewLine + ex.Message);
                }
            }
        }

        public static string GetStringFromWeb(string URL)
        {
            try
            {
                return new WebClient().DownloadString(URL);
            }
            catch
            {
                return null;
            }
        }


        public static void CreateDefaultForder(string Default_PATH = null)
        {
            if (Default_PATH == null)
                Default_PATH = Data.User.BSN_Path; //Environment.GetEnvironmentVariable("SYSTEMDRIVE") + "\\BSN\\";

            CreateFolder(Default_PATH);
            CreateFolder(Default_PATH + "Temp\\");
            CreateFolder(Default_PATH + "logs\\");
            CreateFolder(Default_PATH + "Utilities\\");
        }
        private static void CreateFolder(string FolderURL)
        {
            System.IO.Directory.CreateDirectory(FolderURL);
        }
    }
}
