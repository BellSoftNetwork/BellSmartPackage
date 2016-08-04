﻿using Bell_Smart_Manager.Source.Frame;
using BellLib.Class;
using BellLib.Class.Control;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bell_Smart_Manager.Source.Frame
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Loader : Window
    {
        public Loader()
        {
            InitializeComponent();
            pbLoad.Maximum = 13;
            pbLoad.Value = 0;

            SetStatus("Initialize Component", 1);
        }

        private bool Initialize()
        {
            // 오토업데이트 시스템 실행
            SetStatus("매니저 자동업데이트 시스템 생성 시작", 1);
            Task upSys = new Task(UpdateSystem);
            SetStatus("매니저 자동업데이트 시스템 생성 완료", 1);

            SetStatus("매니저 자동업데이트 시스템 가동 시작", 1);
            upSys.Start();
            SetStatus("매니저 자동업데이트 시스템 가동 완료", 1);

            return true;
        }

        /// <summary>
        /// 업데이트 시스템을 비동기 시작합니다.
        /// </summary>
        private static void UpdateSystem()
        {
            if (UpdateControl.UpdateCheck())
            {
                Updater updater = new Updater();
                updater.Show();
            }
        }

        private void SetStatus(string value, double addProgress = 0)
        {
            lbStatus.Content = value;
            pbLoad.Value += addProgress;
            Common.DoEvents();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            SetStatus("로더 실행 완료", 1);

            SetStatus("서버 최신버전 체크", 1);
            if (UpdateControl.UpdateCheck(false))
            {
                SetStatus("업데이트 시작", 1);
                Updater updater = new Updater();
                updater.Show();

                this.Close();

                return;
            }

            SetStatus("로더 초기화 시작", 1);
            if (Initialize())
            {
                SetStatus("로더 초기화 완료", 1);
                
                SetStatus("회원 로그인창 생성중", 1);
                Login login = new Login(); // 추후 로그인창으로 수정
                SetStatus("회원 로그인창 생성완료", 1);

                SetStatus("로그인창 실행", 1);
                login.Show();
                SetStatus("로그인창 실행 완료", 1);
            }

            this.Close();
        }
    }
}
