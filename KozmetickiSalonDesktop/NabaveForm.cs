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
    public partial class NabaveForm : Form
    {
        ISession session = NHibertnateSession.OpenSession();
        public NabaveForm()
        {
            InitializeComponent();
            FillData();
        }
        void FillData()
        {

            

                var artikli = session.Query<Artikl>().Select(a => new ArtiklVM()
                {
                    IdArtikl = a.IdArtikl,
                    IdDobavljac = a.Dobavljac.Iddobavljac,
                    IdProizvodac = a.Proizvodac.IdProizvodac,
                    IdKategorija = a.Kategorija.IdKategorija,
                    Cijena = a.Cijena,
                    Naziv = a.Naziv,
                    Dobavljac = new DobavljacVM()
                    {

                        Iddobavljac = a.Dobavljac.Iddobavljac,
                        Idadresa = a.Dobavljac.Adresa.IdAdresa,
                        Naziv = a.Dobavljac.NazivDobavljaca,

                    },
                    Proizvodac =  new ProizvodacVM()
                    {

                        IdProizvodac = a.Proizvodac.IdProizvodac,
                        Naziv = a.Proizvodac.Nazivproizvodaca,
                    },
                    Kategorija = new KategorijaVM()
                    {

                        IdKategorija = a.Kategorija.IdKategorija,
                        Naziv = a.Kategorija.NazivKategorija,
                    }


                }).ToList();



                int i = 1;
                dataGridView1.Columns.Add("br", "#");
                dataGridView1.Columns.Add("NazivArt", "Naziv Artikla");
                dataGridView1.Columns.Add("Cijena", "Cijena");
                dataGridView1.Columns.Add("pro", "Proizvodac");
                dataGridView1.Columns.Add("dob", "Dobavljac");
                dataGridView1.Columns.Add("kat", "Kategorija");
                dataGridView1.Columns.Add("kolicina", "Kolicina");
                dataGridView1.Columns.Add("Idart", "Idart");
                dataGridView1.Columns.Add("Iddob", "Idob");

                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.Columns["NazivArt"].ReadOnly = true;
                dataGridView1.Columns["kolicina"].ReadOnly = false;
                dataGridView1.Columns["Cijena"].ReadOnly = true;
                dataGridView1.Columns["kat"].ReadOnly = true;
                dataGridView1.Columns["pro"].ReadOnly = true;
                dataGridView1.Columns["dob"].ReadOnly = true;
                dataGridView1.Columns["idart"].Visible = false;
                dataGridView1.Columns["iddob"].Visible = false;

                foreach (var zapo in artikli)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1);
                    row.Cells[0].Value = i.ToString();
                    row.Cells[1].Value = zapo.Naziv;
                    row.Cells[2].Value = zapo.Cijena;
                    row.Cells[3].Value = zapo.Proizvodac.Naziv;

                    row.Cells[4].Value = zapo.Dobavljac.Naziv;

                    row.Cells[5].Value = zapo.Kategorija.Naziv;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = zapo.IdArtikl;
                    row.Cells[8].Value = zapo.IdDobavljac;
                    dataGridView1.Rows.Add(row);
                    i++;
                }


            }


        

        private void button1_Click(object sender, EventArgs e)
        {
           


                DateTime dateTime = DateTime.UtcNow.Date;
                var dob = session.Query<Dobavljac>().Select(a => a.NazivDobavljaca).Distinct().ToList();

                foreach (var d in dob)
                {
                    var map = new Dictionary<string, string>();
                    var novaNabava = new Nabava();
           
                    novaNabava.Salon = session.Get<Salon>(PocetnaForm.ID);
                    var sum = 0;
                    int id = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (row.Cells["dob"].Value == null)
                        {

                            break;

                        }
                        if (row.Cells["dob"].Value.ToString() == d)
                        {
                            if (row.Cells["kolicina"].Value.ToString() != "0")
                            {
                                id = Convert.ToInt32(row.Cells["iddob"].Value);
                                 var idDob = session.Get<Dobavljac>(id);
                                novaNabava.Dobavljac = idDob;
                                map.Add(row.Cells["idart"].Value.ToString(), row.Cells["kolicina"].Value.ToString());
                                sum += Convert.ToInt32(row.Cells["kolicina"].Value) * Convert.ToInt32(row.Cells["Cijena"].Value);
                            }
                        }


                    }
                    if (id != 0)
                    {
                        novaNabava.Datum = dateTime;
                        novaNabava.UkupnaCijena = sum;
                         using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                             {
                              session.Save(novaNabava); //  Save the book in session
                            transaction.Commit();   //  Commit the changes to the database
                          }
                         int idnab = novaNabava.Idnabava;

                        foreach (var pair in map)
                        {
                            int key = Convert.ToInt32(pair.Key);
                            int value = Convert.ToInt32(pair.Value);

                            var artnab = new Nabavaartikl();

                            artnab.Nabava= novaNabava;
                             var idArtikl = session.Get<Artikl>(key);
                            artnab.Artikl = idArtikl;
                            
                            artnab.Kolicina = value;
                            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                            {
                                session.Save(artnab); //  Save the book in session
                                transaction.Commit();   //  Commit the changes to the database
                            }

                    }
                    }



                }
                MessageBox.Show("Nabava uspjesno pohranjena");

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PregledNabavaForm pn = new PregledNabavaForm();
            pn.Show();
        }

        private void NabaveForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
