using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using simulado.data.Context;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public class RespostaUsuarioService : IRespostaUsuarioService
{
    private readonly ApplicationDbContext _db;

    public RespostaUsuarioService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<RespostaUsuario>> GetAllAsync()
    {
        return await _db.Set<RespostaUsuario>().AsNoTracking().ToListAsync();
    }

    public async Task<RespostaUsuario?> GetByIdAsync(Guid id)
    {
        return await _db.Set<RespostaUsuario>().FindAsync(id);
    }

    public async Task<RespostaUsuario> CreateAsync(RespostaUsuarioViewModel vm)
    {
        var entity = new RespostaUsuario
        {
            SessaoSimuladoId = vm.SessaoSimuladoId,
            QuestaoId = vm.QuestaoId,
            RespostaDada = vm.RespostaDada,
            EstaCorreta = vm.EstaCorreta,
            DataResposta = vm.DataResposta
        };

        _db.Set<RespostaUsuario>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Guid id, RespostaUsuarioViewModel vm)
    {
        var entity = await _db.Set<RespostaUsuario>().FindAsync(id);
        if (entity == null) return false;

        entity.SessaoSimuladoId = vm.SessaoSimuladoId;
        entity.QuestaoId = vm.QuestaoId;
        entity.RespostaDada = vm.RespostaDada;
        entity.EstaCorreta = vm.EstaCorreta;
        entity.DataResposta = vm.DataResposta;

        _db.Set<RespostaUsuario>().Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _db.Set<RespostaUsuario>().FindAsync(id);
        if (entity == null) return false;

        _db.Set<RespostaUsuario>().Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}