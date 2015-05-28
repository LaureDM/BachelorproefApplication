using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using p2g21.Models.Domain;
using p2g21.Models.Domain.StatePattern;

namespace p2g21.Models.Domain
{
    public class Voorstel
    {
        public Voorstel()
        {          
           Onderzoeksdomeinen = new List<Onderzoeksdomein>();
           if(Historiek==null)
                Historiek = new Historiek();
           NaarNieuwVoorstelToestand();
        }

        public int VoorstelId { get; set; }
        public Historiek Historiek { get; set; }
        public string VoorstelTitel { get; set; }

        public ICollection<Onderzoeksdomein> Onderzoeksdomeinen { get; set; }


        public Onderzoeksdomein Od1 { get; set; }
 
        public Onderzoeksdomein Od2 { get; set; }

        public string VrijeTrefwoorden { get; set; }


        public string ProbleemStelling { get; set; }

        public string Context { get; set; }

        public string Doelstelling { get; set; }


        public string Onderzoeksvraag { get; set; }


        public string PlanVanAanpak { get; set; }


        public string ReferentieLijst { get; set; }

        public DateTime Creatiedatum { get; set; }


        public VoorstelToestand HuidigeToestand { get; set; }

        public Boolean KanVerwijderdWorden { get; set; }

        public virtual Feedback Feedback { get; set; }
        [Display(Name="Vraag voor BPC")]
        public string AdviesVraag { get; set; }
        public string Advies { get; set; }
        protected internal void NaarNieuwVoorstelToestand()
        {
            this.Toestand = "Nieuw voorstel";
            KanVerwijderdWorden = true;
            Historiek.AddLog(Toestand, DateTime.Now);
        }

        protected internal void NaarGoedgekeurdToestand()
        {
            this.Toestand = "Goedgekeurd";
            KanVerwijderdWorden = false;
            Historiek.AddLog(Toestand, DateTime.Now);
        }

        protected internal void NaarVoorstelInBehandelingToestand()
        {
            this.Toestand = "Voorstel in behandeling";
            KanVerwijderdWorden = false;
            Historiek.AddLog(Toestand, DateTime.Now);
        }

        protected internal void NaarGoedgekeurdMetOpmerkingen()
        {
            this.Toestand = "Goedgekeurd met opmerkingen";
            KanVerwijderdWorden = false;
            Historiek.AddLog(Toestand, DateTime.Now);
        }

        protected internal void NaarAdviesBPC()
        {
            this.Toestand = "Advies BPC";
            KanVerwijderdWorden = false;
            Historiek.AddLog(Toestand, DateTime.Now);
        }

        public Voorstel(string titel, List<Onderzoeksdomein> od, string vrijeT, string ps, string context, string doelst, string odv, string pva, string rlijst, string toestand)
        {
            if (Historiek == null)
                Historiek = new Historiek();
         
            switch (toestand)
            {
                case "Nieuw voorstel": NaarNieuwVoorstelToestand();
                    break;
                case "Goedgekeurd": NaarGoedgekeurdToestand();
                    break;
                case "Voorstel in behandeling" : NaarVoorstelInBehandelingToestand();
                    break;
                case "Goedgekeurd met opmerkingen": NaarGoedgekeurdMetOpmerkingen();
                    break;
                case "Advies BPC": NaarAdviesBPC();
                    break;
            }   

            VoorstelTitel = titel;
            Onderzoeksdomeinen = od;
            VrijeTrefwoorden = vrijeT;
            ProbleemStelling = ps;
            Context = context;
            Doelstelling = doelst;
            Onderzoeksvraag = odv;
            PlanVanAanpak = pva;
            ReferentieLijst = rlijst;
        }
    
        public Voorstel(string vnc, string ncp, string emailcp, string organisatiecp, string titel,List<Onderzoeksdomein> od, string vrijeT, string ps, string context, string doelst, string odv, string pva, string rlijst, string toestand)
        :this(titel, od, vrijeT, ps, context, doelst, odv, pva, rlijst, toestand)
        {
            VoornaamCP = vnc;
            NaamCP = ncp;
            EmailCP = emailcp;
            OrganisatieCp = organisatiecp;
            
        }

        public string VoornaamCP { get; set; }

        public string NaamCP { get; set; }

        public string EmailCP { get; set; }

        public string OrganisatieCp { get; set; }

        private string toestand;
        public string Toestand {
            get
            {
                return toestand;
            }
            set
            {

                switch (value)
                {
                    case "Nieuw voorstel": HuidigeToestand = new NieuwVoorstel(this);
                        break;
                    case "Goedgekeurd": HuidigeToestand = new Goedgekeurd(this);
                        break;
                    case "Voorstel in behandeling": HuidigeToestand = new VoorstelInBehandeling(this);
                        break;
                    case "Goedgekeurd met opmerkingen": HuidigeToestand = new GoedgekeurdMetOpmerkingen(this);
                        break;
                    case "Advies BPC": HuidigeToestand = new AdviesBPC(this);
                        break;
                }
                toestand = value;
            }
        } // in setter automatisch object aanmaken

        public void VoegFeedbackToe(Feedback feedback)
        {
            Feedback = feedback;
        }

        public Boolean AlleVerplichteVeldenIngevuld()
        {
            if (VoorstelTitel == null || Od1 == null || ProbleemStelling == null || Onderzoeksvraag == null || PlanVanAanpak == null || ReferentieLijst == null)
                return false;
            else if (VoorstelTitel == "" || ProbleemStelling == "" || Onderzoeksvraag == "" || PlanVanAanpak == "" || ReferentieLijst == "")
                return false;
            else
                return true;
        }
        public DateTime TijdstipIndienen { get; set; }
        public Boolean Goedgekeurd { get; set; }

        public void PasVoorstelAan(String voornaamCP, String naamCp, String emailCP, string organisatie, string titel, List<Onderzoeksdomein> domeinen, String vrijeTrefwoorden, String probleem, string context, string doelstelling, string onderzoeksvraag, string plan, string referenties, Onderzoeksdomein o1, Onderzoeksdomein o2)
        {
            VoornaamCP = voornaamCP;
            NaamCP = naamCp;
            EmailCP = emailCP;
            OrganisatieCp = organisatie;
            VoorstelTitel = titel;
            Onderzoeksdomeinen = domeinen;
            VrijeTrefwoorden = vrijeTrefwoorden;
            ProbleemStelling = probleem;
            Context = context;
            Doelstelling = doelstelling;
            Onderzoeksvraag = onderzoeksvraag;
            PlanVanAanpak = plan;
            ReferentieLijst = referenties;
            Od1 = o1;
            Od2 = o2;
        }

        public void GeefAdvies(String antwoord)
        {
            this.Advies = antwoord;
            this.NaarVoorstelInBehandelingToestand();
        }

        public void VraagAdvies(String vraag)
        {
            this.AdviesVraag = vraag;
            this.NaarAdviesBPC();
        }
        
        public void Goedkeuren()
        {
            if (Feedback.Opmerkingen == null)
            {
                NaarGoedgekeurdToestand();
                Feedback.IsDefinitief = true;
            }
            else
            {
                NaarGoedgekeurdMetOpmerkingen();
                Feedback.IsDefinitief = true;
            }
        }
    }
}
