using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OceanBank
{
    class WaitForBankCardState : State
    {
        public WaitForBankCardState(GUIforATM mainForm, string language) : base(mainForm, language)
        {
            bigDisplayLBL.Text = "Welcome to Ocean Bank \n欢迎来到 Ocean bank \nSelamat datang ke Ocean bank";
            smallDisplayLBL.Text = "";

            left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
            right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "";
        }


        public override State handleRightPicBoxClick()
        {
            theCardReader.insertCard();
            State nextStep = new LanguageSelectionState(mainForm, language);
            return nextStep;
        }

    }
}
