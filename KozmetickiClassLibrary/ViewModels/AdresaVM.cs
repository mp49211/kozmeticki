using System;
using System.Collections.Generic;
using System.Text;
using KozmetickiClassLibrary.Model;
namespace KozmetickiClassLibrary.ViewModels
{
    public class AdresaVM
    {
        public AdresaVM()
        {
           
        }

        public int IdAdresa { get; set; }
        public string Naziv { get; set; }
        public int IdGrad { get; set; }
        public GradVM Grad { get; set; }
       
    }
}
