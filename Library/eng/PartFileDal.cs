namespace library.worldcomputer.info;
class PartFileDal<T> : IDal<T>
{
    public T[] Items { get; set; }

    public void Load(string path)
    {
        throw new NotImplementedException();
    }

    public T Reload(T item)
    {
        throw new NotImplementedException();
    }

    public void Save(T item)
    {
        throw new NotImplementedException();
    }

    public void SaveAll()
    {
        throw new NotImplementedException();
    }
}