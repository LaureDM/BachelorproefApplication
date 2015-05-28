using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2g21.Models.Domain.IRepositories
{
    public interface IGebruikerRepository
    {
        Gebruiker FindGebruikerByName(string loginnaam);
        Gebruiker FindGebruikerById(int id);
        Gebruiker FindGebruikerByEmail(string email);

        IQueryable<Gebruiker> FindAll();
        IQueryable<Student> FindAllStudenten();
        IQueryable<Promotor> FindAllPromotors();

        void AddGebruiker(Gebruiker gebruiker);
        void RemoveGebruiker(Gebruiker gebruiker);

        void SaveChanges();
        Promotor FindPromotorById(int id);
        Student FindStudentById(int id);
        BachelorProefCoordinator FindBachelorProefCoordinator(int id);
        BachelorProefCoordinator FindBachelorProefCoordinator();
    }
}