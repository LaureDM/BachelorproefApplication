using p2g21.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace p2g21.Models.ViewModels
{
    public class FeedbackCreateViewModel 
    {
        public int VoorstelId { get; set; }

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

        [Display(Name = "Suggesties")]
        [DataType(DataType.MultilineText)]
        public string Suggesties { get; set; }

        [Display(Name = "Opmerkingen")]
        [DataType(DataType.MultilineText)]
        public string Opmerkingen { get; set; }
        public FeedbackCreateViewModel(int id, Feedback feedback)
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
        public FeedbackCreateViewModel() { }

    }

    public class PromotorWijzigenModel
    {
        public Student student{get;set;}
        public string promotor{get;set;}

        public SelectList lijst { get; set; }
        public int id { get; set; }
    }
}