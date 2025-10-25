namespace simulado.shared.Entidades;

public class Concurso :  Entidade
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public Guid CategoriaId { get; set; }
    public Guid NivelEscolaridadeId { get; set; }
    public Categoria Categoria { get; set; }
    public NivelEscolaridade NivelEscolaridade { get; set; }
    public IEnumerable<Simulado> Simulados { get; set; }
    public IEnumerable<Desempenho> Desempenhos { get; set; }

}
