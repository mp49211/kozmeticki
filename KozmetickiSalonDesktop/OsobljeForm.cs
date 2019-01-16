
using KozmetickiClassLibrary;
using KozmetickiClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KozmetickiClassLibrary.Model;
using KozmetickiClassLibrary.ViewModels;
using NHibernate;


namespace Desktop
{
    public partial class OsobljeForm : Form
    {
        ISession session = NHibertnateSession.OpenSession();
        public OsobljeForm()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DodajZaposlenikaForm f = new DodajZaposlenikaForm();
            f.Show();
        }

        private void OsobljeForm_Load(object sender, EventArgs e)
        {
           // this.WindowState = FormWindowState.Maximized;
           
                var zaposlenici = session.Query<Zaposlenik>().Where(a => a.Salon.IdSalon == PocetnaForm.ID).Select(x => new ZaposlenikVM()
                {

                    IdZaposlenik = x.IdZaposlenik,
                    IdSalon = x.Salon.IdSalon,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Oib = x.Oib,
                    Smjena = new SmjenaVM()
                    {
                        IdSmjena = x.Smjena.IdSmjena,
                        Naziv = x.Smjena.SmjenaVal,

                    },
                    Adresa =  new AdresaVM()
                    {
                        IdAdresa = x.Adresa.IdAdresa,
                        Naziv = x.Adresa.Nazivadrese,

                    }


                }).ToList();

                var listaZapUsluga = session.Query<Zaposlenikusluga>().ToList();
                foreach (var z in zaposlenici)
                {
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
                                Kategorija = new KategorijaVM()
                                {
                                    IdKategorija = x.Usluga.Kategorija.IdKategorija,
                                    Naziv = x.Usluga.Kategorija.NazivKategorija
                                }

                            });
                        }
                    }
                }


                int i = 1;
                dataGridView1.Columns.Add("br", "#");
                dataGridView1.Columns.Add("imeiprezime", "Ime i prezime");
                dataGridView1.Columns.Add("oib", "OIB");
                dataGridView1.Columns.Add("adresa", "Adresa");
                dataGridView1.Columns.Add("usluge", "Usluge");
                dataGridView1.Columns.Add("smjena", "Smjena");
                dataGridView1.Columns.Add("id", "id");
                ((DataGridViewTextBoxColumn)dataGridView1.Columns["usluge"]).MaxInputLength = 50;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.Columns["id"].Visible = false;

                foreach (var zapo in zaposlenici)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1);
                    row.Cells[0].Value = i.ToString();
                    row.Cells[1].Value = zapo.Ime + " " + zapo.Prezime;
                    row.Cells[2].Value = zapo.Oib;
                    row.Cells[3].Value = zapo.Adresa.Naziv;
                    string uslug = "";
                    foreach (var u in zapo.Usluge)
                    {
                        uslug += u.Naziv + " ";
                    }
                    row.Cells[4].Value = uslug;
                  
                  
                    row.Cells[5].Value = zapo.Smjena.Naziv;
                    row.Cells[6].Value = zapo.IdZaposlenik;
                    dataGridView1.Rows.Add(row);
                    i++;
                }
               
        
                DataGridViewButtonColumn button = new DataGridViewButtonColumn();

                button.Name = "button";
                button.HeaderText = "Uredi";
                button.Text = "Uredi";
                button.UseColumnTextForButtonValue = true; //dont forget this line
                this.dataGridView1.Columns.Add(button);

                dataGridView1.CellContentClick += myDataGridView_CellContentClick;

            
        }

        private void myDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                UrediZaposlenikForm uz = new UrediZaposlenikForm(Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["id"].Value));
                uz.Show();

            }
        }
    }
}
