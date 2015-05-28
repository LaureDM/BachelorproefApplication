using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace p2g21.Models.Domain.StatePattern
{
    [NotMapped]
    public class VoorstelInBehandeling : VoorstelToestand
    {
        protected internal VoorstelInBehandeling(Voorstel voorstel) : base(voorstel)
        {
            Naam = "Voorstel in behandeling";
        }

        protected override void VoorstelGoedkeuren()
        {
            Voorstel.NaarGoedgekeurdToestand();
        }

        protected override void AdviesVragenBPC()
        {
            Voorstel.NaarAdviesBPC();
        }

        protected override void GoedkeurenMetOpmerkingen()
        {
            Voorstel.NaarGoedgekeurdMetOpmerkingen();
        }
    }
}
