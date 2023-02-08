using Microsoft.EntityFrameworkCore;
using ProiectASP.Data;
using ProiectASP.Entities;
using ProiectASP.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectASP.Repositories.ComenziRepository
{
    public class ComenziRepository : GenericRepository<Comenzi>, IComenziRepository
    {
        public ComenziRepository(Context context) : base(context) { }


        public async Task<List<Comenzi>> GetAllComenzi() 
        {
            var response =  await _context.Comenzi.ToListAsync();
            var result = response.OrderByDescending(c =>
            {
                string[] dateParts = c.Data.Split('-');
                int year = int.Parse(dateParts[0]);
                int month = int.Parse(dateParts[1]);
                return new DateTime(year, month, 1);
            }).ToList();
            
            return result;
        }

        public async Task<Comenzi> GetByIdC(int id)
        {
            return await _context.Comenzi.Where(a => a.IdComanda.Equals(id)).FirstOrDefaultAsync();
        }

    }
}
