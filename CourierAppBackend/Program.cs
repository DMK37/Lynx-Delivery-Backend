using CourierAppBackend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// online db cs "Server=courierdb.postgres.database.azure.com;Database=courierdb;Port=5432;User Id=courier;Password=fucxo8-moxwev-suQduw;Ssl Mode=VerifyFull;"
// local cs "Host=localhost;Database=testDb;Username=postgres;Password=password123"
// later connection string won't be hardcoded
builder.Services.AddDbContext<CourierAppContext>(
    options => options.UseNpgsql("Server=courierdb.postgres.database.azure.com;Database=courierdb;Port=5432;User Id=courier;Password=fucxo8-moxwev-suQduw;Ssl Mode=VerifyFull;"));

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