using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace p2g21.Models.Domain.StatePattern
{
    [NotMapped]
    public class Goedgekeurd : VoorstelToestand
    {
        protected internal Goedgekeurd(Voorstel voorstel) : base(voorstel)
        {
            this.Naam = "Goedgekeurd";
        }
    }
}
