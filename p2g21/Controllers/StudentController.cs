using MySql.Data.MySqlClient;
using p2g21.Models;
using p2g21.Models.DAL;
using p2g21.Models.DAL.Repositories;
using p2g21.Models.Domain;
using p2g21.Models.Domain.Exceptions;
using p2g21.Models.Domain.IRepositories;
using p2g21.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace p2g21.Controllers
{
    public class StudentController : Controller
    {
        IGebruikerRepository gebruikerRepository;
        IOnderzoeksdomeinRepository onderzoeksdomeinRepository;


        public StudentController(GebruikerRepository repos, OnderzoeksdomeinRepository onderzoekRepos)
        {
            gebruikerRepository = repos;
            onderzoeksdomeinRepository = onderzoekRepos;
         
        }

        public ActionResult Index(Gebruiker gebruiker)
        {
            return View("Index");
        }

        public ActionResult Voorstellen(Gebruiker gebruiker)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            if (student.Voorstellen == null)
                return View("Voorstellen", student.Voorstellen);
            else
            {
                IEnumerable<Voorstel> voorstellen = student.Voorstellen.OrderBy(m => m.VoorstelTitel).ToList();
                ViewBag.Gebruiker = gebruiker;
                return View("Voorstellen", voorstellen);

            }
        }

        public ActionResult Dossiers(Gebruiker gebruiker)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            return View("Dossiers", student);
        }

        public ActionResult FeedbackBekijken(Gebruiker gebruiker,int id)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            Dossier dossier = student.Dossiers.FirstOrDefault(m => m.DossierId == id);
            Feedback feedback = dossier.IngediendVoorstel.Feedback;
            ViewBag.DossierId = dossier.DossierId;
            FeedbackBekijkenModel model = new FeedbackBekijkenModel(dossier.IngediendVoorstel.VoorstelId, feedback);
            return View("Feedback", model);
        }
        public ActionResult VoorstelGoedkeuren(Gebruiker gebruiker, int id)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            Dossier dossier = student.Dossiers.FirstOrDefault(m => m.DossierId == id);
            Feedback feedback = dossier.IngediendVoorstel.Feedback;
            feedback.Goedkeuren();
            dossier.Goedkeuren();
            gebruikerRepository.SaveChanges();
            return View("Dossiers", student);
        }

        public ActionResult VoorstelAfkeuren(Gebruiker gebruiker, int id)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            Dossier dossier = student.Dossiers.FirstOrDefault(m => m.DossierId == id);
            Feedback feedback = dossier.IngediendVoorstel.Feedback;
            feedback.Afkeuren();
            dossier.Afkeuren();
            gebruikerRepository.SaveChanges();
            return View("Dossiers", student);
        }
        public ActionResult VoorstelBekijken(Gebruiker gebruiker, int id)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            Dossier dossier = student.Dossiers.FirstOrDefault(m => m.DossierId == id);
            return View("VoorstelBekijken", dossier.IngediendVoorstel);
        }
        public ActionResult VoorstelIndienen(string returnUrl, Gebruiker gebruiker, int id)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.VoorstelId == id);
            
            return View("VoorstelIndienen", voorstel);
                
        }

        [HttpPost]
        public ActionResult VoorstelIndienen(Gebruiker gebruiker, int id)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            Promotor promotor = (Promotor)gebruikerRepository.FindAllPromotors().ToList().FirstOrDefault(m=>m.Studenten.Contains(student));
            BachelorProefCoordinator coord = (BachelorProefCoordinator)gebruikerRepository.FindBachelorProefCoordinator();
            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.VoorstelId == id);
            try
            {
                student.IndienenVoorstel(id);
                gebruikerRepository.SaveChanges();
                student.ZendNotificatieMail("Voorstel ingediend door " + student.Voornaam + " " + student.Naam, "Er is een voorstel ingediend door "+ student.Voornaam + " " + student.Naam+".\nDit voorstel wacht op uw goedkeuring.",promotor.Email);
                student.ZendNotificatieMail("Voorstel ingediend door " + student.Voornaam + " " + student.Naam, "Er is een voorstel ingediend door "+ student.Voornaam + " " + student.Naam+".\nDe promotor "+promotor.Voornaam+" "+promotor.Naam+" kan dit voorstel nu goed- of afkeuren.", coord.Email);
                TempData["Info"] = "Voorstel \"" + voorstel.VoorstelTitel + "\" werd ingediend";
                return RedirectToAction("Voorstellen", student.Voorstellen);
            }
            catch(VoorstelIndienenExceptie e)
            {
                TempData["Error"] = e.Message;
                return View("Voorstellen", student.Voorstellen);
            }

        }

        public ActionResult VoorstelOpslaan(Gebruiker gebruiker)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            VoorstelIndienenModel model = new VoorstelIndienenModel();
            model.SelectList1 = new SelectList(onderzoeksdomeinRepository.FindAll().OrderBy(m => m.Naam), "Naam", "Naam", model.Onderzoeksdomein1);
            model.SelectList2 = new SelectList(onderzoeksdomeinRepository.FindAll().OrderBy(m => m.Naam), "Naam", "Naam", model.Onderzoeksdomein2);
            model.student = student;
            return View("VoorstelOpslaan", model);

        }


        [HttpPost]
        public ActionResult VoorstelOpslaan(VoorstelIndienenModel model, Gebruiker gebruiker)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            Voorstel voorstel = new Voorstel();

            if(ModelState.IsValid)
            { 
            Onderzoeksdomein o1 = onderzoeksdomeinRepository.FindOnderzoeksdomeinByName(model.Onderzoeksdomein1);
            Onderzoeksdomein o2 = onderzoeksdomeinRepository.FindOnderzoeksdomeinByName(model.Onderzoeksdomein2);
            List<Onderzoeksdomein> domeinen = new List<Onderzoeksdomein>() { o1, o2 };
            voorstel = new Voorstel(model.Voornaamcp, model.Naamcp, model.Emailcp, model.Organisatiecp, model.Titel, domeinen, model.Trefwoorden, model.Probleemstelling, model.Context, model.Doelstelling, model.Onderzoeksvraag, model.PlanVanAanpak, model.ReferentieLijst, "Nieuw voorstel");
            voorstel.Od1 = o1;
            voorstel.Od2 = o2;
            student.AddVoorstel(voorstel);
            gebruikerRepository.SaveChanges();
            TempData["Info"] = "Voorstel \"" + voorstel.VoorstelTitel + "\" is bewaard";
            return RedirectToAction("Voorstellen", gebruiker);
            }
            else
            {
                model.SelectList1 = new SelectList(onderzoeksdomeinRepository.FindAll().OrderBy(m => m.Naam), "Naam", "Naam", model.Onderzoeksdomein1);
                model.SelectList2 = new SelectList(onderzoeksdomeinRepository.FindAll().OrderBy(m => m.Naam), "Naam", "Naam", model.Onderzoeksdomein2);
                model.student = student;
                return View("VoorstelOpslaan", model);
            }
        }


        public ActionResult VoorstelAanpassen(Gebruiker gebruiker, int id)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.VoorstelId == id);
            IEnumerable<Onderzoeksdomein> onderzoeksdomeinen = onderzoeksdomeinRepository.FindAll().OrderBy(m => m.Naam).ToList();
            
            if (voorstel == null)
            {
                VoorstelIndienenModel model = new VoorstelIndienenModel();
                model.SelectList1 = new SelectList(onderzoeksdomeinRepository.FindAll().OrderBy(m => m.Naam), "Naam", "Naam", model.Onderzoeksdomein1);
                model.SelectList2 = new SelectList(onderzoeksdomeinRepository.FindAll().OrderBy(m => m.Naam), "Naam", "Naam", model.Onderzoeksdomein2);
                model.student = student;
                return View("VoorstelOpslaan", model);
            }
            if(!voorstel.KanVerwijderdWorden)
            {
                TempData["Error"] = "Voorstel kan niet meer aangepast worden";
                return View("Voorstellen", student.Voorstellen);
            }
            else
            {
                VoorstelIndienenModel model = new VoorstelIndienenModel(voorstel);
                model.SelectList1 = new SelectList(onderzoeksdomeinRepository.FindAll().OrderBy(m => m.Naam), "Naam", "Naam", model.Onderzoeksdomein1);
                model.SelectList2 = new SelectList(onderzoeksdomeinRepository.FindAll().OrderBy(m => m.Naam), "Naam", "Naam", model.Onderzoeksdomein2);
                model.student = student;
                return View("VoorstelOpslaan", model);
            }
        }

        [HttpPost]
        public ActionResult VoorstelAanpassen(Gebruiker gebruiker, int id, VoorstelIndienenModel model)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.VoorstelId == id);
            if(ModelState.IsValid)
            { 
            Onderzoeksdomein o1 = onderzoeksdomeinRepository.FindOnderzoeksdomeinByName(model.Onderzoeksdomein1);
            Onderzoeksdomein o2 = onderzoeksdomeinRepository.FindOnderzoeksdomeinByName(model.Onderzoeksdomein2);
            List<Onderzoeksdomein> domeinen = new List<Onderzoeksdomein>() { o1, o2 };
            voorstel.PasVoorstelAan(model.Voornaamcp, model.Naamcp, model.Emailcp, model.Organisatiecp, model.Titel, domeinen, model.Trefwoorden, model.Probleemstelling, model.Context, model.Doelstelling, model.Onderzoeksvraag, model.PlanVanAanpak, model.ReferentieLijst, o1, o2);
            gebruikerRepository.SaveChanges();
            TempData["Info"] = "Voorstel \"" + voorstel.VoorstelTitel + "\" is aangepast";
            return RedirectToAction("Voorstellen", student.Voorstellen);
            }
            else
            {
                model.SelectList1 = new SelectList(onderzoeksdomeinRepository.FindAll().OrderBy(m => m.Naam), "Naam", "Naam", model.Onderzoeksdomein1);
                model.SelectList2 = new SelectList(onderzoeksdomeinRepository.FindAll().OrderBy(m => m.Naam), "Naam", "Naam", model.Onderzoeksdomein2);
                model.student = student;
                return View("VoorstelOpslaan", model);
            }

        }

        public ActionResult Delete(Gebruiker gebruiker, int id)
        {
            Student student = gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.VoorstelId == id);
            return View("VoorstelVerwijderen", voorstel);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Gebruiker gebruiker, int id)
        {
            Student student = gebruikerRepository.FindStudentById(gebruiker.GebruikerId);
            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.VoorstelId == id);
            try
            {
                student.DeleteVoorstel(voorstel);
                gebruikerRepository.SaveChanges();
                TempData["Info"] = "Voorstel \"" + voorstel.VoorstelTitel + "\" werd verwijderd";
            }
            catch (VoorstelVerwijderenExceptie ex)
            {
                TempData["Error"] = ex.Message;   
            }
            catch(ArgumentException exx)
            {
                TempData["Error"] = exx.Message;
            }
            return RedirectToAction("Voorstellen", student.Voorstellen);
        }

    }




}

