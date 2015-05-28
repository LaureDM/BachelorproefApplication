using p2g21.Models.DAL.Repositories;
using p2g21.Models.Domain;
using p2g21.Models.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace p2g21.Models.DAL
{
    public class BachelorProefInitializer : DropCreateDatabaseAlways<BachelorProefContext>
    {
        protected override void Seed(BachelorProefContext context)
        {
            try
            {
                string[] onderzoeksdomeinen = { "Consumentenelektronica", "Databanken", "Industriële toepassingen van ICT", "Kunstmatige intelligentie en Machine Learning", "Software-ontwikkeling : beveiliging", "Software-ontwikkeling : Desktop-applicaties", "Software-ontwikkeling : java", "Software-ontwikkeling : Management en Organisatie", "Software-ontwikkeling : Mobiele applicaties", "Software-ontwikkeling : .Net", "Software-ontwikkeling : Webapplicaties", "Systeem- en netwerkbeheer : Besturingssystemen", "Systeem- en netwerkbeheer : beveiliging", "Systeem- en netwerkbeheer : DevOps", "Systeem- en netwerkbeheer : Management en organisatie", "Systeem- en netwerkbeheer : netwerktechnologiën", "Systeem- en netwerkbeheer : Opslagtechnologiën", "Systeem- en netwerkbeheer : Virtualisatie en cloud", "Mainframe : Applicaties", "Mainframe : Beheer", "Mainframe : Beveiliging", "Multimedia : Grafische toepassingen", "Multimedia : Grafische vormgeving en design", "Multimedia : Video en geluid", "Ondernemen in ICT", "Sociale en legale aspecten van ICT : Sociale media" };

                for (int i = 0; i < onderzoeksdomeinen.Length; i++)
                {
                    context.Onderzoeksdomeinen.Add(new Onderzoeksdomein(onderzoeksdomeinen[i]));
                }
                context.SaveChanges();

                IOnderzoeksdomeinRepository domrep = new OnderzoeksdomeinRepository(context);
                Promotor dePromotor = new Promotor("JanDeSmet", "De Smet", "Jan");
                dePromotor.Wachtwoord = "ABcd12&&";
                dePromotor.EncrypteerWachtwoord();
                dePromotor.Email = "glennvanmele@gmail.com";
                dePromotor.EersteGebruik = false;

                Voorstel glennvoorstel = new Voorstel
                {
                    VoorstelTitel = "Native apps feel in browsers",
                    VrijeTrefwoorden = "Html5, apps, browsers",
                    Context = "Applications",
                    Doelstelling = "Mogelijkheden van HTML5 ontdekken",
                    Od1 = domrep.FindOnderzoeksdomeinById(4),
                    Od2 = domrep.FindOnderzoeksdomeinById(2),
                    Onderzoeksdomeinen = new List<Onderzoeksdomein>() { domrep.FindOnderzoeksdomeinById(4), domrep.FindOnderzoeksdomeinById(2) },
                    Onderzoeksvraag = "Wat zijn de mogelijkheden van HTML5?",
                    PlanVanAanpak = "Interviews met apps bedrijven",
                    ProbleemStelling= "Browsers zijn overbodig",
                    ReferentieLijst="InDePocket"
                    };

                Promotor dePromotor2 = new Promotor("JorisBuysse", "Buysse", "Joris");
                dePromotor2.Wachtwoord = "ABcd12&&";
                dePromotor2.EncrypteerWachtwoord();
                dePromotor2.Email = "lauredemey@gmail.com";
                dePromotor2.EersteGebruik = true;

                Student deStudent = new Student("GlennVanMele", "Van Mele", "Glenn");
                deStudent.Email = "glennvanmele@gmail.com";
                deStudent.Wachtwoord = "ABcd12&&";
                deStudent.EncrypteerWachtwoord();
                deStudent.EersteGebruik = true;
                deStudent.Campus = "Aalst";

                context.Gebruikers.Add(deStudent);

                Student laure = new Student("LaureDeMey", "De Mey", "Laure");
                laure.Email = "glennvanmele@gmail.com";
                laure.Wachtwoord = "oddish1!";
                laure.EncrypteerWachtwoord();
                laure.Campus = "Gent";
                laure.EersteGebruik = true;
                context.Gebruikers.Add(laure);


                Student maxim = new Student("MaximValcke", "Valcke", "Maxim");
                maxim.Email = "valcke.maxim@gmail.com";
                maxim.Wachtwoord = "ABcd12&&";
                maxim.EncrypteerWachtwoord();
                maxim.Campus = "Gent";
                context.Gebruikers.Add(maxim);

                Voorstel maximvoorstel = new Voorstel
                {
                    VoorstelTitel = "Chocolade",
                    VrijeTrefwoorden = "lekker, zoet, saus, mousse",
                    Context="Desserten",
                    Doelstelling="Dik worden",
                    Onderzoeksvraag="Zal chocolade nog bestaan over 5 jaar?",
                    PlanVanAanpak="chocolade eten",
                    ProbleemStelling="chocolade wordt te duur",
                    ReferentieLijst="cote d'or, milka, jacques",
                    Od1 = domrep.FindOnderzoeksdomeinById(4),
                    Od2 = domrep.FindOnderzoeksdomeinById(2),
                    Onderzoeksdomeinen = new List<Onderzoeksdomein>() { domrep.FindOnderzoeksdomeinById(4), domrep.FindOnderzoeksdomeinById(2) }
                    };

                Student fabrice = new Student("FabriceBossuyt", "Bossuyt", "Fabrice");
                fabrice.Email = "fabricebossuyt@gmail.com";
                fabrice.Wachtwoord = "ABcd12&&";
                fabrice.EncrypteerWachtwoord();
                fabrice.Campus = "Gent";
                context.Gebruikers.Add(fabrice);

                Student sandro = new Student("SandroDeVos", "De Vos", "Sandro");
                sandro.Email = "sandro.devos@gmail.com";
                sandro.Wachtwoord = "ABcd12&&";
                sandro.EncrypteerWachtwoord();
                sandro.Campus = "Aalst";

                context.Gebruikers.Add(sandro);

                Student tomas = new Student("TomasVerhelst", "Verhelst", "Tomas");
                tomas.Email = "tomasverhelst@gmail.com";
                tomas.Wachtwoord = "ABcd12&&";
                tomas.EncrypteerWachtwoord();
                tomas.Campus = "Gent";

                context.Gebruikers.Add(tomas);

                Voorstel VoorstelLau = new Voorstel()
                {
                    VoorstelTitel = "Cybercrime",
                    Context = "Beveiliging",
                    ProbleemStelling = "Steeds meer bedrijven worden gehackt",
                    Doelstelling = "Software veiliger maken",
                    Od1 = domrep.FindOnderzoeksdomeinById(4),
                    Od2 = domrep.FindOnderzoeksdomeinById(2),
                    Onderzoeksdomeinen = new List<Onderzoeksdomein>() { domrep.FindOnderzoeksdomeinById(4), domrep.FindOnderzoeksdomeinById(2) },
                    Onderzoeksvraag = "Hoe kunnen we software hack-vrij maken?",
                    PlanVanAanpak = "Software proberen hacken en dan meest voorkomende loopholes bijhouden",
                    ReferentieLijst = "Verschillende bedrijven",
                    Toestand = "Nieuw voorstel",
                    VrijeTrefwoorden = "beveiliging,sql injectie, cybercrime"
                };

                

                laure.AddVoorstel(VoorstelLau);
                context.Gebruikers.Add(laure);
                context.SaveChanges();
                maxim.AddVoorstel(maximvoorstel);
                maxim.IndienenVoorstel(maximvoorstel.VoorstelId);
                BachelorProefCoordinator coordinator = new BachelorProefCoordinator("JohanBach", "Johan", "Bach");
                coordinator.Email = "lauredemey@gmail.com";
                coordinator.Wachtwoord = "ABcd12&&";
                coordinator.EncrypteerWachtwoord();
                
                context.Gebruikers.Add(coordinator);

                dePromotor.Studenten.Add(deStudent);
                dePromotor.Studenten.Add(laure);
                dePromotor.Studenten.Add(maxim);
                dePromotor2.Studenten.Add(fabrice);
                dePromotor2.Studenten.Add(sandro);
                dePromotor2.Studenten.Add(tomas);

                context.Gebruikers.Add(dePromotor);
                context.Gebruikers.Add(dePromotor2);
                deStudent.AddVoorstel(glennvoorstel);
                deStudent.IndienenVoorstel(glennvoorstel.VoorstelId);
                glennvoorstel.AdviesVraag = "Is dit onderzoeksveld te ruim?";
                glennvoorstel.Toestand = "Advies BPC";
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string s = "Fout creatie database";
                foreach (var eve in e.EntityValidationErrors)
                {
                    s += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                         eve.Entry.Entity.GetType().Name, eve.Entry.GetValidationResult());
                    foreach (var ve in eve.ValidationErrors)
                    {
                        s += String.Format("- HuidigeToestand: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(s);
            }
        }

    }
}