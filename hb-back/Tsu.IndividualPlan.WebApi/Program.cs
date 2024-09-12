using Tsu.IndividualPlan.Data;
using Tsu.IndividualPlan.Data.Extensions;
using Tsu.IndividualPlan.Domain;
using Tsu.IndividualPlan.Transfer;
using Tsu.IndividualPlan.WebApi;
using Tsu.IndividualPlan.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainServices();
builder.Services.AddTransferServices();
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddPresentationServices(builder.Configuration);

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