using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using simulado.data.Context;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public class ConcursoService : IConcursoService
{
    private readonly ApplicationDbContext _db;

    public ConcursoService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Concurso>> GetAllAsync()
    {
        return await _db.Set<Concurso>().AsNoTracking().ToListAsync();
    }

    public async Task<Concurso?> GetByIdAsync(Guid id)
    {
        return await _db.Set<Concurso>().FindAsync(id);
    }

    public async Task<Concurso> CreateAsync(ConcursoViewModel vm)
    {
        var entity = new Concurso
        {
            Nome = vm.Nome,
            Descricao = vm.Descricao,
            CategoriaId = vm.CategoriaId,
            NivelEscolaridadeId = vm.NivelEscolaridadeId
        };

        _db.Set<Concurso>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Guid id, ConcursoViewModel vm)
    {
        var entity = await _db.Set<Concurso>().FindAsync(id);
        if (entity == null) return false;

        entity.Nome = vm.Nome;
        entity.Descricao = vm.Descricao;
        entity.CategoriaId = vm.CategoriaId;
        entity.NivelEscolaridadeId = vm.NivelEscolaridadeId;

        _db.Set<Concurso>().Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _db.Set<Concurso>().FindAsync(id);
        if (entity == null) return false;

        _db.Set<Concurso>().Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}