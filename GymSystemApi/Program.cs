using GymSystemApi.Data;
using GymSystemApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Conexión a PostgreSQL con Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostSqlConnection")));

// Configuración de JWT
var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Valida que el token fue firmado con nuestra clave
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

            // Valida el emisor y la audiencia del token
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            // Valida que el token no haya expirado
            ValidateLifetime = true,

            // Elimina el margen extra de 5 minutos
            ClockSkew = TimeSpan.Zero

        };
    });


builder.Services.AddAuthorization();
// Registra el servicio de autenticación
builder.Services.AddScoped<AuthService>();
// Registra el servicio de rutinas
builder.Services.AddScoped<RutinaService>();
// Registra el servicio de ejercicios
builder.Services.AddScoped<EjercicioService>();
// Registra el servicio de progreso
builder.Services.AddScoped<ProgresoService>();
// Registra el servicio de usuarios
builder.Services.AddScoped<UsuarioService>();
// Registra el servicio de perfil
builder.Services.AddScoped<PerfilService>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// CORS para permitir peticiones desde Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}



app.UseCors("AllowAngular");


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
