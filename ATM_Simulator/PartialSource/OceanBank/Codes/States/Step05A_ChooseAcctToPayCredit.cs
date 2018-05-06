using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;

namespace OceanBank
{
    class ChooseAcctToPayCredit : State
    {
        //Timer
        Timer aTimer;
        int counter = 30;
        bool session = true;

        public ChooseAcctToPayCredit(GUIforATM mainForm, string language) : base(mainForm, language)
        {
            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
            aTimer.SynchronizingObject = mainForm;
            aTimer.Interval = 1000;
            aTimer.Start();

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Sila pilih akaun untuk membuat bayaran";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu Utama";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "请选择帐户付款";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";
            }
            else
            {
                bigDisplayLBL.Text = "Please select account to pay from";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";
            }

            if (theCard.getNumAccounts() >= 1)
                left1BTN.Text = theCard.getAcctAtIndex(0).getAcctNo();

            if (theCard.getNumAccounts() >= 2)
                left2BTN.Text = theCard.getAcctAtIndex(1).getAcctNo();

            if (theCard.getNumAccounts() == 3)  // Max 3 accounts per Card only
                left3BTN.Text = theCard.getAcctAtIndex(2).getAcctNo();

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

        public override State handleLeft1BTNClick()
        {
            State nextStep = this;

            if (session)
            {
                if (left1BTN.Text != "")
                {
                    nextStep = new EnterCreditCardNumberState(mainForm, language, left1BTN.Text);
                }
            }
            else
                theCardReader.ejectCard();
            
            return nextStep;
        }

        public override State handleLeft2BTNClick()
        {
            State nextStep = this;

            if (session)
            {
                if (left2BTN.Text != "")
                {
                    nextStep = new EnterCreditCardNumberState(mainForm, language, left2BTN.Text);
                }
            }
            else
                theCardReader.ejectCard();
            
            return nextStep;
        }


        public override State handleLeft3BTNClick()
        {
            State nextStep = this;

            if (session)
            {
                if (left3BTN.Text != "")
                {
                    nextStep = new EnterCreditCardNumberState(mainForm, language, left3BTN.Text);
                }

            }
            else
                theCardReader.ejectCard();
            
            return nextStep;
        }


        public override State handleRight4BTNClick()
        {
            State nextStep = this;
            if (session)
                nextStep = new DisplayMainMenuState(mainForm, language);
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
