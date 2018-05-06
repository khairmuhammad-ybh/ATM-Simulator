using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using System.Timers;

namespace OceanBank
{
    class EnterAmtToFundTransfer : State
    {
        //Timer
        Timer aTimer;
        int counter = 30;
        bool session = true;

        private string amountEnteredTxt;
        private string acctNo;
        private string cardNo;
        Account account;
        double maxAcctTransferLimit;

        //$$ amount display 
        Char[] ch;
        string newString;
        string amtLength;
        string increaseAmt;
        int cnt = 0;
        int firstHalf = 0;

        public EnterAmtToFundTransfer(GUIforATM mainForm, string language, string acctNo, string cardNo) : base(mainForm, language)
        {
            this.acctNo = acctNo;
            this.cardNo = cardNo;
            this.account = theCard.getAcctUsingAcctNo(acctNo);

            maxAcctTransferLimit = 1000;

            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
            aTimer.SynchronizingObject = mainForm;
            aTimer.Interval = 1000;
            aTimer.Start();

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
                account.resetFundTransferLimit();

            }

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Sila tekan jumlah untuk dipindahkan dari " + this.acctNo + "\nke " + this.cardNo;
                smallDisplayLBL.Text = "00.00";
                left1BTN.Text = "Tukar nombor akaun"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "请输入转账金额从 " + this.acctNo + "\n至 " + this.cardNo;
                smallDisplayLBL.Text = "00.00";
                left1BTN.Text = "修改帐户"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";
            }
            else
            {
                bigDisplayLBL.Text = "Please enter amount to transfer from " + this.acctNo + "\nto " + this.cardNo;
                smallDisplayLBL.Text = "00.00";
                left1BTN.Text = "Edit Account No."; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";
            }


            amountEnteredTxt = "";

            //$$ amount display
            amtLength = "";
            ch = smallDisplayLBL.Text.ToCharArray();
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
                nextStep = new EnterAcctNumberToFundTransferToState(mainForm, language, acctNo);
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
            double amtTransfer;
            Account transferFromAcct;
            double maxAmountAllowed = 1000;
            double minAmountAllowed = 0.01;

            State nextStep = this;

            if (session)
            {
                if (!amountEnteredTxt.Equals(""))
                {
                    amtTransfer = Convert.ToDouble(amountEnteredTxt);
                }
                else
                {
                    amtTransfer = 0.00;
                }

                transferFromAcct = theCard.getAcctUsingAcctNo(acctNo);

                if (language.Equals("MALAY"))
                {
                    //check for min max amount allowed
                    if (amtTransfer <= maxAmountAllowed && amtTransfer >= minAmountAllowed)
                    {

                        //check for valid balance before withdraw
                        if (transferFromAcct.getBalance() - amtTransfer < 0)
                        {
                            bigDisplayLBL.Text = "Baki untuk dipindahkan tidak mencukupi\nSila tekan semula jumlah untuk dipindahkan";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else
                        {
                            //check for account transfer limit balance
                            double afterTransfer = transferFromAcct.getFundTransferLimit() - amtTransfer;

                            if (afterTransfer < maxAcctTransferLimit && afterTransfer >= 0)
                            {
                                nextStep = new FundTransferConfirmation(mainForm, language, acctNo, cardNo, amtTransfer);
                            }
                            else
                            {
                                bigDisplayLBL.Text = "Had maksimum telah dicapai\nHad akaun untuk hari ini: $" + transferFromAcct.getFundTransferLimit();
                                pauseforMilliseconds(3000);
                                resetDisplay();
                            }

                        }
                    }
                    else
                    {
                        bigDisplayLBL.Text = "$" + String.Format("{0:0.00}", amtTransfer) + " tidak dibenarkan\nSile tekan semula jumlah untuk dipindahkan";

                        pauseforMilliseconds(3000);
                        resetDisplay();
                    }
                }
                else if (language.Equals("CHINESE"))
                {
                    //check for min max amount allowed
                    if (amtTransfer <= maxAmountAllowed && amtTransfer >= minAmountAllowed)
                    {

                        //check for valid balance before withdraw
                        if (transferFromAcct.getBalance() - amtTransfer < 0)
                        {
                            bigDisplayLBL.Text = "转移不足的余额\n 请重新输入转账金额";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else
                        {
                            //check for account transfer limit balance
                            double afterTransfer = transferFromAcct.getFundTransferLimit() - amtTransfer;

                            if (afterTransfer < maxAcctTransferLimit && afterTransfer >= 0)
                            {
                                nextStep = new FundTransferConfirmation(mainForm, language, acctNo, cardNo, amtTransfer);
                            }
                            else
                            {
                                bigDisplayLBL.Text = "最大限度地, 今天不能转移\n限于今天的帐户限制: $" + transferFromAcct.getFundTransferLimit();
                                pauseforMilliseconds(3000);
                                resetDisplay();
                            }

                        }
                    }
                    else
                    {
                        bigDisplayLBL.Text = "$" + String.Format("{0:0.00}", amtTransfer) + " 不允许\n请重新输入汇款金额";

                        pauseforMilliseconds(3000);
                        resetDisplay();
                    }
                }
                else //ENGLISH
                {
                    //check for min max amount allowed
                    if (amtTransfer <= maxAmountAllowed && amtTransfer >= minAmountAllowed)
                    {

                        //check for valid balance before withdraw
                        if (transferFromAcct.getBalance() - amtTransfer < 0)
                        {
                            bigDisplayLBL.Text = "Insufficent balance to transfer\n Please re-enter amount to transfer";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else
                        {
                            //check for account transfer limit balance
                            double afterTransfer = transferFromAcct.getFundTransferLimit() - amtTransfer;

                            if (afterTransfer < maxAcctTransferLimit && afterTransfer >= 0)
                            {
                                nextStep = new FundTransferConfirmation(mainForm, language, acctNo, cardNo, amtTransfer);
                            }
                            else
                            {
                                bigDisplayLBL.Text = "Maximum limit reached, unable to transfer for the day\nAccount limit left for today: $" + transferFromAcct.getFundTransferLimit();
                                pauseforMilliseconds(3000);
                                resetDisplay();
                            }

                        }
                    }
                    else
                    {
                        if (amtTransfer > maxAmountAllowed)
                            bigDisplayLBL.Text = "$" + string.Format("{0:0.00}", amtTransfer) + " exceed maximum amount allowed of $" + string.Format("{0:0.00}", maxAmountAllowed) + "\nPlease re-enter amount to transfer";
                        else
                            bigDisplayLBL.Text = "$" + string.Format("{0:0.00}", amtTransfer) + " below minimum amount allowed of $" + string.Format("{0:0.00}", minAmountAllowed) + "\nPlease re-enter amount to transfer";

                        pauseforMilliseconds(3000);
                        resetDisplay();
                    }
                }
            }
            else
                theCardReader.ejectCard();

            return nextStep;
        }

        private void resetDisplay() //resetting display function
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
                bigDisplayLBL.Text = "Sila tekan jumlah untuk dipindahkan dari " + this.acctNo + "\nke " + this.cardNo;
                smallDisplayLBL.Text = amountEnteredTxt;
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "请输入转账金额从 " + this.acctNo + "\n至 " + this.cardNo;
                smallDisplayLBL.Text = amountEnteredTxt;
            }
            else
            {
                bigDisplayLBL.Text = "Please enter amount to transfer from " + this.acctNo + "\nto " + this.cardNo;
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
