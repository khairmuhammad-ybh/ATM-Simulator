using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using System.Timers;

namespace OceanBank
{
    class EnterAmtToPayCreditCard : State
    {
        //Timer
        Timer aTimer;
        int counter = 30;
        bool session = true;


        private string amountEnteredTxt;
        private string acctNo;
        private string cardNo;
        Account account;
        double maxAcctPaymentLimit;

        //$$ amount display 
        Char[] ch;
        string newString;
        string amtLength;
        string increaseAmt;
        int cnt = 0;
        int firstHalf = 0;

        public EnterAmtToPayCreditCard(GUIforATM mainForm, string language, string acctNo, string cardNo) : base(mainForm, language)
        {
            this.acctNo = acctNo;
            this.cardNo = cardNo;
            this.account = theCard.getAcctUsingAcctNo(acctNo);

            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
            aTimer.SynchronizingObject = mainForm;
            aTimer.Interval = 1000;
            aTimer.Start();

            maxAcctPaymentLimit = 1000;

            //Get and set datetime properties
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            DateTime now = DateTime.Now;

            if (now > start && now < end)
            {
                //smallDisplayLBL.Text = "Current time:" + now + " Time within range";
            }
            else
            {
                //smallDisplayLBL.Text = "Current time:" + now + " Time out of range";
                account.resetCreditTransferLimit();

            }


            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Sila tekan jumlah yang ingin dibayar untuk " + this.cardNo + "\ndari " + this.acctNo;
                smallDisplayLBL.Text = "00.00";
                left1BTN.Text = "Edit nombor kad kredit"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu Utama";

                amountEnteredTxt = "";

                //$$ amount display
                amtLength = "";
                ch = smallDisplayLBL.Text.ToCharArray();
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "请输入付款金额 " + this.cardNo + "\n从你的 " + this.acctNo;
                smallDisplayLBL.Text = "00.00";
                left1BTN.Text = "Edit credit card no."; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";

                amountEnteredTxt = "";

                //$$ amount display
                amtLength = "";
                ch = smallDisplayLBL.Text.ToCharArray();
            }
            else
            {
                bigDisplayLBL.Text = "Please enter amount to pay to " + this.cardNo + "\nfrom your " + this.acctNo;
                smallDisplayLBL.Text = "00.00";
                left1BTN.Text = "Edit credit card no."; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";

                amountEnteredTxt = "";

                //$$ amount display
                amtLength = "";
                ch = smallDisplayLBL.Text.ToCharArray();
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
            if (session)
                nextStep = new EnterCreditCardNumberState(mainForm, language, acctNo);
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

        public override State handleKeyDecBTNClick()
        {
            State nextStep = processKey(".");
            return nextStep;
        }

        public override State handleKeyClearBTNClick()
        {
            resetDisplay();
            return this;
        }


        public override State handleRight1BTNClick()
        {
            double payAmt;
            Account payFromAcct;
            double maxAmountAllowed = 1000;
            double minAmountAllowed = 0.01;

            State nextStep = this;

            if (session)
            {
                if (!amountEnteredTxt.Equals(""))
                {
                    payAmt = Convert.ToDouble(amountEnteredTxt);
                }
                else
                {
                    payAmt = 0.00;
                }

                payFromAcct = theCard.getAcctUsingAcctNo(acctNo);


                //check for min max amount allowed
                if (payAmt <= maxAmountAllowed && payAmt >= minAmountAllowed)
                {
                    //State nextStep = new CreditPaymentConfirmation(mainForm, language, acctNo, cardNo, payAmt);
                    //return nextStep;

                    //check for valid balance before withdraw
                    if (payFromAcct.getBalance() - payAmt < 0)
                    {
                        bigDisplayLBL.Text = "Insufficent balance to pay\n Please re-enter amount to pay";
                        pauseforMilliseconds(3000);
                        resetDisplay();
                    }
                    else
                    {
                        //check for account transfer limit balance
                        double afterPayment = payFromAcct.getCreditTransferLimit() - payAmt;

                        if (afterPayment < maxAcctPaymentLimit && afterPayment >= 0)
                        {
                            nextStep = new CreditPaymentConfirmation(mainForm, language, acctNo, cardNo, payAmt);
                        }
                        else
                        {
                            bigDisplayLBL.Text = "Maximum limit reached, unable to pay\nAccount limit left for today: $" + payFromAcct.getCreditTransferLimit();
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }

                    }
                }
                else
                {
                    bigDisplayLBL.Text = "$" + String.Format("{0:0.00}", payAmt) + " is not allowed\nPlease re-enter amount to pay";

                    pauseforMilliseconds(3000);
                    resetDisplay();
                }
            }

            return nextStep;
            
        }

        private void resetDisplay() // resetting display function
        { 

            //Reset attributes
            amountEnteredTxt = "00.00";

            //$$ amount display reset
            amtLength = "";
            string resetCH = "00.00";
            ch = resetCH.ToCharArray();
            cnt = 0;
            firstHalf = 0;

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Sila tekan jumlah yang ingin dibayar untuk " + this.cardNo + "\ndari " + this.acctNo;
                smallDisplayLBL.Text = amountEnteredTxt;
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "请输入付款金额 " + this.cardNo + "\n从你的 " + this.acctNo;
                smallDisplayLBL.Text = amountEnteredTxt;
            }
            else
            {
                bigDisplayLBL.Text = "Please enter amount to pay to " + this.cardNo + "\nfrom your " + this.acctNo;
                smallDisplayLBL.Text = amountEnteredTxt;
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

        private State processKey(string k)
        {
            State nextStep = this;

            if (session)
            {
                //Regex rgx = new Regex("(^\\d{1,4}\\.\\d{1,2}$)");    //USD$ currency format
                Regex rgx = new Regex("(^\\d*\\.?\\d*[0-9]+\\d*$)|(^[0-9]+\\d*\\.\\d*$)|(^\\d{1,4}\\.\\d{1,2}$)");    //USD$ currency format _2
                if (amountEnteredTxt.Equals("") && k.Equals("0"))
                {
                    nextStep = this;
                }
                else
                {
                    if (cnt < 2 && k.Equals(".")) //starts of digits after '.'
                    {
                        amountEnteredTxt += k;
                        amtLength += k;

                    }
                    else if (cnt < 2 && firstHalf < 4)  //digits before '.' 
                    {
                        amountEnteredTxt += k;
                        amtLength += k;
                    }

                    //Display manipulation
                    if (amtLength.Length == 1 && k.Equals(".")) //  for .01 input
                    {
                        ch[1] = Char.Parse("0");
                        newString = new string(ch);
                        amountEnteredTxt = newString;
                    }
                    else if (amtLength.Length == 2 && k.Equals(".")) //Added
                    {
                        //increate first counter
                        firstHalf++;
                    }
                    else if (amountEnteredTxt.Contains(".") && cnt == 1) // 2nd digits of decimal
                    {
                        ch[ch.Length - 1] = Char.Parse(k);
                        newString = new string(ch);
                        amountEnteredTxt = newString;

                        //increase overall counter
                        cnt++;

                    }
                    else if ((amtLength.Contains(".") && k.Equals("."))) //for "." input
                    {
                        ch[ch.Length - 2] = Char.Parse("0");
                        newString = new string(ch);
                        amountEnteredTxt = newString;

                    }
                    else if ((amtLength.Contains(".") && cnt == 0)) //This is for 1st digit of decimal
                    {
                        ch[ch.Length - 2] = Char.Parse(k);
                        newString = new string(ch);
                        amountEnteredTxt = newString;

                        //increase overall counter
                        cnt++;

                    }
                    else if (!amtLength.Contains(".") && amtLength.Length > 2 && cnt == 0 && firstHalf != 4) // allow max 4 digits before '.'
                    {

                        newString = new string(ch);
                        increaseAmt = newString.Insert(newString.Length - 3, k);
                        amountEnteredTxt = increaseAmt;
                        ch = increaseAmt.ToCharArray();

                        firstHalf++;
                    }
                    else if (amtLength.Length == 1)
                    {
                        ch[1] = Char.Parse(k);
                        newString = new string(ch);
                        amountEnteredTxt = newString;

                        //increate first counter
                        firstHalf++;
                    }
                    else if (amtLength.Length == 2)
                    {
                        ch[0] = ch[1];
                        ch[1] = Char.Parse(k);
                        newString = new string(ch);
                        amountEnteredTxt = newString;

                        //increase first counter
                        firstHalf++;
                    }

                }

                //check number format
                if (rgx.IsMatch(amountEnteredTxt))
                {
                    smallDisplayLBL.Text = amountEnteredTxt;
                }
                else
                {
                    amountEnteredTxt.Substring(amountEnteredTxt.Length - 1);
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
