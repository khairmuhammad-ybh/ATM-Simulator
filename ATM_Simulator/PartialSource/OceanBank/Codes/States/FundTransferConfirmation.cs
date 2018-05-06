using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBank
{
    class FundTransferConfirmation : State
    {
        private string acctNo;
        private string cardNo;
        private double transferAmt;

        Account transferFromAcct;

        public FundTransferConfirmation(GUIforATM mainForm, string language, string acctNo, string cardNo, double transferAmt) : base(mainForm, language)
        {
            this.acctNo = acctNo;
            this.cardNo = cardNo;
            this.transferAmt = transferAmt;

            transferFromAcct = theCard.getAcctUsingAcctNo(acctNo);

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Dari nombor akaun: " + acctNo + "\nKe nombor akaun: " + cardNo + "\nJumlah pemindahan: $" + string.Format("{0:0.00}", transferAmt);
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Tukar jumlah pemindahan"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu Utama";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "从帐户: " + acctNo + "\n到帐户: " + cardNo + "\n转账金额: $" + string.Format("{0:0.00}", transferAmt);
                smallDisplayLBL.Text = "";
                left1BTN.Text = "编辑转帐金额"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";
            }
            else
            {
                bigDisplayLBL.Text = "FROM Account No: " + acctNo + "\nTo account no: " + cardNo + "\nAmount Transfer: $" + string.Format("{0:0.00}", transferAmt);
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Edit amount to transfer"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";
            }

            
        }

        public override State handleRight1BTNClick()
        {
            //Transfer to other account
            //Deduct amount from account

            transferFromAcct.deductToFundTransferLimit(transferAmt);
            transferFromAcct.withdraw(transferAmt);

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Jumlah baki di dalam akaun : $" + string.Format("{0:0.00}", transferFromAcct.getBalance()) + "\nTransaksi berjaya";
                left1BTN.Text = "";
                smallDisplayLBL.Text = "";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "账户余额 : $" + string.Format("{0:0.00}", transferFromAcct.getBalance()) + "\n交易成功";
                left1BTN.Text = "";
                smallDisplayLBL.Text = "";
            }
            else
            {
                bigDisplayLBL.Text = "Account balance : $" + string.Format("{0:0.00}", transferFromAcct.getBalance()) + "\nTransaction successful";
                left1BTN.Text = "";
                smallDisplayLBL.Text = "";
            }

            pauseforMilliseconds(3000);

            State nextStep = new DisplayMainMenuState(mainForm, language);
            return nextStep;
        }

        public override State handleLeft1BTNClick()
        {
            State nextStep = new EnterAmtToFundTransfer(mainForm, language, acctNo, cardNo);
            return nextStep;
        }

        public override State handleRight4BTNClick()
        {
            State nextStep = new DisplayMainMenuState(mainForm, language);
            return nextStep;
        }
    }
}
