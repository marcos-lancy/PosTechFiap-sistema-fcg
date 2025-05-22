using Microsoft.AspNetCore.Mvc;

namespace Fcg.WebApi.Controllers
{
    public class UsuarioController : MainController
    {
        [HttpPost]
        public async Task<IActionResult> Incluir()
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar()
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Remover()
        {
            return Ok();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Obter()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            return Ok();
        }
    }
}
