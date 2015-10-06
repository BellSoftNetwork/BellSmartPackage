using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BellLib.Class.BSN;
using BellLib.Data;
using BellLib.Class;

namespace Bell_Smart_Manager.Source.Frame.BSL
{
    /// <summary>
    /// BSL_PackUploader.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BSL_PackUploader : Window
    {
        public BSL_PackUploader()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            // 데이터 초기화
            cbUID.Items.Clear();

            // 데이터 기본값
            if (cbUID.Tag == null)
                cbUID.Tag = "ModPack";

            // 데이터 로드
            switch (cbUID.Tag.ToString())
            {
                case "ModPack":
                    foreach (string tmp in BSN_BSM.loadPackList(BSN_BSL.PACK.ModPack, User.BSN_member_srl))
                        cbUID.Items.Add(Common.getElement(tmp, "UID"));
                    break;

                case "BasePack":
                    foreach (string tmp in BSN_BSM.loadPackList(BSN_BSL.PACK.BasePack, User.BSN_member_srl))
                        cbUID.Items.Add(Common.getElement(tmp, "UID"));
                    break;

                case "Resource":
                    foreach (string tmp in BSN_BSM.loadPackList(BSN_BSL.PACK.Resource, User.BSN_member_srl))
                        cbUID.Items.Add(Common.getElement(tmp, "UID"));
                    break;
            }
            cbUID.SelectedIndex = 0;
        }

        private void rbModPack_Checked(object sender, RoutedEventArgs e)
        {
            if (!rbModPack.IsInitialized)
                return;
            cbUID.Tag = "ModPack";
            Initialize();
        }

        private void rbBasePack_Checked(object sender, RoutedEventArgs e)
        {
            if (!rbBasePack.IsInitialized)
                return;
            cbUID.Tag = "BasePack";
            Initialize();
        }

        private void rbResource_Checked(object sender, RoutedEventArgs e)
        {
            if (!rbResource.IsInitialized)
                return;
            cbUID.Tag = "Resource";
            Initialize();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            BSN_BSL.PACK pack = BSN_BSL.PACK.ModPack;
            if ((bool)rbBasePack.IsChecked)
                pack = BSN_BSL.PACK.BasePack;
            if ((bool)rbResource.IsChecked)
                pack = BSN_BSL.PACK.Resource;

            switch (pack)
            {
                case BSN_BSL.PACK.ModPack:

                    break;

                case BSN_BSL.PACK.BasePack:

                    break;

                case BSN_BSL.PACK.Resource:

                    break;
            }
        }
    }
}
