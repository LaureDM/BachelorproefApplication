using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace p2g21.Models.Domain
{
   
    public class Promotor : Gebruiker
    {
        public Promotor()
        {
            Studenten = new List<Student>();
        }
        public Promotor(String Loginnaam, String Naam, String Voornaam) : base(Loginnaam,Naam,Voornaam)
        {
            Studenten = new List<Student>();
        }

        public virtual ICollection<Student> Studenten { get; private set; }   
        public override void NieuwWachtwoord(string wachtwoord)
        {
            Wachtwoord = wachtwoord;
        }
    }
}