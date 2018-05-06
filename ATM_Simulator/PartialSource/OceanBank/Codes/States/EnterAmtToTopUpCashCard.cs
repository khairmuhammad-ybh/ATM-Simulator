using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OceanBank
{
    class EnterAmtToTopUpCashCard : State
    {
        private string amountEnteredTxt;
        private string acctNo;
        Account account;
        double maxAcctTransferLimit;

        //$$ amount display 
        Char[] ch;
        string newString;
        string amtLength;
        string increaseAmt;
        int cnt = 0;
        int firstHalf = 0;

        public EnterAmtToTopUpCashCard(GUIforATM mainForm, string language, string acctNo) : base(mainForm, language)
        {
            this.acctNo = acctNo;

            bigDisplayLBL.Text = "Please enter amount to top-up";
            smallDisplayLBL.Text = "00.00";
            left1BTN.Text = "Edit Account No."; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
            right1BTN.Text = "Ok"; right2BTN.Text = "Clear"; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";

            amountEnteredTxt = "";

            //$$ amount display
            amtLength = "";
            ch = smallDisplayLBL.Text.ToCharArray();
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

        public override State handleRight1BTNClick()    //Ok button
        {
            double amtTopup;
            Account transferFromAcct;
            double maxAmountAllowed = 100.00;
            double minAmountAllowed = 10.00;

            if (!amountEnteredTxt.Equals(""))
            {
                amtTopup = Convert.ToDouble(amountEnteredTxt);
            }
            else
            {
                amtTopup = 0.00;
            }

            transferFromAcct = theCard.getAcctUsingAcctNo(acctNo);


            //check for min max amount allowed
            if (amtTopup <= maxAmountAllowed && amtTopup >= minAmountAllowed)
            {

                //check for valid balance before withdraw
                if (transferFromAcct.getBalance() - amtTopup < 0)
                {
                    bigDisplayLBL.Text = "Insufficent balance to transfer\n Please re-enter amount to transfer";
                    pauseforMilliseconds(3000);
                    resetDisplay();
                    return this;
                }
                else
                {
                    State nextStep = new WaitForCashCardState(mainForm, language, acctNo, amtTopup);
                    return nextStep;

                }
            }
            else
            {
                bigDisplayLBL.Text = "$" + String.Format("{0:0.00}", amtTopup) + " is not allowed\nPlease re-enter amount to transfer";

                pauseforMilliseconds(3000);
                resetDisplay();

                return this;
            }
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

            bigDisplayLBL.Text = "Please enter amount to top-up";
            smallDisplayLBL.Text = amountEnteredTxt;
        }

        public override State handleRight2BTNClick()
        {
            resetDisplay();

            return this;
        }

        public override State handleRight4BTNClick()
        {
            State nextStep = new DisplayMainMenuState(mainForm, language);
            return nextStep;
        }

        private State processKey(string k)
        {

            State nextStep = this;

            Regex rgx = new Regex("(^\\d{1,4}\\.\\d{1,2}$)");    //USD$ currency format
            if ((amountEnteredTxt.Equals("") && k.Equals(".")) || amountEnteredTxt.Equals("") && k.Equals("0"))
            {
                return nextStep;
            }
            else
            {
                if (cnt < 2 && firstHalf < 4) //digits before '.'
                {
                    amountEnteredTxt += k;
                    amtLength += k;
                }
                else if (cnt < 2 && k.Equals("."))  //starts of digits after '.'
                {
                    amountEnteredTxt += k;
                    amtLength += k;
                }

                //Display manipulation
                if (amtLength.Length == 1)
                {
                    ch[1] = Char.Parse(k);
                    newString = new string(ch);
                    amountEnteredTxt = newString;

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
                else if (!amtLength.Contains(".") && amtLength.Length > 2 && cnt == 0 && firstHalf != 4)
                {

                    newString = new string(ch);
                    increaseAmt = newString.Insert(newString.Length - 3, k);
                    amountEnteredTxt = increaseAmt;
                    ch = increaseAmt.ToCharArray();

                    firstHalf++;
                }

            }

            //check number format
            if (rgx.IsMatch(amountEnteredTxt))
            {
                smallDisplayLBL.Text = amountEnteredTxt;
                return nextStep;
            }
            else
            {
                amountEnteredTxt.Substring(amountEnteredTxt.Length - 1);
                return nextStep;
            }
        }
    }
}
