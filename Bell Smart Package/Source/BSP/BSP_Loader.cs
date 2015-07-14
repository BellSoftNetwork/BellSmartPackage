using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bell_Smart_Package.Source.BSP;
using Bell_Smart_Server.Source.BSS;
using Bell_Smart_Tools.Source.BST;
using BellLib.Class;


namespace Bell_Smart_Package.Source.BSP
{
    public partial class BSP_Loader : Form
    {
        public BSP_Loader()
        {
            InitializeComponent();
            lb_Log.Parent = pb_Logo; // 레이블 투명색상을 위해 부모를 설정
        }
        /// <summary>
        /// BSP 로딩 상태를 String으로 유저에게 나타내줍니다.
        /// </summary>
        /// <param name="Text"></param>
        private void Log(string Text)
        {
            lb_Log.Text = Text; // "진행상황 : " + Text;
        }
        private void BSP_Loader_Shown(object sender, EventArgs e)
        {
            Log("Initialize");
            if (Initialize())
            {
                Log("Initialize complete.");
                Application.DoEvents();
                BSP_Management BSPM = new BSP_Management();
                BSP_Login BSP = new BSP_Login();
                Log("Instance generation complete.");
                
                BSPM.Show();
                BSP.Show();
                Log("Instance show complete.");

                this.Hide();
                Log("Hide BSP_Loader");
            } else {
                Log("Initialize fail.");
            }
        }

        private bool Initialize()
        {
            Application.DoEvents();
            Common.CreateDefaultForder();
            var actions = new Dictionary<Action, int> 
            {
                {InitDebug, 1},
                {InitParameter, 1},
            };

            pb_Load.Minimum = 0;
            pb_Load.Maximum = actions.Select(kvp => kvp.Value).Sum();
            pb_Load.Value = 0;
            foreach (var action in actions)
            {
                action.Key();
                pb_Load.Value += action.Value;
                Application.DoEvents();
            }
            InitDebug();
            InitParameter();
            if (Deployment.UpdateAvailable()) // 최신버전 발견시
            {
                BSP_Updater BSPU = new BSP_Updater();
                BSPU.Show(); // 업데이트 실행
                this.Hide();

                return false; // 프로그램 실행 중단.
            }

            return true;
        }

        private void InitDebug()
        {
            Log("Debug Initilaize");
            Debug dbg = new Debug();
            dbg.Initialize();
            Log("Debug Initialize complete.");
        }

        private void InitParameter()
        {
            try
            {
                // 되는지 안되는지는 모르는 소스.
                Log("Program parameter loading");
                string[] tmp = Environment.GetCommandLineArgs(); // 프로그램 파라미터 로드
                /*string Temp = string.Empty;
                foreach (string tp in tmp)
                    Temp += tp + Environment.NewLine;
                Common.Message(Temp);*/
                string[] Parameter = tmp[0].Split('='); // 파라미터 파싱
                if (Parameter[0] == "Debug") // 파라미터가 디버그일경우
                {
                    switch (Parameter[1]) // 파라미터 분석
                    {
                        case "0":
                            Debug.DebuggerMode = Debug.Level.Log; // 디버그 모드 설정
                            break;

                        case "1":
                            Debug.DebuggerMode = Debug.Level.Low;
                            break;

                        case "2":
                            Debug.DebuggerMode = Debug.Level.Middle;
                            break;

                        case "3":
                            Debug.DebuggerMode = Debug.Level.High;
                            break;
                    }
                    Log("Program parameter debug mode active.");
                }
            }
            catch
            {
                Log("Program parameter analysis fail.");
            }
        }
        private void mi_End_Click(object sender, EventArgs e)
        {
            Common.End();
        }

        private void mi_Restart_Click(object sender, EventArgs e)
        {
            Common.End(true);
        }
    }
}
