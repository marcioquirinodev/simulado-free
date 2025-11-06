using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simulado.shared.Entidades;

namespace simulado.data.Mapping;

public class RespostaUsuarioMapping : IEntityTypeConfiguration<RespostaUsuario>
{
    public void Configure(EntityTypeBuilder<RespostaUsuario> builder)
    {
        builder.ToTable("RespostasUsuario");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.SessaoSimuladoId).IsRequired();
        builder.Property(r => r.QuestaoId).IsRequired();
        builder.Property(r => r.RespostaDada).HasColumnType("varchar(255)").IsRequired();
        builder.Property(r => r.EstaCorreta).IsRequired();
        builder.Property(r => r.DataResposta).IsRequired();

        // Relacionamentos (Restrict)
        builder.HasOne(r => r.SessaoSimulado)
               .WithMany(s => s.RespostasUsuario)
               .HasForeignKey(r => r.SessaoSimuladoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Questao)
               .WithMany(q => q.RespostasUsuario)
               .HasForeignKey(r => r.QuestaoId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}