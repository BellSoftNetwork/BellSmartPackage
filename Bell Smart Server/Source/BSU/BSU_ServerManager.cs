using BellLib.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Bell_Smart_Tools.Source.BSU
{
    public partial class BSU_ServerManager : Form
    {
        public BSU_ServerManager()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            // 초기화
            lst_Info_Servers.Items.Clear();
            txt_Info_Name.Text = null;
            txt_Info_URL.Text = null;

            lst_Cloud_Servers.Items.Clear();
            txt_Cloud_Name.Text = null;
            txt_Cloud_URL.Text = null;


            XmlDocument doc = new XmlDocument();
            XmlNodeList xnList;
            StringBuilder sb = new StringBuilder();
            try
            {
                doc.Load(Servers.Bell_Soft_Network.WEB_INFO_ROOT + "BSP/Servers.xml");
            }
            catch { return; }
            
            // 정보서버 정보 로드
            xnList = doc.SelectNodes("/Servers/Info/Server");
            foreach (XmlNode xn in xnList)
            {
                sb.Append(xn.InnerText + "|");
                lst_Info_Servers.Items.Add(xn.Attributes.GetNamedItem("Name").InnerText);
            }
            lst_Info_Servers.Tag = sb.ToString();

            // 클라우드서버 정보 로드
            sb.Clear();
            xnList = doc.SelectNodes("/Servers/Cloud/Server");
            foreach (XmlNode xn in xnList)
            {
                sb.Append(xn.InnerText + "|");
                lst_Cloud_Servers.Items.Add(xn.Attributes.GetNamedItem("Name").InnerText);
            }
            lst_Cloud_Servers.Tag = sb.ToString();
        }

        private void lst_Info_Servers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_Info_Name.Text = (string)lst_Info_Servers.SelectedItem;
                txt_Info_URL.Text = lst_Info_Servers.Tag.ToString().Split('|')[lst_Info_Servers.SelectedIndex];
            }
            catch
            {
                txt_Info_Name.Text = null;
                txt_Info_URL.Text = null;
            }
        }

        private void lst_Cloud_Servers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_Cloud_Name.Text = (string)lst_Cloud_Servers.SelectedItem;
                txt_Cloud_URL.Text = lst_Cloud_Servers.Tag.ToString().Split('|')[lst_Cloud_Servers.SelectedIndex];
            }
            catch
            {
                txt_Cloud_Name.Text = null;
                txt_Cloud_URL.Text = null;
            }
        }
    }
}
