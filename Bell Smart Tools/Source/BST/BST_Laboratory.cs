using System;
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
using BellLib.Data;
using System.IO;
using System.Collections.Specialized;
using System.Globalization;

namespace Bell_Smart_Tools.Source.BSS
{
    public partial class BST_Laboratory : Form
    {
        public BST_Laboratory()
        {
            InitializeComponent();
        }

        private void Button_TicTacToe_Click(object sender, EventArgs e)
        {
            ReflectionExample.Example();
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

        private void btn_DataUD_Click(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            NameValueCollection formData = new NameValueCollection();
            string[] data = txtPOST.Text.Split('\n');
            foreach (string tmp in txtPOST.Lines)
            {
                string[] value = tmp.Split('=');
                formData[value[0]] = value[1];
            }

            byte[] responseBytes = webClient.UploadValues(txtURL.Text, "POST", formData);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);
            WinCom.Message(responsefromserver, Basic.PROJECT.Bell_Smart_Tools);
            webClient.Dispose();
        }

        private void btnUploadOK_Click(object sender, EventArgs e)
        {
            /*string siteURL = "http://info.softbell.net/BSL/upload.php";
            WebClient client = new WebClient();
 
            string fileName = @"M:\Test.jar";
            //Console.WriteLine("Uploading {0} to {1} ...", fileName, siteURL);
 
            // File Uploaded using PUT method
            NameValueCollection formData = new NameValueCollection();
            formData["list"] = "modpack";
            
            byte[] responseBytes = client.UploadValues(siteURL, "POST", formData);
            byte[] responseArray = client.UploadFile(siteURL, "POST", fileName);
            string responsefromserver = Encoding.UTF8.GetString(responseArray);
            WinCom.Message(responsefromserver);
            client.Dispose();*/
            string dumpPath = @"M:\Test.jar";
            string address = "http://info.softbell.net/BSL/upload.php";

            using (var stream = File.Open(dumpPath, FileMode.Open))
            {
                var files = new[]
                {
                    new UploadFile
                    {
                        Name = "file",
                        Filename = Path.GetFileName(dumpPath),
                        ContentType = "text/plain",
                        Stream = stream
                    }
                };

                    var values = new NameValueCollection
                {
                    { "client", "VIP" },
                    { "name", "John Doe" },
                };

                byte[] result = UploadFiles(address, files, values);
                string responsefromserver = Encoding.UTF8.GetString(result);
                WinCom.Message(responsefromserver, Basic.PROJECT.Bell_Smart_Tools);
            }
        }
        public byte[] UploadFiles(string address, IEnumerable<UploadFile> files, NameValueCollection values)
        {
            var request = WebRequest.Create(address);
            request.Method = "POST";
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            boundary = "--" + boundary;

            using (var requestStream = request.GetRequestStream())
            {
                // Write the values
                foreach (string name in values.Keys)
                {
                    var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(values[name] + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                // Write the files
                foreach (var file in files)
                {
                    var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", file.Name, file.Filename, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    file.Stream.CopyTo(requestStream);
                    buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                var boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
                requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
            }

            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var stream = new MemoryStream())
            {
                responseStream.CopyTo(stream);
                return stream.ToArray();
            }
        }
    }
}
public class UploadFile
{
    public UploadFile()
    {
        ContentType = "application/octet-stream";
    }
    public string Name { get; set; }
    public string Filename { get; set; }
    public string ContentType { get; set; }
    public Stream Stream { get; set; }
}