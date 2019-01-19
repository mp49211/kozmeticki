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
    public partial class ProfitForm : Form
    {
        ISession session = NHibertnateSession.OpenSession();
        public ProfitForm(int id)
        {
            InitializeComponent();
            Zaposlenik z = session.Get<Zaposlenik>(id);
            decimal profit = Zaposlenik.GetZaposlenikProfitByMonth(z.Narudzba, 1, 2019);
       
            textBox1.Text = profit.ToString();
             profit = Zaposlenik.GetZaposlenikProfitByMonth(z.Narudzba, 2, 2019);
            textBox2.Text = profit.ToString();
            profit = Zaposlenik.GetZaposlenikProfitByMonth(z.Narudzba, 3, 2019);
            textBox3.Text = profit.ToString();
            profit = Zaposlenik.GetZaposlenikProfitByMonth(z.Narudzba, 4, 2019);
            textBox4.Text = profit.ToString();
             profit = Zaposlenik.GetZaposlenikProfitByMonth(z.Narudzba, 5, 2019);
            textBox5.Text = profit.ToString();
             profit = Zaposlenik.GetZaposlenikProfitByMonth(z.Narudzba, 6, 2019);
            textBox6.Text = profit.ToString();
           profit = Zaposlenik.GetZaposlenikProfitByMonth(z.Narudzba, 7, 2019);
            textBox7.Text = profit.ToString();
             profit = Zaposlenik.GetZaposlenikProfitByMonth(z.Narudzba, 8, 2019);
            textBox8.Text = profit.ToString();
             profit = Zaposlenik.GetZaposlenikProfitByMonth(z.Narudzba, 9, 2019);
            textBox9.Text = profit.ToString();
             profit = Zaposlenik.GetZaposlenikProfitByMonth(z.Narudzba, 10, 2019);
            textBox10.Text = profit.ToString();
            profit = Zaposlenik.GetZaposlenikProfitByMonth(z.Narudzba, 11, 2019);
            textBox11.Text = profit.ToString();
             profit = Zaposlenik.GetZaposlenikProfitByMonth(z.Narudzba, 12, 2019);
            textBox12.Text = profit.ToString();
        }

        
    }
}
