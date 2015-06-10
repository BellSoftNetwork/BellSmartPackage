﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Net;
using BellLib.Class;

namespace Bell_Smart_Tools.Source.BST
{
    public partial class BST_Laboratory : Form
    {
        public BST_Laboratory()
        {
            InitializeComponent();
        }

        private void Button_TicTacToe_Click(object sender, EventArgs e)
        {
            //var game = new BST_TicTacToe();
            //game.Show();
            ReflectionExample.Example();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument XD = new XmlDocument();
            XD.LoadXml(BellLib.Properties.Resources.BellCraft8);
            //XmlNodeReader XNR = new XmlNodeReader(XD);
            XmlNode root = XD;
            textBox2.Text = null;

            XmlNodeList xnList = XD.SelectNodes("/BellCraft8/Version/Ver");
            foreach (XmlNode xn in xnList)
            {
                //textBox2.Text += xn.Name + " = " + xn.InnerText + Environment.NewLine;
                textBox2.Text += xn.InnerText + Environment.NewLine;
                //string firstName = xn["FirstName"].InnerText;
                //string lastName = xn["LastName"].InnerText;
            }
            

            /*if (root.HasChildNodes)
            {
                foreach (XmlNode no in root.ChildNodes)
                {
                    if (no.Name == "BellCraft8")
                    {
                        foreach (XmlNode child in no)
                        {
                            if (child.Name == "Info")
                            {
                                foreach (XmlNode ch in child)
                                {
                                    switch (ch.Name)
                                    {
                                        case "Name":
                                            textBox2.Text += ch.InnerText + Environment.NewLine;
                                            break;

                                        case "Recommended":
                                            textBox2.Text += ch.InnerText + Environment.NewLine;
                                            break;

                                        case "Latest":
                                            textBox2.Text += ch.InnerText + Environment.NewLine;
                                            break;

                                        case "Base":
                                            textBox2.Text += ch.InnerText + Environment.NewLine;
                                            break;

                                        case "Option":
                                            textBox2.Text += ch.InnerText + Environment.NewLine;
                                            break;

                                        case "News":
                                            textBox2.Text += ch.InnerText + Environment.NewLine;
                                            break;

                                        case "Down":
                                            textBox2.Text += ch.InnerText + Environment.NewLine;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }*/
                /*for (int i = 0; i < childnode.Count; i++)
                {
                    XmlNode child = childnode[i];
                    textBox2.Text += child.OuterXml + "의 값 : " + child.InnerText + Environment.NewLine;
                }*/
            }

        private void button2_Click(object sender, EventArgs e)
        {
            ModAnalysisRead MAR = new ModAnalysisRead("BellCraft8");
            textBox2.Text = MAR.GetModInfo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SocketServer SS = new SocketServer();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SocketClient SC = new SocketClient("127.0.0.1");
            SC.Send(textBox1.Text);
            textBox1.Text = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SocketClient SC = new SocketClient("127.0.0.1");
            SC.Send(textBox3.Text);
            textBox3.Text = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Protection pt = new Protection();
            textBox5.Text = pt.Base64(textBox4.Text, Protection.ProtectionType.PROTECTION_ENCODE);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Protection pt = new Protection();
            textBox4.Text = pt.Base64(textBox5.Text, Protection.ProtectionType.PROTECTION_DECODE);
        }
    }
}
