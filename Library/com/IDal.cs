namespace library.worldcomputer.info;
public interface IDal<T>
{

    Task Put(T item);

    Task<T> Get(string key);

    Task<T[]> GetRandomUnbound(int number = 5);

    Task<IEnumerable<T>> GetByAttr<TValue>(string attribute, TValue value, CancellationToken cancellationToken = default);
}