namespace library.worldcomputer.info;

public interface IImageHandler
{
    Image GetNamedImage(string map);

    Image this[string map] {get;}

    Task<Image> GetRandomLandscape(Domain top, Domain bottom);
    Task<List<Image>> GetLandscapeImages(Domain top, Domain bottom);

}