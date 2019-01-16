using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KozmetickiClassLibrary;
using KozmetickiClassLibrary.Model;
using KozmetickiClassLibrary.ViewModels;
using NHibernate;

namespace Desktop
{
    public partial class DodajZaposlenikaForm : Form
    {
        ISession session = NHibertnateSession.OpenSession();
        public DodajZaposlenikaForm()
        {
            InitializeComponent();
            comboBox1.DataSource = session.Query<Smjena>().ToList();
            comboBox1.ValueMember = "idSmjena";
            comboBox1.DisplayMember = "smjenaVal";
            comboBox2.DataSource = session.Query<Grad>().ToList();
            comboBox2.ValueMember = "idGrad";
            comboBox2.DisplayMember = "nazivGrada";


            checkedListBox1.DisplayMember = "naziv";
            checkedListBox1.ValueMember = "Idusluga";
            var uslugelista = session.Query<Usluga>().ToList();
        
            foreach (var u in uslugelista)
            {
               
                    checkedListBox1.Items.Add(u, false);
               

            }
        }

      

        private void DodajZaposlenikaForm_Load(object sender, EventArgs e)
        {
            
        }
        private void ResetForm()
        {
            this.textPrezime.Text = "";
            this.textIme.Text = "";
            this.textOIB.Text = "";
            this.textAdresa.Text = "";

        }
        private bool PregledajPolja() {

            if (string.IsNullOrEmpty(this.textIme.Text)) {
                MessageBox.Show("Upisite ime");
                return false;
            }
            if (string.IsNullOrEmpty(this.textPrezime.Text))
            {
                MessageBox.Show("Upisite prezime");
                return false;
            }
            if (string.IsNullOrEmpty(this.textOIB.Text) || this.textOIB.Text.ToArray().Length>11)
            {
                MessageBox.Show("Upisite OIB");
                return false;
            }
            if (string.IsNullOrEmpty(this.textAdresa.Text))
            {
                MessageBox.Show("Upisite Adresu");
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PregledajPolja())
            {

                Zaposlenik za = new Zaposlenik();
                Adresa ad = new Adresa();
                za.Ime = this.textIme.Text;
                za.Prezime = this.textPrezime.Text;
                za.Oib = this.textOIB.Text;
                ad.Nazivadrese = this.textAdresa.Text;
                var IdGrad = Convert.ToInt32(comboBox2.SelectedValue.ToString());
                Grad grad = session.Get<Grad>(IdGrad);
                ad.Grad = grad;
                var IdSmjena = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                Smjena smjena = session.Get<Smjena>(IdSmjena);
                za.Smjena= smjena;
                var IdSalon = PocetnaForm.ID;
                Salon salon = session.Get<Salon>(IdSalon);
                za.Salon = salon;

                try
                {
                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                        session.Save(ad); //  Save the book in session
                        transaction.Commit();   //  Commit the changes to the database
                    }
                    int id = ad.IdAdresa;
                    za.Adresa = ad;

                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                        session.Save(za); //  Save the book in session
                        transaction.Commit();   //  Commit the changes to the database
                    }
                    foreach (var item in checkedListBox1.CheckedItems)
                    {
                        var row = (Usluga)item;

                        var zu = new Zaposlenikusluga();
                        Usluga usluga = session.Get<Usluga>(row.Idusluga);
                        zu.Usluga = usluga; 
                        zu.Zaposlenik = session.Get<Zaposlenik>(za.IdZaposlenik); 
                        session.Save(zu);


                    }
                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                       
                        transaction.Commit();   //  Commit the changes to the database
                    }
                    MessageBox.Show("Dodan novi zaposlenik " + this.textIme.Text + " " + this.textPrezime.Text);
                    ResetForm();
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
        }

        
    }
}
