using Fcg.Application.Dtos.Usuario;
using Fcg.Application.Interfaces;
using Fcg.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fcg.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsuarioController : MainController
    {
        private readonly IUsuarioAppService _service;

        public UsuarioController(IUsuarioAppService service)
        {
            _service = service;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var usuario = await _service.ObterPorIdAsync(id);
            return Ok(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var usuarios = await _service.ObterTodosAsync();
            return Ok(usuarios);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(
            [FromRoute] Guid id, 
            [FromBody] AtualizarUsuarioDto usuarioDto)
        {
            await _service.AtualizarAsync(id, usuarioDto);
            return NoContent();
        }

        [HttpPut("{id:guid}/role")]
        public async Task<IActionResult> AtualizarRole(
            [FromRoute] Guid id,
            [FromBody] RoleEnum role)
        {
            await _service.AtualizarRoleAsync(id, role);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            await _service.RemoverAsync(id);
            return NoContent();
        }
    }
}