using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using System.Media;

namespace strata
{
    public partial class Form1 : Form
    {
        public string gamemode;
        public string filename;
        public GameData gamedata;
        public string mappath = "map.jpg";
        //public string mappath = "kannyi.png";

        private string[] musicfiles = {"Mobilize.wav", "Drums.wav", "Intro.wav"};
        public PictureBox map = new PictureBox {
            Location = new Point(-500, -500),
            Width = 2500,
            Height = 2000,
            SizeMode = PictureBoxSizeMode.StretchImage,
        };
        public PictureBox minimap = new PictureBox
        {
            Width = 240,
            Height = 240,
            Location = new Point(1500 - 250 - 36 + 5, 1000 - 250 - 57 + 5),
            Image = Image.FromFile("map.jpg"),
            SizeMode = PictureBoxSizeMode.StretchImage,
            Padding = new Padding(3),
            BackColor = Color.Khaki
        };
        public PictureBox minimapBack = new PictureBox
        {
            Width = 1447,
            Height = 250,
            Location = new Point(17, 1000 - 250 - 57),
            BackColor = Color.Black
        };
        public PictureBox minimapBack2 = new PictureBox
        {
            Width = 1441,
            Height = 244,
            Location = new Point(20, 1000 - 250 - 57+3),
            BackColor = Color.FromArgb(255,90,90,90)
        };
        public Panel choosing = new Panel
        {
            Width = 1000,
            Height = 240,
            Location = new Point(109+100+5, 1000 - 250 - 57 + 5),
            BackColor = Color.Khaki
        };
        public PictureBox minimapBackL = new PictureBox
        {
            Width = 187,
            Height = 240,
            Location = new Point(22, 1000 - 250 - 57 + 5),
            BackColor = Color.Khaki
        };

        public Cursor curmove = new Cursor("cursors\\Spider Alt.cur");
        public Cursor curattack = new Cursor("cursors\\Blade.cur");

        public static int DivsCount = 16;
        public division[] divs = new division[DivsCount];
        public PictureBox[] divsCircles = new PictureBox[DivsCount];
        public Label[] divsNames = new Label[DivsCount];
        public Label[] divsHP = new Label[DivsCount];
        public int counter;
        public division[] enemys = new division[DivsCount];

        public int speed = 30;
        public int speed2 = 5;
        public int max;
        public bool MouseIn = false;
        public bool[] coll = new bool[DivsCount];
        public int animCounter = 0;

        public Panel bord_left = new border("left");
        public Panel bord_right = new border("right");
        public Panel bord_up = new border("up");
        public Panel bord_down = new border("down");

        public Button saver = new Button { Location = new Point(1410,25), BackgroundImage = Image.FromFile("save.png"), 
            BackgroundImageLayout = ImageLayout.Stretch, Size = new Size(50,50), FlatStyle = FlatStyle.Popup, BackColor = Color.DarkKhaki };
        public Button loader = new Button { Location = new Point(1410, 80), BackgroundImage = Image.FromFile("load.png"), 
            BackgroundImageLayout = ImageLayout.Stretch, Size = new Size(50, 50), FlatStyle = FlatStyle.Popup, BackColor = Color.DarkKhaki};

        public showcaseDivision[] scd = new showcaseDivision[8];
        public Task cc;


        public Form1(string mode, string filename)
        {
            InitializeComponent();
            gamemode = mode;
            this.filename = filename;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            Text = "STRATA";
            Size = new Size(1500, 1000);

            UI_Load();

            bord_left.MouseEnter += LB_MH;
            bord_right.MouseEnter += RB_MH;
            bord_up.MouseEnter += UB_MH;
            bord_down.MouseEnter += DB_MH;
            bord_left.MouseLeave += ML;
            bord_right.MouseLeave += ML;
            bord_up.MouseLeave += ML;
            bord_down.MouseLeave += ML;

            map.Click += TagPos;

            if (gamemode == "1")
            {
                map.Click += (sender, e) =>
                {
                    if (scd.LastOrDefault(division => division.clicked == true) != null)
                    {
                        Point pos = PointToClient(Cursor.Position);
                        showcaseDivision sd = scd.LastOrDefault(division => division.clicked == true);
                        pos = new Point(pos.X - map.Location.X - sd.width / 2, pos.Y - map.Location.Y - sd.height / 2);
                        division newd = new division();
                        if (sd.side == "friend")
                        {
                            newd = new division(sd.clas, sd.side, pos, -Math.PI / 2, "", counter,gamemode);
                        }
                        if (sd.side == "enemy")
                        {
                            newd = new division(sd.clas, sd.side, pos, Math.PI / 2, "", counter, gamemode);
                        }
                        spawn(newd);
                    }
                };

                saver.Click += (sender, e) =>
                {
                    gamedata = new GameData(this);
                    gamedata.Save();
                };

                loader.Click += (sender, e) =>
                {
                    for (int i = 0; i < counter; i++)
                    {
                        map.Controls.Remove(divs[i]);
                        map.Controls.Remove(divsCircles[i]);
                    }
                    for (int i = 0; i < DivsCount; i++)
                    {
                        divs[i] = new division();
                    }
                    gamedata.Load("save.txt", this);
                    counter = 0;
                    for (int i = 0; i < DivsCount; i++)
                    {
                        spawn(divs[i]);
                    }
                };
            }

            if (gamemode == "0")
            {
                enemyAttack();
            }
        }

        public void TagPos(object sender, EventArgs e)
        {
            
            division clicked_division = divs.LastOrDefault(division => division.clicked == true);
            if (clicked_division != null)
            {

                clicked_division.BorderStyle = BorderStyle.None;
                clicked_division.Padding = new Padding(0, 0, 0, 0);
                clicked_division.BackColor = clicked_division.color;

                Point pos = PointToClient(Cursor.Position);
                clicked_division.clicked = false;
                if (animCounter < 2)
                {
                    Animation(clicked_division, new Point(pos.X - map.Location.X - clicked_division.width / 2, pos.Y - map.Location.Y - clicked_division.height / 2));
                }
                if (divs.FirstOrDefault(division => division.clicked == true) == null) { map.Cursor = Cursors.Default; }
            }
        }

        public async void enemyAttack()
        {
            await Task.Delay(5000);
            Random r = new Random();
            division endiv = enemys[r.Next(1,enemys.Length)];
            while (endiv== null || endiv.side == "")
            {
                endiv = enemys[r.Next(1, enemys.Length)];
                if (divs.LastOrDefault(division => division.side == "enemy") == null) { break; }
            }
            while (endiv!=null && (divs.LastOrDefault(division => division.side == "friend") != null))
            {
                if (animCounter < 3 && endiv.moving == false)
                {
                    int gx=0;
                    int gy=0;
                    int c = 0;
                    foreach(division d in divs)
                    {
                        if (d.side == "friend")
                        {
                            c++;
                            gx += d.Location.X;
                            gy += d.Location.Y;
                        }
                    }
                    gx = (int)(gx / c);
                    gy = (int)(gy / c);
                    Random rnd = new Random();
                    //Point pos = new Point((int)(Math.Sin(rnd.Next())*100),(int)(Math.Sin(rnd.Next()) * 100));
                    Point pos = new Point(gx+ rnd.Next(-350,350), gy+rnd.Next(-250, 250));
                    //Animation(endiv, new Point(pos.X + endiv.Location.X, pos.Y + endiv.Location.Y));
                    Animation(endiv, new Point(pos.X, pos.Y));
                }
                await Task.Delay(2800);
                endiv = enemys[r.Next(1, enemys.Length)];
                if (divs.LastOrDefault(division => division.side == "enemy") != null)
                {
                    while (endiv == null || endiv.side == "")
                    {
                        endiv = enemys[r.Next(1, enemys.Length)];
                    }
                }
            }
        }

        private void UI_Load()
        {
            Controls.Add(minimap);
            Controls.Add(minimapBack);
            Controls.Add(minimapBack2);
            Controls.Add(minimapBackL);
            
            minimapBack2.BringToFront();
            minimapBackL.BringToFront();
            minimap.BringToFront();

            Controls.Add(bord_left);
            Controls.Add(bord_right);
            Controls.Add(bord_up);
            Controls.Add(bord_down);

            if (gamemode == "1")
            {
                Controls.Add(saver);
                saver.BringToFront();

                Controls.Add(loader);
                saver.BringToFront();

                Controls.Add(choosing);
                choosing.BringToFront();

                for (int i = 0; i < scd.Length; i++)
                {
                    scd[i] = new showcaseDivision();
                }

                scd[0] = new showcaseDivision("footman", 1, "friend", this);
                scd[1] = new showcaseDivision("cavalry", 1, "friend", this);
                scd[2] = new showcaseDivision("gun", 1, "friend", this);
                scd[3] = new showcaseDivision("wall", 1, "friend", this);

                scd[4] = new showcaseDivision("footman", 1, "enemy", this);
                scd[5] = new showcaseDivision("cavalry", 1, "enemy", this);
                scd[6] = new showcaseDivision("gun", 1, "enemy", this);
                scd[7] = new showcaseDivision("wall", 1, "enemy", this);
                for (int i = 0; i < scd.Length; i++)
                {
                    Controls.Add(scd[i]);
                    scd[i].Size = new Size(125, 240);
                    scd[i].Parent = choosing;
                    scd[i].Location = new Point(i * 125, 0);
                    scd[i].BorderStyle = BorderStyle.FixedSingle;
                }
            }

            map.Image = Image.FromFile(mappath);
            Bitmap back = new Bitmap(map.Image, map.Size);
            map.Image = back;

            Controls.Add(map);
            map.SendToBack();

            if (gamemode == "0")
            {
                music();
                minimapBack2.Hide();
                minimapBack.Hide();
                minimapBackL.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gamedata = new GameData(this);
            counter = 0;
            for(int i =0; i<counter; i++) { coll[i] = false; }
            for (int i = 0; i < divs.Length; i++){ divs[i] = new division(); }

            MinimapUpdate();

            max = (int)(Math.Ceiling((double)counter / 2));
            if (filename != null) 
            { 
                gamedata.Load(filename, this);
                counter = 0;
                for (int i = 0; i < DivsCount; i++)
                {
                    spawn(divs[i]);
                }
            }

            if (gamemode == "0")
            {
                foreach (var d in divs)
                {
                    if (d.side == "enemy")
                    {
                        d.Click += TagPos;
                        enemys[d.number] = d;
                    }
                }
            } //tagPos на дивизию + enemys

        }


        public void ML(object sender, EventArgs e)
        {
            MouseIn = false;
        }
        public async void LB_MH(object sender, EventArgs e)
        {
            MouseIn = true;
            while (map.Location.X < 0 && MouseIn == true)
            {
                Point newLocation = new Point(map.Location.X + speed2, map.Location.Y);
                map.Location = newLocation;
                
                await Task.Delay(speed);
            }
        }
        public async void RB_MH(object sender, EventArgs e)
        {
            MouseIn = true;
            while (map.Location.X > -1000 && MouseIn == true)
            {
                var newLocation = new Point(map.Location.X - speed2, map.Location.Y);
                map.Location = newLocation;
                
                await Task.Delay(speed);
            }
        }
        public async void UB_MH(object sender, EventArgs e)
        {
            MouseIn = true;
            while (map.Location.Y < 0 && MouseIn == true)
            {
                var newLocation = new Point(map.Location.X, map.Location.Y + speed2);
                map.Location = newLocation;
                
                await Task.Delay(speed);
            }
        }
        public async void DB_MH(object sender, EventArgs e)
        {
            MouseIn = true;
            while (map.Location.Y > -1000 && MouseIn == true)
            {
                var newLocation = new Point(map.Location.X, map.Location.Y - speed2);
                map.Location = newLocation;
                await Task.Delay(speed);
            }
        }


        public void spawn(int count, Point p, string side)
        {
            for (int i = counter; i < count+counter; i++)
            {
                divs[i] = new division(100, 10, Math.PI / 90, 100, side + " #" + (i+1).ToString(), i, new Point(p.X + 150 * (i-counter), p.Y), side,"",gamemode);
                divs[i].Location = divs[i].loc;
                divs[i].BackColor = divs[i].color;
                divs[i].Width = divs[i].width;
                divs[i].Height = divs[i].height;
                Controls.Add(divs[i]);
                divs[i].Parent = map;

                divsCircles[i] = new PictureBox { Image = CreateCircleImage(), SizeMode = PictureBoxSizeMode.StretchImage, Height = 20, Width = 20, BackColor = Color.Transparent };
                Controls.Add(divsCircles[i]);
                divsCircles[i].Parent = map;
                divsCircles[i].Location = new Point((int)(divs[i].Location.X + divs[i].Width/2 +72.6 * (Math.Cos(0)) -10), (int)(divs[i].Location.Y + divs[i].Height / 2 + 72.6 * (Math.Sin(0)) -10));

                divsNames[i] = new Label {Height = 19, Width = 65, BackColor = Color.FromArgb(200, divs[i].color), Text = divs[i].name };
                Controls.Add(divsNames[i]);
                divsNames[i].Parent = divs[i];
                divsNames[i].Location = new Point(4, 33+divs[i].Height/2 - 12);

                divsHP[i] = new Label { Height = 19, Width = 45, BackColor = Color.FromArgb(200, 220,20,20), Text = "Hp:"+divs[i].hp.ToString() };
                Controls.Add(divsHP[i]);
                divsHP[i].Parent = divs[i];
                divsHP[i].Location = new Point(4, 33-19 + divs[i].Height / 2 - 12);

                divsCircles[i].BringToFront();
            }

            foreach (var div in divs)
            {
                div.BringToFront();
            }

            counter += count;
            bord_left.BringToFront();
            bord_right.BringToFront();
            bord_down.BringToFront();
            bord_up.BringToFront();
        }
        public void spawn(division d)
        {
            int i = d.number;
            if (i < DivsCount && i >-1)
            {
                divs[i] = d;
                divs[i].Location = divs[i].loc;
                divs[i].BackColor = divs[i].color;
                divs[i].Width = divs[i].width;
                divs[i].Height = divs[i].height;
                Controls.Add(divs[i]);
                divs[i].Parent = map;

                divsCircles[i] = new PictureBox { Image = CreateCircleImage(), SizeMode = PictureBoxSizeMode.StretchImage, Height = 20, Width = 20, BackColor = Color.Transparent };
                Controls.Add(divsCircles[i]);
                divsCircles[i].Parent = map;
                divsCircles[i].Location = new Point((int)(divs[i].Location.X + divs[i].Width / 2 + 72.6 * (Math.Cos(divs[i].alpha)) - 10), (int)(divs[i].Location.Y + divs[i].Height / 2 + 72.6 * (Math.Sin(divs[i].alpha)) - 10));

                divsNames[i] = new Label { Height = 19, Width = 65, BackColor = Color.FromArgb(200, divs[i].color), Text = divs[i].name };
                Controls.Add(divsNames[i]);
                divsNames[i].Parent = divs[i];
                divsNames[i].Location = new Point(4, 33 + divs[i].Height / 2 - 12);

                divsHP[i] = new Label { Height = 19, Width = 45, BackColor = Color.FromArgb(200, 220, 20, 20), Text = "Hp:" + divs[i].hp.ToString() };
                Controls.Add(divsHP[i]);
                divsHP[i].Parent = divs[i];
                divsHP[i].Location = new Point(4, 33 - 19 + divs[i].Height / 2 - 12);

                divsCircles[i].BringToFront();
                counter++;
            }
            MinimapUpdate();

            foreach (var div in divs)
            {
                div.BringToFront();
            }

            bord_left.BringToFront();
            bord_right.BringToFront();
            bord_down.BringToFront();
            bord_up.BringToFront();
        }



        private async void Animation(division div, Point new_pos)
        {
            animCounter++;
            int D = -1;
            bool score = false;

            div.moving = true;
            int animationSpeed = 50;
            double dx = new_pos.X - div.Location.X;
            double dy = new_pos.Y - div.Location.Y;
            double S = Math.Sqrt(dx * dx + dy * dy);
            double speed = div.speed;
            double alpha = Math.Atan2(dy, (dx + 0.001));

            double dA = (alpha - div.alpha);
            double dAp = div.rot_speed * (Math.Sign(dA));
            if (Math.Abs(dA) > Math.PI) { dAp = (Math.Abs(dA) / (2 * Math.PI - Math.Abs(dA))) * dAp; }
            PictureBox indi_rot = new PictureBox { Image = Image.FromFile("rot.png"), Location = new Point(div.width - 25, 0), SizeMode = PictureBoxSizeMode.StretchImage, Height = 25, Width = 25, BackColor = Color.Transparent };
            Controls.Add(indi_rot);
            indi_rot.Parent = div;
            indi_rot.BringToFront();
            while (Math.Abs(div.alpha - alpha) > Math.PI / 9)
            {
                div.alpha += dAp;
                divsCircles[div.number].Location = new Point((int)(div.Location.X + div.Width / 2 - 10 + 72.6 * (Math.Cos(div.alpha))), (int)(div.Location.Y + div.Height / 2 - 10 + 72.6 * (Math.Sin(div.alpha))));
                await Task.Delay(animationSpeed);
                MinimapUpdate();
                if(Math.Sign(Math.Sin(div.alpha+dAp)) == -Math.Sign(Math.Sin(div.alpha)) && (div.alpha + dAp > Math.PI || div.alpha + dAp < -Math.PI)) { break; }
            }
            if (Math.Abs(div.alpha - alpha) > Math.PI / 12) {
                for (int i = 1; i < 6; i++)
                {
                    divsCircles[div.number].Location = new Point((int)(div.Location.X + div.Width / 2 - 10 + 72.6 * (Math.Cos(div.alpha + (Math.Sign(dA)) * (Math.PI * i) / 50))), (int)(div.Location.Y + div.Height / 2 - 10 + 72.6 * (Math.Sin(div.alpha + (Math.Sign(dA)) * (Math.PI * i) / 50))));
                    await Task.Delay(animationSpeed);
                }
            }
            div.alpha = alpha;
            divsCircles[div.number].Location = new Point((int)(div.Location.X + div.Width / 2 - 10 + 72.6 * (Math.Cos(div.alpha))), (int)(div.Location.Y + div.Height / 2 - 10 + 72.6 * (Math.Sin(div.alpha))));
            Controls.Remove(indi_rot);
            indi_rot.Dispose();
            div.redraw();


            if (div.speed != 0 && div.side!="")
            {
                int dxp = (int)Math.Ceiling(Math.Sign(dx) * speed * (Math.Abs(dx) / S));
                int dyp = (int)Math.Ceiling(Math.Sign(dy) * speed * (Math.Abs(dy) / S));
                bool good = true;

                while (Math.Abs(div.Location.X - new_pos.X) > 10*speed || Math.Abs(div.Location.Y - new_pos.Y) > 10*speed)
                {
                    div.Location = new Point(div.Location.X + dxp, div.Location.Y + dyp);
                    divsCircles[div.number].Location = new Point(divsCircles[div.number].Location.X + dxp, divsCircles[div.number].Location.Y + dyp);

                    for (int i = 0; i < counter; i++)
                    {
                        for (int j = i + 1; j < counter; j++)
                        {
                            score = CheckC(divs[i], divs[j]);
                            if (score == true)
                            {
                                coll[divs[j].number] = score;
                                coll[divs[i].number] = score;
                                if (i == div.number) { D = j; }
                                if (j == div.number) { D = i; }
                            }
                        };
                    };

                    MinimapUpdate();

                    if (div.Location.X <= 0 || div.Location.Y <= 0 || div.Location.X >= 2500 || div.Location.Y >= 2000)
                    {
                        good = false;
                    }
                    good = !coll[div.number];
                    if (good == false)
                    {
                        break;
                    }
                    await Task.Delay(animationSpeed);
                }

                if (good == true)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        div.Location = new Point(div.Location.X + dxp, div.Location.Y + dyp);
                        divsCircles[div.number].Location = new Point(divsCircles[div.number].Location.X + dxp, divsCircles[div.number].Location.Y + dyp);
                        MinimapUpdate();
                        await Task.Delay(animationSpeed);
                    }

                    MinimapUpdate();
                    div.moving = false;
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        div.Location = new Point(div.Location.X - dxp, div.Location.Y - dyp);
                        divsCircles[div.number].Location = new Point(divsCircles[div.number].Location.X - dxp, divsCircles[div.number].Location.Y - dyp);
                        MinimapUpdate();
                    }
                    div.moving = false;
                    MinimapUpdate();
                }

                coll[div.number] = false;

                for (int i = 0; i < counter; i++)
                {
                    for (int j = i + 1; j < counter; j++)
                    {
                        score = CheckC(divs[i], divs[j]);
                        if (score == true)
                        {
                            coll[divs[j].number] = score;
                            coll[divs[i].number] = score;
                            if (i == div.number) { D = j; }
                            if (j == div.number) { D = i; }
                        }
                    };
                };
                while (coll[div.number] == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        div.Location = new Point(div.Location.X - dxp, div.Location.Y - dyp);
                        divsCircles[div.number].Location = new Point(divsCircles[div.number].Location.X - dxp, divsCircles[div.number].Location.Y - dyp);
                        MinimapUpdate();
                    }
                    div.moving = false;
                    MinimapUpdate();

                    coll[div.number] = false;
                    foreach (division d in divs)
                    {
                        if (d != div)
                        {
                            score = CheckC(div, d);
                            if (score == true) { coll[div.number] = true; }
                        }
                    }
                    await Task.Delay(150);
                }

                score = false;
                if (D != -1)
                {
                    coll[D] = false;
                    if (div.side != divs[D].side) { Fight(div, divs[D]); }
                }
            }
            if (div.speed == 0) { div.moving = false; }
            animCounter--;
        }

        private Image CreateCircleImage()
        {
            Bitmap bitmap = new Bitmap(20, 20);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);
                Pen pen = new Pen(Color.Black, 3);
                g.FillEllipse(Brushes.Yellow, 3, 3, 14, 14);
                g.DrawEllipse(pen, 3,3,14,14);
            }
            return bitmap;
        }

        private void MinimapUpdate()
        {
            Bitmap back = new Bitmap(map.Image, new Size(250, 250));
            Pen pen = new Pen(Color.Black,2);
            using (Graphics g = Graphics.FromImage(back))
            {
                foreach(division div in divs)
                {
                    if (div.side != "")
                    {
                        int x = (int)(div.Location.X / 10);
                        int y = (int)(div.Location.Y / 8);
                        if (div.side == "enemy") { g.FillRectangle(Brushes.Red, x, y, 15, 10); }
                        if (div.side == "friend") { g.FillRectangle(Brushes.Green, x, y, 15, 10); }
                        g.DrawRectangle(pen, x + 1, y + 1, 14, 10);
                    }
                }
            }
            minimap.Image = back;
        }


        private void Fight(division divA, division divD)
        {
            int dx = divD.Location.X - divA.Location.X;
            int dy = divD.Location.Y - divA.Location.Y;
            double alphaO = Math.Atan2(dy,dx);
            double daA = divA.alpha - alphaO;
            double daD = divD.alpha - alphaO;

            int damA = (int)(divA.attack * Math.Abs(Math.Cos(daA)) * divD.armor * divA.hp / 100);
            divD.hp -= damA;
            if (divD.hp < 0) {  divD.hp = 0; }
            int damD = (int)(divD.attack * Math.Abs(Math.Cos(daD)) * divA.armor * divD.hp / 100);
            divA.hp -= damD;

            divsHP[divA.number].Text = divA.hp.ToString();
            divsHP[divD.number].Text = divD.hp.ToString();

            MinimapUpdate();

            if (divA.hp <= 7) { Tiyl(divA); }
            if (divD.hp <= 7) { Tiyl(divD); }
        }

        private void Tiyl(division div)
        {
            Bitmap back = new Bitmap(map.Image, map.Size);
            using (Graphics g = Graphics.FromImage(back))
            {
                int x = div.Location.X;
                int y = div.Location.Y;
                g.FillRectangle(Brushes.DarkGray, x+div.Width/2, y + div.Height / 2 -15, 25, 35);
                g.FillRectangle(Brushes.Black, x + div.Width / 2 + 3, y + div.Height / 2 +4, 14, 2);
                g.FillRectangle(Brushes.Black, x + div.Width / 2 + 3, y + div.Height / 2 , 9, 2);
                g.FillEllipse(Brushes.DarkGray, x + div.Width / 2, y + div.Height / 2-22, 25, 20);
            }
            map.Image = back;

            
            map.Controls.Remove(divsCircles[div.number]);
            map.Controls.Remove(div);
            Random rnd = new Random();
            div.Location = new Point(5000,5000+1000*(rnd.Next(1,100)));
            div.side = "";
            div.moving = false;

            MinimapUpdate();
        }

        private bool CheckC(Control d1, Control d2)
        {
            Rectangle r1 = new Rectangle(d1.Location, d1.Size);
            Rectangle r2 = new Rectangle(d2.Location, d2.Size);
            return r1.IntersectsWith(r2);
        }

        async void music()
        {
            while (true)
            {
                SoundPlayer sp = new SoundPlayer();
                Random rnd = new Random();
                sp.SoundLocation = musicfiles[rnd.Next(0, 2)];
                sp.Load();
                sp.Play();
                await Task.Delay(170000);
            }
        }

    }
}

//экран 1500*1000, поле 2500*2000

//информация об отряде при наведении курсора
//отталкивание при успешной атаке

//counter не передаётся в имя дивизии
// атакующий курсор
// к сожалению, из-за неэффективных средств связи античности, вы можете управлять только двумя отрядами одновременно