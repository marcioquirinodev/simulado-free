using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using simulado.shared.Entidades;
using simulado.data.Mapping;

namespace simulado.data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply entity mappings
        builder.ApplyConfiguration(new CategoriaMapping());
        builder.ApplyConfiguration(new ConcursoMapping());
        builder.ApplyConfiguration(new DesempenhoMapping());
        builder.ApplyConfiguration(new DisciplinaMapping());
        builder.ApplyConfiguration(new NivelEscolaridadeMapping());
        builder.ApplyConfiguration(new QuestaoMapping());
        builder.ApplyConfiguration(new RespostaUsuarioMapping());
        builder.ApplyConfiguration(new SessaoSimuladoMapping());
        builder.ApplyConfiguration(new SimuladoMapping());
        builder.ApplyConfiguration(new UsuarioMapping());
}
}
