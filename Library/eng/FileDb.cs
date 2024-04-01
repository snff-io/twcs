using YamlDotNet;
using library.worldcomputer.info;
using System.Security.AccessControl;
using YamlDotNet.Serialization;
using System.Collections.Concurrent;
using Amazon.DynamoDBv2.Model;
using System.Linq;
using System.Security.Cryptography.X509Certificates;


public class FileDb<T> : IDal<T>
    where T : IUnit
{
    private string _collection;
    private string _sourceIdx;
    private string _boundIdx;
    private IDeserializer _deserializer;

    public FileDb(string basePath = "srv/fileDb")
    {
        string col = typeof(T).Name;
        _collection = basePath + "/" + col + "/";
        _sourceIdx = basePath + "/index/names_" + col + ".txt";
        _boundIdx = basePath + "/index/bound_names" + col + ".txt";

        if (!Directory.Exists(_collection))
            throw new DirectoryNotFoundException($"Collection directory {col} not found!");
        if (!File.Exists(_sourceIdx))
            throw new FileNotFoundException("Source index not found!", _sourceIdx);
        if (!File.Exists(_boundIdx))
            throw new FileNotFoundException("Bound index not found!", _boundIdx);

        _deserializer = new DeserializerBuilder().Build();
    }

    public async Task<T> Get(string key)
    {
        if (!File.Exists(_collection + key))
            return default;

        var raw = await File.ReadAllTextAsync(_collection + key);
        using (var reader = new StringReader(raw))
        {
            return _deserializer.Deserialize<T>(reader);
        }
    }

    public async Task<IEnumerable<T>> GetByAttr<TValue>(string attribute, TValue value)
    {
        if (attribute == null || value == null)
            throw new ArgumentException("attribute or value is null");

        var searchTerm = $"{attribute}: {value.ToString()}";

        var files = Directory.GetFiles(_collection, "*.*", SearchOption.AllDirectories);

        var cbag = new ConcurrentBag<T>();
        await Parallel.ForEachAsync(files, async (file, ct) =>
        {
            // Read the contents of the file
            string content = await File.ReadAllTextAsync(file);

            // Check if the content contains the search term
            if (content.Contains(searchTerm))
            {
                var raw = await File.ReadAllTextAsync(file);
                using (var reader = new StringReader(raw))
                {
                    cbag.Add(_deserializer.Deserialize<T>(reader));
                }
            }
        });

        return cbag;
    }

    public async Task<T[]> GetRandomUnbound(int number = 5)
    {

        throw new NotFiniteNumberException();
        IEnumerable<string> bound = await File.ReadAllLinesAsync(_boundIdx);
        using (var reader = new StreamReader(_sourceIdx))
        {
            var collected = 0;
            while (collected <= 5)
            {

                var line = await reader.ReadLineAsync();
                if (line.StartsWith("-"))
                {

                }
            }
        }
    }

    public Task Put(T item)
    {
        throw new NotImplementedException();
    }
}
