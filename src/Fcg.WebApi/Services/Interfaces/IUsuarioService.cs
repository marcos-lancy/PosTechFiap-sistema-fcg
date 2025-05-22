using Fcg.WebApi.Models;

namespace Fcg.WebApi.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioEntity>> ObterTodosAsync();
    Task<UsuarioEntity?> ObterPorIdAsync(Guid id);
    Task<UsuarioEntity> CriarUsuarioAsync(UsuarioEntity usuario);
    Task AtualizarUsuarioAsync(UsuarioEntity usuario);
    Task RemoverUsuarioAsync(Guid id);
}