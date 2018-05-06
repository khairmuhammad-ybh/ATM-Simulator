using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OceanBank
{
    class CashSelectorState : CashState
    {
        private int c2, c5, c10, c50, c100;
        public static Cash cash;

        public CashSelectorState(CashSelector mainForm, string language, Cash cash) : base(mainForm, language)
        {
            //if (language.Equals("CHINESE"))
            //    header.Text = "This is Chinese";
            //else if (language.Equals("MALAY"))
            //    header.Text = "This is Malay";
            //else    //English
            //    header.Text = "This is English";

            if (cash == null)
            {
                c2 = 0;
                c5 = 0;
                c10 = 0;
                c50 = 0;
                c100 = 0;
            }else
            {
                c2 = Convert.ToInt32(cash.getNotesQty(0));
                c5 = Convert.ToInt32(cash.getNotesQty(1));
                c10 = Convert.ToInt32(cash.getNotesQty(2));
                c50 = Convert.ToInt32(cash.getNotesQty(3));
                c100 = Convert.ToInt32(cash.getNotesQty(4));

                //label
                c2Cnt.Text = Convert.ToString(c2);
                c5Cnt.Text = Convert.ToString(c5);
                c10Cnt.Text = Convert.ToString(c10);
                c50Cnt.Text = Convert.ToString(c50);
                c100Cnt.Text = Convert.ToString(c100);
            }
        }

        public override CashState handlePicBox1Click()
        {
            c2Cnt.Text = Convert.ToString(++c2);

            return this;
        }

        public override CashState handlePicBox2Click()
        {
            c5Cnt.Text = Convert.ToString(++c5);

            return this;
        }

        public override CashState handlePicBox3Click()
        {
            c10Cnt.Text = Convert.ToString(++c10);

            return this;
        }

        public override CashState handlePicBox4Click()
        {
            c50Cnt.Text = Convert.ToString(++c50);

            return this;
        }

        public override CashState handlePicBox5Click()
        {
            c100Cnt.Text = Convert.ToString(++c100);

            return this;
        }

        public override CashState handleC2CntMClick()
        {
            if(c2 > 0)
                c2Cnt.Text = Convert.ToString(--c2);
                

            return this;
        }

        public override CashState handleC2CntPClick()
        {
            c2Cnt.Text = Convert.ToString(++c2);

            return this;
        }

        public override CashState handleC5CntMClick()
        {
            if (c5 > 0)
                c5Cnt.Text = Convert.ToString(--c5);

            return this;
        }

        public override CashState handleC5CntPClick()
        {
            c5Cnt.Text = Convert.ToString(++c5);

            return this;
        }

        public override CashState handleC10CntMClick()
        {
            if (c10 > 0)
                c10Cnt.Text = Convert.ToString(--c10);

            return this;
        }

        public override CashState handleC10CntPClick()
        {
            c10Cnt.Text = Convert.ToString(++c10);

            return this;
        }

        public override CashState handleC50CntMClick()
        {
            if (c50 > 0)
                c50Cnt.Text = Convert.ToString(--c50);

            return this;
        }

        public override CashState handleC50CntPClick()
        {
            c50Cnt.Text = Convert.ToString(++c50);

            return this;
        }

        public override CashState handleC100CntMClick()
        {
            if (c100 > 0)
                c100Cnt.Text = Convert.ToString(--c100);

            return this;
        }

        public override CashState handleC100CntPClick()
        {
            c100Cnt.Text = Convert.ToString(++c100);

            return this;
        }

        public override CashState handleCashOkClick(string language)
        {
            if (language.Equals("MALAY"))
            {
                //Create new class for storing notes' quantity
                if (!c2Cnt.Text.Equals("0") || !c5Cnt.Text.Equals("0") || !c10Cnt.Text.Equals("0") || !c50Cnt.Text.Equals("0") || !c100Cnt.Text.Equals("0"))
                    cash = new Cash(c2Cnt.Text, c5Cnt.Text, c10Cnt.Text, c50Cnt.Text, c100Cnt.Text);
                else
                {
                    //MessageBox.Show("No amount selected, continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    DialogResult res = MessageBox.Show("Tiada jumlah yang dipilih, teruskan?", "Pengesahan", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res == DialogResult.Yes)
                    {
                        cash = new Cash(c2Cnt.Text, c5Cnt.Text, c10Cnt.Text, c50Cnt.Text, c100Cnt.Text);
                        mainForm.Close();
                    }
                    if (res == DialogResult.No)
                    {
                        return this;
                    }
                }
            }
            else if (language.Equals("CHINESE"))
            {
                //Create new class for storing notes' quantity
                if (!c2Cnt.Text.Equals("0") || !c5Cnt.Text.Equals("0") || !c10Cnt.Text.Equals("0") || !c50Cnt.Text.Equals("0") || !c100Cnt.Text.Equals("0"))
                    cash = new Cash(c2Cnt.Text, c5Cnt.Text, c10Cnt.Text, c50Cnt.Text, c100Cnt.Text);
                else
                {
                    //MessageBox.Show("No amount selected, continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    DialogResult res = MessageBox.Show("没有选择的金额, 继续?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res == DialogResult.Yes)
                    {
                        cash = new Cash(c2Cnt.Text, c5Cnt.Text, c10Cnt.Text, c50Cnt.Text, c100Cnt.Text);
                        mainForm.Close();
                    }
                    if (res == DialogResult.No)
                    {
                        return this;
                    }
                }
            }
            else //ENGLISH
            {
                //Create new class for storing notes' quantity
                if (!c2Cnt.Text.Equals("0") || !c5Cnt.Text.Equals("0") || !c10Cnt.Text.Equals("0") || !c50Cnt.Text.Equals("0") || !c100Cnt.Text.Equals("0"))
                    cash = new Cash(c2Cnt.Text, c5Cnt.Text, c10Cnt.Text, c50Cnt.Text, c100Cnt.Text);
                else
                {
                    //MessageBox.Show("No amount selected, continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    DialogResult res = MessageBox.Show("No amount selected, continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res == DialogResult.Yes)
                    {
                        cash = new Cash(c2Cnt.Text, c5Cnt.Text, c10Cnt.Text, c50Cnt.Text, c100Cnt.Text);
                        mainForm.Close();
                    }
                    if (res == DialogResult.No)
                    {
                        return this;
                    }
                }
            }
            
            
            mainForm.Close();
            return this;
        }

    }
}
