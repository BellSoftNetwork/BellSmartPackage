using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BellLib.Class;

namespace Bell_Smart_Server.Source.BSU
{
    public partial class BSU_ModManager : Form
    {
        public BSU_ModManager()
        {
            InitializeComponent();
        }

        private void btn_Set_Click(object sender, EventArgs e)
        {
            if ("로드성공" == "로드성공")
            {
                txt_MUID.ReadOnly = true;
                gb_Mod_Info.Enabled = false;
                gb_Mod_Setting.Enabled = true;
                gb_Mod_Upload.Enabled = true;
            }
        }

        private void lst_Mod_File_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            lst_Mod_File.Items.AddRange(files);
        }

        private void lst_Mod_File_DragOver(object sender, DragEventArgs e)
        {
            //드레그하는 개체가 파일이면
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //마우스 커서를 Copy모양으로 바꿔준다.
                e.Effect = DragDropEffects.Copy;
            } else {
                //아닐경우 커서의 모양을 θ 요런 모양으로 바꾼다.
                e.Effect = DragDropEffects.None;
            }
        }

        private void mi_Exclusion_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_Mod_Save_Click(object sender, EventArgs e)
        {
            ModAnalysisServer MAS = new ModAnalysisServer(ModAnalysisServer.Type.ModPack, "BellCraft8");
            string[] Version = null;
            Version[0] = "8.0.0";
            Version[1] = "8.1.0";
            MAS.WriteXML(txt_MUID.Text, txt_Mod_Name.Text, txt_Mod_Recommended.Text, txt_Mod_Latest.Text, cb_Mod_Base.SelectedText, cb_Mod_Option.SelectedText, txt_Mod_News.Text, txt_Mod_Down.Text, Version);
        }
    }
}
