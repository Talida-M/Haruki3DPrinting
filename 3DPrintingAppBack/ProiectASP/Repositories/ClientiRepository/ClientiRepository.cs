using Microsoft.EntityFrameworkCore;
using ProiectASP.Data;
using ProiectASP.Entities;
using ProiectASP.Entities.DTO;
using ProiectASP.Entities.DTOs;
using ProiectASP.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectASP.Repositories.ClientiRepository
{
    public class ClientiRepository : GenericRepository<Clienti>, IClientiRepository
    {
        public ClientiRepository(Context context) : base(context) { }


        public async Task<List<Clienti>> GetAllClienti()
        {
            return await _context.Clienti.ToListAsync();
        }

        public async Task<UserDetailsDTO> GetByIdUser(string email)
        {
            return await _context.Users.Where(a => a.Email.Equals(email)).Select(u => new UserDetailsDTO
            {
                Id = u.Id,
                Nume = u.Nume,
                Prenume = u.Prenume,
                Telefon = u.Telefon,
                Email = u.Email,
                ClientId = _context.Clienti.
                          Where(a => a.Mail.Equals(email))
                          .Select(a => a.IdClient).Single()
            }).FirstOrDefaultAsync();
        }

        public async Task<Clienti> GetByIdClient(int id)
        {
            return await _context.Clienti.Where(a => a.IdClient.Equals(id)).FirstOrDefaultAsync();
        }
        public async Task<Clienti> GetByEmailClient(string email)
        {
            return await _context.Clienti.Where(a => a.Mail.Equals(email)).FirstOrDefaultAsync();
        }



        public async Task<List<string>> GetByIdWithComenzi(int id)
        {
            return await _context.Clienti.Where(a => a.IdClient.Equals(id)).Join(_context.Comenzi,
                                                                                        comanda => comanda.IdClient,
                                                                                        cl => cl.ClientId,
                                                                                        (Clienti, Comenzi) => new
                                                                                        {
                                                                                            IdClient = Clienti.IdClient,
                                                                                            Data = Comenzi.Data
                                                                                            
                                                                                        }
                                                                                        )
                .Select(a => a.Data)
                .ToListAsync();


        }

        public async Task<List<GetComenziDTO>> GetUserOrders (int id)
        {
            var response =   _context.Comenzi.
                Where(c => c.ClientId.Equals(id)).Select(c => new GetComenziDTO
                {
                    IdComanda = c.IdComanda,
                    IdClient = c.ClientId,
                    Data = c.Data,
                    Valoare = c.Valoare,
                    StatusTotal = c.StatusTotal

                }).ToList();
            var result = response.OrderByDescending(c =>
            {
                string[] dateParts = c.Data.Split('-');
                int year = int.Parse(dateParts[0]);
                int month = int.Parse(dateParts[1]);
                return new DateTime(year, month, 1);
            }).ToList();
            return result;
        }



    }
}
