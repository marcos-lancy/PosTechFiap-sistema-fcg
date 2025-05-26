using Microsoft.AspNetCore.Mvc;

namespace Fcg.WebApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class MainController : ControllerBase
{
}
