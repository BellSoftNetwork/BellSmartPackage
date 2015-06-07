using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BellLib.Class;

namespace Bell_Smart_Package.Source.BSP
{
    public partial class BSP_Updater : Form
    {
        public BSP_Updater()
        {
            InitializeComponent();
        }

        private void BSP_Updater_Load(object sender, EventArgs e)
        {

        }
        private void InstallUpdateSyncWithInfo()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        DialogResult dr = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel);
                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        MessageBox.Show("This application has detected a mandatory update from your current " +
                            "version to version " + info.MinimumRequiredVersion.ToString() +
                            ". The application will now install the update and restart.",
                            "Update Available", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            ad.Update();
                            MessageBox.Show("The application has been upgraded, and will now restart.");
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                            return;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
        }






        public void BST_Update()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                this.Visible = true;
                this.ShowInTaskbar = true;
                ApplicationDeployment AD = ApplicationDeployment.CurrentDeployment;
                
                try
                {
                    Debug.Message(Debug.Level.High, "Start Update");
                    //BST_Main.Hide(); // 업데이트중이므로 폼 하이드
                    AD.UpdateAsync();
                    //AD.Update()
                    Debug.Message(Debug.Level.High, "Update Async Start");
                }
                catch // (DeploymentDownloadException dde)
                {
                    Common.Message("Cannot install the latest version of the application. " + Environment.NewLine + Environment.NewLine + "Please check your network connection, or try again later.");
                }
            }
            else
            {
                Debug.Message(Debug.Level.High, "BST_Update" + Environment.NewLine + "This program is not deployed.");
            }
        }

        public bool Initialize()
        {
            //최신버전 확인 후 업데이트 가능시 버전로더 실행하지 않고 바로 업데이트 후 종료
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                //Application.Deployment.UpdateProgressChanged += this.ProgressChanged;
                //Application.Deployment.UpdateCompleted += this.UpdateCompleted;
            }
            return true;
            //초기화 성공. 가던길 가시오.
        }


        private void UpdateCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Debug.Message(Debug.Level.Middle, "UpdateCompleted");
            Common.End(true);
        }
        private void ProgressChanged(object sender, System.Deployment.Application.DeploymentProgressChangedEventArgs e)
        {
            lb_Info.Text = e.ProgressPercentage + "%";
            pb_Down.Maximum = (int)e.BytesTotal;
            pb_Down.Value = (int)e.BytesCompleted;
            Application.DoEvents();
        }

    }
}
