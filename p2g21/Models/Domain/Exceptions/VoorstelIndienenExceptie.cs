using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p2g21.Models.Domain.Exceptions
{
    public class VoorstelIndienenExceptie : Exception
    {
        public VoorstelIndienenExceptie()
        {

        }

        public VoorstelIndienenExceptie(string message) : base(message)
        {
            
        }
        
    }
}