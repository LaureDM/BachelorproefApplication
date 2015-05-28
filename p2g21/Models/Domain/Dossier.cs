using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p2g21.Models.Domain
{
    public class Dossier
    {
        public Dossier()
        {

        }

        public Dossier(Voorstel voorstel, int id)
        {
            IngediendVoorstel = voorstel;
            StudentId = id;
        }
        public Voorstel IngediendVoorstel { get; set; }
        public Boolean Geaccepteerd { get; set; }
        public int StudentId { get; set; }
        public int DossierId { get; set; }
        
        public void Goedkeuren()
        {
            Geaccepteerd = true;
            IngediendVoorstel.Goedgekeurd = true;
        }

        public void Afkeuren()
        {
            Geaccepteerd = false;
            IngediendVoorstel.Goedgekeurd = false;
            IngediendVoorstel.NaarNieuwVoorstelToestand();
        }
    }
}