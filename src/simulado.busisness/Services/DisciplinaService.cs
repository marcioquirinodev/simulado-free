using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using simulado.data.Context;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public class DisciplinaService : IDisciplinaService
{
    private readonly ApplicationDbContext _db;

    public DisciplinaService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Disciplina>> GetAllAsync()
    {
        return await _db.Set<Disciplina>().AsNoTracking().ToListAsync();
    }

    public async Task<Disciplina?> GetByIdAsync(Guid id)
    {
        return await _db.Set<Disciplina>().FindAsync(id);
    }

    public async Task<Disciplina> CreateAsync(DisciplinaViewModel vm)
    {
        var entity = new Disciplina
        {
            Descricao = vm.Descricao
        };

        _db.Set<Disciplina>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Guid id, DisciplinaViewModel vm)
    {
        var entity = await _db.Set<Disciplina>().FindAsync(id);
        if (entity == null) return false;

        entity.Descricao = vm.Descricao;

        _db.Set<Disciplina>().Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _db.Set<Disciplina>().FindAsync(id);
        if (entity == null) return false;

        _db.Set<Disciplina>().Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}