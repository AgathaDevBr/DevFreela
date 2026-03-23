using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// ✅ Substitua AddScoped pelo AddDbContext com a connection string
builder.Services.AddDbContext<DevFreelaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevFreelaAPIContext")));

// ✅ Mova o AddSwaggerGen para ANTES do builder.Build()
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela.API", Version = "v1" }));

var app = builder.Build();

builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo {  Title = "DevFreela.API", Version = "v1"}));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
