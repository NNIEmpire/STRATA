using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strata
{
    public class division : PictureBox
    {
        Cursor curdiv = new Cursor("cursors\\move.cur");
        Cursor curmove = new Cursor("cursors\\Spider Alt.cur");
        public int hp =100;
        public int speed = 10;
        public double rot_speed = Math.PI/90;
        public int attack = 100;
        public string name = "";
        public Point loc = new Point(0,0);
        public int width = 120;
        public int height = 90;
        public Color color = Color.Green;
        public Color BColor = Color.LightGreen;
        public Image image = Image.FromFile("div.png");
        public bool clicked = false;
        public bool moving = false;
        public string side = "";
        public double alpha = 0;
        public int number = -1;
        public double armor = 0.2;
        public string clas;

        public division(int hp, int speed, double rot_speed, int attack, string name, int counter, Point loc, string side, string clas, string gamemode)
        {
            this.clas = clas;
            this.hp = hp;
            this.number = counter;
            this.speed = speed;
            this.rot_speed = rot_speed;
            this.attack = attack;
            if (name == "") { name = counter.ToString(); }
            this.name = name;
            this.loc = loc;
            Image = image;
            SizeMode = PictureBoxSizeMode.StretchImage;
            this.side = side;

            if (this.side == "enemy")
            {
                color = Color.Red;
                BColor = Color.OrangeRed;
            }
            if (this.side == "friend")
            {
                color = Color.Green;
                BColor = Color.LightGreen;
            }

            if (this.side != "enemy" || gamemode != "0")
            {
                MouseEnter += (sender, e) =>
                    {
                        if (clicked == false && moving != true)
                        {
                            Cursor = curdiv;
                            BorderStyle = BorderStyle.FixedSingle;
                            Padding = new Padding(3, 3, 3, 3);
                            BackColor = color;
                        }
                    };
                MouseLeave += (sender, e) =>
                    {
                        if (clicked == false)
                        {
                            Cursor = default;
                            BorderStyle = BorderStyle.None;
                            Padding = new Padding(0, 0, 0, 0);
                            BackColor = color;
                        }
                    };

                Click += (sender, e) =>
                {
                    if (moving == false)
                    {
                        if (clicked == true)
                        {
                            BorderStyle = BorderStyle.None;
                            Padding = new Padding(0, 0, 0, 0);
                            clicked = false;
                            this.Parent.Cursor = default;
                            BackColor = color;
                        }
                        else
                        {
                            clicked = true;
                            this.Parent.Cursor = curmove;
                            Padding = new Padding(8, 8, 8, 8);
                            BackColor = BColor;
                        }
                    }
                };
            }
        }

        public division(string clas, string side, Point loc, double alpha, string name, int n, string gamemode)
        {
            SizeMode = PictureBoxSizeMode.StretchImage;
            this.clas = clas;
            this.side = side;
            this.loc = loc;
            this.number = n;
            this.name = name;
            this.alpha = alpha;
            if (name == "") { name = n.ToString(); }
            
            if (clas == "footman")
            {
                hp = 100; //* longg;
                speed = 10;
                rot_speed = Math.PI / (90); //* longg);
                attack = 100;// + 70* longg;
                width = 120;// + 60* longg;
                height = 90;
                image = Image.FromFile("div.png");
                armor = 0.2;
            }

            if (clas == "cavalry")
            {
                hp = 100; //* longg;
                speed = 16;
                rot_speed = Math.PI / (70); //* longg);
                attack = 120;// + 70* longg;
                width = 100;// + 60* longg;
                height = 100;
                image = Image.FromFile("cav.png");
                armor = 0.4;
            }

            if (clas == "gun")
            {
                hp = 180; //* longg;
                speed = 3;
                rot_speed = Math.PI / (120); //* longg);
                attack = 250;// + 70* longg;
                width = 80;// + 60* longg;
                height = 80;
                image = Image.FromFile("gun.png");
                armor = 1.5;
            }

            if (clas == "wall")
            {
                hp = 100; //* longg;
                speed = 0;
                rot_speed = Math.PI / (100); //* longg);
                attack = 30;// + 70* longg;
                width = 130;// + 60* longg;
                height = 50;
                image = Image.FromFile("wall.png");
                armor = 0.05;
            }




            Image = image;
            if (this.side == "enemy")
            {
                color = Color.Red;
                BColor = Color.OrangeRed;
            }
            if (this.side == "friend")
            {
                color = Color.Green;
                BColor = Color.LightGreen;
            }



            if (this.side != "enemy" || gamemode != "0")
            {
                MouseEnter += (sender, e) =>
            {
                if (clicked == false && moving != true)
                {
                    Cursor = curdiv;
                    BorderStyle = BorderStyle.FixedSingle;
                    Padding = new Padding(3, 3, 3, 3);
                    BackColor = color;
                }
            };
                MouseLeave += (sender, e) =>
                {
                    if (clicked == false)
                    {
                        Cursor = default;
                        BorderStyle = BorderStyle.None;
                        Padding = new Padding(0, 0, 0, 0);
                        BackColor = color;
                    }
                };

                Click += (sender, e) =>
                {
                    if (moving == false)
                    {
                        if (clicked == true)
                        {
                            BorderStyle = BorderStyle.None;
                            Padding = new Padding(0, 0, 0, 0);
                            clicked = false;
                            this.Parent.Cursor = default;
                            BackColor = color;
                        }
                        else
                        {
                            clicked = true;
                            this.Parent.Cursor = curmove;
                            Padding = new Padding(8, 8, 8, 8);
                            BackColor = BColor;
                        }
                    }
                };
            }
        }

        public division() { }
        public void redraw()
        {
            Cursor = default;
            BorderStyle = BorderStyle.None;
            Padding = new Padding(0, 0, 0, 0);
            BackColor = color;
        }

    }

    //в описаниях showcase division мы записываем отображаемые характеристики, и пользуемся затем второй версией функции spawn(sivision)
    public class showcaseDivision: Panel
    {
        Cursor curmove = new Cursor("cursors\\Spider Alt.cur");

        public int hp = 100;
        public int speed = 10;
        public double rot_speed = Math.PI / 90;
        public int attack = 100;
        public Point loc = new Point(0, 0);
        public int width = 120;
        public int height = 90;
        public Color color = Color.Green;
        public Color BColor = Color.LightGreen;
        public Image image = Image.FromFile("div.png");
        public bool clicked = false;
        public bool moving = false;
        public string side = "";
        public int number = -1;
        public double armor = 0.2;
        public string clas;

        public showcaseDivision(string clas, int longg, string side, Form1 form)
        {
            this.clas = clas;
            if (clas == "footman")
            {
                hp = 100; //* longg;
                speed = 10;
                rot_speed = Math.PI / (90); //* longg);
                attack = 100;// + 70* longg;
                width = 120;// + 60* longg;
                height = 90;
                image = Image.FromFile("div.png");
                armor = 0.2;
            }

            if (clas == "cavalry")
            {
                hp = 100; //* longg;
                speed = 16;
                rot_speed = Math.PI / (70); //* longg);
                attack = 120;// + 70* longg;
                width = 100;// + 60* longg;
                height = 100;
                image = Image.FromFile("cav.png");
                armor = 0.4;
            }

            if (clas == "gun")
            {
                hp = 180; //* longg;
                speed = 3;
                rot_speed = Math.PI / (120); //* longg);
                attack = 250;// + 70* longg;
                width = 80;// + 60* longg;
                height = 80;
                image = Image.FromFile("gun.png");
                armor = 1.5;
            }

            if (clas == "wall")
            {
                hp = 100; //* longg;
                speed = 0;
                rot_speed = Math.PI / (100); //* longg);
                attack = 30;// + 70* longg;
                width = 130;// + 60* longg;
                height = 50;
                image = Image.FromFile("wall.png");
                armor = 0.05;
            }


            if (side == "enemy")
            {
                color = Color.Red;
                BColor = Color.OrangeRed;
            }
            if (side == "friend")
            {
                color = Color.Green;
                BColor = Color.LightGreen;
            }

            this.side = side;

            PictureBox pic = new PictureBox();
            pic.Image = image;
            pic.Size = new Size(113,90);
            pic.BackColor = color;
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.Location = new Point(5, 5);
            this.Controls.Add(pic);

            Label cl = new Label();
            cl.Text = clas;
            cl.Location = new Point(5, 105);
            this.Controls.Add(cl);

            Label s = new Label();
            s.Text = side;
            s.Location = new Point(5, 125);
            this.Controls.Add(s);

            Label h = new Label();
            h.Text = "HP: "+hp.ToString();
            h.Location = new Point(5, 145);
            this.Controls.Add(h);

            Label at = new Label();
            at.Text = "Attack: " + attack.ToString();
            at.Location = new Point(5, 165);
            this.Controls.Add(at);

            Label ar = new Label();
            ar.Text = "Armor: " + armor.ToString();
            ar.Location = new Point(5, 185);
            this.Controls.Add(ar);

            Label sp = new Label();
            sp.Text = "Speed: " + speed.ToString();
            sp.Location = new Point(5, 205);
            this.Controls.Add(sp);

            pic.MouseEnter += (sender, e) =>
            {
                if (clicked == false)
                {
                    Cursor = Cursors.Hand;
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.Padding = new Padding(3, 3, 3, 3);
                    pic.BackColor = color;
                }
            };
            pic.MouseLeave += (sender, e) =>
            {
                if (clicked == false)
                {
                    Cursor = default;
                    pic.BorderStyle = BorderStyle.None;
                    pic.Padding = new Padding(0, 0, 0, 0);
                    pic.BackColor = color;
                }
            };

            pic.Click += (sender, e) =>
            {
                if (clicked == true)
                {
                    pic.BorderStyle = BorderStyle.None;
                    pic.Padding = new Padding(0, 0, 0, 0);
                    clicked = false;
                    form.map.Cursor = default;
                    pic.BackColor = color;
                }
                else
                {
                    clicked = true;
                    form.map.Cursor = curmove;
                    pic.Padding = new Padding(8, 8, 8, 8);
                    pic.BackColor = BColor;
                }
            };

            form.map.Click += async (sender, e) =>
            {
                pic.BorderStyle = BorderStyle.None;
                pic.Padding = new Padding(0, 0, 0, 0);
                
                form.map.Cursor = default;
                
                await Task.Delay(300);
                pic.BackColor = color;
                clicked = false;
            };
        }
        public showcaseDivision() { }
    }
}

//arмor не учтен в конструкторе
// неправильно передаются характеристики