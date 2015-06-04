using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Tools.Class
{
    class Common
    {
        public static void Message(string Text)
        {
            MessageBox.Show(Text, "Bell Smart Tools");
        }
        public static void Message(string Text, string Caption)
        {
            MessageBox.Show(Text, Caption);
        }
    }
}
