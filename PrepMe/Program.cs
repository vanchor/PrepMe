using Microsoft.EntityFrameworkCore;
using PrepMe.DAL;
using PrepMe.DAL.Implementations;
using PrepMe.DAL.Interfaces;
using PrepMe.Services.Implementations;
using PrepMe.Services.Intefaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PrepMeDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("sqlServerConnection")));

builder.Services.AddScoped<IApiParser, SpoonacularApi>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
