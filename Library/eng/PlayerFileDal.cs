namespace library.worldcomputer.info;
class PlayerFileDal<IPlayer> : IDal<IPlayer>
{
    public IPlayer[] Items { get; set; }

    public void Load(string path)
    {
        throw new NotImplementedException();
    }

    public IPlayer Reload(IPlayer item)
    {
        throw new NotImplementedException();
    }

    public void Save(IPlayer item)
    {
        throw new NotImplementedException();
    }

    public void SaveAll()
    {
        throw new NotImplementedException();
    }
}