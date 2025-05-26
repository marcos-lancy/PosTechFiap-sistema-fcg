using Fcg.Domain.Entities;

namespace Fcg.Domain.Interfaces;

public interface IUsuarioRepository : IRepository<UsuarioEntity>
{
    Task<UsuarioEntity?> ObterPorEmailAsync(string email);
}