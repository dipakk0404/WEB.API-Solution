using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace NewWebApi.Models
{
    public class TokenManager
    {
        public static string Secret = "bxbnvndfhghgchjvkjlk[poipoiuu21145871y7816R%@^%@^^&@*&(*";

        public static string GenerateToken(string username)
        {
            var Encoded = System.Text.Encoding.UTF8.GetBytes(Secret);
            var base64 = Convert.ToBase64String(Encoded);
            byte[] key = Convert.FromBase64String(base64);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            IEnumerable<Claim> clm = new List<Claim>() {new Claim(ClaimTypes.Name,username)};

            SecurityTokenDescriptor Descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(clm),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                TokenType = "Bearer",
                Audience = "Dpk",
                Issuer = "Dpk",
                
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token=handler.CreateJwtSecurityToken(Descriptor);
            return handler.WriteToken(token);
        }
        public static ClaimsPrincipal GetPrincipal(string Token)
        {
            try
            {
                JwtSecurityTokenHandler Handler = new JwtSecurityTokenHandler();
                JwtSecurityToken SecurityToken = (JwtSecurityToken)Handler.ReadToken(Token);


                var Encoded = System.Text.Encoding.UTF8.GetBytes(Secret);
                var base64 = Convert.ToBase64String(Encoded);
                Byte[] key = Convert.FromBase64String(base64);


                IEnumerable<string> Enum = new List<string>() { SecurityAlgorithms.HmacSha256Signature };

                TokenValidationParameters Parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidAlgorithms = Enum,
                    RequireSignedTokens=true
                };

                SecurityToken secure;
                ClaimsPrincipal principal = Handler.ValidateToken(Token, Parameters, out secure);

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}