
using Avaliacao.Models;
using Avaliacao.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avaliacao.Controllers
{
    
    [ApiController]
    [Route("api/eventos")]
    public class EventosController : ControllerBase
    {
    private readonly IEventoRepository _repository;
    private readonly IUsuarioRepository _repository2;

    public EventosController( IEventoRepository repository, IUsuarioRepository repository2 )
    {
        _repository = repository;
        _repository2 = repository2;
    }
        
    [HttpGet("listar")]
    public IActionResult Listar()
    {
        var eventos = _repository.List(); // <- lista eventos, não usuários
        return Ok(eventos);
    }

    [HttpGet("usuario/eventos")]
    [Authorize]
    public IActionResult UsuarioListar()
    {
        var email = User.Identity?.Name;

        if (string.IsNullOrEmpty(email))
            return Unauthorized("Token inválido ou ausente.");

        var usuarioId = _repository2.SearchUserId(email); // <- CORRETO: usa repositório de usuários

        if (usuarioId == 0)
            return NotFound("Usuário não encontrado.");

        var eventos = _repository.SearchEveryId(usuarioId);
        return Ok(eventos);
    }

    [HttpPost("cadastrar")]
    [Authorize]
    public IActionResult Cadastrar([FromBody] Evento evento)
    {
        var email = User.Identity?.Name;

        if (string.IsNullOrEmpty(email))
            return Unauthorized("Token inválido ou ausente.");

        var usuarioId = _repository2.SearchUserId(email);

        if (usuarioId == 0)
            return NotFound("Usuário não encontrado.");

        // Força a associação segura ao usuário autenticado
        evento.UsuarioId = usuarioId;
        evento.usuario = null; // evita criação de novo usuário

        _repository.Cadastrar(evento);
        _repository.Save();

        return CreatedAtAction(nameof(ListById), new { id = evento.Id }, evento);
    }

    [HttpGet("listar/{id}")]
        public IActionResult ListById(int id)
        {
            var user = _repository.SearchId(id);
            if (user == null)
                return NotFound("Evento não encontrado.");
            return Ok(user);
        }
        
    }
}