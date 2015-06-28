﻿using BellLib.Class;
using BellLib.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Server.Source.BSU
{
    public partial class BSU_PackMaker : Form
    {
        public BSU_PackMaker()
        {
            InitializeComponent();
            InitializeMod();
        }
        private void InitializeMod()
        {
            cb_Mod_Base.Items.Clear();
            cb_Mod_Option.Items.Clear();

            ModAnalysisRead MAR = new ModAnalysisRead();
            
            cb_Mod_Base.Items.AddRange(MAR.GetList(ModAnalysisRead.PackType.Base)); // 팩 리스트 로드!
            cb_Mod_Base.SelectedIndex = 0; // 첫번째 모드팩 기본 선택

            cb_Mod_Option.Items.AddRange(MAR.GetList(ModAnalysisRead.PackType.Option)); // 팩 리스트 로드!
            cb_Mod_Option.SelectedIndex = 0; // 첫번째 모드팩 기본 선택

            txt_MUID.Text = null;
            txt_Mod_Down.Text = null;
            txt_Mod_Name.Text = null;
            txt_Mod_News.Text = null;
        }

        private void InitializeBase()
        {
            txt_BUID.Text = null;
            txt_Base_Down.Text = null;
        }

        private void InitializeOption()
        {
            txt_OUID.Text = null;
            txt_Option_Down.Text = null;
        }

        private void btn_Mod_Upload_Click(object sender, EventArgs e)
        {
            // 필드 검사
            if (txt_MUID.Text == string.Empty || txt_Mod_Down.Text == string.Empty || txt_Mod_Name.Text == string.Empty || txt_Mod_News.Text == string.Empty || cb_Mod_Base.SelectedItem == null || cb_Mod_Option.SelectedItem == null)
            {
                Common.Message("모든 필드에 값을 입력해 주세요.");
                return;
            }
            
            ModAnalysisRead MAR = new ModAnalysisRead();
            foreach (string tmp in MAR.GetList(ModAnalysisRead.PackType.Mod))
            {
                if (txt_MUID.Text == tmp)
                {
                    Common.Message("이미 존재하는 MUID입니다." + Environment.NewLine + "다른 MUID 값으로 시도하거나, 모드팩 관리자 기능으로 수정하십시오.");
                    return;
                }
            }
            btn_Mod_Upload.Enabled = false;
            gb_ModPack.Enabled = false;

            // 작성 시작
            ModAnalysisWrite MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.ModPack, txt_MUID.Text, txt_Mod_Name.Text, null, null, (string)cb_Mod_Base.SelectedItem, (string)cb_Mod_Option.SelectedItem, txt_Mod_News.Text, txt_Mod_Down.Text, null);
            string xmlPath = User.BSN_Temp + "BSU\\Data\\ModPack\\" + txt_MUID.Text + ".xml";
            MAW.WriteXML();

            // FTP서버에 정보 업로드
            FTPUtil FTP_Info = new FTPUtil(BellLib.Data.Base.SERVER_IP, BellLib.Data.Base.FTP_Info_ID, BellLib.Data.Base.FTP_Info_PW); // FTP 객체 생성
            FTP_Info.MakeDir("Pack/" + txt_MUID.Text + "/Version/");
            FTP_Info.Upload("Pack/" + txt_MUID.Text + "/", xmlPath, true); // 모드팩 데이터 업로드
            //File.Delete(xmlPath); // xml 파일 삭제

            List<string> list = new List<string>();
            list.AddRange(MAR.GetList(ModAnalysisRead.PackType.Mod));
            list.Add(txt_MUID.Text);
            MAW.WriteListXML(list.ToArray());
            xmlPath = User.BSN_Temp + "BSU\\Data\\PackList.xml";
            FTP_Info.Upload("Pack/", xmlPath, true); // 모드팩 리스트 업로드

            InitializeMod(); // 다시한번 로드
            btn_Mod_Upload.Enabled = true;
            gb_ModPack.Enabled = true;
            Common.Message("모드팩 등록 성공!");
        }

        private void btn_Base_Upload_Click(object sender, EventArgs e)
        {
            // 필드 검사
            if (txt_BUID.Text == string.Empty || txt_Base_Down.Text == string.Empty)
            {
                Common.Message("모든 필드에 값을 입력해 주세요.");
                return;
            }

            ModAnalysisRead MAR = new ModAnalysisRead();
            foreach (string tmp in MAR.GetList(ModAnalysisRead.PackType.Base))
            {
                if (txt_BUID.Text == tmp)
                {
                    Common.Message("이미 존재하는 BUID입니다." + Environment.NewLine + "다른 BUID 값으로 시도하거나, 모드팩 관리자 기능으로 수정하십시오.");
                    return;
                }
            }
            btn_Base_Upload.Enabled = false;
            gb_BasePack.Enabled = false;

            // 작성 시작
            ModAnalysisWrite MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.BasePack, txt_BUID.Text, null, null, txt_Base_Down.Text, null);
            string xmlPath = User.BSN_Temp + "BSU\\Data\\BasePack\\" + txt_BUID.Text + ".xml";
            MAW.WriteXML();

            // FTP서버에 정보 업로드
            FTPUtil FTP_Info = new FTPUtil(BellLib.Data.Base.SERVER_IP, BellLib.Data.Base.FTP_Info_ID, BellLib.Data.Base.FTP_Info_PW); // FTP 객체 생성
            FTP_Info.MakeDir("Base/" + txt_BUID.Text + "/Version/");
            FTP_Info.Upload("Base/" + txt_BUID.Text + "/", xmlPath, true); // 모드팩 데이터 업로드

            List<string> list = new List<string>();
            list.AddRange(MAR.GetList(ModAnalysisRead.PackType.Base));
            list.Add(txt_BUID.Text);
            MAW.WriteListXML(list.ToArray());
            xmlPath = User.BSN_Temp + "BSU\\Data\\PackList.xml";
            FTP_Info.Upload("Base/", xmlPath, true); // 모드팩 리스트 업로드

            InitializeBase(); // 다시한번 로드
            InitializeMod(); // 다시한번 로드
            btn_Base_Upload.Enabled = true;
            gb_BasePack.Enabled = true;
            Common.Message("베이스팩 등록 성공!");
        }

        private void btn_Option_Upload_Click(object sender, EventArgs e)
        {
            // 필드 검사
            if (txt_OUID.Text == string.Empty || txt_Option_Down.Text == string.Empty)
            {
                Common.Message("모든 필드에 값을 입력해 주세요.");
                return;
            }

            ModAnalysisRead MAR = new ModAnalysisRead();
            foreach (string tmp in MAR.GetList(ModAnalysisRead.PackType.Option))
            {
                if (txt_OUID.Text == tmp)
                {
                    Common.Message("이미 존재하는 OUID입니다." + Environment.NewLine + "다른 OUID 값으로 시도하거나, 모드팩 관리자 기능으로 수정하십시오.");
                    return;
                }
            }
            btn_Option_Upload.Enabled = false;
            gb_OptionPack.Enabled = false;

            // 작성 시작
            ModAnalysisWrite MAW = new ModAnalysisWrite(ModAnalysisWrite.Type.OptionPack, txt_OUID.Text, null, null, txt_Option_Down.Text, null);
            string xmlPath = User.BSN_Temp + "BSU\\Data\\OptionPack\\" + txt_OUID.Text + ".xml";
            MAW.WriteXML();

            // FTP서버에 정보 업로드
            FTPUtil FTP_Info = new FTPUtil(BellLib.Data.Base.SERVER_IP, BellLib.Data.Base.FTP_Info_ID, BellLib.Data.Base.FTP_Info_PW); // FTP 객체 생성
            FTP_Info.MakeDir("Option/" + txt_OUID.Text + "/Version/");
            FTP_Info.Upload("Option/" + txt_OUID.Text + "/", xmlPath, true); // 모드팩 데이터 업로드

            List<string> list = new List<string>();
            list.AddRange(MAR.GetList(ModAnalysisRead.PackType.Option));
            list.Add(txt_OUID.Text);
            MAW.WriteListXML(list.ToArray());
            xmlPath = User.BSN_Temp + "BSU\\Data\\PackList.xml";
            FTP_Info.Upload("Option/", xmlPath, true); // 모드팩 리스트 업로드

            InitializeOption(); // 다시한번 로드
            InitializeMod(); // 다시한번 로드
            btn_Option_Upload.Enabled = true;
            gb_OptionPack.Enabled = true;
            Common.Message("옵션팩 등록 성공!");
        }
    }
}