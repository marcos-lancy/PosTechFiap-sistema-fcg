using Fcg.Application.Dtos.Usuario;
using Fcg.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fcg.WebApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : MainController
{
    private readonly IUsuarioAppService _service;
    private readonly IServiceProvider _serviceProvider;
    public AuthController(IUsuarioAppService service, IServiceProvider serviceProvider)
    {
        _service = service;
        _serviceProvider = serviceProvider;
    }

    [HttpPost("entrar")]
    public async Task<IActionResult> Entrar([FromBody] LoginDto request)
    {
        var resultado = await _service.EfetuarLoginAsync(request.Email, request.Senha);
        return Ok(new { token = resultado });
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] CadastrarUsuarioDto request)
    {
        var usuario = await _service.CadastrarAsync(request);
        return Created($"/usuarios/{usuario.Id}", usuario);
    }
}
