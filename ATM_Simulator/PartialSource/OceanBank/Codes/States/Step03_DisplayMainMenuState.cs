using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;

namespace OceanBank
{
    class DisplayMainMenuState : State
    {
        //Timer
        Timer aTimer;
        int counter = 30;
        bool session = true;

        public DisplayMainMenuState(GUIforATM mainForm, string language) : base(mainForm, language)
        {
            
            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
            aTimer.SynchronizingObject = mainForm;
            aTimer.Interval = 1000;
            aTimer.Start();

            if (language.ToUpper() == "CHINESE")
            {
                bigDisplayLBL.Text = "请选择交易";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "检查存款余额"; left2BTN.Text = "花钱"; left3BTN.Text = "存款"; left4BTN.Text = "汇款";
                right1BTN.Text = "银行信息"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "结束";
            }
            else if (language.ToUpper() == "MALAY")
            {
                bigDisplayLBL.Text = "Sila pilih transaksi anda";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Periksa Baki"; left2BTN.Text = "Wang keluaran"; left3BTN.Text = "Deposit"; left4BTN.Text = "Pindah wang";
                right1BTN.Text = "Khidmat Lain"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Tamat";
            }
            else //ENGLISH
            {
                bigDisplayLBL.Text = "Please select your transaction";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Balance Enquiry"; left2BTN.Text = "Withdraw"; left3BTN.Text = "Deposit"; left4BTN.Text = "Fund Transfer";
                right1BTN.Text = "More Services"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Exit";
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



        public override State handleLeft1BTNClick()
        {
            State nextStep = this;

            aTimer.Stop();

            if (session)
                nextStep = new ChooseAcctToViewBalanceState(mainForm, language);
            else
                theCardReader.ejectCard();
            return nextStep;
        }

        public override State handleLeft2BTNClick()
        {
            State nextStep = this;

            aTimer.Stop();

            if (session)
                nextStep = new ChooseAcctToWithdrawState(mainForm, language);
            else
                theCardReader.ejectCard();
            return nextStep;
        }

        public override State handleLeft3BTNClick()
        {
            State nextStep = this;

            aTimer.Stop();

            if (session)
                nextStep = new ChooseAcctToDepositState(mainForm, language);
            else
                theCardReader.ejectCard();
            return nextStep;
        }

        public override State handleLeft4BTNClick()
        {
            State nextStep = this;

            aTimer.Stop();

            if (session)
                nextStep = new ChooseAcctToFundTransferFromState(mainForm, language);
            else
                theCardReader.ejectCard();
            return nextStep;
        }

        public override State handleRight1BTNClick()
        {
            State nextStep = this;

            aTimer.Stop();

            if (session)
                nextStep = new MoreServices(mainForm, language);
            else
                theCardReader.ejectCard();
            return nextStep;
        }

        public override State handleRight4BTNClick()
        {
            State nextStep = this;

            aTimer.Stop();

            if (session)
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
