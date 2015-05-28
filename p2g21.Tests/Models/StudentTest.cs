//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;

//namespace p2g21.Tests.Models
//{
//    [TestClass]
//    public class StudentTest
//    {
//        //[TestMethod]
//        //public void LoginCorrecteGegevens()
//        //{
//        //    Student student = new Student();

//        //    student.Login("Glenn", "Van Mele");
//        //}
        
//        //[TestMethod]
//        //[ExpectedException(typeof(ArgumentException))]
//        //public void LoginFoutWachtwoord()
//        //{
//        //    Student student = new Student();
//        //    student.Login("Glenn", "Van Melig");
//        //}

//        // [TestMethod]
//        //[ExpectedException(typeof(ArgumentException))]
//        //public void LoginFouteNaam()
//        //{
//        //    Student student = new Student();
//        //    student.Login("Glennz", "Van Mele");
//        //}

//        [TestMethod]
//        public void IndienenJuistVoorstel()
//         {
//             Student student = new Student();
//            List<string> vrijeT = new List<string>(new string[]{"computer","media","informatica"});
//            List<string> referentieLijst = new List<string>(new string[]{"referentie1","referentie2","referentie3"});

//            int LengteLijstVoor = student.Voorstellen.Count;
//            Voorstel voorstel = new Voorstel("Test", "Test", "test@test.test", "TestOrg", "Dit is een test", "Testen", vrijeT, "test", "test", "Testen", "test", "test", referentieLijst);
//            student.IndienenVoorstel(voorstel);
//            Assert.AreEqual(LengteLijstVoor + 1, student.Voorstellen.Count);
//        }

//        [TestMethod]
//        public void VoorstelVerwijderen()
//        {
//            Student student = new Student();
//            List<string> vrijeT = new List<string>(new string[] { "computer", "media", "informatica" });
//            List<string> referentieLijst = new List<string>(new string[] { "referentie1", "referentie2", "referentie3" });

//            Voorstel voorstel = new Voorstel("Test", "Test", "test@test.test", "TestOrg", "Dit is een test", "Testen", vrijeT, "test", "test", "Testen", "test", "test", referentieLijst);
//            student.IndienenVoorstel(voorstel);
//            int LengteLijstVoor = student.Voorstellen.Count;
//            student.DeleteVoorstel(voorstel);
//            Assert.AreEqual(LengteLijstVoor - 1, student.Voorstellen.Count);
//        }
//    }
//}