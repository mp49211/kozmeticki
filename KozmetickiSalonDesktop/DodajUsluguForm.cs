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


namespace Desktop
{
    public partial class DodajUsluguForm : Form
    {
        ISession session = NHibertnateSession.OpenSession();
        List<Usluga> mojeusluge;
        public DodajUsluguForm()
        {
            InitializeComponent();

            checkedListBox1.DisplayMember = "naziv";
            checkedListBox1.ValueMember = "idusluga";
            var uslugelista = session.Query<Usluga>().ToList();
            mojeusluge = session.Query<Salonusluga>().Where(u => u.Salon.IdSalon == PocetnaForm.ID).Select(u => u.Usluga).ToList();

            foreach (var u in uslugelista)
            {

                if (mojeusluge.Contains(u))
                {
                    checkedListBox1.Items.Add(u, true);
                }
                else
                {

                    checkedListBox1.Items.Add(u, false);
                }

            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            int brNovih = 0;
            int brUkloni = 0;
            var odabrane = checkedListBox1.CheckedItems;
            foreach (var item in checkedListBox1.CheckedItems)
             {
               Usluga row = (Usluga)item;
               if (!mojeusluge.Contains(row))
               {
                    Salonusluga dodaj = new Salonusluga();
                    Salon salon = session.Get<Salon>(PocetnaForm.ID);
                    dodaj.Salon = salon;
                    Usluga usluga = session.Get<Usluga>(row.Idusluga);
                    dodaj.Usluga= usluga;
                    using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                    {
                        session.Save(dodaj); //  Save the book in session
                        transaction.Commit();   //  Commit the changes to the database
                    }
                    UslugeForm.sveuslugesalona.Add(dodaj);
                    brNovih++;
               }

            }
            foreach (object item in checkedListBox1.Items) //sve usluge
                {
                    if (!checkedListBox1.CheckedItems.Contains(item)) //ne cekirane
                    {
                        Usluga row = (Usluga)item;
                        if (mojeusluge.Contains(row)) //moja usluga ne cekirana
                        {
                            if (MessageBox.Show("Obrisi uslugu " + row.Naziv + " ?", "EF CRUD Operation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                            Salonusluga obrisi = session.Query<Salonusluga>().Where(s => s.Salon.IdSalon == PocetnaForm.ID).Where(u => u.Usluga.Idusluga == row.Idusluga).SingleOrDefault();
                            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                            {
                                session.Delete(obrisi); //  Save the book in session
                                transaction.Commit();   //  Commit the changes to the database
                            }
                            UslugeForm.sveuslugesalona.Remove(obrisi);
                            brUkloni++;
                            }
                        }
                    }
                }
                
            MessageBox.Show("Broj novih usluga: " + brNovih.ToString() + ". Broj uklonjenih usluga " + brUkloni.ToString());
            this.Close();

        }
        
    }
}
