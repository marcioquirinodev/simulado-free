namespace simulado.shared.Entidades;

public class Simulado : Entidade
{
    public Guid ConcursoId { get; set; }
    public string Titulo { get; set; }
    public DateTime DataCriacao { get; set; }
    public Concurso Concurso { get; set; }
    public IEnumerable<SessaoSimulado> SessoesSimulado { get; set; }

}
