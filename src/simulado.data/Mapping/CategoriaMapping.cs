using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simulado.shared.Entidades;

namespace simulado.data.Mapping;

public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");
        builder.HasKey(c => c.Id);
        builder.Property(c=>c.Descricao).HasColumnType("varchar(255)").IsRequired();
        builder.Property(c => c.Nome).HasColumnType("varchar(100)");
    }
}
