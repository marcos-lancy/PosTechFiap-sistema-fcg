namespace Fcg.WebApi.Models;

public abstract class EntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
}