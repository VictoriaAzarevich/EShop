using EShop.Contracts;
using EShop.PaymentAPI.Messaging;
using MassTransit;
using PaymentProcessor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProcessPayment, ProcessPayment>();
var rabbitConfig = builder.Configuration.GetSection("RabbitMQ");

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PaymentConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(
            rabbitConfig["Host"],
            rabbitConfig["VirtualHost"],
            h =>
            {
                h.Username(rabbitConfig["Username"]);
                h.Password(rabbitConfig["Password"]);
            });

        cfg.ReceiveEndpoint(rabbitConfig["PaymentQueue"], e =>
        {
            e.ConfigureConsumer<PaymentConsumer>(context);
        });

        cfg.Message<IUpdatePaymentResultMessage>(m =>
        {
            m.SetEntityName(rabbitConfig["PaymentExchange"]);
        });
    });
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
