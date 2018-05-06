using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OceanBank
{
    class ValidatePINState : State
    {
        private int noDigitsForPIN = 6;
        private string PINentered;

        public ValidatePINState(GUIforATM mainForm, string language) : base(mainForm, language)
        {
            if (language.ToUpper() == "CHINESE")
            {
                bigDisplayLBL.Text = "请输入密码";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "语言"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "结束";
            }
            else if(language.ToUpper() == "MALAY")
            {
                bigDisplayLBL.Text = "Sila tekan pin";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Bahasa"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Tamat";
            }
            else //ENGLISH
            {
                bigDisplayLBL.Text = "Please enter PIN";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Language"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Exit";
            }

            PINentered = "";
        }

        public override State handleRight1BTNClick()
        {
            State nextStep = this;

            if (validatePIN() == true)
            {
                nextStep = new DisplayMainMenuState(mainForm, language);
            }
            else
            {
                if (!theCard.isCardLocked())
                {
                    if (theCard.getAttempt() > 1)
                    {
                        if (language.ToUpper() == "CHINESE")
                            bigDisplayLBL.Text = "错误密码";
                        else if (language.ToUpper() == "MALAY")
                            bigDisplayLBL.Text = "PIN tidak tepat";
                        else
                            bigDisplayLBL.Text = "Wrong PIN";
                        bigDisplayLBL.Refresh();

                        pauseforMilliseconds(1000);
                        if (language.ToUpper() == "CHINESE")
                            bigDisplayLBL.Text = "请再次输入密码";
                        else if (language.ToUpper() == "MALAY")
                            bigDisplayLBL.Text = "Sila tekan PIN sekali lagi";
                        else
                            bigDisplayLBL.Text = "Please enter PIN again";
                        smallDisplayLBL.Text = "";
                        PINentered = "";
                        nextStep = this;
                    }
                    else if (theCard.getAttempt() == 1)
                    {
                        if (language.ToUpper() == "CHINESE")
                            bigDisplayLBL.Text = "错误密码";
                        else if (language.ToUpper() == "MALAY")
                            bigDisplayLBL.Text = "PIN tidak tepat";
                        else
                            bigDisplayLBL.Text = "Wrong PIN";
                        bigDisplayLBL.Refresh();

                        pauseforMilliseconds(1000);
                        if (language.ToUpper() == "CHINESE")
                            bigDisplayLBL.Text = "你的卡之前的最后一次尝试将被锁定";
                        else if (language.ToUpper() == "MALAY")
                            bigDisplayLBL.Text = "Percubaan terakhir sebelum kad anda terkunci";
                        else
                            bigDisplayLBL.Text = "Last attempt before your card will be lock";
                        smallDisplayLBL.Text = "";
                        PINentered = "";
                        nextStep = this;
                    }
                    else
                    {
                        cardLockout();
                        nextStep = this;
                    }
                }
                // card has been locked
                else
                {
                    cardLockout();
                    nextStep = this;
                }

            }

            return nextStep;
        }

        public override State handleLeft1BTNClick()
        {
            State nextStep = new LanguageSelectionState(mainForm, language);
            return nextStep;
        }

        public override State handleRight4BTNClick()
        {
            State nextStep = new RemoveCardState(mainForm, language);
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
            PINentered = "";
            smallDisplayLBL.Text = PINentered;
            return this;
        }

        private Boolean validatePIN()
        {
            bool PINvalidate = false;
            if (!theCard.isCardLocked())
            {
                if (theCard.getAttempt() > 0)
                {
                    if (PINentered == theCard.getPIN())
                    {
                        theCard.resetAttempt();
                        PINvalidate = true;
                    }
                    else if (theCard.getAttempt() == 1)
                    {
                        theCard.decreaseAttempt();
                        theCard.cardLockout(true);
                        PINvalidate = false;
                    }
                    else
                    {
                        theCard.decreaseAttempt();
                        PINvalidate = false;
                    }
                }
            }
            else
            {
                PINvalidate = false;
            }
            

            return PINvalidate;
            
        }

        private State processKey(string k)
        {
            State nextStep = this;

            if (PINentered.Length < noDigitsForPIN)
            {

                PINentered += k;

                //Mask DisplayLBL with assterisk
                smallDisplayLBL.Text = "";
                for (int i = 0; i < PINentered.Length; i++)
                {
                    smallDisplayLBL.Text += "*";
                }

                //smallDisplayLBL.Text = PINentered;
                smallDisplayLBL.Refresh();
                
                
            }
            //else if (PINentered.Length == noDigitsForPIN)
            //{

            //}
            return nextStep;
        }

        public void cardLockout()
        {
            if (language.ToUpper() == "CHINESE")
                bigDisplayLBL.Text = "由于多次尝试失败，您的卡已被锁定";
            else if (language.ToUpper() == "MALAY")
                bigDisplayLBL.Text = "Kad anda telah terkunci kerana beberapa percubaan yang tidak berjaya";
            else
                bigDisplayLBL.Text = "Your card has been locked due to multiple unsuccessful attempt";
            bigDisplayLBL.Refresh();

            pauseforMilliseconds(2000);
            if (language.ToUpper() == "CHINESE")
                bigDisplayLBL.Text = "请到最近的分行解锁您的ATM卡";
            else if (language.ToUpper() == "MALAY")
                bigDisplayLBL.Text = "Sila pergi ke cawangan terdekat untuk membuka kunci Kad ATM anda";
            else
                bigDisplayLBL.Text = "Please go to the nearest branch to unlock your ATM Card";
            smallDisplayLBL.Text = "";
            PINentered = "";
            theCardReader.ejectCard();
        }

        public override State handleRightPicBoxClick()
        {
            State nextStep;

            theCardReader.withoutCard();
            nextStep = new WaitForBankCardState(mainForm, language);
            return nextStep;
        }

    }
}
