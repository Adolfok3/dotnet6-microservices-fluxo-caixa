using FluxoCaixa.Chassis.Authentication.Utils;
using FluxoCaixa.Chassis.Authentication.Utils.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace FluxoCaixa.Chassis.Authentication.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddAuthentication(this WebApplicationBuilder builder)
        {
            var secret = builder.Configuration.GetSection("Jwt")["Secret"];
            var secretBytes = Encoding.ASCII.GetBytes(secret);
            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("user", policy => policy.RequireClaim(ClaimTypes.Role, "user"));
            });
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddSingleton<IJwtGenerator, JwtGenerator>();
        }
    }
}
