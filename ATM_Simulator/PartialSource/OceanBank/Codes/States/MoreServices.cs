using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;

namespace OceanBank
{
    class MoreServices : State
    {
        //Timer
        Timer aTimer;
        int counter = 30;
        bool session = true;

        public MoreServices(GUIforATM mainForm, string language) : base(mainForm, language)
        {
            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
            aTimer.SynchronizingObject = mainForm;
            aTimer.Interval = 1000;
            aTimer.Start();

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Khidmat lain";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Bayar Kad Kredit"; left2BTN.Text = "Tukar PIN"; left3BTN.Text = "Tambah nilai kad tunai"; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu Utama";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "More service";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "支付信用卡"; left2BTN.Text = "更改您的PIN码"; left3BTN.Text = "为您的现金卡添加价值"; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";
            }
            else
            {
                bigDisplayLBL.Text = "More service";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Pay Credit card"; left2BTN.Text = "Change PIN"; left3BTN.Text = "Top Up Cash Card"; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";
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

        public override State handleRight4BTNClick()
        {
            State nextStep = this;
            if (session)
                nextStep = new DisplayMainMenuState(mainForm, language);
            else
                theCardReader.ejectCard();
            return nextStep;
        }

        public override State handleLeft1BTNClick()
        {
            State nextStep = this;
            if(session)
               nextStep = new ChooseAcctToPayCredit(mainForm, language);
            else
                theCardReader.ejectCard();
            return nextStep;
        }

        public override State handleLeft2BTNClick()
        {
            State nextStep = this;
            if(session)
                nextStep = new ValidateNRICorFINState(mainForm, language);
            else
                theCardReader.ejectCard();
            return nextStep;
        }

        public override State handleLeft3BTNClick()
        {
            State nextStep = this;
            if(session)
                nextStep = new ChooseAcctToTopUpCashCardState(mainForm, language);
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
