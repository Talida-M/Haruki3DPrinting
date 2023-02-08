using ProiectASP.Entities;
using ProiectASP.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectASP.Repositories.UserRepository
{
   public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetByIdWithRoles(int id);
        Task<User> GetUserByEmail(string email);
        Task<string> GetUserPassByEmail(string email);
        Task<UserRoleDTO> GetUserRole(string email);
        bool GetByIdUser(string email, string telefonNou);
    }
}
