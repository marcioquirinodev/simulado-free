using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using simulado.data.Context;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public class QuestaoService : IQuestaoService
{
    private readonly ApplicationDbContext _db;

    public QuestaoService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Questao>> GetAllAsync()
    {
        return await _db.Set<Questao>().AsNoTracking().ToListAsync();
    }

    public async Task<Questao?> GetByIdAsync(Guid id)
    {
        return await _db.Set<Questao>().FindAsync(id);
    }

    public async Task<Questao> CreateAsync(QuestaoViewModel vm)
    {
        var entity = new Questao
        {
            TextoPergunta = vm.TextoPergunta,
            RespostaCerta = vm.RespostaCerta,
            RespostaErradaUm = vm.RespostaErradaUm,
            RespostaErradaDois = vm.RespostaErradaDois,
            RespostaErradaTres = vm.RespostaErradaTres,
            RespostaErradaQuatro = vm.RespostaErradaQuatro,
            SimuladoId = vm.SimuladoId,
            DisciplinaId = vm.DisciplinaId
        };

        _db.Set<Questao>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Guid id, QuestaoViewModel vm)
    {
        var entity = await _db.Set<Questao>().FindAsync(id);
        if (entity == null) return false;

        entity.TextoPergunta = vm.TextoPergunta;
        entity.RespostaCerta = vm.RespostaCerta;
        entity.RespostaErradaUm = vm.RespostaErradaUm;
        entity.RespostaErradaDois = vm.RespostaErradaDois;
        entity.RespostaErradaTres = vm.RespostaErradaTres;
        entity.RespostaErradaQuatro = vm.RespostaErradaQuatro;
        entity.SimuladoId = vm.SimuladoId;
        entity.DisciplinaId = vm.DisciplinaId;

        _db.Set<Questao>().Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _db.Set<Questao>().FindAsync(id);
        if (entity == null) return false;

        _db.Set<Questao>().Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}