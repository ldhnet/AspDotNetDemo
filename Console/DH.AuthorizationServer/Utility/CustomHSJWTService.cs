using Framework.Utility.JWT;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DH.AuthorizationServer.Utility
{
    public class CustomHSJWTService : ICustomJWTService
    {
        private readonly JWTTokenOptions _JWTTokenOptions;
        public CustomHSJWTService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        {
            _JWTTokenOptions = jwtTokenOptions.CurrentValue;
        }
        public string GetToken(string UerName, string password)
        {
            var claims = new[] 
            {
                new Claim(ClaimTypes.Name,UerName),
                new Claim("NickName",UerName),
                new Claim(ClaimTypes.Role,"Administrator"),
                new Claim("ABC","ABC"),
                new Claim("Student","五花肉"),
            };
            //需要加密：需要加密key
            //Nuget引入：Microsoft.IdentityModel.Tokens
            //System.IdentityModel.Tokens.Jwt
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken (
                    issuer: _JWTTokenOptions.Issuer,
                    audience: _JWTTokenOptions.Audience,
                    claims: claims,
                    expires:DateTime.Now.AddMinutes(60 * 8), //8小时过期
                    signingCredentials: creds); 

            string returnToken=new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;        
        }
    }
}
