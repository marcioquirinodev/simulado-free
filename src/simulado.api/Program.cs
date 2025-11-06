using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using pandora.app.Configuration;
using simulado.data.Context;
using simulado.busisness.Services;
using simulado.shared.Entidades;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// identity and other services
builder.Services.AddCustomIdentity();

// add authentication - JWT
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection.GetValue<string>("Key") ?? throw new InvalidOperationException("JWT key not configured");
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSection.GetValue<string>("Issuer"),
        ValidAudience = jwtSection.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ClockSkew = TimeSpan.Zero
    };
});

// controllers
builder.Services.AddControllers();

// register application services (Dependency Injection)
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IConcursoService, ConcursoService>();
builder.Services.AddScoped<IDesempenhoService, DesempenhoService>();
builder.Services.AddScoped<IDisciplinaService, DisciplinaService>();
builder.Services.AddScoped<IQuestaoService, QuestaoService>();
builder.Services.AddScoped<IRespostaUsuarioService, RespostaUsuarioService>();
builder.Services.AddScoped<ISessaoSimuladoService, SessaoSimuladoService>();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Simulado API", Version = "v1" });

    // incluir XML comments se gerado
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Seed roles, NivelEscolaridade e usuário administrador no startup
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
    var db = serviceProvider.GetRequiredService<ApplicationDbContext>();
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    // 1) Roles
    var rolesToEnsure = new[] { "Administrador", "Aluno" };
    foreach (var roleName in rolesToEnsure)
    {
        var exists = await roleManager.RoleExistsAsync(roleName);
        if (!exists)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        }
    }

    // 2) NivelEscolaridade seed (idempotente)
    if (!await db.Set<NivelEscolaridade>().AnyAsync())
    {
        var niveis = new[]
        {
            new NivelEscolaridade { Descricao = "Nível Fundamental" },
            new NivelEscolaridade { Descricao = "Nível Médio" },
            new NivelEscolaridade { Descricao = "Nível Superior" }
        };

        db.Set<NivelEscolaridade>().AddRange(niveis);
        await db.SaveChangesAsync();
    }

    // 3) Criar usuário administrador padrão, se não existir
    var adminEmail = "administrador@estacio.br";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        // procura o NivelEscolaridade "Nível Superior"
        var nivelSuperior = await db.Set<NivelEscolaridade>().FirstOrDefaultAsync(n => n.Descricao == "Nível Superior");
        var nivelId = nivelSuperior?.Id ?? (await db.Set<NivelEscolaridade>().FirstOrDefaultAsync())?.Id ?? Guid.Empty;

        var admin = new Usuario
        {
            UserName = "Admistrador",
            Email = adminEmail,
            EmailConfirmed = true,
            NivelEscolaridadeId = nivelId,
            DataCadastro = DateTime.UtcNow
        };

        // senha: preferencialmente leia de configuração. fallback temporário:
        var adminPassword = configuration.GetValue<string>("Admin:Password") ?? "Admin@12345";

        var createResult = await userManager.CreateAsync(admin, adminPassword);
        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Administrador");
        }
        else
        {
            // opcional: registrar erros em log
            var logger = serviceProvider.GetService<ILoggerFactory>()?.CreateLogger("Seed");
            logger?.LogWarning("Não foi possível criar usuário administrador: {Errors}", string.Join(", ", createResult.Errors.Select(e => e.Description)));
        }
    }
    else
    {
        // se usuário existe, garante que tem a role Administrador e EmailConfirmed
        if (!await userManager.IsInRoleAsync(adminUser, "Administrador"))
            await userManager.AddToRoleAsync(adminUser, "Administrador");

        if (!adminUser.EmailConfirmed)
        {
            adminUser.EmailConfirmed = true;
            await userManager.UpdateAsync(adminUser);
        }

        // garante NivelEscolaridadeId se estiver vazio
        if (adminUser.NivelEscolaridadeId == Guid.Empty)
        {
            var nivelSuperior = await db.Set<NivelEscolaridade>().FirstOrDefaultAsync(n => n.Descricao == "Nível Superior");
            if (nivelSuperior != null)
            {
                adminUser.NivelEscolaridadeId = nivelSuperior.Id;
                await userManager.UpdateAsync(adminUser);
            }
        }
    }
}

// pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simulado API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

// enable auth middlewares
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
