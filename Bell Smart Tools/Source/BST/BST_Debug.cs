﻿using System;
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

namespace Bell_Smart_Tools.Source.BST
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
                Process.Start(BellLib.Class.Debug.LogFile);
            }
            catch (Exception ex)
            {
                BellLib.Class.Debug.Message(BellLib.Class.Debug.Level.Log, "OpenLog_Click" + Environment.NewLine + ex.Message);
                Common.Message("로그파일 실행 중 에러가 발생하였습니다." + Environment.NewLine + User.BSN_Path + "\\logs 폴더를 확인하세요." + Environment.NewLine + Environment.NewLine + ex.Message);
            }
        }

        private void btn_DeleteDL_Click(object sender, EventArgs e)
        {
            if (Common.Message("정말로 모든 디버그 로그를 삭제하시겠습니까?", "[Bell Smart Debug Tools]", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string sDirPath = User.BSN_Path + "logs";
                DirectoryInfo dir = new DirectoryInfo(sDirPath);
                System.IO.FileInfo[] files;

                files = dir.GetFiles("debug*.log", SearchOption.AllDirectories);
                if (files.Length <= 0)
                {
                    Common.Message("삭제할 디버그 로그가 없습니다.");
                    return;
                }

                foreach (System.IO.FileInfo file in files)
                {
                    // 만약 ReadOnly 속성이 있는 파일이 있다면 지울때 에러가 나므로 속성을 Normal로 바꿔놓는다.
                    if (file.Attributes == FileAttributes.ReadOnly)
                        file.Attributes = FileAttributes.Normal;
                    file.Delete();
                }
            }
        }
    }
}
