using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OceanBank
{
    public class CardReader
    {
        private PictureBox cardReaderControl;
        private static SoundPlayer cardSound;

        static CardReader()
        {
            cardSound = new SoundPlayer(Properties.Resources.cardsound);
            cardSound.Load();
        }

        public CardReader(PictureBox p)
        {

            cardReaderControl = p;
            withoutCard();
        }

        public void withoutCard()
        {
            cardReaderControl.Image = Properties.Resources.CardReaderWithoutCard;
        }

        public void insertCard()
        {
            cardSound.PlaySync();
            cardReaderControl.Image = Properties.Resources.CardReaderWithCard;
        }

        public void ejectCard()
        {
            cardSound.PlaySync();
            cardReaderControl.Image = Properties.Resources.CardReaderEjectCard;
        }

        public void insertCashCard()
        {
            cardSound.PlaySync();
            cardReaderControl.Image = Properties.Resources.CardReaderWithCashCard;
        }

        public void ejectCashcard()
        {
            cardSound.PlaySync();
            cardReaderControl.Image = Properties.Resources.CardReaderEjectCashCard;
        }
    }
}
