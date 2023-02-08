using System.Threading.Tasks;

namespace ProiectASP.Repositories.UserRepository
{
    public interface IEmployeesPasswordRepo
    {
        Task<string> GetPass();
    }
}
