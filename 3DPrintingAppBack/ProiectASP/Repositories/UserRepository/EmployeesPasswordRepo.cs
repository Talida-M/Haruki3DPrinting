using Microsoft.EntityFrameworkCore;
using ProiectASP.Data;
using ProiectASP.Entities;
using ProiectASP.Repositories.GenericRepository;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectASP.Repositories.UserRepository
{
    public class EmployeesPasswordRepo : GenericRepository<EmployeesPassword>, IEmployeesPasswordRepo
    {
        public EmployeesPasswordRepo (Context context) : base(context) { }

        public async Task<string> GetPass()
        {
            return await _context.EmployeesPassword.Select(u => u.Password).FirstOrDefaultAsync();
        }
    }
}
