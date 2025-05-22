using Fcg.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fcg.WebApi.Data.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<UsuarioEntity>
{
    public void Configure(EntityTypeBuilder<UsuarioEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(x => x.Nome).IsRequired();

        builder.ToTable("Usuarios");
    }
}