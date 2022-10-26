namespace FluxoCaixa.Services.Authentication.Application.Dto.Users
{
    public class UserDto : Chassis.Utils.Common.Dto
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }
    }
}
