using NetKuber.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
namespace NetKuber.Token;

public class JwtGenerador : IJwtGenerador
{
    public CrearToken(Usuario usuario)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName!),
            new Claim("userId", usuario.Id),
            new Claim("email", usuario.Email!)
        };

        //generar llave
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));

        //firmar credenciales
        var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //crear token
        var tokenDescripcion = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credenciales
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescripcion);

        return tokenHandler.WriteToken(token);
    }
}


