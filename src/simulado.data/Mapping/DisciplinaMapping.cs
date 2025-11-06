using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simulado.shared.Entidades;

namespace simulado.data.Mapping;

public class DisciplinaMapping : IEntityTypeConfiguration<Disciplina>
{
    public void Configure(EntityTypeBuilder<Disciplina> builder)
    {
        builder.ToTable("Disciplinas");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Descricao).HasColumnType("varchar(255)").IsRequired();

        // Relacionamento (Restrict)
        builder.HasMany(d => d.Questoes)
               .WithOne(q => q.Disciplina)
               .HasForeignKey(q => q.DisciplinaId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
