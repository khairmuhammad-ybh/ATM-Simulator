using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;

namespace OceanBank
{
    class EnterCreditCardNumberState : State
    {
        //Timer
        Timer aTimer;
        int counter = 30;
        bool session = true;

        private string acctNo;
        Account accountLimit;

        private int maxCreditDigits = 16;
        private string creditNoEntered;

        public EnterCreditCardNumberState(GUIforATM mainForm, string language, string acctNo) : base(mainForm, language)
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
                bigDisplayLBL.Text = "Sila tekan " + maxCreditDigits + " butang nombor Kad Kredit anda";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Pilih semula akaun"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "OK"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu Utama";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "请输入你的 " + maxCreditDigits + " 信用卡号";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "重新选择帐户"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "OK"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";
            }
            else
            {
                bigDisplayLBL.Text = "Pleases enter the " + maxCreditDigits + " digits credit card number";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Re-select account"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "OK"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";
            }

            creditNoEntered = "";

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
                nextStep = new ChooseAcctToPayCredit(mainForm, language);
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
                if (creditNoEntered.Length == maxCreditDigits)
                {
                    nextStep = new EnterAmtToPayCreditCard(mainForm, language, this.acctNo, smallDisplayLBL.Text);
                }
                else
                {
                    if (language.Equals("MALAY"))
                    {
                        bigDisplayLBL.Text = "Memerlukan " + maxCreditDigits + " digits credit card number to be entered\nSila tekan sekali lagi";
                        creditNoEntered = "";
                        smallDisplayLBL.Text = creditNoEntered;
                    }
                    else if (language.Equals("CHINESE"))
                    {
                        bigDisplayLBL.Text = "需求 " + maxCreditDigits + " 信用卡号码被输入\n请再次输入";
                        creditNoEntered = "";
                        smallDisplayLBL.Text = creditNoEntered;
                    }
                    else
                    {
                        bigDisplayLBL.Text = "Requires " + maxCreditDigits + " digits credit card number to be entered\nPease re-enter";
                        creditNoEntered = "";
                        smallDisplayLBL.Text = creditNoEntered;
                    }
                }
            }
            else
                theCardReader.ejectCard();

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
            creditNoEntered = "";
            smallDisplayLBL.Text = creditNoEntered;
            return this;
        }

        private Boolean validatePIN()
        {
            if (creditNoEntered == theCard.getPIN())
                return true;
            else
                return false;
        }

        private State processKey(string k)
        {
            State nextStep = this;

            if (session)
            {
                if (creditNoEntered.Length < maxCreditDigits)
                {
                    creditNoEntered += k;
                    smallDisplayLBL.Text = creditNoEntered;

                    if (creditNoEntered.Length == maxCreditDigits)
                    {
                        if (language.Equals("MALAY"))
                            bigDisplayLBL.Text = "Tekan butang 'Ok' untuk teruskan";
                        else if (language.Equals("CHINESE"))
                            bigDisplayLBL.Text = "按OK继续";
                        else
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
