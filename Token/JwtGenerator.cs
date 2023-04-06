
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NetKubernates.Models;
using Microsoft.IdentityModel.Tokens;

namespace NetKubernates.Token;

public class JwtGenerator : IJwtGenerator
{
    public string GenerateToken(Usuario user)
    {
        //Add data to the payload
        var claims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName!),
            new Claim("userId", user.Id)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKey"));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescription = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(token);

    }
}