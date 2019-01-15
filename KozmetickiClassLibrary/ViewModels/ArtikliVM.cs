using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozmetickiClassLibrary.Model;
namespace KozmetickiClassLibrary.ViewModels
{
    public class ArtikliVM
    {
        public ArtikliVM()
        {
            Arts = new List<Artikl>();
            Dict = new Dictionary<int, int>();
            Kols = new List<int>();
        }
        public List<Artikl> Arts { get; set; }
        public List<int> Kols { get; set; }
        public Dictionary<int, int> Dict { get; set; }
    }
}
