public interface IImageHandler
{
    Task<string> GetMappedAnsi(string map);
}