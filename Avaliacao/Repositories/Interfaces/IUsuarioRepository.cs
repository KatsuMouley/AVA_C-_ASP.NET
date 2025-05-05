using System;
using Avaliacao.Models;
namespace Avaliacao.Repositories.Interfaces;

public interface IUsuarioRepository
{
    List<Usuario> List();
    Usuario? SearchId(int id);
    void Save();
    void Cadastrar(Usuario user);
    Usuario? Login(string email, string password);
}
