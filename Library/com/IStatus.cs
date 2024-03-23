public interface IStatus
{
    public string this[int id]
    {
        get;
    }

    public string this[string id]
    {
        get;
    }
}