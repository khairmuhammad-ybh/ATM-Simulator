using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OceanBank
{
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
        }

        private void okBTN_Click(object sender, EventArgs e)
        {
            Card theCard = new Card(pinTB.Text,NRICorFIN.Text);
            Account a;
            double bal;

            //Cashcard
            Cashcard theCashcard;

            if (!cashcardBal.Text.Equals(""))
                theCashcard = new Cashcard(cashcardBal.Text);
            else
                theCashcard = new Cashcard("0");


            if (acct1NoTB.Text != "")
            {
                bal = Convert.ToDouble(acct1BalTB.Text);
                a = new Account(acct1NoTB.Text, bal);
                theCard.addAccount(a);
            }
           
            if (acct2NoTB.Text != "")
            {
                bal = Convert.ToDouble(acct2BalTB.Text);
                a = new Account(acct2NoTB.Text, bal);
                theCard.addAccount(a);
            }

            if (acct3NoTB.Text != "")
            {
                bal = Convert.ToDouble(acct3BalTB.Text);
                a = new Account(acct3NoTB.Text, bal);
                theCard.addAccount(a);
            }

            //this.Hide();
            (new GUIforATM(theCard, theCashcard)).Show();

        }

        private void Setup_Load(object sender, EventArgs e)
        {

        }
    }
}
