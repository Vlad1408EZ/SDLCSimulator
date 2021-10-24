using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SDLCSimulator_BusinessLogic.Models.Configuration;

namespace SDLCSimulator_BackEnd.Extensions
{
    public static class AddBearer
    {
        public static void AddMyJwtBearer(this IServiceCollection collection, IConfiguration configuration)
        {
            var jwtConfig = new JwtConfig();
            configuration.GetSection("Jwt").Bind(jwtConfig);
            collection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidAudience = jwtConfig.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key)),
                    };
                });
        }
    }
}
