using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace BellLib.Class
{
    public delegate void FTPDownloadTotalSizeHandle(long totalSize);
    public delegate void FTPDownloadReceivedSizeHandle(int RcvSize);
    public class FTPUtil
    {
        public event FTPDownloadTotalSizeHandle ftpDNTotalSizeEvt;
        public event FTPDownloadReceivedSizeHandle ftpDNRcvSizeEvt;

        string ftpServerIP = null;
        string ftpUserID = null;
        string ftpPassword = null;
        string ftpPort = null;
        bool usePassive = false;


        //ftpUtil 객체를 생성할때 생성자에 인자로 필요한 값들을 넣어준다.
        /// <summary>
        /// FTPUtill을 초기화합니다.
        /// </summary>
        /// <param name="ip">FTP 서버 IP</param>
        /// <param name="id">FTP 계정 ID</param>
        /// <param name="pw">FTP 계정 PW</param>
        /// <param name="port">FTP 포트</param>
        public FTPUtil(string ip, string id, string pw, string port = "21")
        {
            ftpServerIP = ip;   //FTP 서버주소
            ftpUserID = id;     //아이디
            ftpPassword = pw;  //패스워드
            ftpPort = port;        //포트
            usePassive = true;   //패시브모드 사용여부
        }

        /// <summary>
        /// Method to upload the specified file to the specified FTP Server
        /// </summary>
        /// <param name="filename">file full name to be uploaded</param>
        //파일 업로드
        public Boolean Upload(string folder, string filename)
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://" + ftpServerIP + ":" + ftpPort + "/" + folder + "/" + fileInf.Name;
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            reqFTP.KeepAlive = false;

            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            reqFTP.UseBinary = true;
            reqFTP.UsePassive = usePassive;

            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = fileInf.Length;

            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            FileStream fs = fileInf.OpenRead();

            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();

                return true;
            }
            catch
            {
                Common.Message("FTP 파일 전송중 문제가 발생하였습니다." + Environment.NewLine + "네트워크 상황 또는 접속정보를 살펴 보시기 바랍니다.");
                // FTP 서버에 동일파일이 있고, 계정에 삭제권한이 없을때도 이 예외가 발생함.
                return false;
            }

        }

        /// <summary>
        /// FTP 서버에서 지정한 폴더의 파일들을 전부 삭제합니다. (하위폴더 포함)
        /// </summary>
        /// <param name="dirPath">삭제할 디렉토리 경로</param>
        public void DeletePath(string dirPath)
        {
            string[] FileList = GetFileList(dirPath); // 지정한 경로의 파일리스트 받아옴
            try
            {
                foreach (string FilePath in FileList)
                {
                    // 파일인지 경로인지 판단 후 파일이면 파일만 삭제, 경로면 재귀함수 호출 (판단이 복잡해서 보류)
                    string[] Data = FilePath.Split('.');
                    string[] Extension = { "jar", "exe", "cfg", "dat", "zip", "ini", "txt", "conf", "json", "xml", "prop", "png", "dll" };
                    bool isFile = false;
                    
                    foreach (string Ext in Extension)
                    { // 확장자 검사
                        if (Data[Data.Length - 1] == Ext)
                        {
                            isFile = true;
                            break;
                        }
                    }

                    if (isFile) // 이 경로가 파일이면,
                    {
                        DeleteFile(dirPath + FilePath); // 파일 삭제
                    }
                    else
                    { // 이 경로가 디렉토리이면,
                        DeleteFile(dirPath + FilePath); // 디렉토리가 아닐수도 있으니 삭제 시도
                        DeletePath(dirPath + FilePath + "/"); // 재귀함수 호출
                    }
                    Application.DoEvents();
                }
            }
            catch
            {
            }

            // 위에서 하위폴더 다 삭제했으므로
            RemoveDir(dirPath); // 루트 디렉토리 삭제
        }

        /// <summary>
        /// FTP 서버에 업로드된 파일을 삭제합니다.
        /// </summary>
        /// <param name="fileName">파일 경로</param>
        public void DeleteFile(string fileName)
        {
            try
            {
                string uri = "ftp://" + ftpServerIP + "/" + fileName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName));

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch { return; }
            //catch (Exception ex){MessageBox.Show(ex.Message, "FTP 2.0 Delete");}
        }

        /// <summary>
        /// FTP 서버에 생성된 디렉토리를 삭제합니다.
        /// </summary>
        /// <param name="dirName">디렉토리 경로</param>
        public void RemoveDir(string dirName)
        {
            try
            {
                string uri = "ftp://" + ftpServerIP + "/" + dirName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + dirName));

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;

                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch { return; }
            //catch (Exception ex){MessageBox.Show(ex.Message, "FTP 2.0 Delete");}
        }

        public bool GetFilesInfo(string filename, ref DateTime dt)
        {
            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + filename));
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = usePassive;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.GetDateTimestamp;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                dt = response.LastModified;

                response.Close();
                return true;

            }
            catch { return false; }
            //catch (Exception ex){System.Windows.Forms.MessageBox.Show(ex.Message);return false;}
        }

        public List<string> GetFilesDetailList(string subFolder)
        {
            List<string> files = new List<string>();
            string line = null;


            try
            {
                //StringBuilder result = new StringBuilder();

                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + subFolder));
                ftp.UseBinary = true;
                ftp.UsePassive = usePassive;
                ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);



                while ((line = reader.ReadLine()) != null)
                {
                    files.Add(line);
                }

                reader.Close();
                response.Close();
                return files;
                //MessageBox.Show(result.ToString().Split('\n'));
            }
            catch { return files; }
            //catch (Exception ex){System.Windows.Forms.MessageBox.Show(ex.Message);return files;}
        }

        /// <summary>
        /// FTP 서버에 업로드된 파일 및 디렉토리 리스트를 반환합니다.
        /// </summary>
        /// <param name="subFolder">FTP 경로</param>
        /// <returns>파일 및 디렉토리 배열</returns>
        public string[] GetFileList(string subFolder)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + subFolder));
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = usePassive;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                //MessageBox.Show(reader.ReadToEnd());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                //MessageBox.Show(response.StatusDescription);
                return result.ToString().Split('\n');
            }
            catch
            {
                downloadFiles = null;
                return downloadFiles;
            }
            /*
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
            */
        }

        public bool checkDir(string localFullPathFile)
        {
            FileInfo fInfo = new FileInfo(localFullPathFile);

            if (!fInfo.Exists)
            {
                DirectoryInfo dInfo = new DirectoryInfo(fInfo.DirectoryName);
                if (!dInfo.Exists)
                {
                    dInfo.Create();
                }
                //dInfo.Delete();
            }

            //fInfo.Delete();
            return true;

        }
        public bool Download(string localFullPathFile, string serverFullPathFile)
        {
            FtpWebRequest reqFTP;
            try
            {
                //filePath = <<The full path where the file is to be created.>>, 
                //fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>
                checkDir(localFullPathFile);
                FileStream outputStream = new FileStream(localFullPathFile, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + serverFullPathFile));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = usePassive;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;

                if (ftpDNTotalSizeEvt != null) ftpDNTotalSizeEvt(cl);

                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    if (ftpDNRcvSizeEvt != null) ftpDNRcvSizeEvt(readCount);

                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
                return true;
            }
            catch { return false; }
            /*
            catch (Exception ex)
            {
                MessageBox.Show("download()"+ex.Message);
                return false;
            }
            */
        }

        private long GetFileSize(string filename)
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + filename));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = usePassive;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                fileSize = response.ContentLength;

                ftpStream.Close();
                response.Close();

                return fileSize;
            }

            catch { return fileSize; }
            /*
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return fileSize;
            */
        }

        public void Rename(string currentFilename, string newFilename)
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + currentFilename));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = usePassive;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }

            catch { }
            /*
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        /// <summary>
        /// FTP 서버에 디렉토리를 생성합니다.
        /// </summary>
        /// <param name="dirName">생성할 디렉토리 경로</param>
        public void MakeDir(string dirName)
        {
            FtpWebRequest reqFTP;
            try
            {
                // dirName = name of the directory to create.
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + dirName));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = usePassive;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch { }
            /*
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

    }
}
