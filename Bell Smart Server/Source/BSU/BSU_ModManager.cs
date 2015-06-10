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
            ModAnalysisRead MAR = new ModAnalysisRead(txt_MUID.Text);
            
            if (MAR.Availability())
            {
                lst_Mod_Version.Items.AddRange(MAR.GetVersion(ModAnalysisRead.PackType.Mod));
                txt_Mod_Name.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Name");
                txt_Mod_Latest.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Latest");
                txt_Mod_Recommended.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Recommended");
                cb_Mod_Base.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Base");
                cb_Mod_Option.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Option");
                txt_Mod_News.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "News");
                txt_Mod_Down.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Down");
                
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
            string[] Version = { "8.2.0", "8.0.0" };
            //Version = lst_Mod_Version. //lst_Mod_Version 아이템 전부 아래 생성자에 직접 대입
            ModAnalysisWrite MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.ModPack, txt_MUID.Text, txt_Mod_Name.Text, txt_Mod_Recommended.Text, txt_Mod_Latest.Text, cb_Mod_Base.SelectedText, cb_Mod_Option.SelectedText, txt_Mod_News.Text, txt_Mod_Down.Text, Version);
            MAW.WriteXML();
        }
    }
}
