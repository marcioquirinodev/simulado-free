using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simulado.shared.Entidades;

namespace simulado.data.Mapping;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.NivelEscolaridadeId).IsRequired();
        builder.Property(x=>x.DataCadastro).IsRequired();

    }
}
