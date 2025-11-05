using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using simulado.shared.Entidades;

namespace simulado.data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {
       
        base.OnModelCreating(builder);
    }
}
