using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Library.Class;

namespace Bell_Smart_Tools.Source.BST
{
    public partial class BST_Loader : Form
    {
        public BST_Loader()
        {
            InitializeComponent();
        }

        private void BST_Loader_Shown(object sender, EventArgs e)
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
                Common.Message("BST가 이미 실행중입니다." + Environment.NewLine + "BST는 중복실행이 불가능 합니다.");
                Common.End();
                return;
            }
            pb_Load.Value = pb_Load.Maximum;
            BST_Main BST = new BST_Main(); //new BST_Login();
            BST.Show();
            //this.Hide();
            this.Close();
        }
    }
}
