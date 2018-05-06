using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OceanBank
{
    abstract class State
    {
        protected GUIforATM mainForm;
        protected Button left1BTN, left2BTN, left3BTN, left4BTN, right1BTN, right2BTN, right3BTN, right4BTN;
        protected Label bigDisplayLBL, smallDisplayLBL;
        protected CardReader theCardReader;
        protected CashDispenser theCashDispenser;
        protected string language;
        protected Card theCard;
        protected Cashcard theCashcard;

        public State(GUIforATM mainForm, string language)
        {
            this.mainForm = mainForm;

            left1BTN = (Button)mainForm.Controls["left1BTN"];
            left2BTN = (Button)mainForm.Controls["left2BTN"];
            left3BTN = (Button)mainForm.Controls["left3BTN"];
            left4BTN = (Button)mainForm.Controls["left4BTN"];

            right1BTN = (Button)mainForm.Controls["right1BTN"];
            right2BTN = (Button)mainForm.Controls["right2BTN"];
            right3BTN = (Button)mainForm.Controls["right3BTN"];
            right4BTN = (Button)mainForm.Controls["right4BTN"];

            bigDisplayLBL = (Label)mainForm.Controls["bigDisplayLBL"];
            smallDisplayLBL = (Label)mainForm.Controls["smallDisplayLBL"];

            theCardReader = mainForm.getCardReader();
            theCashDispenser = mainForm.getCashDispenser();

            this.language = language.ToUpper(); //default language

            theCard = mainForm.getCard();
            theCashcard = mainForm.getCashcard();
        }

        public virtual State handleLeftPicBoxClick() { return this; }
        public virtual State handleRightPicBoxClick() { return this; }
        public virtual State handleKey1BTNClick() { return this; }
        public virtual State handleKey2BTNClick() { return this; }
        public virtual State handleKey3BTNClick() { return this; }
        public virtual State handleKey4BTNClick() { return this; }
        public virtual State handleKey5BTNClick() { return this; }
        public virtual State handleKey6BTNClick() { return this; }
        public virtual State handleKey7BTNClick() { return this; }
        public virtual State handleKey8BTNClick() { return this; }
        public virtual State handleKey9BTNClick() { return this; }
        public virtual State handleKey0BTNClick() { return this; }
        public virtual State handleKeyDecBTNClick() { return this; }
        public virtual State handleKeyClearBTNClick() { return this; }

        public virtual State handleLeft1BTNClick() { return this; }
        public virtual State handleLeft2BTNClick() { return this; }
        public virtual State handleLeft3BTNClick() { return this; }
        public virtual State handleLeft4BTNClick() { return this; }


        public virtual State handleRight1BTNClick() { return this; }
        public virtual State handleRight2BTNClick() { return this; }
        public virtual State handleRight3BTNClick() { return this; }
        public virtual State handleRight4BTNClick() { return this; }
        

        public void pauseforMilliseconds(int n)
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(n);
            });
            t.Wait();
        }

    }
}
