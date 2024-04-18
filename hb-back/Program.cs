using BackendBase.Data;
using BackendBase.Interfaces;
using BackendBase.Repositories;
using BackendBase.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackendBase.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ActivityEventTypeRepository>();
builder.Services.AddScoped<ActivtyRepository>();
builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<EventFileRepository>();
builder.Services.AddScoped<EventRepository>();
builder.Services.AddScoped<EventTypeRepository>();
builder.Services.AddScoped<FileRepository>();
builder.Services.AddScoped<InstituteRepository>();
builder.Services.AddScoped<JobRepository>();
builder.Services.AddScoped<LessonRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<RoleUserRepository>();
builder.Services.AddScoped<StateRepository>();
builder.Services.AddScoped<StateUserRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<WorkRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReportService, ReportService>();

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
