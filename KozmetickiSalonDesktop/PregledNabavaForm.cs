﻿using KozmetickiClassLibrary;
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

using NHibernate;
using KozmetickiClassLibrary.Model;

namespace Desktop
{
    public partial class PregledNabavaForm : Form
    {
        ISession session = NHibertnateSession.OpenSession();
        public PregledNabavaForm()
        {
            InitializeComponent();
            FillData();
        }
        void FillData()
        {
            dataGridView1.Columns.Add("br", "#");
            dataGridView1.Columns.Add("datum", "Datum");
            dataGridView1.Columns.Add("nazdobavljac", "Dobavljac");
            dataGridView1.Columns.Add("ukcijena", "Ukupna cijena");
            dataGridView1.Columns.Add("idnab", "Id Nabava");

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns["br"].ReadOnly = true;
            dataGridView1.Columns["datum"].ReadOnly = true;
            dataGridView1.Columns["nazdobavljac"].ReadOnly = false;
            dataGridView1.Columns["ukcijena"].ReadOnly = true;
            dataGridView1.Columns["idnab"].Visible = false;



            var nabave = session.Query<Nabava>().Where(a => a.Salon.IdSalon == PocetnaForm.ID).Select(n => new NabavaVM()
                {

                    Idnabava = n.Idnabava,
                    Iddobavljac = n.Dobavljac.Iddobavljac,
                    UkupnaCijena = n.UkupnaCijena,
                    Datum = n.Datum,
                    Dobavljac = new DobavljacVM()
                    {
                        Iddobavljac = n.Dobavljac.Iddobavljac,
                        Naziv = n.Dobavljac.NazivDobavljaca,

                    }



                }).ToList();



                int i = 1;
                
                
                foreach (var zapo in nabave)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1);
                    row.Cells[0].Value = i.ToString();
                    row.Cells[1].Value = zapo.Datum.ToShortDateString();
                    row.Cells[2].Value = zapo.Dobavljac.Naziv;
                    row.Cells[3].Value = zapo.UkupnaCijena;
                    row.Cells[4].Value = zapo.Idnabava;
                    dataGridView1.Rows.Add(row);
                    i++;
                }

                DataGridViewButtonColumn button = new DataGridViewButtonColumn();

                button.Name = "button";
                button.HeaderText = "Pregledaj Narudbu";
                button.Text = "Pregledaj";
                button.UseColumnTextForButtonValue = true; //dont forget this line
                this.dataGridView1.Columns.Add(button);

                dataGridView1.CellContentClick += myDataGridView_CellContentClick;



            


        }
        private void myDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {

                DetaljiNabaveForm ddform = new DetaljiNabaveForm(Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["idnab"].Value), senderGrid.Rows[e.RowIndex].Cells["datum"].Value.ToString(), senderGrid.Rows[e.RowIndex].Cells["ukcijena"].Value.ToString(), senderGrid.Rows[e.RowIndex].Cells["nazdobavljac"].Value.ToString());
                ddform.Show();
            }
        }
    }
}
