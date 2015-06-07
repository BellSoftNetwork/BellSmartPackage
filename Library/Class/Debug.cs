using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Class
{
    class Debug
    {
        /*private static Level Mode = Level.Disable;
        public static string LogFile = Data.User.BSN_Path + "logs\\debug-" + "test.log";//Strings.Format(Now, "yyyy-MM-dd_hh.mm.ss") + ".log";
        public enum Level
        {
	        Disable,
	        //디버깅 사용 안함 (로그 작성안함)
	        Low,
	        //디버깅에 중요한 메시지만 출력 / 중대한 문제가 자주 발생하거나 이 부분때문에 프로그램의 흐름에 문제가 생길 가능성이 있는 부분은 Low로 설정
	        Middle,
	        //너무 자잘한 메시지는 제외 / 중요하지만 간혹 문제를 일으키는 부분은 Middle로 설정
	        High,
	        //모든 디버깅 메시지를 출력 / 중요하지 않은 모든 메시지는 전부 High 레벨로 설정, 잦은 루프를 도는 부분은 High 레벨로 설정
	        Log
	        //메시지는 출력하지 않고 로그파일만 작성 / 프로그램 사용중에 메시지를 확인할 필요는 없고, 매우 자주 루프를 도는 부분일 경우 Log 레벨로 설정
        }/*
        public void New()
        {
	        string strTemp = RegLoad("Debug");
	        try {
		        if (strTemp == Level.High | strTemp == Level.Middle | strTemp == Level.Low) {
			        Mode = strTemp;
		        } else {
			        Mode = Level.Disable;
		        }
	        } catch (Exception ex) {
		        Mode = Level.Disable;
	        }
        }
        public void Initialize()
        {
	        try {
		        //System.IO.File.Delete(DATA_USER.BSN_Path & "logs\Debug.log")
		        F_BASE.WriteTextFile(LogFile, "[" + Now + " - Initialize Bell Smart Debug Tools]" + Constants.vbCrLf, true);
	        } catch {
		        F_BASE.CreateDefaultForder();
	        }
        }
        public static Level Debugger {
	        get { return Mode; }
	        set {
		        RegSave("Debug", value);
		        Mode = value;
	        }
        }
        public static System.Windows.Forms.DialogResult Message(Level OutputLevel, string Text, string Caption = "Bell Smart Debug Tools", MessageBoxButtons Buttons = MessageBoxButtons.OK, MessageBoxIcon Icon = MessageBoxIcon.Information, MessageBoxDefaultButton DefaultButton = MessageBoxDefaultButton.Button1, string Info = "Debug")
        {
	        if (!(Mode == Level.Disable)) {
		        TxtWrite(Text, Info);
	        }
	        //디버그모드가 활성화 되어있을때
	        if (Mode >= OutputLevel & Mode < Level.Log) {
		        return MessageBox.Show(Text, Caption, Buttons, Icon, DefaultButton);
	        }

	        return DialogResult.None;
        }
        private static void TxtWrite(string strText, string Info = null)
        {
	        //string strTab = Environment.ta + Constants.vbTab + Constants.vbTab + Constants.vbTab + Constants.vbTab + Constants.vbTab + Constants.vbTab + Constants.vbTab;
	        string strTemp = "[" + Now;
	        if (!(Info == null)) {
		        strTemp += " " + Info;
	        }
	        strTemp += "] ";
	        F_BASE.WriteTextFile(LogFile, strTemp + strText.Replace(Constants.vbCrLf, Constants.vbCrLf + strTab).Replace("\\n", Constants.vbCrLf) + Constants.vbCrLf, true);
        }*/
    }
}
