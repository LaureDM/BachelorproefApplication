using System;
using System.Collections;
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
using p2g21.Models.ViewModels;
using WebGrease.Css.Extensions;

namespace p2g21.Controllers
{
    [Authorize(Roles="BPC")]
    public class BachelorProefCoordinatorController : Controller
    {
        private IGebruikerRepository gebruikerRepository;
        private IOnderzoeksdomeinRepository onderzoeksdomeinRepository;

        public BachelorProefCoordinatorController(IGebruikerRepository gebruikerRepository, IOnderzoeksdomeinRepository rep)
        {
            this.gebruikerRepository = gebruikerRepository;
            onderzoeksdomeinRepository = rep;
        }

        public ActionResult Index(Gebruiker gebruiker)
        {
            return View("Index");
        }

        public ActionResult Historiek(Gebruiker gebruiker)
        {
            List<Student> studenten = gebruikerRepository.FindAllStudenten().ToList();
            return View("Historiek",studenten);
        }

        public ActionResult HistoriekDossiersStudent(Gebruiker gebruiker, int id)
        {
            Student student = gebruikerRepository.FindStudentById(id);
            ViewBag.Naam = student.Voornaam + " " + student.Naam;
            return View("HistoriekDossiersStudent", student.Dossiers);
        }

        public ActionResult HistoriekTonen(Gebruiker gebruiker, int id)
        {
            Student student = gebruikerRepository.FindStudentById(id);
            return View("HistoriekTonen", student);
        }
        public ActionResult AdviesGeven(Gebruiker gebruiker)
        {
            BachelorProefCoordinator coordinator = (BachelorProefCoordinator)gebruikerRepository.FindBachelorProefCoordinator(gebruiker.GebruikerId);
            List<Voorstel> voorstellen = new List<Voorstel>();
            List<Student> studenten = gebruikerRepository.FindAllStudenten().ToList();
            List<Student> Teruggeven = new List<Student>();

            foreach (Student stud in studenten)
            {
                Student student = (Student)gebruikerRepository.FindStudentById(stud.GebruikerId);

                if (student.Voorstellen != null)
                {
                    Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.Toestand.Equals("Advies BPC"));
                    if (voorstel != null)
                        Teruggeven.Add(student);
                }
            }
            return View("AdviesGeven", Teruggeven);
        }

        public ActionResult VoorstelBekijken(Gebruiker gebruiker, int id)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(id);

            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.Toestand.Equals("Advies BPC"));

            ViewBag.GebruikerId = student.GebruikerId;
            return View("Voorstel", voorstel);
        }

        public ActionResult Advies(Gebruiker gebruiker, int id)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(id);
            Voorstel voorstel = student.Dossiers.FirstOrDefault(m => m.IngediendVoorstel.Toestand.Equals("Advies BPC")).IngediendVoorstel;
            AdviesGevenModel model = new AdviesGevenModel();
            model.Vraag = voorstel.AdviesVraag;
            return View("Advies", model);
        }

        [HttpPost]
        public ActionResult Advies(AdviesGevenModel model, Gebruiker gebruiker, int id)
        {
            Student student = gebruikerRepository.FindStudentById(id);
            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.Toestand.Equals("Advies BPC"));

            voorstel.GeefAdvies(model.Antwoord);

            gebruikerRepository.SaveChanges();
            TempData["Info"] = "Je advies over het voorstel van " + student.Voornaam + " " + student.Naam + " is geregistreerd.";

            return RedirectToAction("AdviesGeven", gebruiker);
        }

        public ActionResult PromotorWijzigen(Gebruiker gebruiker, int id)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(id);
            PromotorWijzigenModel model = new PromotorWijzigenModel();
            model.lijst = new SelectList(gebruikerRepository.FindAllPromotors(), "Naam","Naam",model.promotor);
            model.id = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult PromotorWijzigen(PromotorWijzigenModel model,Gebruiker gebruiker, int id)
        {
            Student student = (Student)gebruikerRepository.FindStudentById(id);
            Promotor oorspr = gebruikerRepository.FindAllPromotors().ToList().FirstOrDefault(m => m.Studenten.Contains(student));
            Promotor promotor = gebruikerRepository.FindAllPromotors().ToList().FirstOrDefault(m => m.Naam.Equals(model.promotor));
            oorspr.Studenten.Remove(student);
            promotor.Studenten.Add(student);
            TempData["Info"] = "Promotor werd gewijzigd van " + student.Voornaam + " " + student.Naam;
            return View("OverzichtDossiers", gebruikerRepository.FindAllPromotors().ToList());
        }
        public ActionResult OverzichtDossiers(Gebruiker gebruiker)
        {
            List<Promotor> prom = gebruikerRepository.FindAllPromotors().ToList();
            return View("OverzichtDossiers",prom);
        }

        public ActionResult OnderzoeksdomeinToevoegen()
        {
            Onderzoeksdomein domein = new Onderzoeksdomein();
            return View("OnderzoeksdomeinToevoegen", domein);
        }

        [HttpPost]
        public ActionResult OnderzoeksdomeinToevoegen(Onderzoeksdomein domein)
        {
            Onderzoeksdomein domeincheck = onderzoeksdomeinRepository.FindAll().ToList().FirstOrDefault(m => m.Naam.ToLower().Equals(domein.Naam.ToLower()));
            if (domeincheck != null)
            {
                TempData["Error"] = "Onderzoeksdomein bestaat reeds";
                return View("OnderzoeksdomeinToevoegen");
            }
            else
            {
                onderzoeksdomeinRepository.AddOnderzoeksdomein(domein);
                onderzoeksdomeinRepository.SaveChanges();
                TempData["Info"] = "Het onderzoeksdomein \"" + domein.Naam + "\" werd toegevoegd";
                return View("OnderzoeksdomeinToevoegen");
            }
        }

        public ActionResult VoorstelBekijkenODV(Gebruiker gebruiker, int ids, int idv)
        {
            Student student = gebruikerRepository.FindStudentById(ids);
            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.VoorstelId == idv);
            VoorstelIndienenModel model = new VoorstelIndienenModel(voorstel);
            model.SelectList1 = new SelectList(onderzoeksdomeinRepository.FindAll().OrderBy(m => m.Naam), "Naam", "Naam", model.Onderzoeksdomein1);
            model.SelectList2 = new SelectList(onderzoeksdomeinRepository.FindAll().OrderBy(m => m.Naam), "Naam", "Naam", model.Onderzoeksdomein2);
            model.vID = idv;
            model.sID = ids;
            return View("VoorstelBekijkenODV", model);
        }
        [HttpPost]
        public ActionResult VoorstelBekijkenODV(Gebruiker gebruiker,int ids, int idv,VoorstelIndienenModel model)
        {
            List<Promotor> prom = gebruikerRepository.FindAllPromotors().ToList();
            Student student = gebruikerRepository.FindStudentById(ids);
            Voorstel voorstel = student.Voorstellen.FirstOrDefault(m => m.VoorstelId == idv);
            Onderzoeksdomein o1 = onderzoeksdomeinRepository.FindOnderzoeksdomeinByName(model.Onderzoeksdomein1);
            Onderzoeksdomein o2 = onderzoeksdomeinRepository.FindOnderzoeksdomeinByName(model.Onderzoeksdomein2);
            List<Onderzoeksdomein> domeinen = new List<Onderzoeksdomein>() { o1, o2 };
            voorstel.Od1 = o1;
            voorstel.Od2 = o2;
            gebruikerRepository.SaveChanges();
            TempData["Info"] = "Onderzoeksdomein van voorstel \"" + voorstel.VoorstelTitel + "\" werd aangepast";
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

        public ActionResult FeedbackAdviesGeven(Gebruiker gebruiker, int id)
        {
            Student student = gebruikerRepository.FindStudentById(id);
            Dossier dossier = student.Dossiers.FirstOrDefault(m => m.IngediendVoorstel.Toestand.Equals("Advies BPC"));
            Voorstel voorstel = dossier.IngediendVoorstel;
            Feedback feedback = voorstel.Feedback;

            if(feedback == null)
            {
                TempData["Error"] = "Er is nog geen feedback bij dit dossier";
                return RedirectToAction("AdviesGeven", gebruiker);
            }
            else
            {
                FeedbackBekijkenModel model = new FeedbackBekijkenModel(voorstel.VoorstelId, feedback);
                return View("FeedbackAdviesGeven", model);
            }
        }
    }
}