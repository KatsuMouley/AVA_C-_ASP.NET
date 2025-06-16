using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacao.Models;
using Avaliacao.Data;
using Avaliacao.Repositories.Interfaces;

namespace Avaliacao.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        //Essa construção injeta a instância do AppDbContext
        //no repositório para permitir o acesso ao banco de dados.
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Cadastrar(Usuario user)
        {
            _context.Usuarios.Add(user);
        }

        public List<Usuario> List()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario? Login(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Email == email && x.Senha == senha);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Usuario? SearchId(int id)
        {
            return _context.Usuarios.FirstOrDefault(p => p.Id == id);
        }

        public int SearchUserId(string email)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
            return usuario?.Id ?? 0;
        }

    }
}