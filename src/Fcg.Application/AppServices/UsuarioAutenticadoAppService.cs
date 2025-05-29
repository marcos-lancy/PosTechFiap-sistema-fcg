using Fcg.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Fcg.Application.AppServices;
public class UsuarioAutenticadoAppService : IUsuarioAutenticadoAppService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioAutenticadoAppService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? ObterEmail()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            return null;

        return user.FindFirst(ClaimTypes.Email)?.Value;
    }
}
