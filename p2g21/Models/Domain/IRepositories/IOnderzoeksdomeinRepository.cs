using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2g21.Models.Domain.IRepositories
{
    public interface IOnderzoeksdomeinRepository
    {
        Onderzoeksdomein FindOnderzoeksdomeinById(int id);
        Onderzoeksdomein FindOnderzoeksdomeinByName(string naam);
        IQueryable<Onderzoeksdomein> FindAll();

        void SaveChanges(Onderzoeksdomein onderzoeksdomein);

        void AddOnderzoeksdomein(Onderzoeksdomein ond);

        void SaveChanges();

    }
}
