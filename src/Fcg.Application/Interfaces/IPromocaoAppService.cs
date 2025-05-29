
using Fcg.Application.Dtos.Promocao;

namespace Fcg.Application.Interfaces;

public interface IPromocaoAppService
{
    Task<IEnumerable<PromocaoDto>> ObterTodosAsync();
    Task<PromocaoDto> ObterPorIdAsync(Guid id);
    Task<PromocaoDto> CadastrarAsync(CadastrarPromocaoDto role);
    Task RemoverAsync(Guid id);
}