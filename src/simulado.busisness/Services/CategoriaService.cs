using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using simulado.data.Context;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ApplicationDbContext _db;

    public CategoriaService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _db.Set<Categoria>().AsNoTracking().ToListAsync();
    }

    public async Task<Categoria?> GetByIdAsync(Guid id)
    {
        return await _db.Set<Categoria>().FindAsync(id);
    }

    public async Task<Categoria> CreateAsync(CategoriaViewModel vm)
    {
        var entity = new Categoria
        {
            Nome = vm.Nome,
            Descricao = vm.Descricao
        };

        _db.Set<Categoria>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Guid id, CategoriaViewModel vm)
    {
        var entity = await _db.Set<Categoria>().FindAsync(id);
        if (entity == null) return false;

        entity.Nome = vm.Nome;
        entity.Descricao = vm.Descricao;

        _db.Set<Categoria>().Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _db.Set<Categoria>().FindAsync(id);
        if (entity == null) return false;

        _db.Set<Categoria>().Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}