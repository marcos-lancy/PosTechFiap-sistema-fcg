using Fcg.Domain.Enums;

namespace Fcg.Application.Dtos;
public class UsuarioDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string SenhaHash { get; set; }
    public TipoPessoaEnum Role { get; set; }
}
