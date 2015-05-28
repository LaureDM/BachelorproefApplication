using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p2g21.Models.Domain
{
    public class Onderzoeksdomein
    {
        public Onderzoeksdomein()
        {
            Voorstellen = new List<Voorstel>();
        }
        public Onderzoeksdomein(String naam)
        {
            Voorstellen = new List<Voorstel>();
            Naam = naam;
        }
        public int OnderzoeksdomeinId
        {
            get;
            set;
        }
        public string Naam
        {
            get;
            set;
        }

        public virtual ICollection<Voorstel> Voorstellen { get; set; }
    }
}