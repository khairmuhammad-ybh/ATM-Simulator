using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;

namespace OceanBank
{
    class EnterNewPin : State
    {
        //Timer
        Timer aTimer;
        int counter = 30;
        bool session = true;

        private int noDigitsForPIN = 6;
        private string PINentered;
        private string PIN_One;
        private string PIN_Two;

        public EnterNewPin(GUIforATM mainForm, string language) : base(mainForm, language)
        {
            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
            aTimer.SynchronizingObject = mainForm;
            aTimer.Interval = 1000;
            aTimer.Start();


            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Sila tekan " + noDigitsForPIN + " butang PIN nombor yang baru";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = "Padam"; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "请输入你的 " + noDigitsForPIN + " 新PIN的按钮";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = "抹去"; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";
            }
            else
            {
                bigDisplayLBL.Text = "Please enter your new " + noDigitsForPIN + " digits PIN number";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = "Clear"; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";
            }
 
            PINentered = "";
            PIN_One = "";
            PIN_Two = "";
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
            State nextStep = this;

            if (session)
            {
                if (language.Equals("MALAY"))
                {
                    //required 2 sets of pin for confirmation
                    bigDisplayLBL.Text = "Menetapkan PIN baru";

                    if (PINentered.Length == noDigitsForPIN)
                    {
                        if (PIN_One.Equals(""))     //Set 1st PIN
                        {
                            PIN_One = PINentered;
                            bigDisplayLBL.Text = "Sila tekan semula nombor PIN untuk pengesahan";
                            PINentered = "";
                            smallDisplayLBL.Text = PINentered;
                        }
                        else    //Set 2nd PIN
                        {
                            PIN_Two = PINentered;

                            //Check for PIN confirmation
                            if (PIN_One == PIN_Two)
                            {
                                //Set new PIN
                                theCard.setPIN(PIN_Two);
                                bigDisplayLBL.Text = "Nombor PIN baru berjaya ditukar";

                                pauseforMilliseconds(2000);
                                nextStep = new DisplayMainMenuState(mainForm, language);

                            }
                            else
                            {
                                bigDisplayLBL.Text = "PIN baru tidak sepadan dengan PIN pengesahan";

                                pauseforMilliseconds(2000);
                                resetDisplayAndPINValues();
                            }
                        }
                    }
                    else
                    {
                        bigDisplayLBL.Text = noDigitsForPIN + " nombor diperlukan untuk PIN baru";
                        PINentered = "";
                        smallDisplayLBL.Text = PINentered;

                        pauseforMilliseconds(2000);
                        if (PIN_One.Equals(""))
                            bigDisplayLBL.Text = "Sila tekan " + noDigitsForPIN + " nombor PIN baru";
                        else
                            bigDisplayLBL.Text = "Sila tekan semula PIN untuk pengesahan";
                    }
                }
                else if (language.Equals("CHINESE"))
                {
                    //required 2 sets of pin for confirmation
                    bigDisplayLBL.Text = "设置一个新的PIN码";

                    if (PINentered.Length == noDigitsForPIN)
                    {
                        if (PIN_One.Equals(""))     //Set 1st PIN
                        {
                            PIN_One = PINentered;
                            bigDisplayLBL.Text = "重新输入密码进行确认";
                            PINentered = "";
                            smallDisplayLBL.Text = PINentered;
                        }
                        else    //Set 2nd PIN
                        {
                            PIN_Two = PINentered;

                            //Check for PIN confirmation
                            if (PIN_One == PIN_Two)
                            {
                                //Set new PIN
                                theCard.setPIN(PIN_Two);
                                bigDisplayLBL.Text = "新密码成功更改";

                                pauseforMilliseconds(2000);
                                nextStep = new DisplayMainMenuState(mainForm, language);

                            }
                            else
                            {
                                bigDisplayLBL.Text = "新的PIN码与确认PIN码不符\n请重新输入PIN码";

                                pauseforMilliseconds(2000);
                                resetDisplayAndPINValues();
                            }
                        }
                    }
                    else
                    {
                        bigDisplayLBL.Text = noDigitsForPIN + " 数字密码需要";
                        PINentered = "";
                        smallDisplayLBL.Text = PINentered;

                        pauseforMilliseconds(2000);
                        if (PIN_One.Equals(""))
                            bigDisplayLBL.Text = "新的PIN码需要 " + noDigitsForPIN + " 位数字";
                        else
                            bigDisplayLBL.Text = "重新输入密码进行确认";
                    }
                }
                else //ENGLISH
                {
                    //required 2 sets of pin for confirmation
                    bigDisplayLBL.Text = "Setting New PIN";

                    if (PINentered.Length == noDigitsForPIN)
                    {
                        if (PIN_One.Equals(""))     //Set 1st PIN
                        {
                            PIN_One = PINentered;
                            bigDisplayLBL.Text = "Please re-enter PIN for confirmation";
                            PINentered = "";
                            smallDisplayLBL.Text = PINentered;
                        }
                        else    //Set 2nd PIN
                        {
                            PIN_Two = PINentered;

                            //Check for PIN confirmation
                            if (PIN_One == PIN_Two)
                            {
                                //Set new PIN
                                theCard.setPIN(PIN_Two);
                                bigDisplayLBL.Text = "New PIN successfully changed";

                                pauseforMilliseconds(2000);
                                nextStep = new DisplayMainMenuState(mainForm, language);

                            }
                            else
                            {
                                bigDisplayLBL.Text = "New PIN does not match the confirmation PIN\nPlease re-enter PIN";

                                pauseforMilliseconds(3000);
                                resetDisplayAndPINValues();
                            }
                        }
                    }
                    else
                    {
                        bigDisplayLBL.Text = noDigitsForPIN + " digits required for a new PIN";
                        PINentered = "";
                        smallDisplayLBL.Text = PINentered;

                        pauseforMilliseconds(2000);
                        if (PIN_One.Equals(""))
                            bigDisplayLBL.Text = "Please enter your new " + noDigitsForPIN + " digits PIN number";
                        else
                            bigDisplayLBL.Text = "Please re-enter PIN for confirmation";
                    }
                }
            }
            else
                theCardReader.ejectCard();

            return nextStep;
        }

        public override State handleRight2BTNClick()
        {
            State nextStep = this;

            if (session)
            {
                if (language.Equals("MALAY"))
                {
                    if (PIN_One.Equals(""))
                        bigDisplayLBL.Text = "Sila tekan " + noDigitsForPIN + " butang nombor PIN yang baru";
                    else
                        bigDisplayLBL.Text = "Tekan PIN sekali lagi untuk pengesahan";
                    PINentered = "";
                    smallDisplayLBL.Text = PINentered;
                }
                else if (language.Equals("CHINESE"))
                {
                    if (PIN_One.Equals(""))
                        bigDisplayLBL.Text = "请输入你的 " + noDigitsForPIN + " 新PIN的按钮";
                    else
                        bigDisplayLBL.Text = "请重新输入您的密码进行确认";
                    PINentered = "";
                    smallDisplayLBL.Text = PINentered;
                }
                else
                {
                    if (PIN_One.Equals(""))
                        bigDisplayLBL.Text = "Please enter your new " + noDigitsForPIN + " digits PIN number";
                    else
                        bigDisplayLBL.Text = "Please re-enter PIN for confirmation";
                    PINentered = "";
                    smallDisplayLBL.Text = PINentered;
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

        public override State handleKeyClearBTNClick()
        {
            if (language.Equals("MALAY"))
            {
                if (PIN_One.Equals(""))
                    bigDisplayLBL.Text = "Sila tekan " + noDigitsForPIN + " butang nombor PIN yang baru";
                else
                    bigDisplayLBL.Text = "Tekan PIN sekali lagi untuk pengesahan";
                PINentered = "";
                smallDisplayLBL.Text = PINentered;
                return this;
            }
            else if (language.Equals("CHINESE"))
            {
                if (PIN_One.Equals(""))
                    bigDisplayLBL.Text = "请输入你的 " + noDigitsForPIN + " 新PIN的按钮";
                else
                    bigDisplayLBL.Text = "请重新输入您的密码进行确认";
                PINentered = "";
                smallDisplayLBL.Text = PINentered;
                return this;
            }
            else
            {
                if (PIN_One.Equals(""))
                    bigDisplayLBL.Text = "Please enter your new " + noDigitsForPIN + " digits PIN number";
                else
                    bigDisplayLBL.Text = "Please re-enter PIN for confirmation";
                PINentered = "";
                smallDisplayLBL.Text = PINentered;
                return this;
            }
        }

        private void resetDisplayAndPINValues() //resetting display function
        {
            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Sila tekan " + noDigitsForPIN + " butang nombor PIN yang baru";
                PIN_One = "";
                PIN_Two = "";
                PINentered = "";
                smallDisplayLBL.Text = PINentered;
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "请输入你的 " + noDigitsForPIN + " 新PIN的按钮";
                PIN_One = "";
                PIN_Two = "";
                PINentered = "";
                smallDisplayLBL.Text = PINentered;
            }
            else
            {
                bigDisplayLBL.Text = "Please enter your new " + noDigitsForPIN + " digits PIN number";
                PIN_One = "";
                PIN_Two = "";
                PINentered = "";
                smallDisplayLBL.Text = PINentered;
            }
            
        }

        private State processKey(string k)
        {
            State nextStep = this;

            if (session)
            {
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

                    if (language.Equals("MALAY"))
                    {
                        if (PINentered.Length == noDigitsForPIN)
                        {
                            bigDisplayLBL.Text = "Tekan butang 'Ok' untuk teruskan";
                        }
                    }
                    else if (language.Equals("CHINESE"))
                    {
                        if (PINentered.Length == noDigitsForPIN)
                        {
                            bigDisplayLBL.Text = "按OK继续";
                        }
                    }
                    else
                    {
                        if (PINentered.Length == noDigitsForPIN)
                        {
                            bigDisplayLBL.Text = "Press 'Ok' to continue";
                        }
                    }
                }
            }
            else
                theCardReader.ejectCard();

            return nextStep;
        }
    }
}
