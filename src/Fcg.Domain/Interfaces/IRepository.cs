using Fcg.Domain.Entities;

namespace Fcg.Domain.Interfaces;

public interface IRepository<T> where T : EntityBase
{
    Task<IEnumerable<T>> ObterTodosAsync();
    Task<T?> ObterPorIdAsync(Guid id);
    Task<T> AdicionarAsync(T entidade);
    Task Atualizar(T entidade);
    Task Remover(T entidade);
}