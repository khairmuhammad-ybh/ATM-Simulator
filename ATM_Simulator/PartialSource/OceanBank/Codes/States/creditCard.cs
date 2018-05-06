using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBank
{
    class creditCard : State
    {
        public creditCard(GUIforATM mainForm, string language) : base(mainForm, language)
        {
            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "Khidmat Lain";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Bayar Kad Kredit"; left2BTN.Text = "Tukar PIN"; left3BTN.Text = "Tambah nilai Kad Tunai"; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu";
            }
            else if (language.Equals("CHINESE"))
            {
                bigDisplayLBL.Text = "更多的服务";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "支付信用卡"; left2BTN.Text = "更改您的PIN码"; left3BTN.Text = "充值现金卡"; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";
            }
            else //ENGLISH
            {
                bigDisplayLBL.Text = "More service";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "Pay Credit card"; left2BTN.Text = "Change PIN"; left3BTN.Text = "Top Up Cash Card"; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Back to Main Menu";
            }
            

        }
    }
}
