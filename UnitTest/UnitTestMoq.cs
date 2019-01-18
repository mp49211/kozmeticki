using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KozmetickiClassLibrary.Model;
using KozmetickiClassLibrary.Interfaces;
using System.Collections.Generic;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class UnitTestMoq
    {
        public readonly IRepository MockZaposlenikRepository;
        public TestContext TestContext { get; set; }
       public  Salon salon = new Salon() { IdSalon = 1, Naziv = "TheBest", Oib = "65786543567" };
        public static  Grad grad = new Grad() { IdGrad = 1, NazivGrada = "Zagreb", Pbr = 10020 };
        public Adresa adresa = new Adresa() { IdAdresa = 1, Nazivadrese = "Petrova", Grad = grad };
        public Smjena smjena = new Smjena() { IdSmjena = 1, SmjenaVal = "jutranja", TimeStart = "8", TimeEnd = "15" };
        public UnitTestMoq()
        {
            // create some mock products to play with

           
            IList<Zaposlenik> zaposlenik = new List<Zaposlenik>
                {
                    new Zaposlenik { IdZaposlenik = 1, Ime = "Tina", Smjena=smjena,
                        Prezime = "Knežević", Oib = "11111111111", Salon=salon, Adresa=adresa},
                   new Zaposlenik { IdZaposlenik = 2, Ime = "Magdalena",
                        Prezime = "Lisica", Oib = "222222222222", Salon=salon,
                        Adresa = new Adresa(){IdAdresa=2, Nazivadrese="Petrova 9",Grad=grad},Smjena = smjena },
                    new Zaposlenik { IdZaposlenik = 3, Ime = "Marta",
                        Prezime = "Posavec", Oib = "33333333333", Salon=salon,
                        Adresa = new Adresa(){IdAdresa=1, Nazivadrese="Petrova 96",Grad=grad},Smjena = smjena },
                };

            // Mock the Zaposlenik Repository using Moq
            Mock<IRepository> mockZaposlenikRepository = new Mock<IRepository>();

            // Return all the zaposlenici
            mockZaposlenikRepository.Setup(mr => mr.GetZaposleniks()).Returns(zaposlenik);

            // return a Zaposlenik by Id
            mockZaposlenikRepository.Setup(mr => mr.GetZaposlenikByID(
                It.IsAny<int>())).Returns((int i) => zaposlenik.Where(
                x => x.IdZaposlenik == i).Single());

            mockZaposlenikRepository.Setup(mr => mr.DeleteZaposlenik(It.IsAny<Zaposlenik>()));
      
        

        // Allows us to test saving 
        mockZaposlenikRepository.Setup(mr => mr.Save(It.IsAny<Zaposlenik>())).Returns(
                (Zaposlenik target) =>
                {
                    DateTime now = DateTime.Now;

                    if (target.IdZaposlenik.Equals(default(int)))
                    {
                       
                        target.IdZaposlenik = zaposlenik.Count() + 1;
                        zaposlenik.Add(target);
                    }
                    else
                    {
                        var original = zaposlenik.Where(
                            q => q.IdZaposlenik == target.IdZaposlenik).Single();

                        if (original == null)
                        {
                            return false;
                        }

                        original.Ime = target.Ime;
                        original.Prezime = target.Prezime;
                        original.Oib = target.Oib;
                        original.Salon = target.Salon;
                        original.Smjena = target.Smjena;
                     
                    }

                    return true;
                });

            // Complete the setup of our Mock Product Repository
            this.MockZaposlenikRepository= mockZaposlenikRepository.Object;
        }
        [TestMethod]
        public void CanReturnZaposlenikById()
        {
            // Try finding a product by id
            Zaposlenik testZaposlenik = this.MockZaposlenikRepository.GetZaposlenikByID(2);

            Assert.IsNotNull(testZaposlenik); // Test if null
            Assert.IsInstanceOfType(testZaposlenik, typeof(Zaposlenik)); // Test type
            Assert.AreEqual("Magdalena", testZaposlenik.Ime); // Verify it is the right product
        }
        [TestMethod]
        public void CanReturnAllZaposleniks()
        {
            // Try finding all products
            IList<Zaposlenik> testZaposlenik = this.MockZaposlenikRepository.GetZaposleniks().ToList();

            Assert.IsNotNull(testZaposlenik); // Test if null
            Assert.AreEqual(3, testZaposlenik.Count); // Verify the correct Number
        }

        [TestMethod]
        public void CanInsertZaposlenik()
        {
            // Create a new product, not I do not supply an id
            Zaposlenik newZaposlenik = new Zaposlenik
            {

                Ime = "Petar",
                Smjena = smjena,
                Prezime = "Knez",
                Oib = "99887766554",
                Salon = salon,
                Adresa = new Adresa() { IdAdresa = 4, Nazivadrese = "Miramarska", Grad = grad }
            };

            int productCount = this.MockZaposlenikRepository.GetZaposleniks().ToList().Count;
            Assert.AreEqual(3, productCount); // Verify the expected Number pre-insert

            // try saving our new product
           var a= this.MockZaposlenikRepository.Save(newZaposlenik);
            Console.Write(a);
            // demand a recount
            productCount = this.MockZaposlenikRepository.GetZaposleniks().ToList().Count;
            Assert.AreEqual(4, productCount); // Verify the expected Number post-insert

            // verify that our new product has been saved
            Zaposlenik testProduct = this.MockZaposlenikRepository.GetZaposlenikByID(4);
            Assert.IsNotNull(testProduct); // Test if null
            Assert.IsInstanceOfType(testProduct, typeof(Zaposlenik)); // Test type
            Assert.AreEqual("Petar", testProduct.Ime); // Verify it has the expected name
        }

        [TestMethod]
        public void CanUpdateProduct()
        {
            // Find a product by id
            Zaposlenik testProduct = this.MockZaposlenikRepository.GetZaposlenikByID(3);

            // Change one of its properties
            testProduct.Ime = "Matea";

            // Save our changes.
            this.MockZaposlenikRepository.Save(testProduct);

            // Verify the change
            Assert.AreEqual("Matea", this.MockZaposlenikRepository.GetZaposlenikByID(3).Ime);
        }
       
    }
}
