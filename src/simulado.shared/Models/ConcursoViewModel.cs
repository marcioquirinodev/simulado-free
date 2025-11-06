using System;

namespace simulado.shared.Models;

public class ConcursoViewModel
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public Guid CategoriaId { get; set; }
    public Guid NivelEscolaridadeId { get; set; }
}