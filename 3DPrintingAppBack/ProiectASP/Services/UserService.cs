using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;
using ProiectASP.Entities;
using ProiectASP.Entities.Constants;
using ProiectASP.Entities.DTOs;
using ProiectASP.Repositories.AdreseRepository;
using ProiectASP.Repositories.AngajatiRepository;
using ProiectASP.Repositories.ClientiRepository;
using ProiectASP.Repositories.SessionTokenRepository;
using ProiectASP.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProiectASP.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ISessionTokenRepository _repository;
        private readonly IUserRepository _repository2;
        private readonly IClientiRepository _repositoryClient;
        private readonly IAngajatiRepository _angajatRepository;
        private readonly IAdresaRepository _adresaRepository;
        private readonly IEmployeesPasswordRepo _employeePassword;

        private const int SaltSize = 16; //128 bits
        private const int KeySize = 32; //256 bits
        private const int Iterations = 10000;



        public string CalculateHashValueWithInput(string input)
        {

            using (var algorithm = new Rfc2898DeriveBytes(input, SaltSize, Iterations, HashAlgorithmName.SHA512))
            {
                var hash = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);



                return $"{Iterations}.{salt}.{hash}";
            }
        }
        public bool IsPasswordVerified(string initialHash, string usedSalt, string input)
        {
            string reHashedPassword = "";

            // var utf8 = new Rfc2898DeriveBytes();
            byte[] saltByteArr = Convert.FromBase64String(usedSalt);
            using (var algorithm = new Rfc2898DeriveBytes(input, saltByteArr, Iterations, HashAlgorithmName.SHA512))
            {
                var hash = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);



                reHashedPassword = $"{Iterations}.{salt}.{hash}";
            }



            if (initialHash == reHashedPassword)
                return true;
            return false;
        }

        public UserService(
            UserManager<User> userManager,
            ISessionTokenRepository repository,
            IUserRepository repository2,
            IClientiRepository clientiRepository,
            IAngajatiRepository angajatiRepository,
            IAdresaRepository adresaRepository,
            IEmployeesPasswordRepo employeePassword) 
        {
            _userManager = userManager;
            _repository = repository;
            _repository2 = repository2;
            _repositoryClient = clientiRepository;
            _angajatRepository = angajatiRepository;
            _adresaRepository = adresaRepository;
            _employeePassword = employeePassword;
        }


        public async Task<bool> RegisterUserAsync(RegisterUserDTO dto)
        {
            var registerUser = new User();

            registerUser.Email = dto.Email;
            registerUser.Nume = dto.Nume;
            registerUser.Prenume = dto.Prenume;
            registerUser.UserName = dto.Nume + "_" + dto.Prenume;
            registerUser.Telefon = dto.Telefon;
            registerUser.Rol = dto.Rol;
            registerUser.PasswordHash = CalculateHashValueWithInput(dto.Password);         
        
            var result = await _userManager.CreateAsync(registerUser);
            if (result.Succeeded)
            {

                if (dto.Rol == "Client")
                {
                    await _userManager.AddToRoleAsync(registerUser, UserRoleType.Client);
                    
                }

                if (dto.Rol == "Executant")
                {
 
                    await _userManager.AddToRoleAsync(registerUser, UserRoleType.Executant);
                }

                if (dto.Rol == "Proiectant")
                {

                    await _userManager.AddToRoleAsync(registerUser, UserRoleType.Proiectant);
                }

                if (dto.Rol == "Admin")
                {
                    await _userManager.AddToRoleAsync(registerUser, UserRoleType.Admin);
                }
                return true;
            }

            return false;
        }

        public async Task<Boolean> VerifyEmployeePassword(string password)
        {
            var pass = await _employeePassword.GetPass();
            string initialSalt = pass.Split('.')[1];
            bool isVerified = IsPasswordVerified(pass, initialSalt, password);
            if (pass is null)
            {
                return false;
            }
            if (isVerified)
            {
                return true;
            }
            return false;
        }
        public async Task<string> LoginUser(LoginUserDTO dto)
        {
            User user = await _userManager.FindByEmailAsync(dto.Email);
            var userPass = await _repository2.GetUserPassByEmail(dto.Email);
            string initialSalt =  userPass.Split('.')[1];
            bool isVerified = IsPasswordVerified(userPass, initialSalt, dto.Password);
            if (userPass is null)
            {
                return null;
            }
            if (user != null)
            {
                if (isVerified)
                {
                    user = await _repository2.GetByIdWithRoles(user.Id);

                    List<string> roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

                    var newJti = Guid.NewGuid().ToString();

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var signinkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisismysecretkey"));

                    var token = GenerateJwtToken(signinkey, user, roles, tokenHandler, newJti);

                    _repository.Create(new SessionToken(newJti, user.Id, token.ValidTo));
                    await _repository.SaveAsync();
                    var clientNou = await _repositoryClient.GetByEmailClient(dto.Email);
                    if (clientNou == null) 
                    {
                        var client = new Clienti();
                        client.Nume = _userManager.Users.Where(d => d.Email == dto.Email).Select(s => s.Nume).Single();
                       // client.IdClient = _userManager.Users.Where(d => d.Email == dto.Email).Select(s => s.Id).Single();
                        client.Prenume = _userManager.Users.Where(d => d.Email == dto.Email).Select(s => s.Prenume).Single();
                        client.Telefon = _userManager.Users.Where(d => d.Email == dto.Email).Select(s => s.Telefon).Single();
                        client.Mail = dto.Email;
                        _repositoryClient.Create(client);
                       await _repositoryClient.SaveAsync();
                    }
                    
                    
                    return tokenHandler.WriteToken(token);
                }
                return null;
            }

            return "";
        }

        private SecurityToken GenerateJwtToken(SymmetricSecurityKey signinKey, User user, List<string> roles, JwtSecurityTokenHandler tokenHandler, string jti)
        {
            var subject = new ClaimsIdentity(new Claim[]{
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Nume + " " + user.Prenume),
                new Claim(ClaimTypes.Role, user.Rol),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, jti)
            });

            foreach (var role in roles)
            {
                subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "test",
                Subject = subject,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha384)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
