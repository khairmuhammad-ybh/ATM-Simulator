using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OceanBank
{
    public class CashDispenser
    {
        private PictureBox cashDispenserControl;
        private static SoundPlayer clickSound;
        private static SoundPlayer cashSound;
        private static SoundPlayer cashReminderSound;

        static CashDispenser()
        {
            clickSound = new SoundPlayer(Properties.Resources.cardsound);
            clickSound.LoadAsync();
            cashSound = new SoundPlayer(Properties.Resources.cashdispensingsound);
            cashSound.LoadAsync();
            cashReminderSound = new SoundPlayer(Properties.Resources.cashremindersound);
            cashReminderSound.LoadAsync();
        }
        public CashDispenser(PictureBox p)
        {
            cashDispenserControl = p;
        }

        public void withoutCash()
        {
            cashDispenserControl.Image = Properties.Resources.CashDispenserEmpty;
        }

        public void removeCash()
        {
            cashDispenserControl.Image = Properties.Resources.CashDispenserEmpty;
            cashReminderSound.Stop();
            clickSound.PlaySync();
        }

        public void ejectCash()
        {
            cashSound.PlaySync();
            clickSound.PlaySync();
            cashReminderSound.PlayLooping();
            cashDispenserControl.Image = Properties.Resources.CashDispenserWithCash;
        }

        public void depositCashState1()
        {
            cashSound.PlaySync();
            clickSound.PlaySync();
            cashDispenserControl.Image = Properties.Resources.CashDispenserWithCash;
            
        }

        public void depositCashState2()
        {
            cashDispenserControl.Image = Properties.Resources.CashDispenserEmpty;
            clickSound.PlaySync();
        }

        //Depsiting Cash
        public void depositNote2()
        {
            cashDispenserControl.Image = Properties.Resources.Note2;
        }

        public void depositNote5()
        {
            cashDispenserControl.Image = Properties.Resources.Note5;
        }

        public void depositNote10()
        {
            cashDispenserControl.Image = Properties.Resources.Note10;
        }

        public void depositNote50()
        {
            cashDispenserControl.Image = Properties.Resources.Note50;
        }

        public void depositNote100()
        {
            cashDispenserControl.Image = Properties.Resources.Note100;
        }
    }
}
