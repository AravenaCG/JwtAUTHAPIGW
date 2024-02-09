using JwtAuthAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthAPI.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IConfiguration _config;
        UsuariosOesatContext _context;


        public UserLoginService(UsuariosOesatContext context, IConfiguration configuration) { _context = context; _config = configuration; }
        public async Task<Usuario> Authenticate(string email, string pass)
        {
            Usuario usuarioExiste = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && x.Password == pass);
            return usuarioExiste;

        }

        public string GenerateToken(Usuario usuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.User),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires:DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
    }

        public async Task<Usuario> Save(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUserLoginService
    {
        Task<Usuario> Save(Usuario usuario);
        
        Task<Usuario> Authenticate(string email, string pass);

        string GenerateToken(Usuario usuario);
    }
}
