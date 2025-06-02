using Fcg.Domain.Entities;

namespace Fcg.Application.Interfaces;

public interface IUsuarioAutenticadoAppService
{
    string? ObterEmail();
    Task<UsuarioEntity> ObterUsuarioAutenticadoAsync();
}