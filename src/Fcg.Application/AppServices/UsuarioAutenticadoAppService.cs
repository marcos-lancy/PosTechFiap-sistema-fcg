using Fcg.Application.Interfaces;
using Fcg.Domain.Entities;
using Fcg.Domain.Exceptions;
using Fcg.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Fcg.Application.AppServices;
public class UsuarioAutenticadoAppService : IUsuarioAutenticadoAppService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioAutenticadoAppService(
        IHttpContextAccessor httpContextAccessor, 
        IUsuarioRepository usuarioRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _usuarioRepository = usuarioRepository;
    }

    public string? ObterEmail()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            return null;

        return user.FindFirst(ClaimTypes.Email)?.Value;
    }

    public async Task<UsuarioEntity> ObterUsuarioAutenticadoAsync()
    {
        return await _usuarioRepository.ObterPorEmailAsync(ObterEmail()) ?? 
            throw new NotFoundException("Usuário autenticado não foi encontrado.");
    }
}
