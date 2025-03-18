using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strata
{
    public class border : Panel
    {
        Cursor curside = new Cursor("cursors\\ew_l.cur");
        Cursor curup = new Cursor("cursors\\ns_l.cur");
        string side = "";
        public border(string side)
        {
            this.side = side;
            set_p(side);
            BackColor = Color.DarkOliveGreen;
        }
        private void set_p(string side)
        {
            if (side == "left") { 
                this.Location = new Point(-8, 0);
                this.Width = 25;
                this.Height = 964;
                MouseHover += (sender, e) =>
                {
                    Cursor = curside;
                };

            }
            if (side == "right") {
                this.Location = new Point(1464, 0);
                this.Width = 27;
                this.Height = 964;
                MouseHover += (sender, e) =>
                {
                    Cursor = curside;
                };
            }
            if (side == "up") {
                this.Location = new Point(-8, -7);
                this.Width = 1500;
                this.Height = 27;
                MouseHover += (sender, e) =>
                {
                    Cursor = curup;
                };
            }
            if (side == "down") {
                this.Location = new Point(-8, 943);
                this.Width = 1500;
                this.Height = 27;
                MouseHover += (sender, e) =>
                {
                    Cursor = curup;
                };
            }
        }

       
    }
}
