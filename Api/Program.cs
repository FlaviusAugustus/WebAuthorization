using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Models;
using WebAppAuthorization.Options;
using WebAppAuthorization.Persistence;
using WebAppAuthorization.Persistence.Repositories.RefreshTokenRepository;
using WebAppAuthorization.Services.DateTimeProvider;
using WebAppAuthorization.Services.JwtAuthenticationService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts => 
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Description = "Enter only the jwt",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT", 
                        
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
                    
    opts.AddSecurityDefinition("Bearer", jwtSecurityScheme);

    opts.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, new List<string>() }
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<WebAuthDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("database"));
});

builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

builder.Services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

builder.Services.Configure<Jwt>(builder.Configuration.GetSection(Jwt.JwtConfig));

var jwtConfig = builder.Configuration
    .GetSection(Jwt.JwtConfig)
    .Get<Jwt>();

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero,

    ValidIssuer = jwtConfig!.Issuer,
    ValidAudience = jwtConfig.Audience,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key))
};

builder.Services.AddSingleton(tokenValidationParameters);
    
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opts =>
    {
        
        opts.RequireHttpsMetadata = false;
        opts.SaveToken = false;
        opts.TokenValidationParameters = tokenValidationParameters;
    });

builder.Services.AddIdentity<User, IdentityRole<Guid>>().AddEntityFrameworkStores<WebAuthDbContext>();


var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WebAuthDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
