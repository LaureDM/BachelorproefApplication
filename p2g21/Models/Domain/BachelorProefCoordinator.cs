using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace p2g21.Models.Domain
{
    public class BachelorProefCoordinator : Gebruiker
    {
        public BachelorProefCoordinator(String Loginnaam, String Naam, String Voornaam) : base(Loginnaam,Naam,Voornaam)
        {

        }
        public BachelorProefCoordinator()
        {

        }
        public override void NieuwWachtwoord(string wachtwoord)
        {
            this.Wachtwoord = wachtwoord;
        }
    }
}
