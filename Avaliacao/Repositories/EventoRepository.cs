using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacao.Models;
using Avaliacao.Data;
using Avaliacao.Repositories.Interfaces;

namespace Avaliacao.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly ApplicationDbContext _context;
        //Essa construção injeta a instância do AppDbContext
        //no repositório para permitir o acesso ao banco de dados.
        public EventoRepository( ApplicationDbContext context)
        {
            _context = context;
        }

        public void Cadastrar(Evento evento)
        {
            _context.Eventos.Add(evento);
        }

        public List<Evento> List()
        {
            return _context.Eventos.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Evento? SearchId(int id)
        {
            return _context.Eventos.FirstOrDefault(p => p.Id == id);
        }
        public Evento? SearchEveryId(int id)
        {
            return _context.Eventos.ToList(p => p.UsuarioId == id);
        }

        
        public int SearchUserId(string id)
        {
            return _context.Usuarios.FirstOrDefault(p => p.Email == id).Id;
        }
    }
}