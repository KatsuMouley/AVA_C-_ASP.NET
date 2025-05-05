using System;
using Avaliacao.Models;
namespace Avaliacao.Repositories.Interfaces;

public interface IEventoRepository
{
    
    List<Evento> List();
    Evento? SearchId(int userid);
    void Save();
    void Cadastrar(Evento evento);
}
