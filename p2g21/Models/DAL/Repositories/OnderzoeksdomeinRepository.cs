using p2g21.Models.Domain;
using p2g21.Models.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace p2g21.Models.DAL.Repositories
{
    public class OnderzoeksdomeinRepository : IOnderzoeksdomeinRepository
    {
        private BachelorProefContext context;
        private DbSet<Onderzoeksdomein> onderzoeksdomeinen;

        public OnderzoeksdomeinRepository(BachelorProefContext c)
        {
            context = c;
            onderzoeksdomeinen = context.Onderzoeksdomeinen;
        }
        public Onderzoeksdomein FindOnderzoeksdomeinById(int id)
        {
            return onderzoeksdomeinen.FirstOrDefault(f => f.OnderzoeksdomeinId == id);
        }

        public Onderzoeksdomein FindOnderzoeksdomeinByName(string naam)
        {
            return onderzoeksdomeinen.FirstOrDefault(f => f.Naam.Equals(naam));
        }

        public IQueryable<Onderzoeksdomein> FindAll()
        {
            return onderzoeksdomeinen;
        }

        public void AddOnderzoeksdomein(Onderzoeksdomein onderzoeksdomein)
        {
            onderzoeksdomeinen.Add(onderzoeksdomein);
        }

        public void SaveChanges(Onderzoeksdomein dom)
        {
            context.Entry(dom).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}