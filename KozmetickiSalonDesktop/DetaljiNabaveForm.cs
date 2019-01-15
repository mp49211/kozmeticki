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
    public partial class DetaljiNabaveForm : Form
    {
        ISession session = NHibertnateSession.OpenSession();
        public DetaljiNabaveForm(int id, string datum, string cijena, string dob)
        {
            InitializeComponent();
            FillData(id);
            label1.Text = dob;
            label2.Text = datum;
            label3.Text = cijena;
        }
        void FillData(int id)
        {

                var artikli = session.Query<Nabavaartikl>().Where(na => na.Nabava.Idnabava == id).Select(na => new NabavaArtiklVM()
                {

                    IdANabavaArtikl = na.IdNabavaArtikl,
                    IdArtikl = na.Artikl.IdArtikl,
                    IdNabva = na.Nabava.Idnabava,
                    Kolicina = na.Kolicina,
                    Artikl = new ArtiklVM()
                    {
                        Cijena = na.Artikl.Cijena,
                        IdArtikl = na.Artikl.IdArtikl,
                        IdKategorija = na.Artikl.Kategorija.IdKategorija,
                        IdDobavljac = na.Artikl.Dobavljac.Iddobavljac,
                        IdProizvodac = na.Artikl.Proizvodac.IdProizvodac,
                        Naziv = na.Artikl.Naziv,
                        Proizvodac =  new ProizvodacVM()
                        {
                            IdProizvodac = na.Artikl.Proizvodac.IdProizvodac,
                            Naziv =na.Artikl.Proizvodac.Nazivproizvodaca

                        },

                        Dobavljac =  new DobavljacVM()
                        {
                            Iddobavljac = na.Artikl.Dobavljac.Iddobavljac,
                            Naziv = na.Artikl.Dobavljac.NazivDobavljaca
                        },

                        Kategorija = new KategorijaVM()
                        {

                            IdKategorija = na.Artikl.Kategorija.IdKategorija,
                            Naziv = na.Artikl.Kategorija.NazivKategorija

                        }


                 
                    }


                }).ToList();



                int i = 1;
                dataGridView1.Columns.Add("br", "#");
                dataGridView1.Columns.Add("naz", "Naziv");
                dataGridView1.Columns.Add("proizvodac", "Proizvodac");
                dataGridView1.Columns.Add("Dostavljac", "Dobavljac");
                dataGridView1.Columns.Add("kat", "Kategorija");
                dataGridView1.Columns.Add("cijena", "Cijena/kom");
                dataGridView1.Columns.Add("kol", "Kolicina");
                dataGridView1.Columns.Add("Suma", "Ukupna Cijena");


                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.Columns["naz"].ReadOnly = true;
                dataGridView1.Columns["proizvodac"].ReadOnly = true;
                dataGridView1.Columns["Dostavljac"].ReadOnly = true;
                dataGridView1.Columns["cijena"].ReadOnly = true;
                dataGridView1.Columns["kol"].ReadOnly = true;
                dataGridView1.Columns["suma"].ReadOnly = true;
                dataGridView1.Columns["kat"].ReadOnly = true;

                foreach (var zapo in artikli)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1);
                    row.Cells[0].Value = i.ToString();
                    row.Cells[1].Value = zapo.Artikl.Naziv;
                    row.Cells[2].Value = zapo.Artikl.Proizvodac.Naziv;
                    row.Cells[3].Value = zapo.Artikl.Dobavljac.Naziv;
                    row.Cells[4].Value = zapo.Artikl.Kategorija.Naziv;
                    row.Cells[5].Value = zapo.Artikl.Cijena;
                    row.Cells[6].Value = zapo.Kolicina;
                    row.Cells[7].Value = Convert.ToInt32(zapo.Kolicina) * Convert.ToInt32(zapo.Artikl.Cijena);


                    dataGridView1.Rows.Add(row);
                    i++;
                }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PregledNabavaForm pn = new PregledNabavaForm();
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(itmap, 0, 0);
        }
        Bitmap itmap;
        private void button1_Click_1(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            itmap = new Bitmap(this.Size.Width, this.Size.Height, g);
            Graphics mg = Graphics.FromImage(itmap);
            mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
            printPreviewDialog1.ShowDialog();
        }

       
    }        
}
