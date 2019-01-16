using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KozmetickiClassLibrary.ViewModels;
using KozmetickiClassLibrary;
using KozmetickiClassLibrary.Model;
using NHibernate;


namespace Desktop
{
    public partial class NarForm : Form
    {
        ISession session = NHibertnateSession.OpenSession();
        String dateNarudzba;

        public NarForm()
        {
            InitializeComponent();
            int ID = PocetnaForm.ID;
            
            dateNarudzba = System.DateTime.Today.ToString("d");
            
            ispuni(ID);
            var sveuslugesalona = session.Query<Salonusluga>().Where(a => a.Salon.IdSalon == ID).Select(x => new UslugaVM()
            {
                Idusluga = x.Usluga.Idusluga,
                Naziv = x.Usluga.Naziv,
            }).ToList();
            nUsluge.DataSource = sveuslugesalona;
            nUsluge.DisplayMember = "Naziv";
            nUsluge.ValueMember = "Naziv";

            var zaposlenicisalona = session.Query<Zaposlenik>().Where(a => a.Salon.IdSalon == ID).Select(x => new ZaposlenikVM()
            {
                Ime = x.Ime,
                Prezime = x.Prezime,
                IdZaposlenik = x.IdZaposlenik,
                IdSmjena=x.Smjena.IdSmjena,
            }).ToList();

            nZaposlenikBox.DataSource = zaposlenicisalona;
            nZaposlenikBox.DisplayMember = "Ime";
            nZaposlenikBox.ValueMember = "Ime";



        }

        private void nNovaButton_Click(object sender, EventArgs e)
        {
            NovaNarudzbaForm nova = new NovaNarudzbaForm();
            nova.Show();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                DateTime thisDate = dateTimePicker1.Value;
                dateNarudzba = thisDate.ToString("d");
                ispuni(PocetnaForm.ID);
            }
            else
            {
                resetTable();
            }
        }
        //usluga
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                String nazivusluge = nUsluge.SelectedValue.ToString();
                DataGridViewRowCollection grows = nPopis.Rows;
                List<DataGridViewRow> RowsToDelete = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in nPopis.Rows)
                    if (row.Cells[4].Value != null && row.Cells[4].Value.ToString() != nazivusluge) RowsToDelete.Add(row);
                foreach (DataGridViewRow row in RowsToDelete) nPopis.Rows.Remove(row);
                RowsToDelete.Clear();
            }
            else
            {
                checkBox3.Checked = false;
            }
        }
        //zaposlenik 
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                String ime = nZaposlenikBox.SelectedValue.ToString();
                DataGridViewRowCollection grows = nPopis.Rows;
                List<DataGridViewRow> RowsToDelete = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in nPopis.Rows)
                    if (row.Cells[3].Value != null && !row.Cells[3].Value.Equals(ime)) RowsToDelete.Add(row);
                foreach (DataGridViewRow row in RowsToDelete) nPopis.Rows.Remove(row);
                RowsToDelete.Clear();
            }
            else
            {
                if(checkBox2.Checked){
                    checkBox2.Checked = false;
                }
                ispuni(PocetnaForm.ID);
               
            }

        }


        private void ispuni(int ID)
        {
            nPopis.Rows.Clear();
            foreach (var nar in PocetnaForm.svenarudzbe)
            {
                if (nar.Vrijeme.ToString("d").Equals(dateNarudzba))
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(nPopis);
                    row.Cells[0].Value = nar.IdNarudzba;
                    row.Cells[1].Value = nar.Klijent;
                    row.Cells[2].Value = nar.Kontakt;
                    var name = session.Query<Zaposlenik>().Where(x => x.IdZaposlenik == nar.IdZaposlenik).Select(a => a.Ime).ToList();
                    row.Cells[3].Value = name[0].ToString();
                    var nazivUsluge = session.Query<Usluga>().Where(x => x.Idusluga == nar.IdUsluga).Select(a => a.Naziv).ToList();
                    row.Cells[4].Value = nazivUsluge[0];
                    row.Cells[5].Value = nar.Vrijeme;
                    var trajanje = session.Query<Usluga>().Where(x => x.Idusluga == nar.IdUsluga).Select(a => a.Trajanje).ToList();
                    DateTime end = nar.Vrijeme.AddMinutes(trajanje[0]);
                    row.Cells[6].Value = end.TimeOfDay.ToString();
                    row.Cells[7].Value = session.Query<Usluga>().Where(x => x.Idusluga == nar.IdUsluga).Select(a => a.Cijena).ToList()[0];
                    row.Cells[8].Value = "Obrisi";
                    nPopis.Rows.Add(row);


                }
            }
            

        }
       
        private void resetTable()
        {
            dateNarudzba= System.DateTime.Today.ToString("d");
            ispuni(PocetnaForm.ID);
        }
        private void nPopis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (MessageBox.Show("Obrisi odabranu narudzbu?", "EF CRUD Operation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //DeleteSelectedItem(e.RowIndex);
                }
            }
            
        }

       /* private void DeleteSelectedItem(int i)
        {
            Narudzba itemToDelete = new Narudzba();
            int idToDelete=(int)nPopis.Rows[i].Cells[0].Value;

            using (kozmetickisalonEntities connection = new kozmetickisalonEntities())
            {
                itemToDelete=connection.narudzba.Find(idToDelete);
                connection.narudzba.Remove(itemToDelete);
                connection.SaveChanges();
                MessageBox.Show("Narudzba uspjesno obrisana " + itemToDelete.idNarudzba.ToString());
                NarudzbaVM ukloni = PocetnaForm.svenarudzbe.Where(n => n.IdNarudzba == itemToDelete.idNarudzba).ToList()[0];
                PocetnaForm.svenarudzbe.Remove(ukloni);
            }
        }*/

        private void NarForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    
       
    }
}
