using Fcg.Domain.Entities;
using Fcg.Domain.Interfaces.Services;

namespace Fcg.Domain.Services;
public class CompraService : ICompraService
{
    public decimal ObterValorPromocionalJogo(JogoEntity jogo)
    {
        var promocaoAtiva = jogo.Promocoes.FirstOrDefault(p => p.Ativa);
        if (promocaoAtiva is not null)
            return promocaoAtiva.PrecoPromocional;

        return jogo.Preco;
    }
}
