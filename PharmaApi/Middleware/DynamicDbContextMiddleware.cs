using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace PharmaApi.Middleware
{
    public class DynamicDbContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DynamicDbContextMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            //var path = context.Request.Path.Value.ToLower();
            //if (path.StartsWith("/Login") || path.StartsWith("/swagger") || path.StartsWith("/swagger-ui") || path.Contains("/swagger/v1") || path.Contains("/"))
            //{
            //    await _next(context);
            //    return;
            //}
            
            // While logout it clears context not revoke jwt
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // maintain session
            //var token = context.Session.GetString("JwtToken");

            if (string.IsNullOrEmpty(token))
            {
                await _next(context);
                return;
            }

            var dbName = GetDatabaseNameFromJwt(token);
            var connectionString = $"Server=(localDb)\\localDB;Database={dbName};Trusted_Connection=True;TrustServerCertificate=True;";

            context.Items["DynamicDbContextConnectionString"] = connectionString;

            await _next(context);
        }

        private string GetDatabaseNameFromJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            string dbName = jwtToken.Claims.First(c => c.Type == "DbName").Value;
            return dbName;
        }

    }
}
