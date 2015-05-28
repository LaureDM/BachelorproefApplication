using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace p2g21.Models.Domain.StatePattern
{
    [NotMapped]
    public class NieuwVoorstel : VoorstelToestand
    {
        protected internal NieuwVoorstel(Voorstel voorstel) : base(voorstel)
        {
            Naam = "Nieuw Voorstel";
        }

        protected override void VoorstelIndienen()
        {
            //controle op rol
            Voorstel.NaarVoorstelInBehandelingToestand();
        }
    }
}
