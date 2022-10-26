using FluentValidation;
using FluxoCaixa.Chassis.Startup.Extentions;
using FluxoCaixa.Services.CashIn.Applications.Dto.Order;
using FluxoCaixa.Services.CashIn.Applications.Interfaces.Services;
using FluxoCaixa.Services.CashIn.Applications.Services;
using FluxoCaixa.Services.CashIn.Applications.Validations.Order;
using FluxoCaixa.Services.CashIn.Controllers;
using FluxoCaixa.Services.CashIn.Infrastrcuture.Context;

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