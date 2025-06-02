using Fcg.Application.Dtos.Conta;

namespace Fcg.Application.Interfaces;

public interface IContaAppService
{
    Task AtualizarAsync(AtualizarContaDto dto);
    Task DescadastrarAsync();
    Task<ContaDto> ObterAsync();
}