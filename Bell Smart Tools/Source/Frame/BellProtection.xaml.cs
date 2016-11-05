using BellLib.Class;
using BellLib.Class.Protection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bell_Smart_Tools.Source.Frame
{
    /// <summary>
    /// BellProtection.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BellProtection : Window
    {
        public BellProtection()
        {
            InitializeComponent();
        }

        private void txtProtect_DragEnter(object sender, DragEventArgs e)
        {
            
        }

        private void txtProtect_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effects = DragDropEffects.All;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                if (files[0].Contains(".bdx"))
                {
                    txtProtect.Text = string.Empty;

                    foreach (string value in Protect.ReadBDXFile(files[0]))
                        txtProtect.Text += value + Environment.NewLine;
                }
                else if (files[0].Contains(".bd"))
                {
                    txtProtect.Text = string.Empty;

                    foreach (char value in Protect.ReadBDFile(files[0]))
                        txtProtect.Text += value + Environment.NewLine;
                }
                else
                {
                    WPFCom.Message("암호화 규칙에 어긋나는 파일입니다.", BellLib.Data.Base.PROJECT.Bell_Smart_Tools);
                }
            }
        }

        private void txtProtect_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }
    }
}
