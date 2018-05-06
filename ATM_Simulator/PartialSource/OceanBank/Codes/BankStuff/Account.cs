using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBank
{
    public class Account
    {
        private string acctNo;
        private double balance;
        private double withdrawalLimit;
        private double creditTransferLimit;
        private double fundTransferLimit;

        public Account(string a, double b)
        {
            acctNo = a;
            balance = b;
            withdrawalLimit = 1000;
            creditTransferLimit = 1000;
            fundTransferLimit = 1000;
        }

        public string getAcctNo()
        {
            return acctNo;
        }

        public double getBalance()
        {
            return balance;
        }

        public void withdraw(double amt)
        {
            balance = balance - amt;
        }

        public void deposit(double amt)
        {
            balance = balance + amt;
        }

        public void resetWithdrawalLimit()
        {
            withdrawalLimit = 1000;
        }

        public void resetCreditTransferLimit()
        {
            creditTransferLimit = 1000;
        }

        public void resetFundTransferLimit()
        {
            fundTransferLimit = 1000;
        }

        public double getWithdrawalLimit()
        {
            return withdrawalLimit;
        }

        public double getCreditTransferLimit()
        {
            return creditTransferLimit;
        }

        public double getFundTransferLimit()
        {
            return fundTransferLimit;
        }

        public void deductToWithdrawalLimit(double amt)
        {
            withdrawalLimit -= amt;
        }

        public void deductToCreditTransferLimit(double amt)
        {
            creditTransferLimit -= amt;
        }

        public void deductToFundTransferLimit(double amt)
        {
            fundTransferLimit -= amt;
        }
    }
}
