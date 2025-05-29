using Fcg.Application.Dtos.Promocao;
using Fcg.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fcg.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PromocoesController : MainController
    {
        private readonly IPromocaoAppService _service;

        public PromocoesController(IPromocaoAppService service)
        {
            _service = service;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var promocao = await _service.ObterPorIdAsync(id);
            return Ok(promocao);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var promocoes = await _service.ObterTodosAsync();
            return Ok(promocoes);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarPromocaoDto role)
        {
            var promo = await _service.CadastrarAsync(role);
            return Created($"api/v1/promocoes/{promo.Id}",promo);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            await _service.RemoverAsync(id);
            return NoContent();
        }
    }
}