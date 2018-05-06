using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using System.Timers;

namespace OceanBank
{
    class EnterAmtToTopupCashcardState : State
    {
        //Timer
        Timer aTimer;
        int counter = 30;
        bool session = true;

        private string amountEnteredTxt;
        private string acctNo;

        //topup
        double topupAmt;
        Account topupFromAcct;

        //$$ amount display 
        Char[] ch;
        string newString;
        string amtLength;
        string increaseAmt;
        int cnt = 0;
        int firstHalf = 0;

        public EnterAmtToTopupCashcardState(GUIforATM mainForm, string language, string acctNo) : base (mainForm, language)
        {
            this.acctNo = acctNo;

            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
            aTimer.SynchronizingObject = mainForm;
            aTimer.Interval = 1000;
            aTimer.Start();

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Tekan jumlah bagi nilai tambah";
                smallDisplayLBL.Text = "00.00";
                left1BTN.Text = "$20"; left2BTN.Text = "$50"; left3BTN.Text = "$100"; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = "Padam"; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "输入金额充值";
                smallDisplayLBL.Text = "00.00";
                left1BTN.Text = "$20"; left2BTN.Text = "$50"; left3BTN.Text = "$100"; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = "抹去"; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";
            }
            else
            {
                bigDisplayLBL.Text = "Enter amount to Top up";
                smallDisplayLBL.Text = "00.00";
                left1BTN.Text = "$20"; left2BTN.Text = "$50"; left3BTN.Text = "$100"; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = "Clear"; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";
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

        private void resetDisplay()
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
                bigDisplayLBL.Text = "Tekan jumlah untuk nilai tambah";
                smallDisplayLBL.Text = amountEnteredTxt;
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "输入金额充值";
                smallDisplayLBL.Text = amountEnteredTxt;
            }
            else
            {
                bigDisplayLBL.Text = "Enter amount to Top up";
                smallDisplayLBL.Text = amountEnteredTxt;
            }

            
        }

        public override State handleLeft1BTNClick()
        {
            topupAmt = 20;

            State nextStep = this;

            if (session)
            {
                topupFromAcct = theCard.getAcctUsingAcctNo(acctNo);

                if (language.Equals("MALAY"))
                {
                    if (topupFromAcct.getBalance() - topupAmt < 0)
                    {
                        bigDisplayLBL.Text = "Baki nilai tambah tidak mencukupi\nSila tekan semula jumlah bagi nilai tambah";
                        pauseforMilliseconds(3000);
                        resetDisplay();
                        bigDisplayLBL.Text = "Tekan jumlah bagi nilai tambah";
                    }
                    else
                    {
                        nextStep = new WaitForCashcardState(mainForm, language, acctNo, topupAmt);
                    }
                }
                else if (language.Equals("CHINESE"))
                {
                    if (topupFromAcct.getBalance() - topupAmt < 0)
                    {
                        bigDisplayLBL.Text = "不足以弥补\n请重新输入充值的金额";
                        pauseforMilliseconds(3000);
                        resetDisplay();
                        bigDisplayLBL.Text = "输入充值的金额";
                    }
                    else
                    {
                        nextStep = new WaitForCashcardState(mainForm, language, acctNo, topupAmt);
                    }
                }
                else
                {
                    if (topupFromAcct.getBalance() - topupAmt < 0)
                    {
                        bigDisplayLBL.Text = "Insufficient balance to top-up\nPlease re-enter amount to top-up";
                        pauseforMilliseconds(3000);
                        resetDisplay();
                        bigDisplayLBL.Text = "Enter amount to top-up";
                    }
                    else
                    {
                        nextStep = new WaitForCashcardState(mainForm, language, acctNo, topupAmt);
                    }
                }
            }

            return nextStep;
            
        }

        public override State handleLeft2BTNClick()
        {
            topupAmt = 50;

            State nextStep = this;

            if (session)
            {
                topupFromAcct = theCard.getAcctUsingAcctNo(acctNo);

                if (language.Equals("MALAY"))
                {
                    if (topupFromAcct.getBalance() - topupAmt < 0)
                    {
                        bigDisplayLBL.Text = "Baki nilai tambah tidak mencukupi\nSila tekan semula jumlah bagi nilai tambah";
                        pauseforMilliseconds(3000);
                        resetDisplay();
                        bigDisplayLBL.Text = "Tekan jumlah bagi nilai tambah";
                    }
                    else
                    {
                        nextStep = new WaitForCashcardState(mainForm, language, acctNo, topupAmt);
                    }
                }
                else if (language.Equals("CHINESE"))
                {
                    if (topupFromAcct.getBalance() - topupAmt < 0)
                    {
                        bigDisplayLBL.Text = "不足以弥补\n请重新输入充值的金额";
                        pauseforMilliseconds(3000);
                        resetDisplay();
                        bigDisplayLBL.Text = "输入充值的金额";
                    }
                    else
                    {
                        nextStep = new WaitForCashcardState(mainForm, language, acctNo, topupAmt);
                    }
                }
                else
                {
                    if (topupFromAcct.getBalance() - topupAmt < 0)
                    {
                        bigDisplayLBL.Text = "Insufficent balance to Topup\nPlease re-enter amount to Topup";
                        pauseforMilliseconds(3000);
                        resetDisplay();
                        bigDisplayLBL.Text = "Enter amount to Top up";
                    }
                    else
                    {
                        nextStep = new WaitForCashcardState(mainForm, language, acctNo, topupAmt);
                    }
                }
            }

            return nextStep;
            
        }

        public override State handleLeft3BTNClick()
        {
            topupAmt = 100;

            State nextStep = this;

            if (session)
            {
                topupFromAcct = theCard.getAcctUsingAcctNo(acctNo);

                if (language.Equals("MALAY"))
                {
                    if (topupFromAcct.getBalance() - topupAmt < 0)
                    {
                        bigDisplayLBL.Text = "Baki bagi nilai tambah tidak mencukupi \nTekan semula jumlah bagi niai tambah";
                        pauseforMilliseconds(3000);
                        resetDisplay();
                        bigDisplayLBL.Text = "Tekan jumlah bagi nilai tambah";
                    }
                    else
                    {
                        nextStep = new WaitForCashcardState(mainForm, language, acctNo, topupAmt);
                    }
                }
                else if (language.Equals("CHINESE"))
                {
                    if (topupFromAcct.getBalance() - topupAmt < 0)
                    {
                        bigDisplayLBL.Text = "不足以弥补\n请重新输入充值的金额";
                        pauseforMilliseconds(3000);
                        resetDisplay();
                        bigDisplayLBL.Text = "输入充值的金额";
                    }
                    else
                    {
                        nextStep = new WaitForCashcardState(mainForm, language, acctNo, topupAmt);
                    }
                }
                else
                {
                    if (topupFromAcct.getBalance() - topupAmt < 0)
                    {
                        bigDisplayLBL.Text = "Insufficent balance to Topup\nPlease re-enter amount to Topup";
                        pauseforMilliseconds(3000);
                        resetDisplay();
                        bigDisplayLBL.Text = "Enter amount to Top up";
                    }
                    else
                    {
                        nextStep = new WaitForCashcardState(mainForm, language, acctNo, topupAmt);
                    }
                }
            }
            return nextStep;
            
        }

        public override State handleRight1BTNClick()    //Ok button
        {
            double maxAmountAllowed = 100;
            double minAmountAllowed = 10;
            double validateAmt;

            State nextStep = this;

            if (session)
            {
                topupAmt = Convert.ToDouble(amountEnteredTxt);

                topupFromAcct = theCard.getAcctUsingAcctNo(acctNo);

                validateAmt = topupAmt % 10;

                //check amountEnteredTxt for valid amount and decimal value
                if (validateAmt == 0 && amountEnteredTxt.EndsWith("0"))
                {

                    //check for min max amount allowed
                    if (topupAmt <= maxAmountAllowed && topupAmt >= minAmountAllowed)
                    {
                        //check for valid balance before withdraw
                        if (topupFromAcct.getBalance() - topupAmt < 0)
                        {
                            if (language.Equals("MALAY"))
                            {
                                bigDisplayLBL.Text = "Baki bagi nilai tambah tidak mencukupi \nTekan semula jumlah bagi niai tambah";
                                pauseforMilliseconds(3000);
                                resetDisplay();
                            }
                            else if (language.Equals("CHINESE"))
                            {
                                bigDisplayLBL.Text = "不足以弥补\n请重新输入充值的金额";
                                pauseforMilliseconds(3000);
                                resetDisplay();
                            }
                            else
                            {
                                bigDisplayLBL.Text = "Insufficent balance to Topup\nPlease re-enter amount to Topup";
                                pauseforMilliseconds(3000);
                                resetDisplay();
                            }
                        }
                        else
                        {
                            nextStep = new WaitForCashcardState(mainForm, language, acctNo, topupAmt);
                        }
                    }
                    else
                    {
                        if (language.Equals("MALAY"))
                        {
                            bigDisplayLBL.Text = "Jumlah minimum dibenarkan sebanyak $20\nJumlah maksimum dibenarkan sebanyak $100";
                            pauseforMilliseconds(4000);
                            resetDisplay();
                        }
                        else if (language.Equals("CHINESE"))
                        {
                            bigDisplayLBL.Text = "允许最低金额20美元\n最高允许金额为$ 100";
                            pauseforMilliseconds(4000);
                            resetDisplay();
                        }
                        else
                        {
                            bigDisplayLBL.Text = "Minimum amount of $20 is allowed\nMaximum amount of $100 is allowed";
                            pauseforMilliseconds(4000);
                            resetDisplay();
                        }
                    }


                }
                else
                {
                    if (language.Equals("MALAY"))
                    {
                        bigDisplayLBL.Text = "Jumlah dibenarkan bagi nilai tambah sebanyak $10 - $100 sahaja";
                        pauseforMilliseconds(3000);
                        resetDisplay();
                    }
                    else if (language.Equals("CHINESE"))
                    {
                        bigDisplayLBL.Text = "只能充值$ 10  -  $ 100";
                        pauseforMilliseconds(3000);
                        resetDisplay();
                    }
                    else
                    {
                        bigDisplayLBL.Text = "Can only top-up $10 - $100 only";
                        pauseforMilliseconds(3000);
                        resetDisplay();
                    }
                }
            }

            return nextStep;

        }

        public override State handleRight2BTNClick()
        {
            if (session)
                resetDisplay();
            else
                theCardReader.ejectCard();
            return this;
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
                    return nextStep;
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
