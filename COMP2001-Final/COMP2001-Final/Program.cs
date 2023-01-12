using COMP2001_Final.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;

var server = configuration["DbServer"] ?? "socem1.uopnet.plymouth.ac.uk";
var port = configuration["DbPort"] ?? "1433";
var user = configuration["DbUser"] = "EAldosari";
var pwd = configuration["DbPwd"] = "VziC799*";
var database = configuration["DB"] = "COMP2001_EAldosari";

builder.Services.AddDbContext<COMP2001_EAldosariContext>(options =>
options.UseSqlServer($"Server={server}, {port};Initial Catalog={database};User ID={user};Password={pwd}; Encrypt=false; TrustServerCertificate=True;"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
