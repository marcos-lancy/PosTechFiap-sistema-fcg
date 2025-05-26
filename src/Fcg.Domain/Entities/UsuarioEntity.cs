using Fcg.Domain.Enums;

namespace Fcg.Domain.Entities;

public class UsuarioEntity : EntityBase
{
    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string SenhaHash { get; set; } = string.Empty;

    public TipoPessoaEnum Role { get; set; } = TipoPessoaEnum.Usuario;

    public List<JogoAdquiridoEntity> JogosAdquiridos { get; set; } = [];

    public UsuarioEntity()
    {
    }

    public UsuarioEntity(
        Guid id,
        string nome,
        string email,
        string senhaHash,
        TipoPessoaEnum role,
        List<JogoAdquiridoEntity> jogosAdquiridos)
    {
        Id = id;
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Role = role;
        JogosAdquiridos = jogosAdquiridos;
    }
}