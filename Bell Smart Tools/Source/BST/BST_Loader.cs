using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BellLib.Class;
using BellLib.Data;

namespace Bell_Smart_Tools.Source.BSS
{
    public partial class BST_Loader : Form
    {
        public BST_Loader()
        {
            InitializeComponent();
        }
        /// <summary>
        /// BST를 초기화합니다.
        /// </summary>
        /// <returns></returns>
        private bool Initialize()
        {
            // GUID 대신 사용자 임의대로 뮤텍스 이름 사용
            string mtxName = "Bell Smart Tools";
            Mutex mtx = new Mutex(true, mtxName);

            // 1초 동안 뮤텍스를 획득하려 대기
            TimeSpan tsWait = new TimeSpan(0, 0, 1);
            bool success = mtx.WaitOne(tsWait);

            // 실패하면 프로그램 종료
            if (!success)
            {
                WinCom.Message("BST가 이미 실행중입니다." + Environment.NewLine + "BST는 중복실행이 불가능 합니다.", Base.PROJECT.Bell_Smart_Tools);
                WinCom.End();
                return false;
            }

            var actions = new Dictionary<Action, int> 
            {
                {BlankMetod, 1},
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
            pb_Load.Value = pb_Load.Maximum;
            return true;
        }

        private void BlankMetod()
        {

        }

        private void BST_Loader_Shown(object sender, EventArgs e)
        {
            if (Initialize())
            {
                BST_Login BST = new BST_Login(); //new BST_Login();
                BST.Show();
                this.Hide();
            }
        }
    }
}
