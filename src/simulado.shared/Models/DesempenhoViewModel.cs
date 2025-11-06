using System;

namespace simulado.shared.Models;

public class DesempenhoViewModel
{
    public Guid UsuarioId { get; set; }
    public Guid SimuladoId { get; set; }
    public Guid ConcursoId { get; set; }
    public int TotalQuestoes { get; set; }
    public int QuestoesCorretas { get; set; }
    public int QuestoesErradas { get; set; }
    // O serviço irá calcular PercentualAcerto e DataDesempenho, mas mantive as propriedades para retorno/uso
    public double PercentualAcerto { get; set; }
    public DateTime DataDesempenho { get; set; }
}