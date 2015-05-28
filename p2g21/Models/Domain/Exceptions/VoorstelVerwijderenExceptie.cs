using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p2g21.Models.Domain.Exceptions
{
    public class VoorstelVerwijderenExceptie : Exception
    {
        public VoorstelVerwijderenExceptie()
        {

        }

        public VoorstelVerwijderenExceptie(string message) : base(message)
        {

        }
    }
}