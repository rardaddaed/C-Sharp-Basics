using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        bool turn = true; // true = X turn; False =  Y turn
        int turn_count = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By JL");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (turn)
            {
                b.Text = "X";
            }
            else
            {
                b.Text = "O";
            }
            turn = !turn;
            b.Enabled = false;
            checkWinner();
        }

        private void checkWinner()
        {
            bool win = false;
            //horizontal checks
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled))
                win = true;
            if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled))
                win = true;
            if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled))
                win = true;

            if (win)
            {
                String winner = "";
                if (turn)
                    winner = "O";
                else
                    winner = "X";
                MessageBox.Show(winner + " Wins!");
            }
        }
    }
}
