using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bell_Smart_Package.Source.BSP;
using BellLib.Class;


namespace Bell_Smart_Package.Source.BSP
{
    public partial class BSP_Loader : Form
    {
        public BSP_Loader()
        {
            InitializeComponent();
        }

        private void BSP_Loader_Shown(object sender, EventArgs e)
        {
            Initialize(); // 프로그램 초기화

            BSP_Login BSP = new BSP_Login();
            BSP.Show();

            this.Hide();

        }

        private void Initialize()
        {
            Debug dbg = new Debug();
            dbg.Initialize();

            try {
                // 되는지 안되는지는 모르는 소스.
                string[] tmp = Environment.GetCommandLineArgs(); // 프로그램 파라미터 로드
                string[] Parameter = tmp[0].Split('='); // 파라미터 파싱
                if (Parameter[0] == "Debug") // 파라미터가 디버그일경우
                {
                    switch (Parameter[1]) // 파라미터 분석
                    {
                        case "0":
                            Debug.Debugger = Debug.Level.Log; // 디버그 모드 설정
                            break;

                        case "1":
                            Debug.Debugger = Debug.Level.Low;
                            break;

                        case "2":
                            Debug.Debugger = Debug.Level.Middle;
                            break;

                        case "3":
                            Debug.Debugger = Debug.Level.High;
                            break;
                    }    
                }
            } catch {

            }
        }
    }
}
