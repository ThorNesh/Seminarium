using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WarsztatAPI.Tools
{
    public static class JwtService
    {
        const string secretKey = "Nst66lpA?!CoSl3m?a%s$3";

        public static string Generate(uint id, Claim[] claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var headers = new JwtHeader(credentials);

            var payload = new JwtPayload(id.ToString(), null, claims, null, DateTime.Now.AddMinutes(15));
            var securityToken = new JwtSecurityToken(headers, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public static JwtSecurityToken Verify(string jwt)
        {
            var tokenHaandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);
            tokenHaandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validetedToken);

            return (JwtSecurityToken)validetedToken;

        }
    }
}
