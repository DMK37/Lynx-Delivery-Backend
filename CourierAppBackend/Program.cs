using CourierAppBackend;
using CourierAppBackend.Abstractions;
using CourierAppBackend.Auth;
using CourierAppBackend.Data;
using CourierAppBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
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
    options.AddPolicy("read:all-inquiries",
        policy => { policy.Requirements.Add(new RbacRequirement("read:all-inquiries")); });
    options.AddPolicy("read:all-offers",
        policy => { policy.Requirements.Add(new RbacRequirement("read:all-offers")); });
    options.AddPolicy("read:all-pending-offers",
        policy => { policy.Requirements.Add(new RbacRequirement("read:all-pending-offers")); });
    options.AddPolicy("edit:profile",
        policy => { policy.Requirements.Add(new RbacRequirement("edit:profile")); });
    options.AddPolicy("get:profile",
        policy => { policy.Requirements.Add(new RbacRequirement("get:profile")); });
    options.AddPolicy("get:last-inquiries",
        policy => { policy.Requirements.Add(new RbacRequirement("get:last-inquiries")); });
});
builder.Services.AddSingleton<IAuthorizationHandler, RbacHandler>();

builder.Services.AddScoped<ApiKeyAuthFilter>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("private", new OpenApiInfo { Title = "CourierAppBackend", Version = "v1" });
    c.SwaggerDoc("public", new OpenApiInfo { Title = "Public API", Version = "v1" });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Api key to access the Public API",
        Type = SecuritySchemeType.ApiKey,
        Name = "XApiKey",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });
    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
    {
        {scheme, new List<string>() }
    };
    c.AddSecurityRequirement(requirement);
}
);

builder.Services.AddScoped<IInquiriesRepository, DbInquiriesRepository>();
builder.Services.AddScoped<IAddressesRepository, DbAddressesRepository>();
builder.Services.AddScoped<IUserInfoRepository, DbUserInfoRepository>();
builder.Services.AddScoped<IOffersRepository, DbOffersRepository>();
builder.Services.AddScoped<IOrdersRepository, DbOrdersRepository>();

builder.Services.AddScoped<IExternalApi, LecturerApi>();
builder.Services.AddScoped<IExternalApi, OurApi>();
builder.Services.AddScoped<IExternalApi, FakeApi>();

builder.Services.AddDbContext<CourierAppContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("MainDatabase")));

builder.Services.AddSendGrid(options =>
            options.ApiKey = builder.Configuration["SENDGRID_API_KEY"]
        );
builder.Services.AddScoped<IMessageSender, EmailSender>();


builder.Services.Configure<ExternalApisOptions>(builder.Configuration.GetSection("ExternalApis"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/private/swagger.json", "private");
    c.SwaggerEndpoint("/swagger/public/swagger.json", "public");
});

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();