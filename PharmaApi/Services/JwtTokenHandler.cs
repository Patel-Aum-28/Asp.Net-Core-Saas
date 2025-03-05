using Microsoft.IdentityModel.Tokens;
using PharmaApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PharmaApi.Services
{
    public class JwtTokenHandler
    {
        private readonly IConfiguration _configuration;
        public JwtTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(MasterTable pharmaDetail)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, pharmaDetail.PharmacyId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, pharmaDetail.Email.ToString()),
                new Claim("PharmacyName", pharmaDetail.PharmacyName.ToString()),
                new Claim("DbName", pharmaDetail.DbName.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
                signingCredentials: signature
            );

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }

        //public string ValidateToken(string token)
        //{
        //    if (token == null)
        //    {
        //        return null;
        //    }

        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    try
        //    {
        //        tokenHandler.ValidateToken(token, new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ValidateIssuerSigningKey = true,
        //            ValidIssuer = _configuration["Jwt:Issuer"],
        //            ValidAudience = _configuration["Jwt:Audience"],
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
        //            ClockSkew = TimeSpan.Zero
        //        }, out SecurityToken validatedToken);

        //        var jwtToken = (JwtSecurityToken)validatedToken;
        //        string dbName = jwtToken.Claims.First(c => c.Type == "DbName").Value;

        //        return dbName;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
    }
}
