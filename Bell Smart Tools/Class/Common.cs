﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Microsoft.Win32;

namespace Bell_Smart_Tools.Class
{
    delegate void OnStart();
    delegate void OnEnd();

    class Common
    {
        public static DialogResult Message(string Text, string Caption = "Bell Smart Tools", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return MessageBox.Show(Text, Caption, buttons, icon, defaultButton);
        }

        public static void Start()
        {
            Program.onStart();
        }

        public static void End()
        {
            Program.onEnd();
            Application.Exit();
        }

        public static void RegSave(string name, object value)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("BSN");
            reg.SetValue(name, value, RegistryValueKind.String);
        }

        public static string RegLoad(string name)
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("BSN");
                return (string)reg.GetValue(name, null);
            } catch { }

            return null;
        }

        public static void RegDelete(string name)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("BSN");
            reg.DeleteValue(name);
        }

        public static void WriteTextFile(string LocalFilePath, string Data, bool Append = false)
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

        public static string GetStringFromWeb(string URL)
        {
            try
            {
                return new WebClient().DownloadString(URL);
            }
            catch
            {
                return null;
            }
        }
    }
}
