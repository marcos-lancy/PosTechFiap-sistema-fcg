using Fcg.Application.Dtos;

namespace Fcg.Application.Interfaces;

public interface IJogoAppService
{
    Task<IEnumerable<JogoDto>> ObterTodosAsync();
    Task<JogoDto?> ObterPorIdAsync(Guid id);
    Task<JogoDto> CadastrarAsync(CadastrarJogoRequest jogo);
    Task AtualizarAsync(JogoDto jogo);
    Task RemoverAsync(Guid id);
}