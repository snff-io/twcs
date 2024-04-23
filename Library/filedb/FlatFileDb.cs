using YamlDotNet;
using library.worldcomputer.info;
using System.Security.AccessControl;
using YamlDotNet.Serialization;
using System.Collections.Concurrent;
using Amazon.DynamoDBv2.Model;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class FlatFileDb<T> : IDal<T>
    where T : IUnit
{
    private string _collection;
    private IDeserializer _deserializer;
    private ISerializer _serializer;
    private string _basePath;

    public FlatFileDb(string basePath = "/srv/fileDb")
    {
        _collection = typeof(T).Name;
        _basePath = basePath;
        _deserializer = new DeserializerBuilder().Build();
        _serializer = new SerializerBuilder().Build();
    }

    public async Task<T> Get(string key)
    {
        var fileName = $"{_basePath}/{_collection}/{key[0]}/{key}.yaml".ToLower();

        if (!File.Exists(fileName))
        {
            throw new KeyNotFoundException("The specified key was not in the database: " + key);
        }

        var stream = await File.ReadAllTextAsync(fileName);

        using (var reader = new StringReader(stream))
        {
            try
            {

                return _deserializer.Deserialize<T>(reader);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        return default;
    }

    public async Task<IEnumerable<T>> GetByAttr<TValue>(string attribute, TValue value, CancellationToken cancellationToken = default)
    {
        if (attribute == null || value == null)
            throw new ArgumentException("attribute or value is null");

        var searchTerm = $"{attribute}: {value}";

        var files = Directory.GetFiles(Path.Combine(_basePath, _collection).ToLower(), "*.*", SearchOption.AllDirectories);

        var tasks = files.Select(async file =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var stream = File.OpenRead(file))
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.Contains(searchTerm))
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        var raw = await File.ReadAllTextAsync(file, cancellationToken);
                        using (var strReader = new StringReader(raw))
                        {
                            return _deserializer.Deserialize<T>(strReader);
                        }
                    }
                }
            }

            return default(T);
        });

        var results = await Task.WhenAll(tasks);
        return results.Where(result => !EqualityComparer<T>.Default.Equals(result, default(T)));
    }




    public async Task<T[]> GetRandomUnbound(int number = 5)
    {
        var dirs = Directory.GetDirectories($"{_basePath}/{_collection}".ToLower());
        var random = new Random();
        dirs = dirs.OrderBy(d => random.Next()).ToArray();

        var cbag = new ConcurrentBag<T>();

        foreach (var d in dirs)
        {
            var dfiles = Directory.GetFiles(d);

            var idx = 0;
            while (cbag.Count() < number && idx < dfiles.Length)
            {
                var text = await File.ReadAllTextAsync(dfiles[idx]);
                idx++;
                if (!text.Contains("Bound: true"))
                {
                    var unit = _deserializer.Deserialize<T>(text);

                    if (unit.Bound)
                        continue;

                    cbag.Add(_deserializer.Deserialize<T>(text));
                }
            }
        }

        return cbag.ToArray();
    }

    public async Task Put(T item)
    {
        var directory = $"{_basePath}/{_collection}/{item.Id[0]}".ToLower();
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory.ToLower());

        var fileName = $"{_basePath}/{_collection}/{item.Id[0]}/{item.Id}.yaml".ToLower();

        var yaml = _serializer.Serialize(item);

        await File.WriteAllTextAsync(fileName, yaml, Encoding.UTF8);
    }
}