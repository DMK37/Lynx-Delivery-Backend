using CourierAppBackend.Abstractions;
using CourierAppBackend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IInquiriesRepository, DbInquiriesRepository>();
builder.Services.AddScoped<IAddressesRepository, DbAddressesRepository>();
builder.Services.AddScoped<IUserRepository, DbUserRepository>();
builder.Services.AddScoped<IOffersRepository, DbOffersRepository>();

builder.Services.AddDbContext<CourierAppContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("MainDatabase")));

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