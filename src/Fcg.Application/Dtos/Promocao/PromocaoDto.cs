using Fcg.Application.Dtos.Jogo;

namespace Fcg.Application.Dtos.Promocao;

public class PromocaoDto
{
    public Guid Id { get; set; }
    public JogoDto Jogo { get; set; }
    public decimal PrecoPromocional { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Final { get; set; }
    public bool Ativo { get; set; }
}