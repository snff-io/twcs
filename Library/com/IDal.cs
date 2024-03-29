namespace library.worldcomputer.info;
public interface IDal<T> {

    Task Put(T item);

    Task<T> Get(string table, string key);

    Task<T[]> GetRandomUnbound(int number = 5);

}