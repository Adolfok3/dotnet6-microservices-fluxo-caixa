using FluxoCaixa.Chassis.Messaging.Interfaces;
using FluxoCaixa.Chassis.Persistence.Extensions;
using FluxoCaixa.Chassis.Startup.Extentions;
using FluxoCaixa.Services.Wallet.Application.Consumers;
using FluxoCaixa.Services.Wallet.Application.Interfaces.Services;
using FluxoCaixa.Services.Wallet.Application.Services;
using FluxoCaixa.Services.Wallet.Controllers;
using FluxoCaixa.Services.Wallet.Domain.Interfaces.Repositories;
using FluxoCaixa.Services.Wallet.Infrastrcuture.Context;
using FluxoCaixa.Services.Wallet.Infrastrcuture.Queries;
using FluxoCaixa.Services.Wallet.Infrastrcuture.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Default setup
builder.AddDefaultSetup<DataContext>();

// App dependencies
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
builder.Services.AddTransient<IConsumer, TransactionConsumer>();

builder.Services.AddTransient<IBalanceService, BalanceService>();
builder.Services.AddTransient<IBalanceRepository, BalanceRepository>();

var app = builder.Build();

// Default setup
app.UseDefaultSetup<DataContext>(app.Environment.EnvironmentName);
app.ExecuteDatabaseQueries<DataContext>(app.Environment.EnvironmentName, Functions.UpdateBalance, Triggers.UpdateBalance);

// Controllers
TransactionController.MapEndpoints(app);
BalanceController.MapEndpoints(app);

app.Run();
