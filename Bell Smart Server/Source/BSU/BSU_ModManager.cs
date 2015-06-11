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

        private void btn_Mod_Set_Click(object sender, EventArgs e)
        {
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Mod, txt_MUID.Text);
            
            if (MAR.Availability())
            {
                lst_Mod_Version.Items.AddRange(MAR.GetVersion(ModAnalysisRead.PackType.Mod));
                txt_Mod_Name.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Name");
                txt_Mod_Latest.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Latest");
                txt_Mod_Recommended.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Recommended");
                txt_Mod_News.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "News");
                txt_Mod_Down.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Down");

                cb_Mod_Base.Items.AddRange(MAR.GetList(ModAnalysisRead.PackType.Base));
                txt_BUID.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Base");
                cb_Mod_Base.SelectedItem = txt_BUID.Text;
                cb_Mod_Option.Items.AddRange(MAR.GetList(ModAnalysisRead.PackType.Option));
                txt_OUID.Text = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Option");
                cb_Mod_Option.SelectedItem = txt_OUID.Text;

                string[] Ver_Base = {"Latest", "Recommended"};
                cb_Mod_Base_Ver.Items.AddRange(Ver_Base);
                cb_Mod_Base_Ver.Items.AddRange(MAR.GetVersion(ModAnalysisRead.PackType.Base));
                cb_Mod_Base_Upload.Items.AddRange(Ver_Base);
                cb_Mod_Base_Upload.Items.AddRange(MAR.GetVersion(ModAnalysisRead.PackType.Base));
                cb_Mod_Base_Upload.SelectedItem = Ver_Base[1];
                cb_Mod_Option_Ver.Items.AddRange(Ver_Base);
                cb_Mod_Option_Ver.Items.AddRange(MAR.GetVersion(ModAnalysisRead.PackType.Option));
                cb_Mod_Option_Upload.Items.AddRange(Ver_Base);
                cb_Mod_Option_Upload.Items.AddRange(MAR.GetVersion(ModAnalysisRead.PackType.Option));
                cb_Mod_Option_Upload.SelectedItem = Ver_Base[1];
                
                txt_MUID.ReadOnly = true;
                gb_Mod_Info.Enabled = false;
                gb_Mod_Setting.Enabled = true;
                gb_Mod_Upload.Enabled = true;

                btn_Base_Set_Click(sender, e);
                btn_Option_Set_Click(sender, e);
            }
            else
            {
                Common.Message("존재하지 않는 MUID 입니다.");
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
            bool stop = false;
            if (txt_Mod_Latest.Text == string.Empty) stop = true;
            if (txt_Mod_Recommended.Text == string.Empty) stop = true;
            if (txt_Mod_Name.Text == string.Empty) stop = true;
            if (txt_Mod_News.Text == string.Empty) stop = true;
            if ((string)cb_Mod_Base.SelectedItem == string.Empty) stop = true;
            if ((string)cb_Mod_Option.SelectedItem == string.Empty) stop = true;

            if (stop)
            {
                Common.Message("모든 필드에 값을 입력해 주세요.");
                return;
            }
            ModAnalysisWrite MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.ModPack, txt_MUID.Text, txt_Mod_Name.Text, txt_Mod_Latest.Text, txt_Mod_Recommended.Text, (string)cb_Mod_Base.SelectedItem, (string)cb_Mod_Option.SelectedItem, txt_Mod_News.Text, txt_Mod_Down.Text, lst_Mod_Version.Items.Cast<string>().ToArray());
            MAW.WriteXML();
            Common.Message("XML 작성 성공!");
        }

        private void BSU_ModManager_Shown(object sender, EventArgs e)
        {
            txt_MUID.Focus();
        }

        private void txt_MUID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { btn_Mod_Set_Click(sender, e); }
        }

        private void mi_Delete_Click(object sender, EventArgs e)
        {

        }

        private void btn_Mod_DelVer_Click(object sender, EventArgs e)
        {
            if (lst_Mod_Version.Items.Count > 1)
            {
                lst_Mod_Version.Items.Remove(lst_Mod_Version.SelectedItem);
            }
            else
            {
                Common.Message("최소 한개 이상의 버전이 존재해야합니다.");
            }
        }

        private void lst_Mod_Version_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_Mod_Base_Ver.Enabled = true;
            cb_Mod_Option_Ver.Enabled = true;
            btn_Mod_DelVer.Enabled = true;
            btn_Mod_SelectSave.Enabled = true;
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Mod, txt_MUID.Text);
            MAR.LoadMod((string)lst_Mod_Version.SelectedItem);
            cb_Mod_Base_Ver.SelectedItem = MAR.GetInstallInfo(ModAnalysisRead.PackType.Mod, "Base");
            cb_Mod_Option_Ver.SelectedItem = MAR.GetInstallInfo(ModAnalysisRead.PackType.Mod, "Option");
        }

        private void btn_Base_Set_Click(object sender, EventArgs e)
        {
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Base, txt_BUID.Text);

            if (MAR.Availability())
            {
                txt_Base_Latest.Text = MAR.GetInfo(ModAnalysisRead.PackType.Base, "Latest");
                txt_Base_Recommended.Text = MAR.GetInfo(ModAnalysisRead.PackType.Base, "Recommended");
                txt_Base_Down.Text = MAR.GetInfo(ModAnalysisRead.PackType.Base, "Down");
                lst_Base_Version.Items.AddRange(MAR.GetVersion(ModAnalysisRead.PackType.Base));

                gb_Base_Info.Enabled = false;
                gb_Base_Setting.Enabled = true;
                gb_Base_Upload.Enabled = true;
            }
            else
            {
                Common.Message("존재하지 않는 BUID 입니다.");
            }
        }

        private void btn_Option_Set_Click(object sender, EventArgs e)
        {
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Option, txt_OUID.Text);

            if (MAR.Availability())
            {
                txt_Option_Latest.Text = MAR.GetInfo(ModAnalysisRead.PackType.Option, "Latest");
                txt_Option_Recommended.Text = MAR.GetInfo(ModAnalysisRead.PackType.Option, "Recommended");
                txt_Option_Down.Text = MAR.GetInfo(ModAnalysisRead.PackType.Option, "Down");
                lst_Option_Version.Items.AddRange(MAR.GetVersion(ModAnalysisRead.PackType.Option));

                gb_Option_Info.Enabled = false;
                gb_Option_Setting.Enabled = true;
                gb_Option_Upload.Enabled = true;
            }
            else
            {
                Common.Message("존재하지 않는 OUID 입니다.");
            }
        }

        private void btn_Base_DelVer_Click(object sender, EventArgs e)
        {
            if (lst_Base_Version.Items.Count > 1)
            {
                lst_Base_Version.Items.Remove(lst_Base_Version.SelectedItem);
            }
            else
            {
                Common.Message("최소 한개 이상의 버전이 존재해야합니다.");
            }
        }

        private void btn_Option_DelVer_Click(object sender, EventArgs e)
        {
            if (lst_Option_Version.Items.Count > 1)
            {
                lst_Option_Version.Items.Remove(lst_Option_Version.SelectedItem);
            }
            else
            {
                Common.Message("최소 한개 이상의 버전이 존재해야합니다.");
            }
        }

        private void txt_BUID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { btn_Base_Set_Click(sender, e); }
        }

        private void txt_OUID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { btn_Option_Set_Click(sender, e); }
        }

        private void btn_Mod_SelectSave_Click(object sender, EventArgs e)
        {
            ModAnalysisWrite MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.ModPack, txt_MUID.Text);
            string[] Dir = {"1", "2", "3"};
            string[] Hash = {"경로|해시", "경로2|해시2"};
            MAW.WriteVersionXML((string)lst_Mod_Version.SelectedItem, (string)cb_Mod_Base.SelectedItem, (string)cb_Mod_Option.SelectedItem, Dir, Hash);
        }

        private void btn_Base_Save_Click(object sender, EventArgs e)
        {
            bool stop = false;
            if (txt_Base_Down.Text == string.Empty) stop = true;
            if (txt_Base_Latest.Text == string.Empty) stop = true;
            if (txt_Base_Recommended.Text == string.Empty) stop = true;

            if (stop)
            {
                Common.Message("모든 필드에 값을 입력해 주세요.");
                return;
            }
            ModAnalysisWrite MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.BasePack, txt_BUID.Text, txt_Base_Latest.Text, txt_Base_Recommended.Text, txt_Base_Down.Text, lst_Base_Version.Items.Cast<string>().ToArray());
            MAW.WriteXML();
            Common.Message("XML 작성 성공!");
        }

        private void btn_Option_Save_Click(object sender, EventArgs e)
        {
            bool stop = false;
            if (txt_Option_Down.Text == string.Empty) stop = true;
            if (txt_Option_Latest.Text == string.Empty) stop = true;
            if (txt_Option_Recommended.Text == string.Empty) stop = true;

            if (stop)
            {
                Common.Message("모든 필드에 값을 입력해 주세요.");
                return;
            }
            ModAnalysisWrite MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.OptionPack, txt_OUID.Text, txt_Option_Latest.Text, txt_Option_Recommended.Text, txt_Option_Down.Text, lst_Option_Version.Items.Cast<string>().ToArray());
            MAW.WriteXML();
            Common.Message("XML 작성 성공!");
        }

        private void btn_Mod_DelFile_Click(object sender, EventArgs e)
        {
            lst_Mod_File.Items.Remove(lst_Mod_File.SelectedItem);
        }

        private void btn_Mod_Init_Click(object sender, EventArgs e)
        {
            lst_Mod_File.Items.Clear();
        }

        private void btn_Base_Init_Click(object sender, EventArgs e)
        {
            lst_Base_File.Items.Clear();
        }

        private void btn_Base_DelFile_Click(object sender, EventArgs e)
        {
            lst_Base_File.Items.Remove(lst_Base_File.SelectedItem);
        }

        private void btn_Option_Init_Click(object sender, EventArgs e)
        {
            lst_Option_File.Items.Clear();
        }

        private void btn_Option_DelFile_Click(object sender, EventArgs e)
        {
            //lst_Option_File.Items.Remove(lst_Option_File.SelectedItems);
        }
    }
}
