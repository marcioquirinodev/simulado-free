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
        builder.Property(x => x.DataCadastro).IsRequired();

        // Relacionamentos (Restrict)
        builder.HasOne<Usuario, NivelEscolaridade>(x => x.NivelEscolaridade)
               .WithMany(n => n.Usuarios)
               .HasForeignKey(x => x.NivelEscolaridadeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Desempenhos)
               .WithOne(d => d.Usuario)
               .HasForeignKey(d => d.UsuarioId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.SessoesSimulado)
               .WithOne(s => s.Usuario)
               .HasForeignKey(s => s.UsuarioId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
