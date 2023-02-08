namespace ProiectASP.Entities.DTOs
{
    public class UserDetailsDTO
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public int ClientId { get; set; }
    }
}
