using System;
using System.Collections.Generic;
using System.Text;
using KozmetickiClassLibrary.Model;
namespace KozmetickiClassLibrary.ViewModels
{
    public class NabavaVM
    {
        public NabavaVM()
        {
            Artikli = new Dictionary<ArtiklVM, int>();
        }

        public int Idnabava { get; set; }
        public int Idsalon { get; set; }
        public int Iddobavljac { get; set; }
        public DateTime Datum { get; set; }
        public decimal UkupnaCijena { get; set; }

        public DobavljacVM Dobavljac { get; set; }
        public SalonVM Salon { get; set; }
        public Dictionary<ArtiklVM, int> Artikli { get; set; }
    }
}
