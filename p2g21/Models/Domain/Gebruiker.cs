using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

namespace p2g21.Models.Domain
{
    public abstract class Gebruiker
    {
        public int GebruikerId { get; set; }
        protected Gebruiker() { }
        protected Gebruiker(String Loginnaam, String Naam, String Voornaam)
        {
            this.Voornaam = Voornaam;
            this.Naam = Naam;
            this.Loginnaam = Loginnaam;
        }

        public string Loginnaam{get;set;}

        public string Naam{get;set;}

        public string Voornaam { get; set; }

        public abstract void NieuwWachtwoord(String wachtwoord);

        public bool DoesPasswordMatch(string userEnteredPassword)
        {
            return BCrypt.CheckPassword(userEnteredPassword, Wachtwoord);
        }

        public void EncrypteerWachtwoord()
        {
            this.Wachtwoord = BCrypt.HashPassword(Wachtwoord, BCrypt.GenerateSalt());
        }
        public String Email { get; set; }

        //encrypteren
        public string Wachtwoord { get; set; }

        public bool EersteGebruik { get; set; }

        public string GenereerNieuwWachtwoord()
        {
            Random random = new Random();
            string output = "";
                for(int i=0;i<2;i++)
                {
                    int rnd = random.Next(33,47);
                    output += (char)rnd;
                }
                for(int i=0;i<2;i++)
                {
                    int rnd = random.Next(48, 57);
                    output += (char)rnd;
                }
                for(int i=0;i<2;i++)
                {
                    int rnd = random.Next(65, 90);
                    output += (char)rnd;
                }
                for(int i=0;i<2;i++)
                {
                    int rnd = random.Next(97, 122);
                    output += (char)rnd;
                }
            
            List<char> shuffleList = new List<char>(output.ToCharArray());
            int lengte = shuffleList.Count;
            output = "";

            for(int i=0;i<lengte;i++)
            {
                int rnd = random.Next(shuffleList.Count);
                output += shuffleList[rnd];
                shuffleList.Remove(shuffleList[rnd]);
            }

            return output;

        }

        public void ZendEmail(string emailadres, String pass)
        {
            MailMessage email = new MailMessage();
            email.To.Add(emailadres);
            email.Subject = "Aanvraag nieuw wachtwoord";
            email.From = new MailAddress("noreply.t3037@gmail.com");
            email.Body = "Beste " + Voornaam + "\nUw nieuwe wachtwoord:\n" + pass + "\nDit is een automatisch genegereerd wachtwoord waarmee u kan inloggen. Bij het inloggen kan u uw wachtwoord veranderen naar iets dat u makkelijker onthoudt. \nMet vriendelijke groeten\nHet Hogent wachtwoord team";
            SmtpClient client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email.From.Address, "MaFaLaGl")
            };
            client.Send(email);
        }

        public void ZendNotificatieMail(String onderwerp, String bericht, string emailadres)
        {
            MailMessage email = new MailMessage();
            email.To.Add(emailadres);
            email.Subject = onderwerp;
            email.From = new MailAddress("noreply.t3037@gmail.com");
            email.Body = bericht;
            SmtpClient client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email.From.Address, "MaFaLaGl")
            };
            client.Send(email);
        }
    }
}
