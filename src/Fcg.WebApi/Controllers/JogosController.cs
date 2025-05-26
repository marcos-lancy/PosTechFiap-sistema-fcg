using Fcg.Application.Dtos;
using Fcg.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fcg.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class JogosController : MainController
    {
        private readonly IJogoAppService _service;

        public JogosController(IJogoAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarJogoRequest dto)
        {
            var registro = await _service.CadastrarAsync(dto);
            return Created($"/jogos/{registro.Id}", registro);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Consultar()
        {
            var jogos = await _service.ObterTodosAsync();
            return Ok(jogos);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Consultar([FromRoute] Guid id)
        {
            var jogo = await _service.ObterPorIdAsync(id);
            return jogo is null ? NotFound() : Ok(jogo);
        }
    }
}
