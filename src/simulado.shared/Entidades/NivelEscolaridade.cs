namespace simulado.shared.Entidades;

public class NivelEscolaridade : Entidade
{
    public string Descricao { get; set; }
    public IEnumerable<Usuario> Usuarios { get; set; }
    public IEnumerable<Concurso> Concursos { get; set; }
}
