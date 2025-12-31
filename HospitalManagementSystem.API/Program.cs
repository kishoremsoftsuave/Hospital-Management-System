using HospitalManagementSystem.Application.Auth;
using HospitalManagementSystem.Application.AutoMapping;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Infrastructure.Data;
using HospitalManagementSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// ? Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<HospitalDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "HospitalAPI",
            ValidAudience = "HospitalSwagger",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("THIS_IS_MY_SECRET_KEY")
            )
        };
    });
builder.Services.AddAuthorization();
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt")
);

builder.Services.AddScoped<TokenService>();

// ? AutoMapper (CORRECT)
builder.Services.AddAutoMapper(typeof(AutoMap).Assembly);
builder.Services.AddScoped<TokenService>();


// DI
builder.Services.AddScoped<IHospitalService, HospitalService>();
builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
