using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public interface IQuestaoService
{
    Task<IEnumerable<Questao>> GetAllAsync();
    Task<Questao?> GetByIdAsync(Guid id);
    Task<Questao> CreateAsync(QuestaoViewModel vm);
    Task<bool> UpdateAsync(Guid id, QuestaoViewModel vm);
    Task<bool> DeleteAsync(Guid id);
}