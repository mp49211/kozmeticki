using KozmetickiClassLibrary;
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
using System.Web;

namespace Desktop
{
    public partial class PocetnaForm : Form
    {
        public static int ID = 1;
        public static List<Narudzba> svenarudzbe;
        ISession session = NHibertnateSession.OpenSession();
        public PocetnaForm()
        {
            InitializeComponent();
            var salonname = session.Query<Salon>().Where(a => a.IdSalon == PocetnaForm.ID).SingleOrDefault();
            label1.Text = salonname.Naziv;
            label5.Text = salonname.Oib;

            svenarudzbe = session.Query<Narudzba>().Where(a => a.Salon.IdSalon == PocetnaForm.ID).ToList();
               /*Select(x => new NarudzbaVM()
            {
                IdNarudzba = x.IdNarudzba,
                IdUsluga = x.Usluga.Idusluga,
                IdZaposlenik = x.Zaposlenik.IdZaposlenik,
                IdSalon = x.Salon.IdSalon,
                Klijent = x.Klijent,
                Kontakt = x.Kontakt,
                Vrijeme = x.Vrijeme,
            }).ToList();*/

            FillData();
        }
        void FillData()
        {
            dataGridView1.Rows.Clear();
            var n = svenarudzbe.Where(a => a.Vrijeme.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
            n = n.OrderBy(x => x.Vrijeme).ToList();
            int i = 1;
            foreach (var zapo in n)
                {
                    Console.Write(zapo.Vrijeme.ToShortDateString());
                    DataGridViewRow row = new DataGridViewRow();
                    var mojausl = session.Query<Usluga>().Where(u => u.Idusluga == zapo.Usluga.Idusluga).SingleOrDefault();
                    var mojzap = session.Query<Zaposlenik>().Where(u => u.IdZaposlenik == zapo.Zaposlenik.IdZaposlenik).SingleOrDefault();
                    row.CreateCells(dataGridView1);
                    row.Cells[0].Value = i.ToString();
                    row.Cells[1].Value = zapo.Vrijeme.ToShortTimeString();
                    row.Cells[2].Value = zapo.Klijent + " (" + zapo.Kontakt + ")";
                    row.Cells[3].Value = mojzap.Ime + " " + mojzap.Prezime;
                    row.Cells[4].Value = mojausl.Naziv;
                    dataGridView1.Rows.Add(row);
                    i++;
                }


            }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        //zaposlenici
        private void button1_Click(object sender, EventArgs e)
        {
            OsobljeForm frm = new OsobljeForm();
            frm.Show();
        }

        //artikli, nabava artikala
        private void button2_Click(object sender, EventArgs e)
        {
            ArtikliForm frm = new ArtikliForm();
            frm.Show();
        }
        //usluge
        private void button3_Click(object sender, EventArgs e)
        {
            UslugeForm usluge = new UslugeForm();
            usluge.Show();
        }

        //narudzbe
        private void button4_Click(object sender, EventArgs e)
        {
            NarForm nfr = new NarForm();
            nfr.ShowDialog();

            if (nfr.Visible == false)
            {
                FillData();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NabaveForm nabave = new NabaveForm();
            nabave.Show();
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void PocetnaForm_Load(object sender, EventArgs e)
        {
            this.WindowState= FormWindowState.Maximized;
            label2.Text = DateTime.Now.ToLongTimeString();
            label3.Text = DateTime.Now.ToLongDateString();

        }
    }
}
