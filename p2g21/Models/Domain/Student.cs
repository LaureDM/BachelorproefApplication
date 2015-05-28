using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using p2g21.Models.Domain.StatePattern;
using System.ComponentModel.DataAnnotations;
using p2g21.Models.Domain.Exceptions;

namespace p2g21.Models.Domain
{
    public class Student : Gebruiker
    {
        public Student()
        {
            Voorstellen = new List<Voorstel>();
            Dossiers = new List<Dossier>();
            Deadline = Convert.ToDateTime("2014-06-13 23:59:59");
            OpTijd = true;

        }
        public Student(String Loginnaam, String Naam, String Voornaam)
            : base(Loginnaam, Naam, Voornaam)
        {
            OpTijd = true;
            Voorstellen = new List<Voorstel>();
            Dossiers = new List<Dossier>();
            Deadline = Convert.ToDateTime("2014-06-13 23:59:59");
        }

        public string Campus { get; set; }
        public Boolean OpTijd { get; set; }
        public virtual ICollection<Voorstel> Voorstellen { get; set; }
        public virtual ICollection<Dossier> Dossiers { get; set; }

        public Historiek Historiek { get; set; }
        public int GetActieveVoorstelId()
        {
           return Voorstellen.FirstOrDefault().VoorstelId;
            
        }

        public Boolean HeeftNieuwVoorstel()
        {
            Boolean booliebool = false;
            if (Voorstellen == null)
                return false;
            foreach(Voorstel v in Voorstellen)
            {
                if(v.HuidigeToestand.GetType().Equals(typeof(NieuwVoorstel)))
                    booliebool = true;
            }

            return booliebool;
        }

        public Voorstel GeefHuidigeNieuweVoorstel()
        {
                return Voorstellen.FirstOrDefault(v => v.Toestand.Equals("Nieuw voorstel"));
            
        }

        public DateTime Deadline { get; set; }

        public Boolean KanEenVoorstelIndienen()
        {
            foreach(Voorstel v in Voorstellen)
            {
                if (!v.Toestand.Equals("Nieuw voorstel"))
                    return false;
            }
            return true;
        }
        public void IndienenVoorstel(int id)
        {
            if(!KanEenVoorstelIndienen())
                throw new VoorstelIndienenExceptie("U kan maar één voorstel indienen");
            Voorstel voorstelUitRep = Voorstellen.FirstOrDefault(v => v.VoorstelId == id);
            voorstelUitRep.TijdstipIndienen = DateTime.Now;
            if (voorstelUitRep.TijdstipIndienen.CompareTo(Deadline) > 0)
            {
                OpTijd = false;
            }
            voorstelUitRep.NaarVoorstelInBehandelingToestand();
            Dossiers.Add(new Dossier(voorstelUitRep, GebruikerId));
        }

        public Boolean HeeftAlIngediendVoorstel()
        {
            Boolean boolie = false;
            foreach(Voorstel v in Voorstellen)
            {
                if (v.Toestand.Equals("Voorstel in behandeling"))
                    boolie = true;
            }
            return boolie;
        }

        public void OpslaanVoorstel(Voorstel voorstel)
        {
            Voorstel voorstelUitRep = Voorstellen.FirstOrDefault(v => v.VoorstelId == voorstel.VoorstelId);
            if (voorstelUitRep == null)
            {
                voorstel.NaarNieuwVoorstelToestand();
                voorstel.Creatiedatum = DateTime.Now;
                Voorstellen.Add(voorstel);
            }
            else
            {
                voorstelUitRep.NaarNieuwVoorstelToestand();
            }
        }

        public override void NieuwWachtwoord(string wachtwoord)
        {
            Wachtwoord = wachtwoord;
        }

        public void AddVoorstel(Voorstel voorstel)
        {
            Voorstellen.Add(voorstel);
            voorstel.Creatiedatum = DateTime.Now;
        }

        public void DeleteVoorstel(Voorstel voorstel)
        {
            if (!Voorstellen.Contains(voorstel))
                throw new ArgumentException(string.Format("{0} {1} heeft dit voorstel niet", this.Naam, this.Voornaam));
            if (!voorstel.KanVerwijderdWorden)
                throw new VoorstelVerwijderenExceptie("U kan een voorstel in deze toestand niet verwijderen");
            Voorstellen.Remove(voorstel);
        }

        internal void VoorstelOpslaan(Voorstel voorstel,Boolean aanpassen)
        {
            if (voorstel.Od1 != null)
            {
                voorstel.Onderzoeksdomeinen.Add(voorstel.Od1);
                if (voorstel.Od2 != null)
                    voorstel.Onderzoeksdomeinen.Add(voorstel.Od2);
            }

            if(voorstel.Od1!=null)
            voorstel.Od1.Voorstellen.Add(voorstel);
            if(voorstel.Od2!=null)
            voorstel.Od2.Voorstellen.Add(voorstel);
            if(!aanpassen)
                AddVoorstel(voorstel);
        }

    }
}