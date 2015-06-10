using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Microsoft.Win32;

namespace BellLib.Class
{
    delegate void OnEnd();

    public class Common
    {
        static bool onExit = false;

        static OnEnd onEnd;

        public static DialogResult Message(string Text, string Caption = "Bell Smart Tools", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            Debug.Message(Debug.Level.Log, Text, Caption, buttons, icon, defaultButton, "MessageBox");
            return MessageBox.Show(Text, Caption, buttons, icon, defaultButton);
        }

        public static void End(bool Restart = false)
        {
            // 콜백에 프로그램 종료를 알린다.
            onEnd += new OnEnd(() => {
                if (!onExit)
                    Debug.Message(Debug.Level.Log, "End point of program. Exiting.");
            });

            // 콜백이 한번만 실행되도록 한다.
            onEnd += new OnEnd(() => onExit = true);

            // 콜백에 프로그램 종료를 체인한다.
            if (Restart)
                onEnd += Application.Restart;
            else
                onEnd += Application.Exit;

            // 콜백을 호출한다.
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

        public static void WriteTextFile(string localFilePath, string data, bool append = false)
        {
            bool written = false;
            while (!written) // 이 구문 현재 LocalFilePath 경로가 존재하지 않으면(폴더가 생성되있지 않으면), 무한루프 도는 구조. 수정 요망
            {
                try
                {
                    if (append)
                        File.AppendAllText(localFilePath, data);
                    else
                        File.WriteAllText(localFilePath, data);

                    written = true;
                }
                catch (Exception ex)
                {
                    Debug.Message(Debug.Level.High, "WriteTextFile" + Environment.NewLine + ex.Message);
                    Debug.Message(Debug.Level.High, "Trying to create folder.");
                    Directory.CreateDirectory(localFilePath);
                }
            }
        }

        public static string GetStringFromWeb(string URL)
        {
            try
            {
                return new WebClient().DownloadString(URL);
            }
            catch { return null; }
        }

        public static void CreateDefaultForder()
        {
            CreateFolder(Data.User.BSN_Path);
            CreateFolder(Data.User.BSN_Path + "logs\\");
            CreateFolder(Data.User.BSL_Root + "ModPack");
            CreateFolder(Data.User.BSL_Root + "Base");
        }

        private static void CreateFolder(string folderPath)
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }

        /// <summary>
        /// 해당 디렉토리의 파일 포맷을 가진 모든 파일을 삭제합니다.
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="Format"></param>
        /// <returns>파일 존재 여부</returns>
        public static bool DeleteDirectoryFile(string dirPath, string format = "*.*")
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            System.IO.FileInfo[] files;

            files = dir.GetFiles(format, SearchOption.AllDirectories);
            if (files.Length <= 0)
            {
                return false;
            }

            foreach (System.IO.FileInfo file in files)
            {
                // 만약 ReadOnly 속성이 있는 파일이 있다면 지울때 에러가 나므로 속성을 Normal로 바꿔놓는다.
                if (file.Attributes == FileAttributes.ReadOnly)
                    file.Attributes = FileAttributes.Normal;
                file.Delete();
            }

            return true;
        }
    }
}
