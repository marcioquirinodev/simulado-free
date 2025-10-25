namespace simulado.shared.Entidades;

public class Desempenho : Entidade
{
    public Guid UsuarioId { get; set; }
    public Guid SimuladoId { get; set; }
    public Guid ConcursoId { get; set; }
    public int TotalQuestoes { get; set; }
    public int QuestoesCorretas { get; set; }
    public int QuestoesErradas { get; set; }
    public double PercentualAcerto { get; set; }
    public DateTime DataDesempenho { get; set; }
    public Usuario Usuario { get; set; }
    public Simulado Simulado { get; set; }
    public Concurso Concurso { get; set; }
}
