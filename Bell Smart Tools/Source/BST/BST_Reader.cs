using BellLib.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Server.Source.BST
{
    public partial class BST_Reader : Form
    {
        public BST_Reader(string FilePath = null)
        {
            InitializeComponent();
            if (FilePath == null)
                return;
            DataAnalysis(FilePath);
        }

        private void DataAnalysis(string FilePath) {
            string[] Path = FilePath.Split('.');
            switch (Path[Path.Length - 1])
            {
                case "bdx":
                    string[] Data = Common.ReadBDXFile(FilePath);
                    StringBuilder sb = new StringBuilder();
                    foreach (string tmp in Data)
                    {
                        sb.AppendLine(tmp.Replace('|', '='));
                    }
                    txt_Content.Text = sb.ToString();
                    break;

                case "bd":
                    txt_Content.Text = Common.ReadBDFile(FilePath);
                    break;

                default:
                    try
                    {
                        txt_Content.Text = File.ReadAllText(FilePath);
                    }
                    catch (Exception ex)
                    {
                        txt_Content.Text = "ERROR" + Environment.NewLine + ex.Message;
                    }
                    break;
            }
        }

        private void txt_Content_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            DataAnalysis(files[0]);
        }

        private void txt_Content_DragOver(object sender, DragEventArgs e)
        {
            //드레그하는 개체가 파일이면
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //마우스 커서를 Copy모양으로 바꿔준다.
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                //아닐경우 커서의 모양을 θ 요런 모양으로 바꾼다.
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
