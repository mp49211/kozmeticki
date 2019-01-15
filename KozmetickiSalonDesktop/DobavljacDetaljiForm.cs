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
    public partial class DobavljacDetalji : Form
    {
        ISession session = NHibertnateSession.OpenSession();
        public DobavljacDetalji(int id)
        {
            InitializeComponent();
           
                if (id != 0)
                {
                    var dobav = session.Query<Dobavljac>().Where(a => a.Iddobavljac == id).Select(a => new DobavljacVM()
                    {
                        Iddobavljac = a.Iddobavljac,
                        Kontakt = a.Kontakt,
                        Naziv = a.NazivDobavljaca,
                        Oib = a.Oib,
                        Idadresa = a.Adresa.IdAdresa,
                        Adresa = new AdresaVM()
                        {
                            IdAdresa = a.Adresa.IdAdresa,
                            IdGrad = a.Adresa.Grad.IdGrad,
                            Naziv = a.Adresa.Nazivadrese,
                            Grad = new GradVM()
                            {
                                IdGrad = a.Adresa.Grad.IdGrad,
                                Naziv = a.Adresa.Grad.NazivGrada,
                                Pbr = a.Adresa.Grad.Pbr,
                            }


                        }


                    }).SingleOrDefault();

                    label2.Text = dobav.Naziv;
                    label4.Text = dobav.Oib;
                    label6.Text = dobav.Kontakt;
                    label8.Text = dobav.Adresa.Naziv;
                    label10.Text = dobav.Adresa.Grad.Naziv;
                    label12.Text = dobav.Adresa.Grad.Pbr.ToString();

                }
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}
