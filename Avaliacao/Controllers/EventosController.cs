
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
            var users = _repository.List();
            return Ok(users);
    }

    // [HttpGet("usuario/{id}")]
    // [Authorize]
    // public IActionResult UsuarioListar()
    // {
    //         var email = User.Identity?.Name;
    //         var eventos = _repository.SearchEveryId(_repository.SearchUserId(email));
    //         return Ok(eventos);

    // }//Como utilizar o JWT para pegar o user

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