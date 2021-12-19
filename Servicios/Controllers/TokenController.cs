using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RetoProgramacion.Helper;
using System.Threading.Tasks;

namespace Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _conf;
        public TokenController(IConfiguration conf)
        {
            _conf = conf;
        }

        [HttpPost]
        public async Task<ActionResult<object>> Token(Token _token)
        {
            var secreta = _conf.GetValue<string>("Secret");
            var jwthelper = new JWTHelper(secreta);
            var token = jwthelper.GenerarToken(_token);
            return Ok(new
            {
                Ok = true,
                token
            });
        }
    }
}
