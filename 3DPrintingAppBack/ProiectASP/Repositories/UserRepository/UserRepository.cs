using Microsoft.EntityFrameworkCore;
using ProiectASP.Data;
using ProiectASP.Entities;
using ProiectASP.Entities.DTOs;
using ProiectASP.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProiectASP.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {


        public UserRepository(Context context) : base(context) { }
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdWithRoles(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public bool GetByIdUser(string email, string telefonNou)
        {
            var telefon =   _context.Users
                .Where(u => u.Email.Equals(email))
                .FirstOrDefault();
            telefon.Telefon = telefonNou;
            _context.Users.Update(telefon);
            _context.SaveChanges();
            return true;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<string> GetUserPassByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email.Equals(email)).Select(u => u.PasswordHash).FirstOrDefaultAsync();
        }

        public async Task<UserRoleDTO> GetUserRole(string email)
        {
            return await _context.Users.Where(u => u.Email.Equals(email))
                .Select(u => new UserRoleDTO { Rol = u.Rol })
                .FirstOrDefaultAsync();
        }
    }
}

