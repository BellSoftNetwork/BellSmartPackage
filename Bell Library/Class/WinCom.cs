using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BellLib.Class
{
    /// <summary>
    /// WinForm 프로젝트 공통함수
    /// </summary>
    public class WinCom
    {
        static bool onExit = false;

        static OnEnd onEnd;

        /// <summary>
        /// 메시지 박스를 띄웁니다.
        /// </summary>
        /// <param name="Text">메시지박스 내용</param>
        /// <param name="Caption">메시지박스 제목</param>
        /// <param name="buttons">메시지박스 버튼</param>
        /// <param name="icon">메시지박스 아이콘</param>
        /// <param name="defaultButton">메시지박스 기본 버튼</param>
        /// <returns>선택한 버튼값</returns>
        public static DialogResult Message(string Text, Base.PROJECT project, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            string title = "Bell Smart Package";

            switch (project)
            {
                case Base.PROJECT.Bell_Smart_Launcher:
                    title = "Bell Smart Launcher";
                    break;

                case Base.PROJECT.Bell_Smart_Manager:
                    title = "Bell Smart Manager";
                    break;

                case Base.PROJECT.Bell_Smart_Server:
                    title = "Bell Smart Server";
                    break;

                case Base.PROJECT.Bell_Smart_Tools:
                    title = "Bell Smart Tools";
                    break;
            }

            Debug.Message(Debug.Level.Log, Text, title, buttons, icon, defaultButton, "MessageBox");
            return MessageBox.Show(Text, title, buttons, icon, defaultButton);
        }

        /// <summary>
        /// 메시지 박스를 띄웁니다.
        /// </summary>
        /// <param name="Text">메시지박스 내용</param>
        /// <param name="Caption">메시지박스 제목</param>
        /// <param name="buttons">메시지박스 버튼</param>
        /// <param name="icon">메시지박스 아이콘</param>
        /// <param name="defaultButton">메시지박스 기본 버튼</param>
        /// <returns>선택한 버튼값</returns>
        [Obsolete]
        public static DialogResult Message(string Text, string Caption = "Bell Smart Package", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            Debug.Message(Debug.Level.Log, Text, Caption, buttons, icon, defaultButton, "MessageBox");
            return MessageBox.Show(Text, Caption, buttons, icon, defaultButton);
        }
               
        /// <summary>
        /// 프로그램을 종료합니다.
        /// </summary>
        /// <param name="Restart">프로그램 재시작 여부</param>
        public static void End(bool Restart = false)
        {
            // 콜백에 프로그램 종료를 알린다.
            onEnd += new OnEnd(() =>
            {
                if (!onExit)
                    Debug.Message(Debug.Level.Log, "End point of program. Exiting.");
            });

            // 콜백이 한번만 실행되도록 한다.
            onEnd += new OnEnd(() => onExit = true);

            // 콜백에 프로그램 종료를 체인한다.
            if (Restart)
                onEnd += Application.Restart;
            else
                onEnd += Application.Exit;

            // 콜백을 호출한다.
            onEnd();
        }

        /// <summary>
        /// 해당 폼 이름으로 폼이 실행되어있는지 여부를 판단합니다.
        /// 폼이 실행되어있을경우, 폼을 활성화 시킵니다.
        /// </summary>
        /// <param name="formName">폼 이름 (대소문자 구별)</param>
        /// <returns>폼 실행 가능 여부</returns>
        public static bool Feasibility(string formName)
        {
            Form frm = GetForm(formName);
            if (frm == null)
                return true; // 폼이 실행되어 있지 않을경우 폼 실행 가능

            frm.Activate();
            return false;
        }

        /// <summary>
        /// 해당 폼 이름으로 폼이 실행되어 있으면, 해당 폼의 Form 값을 반환합니다.
        /// </summary>
        /// <param name="formName">폼 이름 (대소문자 구별)</param>
        /// <returns>Form 정보</returns>
        public static Form GetForm(string formName)
        {
            foreach (Form frm in Application.OpenForms)
                if (frm.Name == formName)
                    return frm;

            return null;
        }
    }
}
