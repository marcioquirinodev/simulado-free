using Microsoft.AspNetCore.Identity;

namespace simulado.shared.Entidades;

public class Usuario : IdentityUser<Guid>
{
    public Guid NivelEscolaridadeId { get; set; }
    public DateTime DataCadastro { get; set; }
    public IEnumerable<Desempenho> Desempenhos { get; set; }
    public IEnumerable<SessaoSimulado> SessoesSimulado { get; set; }
    public NivelEscolaridade NivelEscolaridade { get; set; }

}
