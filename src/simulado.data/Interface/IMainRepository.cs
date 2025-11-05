namespace simulado.data.Interface;

public interface IMainRepository<T> : IDisposable where T : class
{
    Task<bool> Add(T obj);
    Task<bool> AddRangeAsync(List<T> obj);
    Task<IEnumerable<T>> FindAllAsync();
    Task<T> FindByGuidIdAsync(Guid id);
    Task<bool> RemoveAll(IEnumerable<T> obj);
    Task<bool> RemoveByGuidId(Guid id);
    Task<bool> RemoveGenericObject(T obj);
    Task<int> SaveChanges();
    Task<bool> Update(T obj);
}