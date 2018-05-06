using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;

namespace OceanBank
{
    class EnterAcctNumberToFundTransferToState : State
    {
        //Timer
        Timer aTimer;
        int counter = 30;
        bool session = true;

        private string acctNo;
        Account accountLimit;

        private int maxAcctDigits = 7;
        private string acctNoEntered;

        public EnterAcctNumberToFundTransferToState(GUIforATM mainForm, string language, string acctNo) : base(mainForm, language)
        {
            this.acctNo = acctNo;
            this.accountLimit = theCard.getAcctUsingAcctNo(acctNo);

            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
            aTimer.SynchronizingObject = mainForm;
            aTimer.Interval = 1000;
            aTimer.Start();

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Sila tekan " + maxAcctDigits + " nombor akaun anda";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Pilih semula akaun"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "OK"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "请输入 " + maxAcctDigits + " 帐号";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "重新选择帐户"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "OK"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";
            }
            else //ENGLISH
            {
                bigDisplayLBL.Text = "Please enter the " + maxAcctDigits + " digits account number";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Re-select account"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "OK"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";
            }

            acctNoEntered = "";

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
                nextStep = new ChooseAcctToFundTransferFromState(mainForm, language);
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

        public override State handleRight1BTNClick()
        {
            State nextStep = this;

            if (session)
            {
                if (language.Equals("MALAY"))
                {
                    if (acctNoEntered.Length == maxAcctDigits)
                    {
                        nextStep = new EnterAmtToFundTransfer(mainForm, language, this.acctNo, smallDisplayLBL.Text);
                    }
                    else
                    {
                        bigDisplayLBL.Text = "Perlukan " + maxAcctDigits + " nombor akaun anda\nSila tekan semula";
                        acctNoEntered = "";
                        smallDisplayLBL.Text = acctNoEntered;
                    }
                }
                else if (language.Equals("CHINESE"))
                {
                    if (acctNoEntered.Length == maxAcctDigits)
                    {
                        nextStep = new EnterAmtToFundTransfer(mainForm, language, this.acctNo, smallDisplayLBL.Text);
                    }
                    else
                    {
                        bigDisplayLBL.Text = "需要 " + maxAcctDigits + " 帐号\n请再次输入";
                        acctNoEntered = "";
                        smallDisplayLBL.Text = acctNoEntered;
                    }
                }
                else //ENGLISH
                {
                    if (acctNoEntered.Length == maxAcctDigits)
                    {
                        nextStep = new EnterAmtToFundTransfer(mainForm, language, this.acctNo, smallDisplayLBL.Text);
                    }
                    else
                    {
                        bigDisplayLBL.Text = "Requires " + maxAcctDigits + " digits account number to be entered\nPease re-enter";
                        acctNoEntered = "";
                        smallDisplayLBL.Text = acctNoEntered;
                    }
                }
            }theCardReader.ejectCard();

            return nextStep;

        }

        public override State handleKey1BTNClick()
        {
            State nextStep = processKey("1");
            return nextStep;
        }

        public override State handleKey2BTNClick()
        {
            State nextStep = processKey("2");
            return nextStep;
        }

        public override State handleKey3BTNClick()
        {
            State nextStep = processKey("3");
            return nextStep;
        }

        public override State handleKey4BTNClick()
        {
            State nextStep = processKey("4");
            return nextStep;
        }

        public override State handleKey5BTNClick()
        {
            State nextStep = processKey("5");
            return nextStep;
        }

        public override State handleKey6BTNClick()
        {
            State nextStep = processKey("6");
            return nextStep;
        }

        public override State handleKey7BTNClick()
        {
            State nextStep = processKey("7");
            return nextStep;
        }

        public override State handleKey8BTNClick()
        {
            State nextStep = processKey("8");
            return nextStep;
        }

        public override State handleKey9BTNClick()
        {
            State nextStep = processKey("9");
            return nextStep;
        }

        public override State handleKey0BTNClick()
        {
            State nextStep = processKey("0");
            return nextStep;
        }

        public override State handleKeyClearBTNClick()
        {
            acctNoEntered = "";
            smallDisplayLBL.Text = acctNoEntered;
            return this;
        }

        private Boolean validatePIN()
        {
            if (acctNoEntered == theCard.getPIN())
                return true;
            else
                return false;
        }

        private State processKey(string k)
        {
            State nextStep = this;

            if (session)
            {
                if (acctNoEntered.Length < maxAcctDigits)
                {
                    acctNoEntered += k;
                    smallDisplayLBL.Text = acctNoEntered;

                    if (acctNoEntered.Length == maxAcctDigits)
                    {
                        if (language.Equals("MALAY"))
                            bigDisplayLBL.Text = "Tekan butang 'Ok' untuk teruskan";
                        else if (language.Equals("CHINESE"))
                            bigDisplayLBL.Text = "按OK继续";
                        else //ENGLISH
                            bigDisplayLBL.Text = "Press 'Ok' to continue";
                    }
                }
            }
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
