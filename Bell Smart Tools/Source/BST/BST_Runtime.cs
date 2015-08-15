using BellLib.Class;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Tools.Source.BSS
{
    public partial class BST_Runtime : Form
    {
        private bool InstallAvailable = false;
        private RunType SelectType;
        private string CloudURL;
        private string CloudURLTag;
        private string LocalPath;
        private string LocalPathTag;
        private RuntimeAnalysis.JAVAType SelectJAVA;

        public enum RunType
        {
            JAVA
        }
        
        public BST_Runtime(RunType RuntimeType, FormStartPosition Position = FormStartPosition.CenterScreen)
        {
            this.StartPosition = Position;
            InitializeComponent();
            pb_Down.Height = 0;

            this.SelectType = RuntimeType;
            this.LocalPath = User.BSN_Path + "Runtime\\";
            switch (RuntimeType)
            {
                case RunType.JAVA:
                    lb_Name.Text = "Installation of Java runtime";
                    this.CloudURL = Servers.Bell_Soft_Network.WEB_CLOUD_ROOT + "Runtime/JAVA/";
                    this.LocalPath += "JAVA\\";
                    break;
            }
        }

        public void SetJAVA(RuntimeAnalysis.JAVAType Type)
        {
            this.SelectJAVA = Type;
            if (Type == RuntimeAnalysis.JAVAType.x64)
            {
                CloudURLTag = "x64/";
                LocalPathTag = "x64\\";
            }
            else
            {
                CloudURLTag = "x86/";
                LocalPathTag = "x64\\";
            }

            InstallAvailable = true;
        }

        private void BST_Runtime_Shown(object sender, EventArgs e)
        {
            Install();
        }

        private void Initialize()
        {
            SetProgress(30, 5);
            SetProgress(20, 10);
            SetProgress(5, 15);
            SetProgress(5, 15, false);
        }

        private void SetProgress(int Count, int Speed, bool Show = true)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Show)
                    pb_Down.Height += 1;
                else
                    pb_Down.Height -= 1;

                Application.DoEvents();
                Common.Delay(Speed);
            }
        }

        private void Install()
        {
            if (!InstallAvailable)
            {
                throw new System.MethodAccessException("설치 데이터값이 정상적으로 초기화되지 않았습니다.");
            }
            else
            {
                Initialize();
                RuntimeAnalysisRead RAR = new RuntimeAnalysisRead(RuntimeAnalysis.RunType.JAVA);
                RAR.SetJava(this.SelectJAVA);
                RAR.LoadInstallData();
                RAR.CreateDirectory(LocalPath + LocalPathTag);
                string[] Files = RAR.GetInstallData(RuntimeAnalysisRead.DataType.File);
                pb_Down.Maximum = Files.Length;

                foreach (string tmp in Files)
                {
                        WebClient WC = new WebClient();
                        try
                        {
                            WC.DownloadFile(CloudURL + CloudURLTag + tmp, LocalPath + LocalPathTag + tmp); // 파일 다운로드
                        }
                        catch
                        {
                            Common.Message("다운로드에 실패하였습니다." + Environment.NewLine + "File=" + tmp);
                        }
                    pb_Down.PerformStep(); // 진행
                    Application.DoEvents();
                }

                string[] Data = { "Runtime|" + SelectType.ToString() };
                Protection.WriteBDXFile(LocalPath + LocalPathTag + "data.bdx", Data); // 모드팩 버전 데이터 저장

                lb_Name.Text = "Installation complete.";
                Application.DoEvents();
                Common.Delay(3000);
            }

            this.Close();
        }
    }
}
