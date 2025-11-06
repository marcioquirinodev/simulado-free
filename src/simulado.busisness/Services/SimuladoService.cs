using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using simulado.data.Context;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public class SimuladoService : ISimuladoService
{
    private readonly ApplicationDbContext _db;

    public SimuladoService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Simulado>> GetAllAsync()
    {
        return await _db.Set<Simulado>().AsNoTracking().ToListAsync();
    }

    public async Task<Simulado?> GetByIdAsync(Guid id)
    {
        return await _db.Set<Simulado>().FindAsync(id);
    }

    public async Task<Simulado> CreateAsync(SimuladoViewModel vm)
    {
        var entity = new Simulado
        {
            ConcursoId = vm.ConcursoId,
            Titulo = vm.Titulo,
            DataCriacao = vm.DataCriacao == default ? DateTime.UtcNow : vm.DataCriacao
        };

        _db.Set<Simulado>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Guid id, SimuladoViewModel vm)
    {
        var entity = await _db.Set<Simulado>().FindAsync(id);
        if (entity == null) return false;

        entity.ConcursoId = vm.ConcursoId;
        entity.Titulo = vm.Titulo;
        // manter DataCriacao existente (não sobrescrever)

        _db.Set<Simulado>().Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _db.Set<Simulado>().FindAsync(id);
        if (entity == null) return false;

        _db.Set<Simulado>().Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}