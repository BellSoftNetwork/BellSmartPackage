using BellLib.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bell_Smart_Package.Source.BSP
{
    public partial class BSP_Installer : Form
    {
        public BSP_Installer()
        {
            InitializeComponent();
        }

        private void btn_Manager_Click(object sender, EventArgs e)
        {
            //ClickOnce 어플리케이션인지를 확인합니다. 
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                //현재 배포 객체에 대한 참조를 구합니다. 
                ApplicationDeployment current = ApplicationDeployment.CurrentDeployment;
                //원하는 그룹의 파일들이 아직 다운로드되지 않았는지를 확인합니다. 
                if (!current.IsFileGroupDownloaded("Manager"))
                {
                    //1. 동기적으로 파일을 다운로드하는 코드입니다. 
                    //1.1 아직 다운로드 되지 않았다면 파일을 먼저 다운로드 합니다. 
                    current.DownloadFileGroup("Manager");
                    Common.Message("다운로드 완료");
                }
                else
                {
                    //해당 그룹을 이미 다운로드했다면 
                    Common.Message("이미 다운로드 되어있습니다.");
                }
            }
        }
    }
}
