using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace Bell_Smart_Tools.Source.BST
{
    public partial class BST_TicTacToe : MetroForm
    {
        private enum Error
        {
            ERROR_NONE = 0,
            ERROR_ALREADY_SELECTED,
            ERROR_GAME_END,
            ERROR_UNKNOWN
        }

        private bool _turn = false;
        private bool _errorProcessed = true;
        private Error _errorLast = Error.ERROR_NONE;
        private MetroButton[] buttons = new MetroButton[9];
        private Action<MetroButton, string> initAction =
            (param, param2) => { param.Text = param2; };

        public BST_TicTacToe()
        {
            InitializeComponent();

            /* Initialize Tic Button */
            TicButton1.Click += new EventHandler(this.TicButton_Click);
            TicButton2.Click += new EventHandler(this.TicButton_Click);
            TicButton3.Click += new EventHandler(this.TicButton_Click);
            TicButton4.Click += new EventHandler(this.TicButton_Click);
            TicButton5.Click += new EventHandler(this.TicButton_Click);
            TicButton6.Click += new EventHandler(this.TicButton_Click);
            TicButton7.Click += new EventHandler(this.TicButton_Click);
            TicButton8.Click += new EventHandler(this.TicButton_Click);
            TicButton9.Click += new EventHandler(this.TicButton_Click);
            buttons[0] = TicButton1;
            buttons[1] = TicButton2;
            buttons[2] = TicButton3;
            buttons[3] = TicButton4;
            buttons[4] = TicButton5;
            buttons[5] = TicButton6;
            buttons[6] = TicButton7;
            buttons[7] = TicButton8;
            buttons[8] = TicButton9;

            /* Initialize Info Button */
            InfoButton.Click += new EventHandler(this.InfoButton_Click);

            /* Initialize Reset Button */
            ResetButton.Click += new EventHandler(this.ResetButton_Click);
        }

        private void TicButton_Click(object sender, EventArgs e)
        {
            var btn = (MetroButton)sender;

            ProcessTurn(btn);

            if (CanContinueGame())
                _turn = !_turn;
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "Work in Progress" + Environment.NewLine +
                                                      "개발중인 기능입니다.");
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ActionAllButton(initAction);
        }

        private void ActionAllButton(Action<MetroButton, string> a)
        {
            for (int i = 0; i < 9; i++)
            {
                a(buttons[i], String.Empty);
            }
        }

        private string GetErrorMessage(Error e)
        {
            switch (e)
            {
                case Error.ERROR_NONE:
                    return null;
                case Error.ERROR_ALREADY_SELECTED:
                    return "이미 선택된 위치입니다.";
                case Error.ERROR_GAME_END:
                    return "이미 게임이 종료되었습니다.";
                case Error.ERROR_UNKNOWN:
                    return "알 수 없는 오류입니다.";
                default:
                    return "알 수 없는 오류입니다.";
            }
        }

        /// <summary>
        /// 게임 상태를 확인합니다.
        /// </summary>
        /// <returns>게임 진행 여부를 반환합니다. 게임을 진행해도 되면 true, 그렇지 않으면 false입니다.</returns>
        private bool CanContinueGame()
        {
            return true;
        }

        /// <summary>
        /// 턴을 처리합니다.
        /// </summary>
        /// <param name="btn">처리해야 할 MetroButtn 클래스입니다.</param>
        private void ProcessTurn(MetroButton btn)
        {
            if (_turn)
                btn.Text = "O";
            else
                btn.Text = "X";

            _turn = !_turn;
        }
    }
}
