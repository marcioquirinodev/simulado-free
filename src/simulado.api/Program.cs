using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using pandora.app.Configuration;
using simulado.data.Context;
using simulado.busisness.Services;
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

// Seed roles on startup (Administrador, Aluno)
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    RoleManager<IdentityRole<Guid>> roleManager = serviceProvider.GetRequiredService<RoleManager<Microsoft.AspNetCore.Identity.IdentityRole<Guid>>>();
    var userManager = serviceProvider.GetService<UserManager<simulado.shared.Entidades.Usuario>>();

    var rolesToEnsure = new[] { "Administrador", "Aluno" };
    foreach (var roleName in rolesToEnsure)
    {
        var exists = await roleManager.RoleExistsAsync(roleName);
        if (!exists)
        {
            await roleManager.CreateAsync(new Microsoft.AspNetCore.Identity.IdentityRole<Guid>(roleName));
        }
    }
    // optional: create initial admin user if none exists (skip if you prefer manual creation)
    // var adminEmail = builder.Configuration.GetValue<string>("Admin:Email");
    // if (!string.IsNullOrWhiteSpace(adminEmail) && userManager != null) { ... }
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
