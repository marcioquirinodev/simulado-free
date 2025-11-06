using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using simulado.data.Context;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public class DesempenhoService : IDesempenhoService
{
    private readonly ApplicationDbContext _db;

    public DesempenhoService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Desempenho>> GetAllAsync()
    {
        return await _db.Set<Desempenho>().AsNoTracking().ToListAsync();
    }

    public async Task<Desempenho?> GetByIdAsync(Guid id)
    {
        return await _db.Set<Desempenho>().FindAsync(id);
    }

    public async Task<Desempenho> CreateAsync(DesempenhoViewModel vm)
    {
        var entity = new Desempenho
        {
            UsuarioId = vm.UsuarioId,
            SimuladoId = vm.SimuladoId,
            ConcursoId = vm.ConcursoId,
            TotalQuestoes = vm.TotalQuestoes,
            QuestoesCorretas = vm.QuestoesCorretas,
            QuestoesErradas = vm.QuestoesErradas,
            DataDesempenho = DateTime.UtcNow
        };

        // calcula percentual com proteção contra divisão por zero
        entity.PercentualAcerto = entity.TotalQuestoes > 0
            ? (entity.QuestoesCorretas / (double)entity.TotalQuestoes) * 100.0
            : 0.0;

        _db.Set<Desempenho>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Guid id, DesempenhoViewModel vm)
    {
        var entity = await _db.Set<Desempenho>().FindAsync(id);
        if (entity == null) return false;

        entity.UsuarioId = vm.UsuarioId;
        entity.SimuladoId = vm.SimuladoId;
        entity.ConcursoId = vm.ConcursoId;
        entity.TotalQuestoes = vm.TotalQuestoes;
        entity.QuestoesCorretas = vm.QuestoesCorretas;
        entity.QuestoesErradas = vm.QuestoesErradas;
        entity.DataDesempenho = DateTime.UtcNow;

        entity.PercentualAcerto = entity.TotalQuestoes > 0
            ? (entity.QuestoesCorretas / (double)entity.TotalQuestoes) * 100.0
            : 0.0;

        _db.Set<Desempenho>().Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _db.Set<Desempenho>().FindAsync(id);
        if (entity == null) return false;

        _db.Set<Desempenho>().Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}