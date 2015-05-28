using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace p2g21.Models.Domain.StatePattern
{
    [NotMapped]
    public class AdviesBPC : VoorstelToestand
    {
        protected internal AdviesBPC(Voorstel voorstel) : base(voorstel)
        {
            this.Naam = "Advies BPC";
        }

        protected override void AdviesGeven()
        {
            Voorstel.NaarVoorstelInBehandelingToestand();    
        }
    }
}
