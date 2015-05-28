using MySql.Data.MySqlClient;
using p2g21.Models;
using p2g21.Models.DAL;
using p2g21.Models.DAL.Repositories;
using p2g21.Models.Domain;
using p2g21.Models.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace p2g21.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        private IGebruikerRepository repos;

        public AccountController(IGebruikerRepository reposit)
        {
            this.repos = reposit;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            Gebruiker gebruiker = repos.FindGebruikerByName(model.UserName);
            string roles = "";
            if (ModelState.IsValid)
            {
                if (gebruiker != null)
                {
                    if (gebruiker.DoesPasswordMatch(model.Password))
                    {

                        if (gebruiker.GetType().Equals(typeof(Student)))
                        {
                            roles = "Student";
                            
                        }
                        else if (gebruiker.GetType().Equals(typeof(Promotor)))
                        {
                            roles = "Promotor";
                            
                        }
                        else if (gebruiker.GetType().Equals(typeof(BachelorProefCoordinator)))
                        {
                            roles = "BPC";
                            
                        }
                        
                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                1,
                                model.UserName,  //user id
                                DateTime.Now,
                                DateTime.Now.AddMinutes(20),  // expiry
                                false,  //do not remember
                                roles,
                                "/");
                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                           FormsAuthentication.Encrypt(authTicket));
                            Response.Cookies.Add(cookie);

                        if (gebruiker.EersteGebruik)
                            return View("NieuwWachtwoord2");
                        if (gebruiker.GetType().Equals(typeof(Student)))
                            return RedirectToAction("Index", "Student");
                        if (gebruiker.GetType().Equals(typeof(Promotor)))
                            return RedirectToAction("Index", "Promotor");
                        if (gebruiker.GetType().Equals(typeof(BachelorProefCoordinator)))
                            return RedirectToAction("Index", "BachelorProefCoordinator");
                        else
                            return RedirectToAction("Index");                       
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Verkeerd wachtwoord ingegeven");
                        return View("Login");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "Verkeerde gebruikersnaam ingegeven");
                    return View("Login");
                }
            }
            return View("Login");
        }

        public ActionResult WachtwoordVergeten()
        {
            return View("WachtwoordVergeten");   
        }

        [HttpPost]
        public ActionResult WachtwoordVergeten(WachtwoordVergetenModel model)
        {
            Gebruiker gebruiker = repos.FindGebruikerByEmail(model.Email);
            if (gebruiker == null)
            {
                ModelState.AddModelError("Email", "Het opgegeven e-mailadres werd niet teruggevonden");
                return View("WachtwoordVergeten");
            }
            else
            {
                String pass = gebruiker.GenereerNieuwWachtwoord();
                gebruiker.Wachtwoord = pass;
                gebruiker.EncrypteerWachtwoord();
                gebruiker.EersteGebruik = true;
                gebruiker.ZendEmail(model.Email, pass);
                repos.SaveChanges();
                TempData["Info"] = "Een mail is verzonden met uw nieuwe wachtwoord";
                return RedirectToAction("Login");
            }
        }   
  
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        [HttpPost]
        public ActionResult NieuwWachtwoord(NieuwWachtwoordModel model, Gebruiker gebr)
        {
            Gebruiker gebruiker = repos.FindGebruikerById(gebr.GebruikerId);
            if (ModelState.IsValid)
            {
                String pass = model.NieuwWachtwoord;
                gebruiker.Wachtwoord = pass;
                gebruiker.EncrypteerWachtwoord();
                gebruiker.EersteGebruik = false;
                repos.SaveChanges();

                TempData["Info"] = "Uw wachtwoord is gewijzigd";

                return RedirectToAction("Login");
            }
            else
                return View("NieuwWachtwoord2", model);
        }
    }
}
