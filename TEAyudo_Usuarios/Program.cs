using Application.Interface;
using Application.Service;
using Infrastructure.Commands;
using Infrastructure.Persistence;
using Infrastructure.Querys;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<TEAyudoContext>(options =>
{
    options.UseSqlServer("Server=localhost;Database=TEAyudo_Usuarios;Trusted_Connection=True;TrustServerCertificate=True;Persist Security Info=true");

});


builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IUsuarioQuery, UsuarioQuery>();
builder.Services.AddTransient<IUsuarioCommand, UsuarioCommand>();

builder.Services.AddCors(x => x.AddDefaultPolicy(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
