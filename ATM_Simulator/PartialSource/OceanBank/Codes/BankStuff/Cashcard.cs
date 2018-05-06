using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBank
{
    public class Cashcard
    {
        private double balance;

        public Cashcard(string balance)
        {
            this.balance = Convert.ToDouble(balance);
        }

        public double getBalance()
        {
            return balance;
        }

        public void topUpCashcard(double amt)
        {
            balance += amt;
        }
    }
}
