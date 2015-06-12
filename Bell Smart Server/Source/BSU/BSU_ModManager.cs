using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BellLib.Class;
using BellLib.Data;
using System.Diagnostics;

namespace Bell_Smart_Server.Source.BSU
{
    public partial class BSU_ModManager : Form
    {
        public BSU_ModManager()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            llb_Mod_Upload.Tag = User.BSN_Path + "Upload\\ModPack\\";
            llb_Mod_Upload.Text = "업로드 폴더 : " + (string)llb_Mod_Upload.Tag;
            llb_Base_Upload.Tag = User.BSN_Path + "Upload\\BasePack\\";
            llb_Base_Upload.Text = "업로드 폴더 : " + (string)llb_Base_Upload.Tag;
            llb_Option_Upload.Tag = User.BSN_Path + "Upload\\OptionPack\\";
            llb_Option_Upload.Text = "업로드 폴더 : " + (string)llb_Option_Upload.Tag;
        }
        private bool InitializeMod()
        {
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Mod, txt_MUID.Text);

            if (MAR.Availability())
            {
                lst_Mod_Version.Items.Clear();
                cb_Mod_Base.Items.Clear();
                cb_Mod_Option.Items.Clear();
                cb_Mod_Base_Ver.Items.Clear();
                cb_Mod_Option_Ver.Items.Clear();
                cb_Mod_Base_Upload.Items.Clear();
                cb_Mod_Option_Upload.Items.Clear();

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

                string[] Ver_Base = { "Latest", "Recommended" };
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

                return true;
            }
            else
            {
                return false;
            }
        }
        private void btn_Mod_Set_Click(object sender, EventArgs e)
        {
            if (InitializeMod())
            {
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
            //lst_Mod_File.Items.AddRange(files);
            foreach (string tmp in files)
                lst_Mod_File.Items.AddRange(Directory.GetFiles(tmp));
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
            if (lst_Mod_Version.SelectedItem != null)
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
        }

        private bool InitializeBase()
        {
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Base, txt_BUID.Text);

            if (MAR.Availability())
            {
                lst_Base_Version.Items.Clear();

                txt_Base_Latest.Text = MAR.GetInfo(ModAnalysisRead.PackType.Base, "Latest");
                txt_Base_Recommended.Text = MAR.GetInfo(ModAnalysisRead.PackType.Base, "Recommended");
                txt_Base_Down.Text = MAR.GetInfo(ModAnalysisRead.PackType.Base, "Down");
                lst_Base_Version.Items.AddRange(MAR.GetVersion(ModAnalysisRead.PackType.Base));

                return true;
            }
            else
            {
                return false;
            }
        }
        private void btn_Base_Set_Click(object sender, EventArgs e)
        {
            if (InitializeBase())
            {
                gb_Base_Info.Enabled = false;
                gb_Base_Setting.Enabled = true;
                gb_Base_Upload.Enabled = true;
            }
            else
            {
                Common.Message("존재하지 않는 BUID 입니다.");
            }
        }

        private bool InitializeOption()
        {
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Option, txt_OUID.Text);

            if (MAR.Availability())
            {
                lst_Option_Version.Items.Clear();

                txt_Option_Latest.Text = MAR.GetInfo(ModAnalysisRead.PackType.Option, "Latest");
                txt_Option_Recommended.Text = MAR.GetInfo(ModAnalysisRead.PackType.Option, "Recommended");
                txt_Option_Down.Text = MAR.GetInfo(ModAnalysisRead.PackType.Option, "Down");
                lst_Option_Version.Items.AddRange(MAR.GetVersion(ModAnalysisRead.PackType.Option));

                return true;
            }
            else
            {
                return false;
            }
        }
        private void btn_Option_Set_Click(object sender, EventArgs e)
        {
            if (InitializeOption())
            {
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
            bool stop = false;
            if ((string)lst_Mod_Version.SelectedItem == string.Empty) stop = true;
            if ((string)cb_Mod_Base.SelectedItem == string.Empty) stop = true;
            if ((string)cb_Mod_Option.SelectedItem == string.Empty) stop = true;
            if (stop)
            {
                Common.Message("누락된 정보가 있습니다." + Environment.NewLine + "빠진 값이 없는지 확인해주세요.");
                return;
            }
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Mod, txt_MUID.Text);
            ModAnalysisWrite MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.ModPack, txt_MUID.Text);
            MAR.LoadMod((string)lst_Mod_Version.SelectedItem);
            MAW.WriteVersionXML((string)lst_Mod_Version.SelectedItem, (string)cb_Mod_Base_Ver.SelectedItem, (string)cb_Mod_Option_Ver.SelectedItem, MAR.GetInstallData(ModAnalysisRead.PackType.Mod, "Directory"), MAR.GetInstallData(ModAnalysisRead.PackType.Mod, "Hash"));
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

        private void btn_Mod_Upload_Click(object sender, EventArgs e)
        {
            bool stop = false;
            if (txt_Mod_Version.Text == string.Empty) stop = true;
            if ((string)cb_Mod_Base_Upload.SelectedItem == string.Empty) stop = true;
            if ((string)cb_Mod_Option_Upload.SelectedItem == string.Empty) stop = true;
            if (stop)
            {
                Common.Message("모든 필드에 값을 입력해 주세요.");
                return;
            }
            List<string> list = new List<string>();
            Protection Pro = new Protection();
            string[] Array = lst_Mod_File.Items.Cast<string>().ToArray();
            string SetVer = txt_Mod_Version.Text;

            // 모드팩 버전.xml 생성
            foreach (string tmp in Array)
            {
                string Path = (string)llb_Mod_Upload.Tag + tmp;
                list.Add(tmp + "|" + Pro.MD5Hash(Path));
            }
            ModAnalysisWrite MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.ModPack, txt_MUID.Text);
            MAW.WriteInstallXML(SetVer, (string)cb_Mod_Base_Upload.SelectedItem, (string)cb_Mod_Option_Upload.SelectedItem, Common.GetDirectoryArray((string)llb_Mod_Upload.Tag, true), list.ToArray());
            
            // 모드팩.XML 생성
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Mod, txt_MUID.Text);
            if (MAR.Availability())
            {
                string Name, Latest, Recommended, Base, Option, News, Down;
                List<string> Version = new List<string>();
                Name = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Name");
                Latest = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Latest");
                Recommended = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Recommended");
                News = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "News");
                Down = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Down");
                Base = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Base");
                Option = MAR.GetInfo(ModAnalysisRead.PackType.Mod, "Option");

                foreach (string tmp in MAR.GetVersion(ModAnalysisRead.PackType.Mod))
                {
                    Version.Add(tmp);
                    Version.Sort();
                }
                Version.Add(SetVer);
                Version.Sort();

                if (cb_Mod_Latest.Checked)
                    Latest = SetVer;
                if (cb_Mod_Recommended.Checked)
                    Recommended = SetVer;
                MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.ModPack, txt_MUID.Text, Name, Latest, Recommended, Base, Option, News, Down, Version.ToArray());
                MAW.WriteXML();
            }
            else
            {
                Common.Message(txt_MUID.Text + ".xml 파일 작성을 시도하던 중 문제가 발생하였습니다." + Environment.NewLine + "MAR.Availability() = false");
                return;
            }
            
            // 파일 업로드
            FTPUtil FTPU = new FTPUtil(BellLib.Data.Base.SERVER_IP, "Bell", "bellpackage", "21");
            FTPU.MakeDir("Pack/BellCraft8/8.8.0/mods");
            //FTPU.DeleteFTP("Pack/BellCraft8/8.8.0/mods/b72_1710.jar");
            FTPU.Upload("Pack/BellCraft8/8.8.0/mods", (string)llb_Mod_Upload.Tag + "mods\\b72_1710.jar");

            InitializeMod();
            Common.Message("모드팩이 정상적으로 업로드 되었습니다!");
        }

        private void btn_Base_Upload_Click(object sender, EventArgs e)
        {
            bool stop = false;
            if (txt_Base_Version.Text == string.Empty) stop = true;
            if (stop)
            {
                Common.Message("모든 필드에 값을 입력해 주세요.");
                return;
            }
            List<string> list = new List<string>();
            Protection Pro = new Protection();
            string[] Array = lst_Base_File.Items.Cast<string>().ToArray();
            string SetVer = txt_Base_Version.Text;

            // 베이스팩 버전.xml 생성
            foreach (string tmp in Array)
            {
                string Path = (string)llb_Base_Upload.Tag + tmp;
                list.Add(tmp + "|" + Pro.MD5Hash(Path));
            }
            ModAnalysisWrite MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.BasePack, txt_BUID.Text);
            MAW.WriteInstallXML(txt_Base_Version.Text, Common.GetDirectoryArray((string)llb_Base_Upload.Tag, true), list.ToArray());
            
            // 베이스팩.XML 생성
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Base, txt_BUID.Text);
            if (MAR.Availability())
            {
                string Latest, Recommended, Down;
                List<string> Version = new List<string>();
                Latest = MAR.GetInfo(ModAnalysisRead.PackType.Base, "Latest");
                Recommended = MAR.GetInfo(ModAnalysisRead.PackType.Base, "Recommended");
                Down = MAR.GetInfo(ModAnalysisRead.PackType.Base, "Down");

                foreach (string tmp in MAR.GetVersion(ModAnalysisRead.PackType.Base))
                {
                    Version.Add(tmp);
                    Version.Sort();
                }
                Version.Add(SetVer);
                Version.Sort();

                if (cb_Base_Latest.Checked)
                    Latest = SetVer;
                if (cb_Base_Recommended.Checked)
                    Recommended = SetVer;
                MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.BasePack, txt_BUID.Text, Latest, Recommended, Down, Version.ToArray());
                MAW.WriteXML();
            }
            else
            {
                Common.Message(txt_BUID.Text + ".xml 파일 작성을 시도하던 중 문제가 발생하였습니다." + Environment.NewLine + "MAR.Availability() = false");
                return;
            }
            // 파일 업로드
            InitializeBase();
            Common.Message("베이스팩이 정상적으로 업로드 되었습니다!");
        }

        private void btn_Option_Upload_Click(object sender, EventArgs e)
        {
            bool stop = false;
            if (txt_Option_Version.Text == string.Empty) stop = true;
            if (stop)
            {
                Common.Message("모든 필드에 값을 입력해 주세요.");
                return;
            }
            List<string> Option = new List<string>();
            List<string> Hash = new List<string>();
            Protection Pro = new Protection();
            string SetVer = txt_Option_Version.Text;

            // 옵션팩 버전.xml 생성
            foreach (ListViewItem item in lst_Option_File.Items)
            {
                if (item.SubItems[0].Text == string.Empty || item.SubItems[1].Text == string.Empty || item.SubItems[2].Text == string.Empty || item.SubItems[3].Text == string.Empty)
                {
                    Common.Message("옵션파일 상세리스트 모든 필드에 값을 입력해 주세요.");
                    return;
                }
                Option.Add(item.SubItems[0].Text + "|" + item.SubItems[1].Text + "|" + item.SubItems[2].Text);
                Hash.Add(item.SubItems[0].Text + "|" + item.SubItems[3].Text + "|" + Pro.MD5Hash(llb_Option_Upload.Tag + item.SubItems[3].Text));
            }
            ModAnalysisWrite MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.OptionPack, txt_OUID.Text);
            MAW.WriteInstallXML(txt_Option_Version.Text, Option.ToArray(), Common.GetDirectoryArray((string)llb_Option_Upload.Tag, true), Hash.ToArray());
            
            // 옵션팩.XML 생성
            ModAnalysisRead MAR = new ModAnalysisRead(ModAnalysisRead.PackType.Option, txt_OUID.Text);
            if (MAR.Availability())
            {
                string Latest, Recommended, Down;
                List<string> Version = new List<string>();
                Latest = MAR.GetInfo(ModAnalysisRead.PackType.Option, "Latest");
                Recommended = MAR.GetInfo(ModAnalysisRead.PackType.Option, "Recommended");
                Down = MAR.GetInfo(ModAnalysisRead.PackType.Option, "Down");

                foreach (string tmp in MAR.GetVersion(ModAnalysisRead.PackType.Option))
                {
                    Version.Add(tmp);
                    Version.Sort();
                }
                Version.Add(SetVer);
                Version.Sort();

                if (cb_Option_Latest.Checked)
                    Latest = SetVer;
                if (cb_Option_Recommended.Checked)
                    Recommended = SetVer;
                MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.OptionPack, txt_OUID.Text, Latest, Recommended, Down, Version.ToArray());
                MAW.WriteXML();
            }
            else
            {
                Common.Message(txt_OUID.Text + ".xml 파일 작성을 시도하던 중 문제가 발생하였습니다." + Environment.NewLine + "MAR.Availability() = false");
                return;
            }
            // 파일 업로드
            InitializeOption();
            Common.Message("옵션팩이 정상적으로 업로드 되었습니다!");
        }

        private void llb_Mod_Upload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start((string)llb_Mod_Upload.Tag);
            }
            catch (Exception ex)
            {
                Common.Message("해당 폴더를 여는 중, 문제가 발생하였습니다." + Environment.NewLine + ex.Message);
            }
        }

        private void llb_Base_Upload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start((string)llb_Base_Upload.Tag);
            }
            catch (Exception ex)
            {
                Common.Message("해당 폴더를 여는 중, 문제가 발생하였습니다." + Environment.NewLine + ex.Message);
            }
        }

        private void llb_Option_Upload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start((string)llb_Option_Upload.Tag);
            }
            catch (Exception ex)
            {
                Common.Message("해당 폴더를 여는 중, 문제가 발생하였습니다." + Environment.NewLine + ex.Message);
            }
        }

        private void btn_Mod_Load_Click(object sender, EventArgs e)
        {
            lst_Mod_File.Items.Clear();
            lst_Mod_File.Items.AddRange(Common.GetFileArray((string)llb_Mod_Upload.Tag, true));
        }

        private void btn_Base_Load_Click(object sender, EventArgs e)
        {
            lst_Base_File.Items.Clear();
            lst_Base_File.Items.AddRange(Common.GetFileArray((string)llb_Base_Upload.Tag, true));
        }

        private void btn_Option_Load_Click(object sender, EventArgs e)
        {
            txt_Option_Name.Text = string.Empty;
            txt_Option_Name.Enabled = false;
            txt_Option_UID.Text = string.Empty;
            txt_Option_UID.Enabled = false;
            cb_Option_Default.Enabled = false;
            lst_Option_File.Items.Clear();
            lst_Option_File.BeginUpdate();
            ListViewItem lvi;
            foreach (string tmp in Common.GetFileArray((string)llb_Option_Upload.Tag, true))
            {
                lvi = new ListViewItem(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add("false");
                lvi.SubItems.Add(tmp);
                lst_Option_File.Items.Add(lvi);
            }
            lst_Option_File.EndUpdate();
        }

        private void lst_Option_File_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection col = lst_Option_File.SelectedItems;

            foreach (ListViewItem item in col)
            {
                txt_Option_Name.Text = item.SubItems[0].Text;
                txt_Option_UID.Text = item.SubItems[1].Text;
                cb_Option_Default.SelectedItem = item.SubItems[2].Text;
            }

            if (lst_Option_File.SelectedItems == null)
            {
                txt_Option_Name.Enabled = false;
                txt_Option_UID.Enabled = false;
                cb_Option_Default.Enabled = false;
                btn_Option_Apply.Enabled = false;
            }
            else
            {
                txt_Option_Name.Enabled = true;
                txt_Option_UID.Enabled = true;
                cb_Option_Default.Enabled = true;
                btn_Option_Apply.Enabled = true;
            }
        }

        private void txt_Option_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            ListView.SelectedListViewItemCollection col = lst_Option_File.SelectedItems;

            foreach (ListViewItem item in col)
                item.SubItems[0].Text = txt_Option_Name.Text;
        }

        private void txt_Option_UID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            ListView.SelectedListViewItemCollection col = lst_Option_File.SelectedItems;

            foreach (ListViewItem item in col)
                item.SubItems[1].Text = txt_Option_UID.Text;
        }

        private void cb_Option_Default_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection col = lst_Option_File.SelectedItems;

            foreach (ListViewItem item in col)
                item.SubItems[2].Text = (string)cb_Option_Default.SelectedItem;
        }

        private void btn_Option_Apply_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection col = lst_Option_File.SelectedItems;

            foreach (ListViewItem item in col)
            {
                item.SubItems[0].Text = txt_Option_Name.Text;
                item.SubItems[1].Text = txt_Option_UID.Text;
                item.SubItems[2].Text = (string)cb_Option_Default.SelectedItem;
            }
        }
    }
}
