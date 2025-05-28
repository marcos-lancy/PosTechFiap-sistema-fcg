using Fcg.Application.Dtos.Jogo;

namespace Fcg.Application.Interfaces;

public interface IJogoAppService
{
    Task<IEnumerable<JogoDto>> ObterTodosAsync();
    Task<JogoDto?> ObterPorIdAsync(Guid id);
    Task<JogoDto> CadastrarAsync(CadastrarJogoDto jogo);
    Task AtualizarAsync(Guid id, AtualizarJogoDto jogo);
    Task RemoverAsync(Guid id);
}