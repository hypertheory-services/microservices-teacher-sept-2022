using AspNetCore.Identity.Mongo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebPresenceBFF.Adapters;
using WebPresenceBFF.Domain;

namespace WebPresenceBFF;

public static class Auth
{
    public static void AddMongoJwtAuth(this IServiceCollection services, WebApplicationBuilder builder, string mongoConnectionString)
    {
        builder.Services.AddIdentityMongoDbProvider<SiteUser>(identity =>
        {

            identity.Password.RequireDigit = false;
            identity.Password.RequireLowercase = false;
            identity.Password.RequireNonAlphanumeric = false;
            identity.Password.RequireUppercase = false;
            identity.Password.RequiredLength = 1;
            identity.Password.RequiredUniqueChars = 0;
        },
    mongo =>
    {
        mongo.ConnectionString = mongoConnectionString + "/bff?authSource=admin";

    }
).AddDefaultTokenProviders();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
            options.AddPolicy("User", policy => policy.RequireClaim("role", "user"));
        });
    }

    public static JwtSecurityToken GetToken(List<Claim> authClaims, IConfiguration config)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: config["JWT:ValidIssuer"],
            audience: config["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(30), // This is classroom stuff. Not for real. Don't do this.
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    public static Func<UserManager<SiteUser>, LoginRequest, IConfiguration, Task<IResult>> Login()
    {
        return async (UserManager<SiteUser> userManager, [FromBody] LoginRequest request, IConfiguration config) =>
        {
            var user = await userManager.FindByNameAsync(request.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, request.Password))
            {
                var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

                var token = Auth.GetToken(authClaims, config);

                return Results.Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });

            }
            else
            {
                return Results.BadRequest();
            }
        };
    }

    public static Func<UserManager<SiteUser>, DaprAdapter, RegistrationRequest, Task<IResult>> Register()
    {
        return async (UserManager<SiteUser> userManager, DaprAdapter adapter, [FromBody] RegistrationRequest request) =>
        {
            var user = await userManager.FindByNameAsync(request.Email);
            if (user != null)
            {
                return Results.BadRequest("User already exists");
            }

            var newUser = new SiteUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var result = await userManager.CreateAsync(newUser, request.Password);
            await adapter.UserOnboardedAsync(newUser);
            return Results.Ok();

        };
    }
}
