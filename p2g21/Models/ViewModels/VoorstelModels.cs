using p2g21.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace p2g21.Models.ViewModels
{
  
    public class VoorstelIndienenModel
    {
        public int sID { get; set; }
        public int vID { get; set; }
        public Student student { get; set; }
        [Display(Name = "Naam student")]
        public string NaamStudent { get; set; }
        [Display(Name = "Voornaam student")]
        public string VoornaamStudent { get; set; }
        [Display(Name = "Emailadres student")]
        public string EmailStudent { get; set; }
        [Display(Name="Voornaam co-promotor")]
        public string Voornaamcp { get; set; }
        [Display(Name = "Naam co-promotor")]
        public string Naamcp { get; set; }
        [Display(Name = "Emailadres co-promotor")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage="Gelieve een geldig e-mailadres in te vullen")]
        public string Emailcp { get; set; }
        [Display(Name = "Organisatie co-promotor")]
        public string Organisatiecp { get; set; }
        [Display(Name = "Titel bachelorproef")]
        [Required(ErrorMessage = "*")]
        public string Titel { get; set; }
        [Display(Name = "Onderzoeksdomein 1")]
        [Required(ErrorMessage = "*")]
        public string Onderzoeksdomein1 { get; set; }
        [Display(Name = "Onderzoeksdomein 2")]
        public string Onderzoeksdomein2{get; set;}
        [Display(Name = "Trefwoorden")]
        [Required(ErrorMessage="*")]
        public string Trefwoorden { get; set; }
        [Required(ErrorMessage = "*")]
        public string Probleemstelling { get; set; }
        [Required(ErrorMessage = "*")]
        public string Context { get; set; }
        [Required(ErrorMessage = "*")]
        public string Doelstelling { get; set; }
        [Required(ErrorMessage = "*")]
        public string Onderzoeksvraag { get; set; }
        [Display(Name = "Plan van aanpak")]
        [Required(ErrorMessage = "*")]
        public string PlanVanAanpak { get; set; }
        [Display(Name = "Referenties")]
        [Required(ErrorMessage = "*")]
        public string ReferentieLijst { get; set; }

        public SelectList SelectList1 { get; set; }
        public SelectList SelectList2 { get; set; }
        public DateTime TijdstipIndienen { get; set; }
        public string Toestand { get; set; }
        public VoorstelIndienenModel(Voorstel voorstel)
        {
            Voornaamcp = voorstel.VoornaamCP;
            Naamcp = voorstel.NaamCP;
            Emailcp = voorstel.EmailCP;
            Organisatiecp = voorstel.OrganisatieCp;
            Titel = voorstel.VoorstelTitel;
            Onderzoeksdomein1 = voorstel.Od1.Naam;
            if(voorstel.Od2!=null)
                Onderzoeksdomein2 = voorstel.Od2.Naam;
            Trefwoorden = voorstel.VrijeTrefwoorden;
            Probleemstelling = voorstel.ProbleemStelling;
            Doelstelling = voorstel.Doelstelling;
            Context = voorstel.Context;
            Onderzoeksvraag = voorstel.Onderzoeksvraag;
            PlanVanAanpak = voorstel.PlanVanAanpak;
            ReferentieLijst = voorstel.ReferentieLijst;
            TijdstipIndienen = voorstel.TijdstipIndienen;
            Toestand = voorstel.Toestand;
        }
        public VoorstelIndienenModel() { }

    }

    public class AdviesVragenModel
    {
     
        [Required(ErrorMessage = "Verplicht in te vullen")]
        [Display(Name = "Vraag advies aan BPC")]
        [DataType(DataType.MultilineText)]
        public string Vraag { get; set; }
    }

    public class AdviesGevenModel
    {
        [Required(ErrorMessage = "Verplicht in te vullen")]
        [Display(Name = "Geef advies aan de promotor")]
        [DataType(DataType.MultilineText)]
        public string Antwoord { get; set; }
        public string Vraag { get; set; }
    }

    public class FeedbackBekijkenModel
    {
 
       public int VoorstelId { get; set; }

        [Display(Name = "De titel beschrijft specifiek wat het onderwerp van de scriptie is.")]
        public int Titel { get; set; }


        [Display(Name = "De context is duidelijk beschreven, vaktermen worden uitgelegd.")]
        public int Context { get; set; }


        [Display(Name = "De doelstellingzijn “S.M.A.R.T.” (specifiek, meetbaar, aanvaardbaar, realistisch en tijdsgebonden)")]
        public int Doelstellingen { get; set; }


        [Display(Name = "De onderzoeksvraag is geïnspireerd door een reëel probleem uit het werkveld")]
        public int Onderzoeksvraag { get; set; }


        [Display(Name = "Het is duidelijk wat de bijdrage van de student zal zijn (buiten het verzamelen en structureren van relevante informatie, d.w.z. literatuuronderzoek)")]
        public int Bijdrage { get; set; }


        [Display(Name = "Het onderwerp is voldoende uitdagend en haalbaar")]
        public int Onderwerp { get; set; }


        [Display(Name = "Er is minstens één goede bron opgenomen in de referentielijst")]
        public int Bron { get; set; }

        [Display(Name = "Suggesties")]
        public string Suggesties { get; set; }

        [Display(Name = "Opmerkingen")]
        public string Opmerkingen { get; set; }
        
        //constructor maken met Feedback object!
        public FeedbackBekijkenModel(int id, Feedback feedback)
        {
            VoorstelId = id;
            Titel = feedback.Titel;
            Context = feedback.Context;
            Doelstellingen = feedback.Doelstellingen;
            Onderzoeksvraag = feedback.Onderzoeksvraag;
            Bijdrage = feedback.Bijdrage;
            Onderwerp = feedback.Onderwerp;
            Bron = feedback.Bron;
            Suggesties = feedback.Suggesties;
            Opmerkingen = feedback.Opmerkingen;
        }
    }

    public class DeadlineWijzigenModel
    {
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }
    }
}
    

