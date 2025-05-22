using Fcg.WebApi.Models;

namespace Fcg.WebApi.Services.Interfaces;

public interface IJogoService
{
    Task<IEnumerable<JogoEntity>> ObterTodosAsync();
    Task<JogoEntity?> ObterPorIdAsync(Guid id);
    Task<JogoEntity> CriarUsuarioAsync(JogoEntity jogo);
    Task AtualizarUsuarioAsync(JogoEntity jogo);
    Task RemoverUsuarioAsync(Guid id);
}