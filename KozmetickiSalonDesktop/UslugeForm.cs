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
    public partial class UslugeForm : Form
    {
        ISession session = NHibertnateSession.OpenSession();
        public static List<Salonusluga> sveuslugesalona;
        public UslugeForm()
        {
            InitializeComponent();
            
            
        }

        private void Usluge_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            uPopis.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            ispisi(uPopis);
        }

        private void novaUslugaButton_Click(object sender, EventArgs e)
        {
            DodajUsluguForm dodaj = new DodajUsluguForm();
            dodaj.ShowDialog();
            if (dodaj.Visible == false)
            {
                ispisi(uPopis);
            }
        }

        private  void ispisi(DataGridView uPopis)
        {
            uPopis.Rows.Clear();
           
                sveuslugesalona = session.Query<Salonusluga>().Where(s => s.Salon.IdSalon == PocetnaForm.ID).ToList();
                var zaposlenicisalona = session.Query<Zaposlenik>().Where(z => z.Salon.IdSalon == PocetnaForm.ID).ToList();
                var zapusluga = session.Query<Zaposlenikusluga>().ToList();

                Dictionary<int, List<ZaposlenikVM>> uslugaZaposlenici = new Dictionary<int, List<ZaposlenikVM>>();

                foreach (var usl in sveuslugesalona)
                {
                    uslugaZaposlenici.Add(usl.Usluga.Idusluga, new List<ZaposlenikVM>());
                    foreach (var zap in zaposlenicisalona)
                    {
                        foreach (var zu in zapusluga)
                        {
                            if (zu.Zaposlenik.IdZaposlenik == zap.IdZaposlenik && zu.Usluga.Idusluga == usl.Usluga.Idusluga)
                            {
                                List<ZaposlenikVM> lista;
                                lista = uslugaZaposlenici[zu.Usluga.Idusluga];

                                ZaposlenikVM noviZap = new ZaposlenikVM();
                                noviZap.IdZaposlenik = zu.Zaposlenik.IdZaposlenik;
                                noviZap.IdSalon = PocetnaForm.ID;
                                noviZap.Ime = session.Query<Zaposlenik>().Where(a => a.IdZaposlenik == zu.Zaposlenik.IdZaposlenik).Select(a => a.Ime).ToList()[0].ToString();
                                noviZap.Prezime = session.Query<Zaposlenik>().Where(a => a.IdZaposlenik == zu.Zaposlenik.IdZaposlenik).Select(a => a.Prezime).ToList()[0].ToString();
                                noviZap.Oib = session.Query<Zaposlenik>().Where(a => a.IdZaposlenik == zu.Zaposlenik.IdZaposlenik).Select(a => a.Oib).ToList()[0].ToString();
                                lista.Add(noviZap);
                                uslugaZaposlenici[zu.Usluga.Idusluga] = lista;
                            }
                        }
                    }
                }
                foreach (int idu in uslugaZaposlenici.Keys)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(uPopis);
                    row.Cells[0].Value = idu;
                    row.Cells[1].Value = session.Query<Usluga>().Where(x => x.Idusluga == idu).Select(a => a.Naziv).ToList()[0];
                    row.Cells[2].Value = session.Query<Usluga>().Where(x => x.Idusluga == idu).Select(a => a.Cijena).ToList()[0];
                    row.Cells[3].Value = session.Query<Usluga>().Where(x => x.Idusluga == idu).Select(a => a.Trajanje).ToList()[0];
                    row.Cells[4].Value = session.Query<Usluga>().Where(x => x.Idusluga == idu).Select(a => a.Opis).ToList()[0];
                    int kat = session.Query<Usluga>().Where(x => x.Idusluga == idu).Select(a => a.Kategorija.IdKategorija).ToList()[0];
                    row.Cells[5].Value = session.Query<Kategorija>().Where(k => k.IdKategorija == kat).Select(k => k.NazivKategorija).ToList()[0];
                    string za = "";
                    foreach (var zap in uslugaZaposlenici[idu])
                    {
                        za += zap.Ime + ", ";
                    }
                    row.Cells[6].Value = za;
                    uPopis.Rows.Add(row);

                }
            
        }
    }
}
