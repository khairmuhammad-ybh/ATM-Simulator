using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace OceanBank
{
    class WaitForCashcardState : State
    {
        private string acctNo;
        private double topupAmt;

        //Timer
        System.Timers.Timer aTimer;
        private int counter = 5;


        //Cashcard state
        private bool isCardRemoved = false;
        private bool isCashcardInsert = false;

        public WaitForCashcardState(GUIforATM mainForm, string language, string acctNo, double topupAmt) : base (mainForm, language)
        {
            this.acctNo = acctNo;
            this.topupAmt = topupAmt;

            //TIMER
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
            aTimer.SynchronizingObject = mainForm;
            aTimer.Interval = 1000;
            aTimer.Start();

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Sila masukkan Kad Tunai anda";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Tamatkan";
                
                theCardReader.ejectCard();
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "请插入现金卡";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "终止";
                
                theCardReader.ejectCard();
            }
            else //for ENGLISH
            {
                bigDisplayLBL.Text = "Please insert your cashcard";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Terminate";
                
                theCardReader.ejectCard();
            }
            
        }

        public void OnElapsedEvent(object source, ElapsedEventArgs e)
        {
            if (counter > 0)
                counter--;

            if (language.Equals("MALAY"))
            {
                if (counter == 0)
                {
                    aTimer.Stop();
                    bigDisplayLBL.Text = "Sesi telah berakhir kerana tiada aktiviti";
                    bigDisplayLBL.Refresh();

                    pauseforMilliseconds(3000);
                    handleRight4BTNClick();
                }
            }
            else if (language.Equals("CHINESE"))
            {
                if (counter == 0)
                {
                    aTimer.Stop();
                    bigDisplayLBL.Text = "会议结束，没有活动";
                    bigDisplayLBL.Refresh();

                    pauseforMilliseconds(3000);
                    handleRight4BTNClick();
                }
            }
            else //ENGLISH
            {
                if (counter == 0)
                {
                    aTimer.Stop();
                    //bigDisplayLBL.Invoke(new Action(() => bigDisplayLBL.Text = "Session has ended due to no activity"));
                    bigDisplayLBL.Text = "Session has ended due to no activity";
                    bigDisplayLBL.Refresh();

                    pauseforMilliseconds(3000);
                    handleRight4BTNClick();
                }
            }

        }

        public override State handleRight4BTNClick() ///Terminate button
        {
            State nextStep = this;

            if (isCardRemoved == false && isCashcardInsert == false) // remove atm card
                nextStep = new RemoveCardState(mainForm, language);
            else if (isCardRemoved && isCashcardInsert == false) //remove atm card (card already removed)
                nextStep = new WaitForBankCardState(mainForm, language);
            else if (isCardRemoved && isCashcardInsert) //remove cashcard
                nextStep = new RemoveCashCardState(mainForm, language, "CASHCARD");

            aTimer.Stop();
            return nextStep;
        }

        public override State handleRightPicBoxClick()
        {
            State nextStep = this;

            if(isCardRemoved == false && counter != 0)
            {
                theCardReader.withoutCard();
                isCardRemoved = true;
            }
            else if(isCardRemoved == false && counter == 0)
            {
                theCardReader.withoutCard();
                nextStep = new WaitForBankCardState(mainForm, language);
            }
            else
            {
                if (isCashcardInsert == false && counter != 0)
                {
                    theCardReader.insertCashCard();
                    isCashcardInsert = true;
                    aTimer.Stop();
                    bigDisplayLBL.Text = "Cashcard inserted";
                    bigDisplayLBL.Refresh();

                    //do topup algorithm OR pass to confirmation state

                    //Ask for confirmation before deducting
                    nextStep = new CashCardTopUpConfirmation(mainForm, language, acctNo, topupAmt,isCardRemoved, isCashcardInsert);

                }else if(isCashcardInsert == false && counter == 0)
                {
                    nextStep = new WaitForBankCardState(mainForm, language);
                }
            }
            
            return nextStep;
        }

    }
}
