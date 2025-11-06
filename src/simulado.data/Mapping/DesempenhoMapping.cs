using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simulado.shared.Entidades;

namespace simulado.data.Mapping;

public class DesempenhoMapping : IEntityTypeConfiguration<Desempenho>
{
    public void Configure(EntityTypeBuilder<Desempenho> builder)
    {
        builder.ToTable("Desempenhos");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.UsuarioId).IsRequired();
        builder.Property(e => e.SimuladoId).IsRequired();
        builder.Property(e => e.ConcursoId).IsRequired();
        builder.Property(e => e.TotalQuestoes).IsRequired();
        builder.Property(e => e.QuestoesCorretas).IsRequired();
        builder.Property(e => e.QuestoesErradas).IsRequired();
        builder.Property(e => e.PercentualAcerto).IsRequired();
        builder.Property(e => e.DataDesempenho).IsRequired();

        // Relacionamentos (todas com Restrict)
        builder.HasOne(e => e.Usuario)
               .WithMany(u => u.Desempenhos)
               .HasForeignKey(e => e.UsuarioId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Simulado)
               .WithMany(s => s.Desempenhos)
               .HasForeignKey(e => e.SimuladoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Concurso)
               .WithMany(c => c.Desempenhos)
               .HasForeignKey(e => e.ConcursoId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

