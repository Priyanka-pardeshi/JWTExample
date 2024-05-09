using JWTProject.Store.Abstractions;
using JWTProject.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JWTProject
{
    public static class DepedencyInjection
    {
        public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = configuration.GetSection("Jwt")["Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = configuration.GetSection("Jwt")["Issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt")["Secret"])),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(1)
                    };
                    //option.Events = new JwtBearerEvents
                    //{
                    //    OnChallenge = async context =>
                    //    {
                    //        // Call this to skip the default logic and avoid using the default response
                    //        context.HandleResponse();

                    //        // Write to the response in any way you wish
                    //        context.Response.StatusCode = 401;
                    //        context.Response.Headers.Append("my-custom-header", "custom-value");
                    //        await context.Response.WriteAsync("You are not authorized! (or some other custom message)");
                    //    }
                    //};
                });

        }

        public static void AddMyServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginStore, LoginStore>();
            services.AddScoped<IAccessInfoStore, AccessInfoStore>();
        }
    }
}
