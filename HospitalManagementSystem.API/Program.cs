using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using HospitalManagementSystem.API.Exceptions;
using HospitalManagementSystem.Application.AutoMapping;
using HospitalManagementSystem.Application.Configuratioon;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Application.Interfaces.ElasticSearch;
using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Application.Services.ElasticSearch;
using HospitalManagementSystem.Infrastructure.Data;
using HospitalManagementSystem.Infrastructure.ElasticSearch;
using HospitalManagementSystem.Infrastructure.Repository;
using HospitalManagementSystem.Infrastructure.Repository.ElasticSearch;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// DbContext
builder.Services.AddDbContext<HospitalDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,

            ValidIssuer = "HospitalAPI",
            ValidAudience = "HospitalSwagger",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    "f9b7e2a4c8d1439a8f6b1d7c3e5a9b0f1234567890abcdef"
                )
            )
        };
    });
builder.Services.AddAuthorization();

// JWT Settings binding (for TokenService)
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt")
);

// Elasticsearch Client
                                //builder.Services.AddSingleton(_ =>
                                //{
                                //    var settings = new ElasticsearchClientSettings(
                                //        new Uri(builder.Configuration["Elasticsearch:Url"])
                                //    )
                                //    .Authentication(new BasicAuthentication(
                                //        builder.Configuration["Elasticsearch:Username"],
                                //        builder.Configuration["Elasticsearch:Password"]
                                //    ))
                                //    .ServerCertificateValidationCallback((_, _, _, _) => true);

                                //    return new ElasticsearchClient(settings);
                                //});

builder.Services.Configure<ElasticsearchSettings>(
    builder.Configuration.GetSection("Elasticsearch")
);

builder.Services.AddSingleton(sp =>
{
    var options = sp.GetRequiredService<IOptions<ElasticsearchSettings>>().Value;

    var settings = new ElasticsearchClientSettings(new Uri(options.Url))
        .Authentication(new BasicAuthentication(options.Username, options.Password))
        .ServerCertificateValidationCallback((_, _, _, _) => true);

    return new ElasticsearchClient(settings);
});

// Elasticsearch DB
builder.Services.AddSingleton<ElasticDB>();
// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMap).Assembly);

// Services
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IHospitalService, HospitalService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IElasticHospitalService, ElasticHospitalService>();

// Repositories
builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordsRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IElasticHospitalRepository, ElasticHospitalRepository>();    

// Build app
var app = builder.Build();

// Global Exception Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS Redirection
app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();   // MUST come before authorization
app.UseAuthorization();

// Map controllers
app.MapControllers();
app.Run();
