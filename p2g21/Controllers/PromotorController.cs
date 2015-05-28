using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Xml.Linq;
using p2g21.Models;
using p2g21.Models.Domain;
using p2g21.Models.Domain.IRepositories;
using WebGrease.Css.Extensions;
using p2g21.Models.ViewModels;

namespace p2g21.Controllers
{
    [Authorize(Roles="Promotor")]
    public class PromotorController : Controller
    {
        private IGebruikerRepository gebruikerRepository;
        
        public PromotorController(IGebruikerRepository gebruikerRepository)
        {
            this.gebruikerRepository = gebruikerRepository;
        }

        public ActionResult Index(Gebruiker gebruiker)
        {
            Promotor prom = gebruikerRepository.FindPromotorById(gebruiker.GebruikerId);

            return View("Index");
        }

        public ActionResult Evalueren(Gebruiker gebruiker)
        {
            Promotor promotor = (Promotor)gebruikerRepository.FindPromotorById(gebruiker.GebruikerId);
            List<Voorstel> voorstellen = new List<Voorstel>();
            List<Student> studenten = promotor.Studenten.ToList();
            List<Student> Teruggeven = new List<Student>();

            foreach (Student stud in studenten)
            {
                Student student = (Student) gebruikerRepository.FindStudentById(stud.GebruikerId);
                
                if(student.Voorstellen != null)
                {
                    Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.Toestand.Equals("Voorstel in behandeling"));
                    if(voorstel!=null)
                        Teruggeven.Add(student);
                }
            }

            if (Teruggeven == null)
                return View("Evalueren");
            else
            {
                ViewBag.Gebruiker = gebruiker;
                return View("Evalueren", Teruggeven);
            }
        }

        public ActionResult VoorstelBekijken(Gebruiker gebruiker, int id)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(id);

            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.Toestand.Equals("Voorstel in behandeling"));

            ViewBag.GebruikerId = student.GebruikerId;
            return View("Voorstel", voorstel);
        }

        public ActionResult AdviesBPCBekijken(Gebruiker gebruiker, int id)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(id);

            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.Toestand.Equals("Voorstel in behandeling"));

            ViewBag.GebruikerId = student.GebruikerId;

            if(voorstel.Advies == null)
            {
                TempData["Error"] = "Er is nog geen advies van de BPC";
                return RedirectToAction("Evalueren");
            }
            else
            {
                return View("AdviesBPCBekijken", voorstel);
            }
            
        }

        public ActionResult AdviesBPC(Gebruiker gebruiker, int id)
        {
            ViewBag.GebruikerId = id;
            return View("AdviesBPC", new AdviesVragenModel());
        }

        [HttpPost]
        public ActionResult AdviesBPC(AdviesVragenModel model, Gebruiker gebruiker, int id)
        {
            Student student = gebruikerRepository.FindStudentById(id);
            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.Toestand.Equals("Voorstel in behandeling"));

            voorstel.VraagAdvies(model.Vraag);

            gebruikerRepository.SaveChanges();
            BachelorProefCoordinator coor = gebruikerRepository.FindBachelorProefCoordinator();
            coor.ZendNotificatieMail("Adviesvraag voorstel " + student.Voornaam + " " + student.Naam, "Beste, de promotor heeft u een adviesvraag gesteld in verband met het voorstel van " + student.Voornaam + " " + student.Naam + ". De adviesvraag staat op u te wachten op de website.",coor.Email);
            
            TempData["Info"] = "Je hebt een adviesvraag gesteld aan de BPC over het voorstel van " + student.Voornaam + " " + student.Naam + ".";

            return RedirectToAction("Evalueren", gebruiker);
        }

        public ActionResult GeefFeedback(Gebruiker gebruiker, int id)
        {
            Student student = gebruikerRepository.FindStudentById(id);
            Dossier dossier = student.Dossiers.FirstOrDefault(m => m.IngediendVoorstel.Toestand.Equals("Voorstel in behandeling"));
            Voorstel voorstel = dossier.IngediendVoorstel;

            if (voorstel.Feedback == null)
            {
                ViewBag.GebruikerId = id;
                return View("GeefFeedback", new FeedbackCreateViewModel());
            }
            else
            {
                FeedbackCreateViewModel model = new FeedbackCreateViewModel();
                model.Bijdrage = voorstel.Feedback.Bijdrage;
                model.Bron = voorstel.Feedback.Bron;
                model.Context = voorstel.Feedback.Context;
                model.Doelstellingen = voorstel.Feedback.Doelstellingen;
                model.Onderwerp = voorstel.Feedback.Onderwerp;
                model.Onderzoeksvraag = voorstel.Feedback.Onderzoeksvraag;
                model.Opmerkingen = voorstel.Feedback.Opmerkingen;
                model.Suggesties = voorstel.Feedback.Suggesties;
                model.Titel = voorstel.Feedback.Titel;

                return View("GeefFeedback", model);
            }
        }

        [HttpPost]
        public ActionResult GeefFeedback(FeedbackCreateViewModel model, Gebruiker gebruiker, int id)
        {
            Student student = gebruikerRepository.FindStudentById(id);
            Dossier dossier = student.Dossiers.FirstOrDefault(m => m.IngediendVoorstel.Toestand.Equals("Voorstel in behandeling"));
            Voorstel voorstel = dossier.IngediendVoorstel;
            Feedback newFeedback = new Feedback(model.Titel, model.Context, model.Doelstellingen, model.Bijdrage, model.Onderzoeksvraag, model.Onderwerp, model.Bron, model.Suggesties, model.Opmerkingen);

            if(voorstel.Feedback == null)
            {
                voorstel.VoegFeedbackToe(newFeedback);
            }
            else
            {
                voorstel.Feedback.updateFeedback(newFeedback);
            }

            gebruikerRepository.SaveChanges();
            TempData["Info"] = "De feedback op het voorstel van " + student.Voornaam + " " + student.Naam + " is opgeslagen.";

            return RedirectToAction("Evalueren");
        }

        public ActionResult VoorstelGoedkeuren(Gebruiker gebruiker, int id)
        {
            Student student = gebruikerRepository.FindStudentById(id);
            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.Toestand.Equals("Voorstel in behandeling"));

            if(voorstel.Feedback != null)
            {
                voorstel.Goedkeuren();
                gebruikerRepository.SaveChanges();

                BachelorProefCoordinator coor = gebruikerRepository.FindBachelorProefCoordinator();
                coor.ZendNotificatieMail("Voorstel van " + student.Voornaam + " " + student.Naam + " goedgekeurd", "Beste, het voorstel van " + student.Voornaam + " " + student.Naam + " is goedgekeurd.",coor.Email);
                student.ZendNotificatieMail("Goedkeuring voorstel", "Beste, uw voorstel met titel " + voorstel.VoorstelTitel + " is goedgekeurd door de promotor. Uw voorstel staat te wachten op uw goedkeuring op de website.",student.Email);
                TempData["Info"] = "Het voorstel van " + student.Voornaam + " " + student.Naam + " is goedgekeurd.";
                return RedirectToAction("Evalueren", gebruiker);
            }
            TempData["Error"] = "U moet eerst feedback op het voorstel geven";
            return RedirectToAction("Evalueren",gebruiker);
        }

        public ActionResult VoorstelAfkeuren(Gebruiker gebruiker, int id)
        {
            Student student = gebruikerRepository.FindStudentById(id);
            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.Toestand.Equals("Voorstel in behandeling"));

            voorstel.NaarNieuwVoorstelToestand();
            gebruikerRepository.SaveChanges();

            BachelorProefCoordinator coor = gebruikerRepository.FindBachelorProefCoordinator();
            coor.ZendNotificatieMail("Voorstel van " + student.Voornaam + " " + student.Naam + " afgekeurd", "Beste, het voorstel van " + student.Voornaam + " " + student.Naam + " is afgekeurd.", coor.Email);
            student.ZendNotificatieMail("Afkeuring voorstel", "Beste, uw voorstel met titel " + voorstel.VoorstelTitel + " is afgekeurd door de promotor. U kan wijzigingen aanbrengen via de website",student.Email);
            
            TempData["Info"] = "Het voorstel van " + student.Voornaam + " " + student.Naam + " is afgekeurd.";

            return RedirectToAction("Evalueren", gebruiker);
        }

        public ActionResult DeadlineWijzigen(Gebruiker gebruiker)
        {
            Promotor prom = gebruikerRepository.FindPromotorById(gebruiker.GebruikerId);
            List<Student> studenten = prom.Studenten.ToList();

            return View("DeadlineWijzigen", studenten);
        }

        public ActionResult Deadline(Gebruiker gebruiker, int id)
        {
            Student student = gebruikerRepository.FindStudentById(id);
            ViewBag.StudentId = id;
            DeadlineWijzigenModel model = new DeadlineWijzigenModel();
            model.Deadline = student.Deadline;
            return View("Deadline", model);
        }

        [HttpPost]
        public ActionResult Deadline(Gebruiker gebruiker, DeadlineWijzigenModel model, int id)
        {
            Student student = gebruikerRepository.FindStudentById(id);
            student.Deadline = model.Deadline;
            gebruikerRepository.SaveChanges();
            TempData["Info"] = "De deadline van " + student.Voornaam + " " + student.Naam + " is gewijzigd.";

            return RedirectToAction("DeadlineWijzigen");
        }

        public ActionResult OverzichtDossiers(Gebruiker gebruiker)
        {
            Promotor prom = gebruikerRepository.FindPromotorById(gebruiker.GebruikerId);
            return View("OverzichtDossiers", prom);
        }

        public ActionResult Feedback(Gebruiker gebruiker, int id)
        {
            Student student = gebruikerRepository.FindStudentById(id);
            Dossier dossier = student.Dossiers.FirstOrDefault(m => m.IngediendVoorstel.Toestand.Equals("Voorstel in behandeling"));
            Voorstel voorstel = dossier.IngediendVoorstel;
            Feedback feedback = voorstel.Feedback;

            if (feedback == null)
            {
                TempData["Error"] = "Er is nog geen feedback bij dit dossier";
                return RedirectToAction("OverzichtDossiers", gebruiker);
            }
            else
            {
                FeedbackBekijkenModel model = new FeedbackBekijkenModel(voorstel.VoorstelId, feedback);
                return View("Feedback", model);
            }
        }

        public ActionResult VoorstelBekijkenODV(Gebruiker gebruiker, int ids, int idv)
        {
            Student student = gebruikerRepository.FindStudentById(ids);
            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.VoorstelId == idv);

            return View("VoorstelBekijkenODV", voorstel);
        }
    }
}
