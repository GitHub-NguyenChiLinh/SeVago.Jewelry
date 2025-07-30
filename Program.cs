using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Repositories.Implementation;
using Services.Interfaces;
using Services.Implementations;
using DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DefaultConnection
builder.Services.AddDbContext<BaseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

// Register Dependency Injection
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMappers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

