using Fcg.Application.Dtos.Compra;
using Fcg.Application.Interfaces;
using Fcg.Domain.Exceptions.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Fcg.WebApi.Controllers;

/// <summary>
/// Responsável pelos endpoints de compras de jogos.
/// </summary>
[Authorize]
[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ComprasController : MainController
{
    private readonly ICompraAppService _service;
    public ComprasController(ICompraAppService service)
    {
        _service = service;
    }

    /// <summary>
    /// Realiza a compra de um jogo para o usuário autenticado.
    /// </summary>
    /// <param name="request">Dados da compra do jogo.</param>
    /// <returns>Informações do jogo adquirido.</returns>
    [SwaggerOperation(
        Summary = "Compra um jogo.",
        Description = "Realiza a compra de um jogo para o usuário autenticado e retorna os detalhes da aquisição."
    )]
    [ProducesResponseType(typeof(JogoAdquiridoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Conflict)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [HttpPost("jogos")]
    public async Task<IActionResult> ComprarJogo([FromBody] ComprarJogoDto request)
    {
        var compra = await _service.ComprarJogoAsync(request);
        return Ok(compra);
    }
}
