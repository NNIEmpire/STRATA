namespace strata
{
    partial class start
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(start));
            pb_Name = new PictureBox();
            playground = new Button();
            kannyi = new Button();
            exit = new Button();
            loadsave = new Button();
            fn = new TextBox();
            label1 = new Label();
            loadsave1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pb_Name).BeginInit();
            SuspendLayout();
            // 
            // pb_Name
            // 
            pb_Name.InitialImage = (Image)resources.GetObject("pb_Name.InitialImage");
            pb_Name.Location = new Point(406, 94);
            pb_Name.Name = "pb_Name";
            pb_Name.Size = new Size(673, 257);
            pb_Name.TabIndex = 4;
            pb_Name.TabStop = false;
            // 
            // playground
            // 
            playground.Font = new Font("Sitka Text", 50F);
            playground.Location = new Point(406, 430);
            playground.Name = "playground";
            playground.Size = new Size(673, 128);
            playground.TabIndex = 5;
            playground.Text = "button1";
            playground.UseVisualStyleBackColor = true;
            playground.Click += playground_Click;
            // 
            // kannyi
            // 
            kannyi.Font = new Font("Sitka Text", 50F);
            kannyi.Location = new Point(406, 576);
            kannyi.Name = "kannyi";
            kannyi.Size = new Size(673, 126);
            kannyi.TabIndex = 6;
            kannyi.Text = "button1";
            kannyi.UseVisualStyleBackColor = true;
            kannyi.Click += kannyi_Click;
            // 
            // exit
            // 
            exit.Font = new Font("Sitka Text", 16F);
            exit.Location = new Point(975, 852);
            exit.Name = "exit";
            exit.Size = new Size(104, 97);
            exit.TabIndex = 7;
            exit.Text = "ВЫХОД";
            exit.UseVisualStyleBackColor = true;
            exit.Click += exit_Click;
            // 
            // loadsave
            // 
            loadsave.Font = new Font("Sitka Text", 18F);
            loadsave.Location = new Point(406, 720);
            loadsave.Name = "loadsave";
            loadsave.Size = new Size(320, 97);
            loadsave.TabIndex = 8;
            loadsave.Text = "button1";
            loadsave.UseVisualStyleBackColor = true;
            loadsave.Click += loadsave_Click;
            // 
            // fn
            // 
            fn.Location = new Point(406, 852);
            fn.Name = "fn";
            fn.Size = new Size(262, 23);
            fn.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(406, 878);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 10;
            label1.Text = "label1";
            // 
            // loadsave1
            // 
            loadsave1.Font = new Font("Sitka Text", 18F);
            loadsave1.Location = new Point(761, 720);
            loadsave1.Name = "loadsave1";
            loadsave1.Size = new Size(318, 97);
            loadsave1.TabIndex = 11;
            loadsave1.Text = "button1";
            loadsave1.UseVisualStyleBackColor = true;
            loadsave1.Click += loadsave1_Click;
            // 
            // start
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1484, 961);
            Controls.Add(loadsave1);
            Controls.Add(label1);
            Controls.Add(fn);
            Controls.Add(loadsave);
            Controls.Add(exit);
            Controls.Add(kannyi);
            Controls.Add(playground);
            Controls.Add(pb_Name);
            Name = "start";
            Text = "start";
            Load += start_Load;
            ((System.ComponentModel.ISupportInitialize)pb_Name).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pb_Name;
        private Button playground;
        private Button kannyi;
        private Button exit;
        private Button loadsave;
        private TextBox fn;
        private Label label1;
        private Button loadsave1;
    }
}