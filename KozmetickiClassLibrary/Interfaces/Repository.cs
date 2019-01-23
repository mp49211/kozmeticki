using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozmetickiClassLibrary.Model;
using NHibernate;

namespace KozmetickiClassLibrary.Interfaces
{
    public class Repository : IRepository, IDisposable
    {

        ISession session ;
        public Repository()
        {
            this.session = NHibertnateSession.OpenSession();
        }


        public Zaposlenik GetZaposlenikByID(int? zaposlenikId)
        {
            return session.Get<Zaposlenik>(zaposlenikId);
        }

        public Usluga GetUslugaByID(int? id)
        {
            return session.Get<Usluga>(id);
        }
        public Salon GetSalonByID(int? id)
        {
            return session.Get<Salon>(id);
        }
        public Artikl GetArtiklByID(int? id)
        {
            return session.Get<Artikl>(id);
        }
        public Narudzba GetNarudzbaByID(int? id)
        {
            return session.Get<Narudzba>(id);
        }
        public Grad GetGradByID(int? id)
        {
            return session.Get<Grad>(id);
        }
        public Smjena GetSmjenaByID(int? id)
        {
            return session.Get<Smjena>(id);
        }
        public Zaposlenikusluga GetZaposlenikUslugaByID(int? id)
        {
            return session.Get<Zaposlenikusluga>(id);
        }



        public void DeleteZaposlenik(Zaposlenik zaposlenik)
        {
            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Delete(zaposlenik); 
                transaction.Commit();   //  Commit the changes to the database
            }
        }
        public void DeleteNarudzba(Narudzba narudzba)
        {
            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Delete(narudzba);
                transaction.Commit();   //  Commit the changes to the database
            }
        }
        public void DeleteArtiklSalon(Artiklsalon ass)
        {
            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Delete(ass);
                transaction.Commit();   //  Commit the changes to the database
            }
        }


        public void DeleteZaposlenikusluga(Zaposlenikusluga zaposlenikusluga)
        {
            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Delete(zaposlenikusluga);
                transaction.Commit();   //  Commit the changes to the database
            }
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    session.Dispose();
                }
            }
            this.disposed = true;
        }

        public IEnumerable<Zaposlenik> GetZaposleniks()
        {
            return session.Query<Zaposlenik>().ToList();
        }
        public IEnumerable<Artikl> GetArtikl()
        {
            return session.Query<Artikl>();
        }
        public IEnumerable<Artiklsalon> GetArtiklSalon()
        {
            return session.Query<Artiklsalon>();
        }
        public IEnumerable<Narudzba> GetNarudzba()
        {
            return session.Query<Narudzba>().ToList();
        }
        public IEnumerable<Zaposlenik> GetZaposleniksQuery()
        {
            return session.Query<Zaposlenik>();
        }
        public IEnumerable<Grad> GetGrad()
        {
            return session.Query<Grad>();
        }
        public IEnumerable<Salon> GetSalon()
        {
            return session.Query<Salon>();
        }
        public IEnumerable<Dobavljac> GetDobavljac()
        {
            return session.Query<Dobavljac>();
        }
        public IEnumerable<Salonusluga> GetSalonUsluga()
        {
            return session.Query<Salonusluga>();
        }
        public IEnumerable<Smjena> GetSmjena()
        {
            return session.Query<Smjena>();
        }
        public IEnumerable<Kategorija> GetKategorija()
        {
            return session.Query<Kategorija>();
        }
      
        public IEnumerable<Usluga> GetUsluga()
        {
            return session.Query<Usluga>();
        }
        public IEnumerable<Adresa> GetAdresa()
        {
            return session.Query<Adresa>();
        }
        public IEnumerable<Zaposlenikusluga> GetZaposlenikUsluga()
        {
            return session.Query<Zaposlenikusluga>().ToList();
        }

      

        public void InsertZaposlenik(Zaposlenik zaposlenik)
        {
            var zaposlenici = GetZaposleniks();
            foreach (var zap in zaposlenici)
            {
                if (zap.Oib.Equals(zaposlenik.Oib))
                {
                    return;
                }
            }
            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Save(zaposlenik);
                transaction.Commit();   //  Commit the changes to the database
            }
        }
        public void InsertZaposlenikusluga(Zaposlenikusluga zaposlenikusluga)
        {
            
            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Save(zaposlenikusluga);
                transaction.Commit();   //  Commit the changes to the database
            }
        }
        public void InsertNarudzba(Narudzba nar)
        {

            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Save(nar);
                transaction.Commit();   //  Commit the changes to the database
            }
        }
        public void InsertArtikl(Artikl nar)
        {

            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Save(nar);
                transaction.Commit();   //  Commit the changes to the database
            }
        }
  
        public int InsertAdresa(Adresa adresa)
        {
            var id=0;
            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                id =(int) session.Save(adresa);
                transaction.Commit();   //  Commit the changes to the database
            }
            return id;
        }

        public void UpdateZaposlenik(Zaposlenik zaposlenik)
        {
            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Save(zaposlenik);
                transaction.Commit();   //  Commit the changes to the database
            }
        }

        public bool Save(Zaposlenik zaposlenik)
        {
            var zaposlenici = GetZaposleniks();
            foreach (var zap in zaposlenici)
            {
                if (zap.Oib==zaposlenik.Oib)
                {
                    return false;
                }
            }

            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Save(zaposlenik);
                transaction.Commit();   //  Commit the changes to the database
            }
            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public void  Commit() {
            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
               
                transaction.Commit();   //  Commit the changes to the database
            }
        }

        void IRepository.InsertArtiklSalon(Artiklsalon art)
        {
            using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
            {
                session.Save(art);
                transaction.Commit();   //  Commit the changes to the database
            }
        }

        public IEnumerable<Proizvodac> GetPrizvodac()
        {
            return session.Query<Proizvodac>();
        }
    }
}
