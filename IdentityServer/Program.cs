using IdentityServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryApiResources(Config.ApiResources) 
    .AddInMemoryClients(Config.Clients) 
    .AddInMemoryIdentityResources(Config.IdentityResources) 
    .AddTestUsers(Config.TestUsers)  
    .AddDeveloperSigningCredential(); 

var app = builder.Build();

app.UseIdentityServer();

app.MapGet("/", () => "Hello World!");

app.Run();
