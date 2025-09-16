using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Proyecto_Cartas.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<SesionUsuario>(); 

await builder.Build().RunAsync();
