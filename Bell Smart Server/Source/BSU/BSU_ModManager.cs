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

namespace Bell_Smart_Tools.Source.BSU
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

            PackAnalysisRead MAR = new PackAnalysisRead();
            cb_MUID.Items.AddRange(PackAnalysisRead.LoadPackList(PackAnalysisRead.PackType.Mod));
            cb_MUID.SelectedIndex = 0;

            cb_BUID.Items.AddRange(PackAnalysisRead.LoadPackList(PackAnalysisRead.PackType.Base));
            cb_BUID.SelectedIndex = 0;

            cb_OUID.Items.AddRange(PackAnalysisRead.LoadPackList(PackAnalysisRead.PackType.Option));
            cb_OUID.SelectedIndex = 0;
        }
        private bool InitializeMod()
        {
            PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Mod, (string)cb_MUID.SelectedItem);

            if (MAR.Availability())
            {
                lst_Mod_Version.Items.Clear();
                cb_Mod_Base.Items.Clear();
                cb_Mod_Option.Items.Clear();
                cb_Mod_Base_Ver.Items.Clear();
                cb_Mod_Option_Ver.Items.Clear();
                cb_Mod_Base_Upload.Items.Clear();
                cb_Mod_Option_Upload.Items.Clear();
                lst_Mod_Servers.Items.Clear();
                lst_Mod_Access.Items.Clear();

                lst_Mod_Version.Items.AddRange(MAR.GetVersion(PackAnalysisRead.PackType.Mod));
                txt_Mod_Name.Text = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Name");
                txt_Mod_Latest.Text = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Latest");
                txt_Mod_Recommended.Text = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Recommended");
                
                cb_Mod_Base.Items.AddRange(PackAnalysisRead.LoadPackList(PackAnalysisRead.PackType.Base));
                cb_BUID.SelectedItem = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Base");
                cb_Mod_Base.SelectedItem = (string)cb_BUID.SelectedItem;
                cb_Mod_Option.Items.AddRange(PackAnalysisRead.LoadPackList(PackAnalysisRead.PackType.Option));
                cb_OUID.SelectedItem = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Option");
                cb_Mod_Option.SelectedItem = (string)cb_OUID.SelectedItem;

                string[] Ver_Base = { "Latest", "Recommended" };
                cb_Mod_Base_Ver.Items.AddRange(Ver_Base);
                cb_Mod_Base_Ver.Items.AddRange(MAR.GetVersion(PackAnalysisRead.PackType.Base));
                cb_Mod_Base_Ver.Enabled = false;
                cb_Mod_Base_Upload.Items.AddRange(Ver_Base);
                cb_Mod_Base_Upload.Items.AddRange(MAR.GetVersion(PackAnalysisRead.PackType.Base));
                cb_Mod_Base_Upload.SelectedItem = Ver_Base[1];
                cb_Mod_Option_Ver.Items.AddRange(Ver_Base);
                cb_Mod_Option_Ver.Items.AddRange(MAR.GetVersion(PackAnalysisRead.PackType.Option));
                cb_Mod_Option_Ver.Enabled = false;
                cb_Mod_Option_Upload.Items.AddRange(Ver_Base);
                cb_Mod_Option_Upload.Items.AddRange(MAR.GetVersion(PackAnalysisRead.PackType.Option));
                cb_Mod_Option_Upload.SelectedItem = Ver_Base[1];

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Input박스를 생성합니다.
        /// </summary>
        /// <param name="Text">메시지박스 내용</param>
        /// <param name="Caption">메시지박스 캡션</param>
        /// <returns>입력값</returns>
        private string InputBox(string Text, string Caption = "삭제")
        {
            BSU_InputBox IB = new BSU_InputBox(Text, Caption);
            IB.ShowDialog();
            return IB.getInput();
        }
        private void btn_Mod_Set_Click(object sender, EventArgs e)
        {
            if (btn_Mod_Set.Text == "불러오기")
            {
                if (InitializeMod())
                {
                    //txt_MUID.ReadOnly = true;
                    //gb_Mod_Info.Enabled = false;
                    cb_MUID.Enabled = false;
                    tc_Setting.Enabled = true;
                    gb_Mod_Upload.Enabled = true;

                    if (btn_Base_Set.Text == "불러오기")
                        btn_Base_Set_Click(sender, e);
                    if (btn_Option_Set.Text == "불러오기")
                        btn_Option_Set_Click(sender, e);
                    btn_Mod_Load_Click(sender, e);

                    btn_Mod_Set.Text = "모드팩 삭제";
                    btn_Mod_Set.BackColor = Color.Red;
                }
                else
                {
                    Common.Message("존재하지 않는 MUID 입니다.");
                }
            }
            else
            { // 모드팩 삭제하기
                if (Common.Message("정말로 불러온 모드팩을 삭제하시겠습니까?" + Environment.NewLine + "삭제 요청 시 서버에서 즉시 삭제되며, 복구하실 수 없습니다.","모드팩 영구 삭제",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (InputBox("불러온 모드팩의 MUID값을 입력 해 주세요.", "모드팩 삭제") == (string)cb_MUID.SelectedItem)
                    {
                        if (InputBox("불러온 모드팩의 이름을 입력 해 주세요.", "모드팩 삭제") == txt_Mod_Name.Text)
                        {
                            if (InputBox("불러온 모드팩의 최신버전을 입력 해 주세요.", "모드팩 삭제") == txt_Mod_Latest.Text)
                            {
                                if (InputBox("불러온 모드팩의 권장버전을 입력 해 주세요.", "모드팩 삭제") == txt_Mod_Recommended.Text)
                                {
                                    gb_Mod_Info.Enabled = false;
                                    tc_Setting.Enabled = false;
                                    gb_Mod_Upload.Enabled = false;
                                    btn_Mod_Set.Enabled = false;

                                    // 모드팩 삭제 진행
                                    // 모드팩 파일 전체 삭제
                                    FTPUtil FTP_Delete = new FTPUtil(FTPUtil.OfficialServer.SangDolE_Cloud); // FTP 객체 생성
                                    string RootPath = Servers.SangDolE.FTP_PATH_BSL + "Pack/" + (string)cb_MUID.SelectedItem + "/";
                                    FTP_Delete.DeletePath(RootPath); // FTP 서버에서 팩 루트 폴더 삭제
                                    RootPath = RootPath.Replace(Servers.SangDolE.FTP_PATH_BSL, Servers.Bell_Soft_Network.FTP_PATH_INFO_BSL);

                                    // 모드팩 리스트에서 현재 모드팩 제거
                                    List<string> list = new List<string>();
                                    PackAnalysisRead MAR = new PackAnalysisRead();
                                    foreach (string tmp in PackAnalysisRead.LoadPackList(PackAnalysisRead.PackType.Mod))
                                    {
                                        if (tmp != (string)cb_MUID.SelectedItem)
                                            list.Add(tmp);
                                    }
                                    PackAnalysisWrite MAW = new PackAnalysisWrite();
                                    MAW.WriteListXML(list.ToArray());
                                    FTPUtil FTP_Info = new FTPUtil(FTPUtil.OfficialServer.Bell_Soft_Network_Info); // FTP 객체 생성
                                    string xmlPath = User.BSN_Temp + "BSU\\Data\\PackList.xml";
                                    FTP_Info.Upload(Servers.Bell_Soft_Network.FTP_PATH_INFO_BSL + "Pack/", xmlPath, true); // 모드팩 리스트 업로드
                                    FTP_Info.DeletePath(RootPath); // FTP 서버에서 팩 정보 루트 폴더 삭제

                                    Common.Message("모드팩 삭제가 완료되었습니다." + Environment.NewLine + "다른 모드팩에 접근하시려면 관리창을 재 실행하세요.");
                                }
                            }
                        }
                    }
                }
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
            if ((string)cb_Mod_Base.SelectedItem == string.Empty) stop = true;
            if ((string)cb_Mod_Option.SelectedItem == string.Empty) stop = true;

            if (stop)
            {
                Common.Message("모든 필드에 값을 입력해 주세요.");
                return;
            }
            PackAnalysisWrite MAW = null;// new ModAnalysisWrite(ModAnalysisWrite.Type.ModPack, (string)cb_MUID.SelectedItem, txt_Mod_Name.Text, txt_Mod_Latest.Text, txt_Mod_Recommended.Text, (string)cb_Mod_Base.SelectedItem, (string)cb_Mod_Option.SelectedItem, txt_Mod_News.Text, txt_Mod_Down.Text, lst_Mod_Version.Items.Cast<string>().ToArray());
            string xmlPath = User.BSN_Temp + "BSU\\Data\\ModPack\\" + (string)cb_MUID.SelectedItem + ".xml";
            MAW.WriteXML();
            
            // FTP서버에 정보 업로드
            FTPUtil FTP_Info = new FTPUtil(FTPUtil.OfficialServer.Bell_Soft_Network_Info); // FTP 객체 생성
            FTP_Info.Upload(Servers.Bell_Soft_Network.FTP_PATH_INFO_BSL + "Pack/" + (string)cb_MUID.SelectedItem + "/", xmlPath, true); // 모드팩 데이터 업로드
            InitializeMod(); // 다시한번 로드
            Common.Message("설정값 업로드 성공!");
        }
                
        private void btn_Mod_DelVer_Click(object sender, EventArgs e)
        {
            if (lst_Mod_Version.Items.Count > 1)
            {
                string SelectVer = (string)lst_Mod_Version.SelectedItem;
                if (txt_Mod_Latest.Text == SelectVer || txt_Mod_Recommended.Text == SelectVer) // 최신버전이나 권장버전이 삭제하려는 버전이면 먼저 버전 수정 요청
                {
                    Common.Message("이 버전은 최신버전 또는 권장버전으로 지정되어 있으므로 삭제할 수 없습니다.");
                    return;
                }
                tc_Setting.Enabled = false;
                // 해당 버전을 FTP서버에서 삭제
                FTPUtil FTP_Delete = new FTPUtil(FTPUtil.OfficialServer.SangDolE_Cloud); // FTP 객체 생성
                string RootPath = "Pack/" + (string)cb_MUID.SelectedItem + "/" + SelectVer + "/";
                FTP_Delete.DeletePath(RootPath); // FTP 서버에서 버전 폴더 삭제
                lst_Mod_Version.Items.Remove(lst_Mod_Version.SelectedItem);
                tc_Setting.Enabled = true;
                btn_Mod_Save_Click(sender, e);
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
                PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Mod, (string)cb_MUID.SelectedItem);
                MAR.LoadMod((string)lst_Mod_Version.SelectedItem);
                cb_Mod_Base_Ver.SelectedItem = MAR.GetInstallInfo(PackAnalysisRead.PackType.Mod, "Base");
                cb_Mod_Option_Ver.SelectedItem = MAR.GetInstallInfo(PackAnalysisRead.PackType.Mod, "Option");
            }
        }

        private bool InitializeBase()
        {
            PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Base, (string)cb_BUID.SelectedItem);

            if (MAR.Availability())
            {
                lst_Base_Version.Items.Clear();

                txt_Base_Latest.Text = MAR.GetInfo(PackAnalysisRead.PackType.Base, "Latest");
                txt_Base_Recommended.Text = MAR.GetInfo(PackAnalysisRead.PackType.Base, "Recommended");
                txt_Base_Down.Text = MAR.GetInfo(PackAnalysisRead.PackType.Base, "Down");
                lst_Base_Version.Items.AddRange(MAR.GetVersion(PackAnalysisRead.PackType.Base));

                return true;
            }
            else
            {
                return false;
            }
        }
        private void btn_Base_Set_Click(object sender, EventArgs e)
        {
            if (btn_Base_Set.Text == "불러오기")
            {
                if (InitializeBase())
                {
                    //gb_Base_Info.Enabled = false;
                    cb_BUID.Enabled = false;
                    gb_Base_Setting.Enabled = true;
                    gb_Base_Upload.Enabled = true;

                    btn_Base_Load_Click(sender, e);
                    btn_Base_Set.Text = "베이스팩 삭제";
                    btn_Base_Set.BackColor = Color.Red;
                }
                else
                {
                    Common.Message("존재하지 않는 BUID 입니다.");
                }
            }
            else
            { // 베이스팩 삭제
                if (Common.Message("정말로 불러온 베이스팩을 삭제하시겠습니까?" + Environment.NewLine + "삭제 요청 시 서버에서 즉시 삭제되며, 복구하실 수 없습니다.", "베이스팩 영구 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (InputBox("불러온 베이스팩의 BUID값을 입력 해 주세요.", "베이스팩 삭제") == (string)cb_BUID.SelectedItem)
                    {
                        if (InputBox("불러온 베이스팩의 최신버전을 입력 해 주세요.", "베이스팩 삭제") == txt_Base_Latest.Text)
                        {
                            if (InputBox("불러온 베이스팩의 권장버전을 입력 해 주세요.", "베이스팩 삭제") == txt_Base_Recommended.Text)
                            {
                                // 선택한 베이스팩을 의존하는 모드팩이 있는지 확인, 있으면 삭제 중단

                                gb_Base_Info.Enabled = false;
                                gb_Base_Setting.Enabled = false;
                                gb_Base_Upload.Enabled = false;
                                btn_Base_Set.Enabled = false;

                                // 베이스팩 삭제 진행
                                // 베이스팩 파일 전체 삭제
                                FTPUtil FTP_Delete = new FTPUtil(FTPUtil.OfficialServer.SangDolE_Cloud); // FTP 객체 생성
                                string RootPath = Servers.SangDolE.FTP_PATH_BSL + "Base/" + (string)cb_BUID.SelectedItem + "/";
                                FTP_Delete.DeletePath(RootPath); // FTP 서버에서 팩 루트 폴더 삭제
                                RootPath = RootPath.Replace(Servers.SangDolE.FTP_PATH_BSL, Servers.Bell_Soft_Network.FTP_PATH_INFO_BSL);

                                // 베이스팩 리스트에서 선택된 베이스팩 제거
                                List<string> list = new List<string>();
                                PackAnalysisRead MAR = new PackAnalysisRead();
                                foreach (string tmp in PackAnalysisRead.LoadPackList(PackAnalysisRead.PackType.Base))
                                {
                                    if (tmp != (string)cb_BUID.SelectedItem)
                                        list.Add(tmp);
                                }
                                PackAnalysisWrite MAW = new PackAnalysisWrite();
                                MAW.WriteListXML(list.ToArray());
                                FTPUtil FTP_Info = new FTPUtil(FTPUtil.OfficialServer.Bell_Soft_Network_Info); // FTP 객체 생성
                                string xmlPath = User.BSN_Temp + "BSU\\Data\\PackList.xml";
                                FTP_Info.Upload(Servers.Bell_Soft_Network.FTP_PATH_INFO_BSL + "Base/", xmlPath, true); // 베이스팩 리스트 업로드
                                FTP_Info.DeletePath(RootPath); // FTP 서버에서 팩 정보 루트 폴더 삭제

                                Common.Message("베이스팩 삭제가 완료되었습니다." + Environment.NewLine + "다른 베이스팩에 접근하시려면 관리창을 재 실행하세요.");
                            }
                        }
                    }
                }
            }
        }

        private bool InitializeOption()
        {
            PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Option, (string)cb_OUID.SelectedItem);

            if (MAR.Availability())
            {
                lst_Option_Version.Items.Clear();

                txt_Option_Latest.Text = MAR.GetInfo(PackAnalysisRead.PackType.Option, "Latest");
                txt_Option_Recommended.Text = MAR.GetInfo(PackAnalysisRead.PackType.Option, "Recommended");
                txt_Option_Down.Text = MAR.GetInfo(PackAnalysisRead.PackType.Option, "Down");
                lst_Option_Version.Items.AddRange(MAR.GetVersion(PackAnalysisRead.PackType.Option));

                return true;
            }
            else
            {
                return false;
            }
        }
        private void btn_Option_Set_Click(object sender, EventArgs e)
        {
            if (btn_Option_Set.Text == "불러오기")
            {
                if (InitializeOption())
                {
                    //gb_Option_Info.Enabled = false;
                    cb_OUID.Enabled = false;
                    gb_Option_Setting.Enabled = true;
                    gb_Option_Upload.Enabled = true;

                    btn_Option_Load_Click(sender, e);
                    btn_Option_Set.Text = "옵션팩 삭제";
                    btn_Option_Set.BackColor = Color.Red;
                }
                else
                {
                    Common.Message("존재하지 않는 OUID 입니다.");
                }
            }
            else
            { // 옵션팩 삭제
                if (Common.Message("정말로 불러온 옵션팩을 삭제하시겠습니까?" + Environment.NewLine + "삭제 요청 시 서버에서 즉시 삭제되며, 복구하실 수 없습니다.", "옵션팩 영구 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (InputBox("불러온 옵션팩의 OUID값을 입력 해 주세요.", "옵션팩 삭제") == (string)cb_OUID.SelectedItem)
                    {
                        if (InputBox("불러온 옵션팩의 최신버전을 입력 해 주세요.", "옵션팩 삭제") == txt_Option_Latest.Text)
                        {
                            if (InputBox("불러온 옵션팩의 권장버전을 입력 해 주세요.", "옵션팩 삭제") == txt_Option_Recommended.Text)
                            {
                                // 선택한 옵션팩을 의존하는 모드팩이 있는지 확인, 있으면 삭제 중단

                                gb_Option_Info.Enabled = false;
                                gb_Option_Setting.Enabled = false;
                                gb_Option_Upload.Enabled = false;
                                btn_Option_Set.Enabled = false;

                                // 옵션팩 삭제 진행
                                // 옵션팩 파일 전체 삭제
                                FTPUtil FTP_Delete = new FTPUtil(FTPUtil.OfficialServer.SangDolE_Cloud); // FTP 객체 생성
                                string RootPath = Servers.SangDolE.FTP_PATH_BSL + "Option/" + (string)cb_OUID.SelectedItem + "/";
                                FTP_Delete.DeletePath(RootPath); // FTP 서버에서 팩 루트 폴더 삭제
                                RootPath = RootPath.Replace(Servers.SangDolE.FTP_PATH_BSL, Servers.Bell_Soft_Network.FTP_PATH_INFO_BSL);

                                // 옵션팩 리스트에서 선택된 옵션팩 제거
                                List<string> list = new List<string>();
                                PackAnalysisRead MAR = new PackAnalysisRead();
                                foreach (string tmp in PackAnalysisRead.LoadPackList(PackAnalysisRead.PackType.Option))
                                {
                                    if (tmp != (string)cb_OUID.SelectedItem)
                                        list.Add(tmp);
                                }
                                PackAnalysisWrite MAW = new PackAnalysisWrite();
                                MAW.WriteListXML(list.ToArray());
                                FTPUtil FTP_Info = new FTPUtil(FTPUtil.OfficialServer.Bell_Soft_Network_Info); // FTP 객체 생성
                                string xmlPath = User.BSN_Temp + "BSU\\Data\\PackList.xml";
                                FTP_Info.Upload(Servers.Bell_Soft_Network.FTP_PATH_INFO_BSL + "Option/", xmlPath, true); // 모드팩 리스트 업로드
                                FTP_Info.DeletePath(RootPath); // FTP 서버에서 팩 정보 루트 폴더 삭제

                                Common.Message("옵션팩 삭제가 완료되었습니다." + Environment.NewLine + "다른 옵션팩에 접근하시려면 관리창을 재 실행하세요.");
                            }
                        }
                    }
                }
            }
        }

        private void btn_Base_DelVer_Click(object sender, EventArgs e)
        {
            if (lst_Base_Version.Items.Count > 1)
            {
                string SelectVer = (string)lst_Base_Version.SelectedItem;
                if (txt_Base_Latest.Text == SelectVer || txt_Base_Recommended.Text == SelectVer) // 최신버전이나 권장버전이 삭제하려는 버전이면 먼저 버전 수정 요청
                {
                    Common.Message("이 버전은 최신버전 또는 권장버전으로 지정되어 있으므로 삭제할 수 없습니다.");
                    return;
                }
                gb_Base_Setting.Enabled = false;
                // 해당 버전을 FTP서버에서 삭제
                FTPUtil FTP_Delete = new FTPUtil(FTPUtil.OfficialServer.SangDolE_Cloud); // FTP 객체 생성
                string RootPath = "Base/" + (string)cb_BUID.SelectedItem + "/" + SelectVer + "/";
                FTP_Delete.DeletePath(RootPath); // FTP 서버에서 버전 폴더 삭제
                lst_Base_Version.Items.Remove(lst_Base_Version.SelectedItem);
                gb_Base_Setting.Enabled = true;
                btn_Base_Save_Click(sender, e);
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
                string SelectVer = (string)lst_Option_Version.SelectedItem;
                if (txt_Option_Latest.Text == SelectVer || txt_Option_Recommended.Text == SelectVer) // 최신버전이나 권장버전이 삭제하려는 버전이면 먼저 버전 수정 요청
                {
                    Common.Message("이 버전은 최신버전 또는 권장버전으로 지정되어 있으므로 삭제할 수 없습니다.");
                    return;
                }
                gb_Option_Setting.Enabled = false;
                // 해당 버전을 FTP서버에서 삭제
                FTPUtil FTP_Delete = new FTPUtil(FTPUtil.OfficialServer.SangDolE_Cloud); // FTP 객체 생성
                string RootPath = "Option/" + (string)cb_OUID.SelectedItem + "/" + SelectVer + "/";
                FTP_Delete.DeletePath(RootPath); // FTP 서버에서 버전 폴더 삭제
                lst_Option_Version.Items.Remove(lst_Option_Version.SelectedItem);
                gb_Option_Setting.Enabled = true;
                btn_Option_Save_Click(sender, e);
            }
            else
            {
                Common.Message("최소 한개 이상의 버전이 존재해야합니다.");
            }
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
            string SetVer = (string)lst_Mod_Version.SelectedItem;
            string MUID = (string)cb_MUID.SelectedItem;
            string xmlPath = User.BSN_Temp + "BSU\\Data\\ModPack\\Version\\" + SetVer + ".xml";
            PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Mod, MUID);
            PackAnalysisWrite MAW = new PackAnalysisWrite(PackAnalysisWrite.Type.ModPack, MUID);
            MAR.LoadMod(SetVer);
            MAW.WriteInstallXML(SetVer, (string)cb_Mod_Base_Ver.SelectedItem, (string)cb_Mod_Option_Ver.SelectedItem, MAR.GetInstallData(PackAnalysisRead.PackType.Mod, "Directory"), MAR.GetInstallData(PackAnalysisRead.PackType.Mod, "Hash"));
            // FTP 서버에 업로드.
            FTPUtil FTP_Info = new FTPUtil(FTPUtil.OfficialServer.Bell_Soft_Network_Info); // FTP 객체 생성
            FTP_Info.Upload("Pack/" + MUID + "/Version/", xmlPath); // 버전 데이터 업로드
            File.Delete(xmlPath); // 로컬 버전 데이터 삭제
            Common.Message(SetVer + "버전 설정값 업로드 성공!");
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
            PackAnalysisWrite MAW = new PackAnalysisWrite(PackAnalysisWrite.Type.BasePack, (string)cb_BUID.SelectedItem, txt_Base_Latest.Text, txt_Base_Recommended.Text, txt_Base_Down.Text, lst_Base_Version.Items.Cast<string>().ToArray());
            MAW.WriteXML();

            // FTP서버에 정보 업로드
            FTPUtil FTP_Info = new FTPUtil(FTPUtil.OfficialServer.Bell_Soft_Network_Info); // FTP 객체 생성
            string xmlPath = User.BSN_Temp + "BSU\\Data\\BasePack\\" + (string)cb_BUID.SelectedItem + ".xml";
            FTP_Info.Upload(Servers.Bell_Soft_Network.FTP_PATH_INFO_BSL + "Base/" + (string)cb_BUID.SelectedItem + "/", xmlPath, true); // 베이스팩 데이터 업로드
            InitializeBase(); // 다시한번 로드
            Common.Message("설정값 업로드 성공!");
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
            PackAnalysisWrite MAW = new PackAnalysisWrite(PackAnalysisWrite.Type.OptionPack, (string)cb_OUID.SelectedItem, txt_Option_Latest.Text, txt_Option_Recommended.Text, txt_Option_Down.Text, lst_Option_Version.Items.Cast<string>().ToArray());
            MAW.WriteXML();

            // FTP서버에 정보 업로드
            FTPUtil FTP_Info = new FTPUtil(FTPUtil.OfficialServer.Bell_Soft_Network_Info); // FTP 객체 생성
            string xmlPath = User.BSN_Temp + "BSU\\Data\\OptionPack\\" + (string)cb_OUID.SelectedItem + ".xml";
            FTP_Info.Upload(Servers.Bell_Soft_Network.FTP_PATH_INFO_BSL + "Option/" + (string)cb_OUID.SelectedItem + "/", xmlPath, true); // 옵션팩 데이터 업로드

            InitializeOption(); // 다시한번 로드
            Common.Message("설정값 업로드 성공!");
        }

        /// <summary>
        /// 모드 업로드시 모드팩 정보를 수정하지 못하게 막습니다.
        /// </summary>
        /// <param name="value">업로드 진행중 여부</param>
        private void ModUploading(bool value)
        {
            value = !value;
            btn_Mod_Upload.Enabled = value;
            btn_Mod_Load.Enabled = value;
            txt_Mod_Version.Enabled = value;
            cb_Mod_Base_Upload.Enabled = value;
            cb_Mod_Option_Upload.Enabled = value;
            cb_Mod_Latest.Enabled = value;
            cb_Mod_Recommended.Enabled = value;
            lst_Mod_File.Enabled = value;

            pb_Mod_Upload.Value = 0;
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
            foreach (string tmp in lst_Mod_Version.Items)
            {
                if (txt_Mod_Version.Text == tmp)
                {
                    Common.Message("이미 서버에 등록되어 있는 버전입니다.");
                    return;
                }
            }
            ModUploading(true);
            btn_Mod_Load_Click(sender, e); // 혹시 모르니 리스트 새로고침
            List<string> list = new List<string>();
            Protection Pro = new Protection();
            string SetVer = txt_Mod_Version.Text; // 업로드시 설정 버전
            string MUID = (string)cb_MUID.SelectedItem; // MUID
            string LocalRoot = (string)llb_Mod_Upload.Tag; // 업로드 루트폴더
            string RequireBase = (string)cb_Mod_Base_Upload.SelectedItem; // 필요 베이스팩 버전
            string RequireOption = (string)cb_Mod_Option_Upload.SelectedItem; // 필요 옵션팩 버전
            string[] FileArray = lst_Mod_File.Items.Cast<string>().ToArray(); // 파일 리스트 배열
            string[] Directory = Common.GetDirectoryArray(LocalRoot, true); // 생성이 필요한 디렉토리 배열
            string[] Hash; // 파일 해시
            // 모드팩 버전.xml 생성
            foreach (string tmp in FileArray)
            {
                string Path = LocalRoot + tmp;
                list.Add(tmp + "|" + Pro.MD5Hash(Path));
            }
            Hash = list.ToArray();
            PackAnalysisWrite MAW = new PackAnalysisWrite(PackAnalysisWrite.Type.ModPack, MUID);
            MAW.WriteInstallXML(SetVer, RequireBase, RequireOption, Directory, Hash);
            Application.DoEvents(); // 반복문 수행시 UI가 렉먹는걸 방지하기 위해 메시지 큐 처리!

            // 모드팩.XML 생성
            PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Mod, MUID);
            if (MAR.Availability())
            {
                string Name, Latest, Recommended, Base, Option, News, Down;
                List<string> Version = new List<string>();
                Name = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Name");
                Latest = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Latest");
                Recommended = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Recommended");
                News = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "News");
                Down = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Down");
                Base = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Base");
                Option = MAR.GetInfo(PackAnalysisRead.PackType.Mod, "Option");

                foreach (string tmp in MAR.GetVersion(PackAnalysisRead.PackType.Mod))
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
                MAW = new PackAnalysisWrite(PackAnalysisWrite.Type.ModPack, MUID, Name, Latest, Recommended, Base, Option, News, Down, Version.ToArray());
                MAW.WriteXML();
            }
            else
            {
                ModUploading(false);
                Common.Message(MUID + ".xml 파일 작성을 시도하던 중 문제가 발생하였습니다." + Environment.NewLine + "MAR.Availability() = false");
                return;
            }

            FTPUtil FTP_Data = new FTPUtil(FTPUtil.OfficialServer.SangDolE_Cloud); // FTP 객체 생성
            string FTP_Default_Cloud = Servers.SangDolE.FTP_PATH_BSL + "Pack/" + MUID + "/"; // FTP 서버접속시 기본경로
            string FTP_Default_Info = Servers.Bell_Soft_Network.FTP_PATH_INFO_BSL + "Pack/" + MUID + "/";
            pb_Mod_Upload.Maximum = Directory.Length + FileArray.Length;

            // 디렉토리 생성
            FTP_Data.MakeDir(FTP_Default_Cloud);
            FTP_Data.MakeDir(FTP_Default_Cloud + SetVer);
            foreach (string tmp in Directory)
            {
                FTP_Data.MakeDir(FTP_Default_Cloud + SetVer + "/" + tmp.Replace("\\", "/"));
                pb_Mod_Upload.PerformStep();
                Application.DoEvents(); // 반복문 수행시 UI가 렉먹는걸 방지하기 위해 메시지 큐 처리!
            }

            // 파일 업로드
            foreach (string tmp in FileArray)
            {
                // 파일 리스트 배열에 값을 디렉토리, 파일명으로 나눠야됨.
                FileInfo FI = new FileInfo(LocalRoot + tmp);
                string FTPDir = FI.DirectoryName.Replace(LocalRoot, string.Empty).Replace(LocalRoot.Substring(0, LocalRoot.Length - 1), string.Empty).Replace('\\', '/');
                FTP_Data.Upload(FTP_Default_Cloud + SetVer + "/" + FTPDir, LocalRoot + tmp);
                pb_Mod_Upload.PerformStep();
                Application.DoEvents(); // 반복문 수행시 UI가 렉먹는걸 방지하기 위해 메시지 큐 처리!
            }

            // xml 업로드
            FTPUtil FTP_Info = new FTPUtil(FTPUtil.OfficialServer.Bell_Soft_Network_Info); // FTP 객체 생성
            string xmlPackPath = User.BSN_Temp + "BSU\\Data\\ModPack\\" + MUID + ".xml";
            string xmlVerPath = User.BSN_Temp + "BSU\\Data\\ModPack\\Version\\" + SetVer + ".xml";
            FTP_Info.Upload(FTP_Default_Info, xmlPackPath, true); // 모드팩 데이터 업로드
            FTP_Info.Upload(FTP_Default_Info + "Version/", xmlVerPath, true); // 버전 데이터 업로드

            ModUploading(false); // 업로드 끝
            txt_Mod_Version.Text = string.Empty;
            cb_Mod_Latest.Checked = false;
            cb_Mod_Recommended.Checked = false;
            InitializeMod();
            Common.Message("모드팩이 정상적으로 업로드 되었습니다!");
        }

        private void BaseUploading(bool value)
        {
            value = !value;
            btn_Base_Upload.Enabled = value;
            btn_Base_Load.Enabled = value;
            txt_Base_Version.Enabled = value;
            cb_Base_Latest.Enabled = value;
            cb_Base_Recommended.Enabled = value;
            lst_Base_File.Enabled = value;

            pb_Base_Upload.Value = 0;
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
            foreach (string tmp in lst_Base_Version.Items)
            {
                if (txt_Base_Version.Text == tmp)
                {
                    Common.Message("이미 서버에 등록되어 있는 버전입니다.");
                    return;
                }
            }
            BaseUploading(true);
            btn_Base_Load_Click(sender, e); // 혹시 모르니 리스트 새로고침
            List<string> list = new List<string>();
            Protection Pro = new Protection();
            string[] FileArray = lst_Base_File.Items.Cast<string>().ToArray(); // 파일 리스트 배열
            string SetVer = txt_Base_Version.Text; // 업로드시 설정버전
            string BUID = (string)cb_BUID.SelectedItem; // BUID
            string LocalRoot = (string)llb_Base_Upload.Tag; // 업로드 루트폴더
            string[] Directory = Common.GetDirectoryArray(LocalRoot, true); // 생성이 필요한 디렉토리 배열
            string[] Hash; // 파일 해시

            // 베이스팩 버전.xml 생성
            foreach (string tmp in FileArray)
            {
                string Path = LocalRoot + tmp;
                list.Add(tmp + "|" + Pro.MD5Hash(Path));
            }
            Hash = list.ToArray();
            PackAnalysisWrite MAW = new PackAnalysisWrite(PackAnalysisWrite.Type.BasePack, BUID);
            MAW.WriteInstallXML(SetVer, Directory, Hash);
            
            // 베이스팩.XML 생성
            PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Base, BUID);
            if (MAR.Availability())
            {
                string Latest, Recommended, Down;
                List<string> Version = new List<string>();
                Latest = MAR.GetInfo(PackAnalysisRead.PackType.Base, "Latest");
                Recommended = MAR.GetInfo(PackAnalysisRead.PackType.Base, "Recommended");
                Down = MAR.GetInfo(PackAnalysisRead.PackType.Base, "Down");

                foreach (string tmp in MAR.GetVersion(PackAnalysisRead.PackType.Base))
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
                MAW = new PackAnalysisWrite(PackAnalysisWrite.Type.BasePack, BUID, Latest, Recommended, Down, Version.ToArray());
                MAW.WriteXML();
            }
            else
            {
                BaseUploading(false);
                Common.Message(BUID + ".xml 파일 작성을 시도하던 중 문제가 발생하였습니다." + Environment.NewLine + "MAR.Availability() = false");
                return;
            }

            // 파일 업로드
            FTPUtil FTP_Data = new FTPUtil(FTPUtil.OfficialServer.SangDolE_Cloud); // FTP 객체 생성
            string FTP_Default_Cloud = Servers.SangDolE.FTP_PATH_BSL + "Base/" + BUID + "/"; // FTP 서버접속시 기본경로
            string FTP_Default_Info = Servers.Bell_Soft_Network.FTP_PATH_INFO_BSL + "Base/" + BUID + "/";
            pb_Base_Upload.Maximum = Directory.Length + FileArray.Length;

            // 디렉토리 생성
            FTP_Data.MakeDir(FTP_Default_Cloud);
            FTP_Data.MakeDir(FTP_Default_Cloud + SetVer);
            foreach (string tmp in Directory)
            {
                FTP_Data.MakeDir(FTP_Default_Cloud + SetVer + "/" + tmp.Replace("\\", "/"));
                pb_Base_Upload.PerformStep();
                Application.DoEvents(); // 반복문 수행시 UI가 렉먹는걸 방지하기 위해 메시지 큐 처리!
            }

            // 파일 업로드
            foreach (string tmp in FileArray)
            {
                // 파일 리스트 배열에 값을 디렉토리, 파일명으로 나눠야됨.
                FileInfo FI = new FileInfo(LocalRoot + tmp);
                string FTPDir = FI.DirectoryName.Replace(LocalRoot, string.Empty).Replace(LocalRoot.Substring(0, LocalRoot.Length - 1), string.Empty);
                FTP_Data.Upload(FTP_Default_Cloud + SetVer + "/" + FTPDir, LocalRoot + tmp);
                pb_Base_Upload.PerformStep();
                Application.DoEvents(); // 반복문 수행시 UI가 렉먹는걸 방지하기 위해 메시지 큐 처리!
            }

            // xml 업로드
            FTPUtil FTP_Info = new FTPUtil(FTPUtil.OfficialServer.Bell_Soft_Network_Info); // FTP 객체 생성
            string xmlPackPath = User.BSN_Temp + "BSU\\Data\\BasePack\\" + BUID + ".xml";
            string xmlVerPath = User.BSN_Temp + "BSU\\Data\\BasePack\\Version\\" + SetVer + ".xml";
            FTP_Info.Upload(FTP_Default_Info, xmlPackPath, true); // 모드팩 데이터 업로드
            FTP_Info.Upload(FTP_Default_Info + "Version/", xmlVerPath, true); // 버전 데이터 업로드

            BaseUploading(false); // 업로드 끝
            txt_Base_Version.Text = string.Empty;
            cb_Base_Latest.Checked = false;
            cb_Base_Recommended.Checked = false;
            InitializeBase(); // 화면 새로고침
            Common.Message("베이스팩이 정상적으로 업로드 되었습니다!");
        }

        private void OptionUploading(bool value)
        {
            value = !value;
            btn_Option_Upload.Enabled = value;
            btn_Option_Load.Enabled = value;
            txt_Option_Version.Enabled = value;
            cb_Option_Latest.Enabled = value;
            cb_Option_Recommended.Enabled = value;
            lst_Option_File.Enabled = value;

            txt_Option_Name.Enabled = false;
            txt_Option_UID.Enabled = false;
            cb_Option_Default.Enabled = false;
            btn_Option_Apply.Enabled = false;

            pb_Option_Upload.Value = 0;
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
            foreach (string tmp in lst_Option_Version.Items)
            {
                if (txt_Option_Version.Text == tmp)
                {
                    Common.Message("이미 서버에 등록되어 있는 버전입니다.");
                    return;
                }
            }
            OptionUploading(true);
            List<string> Option = new List<string>();
            List<string> Hash = new List<string>();
            List<string> FileList = new List<string>();
            Protection Pro = new Protection();
            string SetVer = txt_Option_Version.Text;
            string[] FileArray; // = lst_Option_File.Items.Cast<string>().ToArray(); // 파일 리스트 배열
            string OUID = (string)cb_OUID.SelectedItem; // OUID
            string LocalRoot = (string)llb_Option_Upload.Tag; // 업로드 루트폴더
            string[] Directory = Common.GetDirectoryArray(LocalRoot, true); // 생성이 필요한 디렉토리 배열

            // 옵션팩 버전.xml 생성
            foreach (ListViewItem item in lst_Option_File.Items)
            {
                if (item.SubItems[0].Text == string.Empty || item.SubItems[1].Text == string.Empty || item.SubItems[2].Text == string.Empty || item.SubItems[3].Text == string.Empty)
                {
                    OptionUploading(false);
                    Common.Message("옵션파일 상세리스트 모든 필드에 값을 입력해 주세요.");
                    return;
                }
                Option.Add(item.SubItems[0].Text + "|" + item.SubItems[1].Text + "|" + item.SubItems[2].Text);
                Hash.Add(item.SubItems[0].Text + "|" + item.SubItems[3].Text + "|" + Pro.MD5Hash(LocalRoot + item.SubItems[3].Text));
                FileList.Add(item.SubItems[3].Text);
            }
            FileArray = FileList.ToArray();
            PackAnalysisWrite MAW = new PackAnalysisWrite(PackAnalysisWrite.Type.OptionPack, OUID);
            MAW.WriteInstallXML(SetVer, Option.ToArray(), Directory, Hash.ToArray());
            
            // 옵션팩.XML 생성
            PackAnalysisRead MAR = new PackAnalysisRead(PackAnalysisRead.PackType.Option, OUID);
            if (MAR.Availability())
            {
                string Latest, Recommended, Down;
                List<string> Version = new List<string>();
                Latest = MAR.GetInfo(PackAnalysisRead.PackType.Option, "Latest");
                Recommended = MAR.GetInfo(PackAnalysisRead.PackType.Option, "Recommended");
                Down = MAR.GetInfo(PackAnalysisRead.PackType.Option, "Down");

                foreach (string tmp in MAR.GetVersion(PackAnalysisRead.PackType.Option))
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
                MAW = new PackAnalysisWrite(PackAnalysisWrite.Type.OptionPack, OUID, Latest, Recommended, Down, Version.ToArray());
                MAW.WriteXML();
            }
            else
            {
                OptionUploading(false);
                Common.Message(OUID + ".xml 파일 작성을 시도하던 중 문제가 발생하였습니다." + Environment.NewLine + "MAR.Availability() = false");
                return;
            }

            // 파일 업로드
            FTPUtil FTP_Data = new FTPUtil(FTPUtil.OfficialServer.SangDolE_Cloud); // FTP 객체 생성
            string FTP_Default_Cloud = Servers.SangDolE.FTP_PATH_BSL + "Option/" + OUID + "/"; // FTP 서버접속시 기본경로
            string FTP_Default_Info = Servers.Bell_Soft_Network.FTP_PATH_INFO_BSL + "Option/" + OUID + "/";
            pb_Option_Upload.Maximum = Directory.Length + FileArray.Length;

            // 디렉토리 생성
            FTP_Data.MakeDir(FTP_Default_Cloud);
            FTP_Data.MakeDir(FTP_Default_Cloud + SetVer);
            foreach (string tmp in Directory)
            {
                FTP_Data.MakeDir(FTP_Default_Cloud + SetVer + "/" + tmp.Replace("\\", "/"));
                pb_Option_Upload.PerformStep();
                Application.DoEvents(); // 반복문 수행시 UI가 렉먹는걸 방지하기 위해 메시지 큐 처리!
            }

            // 파일 업로드
            foreach (string tmp in FileArray)
            {
                // 파일 리스트 배열에 값을 디렉토리, 파일명으로 나눠야됨.
                FileInfo FI = new FileInfo(LocalRoot + tmp);
                string FTPDir = FI.DirectoryName.Replace(LocalRoot, string.Empty).Replace(LocalRoot.Substring(0, LocalRoot.Length - 1), string.Empty);
                FTP_Data.Upload(FTP_Default_Cloud + SetVer + "/" + FTPDir, LocalRoot + tmp);
                pb_Option_Upload.PerformStep();
                Application.DoEvents(); // 반복문 수행시 UI가 렉먹는걸 방지하기 위해 메시지 큐 처리!
            }

            // xml 업로드
            FTPUtil FTP_Info = new FTPUtil(FTPUtil.OfficialServer.Bell_Soft_Network_Info); // FTP 객체 생성
            string xmlPackPath = User.BSN_Temp + "BSU\\Data\\OptionPack\\" + OUID + ".xml";
            string xmlVerPath = User.BSN_Temp + "BSU\\Data\\OptionPack\\Version\\" + SetVer + ".xml";
            FTP_Info.Upload(FTP_Default_Info, xmlPackPath, true); // 모드팩 데이터 업로드
            FTP_Info.Upload(FTP_Default_Info + "Version/", xmlVerPath, true); // 버전 데이터 업로드

            OptionUploading(false); // 업로드 끝
            txt_Option_Version.Text = string.Empty;
            cb_Option_Latest.Checked = false;
            cb_Option_Recommended.Checked = false;
            InitializeOption(); // 화면 새로고침
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
            btn_Mod_Upload.Enabled = true;
        }

        private void btn_Base_Load_Click(object sender, EventArgs e)
        {
            lst_Base_File.Items.Clear();
            lst_Base_File.Items.AddRange(Common.GetFileArray((string)llb_Base_Upload.Tag, true));

            btn_Base_Upload.Enabled = true;
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
            txt_Option_Name.Text = string.Empty;
        }

        private void txt_Option_UID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            ListView.SelectedListViewItemCollection col = lst_Option_File.SelectedItems;

            foreach (ListViewItem item in col)
                item.SubItems[1].Text = txt_Option_UID.Text;
            txt_Option_UID.Text = string.Empty;
        }

        private void cb_Option_Default_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection col = lst_Option_File.SelectedItems;

            foreach (ListViewItem item in col)
                item.SubItems[2].Text = (string)cb_Option_Default.SelectedItem;
        }

        private void btn_Option_Apply_Click(object sender, EventArgs e)
        {
            if (txt_Option_UID.Text == string.Empty || txt_Option_Name.Text == string.Empty)
            {
                Common.Message("필드에 누락된 값이 있습니다.");
                return;
            }
            ListView.SelectedListViewItemCollection col = lst_Option_File.SelectedItems;
            
            foreach (ListViewItem item in col)
            {
                item.SubItems[0].Text = txt_Option_Name.Text;
                item.SubItems[1].Text = txt_Option_UID.Text;
                item.SubItems[2].Text = (string)cb_Option_Default.SelectedItem;
            }
            txt_Option_Name.Text = string.Empty;
            txt_Option_UID.Text = string.Empty;
        }

        private void btn_Mod_Add_Click(object sender, EventArgs e)
        {

        }
    }
}
