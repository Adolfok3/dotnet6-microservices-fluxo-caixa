using FluxoCaixa.Services.Authentication.Domain.Entities;

namespace FluxoCaixa.Services.Authentication.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}
