using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using JwtAuthAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthAPI.Services;

namespace JwtAuthAPI.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUserLoginService _userLoginService;

        public UsuarioController(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        [HttpOptions("/usuario/login")]
        public IActionResult HandleOptions()
        {
            // Configurar encabezados CORS
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "POST");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");

            // Devolver respuesta 200 OK
            return Ok();
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUser)
        {
            // Validar si el modelo recibido es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Obtener el usuario desde el servicio
            Usuario usuario = await _userLoginService.Authenticate(loginUser.Email, loginUser.Password);

            // Verificar si el usuario existe
            if (usuario == null)
            {
                return Unauthorized("Credenciales incorrectas");
            }
            //crear TOKEN
            var token =  _userLoginService.GenerateToken(usuario);
            // Usuario encontrado, puedes devolverlo o hacer cualquier otra acción
            return Ok(new { token = token });
        }
    }


    /*{
"email": "cristian.g.aravena@gmail.com",
  "password": "caravena222"
}*/
}


