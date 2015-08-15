using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BellLib.Class
{
    public class FileSystem
    {
        /// <summary>
        /// 텍스트 파일을 작성합니다.
        /// </summary>
        /// <param name="localFilePath">저장할 파일 경로</param>
        /// <param name="data">저장할 텍스트 값</param>
        /// <param name="append">이어쓸지 여부</param>
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

        public static void CreateFolder(string folderPath)
        {
            Directory.CreateDirectory(folderPath);
        }

        /// <summary>
        /// 기본적으로 존재해야하는 디렉토리를 생성합니다.
        /// </summary>
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
            CreateFolder(Data.User.BSN_Path + "Upload\\Runtime");
            CreateFolder(Data.User.BSN_Path + "Upload\\Runtime\\JAVA");

            CreateFolder(Data.User.BSN_Path + "Data");


            CreateFolder(Data.User.BSL_Root + "Data");
            CreateFolder(Data.User.BSL_Root + "Data\\BSL");
            CreateFolder(Data.User.BSL_Root + "Data\\BSL\\Profile");

            CreateFolder(Data.User.BSL_Root + "ModPack");
            CreateFolder(Data.User.BSL_Root + "Base");
            CreateFolder(Data.User.BSL_Root + "Runtime");
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
    }
}
