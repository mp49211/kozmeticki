namespace Desktop
{
    partial class NovaNarudzbaForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.novaDodajButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.novaUsluge = new System.Windows.Forms.ComboBox();
            this.novaZaposlenik = new System.Windows.Forms.ComboBox();
            this.novaDate = new System.Windows.Forms.DateTimePicker();
            this.novaVrijeme = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Gill Sans MT", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.LightSalmon;
            this.label1.Location = new System.Drawing.Point(109, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usluga:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Gill Sans MT", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.LightSalmon;
            this.label2.Location = new System.Drawing.Point(64, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 34);
            this.label2.TabIndex = 1;
            this.label2.Text = "Zaposlenik:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("Gill Sans MT", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.LightSalmon;
            this.label3.Location = new System.Drawing.Point(0, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 34);
            this.label3.TabIndex = 2;
            this.label3.Text = "Datum i vrijeme:";
            // 
            // novaDodajButton
            // 
            this.novaDodajButton.BackColor = System.Drawing.Color.LightSalmon;
            this.novaDodajButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.novaDodajButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.novaDodajButton.Font = new System.Drawing.Font("Gill Sans MT", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.novaDodajButton.Location = new System.Drawing.Point(527, 342);
            this.novaDodajButton.Name = "novaDodajButton";
            this.novaDodajButton.Size = new System.Drawing.Size(212, 74);
            this.novaDodajButton.TabIndex = 3;
            this.novaDodajButton.Text = "Dodaj narudzbu";
            this.novaDodajButton.UseVisualStyleBackColor = false;
            this.novaDodajButton.Click += new System.EventHandler(this.novaDodajButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Gill Sans MT", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.ForeColor = System.Drawing.Color.LightSalmon;
            this.label6.Location = new System.Drawing.Point(109, 321);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 34);
            this.label6.TabIndex = 6;
            this.label6.Text = "Klijent:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Font = new System.Drawing.Font("Gill Sans MT", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.ForeColor = System.Drawing.Color.LightSalmon;
            this.label7.Location = new System.Drawing.Point(95, 382);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 34);
            this.label7.TabIndex = 7;
            this.label7.Text = "Kontakt:";
            // 
            // novaUsluge
            // 
            this.novaUsluge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.novaUsluge.FormattingEnabled = true;
            this.novaUsluge.Location = new System.Drawing.Point(214, 47);
            this.novaUsluge.Name = "novaUsluge";
            this.novaUsluge.Size = new System.Drawing.Size(212, 28);
            this.novaUsluge.TabIndex = 8;
            this.novaUsluge.SelectedIndexChanged += new System.EventHandler(this.novaUsluge_SelectedIndexChanged);
            // 
            // novaZaposlenik
            // 
            this.novaZaposlenik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.novaZaposlenik.FormattingEnabled = true;
            this.novaZaposlenik.Location = new System.Drawing.Point(214, 248);
            this.novaZaposlenik.Name = "novaZaposlenik";
            this.novaZaposlenik.Size = new System.Drawing.Size(212, 28);
            this.novaZaposlenik.TabIndex = 9;
            this.novaZaposlenik.SelectedIndexChanged += new System.EventHandler(this.novaZaposlenik_SelectedIndexChanged);
            // 
            // novaDate
            // 
            this.novaDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.novaDate.Location = new System.Drawing.Point(214, 133);
            this.novaDate.MinDate = new System.DateTime(2018, 12, 17, 16, 51, 39, 587);
            this.novaDate.Name = "novaDate";
            this.novaDate.Size = new System.Drawing.Size(212, 27);
            this.novaDate.TabIndex = 10;
            this.novaDate.Value = new System.DateTime(2019, 1, 2, 21, 13, 14, 695);
            this.novaDate.ValueChanged += new System.EventHandler(this.novaDate_ValueChanged);
            // 
            // novaVrijeme
            // 
            this.novaVrijeme.CustomFormat = "hh:mm";
            this.novaVrijeme.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.novaVrijeme.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.novaVrijeme.Location = new System.Drawing.Point(448, 133);
            this.novaVrijeme.MaxDate = new System.DateTime(2018, 12, 17, 20, 0, 0, 0);
            this.novaVrijeme.MinDate = new System.DateTime(2018, 12, 17, 8, 0, 0, 0);
            this.novaVrijeme.Name = "novaVrijeme";
            this.novaVrijeme.ShowUpDown = true;
            this.novaVrijeme.Size = new System.Drawing.Size(92, 27);
            this.novaVrijeme.TabIndex = 11;
            this.novaVrijeme.Value = new System.DateTime(2018, 12, 17, 20, 0, 0, 0);
            this.novaVrijeme.ValueChanged += new System.EventHandler(this.novaVrijeme_ValueChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSalmon;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Gill Sans MT", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(602, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 62);
            this.button1.TabIndex = 12;
            this.button1.Text = "Pronadi zaposlenike";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(214, 321);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(212, 27);
            this.textBox1.TabIndex = 13;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox2.Location = new System.Drawing.Point(214, 388);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(212, 27);
            this.textBox2.TabIndex = 14;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSalmon;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Gill Sans MT", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.Location = new System.Drawing.Point(602, 229);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 62);
            this.button2.TabIndex = 16;
            this.button2.Text = "Provjeri Dostupnost";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // NovaNarudzbaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(785, 483);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.novaVrijeme);
            this.Controls.Add(this.novaDate);
            this.Controls.Add(this.novaZaposlenik);
            this.Controls.Add(this.novaUsluge);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.novaDodajButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "NovaNarudzbaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NovaNarudzbaForm";
            this.Load += new System.EventHandler(this.NovaNarudzbaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button novaDodajButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox novaUsluge;
        private System.Windows.Forms.ComboBox novaZaposlenik;
        private System.Windows.Forms.DateTimePicker novaDate;
        private System.Windows.Forms.DateTimePicker novaVrijeme;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
    }
}