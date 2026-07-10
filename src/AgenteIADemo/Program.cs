using AplicacaoAgenteIA;
using AplicacaoAgenteIA.Core.ConexaoIA;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddSingleton<App>();
builder.Services.AddScoped<ConexaoIAFactory>();

var host = builder.Build();

var app = host.Services.GetRequiredService<App>();

await app.ExecutarAsync();