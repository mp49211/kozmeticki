using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozmetickiClassLibrary.Model;

namespace KozmetickiClassLibrary.Interfaces
{
   public  interface IRepository : IDisposable
    {
        IEnumerable<Zaposlenik> GetZaposleniks();
        IEnumerable<Grad> GetGrad();
        IEnumerable<Smjena> GetSmjena();
        IEnumerable<Usluga> GetUsluga();
        IEnumerable<Adresa> GetAdresa();
        IEnumerable<Salon> GetSalon();
        IEnumerable<Narudzba> GetNarudzba();
        IEnumerable<Zaposlenik> GetZaposleniksQuery();
        IEnumerable<Zaposlenikusluga> GetZaposlenikUsluga();
        Zaposlenik GetZaposlenikByID(int? zaposlenikId);
        Usluga GetUslugaByID(int? uslugaId);
        Grad GetGradByID(int? gradId);
        Smjena GetSmjenaByID(int? smjenaId);
        Zaposlenikusluga GetZaposlenikUslugaByID(int? szaposlenikuslugaId);
        void InsertZaposlenik(Zaposlenik zaposlenik);
        void InsertZaposlenikusluga(Zaposlenikusluga zaposlenikusluga);
        int InsertAdresa(Adresa zaposlenik);
        void DeleteZaposlenik(Zaposlenik Zaposlenik);
        void DeleteZaposlenikusluga(Zaposlenikusluga Zaposlenikusluga);
        void UpdateZaposlenik(Zaposlenik zaposlenik);
        bool Save(Zaposlenik zaposlenik);
        void Commit();
    }
}
