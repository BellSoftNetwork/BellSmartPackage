using BellLib.Class;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Tools.Source.BSU
{
    public partial class BSU_RuntimeManager : Form
    {
        public BSU_RuntimeManager()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            llb_JAVA_Upload.Tag = User.BSN_Path + "Upload\\Runtime\\JAVA\\";
            llb_JAVA_Upload.Text = "업로드 폴더 : " + (string)llb_JAVA_Upload.Tag;
        }

        private void btn_JAVA_Load_Click(object sender, EventArgs e)
        {
            lst_JAVA_File.Items.Clear();
            lst_JAVA_File.Items.AddRange(Common.GetFileArray((string)llb_JAVA_Upload.Tag, true));
        }

        private void llb_JAVA_Upload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start((string)llb_JAVA_Upload.Tag);
            }
            catch (Exception ex)
            {
                Common.Message("해당 폴더를 여는 중, 문제가 발생하였습니다." + Environment.NewLine + ex.Message);
            }
        }

        private void btn_JAVA_Upload_Click(object sender, EventArgs e)
        {
            btn_JAVA_Upload.Enabled = false;
            btn_JAVA_Load_Click(sender, e);
            Application.DoEvents();
            bool x64 = rb_JAVA_64.Checked;
            RuntimeAnalysisWrite RAW = new RuntimeAnalysisWrite(RuntimeAnalysis.RunType.JAVA);

            if (x64)
            {
                RAW.SetJava(RuntimeAnalysis.JAVAType.x64);
            }
            else
            {
                RAW.SetJava(RuntimeAnalysis.JAVAType.x86);
            }

            string[] Directory = Common.GetDirectoryArray((string)llb_JAVA_Upload.Tag, true);
            string[] Files = Common.GetFileArray((string)llb_JAVA_Upload.Tag, true);
            if (!RAW.WriteInstallData(Directory, Files))
            {
                btn_JAVA_Upload.Enabled = true;
                Common.Message("런타임팩 설치데이터 작성 중 문제가 발생하였습니다.");
                return;
            }

            FTPUtil FTP_Data = new FTPUtil(FTPUtil.OfficialServer.Bell_Soft_Network_Cloud); // 클라우드 FTP 객체 생성
            string FTP_Default_Cloud = Servers.Bell_Soft_Network.FTP_PATH_CLOUD_ROOT + "Runtime/JAVA/"; // 클라우드 FTP 서버접속시 기본경로
            string FTP_Default_Info = Servers.Bell_Soft_Network.FTP_PATH_INFO_ROOT + "BSP/Runtime/JAVA/";
            string LocalRoot = (string)llb_JAVA_Upload.Tag;

            pb_JAVA_Upload.Value = 0;
            pb_JAVA_Upload.Maximum = Directory.Length + Files.Length;
            if (x64)
            {
                FTP_Default_Cloud += "x64/";
            }
            else
            {
                FTP_Default_Cloud += "x86/";
            }

            // 디렉토리 생성
            FTP_Data.MakeDir(FTP_Default_Cloud);
            foreach (string tmp in Directory)
            {
                FTP_Data.MakeDir(FTP_Default_Cloud + tmp.Replace("\\", "/"));
                pb_JAVA_Upload.PerformStep();
                Application.DoEvents(); // 반복문 수행시 UI가 렉먹는걸 방지하기 위해 메시지 큐 처리!
            }
            
            // 파일 업로드
            foreach (string tmp in Files)
            {
                // 파일 리스트 배열에 값을 디렉토리, 파일명으로 나눠야됨.
                FileInfo FI = new FileInfo(LocalRoot + tmp);
                string FTPDir = FI.DirectoryName.Replace(LocalRoot, string.Empty).Replace(LocalRoot.Substring(0, LocalRoot.Length - 1), string.Empty).Replace('\\', '/');
                FTP_Data.Upload(FTP_Default_Cloud + FTPDir, LocalRoot + tmp);
                pb_JAVA_Upload.PerformStep();
                Application.DoEvents(); // 반복문 수행시 UI가 렉먹는걸 방지하기 위해 메시지 큐 처리!
            }

            // xml 업로드
            FTPUtil FTP_Info = new FTPUtil(FTPUtil.OfficialServer.Bell_Soft_Network_Info); // FTP 객체 생성
            FTP_Info.Upload(FTP_Default_Info, RAW.xmlFilePath + RAW.xmlFileName, true); // 모드팩 데이터 업로드

            btn_JAVA_Upload.Enabled = true;
            Common.Message("런타임팩 업로드가 완료되었습니다!");
        }

        private void BSU_RuntimeManager_Load(object sender, EventArgs e)
        {
            btn_JAVA_Load_Click(sender, e);
        }
    }
}
