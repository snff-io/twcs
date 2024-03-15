namespace library.worldcomputer.info;
interface IDal<T> {
    
    T[] Items {get;set;}

    void Load(string path);
    T Reload(T item);

    void SaveAll();
    void Save(T item);

}