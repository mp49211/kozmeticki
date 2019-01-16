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
using KozmetickiClassLibrary.ViewModels;
using KozmetickiClassLibrary.Model;
using KozmetickiClassLibrary.ViewModels;
using NHibernate;

namespace Desktop
{
    public partial class UrediZaposlenikForm : Form
    {
        int id;
        ISession session = NHibertnateSession.OpenSession();
        public UrediZaposlenikForm(int id)
        {
            InitializeComponent();
            this.id = id;
           

                var z = session.Query<Zaposlenik>().Where(a => a.IdZaposlenik == id).Select(x => new ZaposlenikVM()
                {

                    IdZaposlenik = x.IdZaposlenik,
                    IdSalon = x.Salon.IdSalon,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Oib = x.Oib,
                    ImePrezime = x.Ime + " " + x.Prezime,
                    Smjena = new SmjenaVM()
                    {
                        IdSmjena = x.Smjena.IdSmjena,
                        Naziv = x.Smjena.SmjenaVal,

                    },
                    Adresa =  new AdresaVM()
                    {
                        IdAdresa = x.Adresa.IdAdresa,
                        Naziv = x.Adresa.Nazivadrese,
                        IdGrad = x.Adresa.Grad.IdGrad,
                        Grad = new GradVM()
                        {
                            IdGrad = x.Adresa.Grad.IdGrad,
                            Naziv = x.Adresa.Grad.NazivGrada

                        }

                    }


                }).SingleOrDefault();

                var listaZapUsluga = session.Query<Zaposlenikusluga>().ToList();

                foreach (var x in listaZapUsluga)
                {

                    if (x.Zaposlenik.IdZaposlenik == z.IdZaposlenik)
                    {
                        z.Usluge.Add(new UslugaVM()
                        {
                            Idusluga = x.Usluga.Idusluga,
                            Naziv = x.Usluga.Naziv,
                            Opis = x.Usluga.Opis,
                            Cijena = x.Usluga.Cijena,
                            IdKategorija = x.Usluga.Kategorija.IdKategorija,
                            Trajanje = x.Usluga.Trajanje,
                            Kategorija =  new KategorijaVM()
                            {
                                IdKategorija = x.Usluga.Kategorija.IdKategorija,
                                Naziv = x.Usluga.Kategorija.NazivKategorija
                            }

                        });
                    }

                }

                var mojgrad = session.Query<Grad>().Where(g => g.IdGrad == z.Adresa.Grad.IdGrad).SingleOrDefault();
                var mojasmjena = session.Query<Smjena>().Where(s => s.IdSmjena == z.Smjena.IdSmjena).SingleOrDefault();
                label1.Text = "UREDI: " + z.ImePrezime;
                textBox2.Text = z.Ime;
                textBox3.Text = z.Prezime;
                textBox4.Text = z.Oib;
                textBox5.Text = z.Adresa.Naziv;

                comboBox2.DataSource = session.Query<Grad>().ToList();
                comboBox2.ValueMember = "idGrad";
                comboBox2.DisplayMember = "nazivGrada";
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf(mojgrad);
                comboBox1.DataSource = session.Query<Smjena>().ToList();
                comboBox1.ValueMember = "idSmjena";
                comboBox1.DisplayMember = "smjenaVal";
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf(mojasmjena);

                checkedListBox1.DisplayMember = "naziv";
                checkedListBox1.ValueMember = "idusluga";
                var uslugelista = session.Query<Usluga>().ToList();
                var mojeusluge = session.Query<Zaposlenikusluga>().Where(u => u.Zaposlenik.IdZaposlenik == z.IdZaposlenik).Select(u => u.Usluga).ToList();
                foreach (var u in uslugelista)
                {

                    if (mojeusluge.Contains(u))
                    {
                        checkedListBox1.Items.Add(u, true);
                    }
                    else
                    {

                        checkedListBox1.Items.Add(u, false);
                    }



                }


            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                var z = session.Query<Zaposlenik>().Where(a => a.IdZaposlenik == id).SingleOrDefault();
                if (textBox2.Text != z.Ime)
                {
                    z.Ime = textBox2.Text;
                }
                if (textBox3.Text != z.Prezime)
                {

                    z.Prezime = textBox3.Text;
                }
                if (textBox4.Text != z.Oib)
                {

                    z.Oib = textBox4.Text;
                }
                if (textBox4.Text != z.Adresa.Nazivadrese)
                {

                    z.Adresa.Nazivadrese = textBox5.Text;
                }

                if (Convert.ToInt32(comboBox1.SelectedValue.ToString()) != z.Smjena.IdSmjena)
                {

                    var idSmjena = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                    z.Smjena = session.Get<Smjena>(idSmjena);
                }
                if (Convert.ToInt32(comboBox2.SelectedValue.ToString()) != z.Adresa.Grad.IdGrad)
                {

                    var idGrad = Convert.ToInt32(comboBox2.SelectedValue.ToString());
                    z.Adresa.Grad = session.Get<Grad>(idGrad);
            }


                var mojeusluge = session.Query<Zaposlenikusluga>().Where(u => u.Zaposlenik.IdZaposlenik == id).Select(u => u.Usluga.Idusluga).ToList();

                foreach (var item in checkedListBox1.CheckedItems)
                {
                    var row = (Usluga)item;
                    if (!mojeusluge.Contains(row.Idusluga))
                    {
                        var zu = new Zaposlenikusluga();
                        zu.Usluga = session.Get<Usluga>(row.Idusluga);
                        zu.Zaposlenik = session.Get<Zaposlenik>(id);
                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                        session.Save(zu); //  Save the book in session
                        transaction.Commit();   //  Commit the changes to the database
                    }
                }

                }
                foreach (object item in checkedListBox1.Items)
                {
                    if (!checkedListBox1.CheckedItems.Contains(item))
                    {
                        var row = (Usluga)item;
                        if (mojeusluge.Contains(row.Idusluga))
                        {
                            var us = session.Query<Zaposlenikusluga>().Where(a => a.Usluga.Idusluga == row.Idusluga).Where(a => a.Zaposlenik.IdZaposlenik == id).SingleOrDefault();
                        using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                        {
                            session.Delete(us); //  Save the book in session
                            transaction.Commit();   //  Commit the changes to the database
                        }
                    }
                    }
                }


            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                
                transaction.Commit();   //  Commit the changes to the database
            }
            MessageBox.Show("Spremljene promjene");


            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Želite li obrisati ovog zaposlenika??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                
                    var z = session.Query<Zaposlenik>().Where(a => a.IdZaposlenik == id).SingleOrDefault();
                using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                {
                    session.Delete(z);
                    transaction.Commit();   //  Commit the changes to the database
                }
                MessageBox.Show("Zaposlenik obrisan");
                    this.Close();


                
            }
            else
            {
                // If 'No', do something here.
            }

        }
    }
}
