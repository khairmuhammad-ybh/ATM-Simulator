using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OceanBank
{
    public partial class CashSelector : Form
    {

        private CashState currentState;
        string lang;

        public CashSelector(string language, Cash selectedCash)
        {
            InitializeComponent();
            lang = language;

            currentState = new CashSelectorState(this, language, selectedCash);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            currentState = currentState.handlePicBox1Click();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            currentState = currentState.handlePicBox2Click();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            currentState = currentState.handlePicBox3Click();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            currentState = currentState.handlePicBox4Click();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            currentState = currentState.handlePicBox5Click();
        }

        private void cashOkClick(object sender, EventArgs e)
        {

            currentState = currentState.handleCashOkClick(lang);
            //Create new class for storing notes' quantity

            //this.Close();
        }

        private void c2CntM_Click(object sender, EventArgs e)
        {
            currentState = currentState.handleC2CntMClick();
        }

        private void c2CntP_Click(object sender, EventArgs e)
        {
            currentState = currentState.handleC2CntPClick();
        }

        private void c5CntM_Click(object sender, EventArgs e)
        {
            currentState = currentState.handleC5CntMClick();
        }

        private void c5CntP_Click(object sender, EventArgs e)
        {
            currentState = currentState.handleC5CntPClick();
        }

        private void c10CntM_Click(object sender, EventArgs e)
        {
            currentState = currentState.handleC10CntMClick();
        }

        private void c10CntP_Click(object sender, EventArgs e)
        {
            currentState = currentState.handleC10CntPClick();
        }

        private void c50CntM_Click(object sender, EventArgs e)
        {
            currentState = currentState.handleC50CntMClick();
        }

        private void c50CntP_Click(object sender, EventArgs e)
        {
            currentState = currentState.handleC50CntPClick();
        }

        private void c100CntM_Click(object sender, EventArgs e)
        {
            currentState = currentState.handleC100CntMClick();
        }

        private void c100CntP_Click(object sender, EventArgs e)
        {
            currentState = currentState.handleC100CntPClick();
        }
    }
}
