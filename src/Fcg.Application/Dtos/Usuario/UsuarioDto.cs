using Fcg.Domain.Enums;

namespace Fcg.Application.Dtos.Usuario;
public class UsuarioDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public RoleEnum Role { get; set; }
}
