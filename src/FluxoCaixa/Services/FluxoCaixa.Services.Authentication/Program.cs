using FluentValidation;
using FluxoCaixa.Chassis.Startup.Extentions;
using FluxoCaixa.Services.Authentication.Application.Dto.Users;
using FluxoCaixa.Services.Authentication.Application.Interfaces.Services;
using FluxoCaixa.Services.Authentication.Application.Services;
using FluxoCaixa.Services.Authentication.Application.Validations.Authentication;
using FluxoCaixa.Services.Authentication.Controllers;
using FluxoCaixa.Services.Authentication.Domain.Interfaces.Repositories;
using FluxoCaixa.Services.Authentication.Infrastructure.Context;
using FluxoCaixa.Services.Authentication.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Default setup
builder.AddDefaultSetup<DataContext>();

// App dependencies
builder.Services.AddScoped<IValidator<UserAuthenticationRequestDto>, UserAuthenticationRequestDtoValidator>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<AuthenticationService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationValidator<AuthenticationService>>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

// Default setup
app.UseDefaultSetup<DataContext>(app.Environment.EnvironmentName);

// Controllers
AuthenticationController.MapEndpoints(app);

app.Run();
