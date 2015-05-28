//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using Moq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using p2g21.Controllers;
//using p2g21.Models.Domain.IRepositories;
//using p2g21.Tests.Controllers;

//namespace p2g21.Tests
//{
//    [TestClass]
//    public class PromotorControllerTest
//    {
//        private PromotorController controller;
//        private Mock<IGebruikerRepository> mocGebruikerRepository;
//        private int voorstelId;

//        [TestInitialize]
//        public void SetUpContext()
//        {
//            voorstelId = 1;

//            DummyDataContext context = new DummyDataContext();
//            mocGebruikerRepository = new Mock<IGebruikerRepository>();
//            mocGebruikerRepository.Setup(p => p.FindStudentsFromPromotorMetVoorstellen("Vanderplaetsen","Thomas")).Returns(context.StudentenLijst as IQueryable<Student>);
//            controller = new PromotorController(mocGebruikerRepository.Object);
//        }

//        [TestMethod]
//        public void IndexUsesConventionToChooseView() //moet de default view renderen
//        {
//            //Act
//            ViewResult result = controller.Index() as ViewResult;
//            //Assert
//            Assert.IsNotNull(result);
            
//        }
        
//        [TestMethod]
//        public void VoorstelVanStudentWillShowVoorstel()
//        {
//            PartialViewResult result = controller.VoorstelVanStudent(voorstelId) as PartialViewResult;
//            Voorstel voorstel = result.ViewData.Model as Voorstel;
//            Assert.IsNotNull(voorstel);
//        }

        

//    }
//}
