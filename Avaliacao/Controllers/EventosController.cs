
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

    public EventosController( IEventoRepository repository )
    {
        _repository = repository;
    }

    [HttpGet("listar")]
    public IActionResult Listar()
    {
            var users = _repository.List();
            return Ok(users);
    }

    [HttpGet("usuario/{id}")]
    [Authorize]
    public IActionResult UsuarioListar(int id)
    {
            var user = _repository.SearchUserId(id);
            if (user == null)
                return NotFound("não encontrado.");
            return Ok(user);

    }

    [HttpPost("cadastrar")]
    [Authorize]
    public IActionResult Cadastrar([FromBody] Evento evento)
    {
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