using Fcg.Application.Dtos;

namespace Fcg.Application.Interfaces;

public interface IUsuarioAppService
{
    Task<IEnumerable<UsuarioDto>> ObterTodosAsync();
    Task<UsuarioDto?> ObterPorIdAsync(Guid id);
    Task<UsuarioDto?> ObterPorEmailAsync(string email);
    Task<UsuarioDto> CadastrarAsync(CadastrarUsuarioDto dto);
    Task AtualizarAsync(UsuarioDto dto);
    Task RemoverAsync(Guid id);
}