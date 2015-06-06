using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Bell_Smart_Tools.Class;

namespace Bell_Smart_Tools
{
    static class Program
    {
        public static OnEnd onEnd;

        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new BST_Main());
            Application.Run(new Source.BST.BST_Loader());
        }
    }
}
