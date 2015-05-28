using p2g21.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2g21.Tests.Controllers
{
    class DummyDataContext
    {
        public IQueryable<Gebruiker> StudentenLijst { get; private set; }
        public IQueryable<Voorstel> VoorstellenLijst { get; private set; } 
        public DummyDataContext()
        {
            
            Gebruiker deStudent = new Student("GlennDeStudent", "Van Mele", "Glenn");
            deStudent.Email = "glennvanmele@gmail.com";
            deStudent.Wachtwoord = "Eva";
            deStudent.EersteGebruik = true;
            
            StudentenLijst = (new Gebruiker[] {deStudent}).ToList().AsQueryable();

            VoorstellenLijst =
                (new Voorstel[] {new Voorstel() {VoorstelId = 1, VoorstelTitel = "TestVoorstel"}}).ToList()
                    .AsQueryable();
        }


        public Voorstel FindVoorstel
        {
            get { return VoorstellenLijst.FirstOrDefault(); }
        }

    }
}
