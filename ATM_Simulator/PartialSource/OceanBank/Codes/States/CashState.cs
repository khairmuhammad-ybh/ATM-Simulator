using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OceanBank
{
    abstract class CashState
    {
        protected CashSelector mainForm;
        protected Label c2Cnt, c5Cnt, c10Cnt, c50Cnt, c100Cnt;
        protected Label c2CntM, c2CntP, c5CntM, c5CntP, c10CntM, c10CntP, c50CntM, c50CntP, c100CntM, c100CntP;
        protected string language;

        public CashState(CashSelector mainForm, string language)
        {
            this.mainForm = mainForm;

            c2Cnt = (Label)mainForm.Controls["c2Cnt"];
            c5Cnt = (Label)mainForm.Controls["c5Cnt"];
            c10Cnt = (Label)mainForm.Controls["c10Cnt"];
            c50Cnt = (Label)mainForm.Controls["c50Cnt"];
            c100Cnt = (Label)mainForm.Controls["c100Cnt"];
            // increment & decrement
            c2CntM = (Label)mainForm.Controls["c2CntM"];
            c2CntP = (Label)mainForm.Controls["c2CntP"];
            c5CntM = (Label)mainForm.Controls["c5CntM"];
            c5CntP = (Label)mainForm.Controls["c5CntP"];
            c10CntM = (Label)mainForm.Controls["c10CntM"];
            c10CntP = (Label)mainForm.Controls["c10CntP"];
            c50CntM = (Label)mainForm.Controls["c50CntM"];
            c50CntP = (Label)mainForm.Controls["c50CntP"];
            c100CntM = (Label)mainForm.Controls["c100CntM"];
            c100CntP = (Label)mainForm.Controls["c100CntP"];

        }

        public virtual CashState handlePicBox1Click() { return this; }
        public virtual CashState handlePicBox2Click() { return this; }
        public virtual CashState handlePicBox3Click() { return this; }
        public virtual CashState handlePicBox4Click() { return this; }
        public virtual CashState handlePicBox5Click() { return this; }
        public virtual CashState handleC2CntMClick() { return this; }
        public virtual CashState handleC2CntPClick() { return this; }
        public virtual CashState handleC5CntMClick() { return this; }
        public virtual CashState handleC5CntPClick() { return this; }
        public virtual CashState handleC10CntMClick() { return this; }
        public virtual CashState handleC10CntPClick() { return this; }
        public virtual CashState handleC50CntMClick() { return this; }
        public virtual CashState handleC50CntPClick() { return this; }
        public virtual CashState handleC100CntMClick() { return this; }
        public virtual CashState handleC100CntPClick() { return this; }

        public virtual CashState handleCashOkClick(string language) { return this; }



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
