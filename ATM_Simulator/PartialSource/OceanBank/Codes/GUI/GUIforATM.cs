using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OceanBank
{
    public partial class GUIforATM : Form
    {
        private CardReader theCardReader;
        private CashDispenser theCashDispenser;
        private State currentState;
        private Card theCard;
        private SoundPlayer keysound;
        //cashcard
        private Cashcard theCashcard;

        public GUIforATM(Card c, Cashcard cC)
        {
            InitializeComponent();

            theCard = c;
            theCashcard = cC;

            theCardReader = new CardReader(rightPicBox);
            theCardReader.withoutCard();
            theCashDispenser = new CashDispenser(leftPicBox);
            theCashDispenser.withoutCash();

            keysound = new SoundPlayer(Properties.Resources.keysound);
            keysound.Load();

            currentState = new WaitForBankCardState(this, "ENGLISH");
        }

        public CardReader getCardReader()
        {
            return theCardReader;
        }

        public CashDispenser getCashDispenser()
        {
            return theCashDispenser;
        }
        public Card getCard()
        {
            return theCard;
        }

        public Cashcard getCashcard()
        {
            return theCashcard;
        }

        private void leftPicBox_Click(object sender, EventArgs e)
        {
            currentState = currentState.handleLeftPicBoxClick();
        }

        private void rightPicBox_Click(object sender, EventArgs e)
        {
            currentState = currentState.handleRightPicBoxClick();
        }

        private void key1BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleKey1BTNClick();
        }

        private void key2BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleKey2BTNClick();
        }

        private void key3BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleKey3BTNClick();
        }

        private void key4BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleKey4BTNClick();
        }

        private void key5BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleKey5BTNClick();
        }

        private void key6BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleKey6BTNClick();
        }

        private void key7BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleKey7BTNClick();
        }

        private void key8BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleKey8BTNClick();
        }

        private void key9BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleKey9BTNClick();
        }

        private void key0BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleKey0BTNClick();
        }

        private void left1BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleLeft1BTNClick();
        }

        private void left2BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleLeft2BTNClick();
        }

        private void left3BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleLeft3BTNClick();
        }

        private void left4BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleLeft4BTNClick();
        }


        private void right1BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleRight1BTNClick();
        }

        private void right2BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleRight2BTNClick();
        }

        private void right3BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleRight3BTNClick();
        }

        private void right4BTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleRight4BTNClick();
        }

        private void keyDecBTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleKeyDecBTNClick();
        }

        private void keyClearBTN_Click(object sender, EventArgs e)
        {
            keysound.Play();
            currentState = currentState.handleKeyClearBTNClick();
        }
    }
}
