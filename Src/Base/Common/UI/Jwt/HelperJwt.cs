using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.UI.Jwt
{
    public class HelperJwt
    {
        public static List<Claim> GetClaim(IList<string> userRoles, string UserName, string Id)
        {

            var _Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserName),
                    new Claim(JwtRegisteredClaimNames.Sub, UserName),

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("id", Id),

                };

            foreach (var userRole in userRoles) _Claims.Add(new Claim(ClaimTypes.Role, userRole));


            return _Claims;
        }
        public static JwtSecurityToken GetToken(List<Claim> listClaims, IConfiguration _configuration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                listClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return token;
        }
    }
}
