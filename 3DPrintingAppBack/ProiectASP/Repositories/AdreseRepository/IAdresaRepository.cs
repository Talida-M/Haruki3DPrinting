using ProiectASP.Entities;
using ProiectASP.Repositories.GenericRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectASP.Repositories.AdreseRepository
{
    public interface IAdresaRepository : IGenericRepository<Adrese>
    {
        Task<List<Adrese>> GetAllAngajati();
        Task<Adrese> GetByIdA(int id);
        Task<Adrese> GetByIdWithAdresa(int id);
        Task<int> GetAdresaId(int idAng);
        int GetNumarAdrese();
    }
}
