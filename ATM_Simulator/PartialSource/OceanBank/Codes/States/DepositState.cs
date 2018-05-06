using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using System.Timers;

namespace OceanBank
{
    class DepositState : State
    {
        //Timer
        Timer aTimer;
        int counter = 30;
        bool session = true;

        private int maxDeposit = 1000;
        private int minDeposit = 10;
        private string acctNo;

        private double depositSum;

        private bool dollar2, dollar5, dollar10, dollar50, dollar100;

        //New deposit sequence
        Cash selectedCash = CashSelectorState.cash; //Null

        public DepositState(GUIforATM mainForm, string language, string acctNo) : base(mainForm, language)
        {
            this.acctNo = acctNo;

            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnElapsedEvent);
            aTimer.SynchronizingObject = mainForm;
            aTimer.Interval = 1000;
            aTimer.Start();

            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Sila masukkan wang tunai";
                left1BTN.Text = "Re-select Account"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = "Padam"; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "请把现金存入存款";
                left1BTN.Text = "Re-select Account"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = "抹去"; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";
            }
            else //ENGLISH
            {
                bigDisplayLBL.Text = "Please insert cash to deposit";
                left1BTN.Text = "Re-select Account"; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = "Ok"; right2BTN.Text = "Clear"; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";
            }

            

            depositSum = 0;
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
            {
                nextStep = new ChooseAcctToDepositState(mainForm, language);
                //Require animation (Eject money)

                //Empty cash array
                selectedCash = CashSelectorState.cash = null;
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

        public override State handleKeyClearBTNClick()
        {
            if (session)
            {
                counter = 30;   //reset timer counter
                resetdepositDisplay();
            }
            
            return this;
        }

        private void resetdepositDisplay()  //Resetting display function
        {
            depositSum = 0;


            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Sila masukkan wang tunai anda";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "请存入现金";
            }
            else //ENGLISH
            {
                bigDisplayLBL.Text = "Please insert cash to deposit";
            }
            
        }

        public override State handleLeftPicBoxClick()   //Changing of deposit sequence
        {
            State nextStep = this;

            if (session)
            {
                //counter = 30;   //reset timer counter
                aTimer.Stop();  //Stop timer

                if (language.Equals("MALAY"))
                {
                    if (depositSum == 0)
                    {
                        (new CashSelector(language, selectedCash)).ShowDialog();

                        //New deposit sequence
                        selectedCash = CashSelectorState.cash; //Not Null

                        //calculate cash

                        for (int i = 0; i < selectedCash.getLength(); i++)
                        {
                            depositSum += Convert.ToDouble(selectedCash.getNotesId(i)) * Convert.ToDouble(selectedCash.getNotesQty(i));

                            if (Convert.ToDouble(selectedCash.getNotesId(i)) == 2 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar2 = true;
                            }
                            else if (Convert.ToDouble(selectedCash.getNotesId(i)) == 5 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar5 = true;
                            }
                            else if (Convert.ToDouble(selectedCash.getNotesId(i)) == 10 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar10 = true;
                            }
                            else if (Convert.ToDouble(selectedCash.getNotesId(i)) == 50 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar50 = true;
                            }
                            else if (Convert.ToDouble(selectedCash.getNotesId(i)) == 100 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar100 = true;
                            }
                        }
                        if (depositSum != 0)
                            bigDisplayLBL.Text = "Setuju untuk masukkan $" + Convert.ToString(depositSum) + " ke " + acctNo;

                        //Animation (Insert cash)

                        if (dollar100)
                        {
                            theCashDispenser.depositNote100();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar100 = false;
                        }
                        else if (dollar50)
                        {
                            theCashDispenser.depositNote50();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar50 = false;
                        }
                        else if (dollar10)
                        {
                            theCashDispenser.depositNote10();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar10 = false;
                        }
                        else if (dollar5)
                        {
                            theCashDispenser.depositNote5();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar5 = false;
                        }
                        else if (dollar2)
                        {
                            theCashDispenser.depositNote2();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar2 = false;
                        }
                    }
                }
                else if (language.Equals("CHINESE"))
                {
                    if (depositSum == 0)
                    {

                        (new CashSelector(language, selectedCash)).ShowDialog();

                        //New deposit sequence
                        selectedCash = CashSelectorState.cash; //Not Null

                        //calculate cash

                        for (int i = 0; i < selectedCash.getLength(); i++)
                        {
                            depositSum += Convert.ToDouble(selectedCash.getNotesId(i)) * Convert.ToDouble(selectedCash.getNotesQty(i));

                            if (Convert.ToDouble(selectedCash.getNotesId(i)) == 2 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar2 = true;
                            }
                            else if (Convert.ToDouble(selectedCash.getNotesId(i)) == 5 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar5 = true;
                            }
                            else if (Convert.ToDouble(selectedCash.getNotesId(i)) == 10 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar10 = true;
                            }
                            else if (Convert.ToDouble(selectedCash.getNotesId(i)) == 50 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar50 = true;
                            }
                            else if (Convert.ToDouble(selectedCash.getNotesId(i)) == 100 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar100 = true;
                            }
                        }
                        if (depositSum != 0)
                            bigDisplayLBL.Text = "确认存款 $" + Convert.ToString(depositSum) + " 至 " + acctNo;

                        //Animation (Insert cash)

                        if (dollar100)
                        {
                            theCashDispenser.depositNote100();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar100 = false;
                        }
                        else if (dollar50)
                        {
                            theCashDispenser.depositNote50();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar50 = false;
                        }
                        else if (dollar10)
                        {
                            theCashDispenser.depositNote10();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar10 = false;
                        }
                        else if (dollar5)
                        {
                            theCashDispenser.depositNote5();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar5 = false;
                        }
                        else if (dollar2)
                        {
                            theCashDispenser.depositNote2();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar2 = false;
                        }
                    }
                }
                else //ENGLISH
                {
                    if (depositSum == 0)
                    {

                        (new CashSelector(language, selectedCash)).ShowDialog();

                        //New deposit sequence
                        selectedCash = CashSelectorState.cash; //Not Null

                        //calculate cash

                        for (int i = 0; i < selectedCash.getLength(); i++)
                        {
                            depositSum += Convert.ToDouble(selectedCash.getNotesId(i)) * Convert.ToDouble(selectedCash.getNotesQty(i));

                            if(Convert.ToDouble(selectedCash.getNotesId(i)) == 2 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0){
                                dollar2 = true;
                            }
                            else if (Convert.ToDouble(selectedCash.getNotesId(i)) == 5 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar5 = true;
                            }
                            else if (Convert.ToDouble(selectedCash.getNotesId(i)) == 10 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar10 = true;
                            }
                            else if (Convert.ToDouble(selectedCash.getNotesId(i)) == 50 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar50 = true;
                            }
                            else if (Convert.ToDouble(selectedCash.getNotesId(i)) == 100 && Convert.ToDouble(selectedCash.getNotesQty(i)) > 0)
                            {
                                dollar100 = true;
                            }
                        }

                        if (depositSum != 0)
                            bigDisplayLBL.Text = "Confirm to deposit $" + Convert.ToString(depositSum) + " to " + acctNo;

                        //Animation (Insert cash)

                        if(dollar100)
                        {
                            theCashDispenser.depositNote100();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar100 = false;
                        }
                        else if (dollar50)
                        {
                            theCashDispenser.depositNote50();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar50 = false;
                        }
                        else if (dollar10)
                        {
                            theCashDispenser.depositNote10();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar10 = false;
                        }
                        else if (dollar5)
                        {
                            theCashDispenser.depositNote5();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar5 = false;
                        }
                        else if (dollar2)
                        {
                            theCashDispenser.depositNote2();
                            pauseforMilliseconds(2000);
                            theCashDispenser.withoutCash();
                            dollar2 = false;
                        }

                        aTimer.Start();

                    }
                }
            }
            else
                theCardReader.ejectCard();

            return nextStep;
        }

        public override State handleRight1BTNClick()
        {
            State nextStep = this;

            if (session)
            {
                if (depositSum != 0)
                {
                    if (depositSum >= minDeposit && depositSum <= maxDeposit)   //check for valid min_max
                    {
                        //Valid
                        double depositAmt;
                        Account depositToAcct;

                        depositAmt = depositSum;
                        depositToAcct = theCard.getAcctUsingAcctNo(acctNo);
                        depositToAcct.deposit(depositAmt);

                        selectedCash = CashSelectorState.cash = null;

                        nextStep = new ShowNewBalance(mainForm, language, acctNo);
                    }
                    else
                    {
                        //Error
                        if (language.ToUpper() == "MALAY")
                        {
                            if (depositSum > maxDeposit)
                                bigDisplayLBL.Text = "Tidak dapat mendeposit $" + depositSum + "\nJumlah deposit maksimum yang diterima adalah $" + string.Format("{0:0.00}", maxDeposit);
                            else
                                bigDisplayLBL.Text = "Tidak dapat mendeposit $" + depositSum + "\nJumlah deposit minima yang diterima adalah $" + string.Format("{0:0.00}", minDeposit);
                        }
                        else if (language.ToUpper() == "CHINESE")
                        {
                            if (depositSum > maxDeposit)
                                bigDisplayLBL.Text = "无法存款 $" + depositSum + "\n最高存款金额是 $" + string.Format("{0:0.00}", maxDeposit);
                            else
                                bigDisplayLBL.Text = "无法存款 $" + depositSum + "\n最低存款金额是 $" + string.Format("{0:0.00}", minDeposit);
                        }
                        else // ENGLISH
                        {
                            if (depositSum > maxDeposit)
                                bigDisplayLBL.Text = "Unable to deposit $" + depositSum + "\nMax deposit amount accepted is $" + string.Format("{0:0.00}", maxDeposit);
                            else
                                bigDisplayLBL.Text = "Unable to deposit $" + depositSum + "\nMin deposit amount accepted is $" + string.Format("{0:0.00}", minDeposit);
                        }

                        bigDisplayLBL.Refresh();

                        pauseforMilliseconds(2000);
                        if (language.ToUpper() == "MALAY")
                            bigDisplayLBL.Text = "Sila masukkan wang tunai untuk deposit";
                        else if (language.ToUpper() == "CHINESE")
                            bigDisplayLBL.Text = "请插入现金存款";
                        else
                            bigDisplayLBL.Text = "Please insert cash to deposit";

                        //rest depositSum
                        depositSum = 0;

                        nextStep = this;
                    }
                }
                else
                {
                    //Error -  value = 0
                    if (language.ToUpper() == "MALAY")
                        bigDisplayLBL.Text = "Sila masukkan tunai sebelum meneruskan";
                    if (language.ToUpper() == "CHINESE")
                        bigDisplayLBL.Text = "请在继续之前插入现金";
                    else
                        bigDisplayLBL.Text = "Please insert cash before continue";
                    bigDisplayLBL.Refresh();

                    pauseforMilliseconds(2000);
                    if (language.ToUpper() == "MALAY")
                        bigDisplayLBL.Text = "Sila masukkan wang tunai untuk deposit";
                    else if (language.ToUpper() == "CHINESE")
                        bigDisplayLBL.Text = "请插入现金存款";
                    else
                        bigDisplayLBL.Text = "Please insert cash to deposit";
                    nextStep = this;
                }
            }
            else
                theCardReader.ejectCard();

            return nextStep;
        }

        public override State handleRight2BTNClick()
        {
            if (session)
                resetdepositDisplay();
            else
                theCardReader.ejectCard();
            return this;
        }

        public override State handleRightPicBoxClick()
        {
            theCardReader.withoutCard();
            State nextStep = new WaitForBankCardState(mainForm, language);
            return nextStep;
        }

    }
}
