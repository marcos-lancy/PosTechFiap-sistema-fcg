using Fcg.Application.Dtos.Jogo;
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ObterTodos()
        {
            var jogos = await _service.ObterTodosAsync();
            return Ok(jogos);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
        {
            var jogo = await _service.ObterPorIdAsync(id);
            return Ok(jogo);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarJogoDto dto)
        {
            var registro = await _service.CadastrarAsync(dto);
            return Created($"/jogos/{registro.Id}", registro);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Atualizar(
            [FromRoute] Guid id,
            [FromBody] AtualizarJogoDto dto)
        {
            await _service.AtualizarAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remover([FromRoute] Guid id)
        {
            await _service.RemoverAsync(id);
            return NoContent();
        }
    }
}
