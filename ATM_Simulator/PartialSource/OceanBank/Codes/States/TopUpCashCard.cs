using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBank
{
    class TopUpCashCard : State
    {
        public TopUpCashCard(GUIforATM mainForm, string language, string acctNo, double amtTopup) : base (mainForm, language)
        {
            bigDisplayLBL.Text = "Cash card inserted, ready to Top Up";
        }
    }
}
