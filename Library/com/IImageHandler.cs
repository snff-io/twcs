namespace library.worldcomputer.info;

public interface IImageHandler
{
    Image GetNamedImage(string map);

    Image this[string map] {get;}
}