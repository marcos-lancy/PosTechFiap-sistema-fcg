namespace Fcg.WebApi.Models;

public class JogoEntity : EntityBase
{
    public string Nome { get; set; }
    public string Descricao { get; set; }

    public JogoEntity(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
    }
}