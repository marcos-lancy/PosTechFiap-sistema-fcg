using Fcg.Application.Dtos.Compra;
using Fcg.Application.Dtos.Usuario;
using Fcg.Application.Interfaces;
using Fcg.Domain.Exceptions.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fcg.WebApi.Controllers;

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
