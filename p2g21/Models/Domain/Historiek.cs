using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p2g21.Models.Domain
{
    public class Historiek
    {
        public Historiek()
        {
        
        }
        
        public string VolledigeLog { get;set;}

        public void AddLog(string toestand, DateTime datum)
        {
            string output = datum.ToString() + " " + toestand;
            VolledigeLog += output;
        }

        public int HistoriekId { get; set; }
    }
}