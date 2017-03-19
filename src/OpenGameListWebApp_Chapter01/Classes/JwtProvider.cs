using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using OpenGameList.Data;
using OpenGameList.Data.Users;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;

namespace OpenGameList.Classes
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtProvider
    {
        private readonly RequestDelegate _next;

        // JWT-related members
        private TimeSpan TokenExpiration;
        private SigningCredentials SigningCredentials;

        // EF and Identity members, available through DI
        private ApplicationDbContext DbContext;
        private UserManager<ApplicationUser> UserManager;
        private SignInManager<ApplicationUser> SignInManager;

        // Static Members
        private static readonly string PrivateKey = "private_key_1234567890";
        public static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(PrivateKey));
        public static readonly string Issuer = "OpenGameListWebApp";
        public static string TokenEndPoint = "/api/connect/token";

        public JwtProvider(RequestDelegate next, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _next = next;

            // Instantiate JWT-related members
            TokenExpiration = TimeSpan.FromMinutes(10);
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            // Instantiate through Dependency Injection
            DbContext = dbContext;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public Task Invoke(HttpContext httpContext)
        {

            // Check if the request path matches our TokenEndPoint
            if (!httpContext.Request.Path.Equals(TokenEndPoint, StringComparison.Ordinal))
                return _next(httpContext);

            // Check if the current request is a valid POST with the appropriate content type (application/x-www-form-urlencoded)
            if (httpContext.Request.Method.Equals("POST") && httpContext.Request.HasFormContentType)
            {
                // OK : generate  token and send it via json-formatted string
                return CreateToken(httpContext);
            }
            else
            {
                // Not OK : output a 400 - Bad request HTTP error.
                httpContext.Response.StatusCode = 400;
                return httpContext.Response.WriteAsync("Bad request.");
            }
        }

        private async Task CreateToken(HttpContext httpContext)
        {
            try
            {
                // retrieve the relevant FORM data
                string username = httpContext.Request.Form["username"];
                string password = httpContext.Request.Form["password"];

                // check if there's a user with the given username
                var user = await UserManager.FindByNameAsync(username);

                // fallback to support email address instead of username
                if(user == null && username.Contains("@")) 
                    user = await UserManager.FindByEmailAsync(username);

                var success = user != null && await UserManager.CheckPasswordAsync(user, password);

                if(success)
                {
                    DateTime now = DateTime.UtcNow;

                    // add rthe registered claims for JWT (RFC7519).   
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Iss, Issuer),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
                    };

                    // Create the JWT and write it to a string
                    var token = new JwtSecurityToken(
                        claims: claims,
                        notBefore: now,
                        expires: now.Add(TokenExpiration),
                        signingCredentials: SigningCredentials);

                    var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

                    // build the json response
                    var jwt = new
                    {
                        access_token = encodedToken,
                        expriation = (int)TokenExpiration.TotalSeconds
                    };

                    // return token
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(jwt));
                    return;
                }
            }
            catch(Exception ex)
            {
                // TODO: handle errors
                throw ex;
            }

            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsync("Invalid username or password.");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtProviderExtensions
    {
        public static IApplicationBuilder UseJwtProvider(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtProvider>();
        }
    }
}
