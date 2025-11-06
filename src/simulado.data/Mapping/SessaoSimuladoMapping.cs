using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simulado.shared.Entidades;

namespace simulado.data.Mapping;

public class SessaoSimuladoMapping : IEntityTypeConfiguration<SessaoSimulado>
{
    public void Configure(EntityTypeBuilder<SessaoSimulado> builder)
    {
        builder.ToTable("SessoesSimulado");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.SimuladoId).IsRequired();
        builder.Property(s => s.UsuarioId).IsRequired();
        builder.Property(s => s.DataInicio).IsRequired();
        builder.Property(s => s.DataFim);
        builder.Property(s => s.Pontuacao).IsRequired();

        // Relacionamentos (Restrict)
        builder.HasOne(s => s.Simulado)
               .WithMany(sim => sim.SessoesSimulado)
               .HasForeignKey(s => s.SimuladoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Usuario)
               .WithMany(u => u.SessoesSimulado)
               .HasForeignKey(s => s.UsuarioId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.RespostasUsuario)
               .WithOne(r => r.SessaoSimulado)
               .HasForeignKey(r => r.SessaoSimuladoId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}