using Fcg.Application.AppServices;
using Fcg.Application.Dtos;
using Fcg.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fcg.WebApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : MainController
{
    private readonly IUsuarioAppService _service;
    private readonly JwtAppService _jwt;

    public AuthController(
        IUsuarioAppService service,
        JwtAppService jwt)
    {
        _service = service;
        _jwt = jwt;
    }

    [HttpPost("entrar")]
    public async Task<IActionResult> Entrar([FromBody] EfetuarLoginDto request)
    {
        // TODO: Fazer logica de login em uma service
        var usuario = await _service.ObterPorEmailAsync(request.Email);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
            return Unauthorized();

        var token = _jwt.GerarToken(usuario.Email, usuario.Role.ToString());

        return Ok(new
        {
            token
        });
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] CadastrarUsuarioDto request)
    {
        var usuario = await _service.CadastrarAsync(request);

        return Created($"/usuarios/{usuario.Id}", usuario);
    }
}
