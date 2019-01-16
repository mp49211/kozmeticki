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

namespace Desktop
{
    public partial class UrediZaposlenikForm : Form
    {
        int id;
        public UrediZaposlenikForm(int id)
        {
            InitializeComponent();
            this.id = id;
            using (kozmetickisalonEntities conn = new kozmetickisalonEntities())
            {

                var z = conn.zaposlenik.Where(a => a.idZaposlenik == id).Select(x => new ZaposlenikVM()
                {

                    IdZaposlenik = x.idZaposlenik,
                    IdSalon = x.idSalon,
                    Ime = x.ime,
                    Prezime = x.prezime,
                    Oib = x.oib,
                    ImePrezime = x.ime + " " + x.prezime,
                    Smjena = conn.smjena.Select(a => new SmjenaVM()
                    {
                        IdSmjena = a.idSmjena,
                        Naziv = a.smjena1,

                    }).Where(a => a.IdSmjena == x.idSmjena).FirstOrDefault(),
                    Adresa = conn.adresa.Select(ad => new AdresaVM()
                    {
                        IdAdresa = ad.idAdresa,
                        Naziv = ad.nazivadrese,
                        IdGrad = ad.idGrad,
                        Grad = conn.grad.Select(g => new GradVM()
                        {
                            IdGrad = g.idGrad,
                            Naziv = g.nazivGrada,
                            Pbr = g.pbr

                        }).FirstOrDefault(g => g.IdGrad == ad.idGrad)

                    }).Where(a => a.IdAdresa == x.idAdresa).FirstOrDefault(),


                }).SingleOrDefault();

                var listaZapUsluga = conn.zaposlenikusluga.ToList();

                foreach (var x in listaZapUsluga)
                {

                    if (x.idZaposlenik == z.IdZaposlenik)
                    {
                        z.Usluge.Add(new UslugaVM()
                        {
                            Idusluga = x.idUsluga,
                            Naziv = x.usluga.naziv,
                            Opis = x.usluga.opis,
                            Cijena = x.usluga.cijena,
                            IdKategorija = x.usluga.idKategorija,
                            Trajanje = x.usluga.trajanje,
                            Kategorija = conn.kategorija.Select(k => new KategorijaVM()
                            {
                                IdKategorija = k.idKategorija,
                                Naziv = k.nazivKategorija
                            }).FirstOrDefault(k => k.IdKategorija == x.usluga.idKategorija)

                        });
                    }

                }

                var mojgrad = conn.grad.Where(g => g.idGrad == z.Adresa.Grad.IdGrad).SingleOrDefault();
                var mojasmjena = conn.smjena.Where(s => s.idSmjena == z.Smjena.IdSmjena).SingleOrDefault();
                label1.Text = "UREDI: " + z.ImePrezime;
                textBox2.Text = z.Ime;
                textBox3.Text = z.Prezime;
                textBox4.Text = z.Oib;
                textBox5.Text = z.Adresa.Naziv;

                comboBox2.DataSource = conn.grad.ToList();
                comboBox2.ValueMember = "idGrad";
                comboBox2.DisplayMember = "nazivGrada";
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf(mojgrad);
                comboBox1.DataSource = conn.smjena.ToList();
                comboBox1.ValueMember = "idSmjena";
                comboBox1.DisplayMember = "smjena1";
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf(mojasmjena);

                checkedListBox1.DisplayMember = "naziv";
                checkedListBox1.ValueMember = "idusluga";
                var uslugelista = conn.usluga.ToList();
                var mojeusluge = conn.zaposlenikusluga.Where(u => u.idZaposlenik == z.IdZaposlenik).Select(u => u.usluga).ToList();
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (kozmetickisalonEntities conn = new kozmetickisalonEntities())
            {
                var z = conn.zaposlenik.Where(a => a.idZaposlenik == id).SingleOrDefault();
                if (textBox2.Text != z.ime)
                {
                    z.ime = textBox2.Text;
                }
                if (textBox3.Text != z.prezime)
                {

                    z.prezime = textBox3.Text;
                }
                if (textBox4.Text != z.oib)
                {

                    z.oib = textBox4.Text;
                }
                if (textBox4.Text != z.adresa.nazivadrese)
                {

                    z.adresa.nazivadrese = textBox5.Text;
                }

                if (Convert.ToInt32(comboBox1.SelectedValue.ToString()) != z.idSmjena)
                {

                    z.idSmjena = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                }
                if (Convert.ToInt32(comboBox2.SelectedValue.ToString()) != z.adresa.idGrad)
                {

                    z.adresa.idGrad = Convert.ToInt32(comboBox2.SelectedValue.ToString());
                }


                var mojeusluge = conn.zaposlenikusluga.Where(u => u.idZaposlenik == id).Select(u => u.usluga.idusluga).ToList();

                foreach (var item in checkedListBox1.CheckedItems)
                {
                    var row = (usluga)item;
                    if (!mojeusluge.Contains(row.idusluga))
                    {
                        var zu = new zaposlenikusluga();
                        zu.idUsluga = row.idusluga;
                        zu.idZaposlenik = id;
                        conn.zaposlenikusluga.Add(zu);
                    }

                }
                foreach (object item in checkedListBox1.Items)
                {
                    if (!checkedListBox1.CheckedItems.Contains(item))
                    {
                        var row = (usluga)item;
                        if (mojeusluge.Contains(row.idusluga))
                        {
                            var us = conn.zaposlenikusluga.Where(a => a.idUsluga == row.idusluga).Where(a => a.idZaposlenik == id).SingleOrDefault();
                            conn.zaposlenikusluga.Remove(us);
                        }
                    }
                }


                conn.SaveChanges();
                MessageBox.Show("Spremljene promjene");


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Želite li obrisati ovog zaposlenika??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                using (kozmetickisalonEntities conn = new kozmetickisalonEntities())
                {

                    var z = conn.zaposlenik.Where(a => a.idZaposlenik == id).SingleOrDefault();
                    conn.zaposlenik.Remove(z);

                    conn.SaveChanges();
                    MessageBox.Show("Zaposlenik obrisan");
                    this.Close();


                }
            }
            else
            {
                // If 'No', do something here.
            }

        }
    }
}
