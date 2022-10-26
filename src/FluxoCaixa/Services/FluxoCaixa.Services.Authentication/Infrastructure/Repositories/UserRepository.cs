using FluxoCaixa.Services.Authentication.Domain.Entities;
using FluxoCaixa.Services.Authentication.Domain.Interfaces.Repositories;
using FluxoCaixa.Services.Authentication.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Services.Authentication.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(s => s.Username.ToLower().Equals(username.ToLower()));
        }
    }
}
