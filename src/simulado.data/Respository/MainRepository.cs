using Microsoft.EntityFrameworkCore;
using simulado.data.Context;
using simulado.data.Interface;


namespace simulado.data.Respository;

public abstract class MainRepository<T>(ApplicationDbContext db) : IMainRepository<T> where T : class
{
    protected readonly ApplicationDbContext Db = db;
    protected readonly DbSet<T> DbSet = db.Set<T>();

    public virtual async Task<T> FindByGuidIdAsync(Guid id) => await DbSet.FindAsync(id);

    public virtual async Task<IEnumerable<T>> FindAllAsync() => await DbSet.AsNoTracking().ToListAsync();

    public virtual async Task<bool> Add(T obj)
    {
        DbSet.Add(obj);
        return await SaveChanges() > 0;
    }

    public virtual async Task<bool> AddRangeAsync(List<T> obj)
    {
        await DbSet.AddRangeAsync(obj);
        return await SaveChanges() > 0;

    }

    public virtual async Task<bool> Update(T obj)
    {
        DbSet.Update(obj);
        return await SaveChanges() > 0;
    }

    public virtual async Task<bool> RemoveByGuidId(Guid id)
    {
        T obj = await FindByGuidIdAsync(id);
        return await RemoveGenericObject(obj);
    }

    public async Task<int> SaveChanges() => await Db.SaveChangesAsync();

    public void Dispose()
    {
        Db?.Dispose();
        GC.SuppressFinalize(this);
    }
    public async Task<bool> RemoveGenericObject(T obj)
    {
        if (obj != null)
        {
            DbSet.Remove(obj);
            return await SaveChanges() > 0;
        }
        return false;
    }

    public async Task<bool> RemoveAll(IEnumerable<T> obj)
    {
        if (!obj.Any()) return false;
        DbSet.RemoveRange(obj);
        return await SaveChanges() > 0;
    }
}
