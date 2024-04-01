
using System;
using System.IO;
using System.IO.Compression;
using MongoDB.Bson.Serialization.Conventions;
using YamlDotNet.Serialization;

namespace library.worldcomputer.info;

public class ZipFileDb<T> : IDal<T>
{
    private string _zipFilePath;
    private string _collection;
    private IDeserializer _deserializer;

    public ZipFileDb(string zipFilePath)
    {
        _zipFilePath = zipFilePath;
        _collection = typeof(T).Name;
        _deserializer = new DeserializerBuilder().Build();
    }

    public async Task<T> Get(string key)
    {
        var zip = ZipFile.OpenRead(_zipFilePath);

        ZipArchiveEntry found = null;
        //File: filedb/heart/m/Maarcin_Räj.yaml
        //Content: firstName: Maarcin
        var entryKey = $"filedb/{_collection}/{key.ToLower()[0]}/{key}.yaml";
        //ZipArchiveEntry found = null;

        // await Parallel.ForEachAsync(zip.Entries, async (entry, ct) =>
        // {
        //     if (entry.FullName.ToLower() == entryKey.ToLower())
        //     {
        //         found = entry;
        //     }
        // });

        foreach (var entry in zip.Entries) {
            if (entry.FullName.ToLower() == entryKey.ToLower())
            {
                found = entry;
            }
        }

        // var tasks = zip.Entries.Select(async entry =>
        // {
        //     if (entry.FullName.ToLower() == entryKey.ToLower())
        //     {
        //         return entry;
        //     }
        //     return null;
        // });

        // var results = await Task.WhenAll(tasks);

        // var found = results.FirstOrDefault(entry => entry != null);


        if (found == null)
            return default;

        using (Stream stream = found.Open())
        using (StreamReader reader = new StreamReader(stream))
        {

            return _deserializer.Deserialize<T>(reader);
        }

    }

    public Task<IEnumerable<T>> GetByAttr<TValue>(string attribute, TValue value)
    {
        throw new NotImplementedException();
    }

    public Task<T[]> GetRandomUnbound(int number = 5)
    {
        throw new NotImplementedException();
    }

    public Task Put(T item)
    {
        throw new NotImplementedException();
    }
}