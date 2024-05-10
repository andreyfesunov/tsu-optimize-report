using BackendBase.Data;
using BackendBase.Interfaces;
using BackendBase.Repositories;
using BackendBase.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackendBase.Extensions;
using BackendBase.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ActivityEventTypeRepository>();
builder.Services.AddScoped<ActivityRepository>();
builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<EventFileRepository>();
builder.Services.AddScoped<EventRepository>();
builder.Services.AddScoped<EventTypeRepository>();
builder.Services.AddScoped<FileRepository>();
builder.Services.AddScoped<InstituteRepository>();
builder.Services.AddScoped<JobRepository>();
builder.Services.AddScoped<LessonRepository>();
builder.Services.AddScoped<LessonTypeRepository>();
builder.Services.AddScoped<StateRepository>();
builder.Services.AddScoped<StateUserRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<WorkRepository>();
builder.Services.AddScoped<LessonTypeRepository>();
builder.Services.AddScoped<RecordRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReportService, ReportService>();
//builder.Services.AddScoped<IDepartmentService, DepartmentService>();
//builder.Services.AddScoped<IEventTypeService, EventTypeService>();
//builder.Services.AddScoped<IInstituteService, InstituteService>();
//builder.Services.AddScoped<ILessonTypeService, LessonTypeService>();
//builder.Services.AddScoped<IStateService, StateService>();
//builder.Services.AddScoped<IStateUserService, StateUserService>();

builder.Services.AddAutoMapper(typeof(Program));

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

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
    app.Seed();
}

app.UseHttpsRedirection();

app.UseMiddleware<UserInfoMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
