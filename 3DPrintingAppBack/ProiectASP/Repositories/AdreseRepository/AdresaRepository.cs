using Microsoft.EntityFrameworkCore;
using ProiectASP.Data;
using ProiectASP.Entities;
using ProiectASP.Repositories.GenericRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectASP.Repositories.AdreseRepository
{
    public class AdresaRepository : GenericRepository<Adrese>, IAdresaRepository
    {
        public AdresaRepository(Context context) : base(context) { }


        public async Task<List<Adrese>> GetAllAngajati()
        {
            return await _context.Adrese.ToListAsync();
        }

        public int GetNumarAdrese()
        {
            return  _context.Adrese.Count();
        }
        public async Task<Adrese> GetByIdA(int id)
        {
            return await _context.Adrese.Where(a => a.IdAngajat.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<int> GetAdresaId (int idAng)
        {
            return await _context.Adrese.Where(a => a.IdAngajat.Equals(idAng)).Select(a => a.IdAdresa).FirstOrDefaultAsync();
        }

        public async Task<Adrese> GetByIdWithAdresa(int id)
        {
            return await _context.Adrese.Include(a => a.Angajati).Where(a => a.IdAdresa == id).FirstOrDefaultAsync();
        }
    }
}
