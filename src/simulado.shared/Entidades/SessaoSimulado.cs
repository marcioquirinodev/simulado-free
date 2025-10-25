namespace simulado.shared.Entidades;

public class SessaoSimulado :  Entidade
{
    public Guid SimuladoId { get; set; }
    public Guid UsuarioId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public double Pontuacao { get; set; }
    public Simulado Simulado { get; set; }
    public IEnumerable<RespostaUsuario> RespostasUsuario { get; set; }
}
