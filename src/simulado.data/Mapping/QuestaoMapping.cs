using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simulado.shared.Entidades;

namespace simulado.data.Mapping;

public class QuestaoMapping : IEntityTypeConfiguration<Questao>
{
    public void Configure(EntityTypeBuilder<Questao> builder)
    {
        builder.ToTable("Questoes");
        builder.HasKey(q => q.Id);

        builder.Property(q => q.TextoPergunta).HasColumnType("varchar(255)").IsRequired();
        builder.Property(q => q.RespostaCerta).HasColumnType("varchar(255)").IsRequired();
        builder.Property(q => q.RespostaErradaUm).HasColumnType("varchar(255)").IsRequired();
        builder.Property(q => q.RespostaErradaDois).HasColumnType("varchar(255)").IsRequired();
        builder.Property(q => q.RespostaErradaTres).HasColumnType("varchar(255)").IsRequired();
        builder.Property(q => q.RespostaErradaQuatro).HasColumnType("varchar(255)").IsRequired();

        builder.Property(q => q.SimuladoId).IsRequired();
        builder.Property(q => q.DisciplinaId).IsRequired();

        // Relacionamentos (Restrict)
        builder.HasOne(q => q.Simulado)
               .WithMany(s => s.Questoes)
               .HasForeignKey(q => q.SimuladoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(q => q.Disciplina)
               .WithMany(d => d.Questoes)
               .HasForeignKey(q => q.DisciplinaId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(q => q.RespostasUsuario)
               .WithOne(r => r.Questao)
               .HasForeignKey(r => r.QuestaoId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}