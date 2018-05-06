using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace OceanBank
{
    public class Cash
    {
        private string[] notesId = new string[5];
        private string[] notesQty = new string[5];

        public Cash(string c2, string c5, string c10, string c50, string c100)
        {
            //using ArrayList
            //this.notesId.Add("2");
            //this.notesQty.Add(c2);

            //this.notesId.Add("5");
            //this.notesQty.Add(c5);

            //this.notesId.Add("10");
            //this.notesQty.Add(c10);

            //this.notesId.Add("50");
            //this.notesQty.Add(c50);

            //this.notesId.Add("100");
            //this.notesQty.Add(c100);

            //using string[]
            this.notesId[0] = "2";
            this.notesQty[0] = c2;

            this.notesId[1] = "5";
            this.notesQty[1]= c5;

            this.notesId[2] = "10";
            this.notesQty[2] = c10;

            this.notesId[3] = "50";
            this.notesQty[3] = c50;

            this.notesId[4] = "100";
            this.notesQty[4] = c100;

        }

        public ArrayList testArray { get; set; }

        //public ArrayList getNotesId()
        //{
        //    return notesId;
        //}

        //public ArrayList getNotesQty()
        //{
        //    return notesQty;
        //}
        
        public int getLength()
        {
            return notesId.Length;
        }

        public string getNotesId(int i)
        {
            return notesId[i];
        }

        public string getNotesQty(int i)
        {
            return notesQty[i];
        }
    }
}
