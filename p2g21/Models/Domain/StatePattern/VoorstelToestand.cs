using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace p2g21.Models.Domain.StatePattern
{
    [NotMapped]
    public abstract class VoorstelToestand
    {
       
        public virtual Voorstel Voorstel { get; set; }
        
        protected VoorstelToestand(Voorstel voorstel)
        {
            Voorstel = voorstel;
        }

        public String Naam { get; set; }

        protected virtual void AdviesGeven()
        {
            
        }

        protected virtual void AdviesVragenBPC()
        {
            
        }

        protected virtual void GoedkeurenMetOpmerkingen()
        {
            
        }

        protected virtual void VoorstelAfkeuren()
        {
            
        }

        protected virtual void VoorstelGoedkeuren()
        {
           
        }

        protected virtual void VoorstelIndienen()
        {
            
        }
    }
}
