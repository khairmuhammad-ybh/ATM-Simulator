using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;

namespace OceanBank
{
    class ShowNewBalance : State
    {
        //Timer
        Timer aTimer;
        int counter = 30;
        bool session = true;

        public ShowNewBalance(GUIforATM mainForm, string language, string acctNo) : base(mainForm, language)
        {
            Account depositToAcct;

            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
            aTimer.SynchronizingObject = mainForm;
            aTimer.Interval = 1000;
            aTimer.Start();

            depositToAcct = theCard.getAcctUsingAcctNo(acctNo);

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Keseluruhan baki baru dalam " + acctNo + "\n$" + string.Format("{0:0.00}", depositToAcct.getBalance());
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = "Transaksi lain"; right4BTN.Text = "Tamat transaksi";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "此帐户中的新帐户余额 " + acctNo + "\n$" + string.Format("{0:0.00}", depositToAcct.getBalance());
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = "另一个交易"; right4BTN.Text = "终止交易";
            }
            else
            {
                bigDisplayLBL.Text = "The new account balance " + acctNo + "\n$" + string.Format("{0:0.00}",depositToAcct.getBalance());
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = "Another Transaction"; right4BTN.Text = "Terminate Transaction";
            }
            
        }

        private void OnElapsedEvent(object sender, ElapsedEventArgs e)
        {
            counter--;
            if (counter == 0)
            {
                aTimer.Stop();
                session = false;
                if (language.ToUpper() == "CHINESE")
                    bigDisplayLBL.Text = "会议结束，没有活动";
                else if (language.ToUpper() == "MALAY")
                    bigDisplayLBL.Text = "Sesi telah berakhir kerana tiada aktiviti";
                else
                    bigDisplayLBL.Text = "Session end due to inactivity";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "";

                theCardReader.ejectCard();

            }
        }

        public override State handleRight3BTNClick()
        {
            State nextStep = this;
            if (session)
                nextStep = new DisplayMainMenuState(mainForm, language);
            else
                theCardReader.ejectCard();
            return nextStep;
        }

        public override State handleRight4BTNClick()
        {
            State nextStep = this;
            if(session)
            nextStep = new RemoveCardState(mainForm, language);
            else
                theCardReader.ejectCard();
            return nextStep;
        }

        public override State handleRightPicBoxClick()
        {
            theCardReader.withoutCard();
            State nextStep = new WaitForBankCardState(mainForm, language);
            return nextStep;
        }
    }
}
