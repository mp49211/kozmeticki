﻿using System;
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
    public partial class NovaNarudzbaForm : Form
    {
        ISession session = NHibertnateSession.OpenSession();
        String date;
        TimeSpan time;
        List<ZaposlenikVM> zaposleniciUsluge;
        String klijent = "";
        String kontakt = "";
        int ids = 0;
        int idz = 0;
        int trajanje = 0;
        Boolean zauzet;

        public NovaNarudzbaForm()
        {
            InitializeComponent();
        }

        private void NovaNarudzbaForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
          
            int ID = PocetnaForm.ID;
            novaZaposlenik.Enabled = false;
            date = novaDate.Value.ToShortDateString();
            time = novaVrijeme.Value.TimeOfDay;

            var sveuslugesalona = session.Query<Salonusluga>().Where(a => a.Salon.IdSalon == ID).Select(x => new UslugaVM()
            {
                Idusluga = x.Usluga.Idusluga,
                Naziv = x.Usluga.Naziv,
            }).ToList();
            novaUsluge.DataSource = sveuslugesalona;
            novaUsluge.ValueMember = "Idusluga";
            novaUsluge.DisplayMember = "Naziv";
            novaUsluge.SelectedIndex = 0;
        }

        private void novaUsluge_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String value = novaUsluge.SelectedValue.ToString();
                ids = Convert.ToInt32(value);
                zaposleniciUsluge = session.Query<Zaposlenikusluga>().Where(a => a.Usluga.Idusluga == ids).Select(x => new ZaposlenikVM()
                {
                    IdZaposlenik = x.Zaposlenik.IdZaposlenik,
                    Ime = x.Zaposlenik.Ime,
                    IdSmjena = x.Zaposlenik.Smjena.IdSmjena,
                    IdSalon = x.Zaposlenik.Salon.IdSalon,
                }).ToList();
                zaposleniciUsluge = zaposleniciUsluge.Where(a => a.IdSalon == PocetnaForm.ID).ToList();
                novaZaposlenik.DataSource = zaposleniciUsluge;
                novaZaposlenik.ValueMember = "IdZaposlenik";
                novaZaposlenik.DisplayMember = "Ime";

                trajanje = session.Query<Usluga>().Where(x => x.Idusluga == ids).Select(a => a.Trajanje).ToList()[0];
            }
            catch (FormatException ex)
            {
            }
            catch (InvalidOperationException ran)
            {

            }

        }

        private void novaDate_ValueChanged(object sender, EventArgs e)
        {
            date = novaDate.Value.Date.ToShortDateString();            
        }
        private void novaVrijeme_ValueChanged(object sender, EventArgs e)
        {
            time = novaVrijeme.Value.TimeOfDay;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int smjena = time.Hours;
            if (smjena < 14)
            {
                smjena = 1;
            }
            else
            {
                smjena = 2;
            }
            var posmjeni = zaposleniciUsluge.Where(a => a.IdSmjena == smjena).ToList();
            novaZaposlenik.DataSource = posmjeni;
            novaZaposlenik.ValueMember = "IdZaposlenik";
            novaZaposlenik.DisplayMember = "Ime";
            novaZaposlenik.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var narudzbeZaposlenikaZaDan = PocetnaForm.svenarudzbe.Where(x => x.Zaposlenik.IdZaposlenik == idz).ToList();
            narudzbeZaposlenikaZaDan = narudzbeZaposlenikaZaDan.Where(x => x.Vrijeme.Date.ToShortDateString().Equals(date)).ToList();
            Boolean z = Zaposlenik.ProvjeriZauzetost(narudzbeZaposlenikaZaDan, time, trajanje);
            if (z)
            {
                MessageBox.Show("Odabrani zaposlenik je ZAUZET u odabranom terminu!");
            }
            else
            {
                MessageBox.Show("Odabrani zaposlenik je SLOBODAN u odabranom terminu");
            }

        }

        private void novaDodajButton_Click(object sender, EventArgs e)
        {
            if (idz != 0 && ids != 0 && !klijent.Equals(""))
            {
                var narudzbeZaposlenikaZaDan = PocetnaForm.svenarudzbe.Where(x => x.Zaposlenik.IdZaposlenik == idz).ToList();
                narudzbeZaposlenikaZaDan = narudzbeZaposlenikaZaDan.Where(x => x.Vrijeme.Date.ToShortDateString().Equals(date)).ToList();
                Boolean occ = Zaposlenik.ProvjeriZauzetost(narudzbeZaposlenikaZaDan, time, trajanje);
                if (occ)
                {
                    if (MessageBox.Show("Unjeti novu narudzbu iako je zaposlenik zauzet?", "EF CRUD Operation", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        MessageBox.Show("Poništeno");
                    }
                    else
                    {
                        unesiNovu();
                    }
                }
                else
                {
                    unesiNovu();
                }
            }
            else
            {
                MessageBox.Show("NISU UNESENI SVI PARAMETRI NARUDŽBE!");
            }

        }
        private void novaZaposlenik_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                idz = Convert.ToInt32(novaZaposlenik.SelectedValue.ToString());
            }
            catch (FormatException ex)
            {

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            kontakt = textBox2.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            klijent = textBox1.Text;
        }

      
        private void unesiNovu()
        {
                Narudzba nar = new Narudzba();
                nar.Zaposlenik= session.Get<Zaposlenik>(idz);
                nar.Klijent = klijent;
                nar.Kontakt = kontakt;
                nar.Salon = session.Get<Salon>(PocetnaForm.ID);
                nar.Usluga = session.Get<Usluga>(ids);
                String dt = date + " " + time.ToString();
                nar.Vrijeme = DateTime.Parse(dt);
                DateTime end = nar.Vrijeme.AddMinutes(trajanje);
                if (end.TimeOfDay > novaVrijeme.MaxDate.TimeOfDay)
                {
                    MessageBox.Show("TRAJANJE IZVAN RADNOG VREMENA");
                }
                else
                {

                    try
                    {
                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                        session.Save(nar); //  Save the book in session
                        transaction.Commit();   //  Commit the changes to the database
                    }
                    int id = nar.IdNarudzba;
                        MessageBox.Show("Nova narudzba dodana, id:"+id.ToString());
                        textBox1.Text = "";
                        textBox2.Text = "";

                        PocetnaForm.svenarudzbe.Add(nar);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }

            }
        



    }

}

 

