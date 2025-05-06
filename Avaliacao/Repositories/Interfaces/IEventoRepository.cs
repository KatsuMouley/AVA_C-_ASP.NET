using System;
using Avaliacao.Models;
namespace Avaliacao.Repositories.Interfaces;

public interface IEventoRepository
{
    
    List<Evento> List();
    Evento? SearchId(int userid);
    int SearchUserId(string userid);
    void Save();
    void Cadastrar(Evento evento);
    public Evento? SearchEveryId(int id);
}
