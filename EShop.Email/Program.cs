using EShop.Email.DbContexts;
using EShop.Email.Messaging;
using EShop.Email.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IEmailRepository, EmailRepository>();

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

        cfg.ReceiveEndpoint(rabbitConfig["EmailQueue"], e =>
        {
            e.ConfigureConsumer<PaymentConsumer>(context);
            e.Bind(rabbitConfig["PaymentExchange"]);
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
