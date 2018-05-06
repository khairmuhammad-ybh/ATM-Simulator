using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBank
{
    class ViewAboutInfoState : State
    {
        public ViewAboutInfoState(GUIforATM mainForm, string language) : base(mainForm, language)
        {
            if (language.Equals("MALAY"))
            {
                bigDisplayLBL.Text = "OceanBank adalah antara bank terbaik di Singapura,\nJB, serta Batam !";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "Kembali ke Menu Utama";
            }
            else if (language.Equals("CHINESE"))
            {

            }
            else
            {
                bigDisplayLBL.Text = "OceanBank is the best bank in Singapore,\nJB, and some say Batam !";
                smallDisplayLBL.Text = "";
                left1BTN.Text = ""; left2BTN.Text = ""; left3BTN.Text = ""; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "回到主菜单";
            }
            

        }

        public override State handleRight4BTNClick()
        {
            State nextStep = new DisplayMainMenuState(mainForm, language);
            return nextStep;
        }
    }
}
