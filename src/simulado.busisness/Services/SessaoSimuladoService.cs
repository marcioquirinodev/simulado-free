using Microsoft.EntityFrameworkCore;
using simulado.data.Context;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public class SessaoSimuladoService : ISessaoSimuladoService
{
    private readonly ApplicationDbContext _db;

    public SessaoSimuladoService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<SessaoSimulado>> GetAllAsync()
    {
        return await _db.Set<SessaoSimulado>().AsNoTracking().ToListAsync();
    }

    public async Task<SessaoSimulado?> GetByIdAsync(Guid id)
    {
        return await _db.Set<SessaoSimulado>().FindAsync(id);
    }

    public async Task<SessaoSimulado> CreateAsync(SessaoSimuladoViewModel vm)
    {
        var entity = new SessaoSimulado
        {
            SimuladoId = vm.SimuladoId,
            UsuarioId = vm.UsuarioId,
            DataInicio = vm.DataInicio == default ? DateTime.UtcNow : vm.DataInicio,
            DataFim = vm.DataFim,
            Pontuacao = vm.Pontuacao
        };

        _db.Set<SessaoSimulado>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Guid id, SessaoSimuladoViewModel vm)
    {
        var entity = await _db.Set<SessaoSimulado>().FindAsync(id);
        if (entity == null) return false;

        entity.SimuladoId = vm.SimuladoId;
        entity.UsuarioId = vm.UsuarioId;
        entity.DataInicio = vm.DataInicio;
        entity.DataFim = vm.DataFim;
        entity.Pontuacao = vm.Pontuacao;

        _db.Set<SessaoSimulado>().Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _db.Set<SessaoSimulado>().FindAsync(id);
        if (entity == null) return false;

        _db.Set<SessaoSimulado>().Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}