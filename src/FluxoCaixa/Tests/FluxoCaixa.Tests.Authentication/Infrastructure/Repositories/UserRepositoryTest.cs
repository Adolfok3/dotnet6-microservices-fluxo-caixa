using FluxoCaixa.Chassis.Utils.Helpers;
using FluxoCaixa.Services.Authentication.Domain.Entities;
using FluxoCaixa.Services.Authentication.Domain.Interfaces.Repositories;
using FluxoCaixa.Services.Authentication.Infrastructure.Context;
using FluxoCaixa.Services.Authentication.Infrastructure.Repositories;

namespace FluxoCaixa.Tests.Authentication.Infrastructure.Repositories
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private DataContext _context;
        private IUserRepository _repo;

        [SetUp]
        public void SetUp()
        {
            _context = new DataContext(TestHelper.GetDbContextOptions<DataContext>());
            _repo = new UserRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetUserByUsernameShouldReturnNullAsync()
        {
            var user = await _repo.GetUserByUsernameAsync("test");
            Assert.IsNull(user);
        }

        [Test]
        public async Task GetUserByUsernameShouldBeNotNullAsync()
        {
            await _context.Users.AddAsync(new User
            {
                Email = "email@test.com",
                Password = "password@test",
                Fullname = "fullname_test",
                Id = Guid.Empty,
                BirthDate = DateTime.MinValue,
                CreatedAt = DateTime.MinValue,
                Phone = "000000000000",
                Role = "role_test",
                Username = "username_test",
            });
            await _context.SaveChangesAsync();
            var user = await _repo.GetUserByUsernameAsync("username_test");
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Email, Is.EqualTo("email@test.com"));
            Assert.That(user.Password, Is.EqualTo("password@test"));
            Assert.That(user.Fullname, Is.EqualTo("fullname_test"));
            Assert.That(user.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(user.BirthDate, Is.EqualTo(DateTime.MinValue));
            Assert.That(user.CreatedAt, Is.EqualTo(DateTime.MinValue));
            Assert.That(user.Phone, Is.EqualTo("000000000000"));
            Assert.That(user.Role, Is.EqualTo("role_test"));
            Assert.That(user.Username, Is.EqualTo("username_test"));
        }
    }
}
