using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace p2g21.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="*")]
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }

        [Required(ErrorMessage="*")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }
    }

    public class WachtwoordVergetenModel
    {
        [Display(Name="Geef het emailadres in waaraan uw account is gekoppeld")]
        [Required(ErrorMessage="*")]
        public string Email { get; set; }
    }

    public class NieuwWachtwoordModel
    {
        [Required(ErrorMessage="*")]
        [DataType(DataType.Password)]
        [Display(Name="Geef hier je nieuwe wachtwoord in")]
        [MaxLength(20,ErrorMessage="*Wachtwoord mag niet langer dan 20 tekens zijn")]
        [MinLength(6,ErrorMessage="*Wachtwoord moet langer dan 6 tekens zijn")]
        [RegularExpression(@"^.*(?=.*\d)(?=.*[A-Za-z])(?=.*[@%&#]{0,}).*$", ErrorMessage = "Wachtwoord moet tenminste 3 van de volgende tekens bevatten : kleine letters, hoofletters, speciale karakters en cijfers")]
        public string NieuwWachtwoord { get; set; }
        [Required(ErrorMessage="*")]
        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("NieuwWachtwoord", ErrorMessage = "De wachtwoorden komen niet overeen")]
        public string BevestigWachtwoord { get; set; }
    }
    }
