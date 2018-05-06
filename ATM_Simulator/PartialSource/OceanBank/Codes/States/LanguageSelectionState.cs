using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBank
{
    class LanguageSelectionState : State
    {
        public LanguageSelectionState(GUIforATM mainForm, string language) : base(mainForm, language)
        {
            //if (language.ToUpper() == "CHINESE")
            //{
            //    bigDisplayLBL.Text = "请输入密码";
            //    smallDisplayLBL.Text = "";
            //    left1BTN.Text = "English"; left2BTN.Text = "中文"; left3BTN.Text = ""; left4BTN.Text = "";
            //    right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "";
            //}
            //else //ENGLISH
            //{
                bigDisplayLBL.Text = "Please select your language \n请选择您的语言 \nSila pilih bahasa anda";
                smallDisplayLBL.Text = "";
                left1BTN.Text = "English"; left2BTN.Text = "Malay"; left3BTN.Text = "Chinese"; left4BTN.Text = "";
                right1BTN.Text = ""; right2BTN.Text = ""; right3BTN.Text = ""; right4BTN.Text = "";
            //}
        }

        public override State handleLeft1BTNClick()
        {
            State nextStep = new ValidatePINState(mainForm, "ENGLISH");
            return nextStep;
        }

        public override State handleLeft2BTNClick()
        {
            State nextStep = new ValidatePINState(mainForm, "MALAY");
            return nextStep;
        }

        public override State handleLeft3BTNClick()
        {
            State nextStep = new ValidatePINState(mainForm, "CHINESE");
            return nextStep;
        }
    }
}
