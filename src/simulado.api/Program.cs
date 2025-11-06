using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using pandora.app.Configuration;
using simulado.data.Context;

var builder = WebApplication.CreateBuilder(args);

// connection string (já existente)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// identity and other services
builder.Services.AddCustomIdentity();

// controllers
builder.Services.AddControllers();

// Swagger / OpenAPI (registrar sempre, ativar apenas em Development)
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

// pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // gera swagger.json
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simulado API v1");
        c.RoutePrefix = "swagger"; // acesso em /swagger
        // c.RoutePrefix = string.Empty; // descomente para servir UI na raiz durante dev
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
