using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simulado.shared.Entidades;

namespace simulado.data.Mapping;

public class ConcursoMapping : IEntityTypeConfiguration<Concurso>
{
    public void Configure(EntityTypeBuilder<Concurso> builder)
    {
        builder.ToTable("Concursos");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Nome).HasColumnType("varchar(100)").IsRequired();
        builder.Property(t => t.Descricao).HasColumnType("varchar(255)").IsRequired();
        builder.Property(t=>t.CategoriaId).IsRequired();
        builder.Property(t=>t.NivelEscolaridadeId).IsRequired();    
    }
}
