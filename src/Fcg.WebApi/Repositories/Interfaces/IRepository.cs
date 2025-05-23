using Fcg.WebApi.Models;

namespace Fcg.WebApi.Repositories.Interfaces;

public interface IRepository<T> where T : EntityBase
{
    Task<IEnumerable<T>> ObterTodosAsync();
    Task<T?> ObterPorIdAsync(Guid id);
    Task AdicionarAsync(T entidade);
    void Atualizar(T entidade);
    void Remover(T entidade);
    Task SaveChangesAsync();
}