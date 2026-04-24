using SistemadeAvisosEscolares.Repositories;
using SistemadeAvisosEscolares.Services;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using SistemadeAvisosEscolaresApi.Models.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AvisosEscolaresContext>(x =>
{
    x.UseMySql(cs, ServerVersion.AutoDetect(cs));
});

builder.Services.AddScoped(typeof(Repository<>), typeof(Repository<>));

builder.Services.AddScoped<AlumnosService>();
builder.Services.AddScoped<MaestrosService>();
builder.Services.AddScoped<AvisosService>();

builder.Services.AddAutoMapper(x =>
{

}, typeof(Program).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));
var app = builder.Build();

app.MapControllers();

app.Run();
