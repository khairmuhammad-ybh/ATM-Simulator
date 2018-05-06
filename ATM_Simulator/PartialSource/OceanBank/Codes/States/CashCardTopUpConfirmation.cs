using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBank
{
    class CashCardTopUpConfirmation : State
    {
        private string acctNo;
        private double topupAmt;
        private bool isCardRemoved;
        private bool isCashcardInsert;

        Account account;

        public CashCardTopUpConfirmation(GUIforATM mainForm, string language, string acctNo, double topupAmt,bool isCardRemoved, bool isCashcardInsert) : base(mainForm, language)
        {
            this.acctNo = acctNo;
            this.topupAmt = topupAmt;
            this.isCardRemoved = isCardRemoved;
            this.isCashcardInsert = isCashcardInsert;

            if (language.Equals("MALAY"))
            {
                if (isCashcardInsert)
                {
                    bigDisplayLBL.Text = "Tambah Nilai $" + string.Format("{0:0.00}", topupAmt) + " dari akaun " + acctNo + " ke dalam Kad Tunai";
                    smallDisplayLBL.Text = "";
                    left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                    right1BTN.Text = "Setuju"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Tamat";
                }
                else
                {
                    bigDisplayLBL.Text = "KEROSAKKAN pada ATM\nAMARAN: Tiada Kad Tunai yang dimasukkan";
                    smallDisplayLBL.Text = "";
                    left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                    right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Tamat";
                }
            }
            else if (language.Equals("CHINESE"))
            {
                if (isCashcardInsert)
                {
                    bigDisplayLBL.Text = "充值 $" + string.Format("{0:0.00}", topupAmt) + " 从帐户 " + acctNo + " 到你的CashCard";
                    smallDisplayLBL.Text = "";
                    left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                    right1BTN.Text = "确认"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "终止";
                }
                else
                {
                    bigDisplayLBL.Text = "ATM错误\n错误: 没有插入卡";
                    smallDisplayLBL.Text = "";
                    left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                    right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "终止";
                }
            }
            else //ENGLISH
            {
                if (isCashcardInsert)
                {
                    bigDisplayLBL.Text = "Top Up $" + string.Format("{0:0.00}", topupAmt) + " from account " + acctNo + " to your cashcard";
                    smallDisplayLBL.Text = "";
                    left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                    right1BTN.Text = "Confirm"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Terminate";
                }
                else
                {
                    bigDisplayLBL.Text = "ERROR with ATM\nERROR: No Cashcard inserted";
                    smallDisplayLBL.Text = "";
                    left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                    right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Terminate";
                }
            }

            
            
        }

        public override State handleRight1BTNClick()
        {
            pauseforMilliseconds(1000);

            State nextStep = this;

            if (isCashcardInsert)
            {
                //get account & cashcard
                account = theCard.getAcctUsingAcctNo(acctNo);

                //Do deduction for topup
                account.withdraw(topupAmt);
                theCashcard.topUpCashcard(topupAmt);

                if (language.Equals("MALAY"))
                {
                    bigDisplayLBL.Text = "Baki Kad Tunai : $" + string.Format("{0:0.00}", theCashcard.getBalance()) + "\nTransaksi Berjaya";
                    bigDisplayLBL.Refresh();
                }
                else if (language.Equals("CHINESE"))
                {
                    bigDisplayLBL.Text = "现金卡余额: $" + string.Format("{0:0.00}", theCashcard.getBalance()) + "\n交易成功";
                    bigDisplayLBL.Refresh();
                }
                else //ENGLISH
                {
                    bigDisplayLBL.Text = "Cash card balance : $" + string.Format("{0:0.00}", theCashcard.getBalance()) + "\nTransaction Successful";
                    bigDisplayLBL.Refresh();
                }

                pauseforMilliseconds(3000);

                nextStep = new RemoveCashCardState(mainForm, language, "CASHCARD");
            }
            else
            {
                if (language.Equals("MALAY"))
                {
                    bigDisplayLBL.Text = "KEROSAKKAN pada ATM\nAMARAN: Tiada Kad Tunai yang dimasukkan";
                    bigDisplayLBL.Refresh();
                }
                else if (language.Equals("CHINESE"))
                {
                    bigDisplayLBL.Text = "ATM错误\n错误: 没有插入卡";
                    bigDisplayLBL.Refresh();
                }
                else //ENGLISH
                {
                    bigDisplayLBL.Text = "ERROR with ATM\nERROR: No Cashcard inserted";
                    bigDisplayLBL.Refresh();
                }
                
                pauseforMilliseconds(3000);
                if(isCardRemoved) // ATM card already removed
                    nextStep = new WaitForBankCardState(mainForm, language);
                else // ATM card not yet removed
                    nextStep = new RemoveCashCardState(mainForm, language, "ATM");
            }

            return nextStep;
        }

        public override State handleRight4BTNClick()
        {
            State nextStep = new RemoveCashCardState(mainForm, language, "CASHCARD");

            return nextStep;
        }
    }
}
