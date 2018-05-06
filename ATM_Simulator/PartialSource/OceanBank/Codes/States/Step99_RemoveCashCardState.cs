using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OceanBank
{
    class RemoveCashCardState : State
    {
        public RemoveCashCardState(GUIforATM mainForm, string language, string card) : base(mainForm, language)
        {
            if (card == "ATM")
                theCardReader.ejectCard();
            else if (card == "CASHCARD")
                theCardReader.ejectCashcard();

            if (language.ToUpper() == "CHINESE")
            {
                bigDisplayLBL.Text = "感谢您使用OceanBank\n请取回银行卡";
                smallDisplayLBL.Text = "";
                //theCardReader.ejectCashcard();
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "";
            }
            else if (language.ToUpper() == "MALAY")
            {
                bigDisplayLBL.Text = "Terima kasih kerana menggunakan OceanBank\nSila keluarkan kad anda";
                smallDisplayLBL.Text = "";
                //theCardReader.ejectCashcard();
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "";
            }
            else //ENGLISH
            {
                bigDisplayLBL.Text = "Thank you for using OceanBank\nPlease remove card";
                smallDisplayLBL.Text = "";
                //theCardReader.ejectCashcard();
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "";
            }
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
