using BellLib.Data;
using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Bell_Smart_Launcher.Source.Frame
{
    /// <summary>
    /// Main 창의 Maps 탭 분할클래스 입니다.
    /// </summary>
    public partial class Main2
    {
        #region *** INITIALIZE ***

        /// <summary>
        /// 세팅탭 관련 기능을 사전 초기화합니다.
        /// </summary>
        public void PreInitSetting()
        {
            Setting_General_cbTheme.Items.Clear();
            Setting_General_cbAccent.Items.Clear();
            Setting_General_cbLanguage.Items.Clear();
        }

        /// <summary>
        /// 세팅탭 관련 기능을 초기화합니다.
        /// </summary>
        public void InitSetting()
        {
            InitTheme();
            InitAccent();
            InitLanguage();
        }

        /// <summary>
        /// 세팅탭 관련 기능을 최종 초기화합니다.
        /// </summary>
        public void PostInitSetting()
        {
            // 세팅값 로드해서 설정
        }

        /// <summary>
        /// 테마 리스트를 초기화합니다.
        /// </summary>
        private void InitTheme()
        {
            //string[] arrTheme = { "하얀색", "검은색" };

            foreach (string value in Basic.LST_THEME_KOR)
                Setting_General_cbTheme.Items.Add(value);
        }

        /// <summary>
        /// 강조색 리스트를 초기화합니다.
        /// </summary>
        private void InitAccent()
        {
            //string[] arrAccent = { "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
            //string[] arrAccent = { "빨강", "초록", "파랑", "보라", "주황", "라임", "에메랄드", "물오리", "하늘", "코발트", "남빛", "제비꽃", "분홍", "짙은 흥", "진홍", "호박", "노랑", "갈", "올리브", "강철", "자주 빛", "짙은 회갈", "시에나" };
            
            foreach (string value in Basic.LST_ACCENT_KOR)
                Setting_General_cbAccent.Items.Add(value);
        }

        private void InitLanguage()
        {
            string[] arrLanguage = { "한국어", "영어" };

            foreach (string value in arrLanguage)
                Setting_General_cbLanguage.Items.Add(value);
        }

        #endregion


        private void Setting_General_cbTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Setting_General_cbTheme.IsInitialized || Setting_General_cbTheme.SelectedIndex == -1)
                return;
            ////////////////// 클래스로 옮기기
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);

            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent(appStyle.Item2.Name),
                                        ThemeManager.GetAppTheme(Basic.LST_THEME[Setting_General_cbTheme.SelectedIndex])); // or appStyle.Item1
        }

        private void Setting_General_cbAccent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Setting_General_cbAccent.IsInitialized || Setting_General_cbAccent.SelectedIndex == -1)
                return;

            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);

            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent(Basic.LST_ACCENT[Setting_General_cbAccent.SelectedIndex]),
                                        ThemeManager.GetAppTheme(appStyle.Item1.Name)); // or appStyle.Item1
        }
    }
}
