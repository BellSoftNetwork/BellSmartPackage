﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using BellLib.Class;
using RegKind = Microsoft.Win32.RegistryValueKind;
using DialogResult = System.Windows.Forms.DialogResult;

namespace BellLib.Class
{
    [Serializable]
    public class Debug
    {
        private static Level Mode;
        public static string LogFile = Data.User.BSN_Path + "logs\\debug-" + DateTime.Now.ToString("yyyy-MM-dd_hh.mm.ss") +".log"; // string.Format("yyyy-MM-dd_hh.mm.ss", DateTime.Now)

        public enum Level : int
        {
	        Disable = 0, // 디버깅 사용 안함 (로그 작성안함)
	        Low, // 디버깅에 중요한 메시지만 출력 / 중대한 문제가 자주 발생하거나 이 부분때문에 프로그램의 흐름에 문제가 생길 가능성이 있는 부분은 Low로 설정
	        Middle, // 너무 자잘한 메시지는 제외 / 중요하지만 간혹 문제를 일으키는 부분은 Middle로 설정
	        High, // 모든 디버깅 메시지를 출력 / 중요하지 않은 모든 메시지는 전부 High 레벨로 설정, 잦은 루프를 도는 부분은 High 레벨로 설정
	        Log // 메시지는 출력하지 않고 로그파일만 작성 / 프로그램 사용중에 메시지를 확인할 필요는 없고, 매우 자주 루프를 도는 부분일 경우 Log 레벨로 설정
        }

        public void Initialize()
        {
            RegistryReader rReader = new RegistryReader("DebugMode", RegKind.Binary);

            var obj = (byte[])rReader.GetValue();

            try
            {
                using (MemoryStream ms = new MemoryStream(obj))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    Mode = (Level)bf.Deserialize(ms);
                }
            }
            catch (ArgumentException)
            {
                Mode = Level.Disable;
            }

	        try {
		        //System.IO.File.Delete(DATA_USER.BSN_Path & "logs\Debug.log")
		        Common.WriteTextFile(LogFile, "[" + DateTime.Now + " - Initialize Bell Smart Debug Tools]" + Environment.NewLine, true);
	        } catch {
                Common.CreateDefaultForder();
	        }
        }
        public static Level Debugger {
	        get { return Mode; }
	        set {
		        /*Common.RegSave("Debug", value);*/
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, Mode);
                    byte[] data = ms.ToArray();
                    using (RegistryManager rm = new RegistryManager("DebugMode", data, RegKind.Binary))
                        rm.SetValue();
                }
		        Mode = value;
	        }
        }
        public static DialogResult Message(Level OutputLevel, string Text, string Caption = "Bell Smart Debug Tools",
            MessageBoxButtons Buttons = MessageBoxButtons.OK, MessageBoxIcon Icon = MessageBoxIcon.Information,
            MessageBoxDefaultButton DefaultButton = MessageBoxDefaultButton.Button1, string Info = "Debug")
        {
	        if (Mode != Level.Disable) {
		        TxtWrite(Text, Info);
	        }
	        //디버그모드가 활성화 되어있을때
	        if (Mode >= OutputLevel && Mode < Level.Log) {
		        return MessageBox.Show(Text, Caption, Buttons, Icon, DefaultButton);
	        }

	        return DialogResult.None;
        }
        private static void TxtWrite(string strText, string Info = null)
        {
	        //string strTab = Environment.ta + Constants.vbTab + Constants.vbTab + Constants.vbTab + Constants.vbTab + Constants.vbTab + Constants.vbTab + Constants.vbTab;
	        string strTemp = "[" + DateTime.Now;
	        if (!(Info == null)) {
		        strTemp += " " + Info;
	        }
	        strTemp += "] ";
            Common.WriteTextFile(LogFile, strTemp + strText.Replace(Environment.NewLine, Environment.NewLine + "\t").Replace("\\n", Environment.NewLine) + Environment.NewLine, true);
        }
    }
}
