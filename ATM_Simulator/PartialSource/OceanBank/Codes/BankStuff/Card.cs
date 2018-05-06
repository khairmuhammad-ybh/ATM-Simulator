using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBank
{
    public class Card
    {
        private string PIN;
        private List<Account> accounts; //List is like an array

        //Card locking system
        private int attempt;
        private bool cardLock;

        //Change PIN
        private string NRIC_FIN;
        private int idAttempt;

        public Card(string p, string n)
        {
            PIN = p;
            accounts = new List<Account>();     //Create a new empty List
            attempt = 3;
            idAttempt = 3;
            cardLock = false;
            NRIC_FIN = n;
        }

        public int getAttempt()
        {
            return attempt;
        }

        public int getIdAttempt()
        {
            return idAttempt;
        }

        public void decreaseAttempt()
        {
            attempt--;
        }

        public void decreseIdAttempt()
        {
            idAttempt--;
        }

        public Boolean isCardLocked()
        {
            return cardLock;
        }

        public void cardLockout(bool lockout)
        {
            cardLock = true;
        }

        public void resetAttempt()
        {
            attempt = 3;
        }

        public void resetIdAttempt()
        {
            idAttempt = 3;
        }

        public string getPIN()
        {
            return PIN;
        }

        public void setPIN(string newPin)
        {
            PIN = newPin;
        }

        public string getNRIC_FIN()
        {
            return NRIC_FIN;
        }

        public void addAccount(Account newAcct)
        {
            accounts.Add(newAcct);      //Add the newAcct to the list
        }

        public int getNumAccounts()
        {
            return accounts.Count;
        }

        public Account getAcctAtIndex(int i)
        {
            if (i >= 0 && i < getNumAccounts())
                return accounts[i];
            else
                return null;
        }

        public Account getAcctUsingAcctNo(string acctNo)
        {
            Account result = null;
            for (int i=0;i<accounts.Count;i++)
            {
                if (accounts[i].getAcctNo() == acctNo)
                {
                    result = accounts[i];
                    break;
                }
            }
            return result;
        }

    }
}
