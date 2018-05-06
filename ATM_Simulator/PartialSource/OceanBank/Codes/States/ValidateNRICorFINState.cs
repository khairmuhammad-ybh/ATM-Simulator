using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;

namespace OceanBank
{
    class ValidateNRICorFINState : State
    {
        //Timer
        Timer aTimer;
        int counter = 30;
        bool session = true;

        private int noDigitsForNRICorFIN = 7;
        private string NRICorFINentered;

        public ValidateNRICorFINState(GUIforATM mainForm, string language) : base(mainForm, language)
        {
            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
            aTimer.SynchronizingObject = mainForm;
            aTimer.Interval = 1000;
            aTimer.Start();

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Sila tekan " + noDigitsForNRICorFIN + " nombor Kad Pengenalan anda";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = "Padam"; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu Utama";

                NRICorFINentered = "";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "请输入你的 " + noDigitsForNRICorFIN + " 身份证号码";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = "抹去"; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";

                NRICorFINentered = "";
            }
            else
            {
                bigDisplayLBL.Text = "Please enter your " + noDigitsForNRICorFIN + " numeric numbers of NRIC/FIN";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = "Clear"; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";

                NRICorFINentered = "";
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

        public override State handleRight1BTNClick()
        {
            //pass to next activity
            State nextStep = this;

            if (session)
            {
                if (NRICorFINentered.Length == noDigitsForNRICorFIN)
                {
                    if (validateId() == true)
                    {
                        nextStep = new EnterNewPin(mainForm, language);
                    }
                    else
                    {
                        if (!theCard.isCardLocked())
                        {
                            if (theCard.getIdAttempt() > 1)
                            {
                                if (language.ToUpper() == "CHINESE")
                                    bigDisplayLBL.Text = "错误密码";
                                else if (language.ToUpper() == "MALAY")
                                    bigDisplayLBL.Text = "PIN salah";
                                else
                                    bigDisplayLBL.Text = "Wrong NRIC/FIN";
                                bigDisplayLBL.Refresh();

                                pauseforMilliseconds(1000);
                                if (language.ToUpper() == "CHINESE")
                                    bigDisplayLBL.Text = "请再次输入密码";
                                else if (language.ToUpper() == "MALAY")
                                    bigDisplayLBL.Text = "Sila tekan PIN anda sekali lagi";
                                else
                                    bigDisplayLBL.Text = "Please enter 7 digits NRIC/FIN number again";
                                smallDisplayLBL.Text = "";
                                NRICorFINentered = "";
                                nextStep = this;
                            }
                            else if (theCard.getIdAttempt() == 1)
                            {
                                if (language.ToUpper() == "CHINESE")
                                    bigDisplayLBL.Text = "错误密码";
                                else if (language.ToUpper() == "MALAY")
                                    bigDisplayLBL.Text = "PIN salah";
                                else
                                    bigDisplayLBL.Text = "Wrong NRIC/FIN";
                                bigDisplayLBL.Refresh();

                                pauseforMilliseconds(1000);
                                if (language.ToUpper() == "CHINESE")
                                    bigDisplayLBL.Text = "你的卡之前的最后一次尝试将被锁定";
                                else if (language.ToUpper() == "MALAY")
                                    bigDisplayLBL.Text = "Percubaan terakhir sebelum kad anda akan dikunci";
                                else
                                    bigDisplayLBL.Text = "Last attempt before your card will be lock";
                                smallDisplayLBL.Text = "";
                                NRICorFINentered = "";
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
                }
                else
                {
                    bigDisplayLBL.Text = noDigitsForNRICorFIN + " digits required for identification";
                    NRICorFINentered = "";
                    smallDisplayLBL.Text = NRICorFINentered;

                    pauseforMilliseconds(2000);
                    resetDisplay();
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

        public void resetDisplay()  //resetting display function
        {
            if (language.ToUpper() == "CHINESE")
                bigDisplayLBL.Text = "请输入您的7位IC号码";
            else if (language.ToUpper() == "MALAY")
                bigDisplayLBL.Text = "Sila tekan 7 butang nombor Kad Pengenalan anda";
            else //English
                bigDisplayLBL.Text = "Please enter your 7 numeric numbers of NRIC/FIN";
            NRICorFINentered = "";
            smallDisplayLBL.Text = NRICorFINentered;
        }

        public override State handleRight2BTNClick()
        {
            resetDisplay();
            return this;
        }

        public override State handleKeyClearBTNClick()
        {
            resetDisplay();
            return this;
        }

        private Boolean validateId()
        {
            bool identityValidate = false;

            if (!theCard.isCardLocked())
            {
                if (theCard.getIdAttempt() > 0)
                {
                    if (NRICorFINentered == theCard.getNRIC_FIN())
                    {
                        theCard.resetIdAttempt();
                        identityValidate = true;
                    }
                    else if (theCard.getIdAttempt() == 1)
                    {
                        theCard.decreseIdAttempt();
                        theCard.cardLockout(true);
                        identityValidate = false;
                    }
                    else
                    {
                        theCard.decreseIdAttempt();
                        identityValidate = false;
                    }
                }
            }
            else
            {
                identityValidate = false;
            }

            return identityValidate;
        }

        private State processKey(string k)
        {
            State nextStep = this;

            if (NRICorFINentered.Length < noDigitsForNRICorFIN)
            {
                NRICorFINentered += k;
                smallDisplayLBL.Text = NRICorFINentered;
                smallDisplayLBL.Refresh();
                if (NRICorFINentered.Length == noDigitsForNRICorFIN)
                {
                    bigDisplayLBL.Text = "Press 'OK' to continue";
                    
                }
            }

            return nextStep;
        }

        public void cardLockout()
        {
            if (language.ToUpper() == "CHINESE")
                bigDisplayLBL.Text = "由于多次尝试失败，您的卡已被锁定";
            else if (language.ToUpper() == "MALAY")
                bigDisplayLBL.Text = "Kad anda telah dikunci di atas beberapa percubaan yang tidak berjaya";
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
            NRICorFINentered = "";
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
