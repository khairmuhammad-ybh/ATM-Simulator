using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace OceanBank
{
    class TakeCashState : State
    {

        public TakeCashState(GUIforATM mainForm, string language, string acctNo) : base(mainForm, language)
        {
            if(language.Equals("MALAY"))
            {
                Account withdrawFromAcct;

                withdrawFromAcct = theCard.getAcctUsingAcctNo(acctNo);

                bigDisplayLBL.Text = "Baki yang tinggal " + acctNo + "\n$" + string.Format("{0:0.00}", withdrawFromAcct.getBalance()) + "\nSila ambil tunai anda";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "";

                theCashDispenser.ejectCash();
            }
            else if (language.Equals("CHINESE"))
            {
                Account withdrawFromAcct;

                withdrawFromAcct = theCard.getAcctUsingAcctNo(acctNo);

                bigDisplayLBL.Text = "剩余的余额 " + acctNo + "\n$" + string.Format("{0:0.00}", withdrawFromAcct.getBalance()) + "\n请拿你的现金";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "";

                theCashDispenser.ejectCash();
            }
            else
            {
                Account withdrawFromAcct;

                withdrawFromAcct = theCard.getAcctUsingAcctNo(acctNo);

                bigDisplayLBL.Text = "Remaining balance in " + acctNo + "\n$" + string.Format("{0:0.00}", withdrawFromAcct.getBalance()) + "\nPlease take your cash";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "";

                theCashDispenser.ejectCash();
            }
            

        }

        public override State handleLeftPicBoxClick()
        {
            theCashDispenser.removeCash();
            State nextStep = new DisplayMainMenuState(mainForm, language);
            return nextStep;
        }
    }
}
