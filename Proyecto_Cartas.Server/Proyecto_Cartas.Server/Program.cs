using Microsoft.EntityFrameworkCore;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Repositorio.Repositorios;
using Proyecto_Cartas.Server.Client.Pages;
using Proyecto_Cartas.Server.Components;
using Proyecto_Cartas.Server.Hubs;
using Proyecto_Cartas.Servicio.ServiciosHttp;
using Proyecto_Cartas.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:7192") });

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("ConnSqlServer") ?? throw new InvalidOperationException("Connection string 'ConnSqlServer' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IConfiguracionUsuarioRepositorio, ConfiguracionUsuarioRepositorio>();
builder.Services.AddScoped<IPerfilUsuarioRepositorio, PerfilUsuarioRepositorio>();
builder.Services.AddScoped<IBilleteraRepositorio, BilleteraRepositorio>();

builder.Services.AddScoped<IAmigoRepositorio, AmigoRepositorio>();
builder.Services.AddScoped<IRankingRepositorio, RankingRepositorio>();
builder.Services.AddScoped<IPartidaRepositorio, PartidaRepositorio>();
builder.Services.AddScoped<IUsuarioPartidaRepositorio, UsuarioPartidaRepositorio>();
builder.Services.AddScoped<IInventarioRepositorio, InventarioRepositorio>();

builder.Services.AddScoped<ITurnoRepositorio, TurnoRepositorio>();
builder.Services.AddScoped<IEventoRepositorio, EventoRepositorio>();
builder.Services.AddScoped<IEstadoCartaRepositorio, EstadoCartaRepositorio>();

builder.Services.AddScoped<ICartaRepositorio, CartaRepositorio>();
builder.Services.AddScoped<ISobreRepositorio, SobreRepositorio>();
builder.Services.AddScoped<ICartaSobreRepositorio, CartaSobreRepositorio>();
builder.Services.AddScoped<ICompraSobreRepositorio, CompraSobreRepositorio>();
builder.Services.AddScoped<ICartaAperturaRepositorio, CartaAperturaRepositorio>();

builder.Services.AddScoped<SesionUsuario>();
builder.Services.AddScoped<IHttpServicio, HttpServicio>();

builder.Services.AddServerSideBlazor().AddCircuitOptions(opt => { opt.DetailedErrors = true; });

builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Proyecto_Cartas.Server.Client._Imports).Assembly);

app.MapHub<JuegoHub>("/juegoHub");
app.MapControllers();

app.Run();
