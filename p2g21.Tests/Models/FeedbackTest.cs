//using System;
//using System.Text;
//using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace p2g21.Tests.Models
//{
    
//    [TestClass]
//    public class FeedbackTest
//    {
//        private Feedback deFeedback;
//        private int geldigeInput;
//        private int foutieveInputKleinerDan;
//        private int foutieveInputGroterDan;
//        private string suggestie;

//        [TestInitialize]
//        public void Before()
//        {
            
//            geldigeInput = 2;
//            foutieveInputGroterDan = 6;
//            foutieveInputKleinerDan = 0;
//            suggestie = "Kies een ander onderwerp, begin volledig opnieuw!!";

//        }

//        [TestMethod]
//        public void FeedbackConstructorMetGeldigeInputOK()
//        {
//            deFeedback = new Feedback(geldigeInput, geldigeInput, geldigeInput, geldigeInput, geldigeInput, geldigeInput, geldigeInput, suggestie);
//            Assert.IsNotNull(deFeedback);
//            Assert.AreEqual(geldigeInput,deFeedback.Bijdrage);
//            Assert.AreEqual(geldigeInput, deFeedback.Bron);
//            Assert.AreEqual(geldigeInput, deFeedback.Context);
//            Assert.AreEqual(geldigeInput, deFeedback.Doelstellingen);
//            Assert.AreEqual(geldigeInput, deFeedback.Onderwerp);
//            Assert.AreEqual(geldigeInput, deFeedback.Onderzoeksvraag);
//            Assert.AreEqual(geldigeInput, deFeedback.Titel);
//            Assert.AreEqual(suggestie, deFeedback.Suggesties);
//        }

        
//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeGroteFoutieveTitelBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback(foutieveInputGroterDan, geldigeInput, geldigeInput, geldigeInput, geldigeInput, geldigeInput, geldigeInput, suggestie);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeKleineFoutieveTitelBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback(foutieveInputKleinerDan, geldigeInput, geldigeInput, geldigeInput, geldigeInput, geldigeInput, geldigeInput, suggestie);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeGroteFoutieveContextBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback( geldigeInput,foutieveInputGroterDan, geldigeInput, geldigeInput, geldigeInput, geldigeInput, geldigeInput, suggestie);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeKleineFoutieveContextBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback( geldigeInput,foutieveInputKleinerDan, geldigeInput, geldigeInput, geldigeInput, geldigeInput, geldigeInput, suggestie);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeGroteFoutieveDoelstellingBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback( geldigeInput, geldigeInput,foutieveInputGroterDan, geldigeInput, geldigeInput, geldigeInput, geldigeInput, suggestie);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeKleineFoutieveDoelstellingBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback( geldigeInput, geldigeInput,foutieveInputKleinerDan, geldigeInput, geldigeInput, geldigeInput, geldigeInput, suggestie);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeGroteFoutieveBijdrageBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback(geldigeInput, geldigeInput,  geldigeInput, foutieveInputGroterDan,geldigeInput, geldigeInput, geldigeInput, suggestie);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeKleineFoutieveBijdrageBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback(geldigeInput, geldigeInput,  geldigeInput,foutieveInputKleinerDan, geldigeInput, geldigeInput, geldigeInput, suggestie);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeGroteFoutieveOnderzoeksvraagBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback(geldigeInput, geldigeInput, geldigeInput,  geldigeInput,foutieveInputGroterDan, geldigeInput, geldigeInput, suggestie);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeKleineFoutieveOnderzoeksvraagBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback(geldigeInput, geldigeInput, geldigeInput,  geldigeInput,foutieveInputKleinerDan, geldigeInput, geldigeInput, suggestie);
//        }
//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeGroteFoutieveOnderwerpBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback(geldigeInput, geldigeInput, geldigeInput, geldigeInput,  geldigeInput,foutieveInputGroterDan, geldigeInput, suggestie);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeKleineFoutieveOnderwerpBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback(geldigeInput, geldigeInput, geldigeInput, geldigeInput,  geldigeInput,foutieveInputKleinerDan, geldigeInput, suggestie);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeGroteFoutieveBronBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback(geldigeInput, geldigeInput, geldigeInput, geldigeInput, geldigeInput,  geldigeInput, foutieveInputGroterDan,suggestie);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ApplicationException))]
//        public void FeedbackMetTeKleineFoutieveBronBeoordelingGeeftException()
//        {
//            deFeedback = new Feedback(geldigeInput, geldigeInput, geldigeInput, geldigeInput, geldigeInput,  geldigeInput,foutieveInputKleinerDan, suggestie);
//        }


        

//    }
//}
