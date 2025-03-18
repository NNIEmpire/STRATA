using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace strata
{
    public partial class start : Form
    {
        public start()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            BackgroundImage = Image.FromFile("fight.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            Text = "STRATA";
            Size = new Size(1500, 1000);
        }

        private void start_Load(object sender, EventArgs e)
        {
            pb_Name.Image = Image.FromFile("Strata.png");
            pb_Name.BackColor = Color.FromArgb(145, 255, 255, 255);
            pb_Name.Parent = this;

            playground.Text = "Песочница";
            playground.BackColor = Color.FromArgb(145, 255, 235, 200);
            playground.FlatStyle = FlatStyle.Popup;
            playground.Parent = this;

            kannyi.Text = "Битва при Каннах";
            kannyi.BackColor = Color.FromArgb(145, 200, 200, 255);
            kannyi.FlatStyle = FlatStyle.Popup;
            kannyi.Parent = this;

            loadsave.Text = "Загрузить файл \"Имя\" как Сражение";
            loadsave.BackColor = Color.FromArgb(145, 255, 200, 200);
            loadsave.FlatStyle = FlatStyle.Popup;
            loadsave.Parent = this;

            loadsave1.Text = "Загрузить файл \"Имя\" как Песочница";
            loadsave1.BackColor = Color.FromArgb(145, 200, 255, 200);
            loadsave1.FlatStyle = FlatStyle.Popup;
            loadsave1.Parent = this;

            fn.Text = "Имя файла";
            fn.Parent = this;

            label1.Text = "Save.txt";

            exit.Text = "ВЫХОД";
            exit.BackColor = Color.FromArgb(145, 200, 140, 140);
            exit.FlatStyle = FlatStyle.Popup;
            exit.Parent = this;
        }

        private void playground_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1("1", null);
            newForm.Show();
        }

        private void kannyi_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1("0", "Kannyi.txt");
            newForm.Show();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadsave_Click(object sender, EventArgs e)
        {
            if (File.Exists(fn.Text))
            {
                Form1 newForm = new Form1("0", fn.Text);
                newForm.Show();
            }
            else { label1.Text = "Файла не существует"; }
        }

        private void loadsave1_Click(object sender, EventArgs e)
        {
            if (File.Exists(fn.Text))
            {
                Form1 newForm = new Form1("1", fn.Text);
                newForm.Show();
            }
            else { label1.Text = "Файла не существует"; }
        }
    }
}
