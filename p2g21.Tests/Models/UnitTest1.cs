using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using p2g21.Models;
using p2g21.Models.Domain;
using p2g21.Tests.Controllers;

namespace p2g21.Tests.Models
{
    [TestClass]
    public class UnitTest1
    {
        private LoginModel model;
        private Gebruiker gebruiker;
        private DummyDataContext context;

        [TestInitialize]
        public void Before()
        {
           context = new DummyDataContext();
            model=new LoginModel();
            model.Password = "Eva";
            model.UserName = "Glenn";

            gebruiker = context.StudentenLijst.FirstOrDefault();
        }
        [TestMethod]
        public void TestMethod1()
        {
            //Assert.AreEqual("Eva",gebruiker.Wachtwoord);
            
            //Assert.IsTrue(gebruiker.DoesPasswordMatch(gebruiker.Wachtwoord, model.Password));
        }
    }
}
