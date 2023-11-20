using CourierAppBackend.Abstractions;
using CourierAppBackend.Auth;
using CourierAppBackend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    var origin = builder.Configuration["Auth0:CLIENT_ORIGIN_URL"];
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins(origin
               ).AllowAnyMethod().AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var audience =
            builder.Configuration["Auth0:Audience"];
        var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
        options.Authority = domain;
        options.Audience = audience;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:inquiries",
        policy => { policy.Requirements.Add(new RbacRequirement("read:inquiries")); });
    options.AddPolicy("edit:profile",
        policy => { policy.Requirements.Add(new RbacRequirement("edit:profile")); });
    options.AddPolicy("get:profile",
        policy => { policy.Requirements.Add(new RbacRequirement("get:profile")); });
});
builder.Services.AddSingleton<IAuthorizationHandler, RbacHandler>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IInquiriesRepository, DbInquiriesRepository>();
builder.Services.AddScoped<IAddressesRepository, DbAddressesRepository>();
builder.Services.AddScoped<IUserInfoRepository, DbUserInfoRepository>();
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

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();