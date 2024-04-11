using BackendBase.Data;
using BackendBase.Interfaces;
using BackendBase.Repositories;
using BackendBase.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackendBase.Extensions;
using OfficeOpenXml;
using OfficeOpenXml.Style;


{
    var Articles = new[]
    {
        new {
            Id = "101", Name = "C++"
        },
        new {
            Id = "102", Name = "Python"
        },
        new {
            Id = "103", Name = "Java Script"
        },
        new {
            Id = "104", Name = "GO"
        },
        new {
            Id = "105", Name = "Java"
        },
        new {
            Id = "106", Name = "C#"
        }
    };
}


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddDbContext<DataContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
