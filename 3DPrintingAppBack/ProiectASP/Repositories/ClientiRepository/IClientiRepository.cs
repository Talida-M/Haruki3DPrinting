using ProiectASP.Entities;
using ProiectASP.Entities.DTOs;
using ProiectASP.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectASP.Repositories.ClientiRepository
{
    public interface IClientiRepository : IGenericRepository<Clienti>
    {
        
        Task<Clienti> GetByIdClient(int id);
        Task<Clienti> GetByEmailClient(string email);
        Task<List<Clienti>> GetAllClienti(); 
        Task<List<string>> GetByIdWithComenzi(int id);
        Task<UserDetailsDTO> GetByIdUser(string email);
        Task<List<GetComenziDTO>> GetUserOrders(int id);



    }
}
