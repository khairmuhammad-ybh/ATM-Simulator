using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBank
{
    class CreditPaymentConfirmation : State
    {
        private string acctNo;
        private string cardNo;
        private double payAmt;

        Account payFromAcct;

        public CreditPaymentConfirmation(GUIforATM mainForm, string language, string acctNo, string cardNo, double payAmt) : base(mainForm, language)
        {
            this.acctNo = acctNo;
            this.cardNo = cardNo;
            this.payAmt = payAmt;

            payFromAcct = theCard.getAcctUsingAcctNo(acctNo);

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "DARI nombor akaun: " + acctNo + "\n KE nombor kad kredit: " + cardNo + "\n Jumlah Bayaran: $" + string.Format("{0:0.00}", payAmt);
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Tukar Jumlah"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "从帐户: " + acctNo + "\n 到信用卡: " + cardNo + "\n 支付的金额: $" + string.Format("{0:0.00}", payAmt);
                smallDisplayLBL.Text = "";
                left1BTN.Text = "编辑金额支付"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";
            }
            else //ENGLISH
            {
                bigDisplayLBL.Text = "FROM Account No: " + acctNo + "\n TO Credit card no: " + cardNo + "\n Amount Paid: $" + string.Format("{0:0.00}", payAmt);
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Edit amount to pay"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";
            }

            
        }

        public override State handleRight1BTNClick()
        {
            //Pay to credit card
            //Deduct amount from account

            payFromAcct.deductToCreditTransferLimit(payAmt);
            payFromAcct.withdraw(payAmt);

            //State nextStep = new PayToCreditCard(mainForm, language, acctNo);
            //return nextStep;

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Transaksi berjaya\nBaki akaun: $" + string.Format("{0:0.00}", payFromAcct.getBalance());
                left1BTN.Text = "";
                smallDisplayLBL.Text = "";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "交易成功\n账户余额: $" + string.Format("{0:0.00}", payFromAcct.getBalance());
                left1BTN.Text = "";
                smallDisplayLBL.Text = "";
            }
            else //ENGLISH
            {
                bigDisplayLBL.Text = "Transaction successful\nAccount balance: $" + string.Format("{0:0.00}", payFromAcct.getBalance());
                left1BTN.Text = "";
                smallDisplayLBL.Text = "";
            }

            

            pauseforMilliseconds(3000);

            State nextStep = new DisplayMainMenuState(mainForm, language);
            return nextStep;
        }

        public override State handleLeft1BTNClick()
        {
            State nextStep = new EnterAmtToPayCreditCard(mainForm, language, acctNo, cardNo);
            return nextStep;
        }

        public override State handleRight4BTNClick()
        {
            State nextStep = new DisplayMainMenuState(mainForm, language);
            return nextStep;
        }
    }
}
