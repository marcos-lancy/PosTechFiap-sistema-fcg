using Fcg.Application.Dtos.Usuario;
using Fcg.Application.Interfaces;
using Fcg.Domain.Exceptions.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

    [ProducesResponseType(typeof(TokenLoginDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [HttpPost("entrar")]
    public async Task<IActionResult> Entrar([FromBody] EfetuarLoginDto request)
    {
        var resultado = await _service.EfetuarLoginAsync(request.Email, request.Senha);
        return Ok(new TokenLoginDto(resultado));
    }

    [ProducesResponseType(typeof(UsuarioDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Conflict)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] CadastrarUsuarioDto request)
    {
        var usuario = await _service.CadastrarAsync(request);
        return Created($"/usuarios/{usuario.Id}", usuario);
    }
}
