using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

public static class AuthenticationExtension
{
    public static IServiceCollection AddAuthenticationSetup(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSetting = configuration.GetSection(nameof(JwtOptions));
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:SecurityKey").Value!));

        services.Configure<JwtOptions>(options =>
        {

            options.Issuer = jwtSetting[nameof(JwtOptions.Issuer)]!;
            options.Audience = jwtSetting[nameof(JwtOptions.Audience)]!;
            options.AccessTokenExpiration = int.Parse(jwtSetting[nameof(JwtOptions.AccessTokenExpiration)] ?? "0");
            options.RefreshTokenExpiration = int.Parse(jwtSetting[nameof(JwtOptions.RefreshTokenExpiration)] ?? "0");
            options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 6;
        });

        var tokenValidation = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration.GetSection("JwtOptions:Issuer").Value,

            ValidateAudience = true,
            ValidAudience = configuration.GetSection("JwtOptions:Audience").Value,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,

            RequireExpirationTime = true,
            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero
        };

        services.AddDefaultIdentity<User>().
        AddRoles<IdentityRole>().
        AddEntityFrameworkStores<IdentityDataContext>().
        AddDefaultTokenProviders();

        return services;
    }

}