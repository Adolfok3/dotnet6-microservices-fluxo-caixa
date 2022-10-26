using FluentValidation;
using FluxoCaixa.Chassis.Startup.Extentions;
using FluxoCaixa.Services.CashOut.Applications.Dto.Order;
using FluxoCaixa.Services.CashOut.Applications.Interfaces.Services;
using FluxoCaixa.Services.CashOut.Applications.Services;
using FluxoCaixa.Services.CashOut.Applications.Validations.Order;
using FluxoCaixa.Services.CashOut.Controllers;
using FluxoCaixa.Services.CashOut.Infrastrcuture.Context;

var builder = WebApplication.CreateBuilder(args);

// Default setup
builder.AddDefaultSetup<DataContext>();

// App dependencies
builder.Services.AddScoped<IValidator<OrderCreateDto>, OrderCreateDtoValidator>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<OrderService>();
builder.Services.AddTransient<IOrderService, OrderValidator<OrderService>>();

var app = builder.Build();

// Default setup
app.UseDefaultSetup<DataContext>(app.Environment.EnvironmentName);

// Controllers
OrderController.MapEndpoints(app);

app.Run();
