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
        IEnumerable<Dobavljac> GetDobavljac();
        IEnumerable<Kategorija> GetKategorija();
        IEnumerable<Proizvodac> GetPrizvodac();
        IEnumerable<Smjena> GetSmjena();
        IEnumerable<Usluga> GetUsluga();
        IEnumerable<Adresa> GetAdresa();
        IEnumerable<Salon> GetSalon();
        IEnumerable<Narudzba> GetNarudzba();
        IEnumerable<Salonusluga> GetSalonUsluga();
        IEnumerable<Zaposlenik> GetZaposleniksQuery();
        IEnumerable<Zaposlenikusluga> GetZaposlenikUsluga();
        IEnumerable<Artiklsalon> GetArtiklSalon();
        IEnumerable<Artikl> GetArtikl();
        Zaposlenik GetZaposlenikByID(int? zaposlenikId);
        Usluga GetUslugaByID(int? uslugaId);
        Artikl GetArtiklByID(int? artiklId);
        Grad GetGradByID(int? gradId);
        Salon GetSalonByID(int? salonId);
        Narudzba GetNarudzbaByID(int? narudzbaId);
        Smjena GetSmjenaByID(int? smjenaId);
        Zaposlenikusluga GetZaposlenikUslugaByID(int? szaposlenikuslugaId);
        void InsertZaposlenik(Zaposlenik zaposlenik);
        void InsertNarudzba(Narudzba zaposlenik);
        void InsertZaposlenikusluga(Zaposlenikusluga zaposlenikusluga);
        int InsertAdresa(Adresa zaposlenik);
        void InsertArtiklSalon(Artiklsalon art);
        void InsertArtikl(Artikl artikl);
        void DeleteZaposlenik(Zaposlenik Zaposlenik);
        void DeleteArtiklSalon(Artiklsalon Zaposlenik);
        void DeleteNarudzba(Narudzba Narudzba);
        void DeleteZaposlenikusluga(Zaposlenikusluga Zaposlenikusluga);
        void UpdateZaposlenik(Zaposlenik zaposlenik);
        bool Save(Zaposlenik zaposlenik);
        void Commit();
    }
}
