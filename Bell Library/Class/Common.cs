using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;

namespace BellLib.Class
{
    delegate void OnEnd();

    public class Common
    {
        static bool onExit = false;

        static OnEnd onEnd;

        public static DialogResult Message(string Text, string Caption = "Bell Smart Package", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
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
            CreateFolder(Data.User.BSN_Path + "logs");
            CreateFolder(Data.User.BSN_Path + "Temp");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data\\ModPack");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data\\ModPack\\Version");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data\\BasePack");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data\\BasePack\\Version");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data\\OptionPack");
            CreateFolder(Data.User.BSN_Path + "Temp\\BSU\\Data\\OptionPack\\Version");

            CreateFolder(Data.User.BSN_Path + "Upload");
            CreateFolder(Data.User.BSN_Path + "Upload\\ModPack");
            CreateFolder(Data.User.BSN_Path + "Upload\\BasePack");
            CreateFolder(Data.User.BSN_Path + "Upload\\OptionPack");

            CreateFolder(Data.User.BSN_Path + "Data");


            CreateFolder(Data.User.BSL_Root + "Data");
            CreateFolder(Data.User.BSL_Root + "Data\\BSL");
            CreateFolder(Data.User.BSL_Root + "Data\\BSL\\Profile");

            CreateFolder(Data.User.BSL_Root + "ModPack");
            CreateFolder(Data.User.BSL_Root + "Base");
        }

        private static void CreateFolder(string folderPath)
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }

        /// <summary>
        /// 새로운 폼의 인스턴스 생성하고 보여줍니다.
        /// </summary>
        /// <param name="iName">폼 클래스 인스턴스의 이름입니다.</param>
        /// <param name="asm">
        /// 폼 클래스 인스턴스가 포함된 어셈블리입니다.
        /// 불분명한 경우는 Assembly.GetExecutingAssembly()를 사용하십시오.
        /// Bell Library 내부에서 호출 할 경우에는 값을 할당하지 않아도 됩니다.
        /// </param>
        /// <returns>성공 여부를 반환합니다.</returns>
        public static bool CreateFormAndShow(string iName, bool throwException = false)
        {
            try
            {
                Type type = Assembly.GetCallingAssembly().GetTypes().First(t => t.Name == iName);

                object instance = Activator.CreateInstance(type);

                try
                {
                    type.GetMethod("Show", BindingFlags.Public | BindingFlags.Instance).Invoke(instance, null);
                }
                catch (AmbiguousMatchException)
                {
                    foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                        if (method.Name == "Show" && method.GetParameters().Length == 0)
                            method.Invoke(instance, null);
                }

                return true;
            }
            catch
            {
                if (throwException)
                    throw;

                return false;
            }
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

        /// <summary>
        /// 디렉토리 배열을 반환합니다.
        /// </summary>
        /// <param name="strFilePath">최상위 경로</param>
        /// <param name="Replace">최상위 경로를 제거한 뒤 반환할지 여부</param>
        /// <returns>디렉토리 리스트</returns>
        public static string[] GetDirectoryArray(string strFilePath, bool Replace)
        {
            if (strFilePath[strFilePath.Length - 1] == '\\')
                strFilePath = strFilePath.Substring(0, strFilePath.Length - 1);
            List<string> list = new List<string>();
            foreach (FileInfo File in (new DirectoryInfo(strFilePath)).GetFiles("*", SearchOption.AllDirectories))
            {
                string Path = File.DirectoryName;
                if (Replace)
                {
                    Path = Path.Replace(strFilePath + '\\', string.Empty);
                    Path = Path.Replace(strFilePath, string.Empty);
                }
                if (!list.Contains(Path) && Path != string.Empty)
                    list.Add(Path);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 파일 배열을 반환합니다.
        /// </summary>
        /// <param name="strFilePath">최상위 폴더</param>
        /// <param name="Replace">최상위 경로를 제거할지 여부</param>
        /// <returns>파일 리스트 배열</returns>
        public static string[] GetFileArray(string strFilePath, bool Replace)
        {
            List<string> list = new List<string>();
            foreach (FileInfo File in (new DirectoryInfo(strFilePath)).GetFiles("*", SearchOption.AllDirectories))
            {
                string Path = File.FullName;
                if (Replace)
                    Path = Path.Replace(strFilePath, string.Empty);
                if (!list.Contains(Path))
                    list.Add(Path);
            }

            return list.ToArray();
        }

        /// <summary>
        /// .bd (Bell Data) 파일을 작성합니다.
        /// </summary>
        /// <param name="Path">작성할 .bd파일 경로</param>
        /// <param name="Text">작성할 텍스트</param>
        /// <param name="Append">내용 추가여부</param>
        /// <param name="Encryption">암호화 작성여부</param>
        public static void WriteBDText(string Path, string Text, bool Append = false, bool Encryption = true)
        {
            if (Encryption)
            {
                Protection Pro = new Protection();
                Text = Pro.Base64(Text, Protection.ProtectionType.PROTECTION_ENCODE);
            }
            WriteTextFile(Path, Text, Append);
        }

        /// <summary>
        /// .bd (Bell Data) 파일을 읽습니다.
        /// </summary>
        /// <param name="Path">.bd파일 경로</param>
        /// <param name="Decryption">복호화 여부</param>
        /// <returns>복호화된 bd파일 값</returns>
        public static string ReadBDText(string Path, bool Decryption = true)
        {
            string Text = File.ReadAllText(Path);
            if (Decryption)
            {
                Protection Pro = new Protection();
                Text = Pro.Base64(Text, Protection.ProtectionType.PROTECTION_DECODE);
            }

            return Text;
        }
    }
}
