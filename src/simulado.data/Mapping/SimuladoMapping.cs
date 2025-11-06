using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simulado.shared.Entidades;

namespace simulado.data.Mapping;

public class SimuladoMapping : IEntityTypeConfiguration<Simulado>
{
    public void Configure(EntityTypeBuilder<Simulado> builder)
    {
        builder.ToTable("Simulados");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.ConcursoId).IsRequired();
        builder.Property(s => s.Titulo).HasColumnType("varchar(150)").IsRequired();
        builder.Property(s => s.DataCriacao).IsRequired();

        // Relacionamentos (todos Restrict)
        builder.HasOne(s => s.Concurso)
               .WithMany(c => c.Simulados)
               .HasForeignKey(s => s.ConcursoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Questoes)
               .WithOne(q => q.Simulado)
               .HasForeignKey(q => q.SimuladoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.SessoesSimulado)
               .WithOne(ss => ss.Simulado)
               .HasForeignKey(ss => ss.SimuladoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Desempenhos)
               .WithOne(d => d.Simulado)
               .HasForeignKey(d => d.SimuladoId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}