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
    public partial class ArtikliForm : Form
    {
        ISession session = NHibertnateSession.OpenSession();
        public ArtikliForm()
        {
            InitializeComponent();
            FillData();
        }
        void FillData()
        {

          
                var artiklisalona = session.Query<Artiklsalon>().Where(a => a.Salon.IdSalon == PocetnaForm.ID).ToList();

                int i = 1;
                dataGridView1.Columns.Add("br", "#");

                dataGridView1.Columns.Add("naziv", "Naziv Artikla");
                dataGridView1.Columns.Add("cijena", "Cijena");
                dataGridView1.Columns.Add("kolicina", "Kolicina");

                dataGridView1.Columns.Add("kat", "Kategorija");
                dataGridView1.Columns.Add("proizvodac", "Proizvodac");
                dataGridView1.Columns.Add("dobavljac", "Dobavljac");
                dataGridView1.Columns.Add("id", "id");
                dataGridView1.Columns.Add("iddob", "iddob");

                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.Columns["naziv"].ReadOnly = true;
                dataGridView1.Columns["kolicina"].ReadOnly = false;
                dataGridView1.Columns["cijena"].ReadOnly = true;
                dataGridView1.Columns["kat"].ReadOnly = true;
                dataGridView1.Columns["proizvodac"].ReadOnly = true;
                dataGridView1.Columns["dobavljac"].ReadOnly = true;
                dataGridView1.Columns["id"].Visible = false;
                dataGridView1.Columns["iddob"].Visible = false;


                foreach (var zapo in artiklisalona)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1);
                    row.Cells[0].Value = i.ToString();
                    row.Cells[1].Value = session.Query<Artikl>().Where(a => a.IdArtikl == zapo.Artikl.IdArtikl).Select(a=>a.Naziv).ToList()[0];
                    row.Cells[2].Value = session.Query<Artikl>().Where(a => a.IdArtikl == zapo.Artikl.IdArtikl).Select(a => a.Cijena).ToList()[0];
                    row.Cells[3].Value = zapo.Kolicina;
                    int idk = session.Query<Artikl>().Where(a => a.IdArtikl == zapo.Artikl.IdArtikl).Select(a => a.Kategorija.IdKategorija).ToList()[0];
                    row.Cells[4].Value = session.Query<Kategorija>().Where(a => a.IdKategorija == idk).Select(a => a.NazivKategorija).ToList()[0];
                    int idp= session.Query<Artikl>().Where(a => a.IdArtikl == zapo.Artikl.IdArtikl).Select(a => a.Proizvodac.IdProizvodac).ToList()[0];
                    row.Cells[5].Value = session.Query<Proizvodac>().Where(a => a.IdProizvodac == idp).Select(a => a.Nazivproizvodaca).ToList()[0];
                    int idd= session.Query<Artikl>().Where(a => a.IdArtikl == zapo.Artikl.IdArtikl).Select(a => a.Dobavljac.Iddobavljac).ToList()[0];
                    row.Cells[6].Value = session.Query<Dobavljac>().Where(a => a.Iddobavljac==idd).Select(a => a.NazivDobavljaca).ToList()[0];
                    row.Cells[7].Value = zapo.IdArtiklSalon;
                    row.Cells[8].Value = idd;
                    dataGridView1.Rows.Add(row);
                    i++;
                }

                DataGridViewButtonColumn button = new DataGridViewButtonColumn();

                button.Name = "button";
                button.HeaderText = "Detalji Dobavljaca";
                button.Text = "Detalji";
                button.UseColumnTextForButtonValue = true; //dont forget this line
                this.dataGridView1.Columns.Add(button);

                dataGridView1.CellContentClick += myDataGridView_CellContentClick;
            


        }

        private void myDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {

                DobavljacDetalji ddform = new DobavljacDetalji(Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["iddob"].Value));
                ddform.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

                var artik = session.Query<Artiklsalon>();


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int id = Convert.ToInt32(row.Cells["id"].Value);

                    int kolicina = Convert.ToInt32(row.Cells["kolicina"].Value);
                    var a = session.Get<Artiklsalon>(id);
                    if (a == null) continue;
                    if (a.Kolicina != kolicina)
                    {
                        a.Kolicina = kolicina;
                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                        session.Delete(a); //  Save the book in session
                        transaction.Commit();   //  Commit the changes to the database
                    }
                }
                }
                MessageBox.Show("Promjenjena Kolicina");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NabaveForm nf = new NabaveForm();
            nf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NoviArtiklForm da = new NoviArtiklForm();
            da.Show();
        }

        private void ArtikliForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
