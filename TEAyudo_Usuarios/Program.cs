using Application.Interface;
using Application.Service;
using Infrastructure.Commands;
using Infrastructure.Persistence;
using Infrastructure.Querys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingrese: 'Bearer eyJhbGciOiJodHRwOi8vd3d3...'",
        Name = "Authorization",
        BearerFormat = "JWT",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddDbContext<TEAyudoContext>(options =>
{
    options.UseSqlServer("Server=localhost;Database=TEAyudo_Usuarios;Trusted_Connection=True;TrustServerCertificate=True;Persist Security Info=true");

});
IdentityModelEventSource.ShowPII = true;
var key = "83kZz7QOdv9Sj3SqT1gS0sjTPqmGDqo8XVXzNDLL";
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        RequireExpirationTime = true,
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
});

builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IUsuarioQuery, UsuarioQuery>();
builder.Services.AddTransient<IUsuarioCommand, UsuarioCommand>();

builder.Services.AddCors(x => x.AddDefaultPolicy(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));
builder.Services.AddTransient<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
