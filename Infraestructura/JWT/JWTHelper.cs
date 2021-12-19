using Dominio.Entidades;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RetoProgramacion.Helper
{
    public class JWTHelper
    {
        private readonly byte[] Secreta;
        public JWTHelper(string llavesecreta)
        {
            this.Secreta = Encoding.ASCII.GetBytes(@llavesecreta);
        }
        public string GenerarToken(Token token)
        {
            if (token.Usuario.Equals("pruebaNexosSoftware") && token.Contrasenia.Equals("1234567890"))
            {
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, token.Usuario + token.Contrasenia));
                var tokendescripcion = new SecurityTokenDescriptor()
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(this.Secreta), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenhandler = new JwtSecurityTokenHandler();
                var creadotoken = tokenhandler.CreateToken(tokendescripcion);
                return tokenhandler.WriteToken(creadotoken);
            }
            else
            {
                return "not found";
            }
        }
    }
}
