namespace Desktop
{
    partial class UslugeForm
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
            this.novaUslugaButton = new System.Windows.Forms.Button();
            this.uPopis = new System.Windows.Forms.DataGridView();
            this.IdUsluge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vrijeme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Opis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kategorija = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Zaposlenici = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.uPopis)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gill Sans MT", 25.8F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.LightSalmon;
            this.label1.Location = new System.Drawing.Point(24, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 60);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usluge:";
            // 
            // novaUslugaButton
            // 
            this.novaUslugaButton.BackColor = System.Drawing.Color.LightSalmon;
            this.novaUslugaButton.Font = new System.Drawing.Font("Gill Sans MT", 16.2F, System.Drawing.FontStyle.Bold);
            this.novaUslugaButton.Location = new System.Drawing.Point(591, 12);
            this.novaUslugaButton.Name = "novaUslugaButton";
            this.novaUslugaButton.Size = new System.Drawing.Size(250, 88);
            this.novaUslugaButton.TabIndex = 1;
            this.novaUslugaButton.Text = "Dodaj/Ukloni uslugu";
            this.novaUslugaButton.UseVisualStyleBackColor = false;
            this.novaUslugaButton.Click += new System.EventHandler(this.novaUslugaButton_Click);
            // 
            // uPopis
            // 
            this.uPopis.AllowUserToAddRows = false;
            this.uPopis.AllowUserToDeleteRows = false;
            this.uPopis.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.uPopis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uPopis.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdUsluge,
            this.Naziv,
            this.Cijena,
            this.Vrijeme,
            this.Opis,
            this.Kategorija,
            this.Zaposlenici});
            this.uPopis.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uPopis.Location = new System.Drawing.Point(0, 114);
            this.uPopis.Name = "uPopis";
            this.uPopis.ReadOnly = true;
            this.uPopis.RowTemplate.Height = 24;
            this.uPopis.Size = new System.Drawing.Size(857, 501);
            this.uPopis.TabIndex = 2;
            // 
            // IdUsluge
            // 
            this.IdUsluge.DataPropertyName = "IdUsluge";
            this.IdUsluge.HeaderText = "ID USLUGE";
            this.IdUsluge.Name = "IdUsluge";
            this.IdUsluge.ReadOnly = true;
            // 
            // Naziv
            // 
            this.Naziv.DataPropertyName = "Naziv";
            this.Naziv.HeaderText = "NAZIV USLUGE";
            this.Naziv.Name = "Naziv";
            this.Naziv.ReadOnly = true;
            // 
            // Cijena
            // 
            this.Cijena.DataPropertyName = "Cijena";
            this.Cijena.HeaderText = "CIJENA(kn)";
            this.Cijena.Name = "Cijena";
            this.Cijena.ReadOnly = true;
            // 
            // Vrijeme
            // 
            this.Vrijeme.DataPropertyName = "Vrijeme";
            this.Vrijeme.HeaderText = "TRAJANJE(min)";
            this.Vrijeme.Name = "Vrijeme";
            this.Vrijeme.ReadOnly = true;
            // 
            // Opis
            // 
            this.Opis.HeaderText = "OPIS";
            this.Opis.Name = "Opis";
            this.Opis.ReadOnly = true;
            // 
            // Kategorija
            // 
            this.Kategorija.DataPropertyName = "Kategorija";
            this.Kategorija.HeaderText = "KATEGORIJA";
            this.Kategorija.Name = "Kategorija";
            this.Kategorija.ReadOnly = true;
            // 
            // Zaposlenici
            // 
            this.Zaposlenici.DataPropertyName = "Zaposlenici";
            this.Zaposlenici.HeaderText = "PRUŽAJU USLUGU";
            this.Zaposlenici.Name = "Zaposlenici";
            this.Zaposlenici.ReadOnly = true;
            // 
            // UslugeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(857, 615);
            this.Controls.Add(this.uPopis);
            this.Controls.Add(this.novaUslugaButton);
            this.Controls.Add(this.label1);
            this.Name = "UslugeForm";
            this.Text = "Usluge";
            this.Load += new System.EventHandler(this.Usluge_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uPopis)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button novaUslugaButton;
        private System.Windows.Forms.DataGridView uPopis;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUsluge;
        private System.Windows.Forms.DataGridViewTextBoxColumn Naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cijena;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vrijeme;
        private System.Windows.Forms.DataGridViewTextBoxColumn Opis;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kategorija;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zaposlenici;
    }
}