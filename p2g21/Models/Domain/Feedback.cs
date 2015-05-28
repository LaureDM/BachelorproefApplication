using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace p2g21.Models.Domain
{
    public class Feedback
    {

        [Range(1, 5, ErrorMessage = "verplicht")]
        [Display(Name = "De titel beschrijft specifiek wat het onderwerp van de scriptie is.")]
        public int Titel { get; set; }


        [Range(1, 5, ErrorMessage = "verplicht")]
        [Display(Name = "De context is duidelijk beschreven, vaktermen worden uitgelegd.")]
        public int Context { get; set; }


        [Range(1, 5, ErrorMessage = "verplicht")]
        [Display(Name = "De doelstellingzijn “S.M.A.R.T.” (specifiek, meetbaar, aanvaardbaar, realistisch en tijdsgebonden)")]
        public int Doelstellingen { get; set; }


        [Range(1, 5, ErrorMessage = "verplicht")]
        [Display(Name = "De onderzoeksvraag is geïnspireerd door een reëel probleem uit het werkveld")]
        public int Onderzoeksvraag { get; set; }


        [Range(1, 5, ErrorMessage = "verplicht")]
        [Display(Name = "Het is duidelijk wat de bijdrage van de student zal zijn (buiten het verzamelen en structureren van relevante informatie, d.w.z. literatuuronderzoek)")]
        public int Bijdrage { get; set; }


        [Range(1, 5, ErrorMessage = "verplicht")]
        [Display(Name = "Het onderwerp is voldoende uitdagend en haalbaar")]
        public int Onderwerp { get; set; }


        [Range(1, 5, ErrorMessage = "verplicht")]
        [Display(Name = "Er is minstens één goede bron opgenomen in de referentielijst")]
        public int Bron { get; set; }

        [Display(Name="Suggesties")]
        [DataType(DataType.MultilineText)]
        
        public string Suggesties { get; set; }
        public string Opmerkingen { get; set; }

        public Boolean Geaccepteerd { get; set; }
        public Boolean IsDefinitief { get; set; }

        public Feedback(int titel, int context, int doelstellingen, int bijdrage, int onderzoeksvraag, int onderwerp, int bron, string suggesties, string opmerkingen)
            : this(titel, context, doelstellingen, bijdrage, onderzoeksvraag, onderwerp, bron, suggesties)
        {
            Titel = titel;
            Context = context;
            Doelstellingen = doelstellingen;
            Bijdrage = bijdrage;
            Onderzoeksvraag = onderzoeksvraag;
            Onderwerp = onderwerp;
            Bron = bron;
            Suggesties = suggesties;
            Opmerkingen = opmerkingen;
        }

        public Feedback(int titel, int context, int doelstellingen, int bijdrage, int onderzoeksvraag, int onderwerp, int bron, string suggesties)
        {
            Titel = titel;
            Context = context;
            Doelstellingen = doelstellingen;
            Bijdrage = bijdrage;
            Onderzoeksvraag = onderzoeksvraag;
            Onderwerp = onderwerp;
            Bron = bron;
            Suggesties = suggesties;
            
        }

        public void Goedkeuren()
        {
            Geaccepteerd = true;
        }

        public void Afkeuren()
        {
            Geaccepteerd = false;
        }
        public Feedback()
        {
            
        }

        public int FeedbackID{ get; set; }

        public void updateFeedback(Feedback newFeedback)
        {
            this.Bijdrage = newFeedback.Bijdrage;
            this.Bron = newFeedback.Bron;
            this.Context = newFeedback.Context;
            this.Doelstellingen = newFeedback.Doelstellingen;
            this.Onderwerp = newFeedback.Onderwerp;
            this.Onderzoeksvraag = newFeedback.Onderzoeksvraag;
            this.Opmerkingen = newFeedback.Opmerkingen;
            this.Suggesties = newFeedback.Suggesties;
            this.Titel = newFeedback.Titel;
        }
    }
}
