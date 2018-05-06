using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using System.Timers;

namespace OceanBank
{
    class EnterAmtToWithdrawState : State
    {
        //Timer
        Timer aTimer;
        int counter = 30;
        bool session = true;

        private string amountEnteredTxt;
        private string acctNo;
        Account accountLimit;
        double maxAccountLimit;

        //$$ amount display 
        Char[] ch;
        string newString;
        string amtLength;
        string increaseAmt;
        int cnt = 0;
        int firstHalf = 0;

        public EnterAmtToWithdrawState(GUIforATM mainForm, string language, string acctNo) : base(mainForm, language)
        {
            this.acctNo = acctNo;
            this.accountLimit = theCard.getAcctUsingAcctNo(acctNo);

            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
            aTimer.SynchronizingObject = mainForm;
            aTimer.Interval = 1000;
            aTimer.Start();


            maxAccountLimit = 1000;

            //Get and set datetime properties
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            DateTime now = DateTime.Now;

            if(now > start && now < end)
            {
                //smallDisplayLBL.Text = "Current time:" + now + " Time within range";
            }else
            {
                //smallDisplayLBL.Text = "Current time:" + now + " Time out of range";
                accountLimit.resetWithdrawalLimit();
                    
            }

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Keluarkan wang tunai dari akaun " + acctNo + "\nTekan jumlah wang untuk dikeluarkan\n\nJumlah minimum $20";
                smallDisplayLBL.Text = "00.00";
                left1BTN.Text = "$20"; left2BTN.Text = "$50"; left3BTN.Text = "$100"; left4BTN.Text = "$200";
                right1BTN.Text = "Ok"; right2BTN.Text = "Padam"; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu Utama";


                amountEnteredTxt = "";

                //$$ amount display
                amtLength = "";
                ch = smallDisplayLBL.Text.ToCharArray();
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "退出帐户 " + acctNo + "\n输入金额退出\n\n最低金额20美元";
                smallDisplayLBL.Text = "00.00";
                left1BTN.Text = "$20"; left2BTN.Text = "$50"; left3BTN.Text = "$100"; left4BTN.Text = "$200";
                right1BTN.Text = "Ok"; right2BTN.Text = "抹去"; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";


                amountEnteredTxt = "";

                //$$ amount display
                amtLength = "";
                ch = smallDisplayLBL.Text.ToCharArray();
            }
            else
            {
                bigDisplayLBL.Text = "Withdraw from Account " + acctNo + "\nEnter amount to withdraw\n\nMinimum amount of $20";
                smallDisplayLBL.Text = "00.00";
                left1BTN.Text = "$20"; left2BTN.Text = "$50"; left3BTN.Text = "$100"; left4BTN.Text = "$200";
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
            resetDisplay();
            return this;
        }

        public override State handleLeft1BTNClick()
        {
            double withdrawAmt;
            Account withdrawFromAcct;

            State nextStep = this;

            if (session)
            {
                withdrawAmt = 20;
                withdrawFromAcct = theCard.getAcctUsingAcctNo(acctNo);

                if (language.Equals("MALAY"))
                {
                    if (withdrawFromAcct.getBalance() - withdrawAmt < 0)
                    {
                        bigDisplayLBL.Text = "Jumlah baki tidak mencukupi\n Sila tekan semula jumlah wang untuk dikeluarkan";
                    }
                    else
                    {

                        //check for account limit balance
                        double afterwithrawal = withdrawFromAcct.getWithdrawalLimit() - withdrawAmt;

                        if (afterwithrawal < maxAccountLimit && afterwithrawal >= 0)
                        {
                            withdrawFromAcct.deductToWithdrawalLimit(withdrawAmt);
                            withdrawFromAcct.withdraw(withdrawAmt);

                            nextStep = new TakeCashState(mainForm, language, acctNo);

                        }
                        else if (withdrawFromAcct.getWithdrawalLimit() == 0) //Max limit reached
                        {
                            bigDisplayLBL.Text = "Maximum withdrawal limit reached, unable to withdraw";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else if (afterwithrawal < maxAccountLimit && afterwithrawal <= 0) //Exceed limit Language Change
                        {
                            bigDisplayLBL.Text = "Had pengeluaran melebihi, tidak dapat dikeluarkan\nHad akaun yang ditinggalkan untuk hari ini: $" + withdrawFromAcct.getWithdrawalLimit();
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }

                    }
                }
                else if (language.Equals("CHINESE"))
                {
                    if (withdrawFromAcct.getBalance() - withdrawAmt < 0)
                    {
                        bigDisplayLBL.Text = "余额不足以提取资金\n 请重新输入金额退出";
                    }
                    else
                    {

                        //check for account limit balance
                        double afterwithrawal = withdrawFromAcct.getWithdrawalLimit() - withdrawAmt;

                        if (afterwithrawal < maxAccountLimit && afterwithrawal >= 0)
                        {
                            withdrawFromAcct.deductToWithdrawalLimit(withdrawAmt);
                            withdrawFromAcct.withdraw(withdrawAmt);

                            nextStep = new TakeCashState(mainForm, language, acctNo);

                            return nextStep;
                        }
                        else if (withdrawFromAcct.getWithdrawalLimit() == 0) //Max limit reached
                        {
                            bigDisplayLBL.Text = "Maximum withdrawal limit reached, unable to withdraw";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else if (afterwithrawal < maxAccountLimit && afterwithrawal <= 0) //Exceed limit Language Change
                        {
                            bigDisplayLBL.Text = "超出提款限额，无法提款\n今天剩余的帐户限额: $" + withdrawFromAcct.getWithdrawalLimit();
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }


                    }
                }
                else
                {
                    if (withdrawFromAcct.getBalance() - withdrawAmt < 0)
                    {
                        bigDisplayLBL.Text = "Insufficent balance to withdraw\n Please re-enter amount to withdraw";
                    }
                    else
                    {

                        //check for account limit balance
                        double afterwithrawal = withdrawFromAcct.getWithdrawalLimit() - withdrawAmt;

                        if (afterwithrawal < maxAccountLimit && afterwithrawal >= 0)
                        {
                            withdrawFromAcct.deductToWithdrawalLimit(withdrawAmt);
                            withdrawFromAcct.withdraw(withdrawAmt);

                            nextStep = new TakeCashState(mainForm, language, acctNo);

                        }
                        else if (withdrawFromAcct.getWithdrawalLimit() == 0) //Max limit reached
                        {
                            bigDisplayLBL.Text = "Maximum withdrawal limit reached, unable to withdraw";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else if (afterwithrawal < maxAccountLimit && afterwithrawal <= 0) //Exceed limit
                        {
                            bigDisplayLBL.Text = "Withdrawal limit exceeded, unable to withdraw\nAccount limit left for today: $" + withdrawFromAcct.getWithdrawalLimit();
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                    }
                }
            }
            else
                theCardReader.ejectCard();
            

            return nextStep;
        }

        public override State handleLeft2BTNClick()
        {
            double withdrawAmt;
            Account withdrawFromAcct;

            State nextStep = this;

            if (session)
            {
                withdrawAmt = 50;
                withdrawFromAcct = theCard.getAcctUsingAcctNo(acctNo);

                if (language.Equals("MALAY"))
                {
                    if (withdrawFromAcct.getBalance() - withdrawAmt < 0)
                    {
                        bigDisplayLBL.Text = "Jumlah baki tidak mencukupi\n Sila tekan semula jumlah wang untuk dikeluarkan";
                    }
                    else
                    {
                        //check for account limit balance
                        double afterwithrawal = withdrawFromAcct.getWithdrawalLimit() - withdrawAmt;

                        if (afterwithrawal < maxAccountLimit && afterwithrawal >= 0)
                        {
                            withdrawFromAcct.deductToWithdrawalLimit(withdrawAmt);
                            withdrawFromAcct.withdraw(withdrawAmt);

                            nextStep = new TakeCashState(mainForm, language, acctNo);

                        }
                        else if (withdrawFromAcct.getWithdrawalLimit() == 0) //Max limit reached
                        {
                            bigDisplayLBL.Text = "Maximum withdrawal limit reached, unable to withdraw";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else if (afterwithrawal < maxAccountLimit && afterwithrawal <= 0) //Exceed limit Language Change
                        {
                            bigDisplayLBL.Text = "Had pengeluaran melebihi, tidak dapat dikeluarkan\nHad akaun yang ditinggalkan untuk hari ini: $" + withdrawFromAcct.getWithdrawalLimit();
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                    }
                }
                else if (language.Equals("CHINESE"))
                {
                    if (withdrawFromAcct.getBalance() - withdrawAmt < 0)
                    {
                        bigDisplayLBL.Text = "余额不足以提取资金\n 请重新输入金额退出";
                    }
                    else
                    {
                        //check for account limit balance
                        double afterwithrawal = withdrawFromAcct.getWithdrawalLimit() - withdrawAmt;

                        if (afterwithrawal < maxAccountLimit && afterwithrawal >= 0)
                        {
                            withdrawFromAcct.deductToWithdrawalLimit(withdrawAmt);
                            withdrawFromAcct.withdraw(withdrawAmt);

                            nextStep = new TakeCashState(mainForm, language, acctNo);

                        }
                        else if (withdrawFromAcct.getWithdrawalLimit() == 0) //Max limit reached
                        {
                            bigDisplayLBL.Text = "Maximum withdrawal limit reached, unable to withdraw";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else if (afterwithrawal < maxAccountLimit && afterwithrawal <= 0) //Exceed limit Language Change
                        {
                            bigDisplayLBL.Text = "超出提款限额，无法提款\n今天剩余的帐户限额: $" + withdrawFromAcct.getWithdrawalLimit();
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                    }
                }
                else //ENGLISH
                {
                    if (withdrawFromAcct.getBalance() - withdrawAmt < 0)
                    {
                        bigDisplayLBL.Text = "Insufficent balance to withdraw\n Please re-enter amount to withdraw";
                    }
                    else
                    {
                        //check for account limit balance
                        double afterwithrawal = withdrawFromAcct.getWithdrawalLimit() - withdrawAmt;

                        if (afterwithrawal < maxAccountLimit && afterwithrawal >= 0)
                        {
                            withdrawFromAcct.deductToWithdrawalLimit(withdrawAmt);
                            withdrawFromAcct.withdraw(withdrawAmt);

                            nextStep = new TakeCashState(mainForm, language, acctNo);

                        }
                        else if (withdrawFromAcct.getWithdrawalLimit() == 0) //Max limit reached
                        {
                            bigDisplayLBL.Text = "Maximum withdrawal limit reached, unable to withdraw";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else if (afterwithrawal < maxAccountLimit && afterwithrawal <= 0) //Exceed limit
                        {
                            bigDisplayLBL.Text = "Withdrawal limit exceeded, unable to withdraw\nAccount limit left for today: $" + withdrawFromAcct.getWithdrawalLimit();
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                    }
                }
            }
            else
                theCardReader.ejectCard();
            

            return nextStep;
        }

        public override State handleLeft3BTNClick()
        {
            double withdrawAmt;
            Account withdrawFromAcct;

            State nextStep = this;

            if (session)
            {
                withdrawAmt = 100;
                withdrawFromAcct = theCard.getAcctUsingAcctNo(acctNo);

                if (language.Equals("MALAY"))
                {
                    if (withdrawFromAcct.getBalance() - withdrawAmt < 0)
                    {
                        bigDisplayLBL.Text = "Jumlah baki tidak mencukupi\n Sila tekan semula jumlah wang untuk dikeluarkan";
                    }
                    else
                    {
                        //check for account limit balance
                        double afterwithrawal = withdrawFromAcct.getWithdrawalLimit() - withdrawAmt;

                        if (afterwithrawal < maxAccountLimit && afterwithrawal >= 0)
                        {
                            withdrawFromAcct.deductToWithdrawalLimit(withdrawAmt);
                            withdrawFromAcct.withdraw(withdrawAmt);

                            nextStep = new TakeCashState(mainForm, language, acctNo);

                        }
                        else if (withdrawFromAcct.getWithdrawalLimit() == 0) //Max limit reached
                        {
                            bigDisplayLBL.Text = "Maximum withdrawal limit reached, unable to withdraw";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else if (afterwithrawal < maxAccountLimit && afterwithrawal <= 0) //Exceed limit Language Change
                        {
                            bigDisplayLBL.Text = "Had pengeluaran melebihi, tidak dapat dikeluarkan\nHad akaun yang ditinggalkan untuk hari ini: $" + withdrawFromAcct.getWithdrawalLimit();
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                    }
                }
                else if (language.Equals("CHINESE"))
                {
                    if (withdrawFromAcct.getBalance() - withdrawAmt < 0)
                    {
                        bigDisplayLBL.Text = "余额不足以提取资金\n 请重新输入金额退出";
                    }
                    else
                    {
                        //check for account limit balance
                        double afterwithrawal = withdrawFromAcct.getWithdrawalLimit() - withdrawAmt;

                        if (afterwithrawal < maxAccountLimit && afterwithrawal >= 0)
                        {
                            withdrawFromAcct.deductToWithdrawalLimit(withdrawAmt);
                            withdrawFromAcct.withdraw(withdrawAmt);

                            nextStep = new TakeCashState(mainForm, language, acctNo);

                        }
                        else if (withdrawFromAcct.getWithdrawalLimit() == 0) //Max limit reached
                        {
                            bigDisplayLBL.Text = "Maximum withdrawal limit reached, unable to withdraw";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else if (afterwithrawal < maxAccountLimit && afterwithrawal <= 0) //Exceed limit Language Change
                        {
                            bigDisplayLBL.Text = "超出提款限额，无法提款\n今天剩余的帐户限额: $" + withdrawFromAcct.getWithdrawalLimit();
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                    }
                }
                else //ENGLISH
                {
                    if (withdrawFromAcct.getBalance() - withdrawAmt < 0)
                    {
                        bigDisplayLBL.Text = "Insufficent balance to withdraw\n Please re-enter amount to withdraw";
                    }
                    else
                    {
                        //check for account limit balance
                        double afterwithrawal = withdrawFromAcct.getWithdrawalLimit() - withdrawAmt;

                        if (afterwithrawal < maxAccountLimit && afterwithrawal >= 0)
                        {
                            withdrawFromAcct.deductToWithdrawalLimit(withdrawAmt);
                            withdrawFromAcct.withdraw(withdrawAmt);

                            nextStep = new TakeCashState(mainForm, language, acctNo);

                        }
                        else if (withdrawFromAcct.getWithdrawalLimit() == 0) //Max limit reached
                        {
                            bigDisplayLBL.Text = "Maximum withdrawal limit reached, unable to withdraw";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else if (afterwithrawal < maxAccountLimit && afterwithrawal <= 0) //Exceed limit
                        {
                            bigDisplayLBL.Text = "Withdrawal limit exceeded, unable to withdraw\nAccount limit left for today: $" + withdrawFromAcct.getWithdrawalLimit();
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                    }
                }
            }
            else
                theCardReader.ejectCard();
            

            return nextStep;
        }

        public override State handleLeft4BTNClick()
        {
            double withdrawAmt;
            Account withdrawFromAcct;

            State nextStep = this;

            if (session)
            {
                withdrawAmt = 200;
                withdrawFromAcct = theCard.getAcctUsingAcctNo(acctNo);

                if (language.Equals("MALAY"))
                {
                    if (withdrawFromAcct.getBalance() - withdrawAmt < 0)
                    {
                        bigDisplayLBL.Text = "Jumlah baki tidak mencukupi\n Sila tekan semula jumlah wang untuk dikeluarkan";
                    }
                    else
                    {
                        //check for account limit balance
                        double afterwithrawal = withdrawFromAcct.getWithdrawalLimit() - withdrawAmt;

                        if (afterwithrawal < maxAccountLimit && afterwithrawal >= 0)
                        {
                            withdrawFromAcct.deductToWithdrawalLimit(withdrawAmt);
                            withdrawFromAcct.withdraw(withdrawAmt);

                            nextStep = new TakeCashState(mainForm, language, acctNo);

                        }
                        else if (withdrawFromAcct.getWithdrawalLimit() == 0) //Max limit reached
                        {
                            bigDisplayLBL.Text = "Maximum withdrawal limit reached, unable to withdraw";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else if (afterwithrawal < maxAccountLimit && afterwithrawal <= 0) //Exceed limit Language Change
                        {
                            bigDisplayLBL.Text = "Had pengeluaran melebihi, tidak dapat dikeluarkan\nHad akaun yang ditinggalkan untuk hari ini: $" + withdrawFromAcct.getWithdrawalLimit();
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                    }
                }
                else if (language.Equals("CHINESE"))
                {
                    if (withdrawFromAcct.getBalance() - withdrawAmt < 0)
                    {
                        bigDisplayLBL.Text = "余额不足以提取资金\n 请重新输入金额退出";
                    }
                    else
                    {
                        //check for account limit balance
                        double afterwithrawal = withdrawFromAcct.getWithdrawalLimit() - withdrawAmt;

                        if (afterwithrawal < maxAccountLimit && afterwithrawal >= 0)
                        {
                            withdrawFromAcct.deductToWithdrawalLimit(withdrawAmt);
                            withdrawFromAcct.withdraw(withdrawAmt);

                            nextStep = new TakeCashState(mainForm, language, acctNo);

                        }
                        else if (withdrawFromAcct.getWithdrawalLimit() == 0) //Max limit reached
                        {
                            bigDisplayLBL.Text = "Maximum withdrawal limit reached, unable to withdraw";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else if (afterwithrawal < maxAccountLimit && afterwithrawal <= 0) //Exceed limit Language Change
                        {
                            bigDisplayLBL.Text = "超出提款限额，无法提款\n今天剩余的帐户限额: $" + withdrawFromAcct.getWithdrawalLimit();
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                    }
                }
                else //ENGLISH
                {
                    if (withdrawFromAcct.getBalance() - withdrawAmt < 0)
                    {
                        bigDisplayLBL.Text = "Insufficent balance to withdraw\n Please re-enter amount to withdraw";
                    }
                    else
                    {
                        //check for account limit balance
                        double afterwithrawal = withdrawFromAcct.getWithdrawalLimit() - withdrawAmt;

                        if (afterwithrawal < maxAccountLimit && afterwithrawal >= 0)
                        {
                            withdrawFromAcct.deductToWithdrawalLimit(withdrawAmt);
                            withdrawFromAcct.withdraw(withdrawAmt);

                            nextStep = new TakeCashState(mainForm, language, acctNo);

                        }
                        else if (withdrawFromAcct.getWithdrawalLimit() == 0) //Max limit reached
                        {
                            bigDisplayLBL.Text = "Maximum withdrawal limit reached, unable to withdraw";
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                        else if (afterwithrawal < maxAccountLimit && afterwithrawal <= 0) //Exceed limit
                        {
                            bigDisplayLBL.Text = "Withdrawal limit exceeded, unable to withdraw\nAccount limit left for today: $" + withdrawFromAcct.getWithdrawalLimit();
                            pauseforMilliseconds(3000);
                            resetDisplay();
                        }
                    }
                }
            }
            else
                theCardReader.ejectCard();
            

            return nextStep;
        }

        public override State handleRight1BTNClick()
        {
            double withdrawAmt;
            Account withdrawFromAcct;
            double maxAmountAllowed = 1000;
            double minAmountAllowed = 20;
            double validateAmt;

            State nextStep = this;

            if (session)
            {
                withdrawAmt = Convert.ToDouble(amountEnteredTxt);

                withdrawFromAcct = theCard.getAcctUsingAcctNo(acctNo);

                validateAmt = withdrawAmt % 10;



                //check amountEnteredTxt for valid amount and decimal value
                if (validateAmt == 0 && amountEnteredTxt.EndsWith("0"))
                {

                    //check for min max amount allowed
                    if (withdrawAmt <= maxAmountAllowed && withdrawAmt >= minAmountAllowed)
                    {
                        //check for valid balance before withdraw
                        if (withdrawFromAcct.getBalance() - withdrawAmt < 0)
                        {
                            if (language.ToUpper().Equals("CHINESE"))   //Require change
                                bigDisplayLBL.Text = "不足的余额退出\n请重新输入金额退出";
                            else if (language.ToUpper().Equals("MALAY"))    //Require change
                                bigDisplayLBL.Text = "Baki tidak mencukupi untuk menarik diri\nSila masukkan semula jumlah untuk menarik balik";
                            else
                                bigDisplayLBL.Text = "Insufficent balance to withdraw\nPlease re-enter amount to withdraw";

                            pauseforMilliseconds(3000);
                            resetDisplay();
                            
                            //return nextStep;
                        }
                        else
                        {
                            //check for account limit balance
                            double afterwithrawal = withdrawFromAcct.getWithdrawalLimit() - withdrawAmt;

                            if (afterwithrawal < maxAccountLimit && afterwithrawal >= 0)
                            {
                                withdrawFromAcct.deductToWithdrawalLimit(withdrawAmt);
                                withdrawFromAcct.withdraw(withdrawAmt);

                                nextStep = new TakeCashState(mainForm, language, acctNo);

                            }
                            else if (withdrawFromAcct.getWithdrawalLimit() == 0) //Max limit reached
                            {
                                if (language.ToUpper().Equals("CHINESE"))   //require change
                                    bigDisplayLBL.Text = "达到最大取款限额，无法撤回";
                                else if (language.ToUpper().Equals("MALAY"))    //require chanage
                                    bigDisplayLBL.Text = "Had pengeluaran maksimum dicapai, tidak dapat dikeluarkan";
                                else
                                    bigDisplayLBL.Text = "Maximum withdrawal limit reached, unable to withdraw";

                                pauseforMilliseconds(3000);
                                resetDisplay();
                                
                            }
                            else if (afterwithrawal < maxAccountLimit && afterwithrawal <= 0) //Exceed limit
                            {
                                if(language.ToUpper().Equals("CHINESE"))    //require change
                                    bigDisplayLBL.Text = "超出提款限额，无法提款\n今天剩余的帐户限额: $" + string.Format("{0:0.00}", withdrawFromAcct.getWithdrawalLimit());
                                else if(language.ToUpper().Equals("MALAY")) //require change
                                    bigDisplayLBL.Text = "Had pengeluaran melebihi, tidak dapat dikeluarkan\nHad akaun yang ditinggalkan untuk hari ini: $" + string.Format("{0:0.00}", withdrawFromAcct.getWithdrawalLimit());
                                else
                                    bigDisplayLBL.Text = "Withdrawal limit exceeded, unable to withdraw\nAccount limit left for today: $" + string.Format("{0:0.00}", withdrawFromAcct.getWithdrawalLimit());

                                pauseforMilliseconds(3000);
                                resetDisplay();
                            }

                        }
                    }
                    else
                    {
                        if(language.ToUpper().Equals("CHINESE"))    //require change
                            bigDisplayLBL.Text = "最低金额 $20 被允许\n最高金额 $1000 被允许";
                        else if (language.ToUpper().Equals("MALAY"))    //require change
                            bigDisplayLBL.Text = "Jumlah minimum $20 dibenarkan\nJumlah maksimum $1000 dibenarkan";
                        else
                            bigDisplayLBL.Text = "Minimum amount of $20 is allowed\nMaximum amount of $1000 is allowed";

                        pauseforMilliseconds(4000);
                        resetDisplay();
                    }

                }
                else
                {
                    //bigDisplayLBL.Text = "Unable to withdraw $" + String.Format("{0:0.00}", Convert.ToDouble(amountEnteredTxt)) + ", minimum is $20";
                    if(language.ToUpper().Equals("CHINESE"))
                        bigDisplayLBL.Text = "无法退出 $" + String.Format("{0:0.00}", Convert.ToDouble(amountEnteredTxt)) + ", 最小的是 $20";
                    else if (language.ToUpper().Equals("MALAY"))
                        bigDisplayLBL.Text = "Tidak dapat mengeluarkan $" + String.Format("{0:0.00}", Convert.ToDouble(amountEnteredTxt)) + ", minimum adalah $20";
                    else
                        bigDisplayLBL.Text = "Unable to withdraw $" + String.Format("{0:0.00}", Convert.ToDouble(amountEnteredTxt)) + ", minimum is $20";
                    pauseforMilliseconds(3000);
                    resetDisplay();
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

            bigDisplayLBL.Text = "Withdraw from Account " + acctNo + "\nEnter amount to withdraw\n\nMinimum amount of $20";
            smallDisplayLBL.Text = amountEnteredTxt;
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
                    if (cnt < 2 && firstHalf < 4)  //digits before '.' 
                    {
                        amountEnteredTxt += k;
                        amtLength += k;
                    }
                    if (amtLength.Length > 2 && cnt == 0 && firstHalf != 4) // allow max 4 digits before '.'
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
