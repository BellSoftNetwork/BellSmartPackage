using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BellLib.Class;
using BellLib.Data;
using System.IO;
using Debug = BellLib.Class.Debug;

namespace Bell_Smart_Tools.Source.BSS
{
    public partial class BST_Debug : Form
    {
        public BST_Debug()
        {
            InitializeComponent();
        }

        private void btn_OpenLog_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Debug.GetLogFileName());
            }
            catch (Exception ex)
            {
                Debug.Message(Debug.Level.Log, "OpenLog_Click" + Environment.NewLine + ex.Message);
                WinCom.Message("로그파일 실행 중 에러가 발생하였습니다." + Environment.NewLine +
                    User.BSN_Path + @"\logs 폴더를 확인하세요." + Environment.NewLine + Environment.NewLine +
                    ex.Message, Basic.PROJECT.Bell_Smart_Tools);
            }
        }

        private void btn_DeleteDL_Click(object sender, EventArgs e)
        {
            if (WinCom.Message("정말로 모든 디버그 로그를 삭제하시겠습니까?", Basic.PROJECT.Bell_Smart_Tools, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return; // Yes가 아니면 반환.

            string sDirPath = Path.Combine(User.BSN_Path, "logs");
            DirectoryInfo dir = new DirectoryInfo(sDirPath);
            FileInfo[] files = dir.GetFiles("debug*.log", SearchOption.AllDirectories);

            if (files.Length <= 0)
            {
                WinCom.Message("삭제 할 디버그 로그가 없습니다.", Basic.PROJECT.Bell_Smart_Tools);
                return;
            }

            foreach (FileInfo file in files)
            {
                // 만약 ReadOnly 속성이 있는 파일이 있다면 지울때 에러가 나므로 속성을 Normal로 바꿔놓는다.
                if (file.Attributes == FileAttributes.ReadOnly)
                    file.Attributes = FileAttributes.Normal;
                file.Delete();
            }
        }
    }
}
