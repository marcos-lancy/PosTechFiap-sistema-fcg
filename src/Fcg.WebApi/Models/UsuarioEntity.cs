using Fcg.WebApi.Utils;

namespace Fcg.WebApi.Models;

public class UsuarioEntity : EntityBase
{
    public string Nome { get; set; }
    public TipoPessoaEnum TipoUsuario { get; set; }
}