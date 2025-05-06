using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacao.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Avaliacao.Models;
using Avaliacao.Repositories;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Avaliacao.Controllers
{
    
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IConfiguration _configuration;
        public UsuariosController(IUsuarioRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] Usuario usuario)
        {
            _repository.Cadastrar(usuario);
            _repository.Save();
            return CreatedAtAction(nameof(ListById), new { id = usuario.Id }, usuario);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            var foundUser = _repository.Login(usuario.Email, usuario.Senha);
            if (foundUser == null)
                return Unauthorized("Credenciais inválidas.");

            var token = GenerateToken(foundUser);
            return Ok(new { token });
        }

        [HttpGet("listar")]
        [Authorize]
        public IActionResult Listar()
        {
            var users = _repository.List();
            return Ok(users);
        }

        [HttpGet("listar/{id}")]
        public IActionResult ListById(int id)
        {
            var user = _repository.SearchId(id);
            if (user == null)
                return NotFound("Usuário não encontrado.");
            return Ok(user);
        }

        [ApiExplorerSettings(IgnoreApi = true)]//Esta linha é utilizada quando queremos que nosso código ignore um endpoint
        private string GenerateToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var chave = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!);
            var credenciais = new SigningCredentials(
                new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credenciais);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
            
            
    }
}