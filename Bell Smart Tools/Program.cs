﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Bell_Smart_Tools
{
    static class Program
    {
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