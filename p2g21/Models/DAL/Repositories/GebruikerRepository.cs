using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using p2g21.Models.Domain.IRepositories;
using p2g21.Models.Domain;

namespace p2g21.Models.DAL.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private DbSet<Gebruiker> gebruikers;
        private BachelorProefContext context;

        public GebruikerRepository(BachelorProefContext bachelorProefContext)
        {
            context = bachelorProefContext;
            gebruikers = context.Gebruikers;
        }

        public Gebruiker FindGebruikerByName(string loginnaam)
        {
            Gebruiker gebruiker = (from g in gebruikers
                                   where g.Loginnaam.Equals(loginnaam)
                                   select g).FirstOrDefault();            
            return gebruiker;
        }

        public Promotor FindPromotorById(int id)
        {
            return gebruikers.OfType<Promotor>()
                .Include(s=>s.Studenten
                    .Select(m => m.Dossiers
                        .Select(d => d.IngediendVoorstel)))
                .FirstOrDefault(s => s.GebruikerId == id);
        }

        public Student FindStudentById(int id)
        {
            return gebruikers.OfType<Student>()
                .Include(s => s.Dossiers
                    .Select(m => m.IngediendVoorstel))
                .Include(s => s.Voorstellen
                    .Select(m => m.Onderzoeksdomeinen))
                .Include(s => s.Voorstellen
                    .Select(j => j.Feedback))
                .Include(s => s.Historiek)
                .FirstOrDefault(s => s.GebruikerId == id);
        }

        public BachelorProefCoordinator FindBachelorProefCoordinator(int id)
        {
            return gebruikers.OfType<BachelorProefCoordinator>().FirstOrDefault(s => s.GebruikerId == id);
        }

        public BachelorProefCoordinator FindBachelorProefCoordinator()
        {
            return gebruikers.OfType<BachelorProefCoordinator>().First();
        }

        public Gebruiker FindGebruikerById(int id)
        {
            return gebruikers.FirstOrDefault(m => m.GebruikerId == id);
        }

        public Gebruiker FindGebruikerByEmail(string email)
        {
            return gebruikers.FirstOrDefault(m => m.Email.Equals(email));
        }

        public IQueryable<Gebruiker> FindAll()
        {
            return gebruikers.AsQueryable();
        }

        public IQueryable<Student> FindAllStudenten()
        {
            return gebruikers.OfType<Student>().AsQueryable();
        }

        public IQueryable<Promotor> FindAllPromotors()
        {
            return gebruikers.OfType<Promotor>()
                .Include(m => m.Studenten
                    .Select(s => s.Dossiers
                        .Select(j => j.IngediendVoorstel))).AsQueryable();
        }

        public void AddGebruiker(Gebruiker gebruiker)
        {
            gebruikers.Add(gebruiker);
        }

        public void RemoveGebruiker(Gebruiker gebruiker)
        {
            gebruikers.Remove(gebruiker);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}