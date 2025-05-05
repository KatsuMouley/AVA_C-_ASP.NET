using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public EventosController(IEventoRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("listar")]
    public IActionResult Listar()
    {
            var users = _repository.List();
            return Ok(users);
    }

    [HttpPost("usuario")]
    [Authorize]
    public IActionResult Usuario()
    {
        var eventos = _repository.SearchId(id);
        if (user == null)
            return NotFound("Usuário não encontrado.");
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