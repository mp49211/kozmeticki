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

namespace Desktop
{
    public partial class DetaljiNabaveForm : Form
    {
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

            using (kozmetickisalonEntities conn = new kozmetickisalonEntities())
            {

                var artikli = conn.nabavaartikl.Where(na => na.idNabava == id).Select(na => new NabavaArtiklVM()
                {

                    IdANabavaArtikl = na.idNabavaArtikl,
                    IdArtikl = na.idArtikl,
                    IdNabva = na.idNabava,
                    Kolicina = na.kolicina,
                    Artikl = conn.artikl.Select(a => new ArtiklVM()
                    {
                        Cijena = a.cijena,
                        IdArtikl = a.idArtikl,
                        IdKategorija = a.idKategorija,
                        IdDobavljac = a.idDobavljac,
                        IdProizvodac = a.idDobavljac,
                        Naziv = a.naziv,
                        Proizvodac = conn.proizvodac.Select(p => new ProizvodacVM()
                        {
                            IdProizvodac = p.idProizvodac,
                            Naziv = p.nazivproizvodaca

                        }).FirstOrDefault(p => p.IdProizvodac == a.idProizvodac),

                        Dobavljac = conn.dobavljac.Select(d => new DobavljacVM()
                        {
                            Iddobavljac = d.iddobavljac,
                            Naziv = d.nazivDobavljaca
                        }).FirstOrDefault(d => d.Iddobavljac == a.idDobavljac),

                        Kategorija = conn.kategorija.Select(k => new KategorijaVM()
                        {

                            IdKategorija = k.idKategorija,
                            Naziv = k.nazivKategorija

                        }).FirstOrDefault(k => k.IdKategorija == a.idKategorija)



                    }).FirstOrDefault(a => a.IdArtikl == na.idArtikl)


                }).ToList();



                int i = 1;
               
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

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
