using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Microsoft.Win32;

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

        public static void End()
        {
            Application.Exit();
        }

        private RegistryKey reg = Registry.LocalMachine.CreateSubKey("Software").CreateSubKey("BSN");
        public void RegSave(string name, object value)
        {
            reg.SetValue(name, value);
        }
        public object RegLoad(string name)
        {
            return reg.GetValue(name, null);
        }
        public void RegDelete(string name)
        {
            reg.DeleteValue(name);
        }

        public void WriteTextFile(string LocalFilePath, string Data, bool Append = false)
        {
            bool boolWritten = false;

            while (!boolWritten)
            {
                try
                {
                    if (Append)
                    {
                        File.AppendAllText(LocalFilePath, Data);
                    }
                    else
                    {
                        File.WriteAllText(LocalFilePath, Data);
                    }
                    boolWritten = true;
                }
                catch (Exception ex)
                {
                    string C_Bal = ex.Message;
                    //Debug.Message(Debug.Level.High, "WriteTextFile" + Environment.NewLine + ex.Message);
                }
            }
        }


        public static string WebHTML(string URL)
        {
            WebClient WC = new WebClient();
            string Temp = null;

            try
            {
                Temp = WC.DownloadString(URL);
            }
            catch
            {
            }
            return Temp;
        }
    }
}
