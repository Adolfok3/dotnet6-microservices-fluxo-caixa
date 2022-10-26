using FluxoCaixa.Chassis.Persistence.Common;

namespace FluxoCaixa.Services.Authentication.Domain.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
