using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using simulado.data.Context;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public class NivelEscolaridadeService : INivelEscolaridadeService
{
    private readonly ApplicationDbContext _db;

    public NivelEscolaridadeService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<NivelEscolaridade>> GetAllAsync()
    {
        return await _db.Set<NivelEscolaridade>().AsNoTracking().ToListAsync();
    }

    public async Task<NivelEscolaridade?> GetByIdAsync(Guid id)
    {
        return await _db.Set<NivelEscolaridade>().FindAsync(id);
    }

    public async Task<NivelEscolaridade> CreateAsync(NivelEscolaridadeViewModel vm)
    {
        var entity = new NivelEscolaridade
        {
            Descricao = vm.Descricao
        };

        _db.Set<NivelEscolaridade>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Guid id, NivelEscolaridadeViewModel vm)
    {
        var entity = await _db.Set<NivelEscolaridade>().FindAsync(id);
        if (entity == null) return false;

        entity.Descricao = vm.Descricao;

        _db.Set<NivelEscolaridade>().Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _db.Set<NivelEscolaridade>().FindAsync(id);
        if (entity == null) return false;

        _db.Set<NivelEscolaridade>().Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}