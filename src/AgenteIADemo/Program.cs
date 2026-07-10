using AplicacaoAgenteIA;
using AplicacaoAgenteIA.Core.ConexaoIA;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<App>();
builder.Services.AddSingleton<ConexaoIAFactory>();

var host = builder.Build();

var app = host.Services.GetRequiredService<App>();

await app.ExecutarAsync();