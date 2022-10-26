using AutoMapper;
using FluxoCaixa.Chassis.Authentication.Utils;
using FluxoCaixa.Chassis.Utils.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text;

namespace FluxoCaixa.Chassis.Utils.Helpers
{
    public class TestHelper
    {
        public static DbContextOptions<T> GetDbContextOptions<T>() where T : DbContext
        {
            return new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(databaseName: "FluxoCaixa")
                .Options;
        }

        public static IMapper GetMapper(params Type[] types)
        {
            var confg = new MapperConfiguration(opt =>
            {
                opt.AddMaps(Assembly.GetEntryAssembly());
                types.ToList().ForEach(opt.AddProfile);
            });

            return confg.CreateMapper();
        }

        public static HttpClient CreateClient<T>(params TestService[] services) where T : class
        {
            var app = new WebApplicationFactory<T>().WithWebHostBuilder(host =>
            {
                host.UseConfiguration(GetJwtConfiguration());
                host.ConfigureTestServices(c =>
                {
                    foreach (var service in services)
                    {
                        c.AddSingleton(service.Type, service.Implementation);
                    }
                });
                host.UseEnvironment("Testing");
            });
            return app.CreateClient();
        }

        public static IConfiguration GetJwtConfiguration()
        {
            var jsonString = @"{
                ""Jwt"": {
                  ""Secret"": ""3d941af7-697d-4f81-abd4-5aba2c083ed1"",
                  ""ExpirationInMinutes"": 3,
                  ""RefreshTokenExpirationInMinutes"": 3
                }
            }";
            return new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(jsonString))).Build();
        }

        public static string GenerateFakeAccessToken()
        {
            var jwt = new JwtGenerator(GetJwtConfiguration());
            var (token, _) = jwt.GenerateToken(Guid.NewGuid(), "user test", "user");
            return token;
        }
    }
}
