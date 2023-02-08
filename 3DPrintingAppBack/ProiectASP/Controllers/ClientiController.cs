using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProiectASP.Entities;
using ProiectASP.Entities.DTOs;
using ProiectASP.Repositories.ClientiRepository;
using ProiectASP.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientiController : ControllerBase
    {

        private readonly IClientiRepository _repository;
        private readonly IUserRepository _userRepository;
        public ClientiController(IClientiRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllClienti()
        {
            var cl = await _repository.GetAllClienti();

            var clToReturn = new List<ClientiDTO>();

            foreach (var client in cl)
            {
                clToReturn.Add(new ClientiDTO(client));
            }

            return Ok(clToReturn);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var ang = await _repository.GetById(id);

            return Ok(new ClientiDTO(ang));
        }

        [HttpGet("GetUserById/{email}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(string email)
        {
            var ang = await _repository.GetByIdUser(email);

            return Ok(ang);
        }


        [HttpGet("GetUserOrders/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserOrders(int id)
        {
            var ang = await _repository.GetUserOrders(id);

            return Ok(ang);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClienti(int id)
        {
            var dc = await _repository.GetById(id);

            if (dc == null)
            {
                return NotFound("Client does not exist!");
            }

            _repository.Delete(dc);

            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClienti(CreateClientiDTO dto)
        {
            Clienti newC = new Clienti();
            newC.IdClient = dto.IdClient;
            newC.Nume = dto.Nume;
            newC.Prenume = dto.Prenume;
            newC.Telefon = dto.Telefon;
            newC.Mail = dto.Mail;
           // newC.Comenzi = dto.Comenzi;


            _repository.Create(newC);

            await _repository.SaveAsync();


            return Ok(new ClientiDTO(newC));
        }

        [HttpPatch("{id}")]
        //[Authorize(Roles = "Client")]
        public IActionResult Update(string id, string tel)
        {
            var result = _userRepository.GetByIdUser(id, tel);
            return Ok(result);
        }
    }
}
