using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using HospitalManagementSystem.API.Exceptions;
using HospitalManagementSystem.Application.AutoMapping;
using HospitalManagementSystem.Application.Configuratioon;
using HospitalManagementSystem.Application.Interfaces.ElasticSearch;
using HospitalManagementSystem.Application.Interfaces.WebAPI;
using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Application.Services.ElasticSearch;
using HospitalManagementSystem.Application.Services.WebAPI;
using HospitalManagementSystem.Infrastructure.Data;
using HospitalManagementSystem.Infrastructure.ElasticSearch;
using HospitalManagementSystem.Infrastructure.Injection;
using HospitalManagementSystem.Infrastructure.Repository.ElasticSearch;
using HospitalManagementSystem.Infrastructure.Repository.WebAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Cosmos;
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

    var settings = new ElasticsearchClientSettings(new Uri(options.Uri))
        .Authentication(new BasicAuthentication(options.Username, options.Password))
        .ServerCertificateValidationCallback((sender, cert, chain, errors) => true);

    return new ElasticsearchClient(settings);
});

// Elasticsearch DB
builder.Services.AddSingleton<ElasticDB>();

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMap).Assembly);
builder.Services.AddAutoMapper(typeof(CosmosAutoMap).Assembly);
builder.Services.AddAutoMapper(typeof(ElasticAutoMap).Assembly);

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

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSwagger();
//app.UseSwaggerUI();

// Global Exception Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

// Temporary 
using (var scope = app.Services.CreateScope())
{
    var opt = scope.ServiceProvider
                   .GetRequiredService<IOptions<CosmosDbOptions>>().Value;

    Console.WriteLine("DB = " + opt.DatabaseId);
    Console.WriteLine("Container = " + opt.ContainerId);
}

//// Create Elasticsearch Indices on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ElasticDB>();
    var result = await db.CreateAllIndicesAsync();
    if (result)
    {
        Console.WriteLine("Elasticsearch indices created successfully on startup.");
    }
    else
    {
        Console.WriteLine("Failed to create Elasticsearch indices on startup.");
    }
}
//// Initialize Cosmos DB on startup
using (var scope = app.Services.CreateScope())
{
    var client = scope.ServiceProvider.GetRequiredService<CosmosClient>();
    var opt = scope.ServiceProvider.GetRequiredService<IOptions<CosmosDbOptions>>().Value;

    await CosmosInitializer.InitializeAsync(
        client,
        opt.DatabaseId,
        opt.ContainerId,
        opt.PartitionKeyPath);
}


// HTTPS Redirection
app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();   // MUST come before authorization
app.UseAuthorization();

// Map controllers
app.MapControllers();
app.Run();
