

using NHibernate;
using NHibernate.Cfg;
using System;
using System.IO;
using System.Reflection;
using System.Web;



namespace KozmetickiClassLibrary.Model
{
   public class NHibertnateSession
    {
        public static ISession OpenSession()
        {
            Configuration c = new Configuration();
            c.Configure();

            c.AddAssembly(Assembly.GetCallingAssembly());
            ISessionFactory f = c.BuildSessionFactory();
            return f.OpenSession();
        }
        //public static ISession OpenSession()
        //{
        //    var configuration = new Configuration();
        //    var configurationPath = System.Web.HttpContext.Current.Server.MapPath(@"~\hibernate.cfg.xml");
        //    configuration.Configure(configurationPath);

        //    string zaposlenik = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Zaposlenik.hbm.xml";
        //    string adresa = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Adresa.hbm.xml";
        //    string artiklsalon = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Artiklsalon.hbm.xml";
        //    string dobavljac = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Dobavljac.hbm.xml";
        //    string grad = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Grad.hbm.xml";
        //    string kategorija = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Kategorija.hbm.xml";
        //    string nabava = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Nabava.hbm.xml";
        //    string nabavaartikl = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Nabavaartikl.hbm.xml";
        //    string narudzba = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Narudzba.hbm.xml";
        //    string proizvodac = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Proizvodac.hbm.xml";
        //    string salon = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Salon.hbm.xml";
        //    string salonusluga = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Salonusluga.hbm.xml";
        //    string smjena = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Smjena.hbm.xml";
        //    string usluga = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Usluga.hbm.xml";
        //    string zaposlenikusluga = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/")).Parent.Parent.FullName + "\\KozmetickiSalon\\KozmetickiClassLibrary\\Mapiranje\\Zaposlenikusluga.hbm.xml";
        //    configuration.AddFile(zaposlenik).
        //                   AddFile(adresa).
        //                   AddFile(artiklsalon).
        //                   AddFile(dobavljac).
        //                   AddFile(grad).
        //                   AddFile(kategorija).
        //                   AddFile(nabava).
        //                   AddFile(nabavaartikl).
        //                   AddFile(narudzba).
        //                   AddFile(proizvodac)
        //                   .AddFile(salon).
        //                   AddFile(salonusluga).
        //                   AddFile(smjena).
        //                   AddFile(usluga).
        //                   AddFile(zaposlenikusluga);
        //    ISessionFactory sessionFactory = configuration.BuildSessionFactory();
        //    return sessionFactory.OpenSession();
        //}
    }
}
