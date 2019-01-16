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
using KozmetickiClassLibrary.ViewModels;

namespace Desktop
{
    public partial class NoviArtiklForm : Form
    {
        ISession session = NHibertnateSession.OpenSession();
        public NoviArtiklForm()
        {
            InitializeComponent();
            FillCode();
        }
        void FillCode()
        {

          
                var artikli = session.Query<Artikl>().Select(ar => new ArtiklVM()
                {
                    IdArtikl = ar.IdArtikl,
                    Naziv = ar.Naziv,
                    Cijena = ar.Cijena,
                    IdProizvodac = ar.Proizvodac.IdProizvodac,
                    IdDobavljac = ar.Dobavljac.Iddobavljac,
                    IdKategorija = ar.Kategorija.IdKategorija,
                    Kategorija =  new KategorijaVM()
                    {
                        IdKategorija = ar.Kategorija.IdKategorija,
                        Naziv = ar.Kategorija.NazivKategorija,

                    },

                    Proizvodac = new ProizvodacVM()
                    {
                        IdProizvodac = ar.Proizvodac.IdProizvodac,
                        Naziv = ar.Proizvodac.Nazivproizvodaca,


                    },

                    Dobavljac = new DobavljacVM()
                    {
                        Iddobavljac = ar.Dobavljac.Iddobavljac,
                        Naziv = ar.Dobavljac.NazivDobavljaca,
                        Kontakt = ar.Dobavljac.Kontakt,
                        Oib = ar.Dobavljac.Oib,

                        Idadresa = ar.Dobavljac.Adresa.IdAdresa,
                        Adresa = new AdresaVM()
                        {
                            IdAdresa = ar.Dobavljac.Adresa.IdAdresa,
                            Naziv = ar.Dobavljac.Adresa.Nazivadrese,
                            IdGrad = ar.Dobavljac.Adresa.Grad.IdGrad,
                            Grad = new GradVM()
                            {
                                IdGrad = ar.Dobavljac.Adresa.Grad.IdGrad,
                                Naziv = ar.Dobavljac.Adresa.Grad.NazivGrada,
                                Pbr = ar.Dobavljac.Adresa.Grad.Pbr
                            }
                        }

                    }
                }).ToList();

                var mojsalon = session.Query<Artiklsalon>().Where(a => a.Salon.IdSalon == PocetnaForm.ID).Select(a => a.Artikl.IdArtikl).ToList();




                int i = 1;
                dataGridView1.Columns.Add("br", "#");

                dataGridView1.Columns.Add("naziv", "Naziv Artikla");
                dataGridView1.Columns.Add("cijena", "Cijena");

                dataGridView1.Columns.Add("kat", "Kategorija");
                dataGridView1.Columns.Add("proizvodac", "Proizvodac");
                dataGridView1.Columns.Add("dobavljac", "Dobavljac");
                dataGridView1.Columns.Add("kol", "Kolicina");
                dataGridView1.Columns.Add("idart", "idart");


                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.Columns["naziv"].ReadOnly = true;
                dataGridView1.Columns["cijena"].ReadOnly = true;
                dataGridView1.Columns["kat"].ReadOnly = true;
                dataGridView1.Columns["proizvodac"].ReadOnly = true;
                dataGridView1.Columns["dobavljac"].ReadOnly = true;
                dataGridView1.Columns["idart"].Visible = false;


                foreach (var zapo in artikli)
                {
                    if (!mojsalon.Contains(zapo.IdArtikl))
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1);
                        row.Cells[0].Value = i.ToString()

                            ;
                        row.Cells[1].Value = zapo.Naziv;
                        row.Cells[2].Value = zapo.Cijena;

                        row.Cells[3].Value = zapo.Kategorija.Naziv;
                        row.Cells[4].Value = zapo.Proizvodac.Naziv;
                        row.Cells[5].Value = zapo.Dobavljac.Naziv;
                        row.Cells[6].Value = 0;
                        row.Cells[7].Value = zapo.IdArtikl;
                        dataGridView1.Rows.Add(row);
                        i++;
                    }
                }
                DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
                checkColumn.Name = "X";
                checkColumn.HeaderText = "X";
                checkColumn.Width = 50;
                checkColumn.ReadOnly = false;
                checkColumn.FillWeight = 10; //if the datagridview is resized (on form resize) the checkbox won't take up too much; value is relative to the other columns' fill values
                dataGridView1.Columns.Add(checkColumn);
            }
        


        private void button1_Click(object sender, EventArgs e)
        {
              foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[8].Value != null)
                    {
                        if (row.Cells["naziv"].Value != null)
                        {

                            var noviartikl = new Artiklsalon();
                            var idArtikl = Convert.ToInt32(row.Cells["idart"].Value.ToString());
                             noviartikl.Artikl = session.Get<Artikl>(idArtikl);
                            var idSalon = PocetnaForm.ID;
                            noviartikl.Salon = session.Get<Salon>(idSalon);

                            noviartikl.Kolicina = Convert.ToInt32(row.Cells["kol"].Value.ToString());
                        using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                        {
                            session.Save(noviartikl); //  Save the book in session
                            transaction.Commit();   //  Commit the changes to the database
                        }




                    }



                    }


                



            }
            MessageBox.Show("Dodan Artikl");
        }
    }
}
