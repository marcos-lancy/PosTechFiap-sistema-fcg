namespace Fcg.Domain.Entities;
public class JogoAdquiridoEntity : EntityBase
{
    public Guid UsuarioId { get; set; }
    public UsuarioEntity Usuario { get; set; } = null!;

    public Guid JogoId { get; set; }
    public JogoEntity Jogo { get; set; } = null!;

    public DateTime DataAquisicao { get; set; } = DateTime.UtcNow;
    public decimal PrecoPago { get; set; }
}
