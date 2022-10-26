using FluxoCaixa.Chassis.HttpLogging.Extensions;
using FluxoCaixa.Chassis.Logging.Extensions;
using FluxoCaixa.Chassis.Vault.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddVault();
builder.AddLogging();
builder.Services.AddOcelot();

var app = builder.Build();
app.UseHttpLogginMiddleware();
await app.UseOcelot();

app.Run();