using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simulado.shared.Entidades;

namespace simulado.data.Mapping;

public class NivelEscolaridadeMapping : IEntityTypeConfiguration<NivelEscolaridade>
{
    public void Configure(EntityTypeBuilder<NivelEscolaridade> builder)
    {
        builder.ToTable("NivelEscolaridades");
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Descricao).HasColumnType("varchar(255)").IsRequired();

        // Relacionamentos (Restrict)
        builder.HasMany(n => n.Usuarios)
               .WithOne(u => u.NivelEscolaridade)
               .HasForeignKey(u => u.NivelEscolaridadeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(n => n.Concursos)
               .WithOne(c => c.NivelEscolaridade)
               .HasForeignKey(c => c.NivelEscolaridadeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}