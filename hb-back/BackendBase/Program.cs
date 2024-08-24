using System.Text;
using System.Text.Json.Serialization;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Extensions;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.SecurityServices;
using BackendBase.Interfaces.Services;
using BackendBase.Interfaces.Services.Report;
using BackendBase.Interfaces.Utils;
using BackendBase.Middlewares;
using BackendBase.Repositories;
using BackendBase.SecurityServices;
using BackendBase.Services;
using BackendBase.Services.Report;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<UserInfo>();

// Repositories
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IActivityEventTypeRepository, ActivityEventTypeRepository>();
builder.Services.AddScoped<IEventFileRepository, EventFileRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventTypeRepository, EventTypeRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IInstituteRepository, InstituteRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ILessonTypeRepository, LessonTypeRepository>();
builder.Services.AddScoped<IWorkRepository, WorkRepository>();
builder.Services.AddScoped<IRecordRepository, RecordRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IStateUserRepository, StateUserRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReportCreateService, ReportCreateService>();
builder.Services.AddScoped<IReportExportService, ReportExportService>();
builder.Services.AddScoped<IEventTypeService, EventTypeService>();
builder.Services.AddScoped<IInstituteService, InstituteService>();
builder.Services.AddScoped<ILessonTypeService, LessonTypeService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IWorkService, WorkService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IRecordService, RecordService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IStateUserService, StateUserService>();
builder.Services.AddScoped<IFileService, FileService>();

// Security Services
builder.Services.AddScoped<IEventSecurityService, EventSecurityService>();
builder.Services.AddScoped<ILessonSecurityService, LessonSecurityService>();
builder.Services.AddScoped<ICommentSecurityService, CommentSecurityService>();

// Utils
builder.Services.AddScoped<IStorage, FileService>();

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
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

/* var ClientOriginsPolicy = "ClientOriginsPolicy"; */

/* builder.Services.AddCors(options => */
/* { */
/*     options.AddPolicy(name: ClientOriginsPolicy, */
/*                       policy => */
/*                       { */
/*                           policy.WithOrigins("http://tsu71.ru", "http://localhost"); */
/*                       }); */
/* }); */

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
    app.Seed();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<UserInfoMiddleware>();

app.Run();