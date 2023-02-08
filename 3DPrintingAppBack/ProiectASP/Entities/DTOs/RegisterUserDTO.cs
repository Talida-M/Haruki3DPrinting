using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectASP.Entities.DTOs
{
    public class RegisterUserDTO
    {

        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
          public string Email { get; set; }
        public string Rol { get; set; }
        //public ICollection<UserRole> UserRoles { get; set; }
        public int salariu { get; set; } = 0;
        public string Oras { get; set; } = "default";
        public string Strada { get; set; } = "default";
        public string Password { get; set; }
    }
}
