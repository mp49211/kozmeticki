namespace Desktop
{
    partial class NarForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nPopis = new System.Windows.Forms.DataGridView();
            this.IdNarudzbe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Klijent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kontakt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Zaposlenik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usluga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vrijeme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VrijemeZavrsetka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nNovaButton = new System.Windows.Forms.Button();
            this.nUsluge = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.nZaposlenikBox = new System.Windows.Forms.ComboBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nPopis)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Gill Sans MT", 25.8F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.LightSalmon;
            this.label2.Location = new System.Drawing.Point(388, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 60);
            this.label2.TabIndex = 1;
            this.label2.Text = "Narudžbe:";
            // 
            // nPopis
            // 
            this.nPopis.AllowUserToAddRows = false;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black;
            this.nPopis.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.nPopis.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nPopis.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.nPopis.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.nPopis.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.nPopis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.nPopis.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 10.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.nPopis.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.nPopis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.nPopis.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdNarudzbe,
            this.Klijent,
            this.Kontakt,
            this.Zaposlenik,
            this.Usluga,
            this.Vrijeme,
            this.VrijemeZavrsetka,
            this.Cijena});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.nPopis.DefaultCellStyle = dataGridViewCellStyle22;
            this.nPopis.EnableHeadersVisualStyles = false;
            this.nPopis.Location = new System.Drawing.Point(0, 130);
            this.nPopis.Name = "nPopis";
            this.nPopis.ReadOnly = true;
            this.nPopis.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.nPopis.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.nPopis.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.Color.Black;
            this.nPopis.RowsDefaultCellStyle = dataGridViewCellStyle24;
            this.nPopis.RowTemplate.Height = 24;
            this.nPopis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.nPopis.Size = new System.Drawing.Size(1330, 408);
            this.nPopis.TabIndex = 2;
            this.nPopis.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.nPopis_UserDeletingRow);
            // 
            // IdNarudzbe
            // 
            this.IdNarudzbe.DataPropertyName = "IdNarudzbe";
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.IdNarudzbe.DefaultCellStyle = dataGridViewCellStyle19;
            this.IdNarudzbe.HeaderText = "ID";
            this.IdNarudzbe.Name = "IdNarudzbe";
            this.IdNarudzbe.ReadOnly = true;
            // 
            // Klijent
            // 
            this.Klijent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Klijent.DataPropertyName = "Klijent";
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Klijent.DefaultCellStyle = dataGridViewCellStyle20;
            this.Klijent.HeaderText = "Klijent";
            this.Klijent.Name = "Klijent";
            this.Klijent.ReadOnly = true;
            // 
            // Kontakt
            // 
            this.Kontakt.DataPropertyName = "Kontakt";
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Kontakt.DefaultCellStyle = dataGridViewCellStyle21;
            this.Kontakt.HeaderText = "Kontakt";
            this.Kontakt.Name = "Kontakt";
            this.Kontakt.ReadOnly = true;
            // 
            // Zaposlenik
            // 
            this.Zaposlenik.DataPropertyName = "Zaposlenik";
            this.Zaposlenik.HeaderText = "Zaposlenik";
            this.Zaposlenik.Name = "Zaposlenik";
            this.Zaposlenik.ReadOnly = true;
            // 
            // Usluga
            // 
            this.Usluga.DataPropertyName = "Usluga";
            this.Usluga.HeaderText = "Usluga";
            this.Usluga.Name = "Usluga";
            this.Usluga.ReadOnly = true;
            // 
            // Vrijeme
            // 
            this.Vrijeme.DataPropertyName = "Vrijeme";
            this.Vrijeme.HeaderText = "Vrijeme";
            this.Vrijeme.Name = "Vrijeme";
            this.Vrijeme.ReadOnly = true;
            // 
            // VrijemeZavrsetka
            // 
            this.VrijemeZavrsetka.DataPropertyName = "VrijemeZavrsetka";
            this.VrijemeZavrsetka.HeaderText = "Vrijeme Zavrsetka";
            this.VrijemeZavrsetka.Name = "VrijemeZavrsetka";
            this.VrijemeZavrsetka.ReadOnly = true;
            // 
            // Cijena
            // 
            this.Cijena.DataPropertyName = "Cijena";
            this.Cijena.HeaderText = "Cijena (KN)";
            this.Cijena.Name = "Cijena";
            this.Cijena.ReadOnly = true;
            // 
            // nNovaButton
            // 
            this.nNovaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nNovaButton.BackColor = System.Drawing.Color.LightSalmon;
            this.nNovaButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.nNovaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nNovaButton.Font = new System.Drawing.Font("Gill Sans MT", 16.2F, System.Drawing.FontStyle.Bold);
            this.nNovaButton.Location = new System.Drawing.Point(1026, 43);
            this.nNovaButton.Name = "nNovaButton";
            this.nNovaButton.Size = new System.Drawing.Size(240, 55);
            this.nNovaButton.TabIndex = 3;
            this.nNovaButton.Text = "Nova narudžba";
            this.nNovaButton.UseVisualStyleBackColor = false;
            this.nNovaButton.Click += new System.EventHandler(this.nNovaButton_Click);
            // 
            // nUsluge
            // 
            this.nUsluge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nUsluge.FormattingEnabled = true;
            this.nUsluge.Location = new System.Drawing.Point(-2, 62);
            this.nUsluge.Name = "nUsluge";
            this.nUsluge.Size = new System.Drawing.Size(206, 28);
            this.nUsluge.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateTimePicker1.Location = new System.Drawing.Point(-2, 18);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(206, 27);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Gill Sans MT", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBox1.Location = new System.Drawing.Point(214, 16);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(90, 29);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Datum";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Gill Sans MT", 10.2F, System.Drawing.FontStyle.Bold);
            this.checkBox2.Location = new System.Drawing.Point(214, 58);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(88, 29);
            this.checkBox2.TabIndex = 9;
            this.checkBox2.Text = "Usluga";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // nZaposlenikBox
            // 
            this.nZaposlenikBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nZaposlenikBox.FormattingEnabled = true;
            this.nZaposlenikBox.Location = new System.Drawing.Point(-2, 93);
            this.nZaposlenikBox.Name = "nZaposlenikBox";
            this.nZaposlenikBox.Size = new System.Drawing.Size(206, 28);
            this.nZaposlenikBox.TabIndex = 10;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("Gill Sans MT", 10.2F, System.Drawing.FontStyle.Bold);
            this.checkBox3.Location = new System.Drawing.Point(214, 92);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(120, 29);
            this.checkBox3.TabIndex = 11;
            this.checkBox3.Text = "Zaposlenik";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSalmon;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.checkBox3);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.nZaposlenikBox);
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.nUsluge);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 130);
            this.panel1.TabIndex = 12;
            // 
            // NarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1330, 538);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.nNovaButton);
            this.Controls.Add(this.nPopis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NarForm";
            this.Text = "NarForm";
            this.Load += new System.EventHandler(this.NarForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nPopis)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView nPopis;
        private System.Windows.Forms.Button nNovaButton;
        private System.Windows.Forms.ComboBox nUsluge;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ComboBox nZaposlenikBox;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdNarudzbe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Klijent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kontakt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zaposlenik;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usluga;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vrijeme;
        private System.Windows.Forms.DataGridViewTextBoxColumn VrijemeZavrsetka;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cijena;
    }
}