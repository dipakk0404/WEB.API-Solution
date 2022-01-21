using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WEBAPIAuthorization.Models
{
    public class TokenManager
    {
        private static string secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";

        public static string GenerateToken(string UserName)
        {
            byte[] key = Convert.FromBase64String(secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, UserName) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler Handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = Handler.CreateJwtSecurityToken(descriptor);
            string jwtToken =Handler.WriteToken(token);
            return jwtToken;

        }

        public static ClaimsPrincipal GetPrincipal(string Token)
        {
            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken securityToken = (JwtSecurityToken)handler.ReadToken(Token);

                if (securityToken == null)
                {
                    return null;
                }

                byte[] key = Convert.FromBase64String(Token);

                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience=false,
                    IssuerSigningKey=new SymmetricSecurityKey(key)
                };

                SecurityToken SecureKey;

                ClaimsPrincipal Principal=handler.ValidateToken(Token, parameters, out SecureKey);

                return Principal;
            }
            catch
            {

                return null;
            }


        }
        public static string ValidateToken(string Token)
        {
            string UserName = null;

            ClaimsPrincipal principal = GetPrincipal(Token);

            if (principal==null)
            {
                return null;
            }

            ClaimsIdentity identity = null;

            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch(NullReferenceException)
            {

                return null;

            }
            Claim Userclaim=identity.FindFirst(ClaimTypes.Name);

            UserName=Userclaim.Value;

            return UserName;

        }

    }
}